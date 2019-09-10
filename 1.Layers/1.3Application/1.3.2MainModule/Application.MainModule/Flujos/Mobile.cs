using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Mobile;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagas.MainModule.Entidades;
using Application.MainModule.Servicios.Ventas;
using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.Servicios.Compras;
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.DTOs.Mobile.PuntoVenta;
using Application.MainModule.AdaptadoresDTO.Mobile.Cortes;
using Application.MainModule.DTOs.Mobile.Cortes;
using System.Net.Http;
using Application.MainModule.AdaptadoresDTO.Mobile.VentaExtraordinaria;
using Utilities.MainModule;

namespace Application.MainModule.Flujos
{
    public class Mobile
    {

        public RespuestaOrdenesCompraDTO ConsultarOrdenesCompra(short IdEmpresa, bool EsGas, bool EsActivoVenta, bool EsTransporteGas)
        {
            return MobileOrdenesCompraServicio.Consultar(IdEmpresa, EsGas, EsActivoVenta, EsTransporteGas);
        }
        public RespuestaDto ConsultarOCAlternativa(int IdOrdenCompra)
        {
            return new RespuestaDto()
            {
                Id = MobileOrdenesCompraServicio.Consultar(IdOrdenCompra),
                Exito = true,
                Mensaje = "OK"
            };

        }
        public List<MenuDto> ObtenerMenu()
        {
            int idUsuario = TokenServicio.ObtenerIdUsuario();
            return MenuServicio.Crear(idUsuario);
        }
        public List<MedidorDto> ObtenerMedidores()
        {
            return TipoMedidorAdapter.ToDto(TipoMedidorGasServicio.Obtener());
        }
        public List<AlmacenDto> ObtenerAlmacenesGas()
        {
            return AlmacenAdapter.ToDto(AlmacenGasServicio.ObtenerAlmacenGeneral(TokenServicio.ObtenerIdEmpresa(), true));
        }
        public RespuestaDto RegistrarPapeleta(PapeletaDTO papeletaDto)
        {
            var resp = EntradaGasServicio.EvaluarClaveOperacion(papeletaDto);
            if (resp.Exito) return resp;

            resp = EntradaGasServicio.EvaluarExistenciaRegistro(papeletaDto);
            if (resp.Exito) return resp;

            /*Se genera lista de ordenes de compra para actualizar el estatus de estas */
            //List<OrdenCompra> ocs = new List<OrdenCompra>();
            //var ocp = OrdenComprasAdapter.FromEntity(OrdenCompraServicio.Buscar(papeletaDto.IdOrdenCompraPorteador));
            var oce = OrdenComprasAdapter.FromEntity(OrdenCompraServicio.Buscar(papeletaDto.IdOrdenCompraExpedidor));
            //ocp.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.Proceso_compra;
            //oce.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.EnComplementoCompra;
            //ocs.Add(ocp);
            //ocs.Add(oce);

            var papeleta = AlmacenAdapter.FromDto(papeletaDto);
            papeleta.IdRequisicion = oce.IdRequisicion;
            var almacen = CentroCostoServicio.Obtener(oce.IdCentroCosto).UnidadAlmacenGas;
            papeleta.IdCAlmacenGas = almacen.IdCAlmacenGas;
            papeleta.IdAlmacenGas = almacen.IdAlmacenGas;
            papeleta.IdTipoMedidorAlmacen = almacen.IdTipoMedidor;
            /* Fin cambio: JSA*/
            return EntradaGasServicio.RegistrarPapeleta(papeleta);
        }
        public RespuestaDto InicializarDescarga(DescargaDto desDto)
        {
            var resp = EntradaGasServicio.EvaluarClaveOperacion(desDto);
            if (resp.Exito) return resp;

            return EntradaGasServicio.Descargar(desDto);
        }
        public RespuestaDto FinalizarDescarga(DescargaDto desDto)
        {
            var resp = EntradaGasServicio.EvaluarClaveOperacion(desDto);
            if (resp.Exito) return resp;

            return EntradaGasServicio.Descargar(desDto, true);
        }
        public RespuestaDto InicializarTomaDeLectura(LecturaDTO liadto)
        {
            var resp = LecturaGasServicio.EvaluarClaveOperacion(liadto);
            if (resp.Exito) return resp;

            return LecturaGasServicio.Lectura(liadto);
        }
        public RespuestaDto FinalizarTomaDeLectura(LecturaDTO lfadto)
        {
            var resp = LecturaGasServicio.EvaluarClaveOperacion(lfadto);
            if (resp.Exito) return resp;

            return LecturaGasServicio.Lectura(lfadto, true);
        }
        public RespuestaDto IniciarTomaDeLecturaCamioneta(LecturaCamionetaDTO lcdto)
        {
            var resp = LecturaGasServicio.EvaluarClaveOperacion(lcdto);
            if (resp.Exito) return resp;

            return LecturaGasServicio.Lectura(lcdto);
        }
        public RespuestaDto FinalizarTomaDeLecturaCamioneta(LecturaCamionetaDTO lcdto)
        {
            var resp = LecturaGasServicio.EvaluarClaveOperacion(lcdto);
            if (resp.Exito) return resp;

            return LecturaGasServicio.Lectura(lcdto, true);
        }
        public DatosTomaLecturaDto ConsultaDatosTomaLectura(bool esEstacion, bool esPipa, bool esCamioneta, bool esFinalizar)
        {
            if (esEstacion)
                return LecturaGasServicio.ConsultaDatosTomaLecturaEstacionCarburacion(esFinalizar);

            if (esPipa)
                return LecturaGasServicio.ConsultaDatosTomaLecturaPipa(esFinalizar);

            if (esCamioneta)
                return LecturaGasServicio.ConsultaDatosTomaLecturaCamioneta(esFinalizar);

            return LecturaGasServicio.ConsultaDatosTomaLecturaAlmacenGeneral(esFinalizar);
        }
        public RespuestaDto IniciarRecargaCamioneta(RecargaDTO rdto)
        {
            var resp = RecargaGasServicio.EvaluarClaveOperacion(rdto);
            if (resp.Exito) return resp;

            return RecargaGasServicio.Recarga(rdto);
        }
        public RespuestaDto IniciarRecarga(RecargaDTO ridto)
        {
            var resp = RecargaGasServicio.EvaluarClaveOperacion(ridto);
            if (resp.Exito) return resp;

            return RecargaGasServicio.Recarga(ridto, false);
        }
        public RespuestaDto FinalizarRecarga(RecargaDTO rfdto)
        {
            var resp = RecargaGasServicio.EvaluarClaveOperacion(rfdto);
            if (resp.Exito) return resp;

            return RecargaGasServicio.Recarga(rfdto, true);
        }
        public DatosTomaLecturaDto CatalogoUnidades()
        {
            return LecturaGasServicio.ConsultaDatosReporteDelDia();
        }
        public RespuestaDto CatalogoEstaciones(bool esAnticipo, bool esCorteCaja)
        {
            return null;
        }
        public DatosTipoPersonaDto CatalogoTipoPersona()
        {
            return ClientesServicio.ConsultarTipoPersonas();
        }
        public RespuestaDto registrarCliente(ClienteDTO cliente)
        {
            //var resp = ClientesServicio.EvaluarCliente(cliente);
            // if (resp.IdCliente!=0)
            //     return ClientesServicio.Modificar(cliente,TokenServicio.ObtenerIdEmpresa());
            //else
            return ClientesServicio.Registar(cliente, TokenServicio.ObtenerIdEmpresa());
        }
        public DatosClientesDto BuscadorClientes(string criterio)
        {
            return ClientesServicio.BuscadorClientes(criterio);
        }
        public ReporteDiaDTO ReporteDia(DateTime fecha, short idCAlmacenGas)
        {
            var almacen = AlmacenGasServicio.ObtenerAlmacen(idCAlmacenGas);
            var resp = AlmacenGasServicio.BuscarReporteDia(fecha, idCAlmacenGas, almacen.IdEmpresa);
            if (resp != null) return AlmacenGasServicio.ReporteDiaExistente(resp, almacen);
            var ReporteAlmacen = AlmacenGasServicio.ReporteDia(fecha, idCAlmacenGas);
            return ReporteAlmacen;
        }
        public RespuestaDto Venta(VentaDTO venta)
        {
            var resp = VentaServicio.BuscarFolioVenta(venta);
            if (resp.Exito) return resp;

            var punto_venta = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
            var almacen = punto_venta.UnidadesAlmacen;
            var operador = PuntoVentaServicio.ObtenerOperador(TokenServicio.ObtenerIdUsuario());
            //var almacen = AlmacenGasServicio.Obtener(punto_venta.IdCAlmacenGas);
            var cliente = ClienteServicio.Obtener(venta.IdCliente);
            var ventas = CajaGeneralServicio.ObtenerVentas();
            int orden = Orden(ventas, venta.Fecha);
            var adapter = VentasEstacionesAdapter.FromDTO(venta, cliente, punto_venta, orden, TokenServicio.ObtenerIdEmpresa());

            adapter.OperadorChofer = operador.Nombre + " " + operador.Apellido1 + " " + operador.Apellido2;
            adapter.FolioVenta = venta.FolioVenta;
            adapter.FolioOperacionDia = venta.FolioVenta;
            adapter.FechaRegistro = DateTime.Now;
            adapter.Dia = (byte)venta.Fecha.Day;
            adapter.Mes = (byte)venta.Fecha.Month;
            adapter.Year = (short)venta.Fecha.Year;
            adapter.FechaAplicacion = venta.Fecha;
            adapter.DatosProcesados = false;
            adapter.RequiereFactura = venta.Factura;
            adapter.VentaACredito = venta.Credito;
            adapter.ClienteConCredito = venta.TieneCredito;

            if (venta.SinNumero || venta.IdCliente == 0)
            {
                Cliente clienteGenerico = ClienteServicio.BuscarClientePorRFC("XAXX010101000");
                adapter.IdCliente = clienteGenerico.IdCliente;
                adapter.RFC = clienteGenerico.Rfc;
                adapter.RazonSocial = clienteGenerico.RazonSocial;
            }
            else
            {
                adapter.IdCliente = venta.IdCliente;
                adapter.RFC = cliente.Rfc;
                adapter.RazonSocial = cliente.RazonSocial;
            }
            RespuestaDto respuesta = new RespuestaDto();

            #region Verifica si la venta que se realiza es extraordinaria
            if (venta.Credito)
            {
                if (cliente.CreditoDisponibleMonto == 0)
                {
                    RespuestaDto _res = new RespuestaDto();
                    if (!cliente.VentaExtraordinaria.Value)
                    {
                        resp.Exito = false;
                        resp.EsInsercion = false;
                        resp.EsActulizacion = false;
                        resp.Mensaje = "El cliente no tiene disponibilidad de credito, favor de comunicarse con el área de crédito y cobranza";
                        resp.Id = 0;
                        resp.Codigo = null;
                        resp.ModeloValido = false;
                        return resp;
                    }
                }
            }
            #endregion
            if (venta.Credito)
            {
                if (venta.VentaExtraordinaria)
                {
                    int dias = Convert.ToInt32(cliente.limiteCreditoDias);
                    var cargo = CargoAdapter.FromDTO(venta, DateTime.Now.AddDays(dias), TokenServicio.ObtenerIdEmpresa());

                    var insertCargo = PuntoVentaServicio.insertCargoMobile(cargo);
                    if (insertCargo.Exito)
                    {
                        if (cliente.CreditoDisponibleMonto == 0)
                        {
                            decimal creditoDisponible = cliente.limiteCreditoMonto - venta.Total;
                            cliente.CreditoDisponibleMonto = creditoDisponible;
                        }
                        else
                        {
                            if (cliente.CreditoDisponibleMonto > 0)
                            {
                                decimal creditoDisponibleMonto = cliente.CreditoDisponibleMonto - venta.Total;
                                cliente.CreditoDisponibleMonto = creditoDisponibleMonto;
                            }
                        }
                        var actualizaCredito = ClienteServicio.ModificarCredito(cliente);
                        if (actualizaCredito.Exito)
                            respuesta = PuntoVentaServicio.InsertMobile(adapter);
                        else
                            respuesta = actualizaCredito;
                    }
                    else
                        respuesta = insertCargo;
                }
                else
                {
                    #region Verifica si tiene credito disponible
                    if (cliente.CreditoDisponibleMonto > 0 && cliente.CreditoDisponibleMonto >= venta.Total)
                    {

                        int dias = Convert.ToInt32(cliente.limiteCreditoDias);
                        var cargo = CargoAdapter.FromDTO(venta, DateTime.Now.AddDays(dias), TokenServicio.ObtenerIdEmpresa());
                        var insertCargo = PuntoVentaServicio.insertCargoMobile(cargo);
                        if (insertCargo.Exito)
                        {
                            if (cliente.CreditoDisponibleMonto == 0)
                            {
                                decimal creditoDisponible = cliente.limiteCreditoMonto - venta.Total;
                                cliente.CreditoDisponibleMonto = creditoDisponible;
                            }
                            if (cliente.CreditoDisponibleMonto > 0)
                            {
                                decimal creditoDisponibleMonto = cliente.CreditoDisponibleMonto - venta.Total;
                                cliente.CreditoDisponibleMonto = creditoDisponibleMonto;
                            }

                            var actualizaCredito = ClienteServicio.ModificarCredito(cliente);
                            if (actualizaCredito.Exito)
                                respuesta = PuntoVentaServicio.InsertMobile(adapter);
                            else
                                respuesta = actualizaCredito;
                        }
                        else
                            respuesta = insertCargo;
                    }
                    else
                    {
                        RespuestaDto _res = new RespuestaDto();
                        resp.Exito = false;
                        resp.EsInsercion = false;
                        resp.EsActulizacion = false;
                        resp.Mensaje = "No se puede realizar la venta, favor de comunicarse con el área de crédito y cobranza";
                        resp.Id = 0;
                        resp.Codigo = null;
                        resp.ModeloValido = false;
                        return resp;
                    }
                    #endregion
                }
            }
            else
                respuesta = PuntoVentaServicio.InsertMobile(adapter);

            //var ventaPuntoDeVenta = PuntoVentaServicio.InsertMobile(adapter);
            //respuesta = ventaPuntoDeVenta;
            if (respuesta.Exito)
            {
                if (almacen.IdCamioneta > 0)
                {
                    verificaInventarioCilindros(venta);
                }

            }
            return respuesta;
        }
        public void verificaInventarioCilindros(VentaDTO venta)
        {
            var puntoVenta = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
            var almacen = puntoVenta.UnidadesAlmacen;
            var camioneta = almacen.Camioneta;
            var idEmpresa = TokenServicio.ObtenerIdEmpresa();

            foreach (ConceptoDTO concepto in venta.Concepto)
            {
                if (concepto.EsVentaCilindro)
                {
                    var cilindro = AlmacenGasServicio.BuscarCamionetaCilindro(almacen.IdCamioneta.Value, concepto.IdCilindro, idEmpresa);
                    CamionetaCilindro editar = new CamionetaCilindro();
                    editar.IdCamioneta = cilindro.IdCamioneta;
                    editar.IdEmpresa = cilindro.IdEmpresa;
                    editar.IdCilindro = cilindro.IdCilindro;
                    editar.Cantidad = cilindro.Cantidad - concepto.Cantidad;
                    var actualizar = AlmacenGasServicio.ActualizaCilindroCamioneta(editar);
                }
            }
        }
        public int Orden(List<VentaPuntoDeVenta> ventas, DateTime fechaVenta)
        {
            var busqueda = ventas.FindAll(x => x.FechaRegistro.Day.Equals(
                fechaVenta.Day) &&
                x.FechaRegistro.Month.Equals(fechaVenta.Month)
                && x.FechaRegistro.Year.Equals(fechaVenta.Year)
            );
            if (busqueda != null)
                if (busqueda.Count == 0)
                    return 0;
                else
                {
                    if (busqueda.Last().Orden > 0)
                        return busqueda.Last().Orden + 1;
                    else
                        return 1;
                }

            else
                return 0;
        }
        public DatosRecargaDto CatalogoRecargas(bool esEstacion, bool esPipa, bool esCamioneta)
        {
            var tipoMedidores = TipoMedidorGasServicio.Obtener();
            if (esCamioneta)
            {
                var almacenesAlternos = AlmacenGasServicio.ObtenerAlmacenGeneral(TokenServicio.ObtenerIdEmpresa(), true);
                var camionetas = AlmacenGasServicio.ObtenerCamionetas(TokenServicio.ObtenerIdEmpresa());
                var camionetasDTO = AlmacenRecargaAdapter.ToDTOCamionetas(camionetas, tipoMedidores);
                var Lcamionetas = new List<CamionetaDto>();
                foreach (var item in camionetasDTO)
                {
                    item.NombreAlmacen = camionetas.Find(x => x.IdCamioneta.Value.Equals(x.IdCamioneta)).Camioneta.Nombre;
                    Lcamionetas.Add(item);
                }
                return AlmacenRecargaAdapter.ToDTO(almacenesAlternos, Lcamionetas, tipoMedidores);
            }
            else if (esEstacion)
            {
                var pipas = AlmacenGasServicio.ObtenerPipas(TokenServicio.ObtenerIdEmpresa());
                var estaciones = AlmacenGasServicio.ObtenerEstaciones(TokenServicio.ObtenerIdEmpresa());

                return AlmacenRecargaAdapter.ToDTO(pipas, estaciones, tipoMedidores);
            }
            else if (esPipa)
            {
                var pipas = AlmacenGasServicio.ObtenerPipas(TokenServicio.ObtenerIdEmpresa());
                var estaciones = AlmacenGasServicio.ObtenerEstaciones(TokenServicio.ObtenerIdEmpresa());

                return AlmacenRecargaAdapter.ToDTO(pipas, estaciones, tipoMedidores);
            }
            return null;
        }
        /// <summary>
        /// UsuariosAnticiposCorte
        /// Permite retornar la lista de usuarios para 
        /// los anticipos o cortes de caja
        /// </summary>
        /// <returns></returns>
        public RespuestaDto UsuariosAnticiposCorte()
        {
            var usuario = TokenServicio.ObtenerUsuarioAplicacion();
            var empresa = usuario.Empresa;
            var usuariosEmpresa = EmpresaServicio.ObtenerUsuarios(empresa);
            var adapter = UsuariosCorteAdapter.ToDTO(empresa, usuariosEmpresa);
            return adapter;
        }
        public RespuestaDto Autoconsumo(AutoconsumoDTO dto, bool esFinal)
        {
            var resp = AutoconsumoServicio.EvaluarAutoconsumo(dto);

            if (resp.Exito) return resp;

            return AutoconsumoServicio.Autoconsumo(dto, esFinal);
        }
        public UsuariosCorteDTO UsuariosAnticiposCorteLiquidar()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());
            var usuarioSession = TokenServicio.ObtenerUsuarioAplicacion();
            var usuarios = EmpresaServicio.ObtenerUsuarios(empresa);
            List<Usuario> usuariosLiquidar = new List<Usuario>();

            if (usuarios != null && usuarios.Count != 0)
            {
                foreach (var usuario in usuarios)
                {
                    var usuariosRoles = usuario.UsuarioRoles;
                    foreach (var usuarioRol in usuariosRoles)
                    {
                        var rolAcciones = RolServicio.Obtener(usuarioRol);
                        if (rolAcciones.CatLiquidarCajaGeneral)
                        {
                            var buscar = usuariosLiquidar.Find(x => x.IdUsuario.Equals(usuario.IdUsuario));
                            if (buscar == null)
                                usuariosLiquidar.Add(usuario);
                        }
                    }

                }
            }
            var adapter = UsuariosCorteAdapter.ToDTO(empresa, usuariosLiquidar);
            return adapter;
        }
        public DatosAutoconsumoDto CatalogoAutoconsumo(bool esEstacion, bool esInventario, bool esPipas, bool esFinal)
        {
            var medidores = TipoMedidorGasServicio.Obtener();

            var autoconsumos = AlmacenGasServicio.ObtenerAutoConsumosNoProcesadas();

            List<Pipa> lpipas = new List<Pipa>();
            List<Camioneta> lcamionetas = new List<Camioneta>();
            List<EstacionCarburacion> lestaciones = new List<EstacionCarburacion>();


            if (esInventario)
            {
                var pipas = AlmacenGasServicio.ObtenerPipas(TokenServicio.ObtenerIdEmpresa());
                var camionetas = AlmacenGasServicio.ObtenerCamionetas(TokenServicio.ObtenerIdEmpresa());
                foreach (var unidadPipas in pipas)
                {
                    lpipas.Add(
                        unidadPipas.Pipa
                        );
                }
                foreach (var unidadCamioneta in camionetas)
                {
                    lcamionetas.Add(
                        unidadCamioneta.Camioneta
                        );
                }
                if (esFinal)
                {
                    //var estacionesInicioEnInicial = estacionesInicio(autoconsumos,false,true,true);

                    //if (estacionesInicioEnInicial.Count > 0)
                    //    return AlmacenAutoconsumoAdapter.ToDTOInventarioGeneral(estacionesInicioEnInicial,lpipas,lcamionetas,medidores);
                    //else
                    return AlmacenAutoconsumoAdapter.ToDTOInventarioGeneral(lpipas, lcamionetas, medidores);
                }
                else
                {
                    //var estacionesFinEnInicial = estacionesFin(autoconsumos, false, true, true);
                    //if(estacionesFinEnInicial.Count>0)
                    //    return AlmacenAutoconsumoAdapter.ToDTOInventarioGeneral(estacionesFinEnInicial, lpipas, lcamionetas, medidores);
                    //else
                    return AlmacenAutoconsumoAdapter.ToDTOInventarioGeneral(lpipas, lcamionetas, medidores);
                }


            }
            else
            {
                var puntoVenta = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
                var pipas = AlmacenGasServicio.ObtenerPipas(puntoVenta.IdEmpresa);
                var camionetas = AlmacenGasServicio.ObtenerCamionetas(puntoVenta.IdEmpresa);
                var almacenes = AlmacenGasServicio.ObtenerAlmacenes(puntoVenta.IdEmpresa);
                var estaciones = AlmacenGasServicio.ObtenerEstaciones(puntoVenta.IdEmpresa);
                var predeterminado = puntoVenta.UnidadesAlmacen;
                foreach (var unidadPipas in pipas)
                {
                    lpipas.Add(
                        unidadPipas.Pipa
                        );
                }
                foreach (var unidadCamioneta in camionetas)
                {
                    lcamionetas.Add(
                        unidadCamioneta.Camioneta
                        );
                }
                foreach (var unidadEstacion in estaciones)
                {
                    lestaciones.Add(
                        unidadEstacion.EstacionCarburacion
                        );
                }
                if (esEstacion)
                {

                    if (esFinal)
                    {
                        var estacionesInicioEnInicial = estacionesInicio(autoconsumos);
                        if (estacionesInicioEnInicial.Count > 0)
                            return AlmacenAutoconsumoAdapter.ToDTO(estacionesInicioEnInicial, lestaciones, lpipas, lcamionetas, medidores);
                        else
                            return AlmacenAutoconsumoAdapter.ToDTO(lestaciones, lpipas, lcamionetas, medidores);
                    }
                    else
                    {
                        var estacionesFinEnInicial = estacionesFin(autoconsumos);
                        if (estacionesFinEnInicial.Count > 0)
                            return AlmacenAutoconsumoAdapter.ToDTO(estacionesFinEnInicial, lestaciones, lpipas, lcamionetas, medidores);
                        else
                            return AlmacenAutoconsumoAdapter.ToDTO(lestaciones, lpipas, lcamionetas, medidores);
                    }


                }
                else if (esPipas)
                {

                    if (esFinal)
                    {
                        var estacionesInicioEnInicial = estacionesInicio(autoconsumos, false, true, true);

                        if (estacionesInicioEnInicial.Count > 0)
                            return AlmacenAutoconsumoAdapter.ToDTOPipas(estacionesInicioEnInicial, lestaciones, lpipas, lcamionetas, medidores);
                        else
                            return AlmacenAutoconsumoAdapter.ToDTOPipas(lpipas, lcamionetas, medidores);
                    }
                    else
                    {
                        var estacionesFinEnInicial = estacionesFin(autoconsumos, false, true, true);
                        if (estacionesFinEnInicial.Count > 0)
                            return AlmacenAutoconsumoAdapter.ToDTOPipas(estacionesFinEnInicial, lestaciones, lpipas, lcamionetas, medidores);
                        else
                            return AlmacenAutoconsumoAdapter.ToDTOPipas(lpipas, lcamionetas, medidores);
                    }

                }
            }

            return null;
        }
        /// <summary>
        /// Permite realizar la comprovacion antes del corte si 
        /// ya se tienen o ya se ha echo una lectura final
        /// </summary>
        /// <param name="idCAlmacenGas">Id del almacen de Gas (IdCAlmacenGas)</param>
        /// <returns>Respuesta resultante de la comprobacion</returns>
        public RespuestaDto GetHayLecturaFinal(short idCAlmacenGas)
        {
            var almacen = AlmacenGasServicio.ObtenerAlmacen(idCAlmacenGas);
            RespuestaDto respuesta = new RespuestaDto();
            if (almacen != null)
            {
                var lecturaIncial = AlmacenGasServicio.ObtenerLecturaFinal(DateTime.Now, idCAlmacenGas);

                if (lecturaIncial != null)
                {
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se ha encontrado una lectura final " + lecturaIncial.ClaveOperacion;
                }
                else
                {
                    respuesta.Exito = false;
                    if (almacen.IdCamioneta != null && almacen.IdCamioneta > 0)
                        respuesta.Mensaje = "No se ha realiado una lectura final para la camioneta: " + almacen.Camioneta.Nombre;
                    else if (almacen.IdPipa != null && almacen.IdPipa > 0)
                        respuesta.Mensaje = "No se ha realizado la lectura final para la pipa: " + almacen.Pipa.Nombre;
                    else if (almacen.IdEstacionCarburacion != null && almacen.IdEstacionCarburacion > 0)
                        respuesta.Mensaje = "No se ha realizado la lectura final para la estación: " + almacen.EstacionCarburacion.Nombre;
                    else
                        respuesta.Mensaje = "No se ha realizado la lectura final para el almacen: " + almacen.Numero;
                }
            }
            else
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "El alamcen no fue encontrado o no esta disponible, intente nuevamente";
            }
            return respuesta;

        }
        /// <summary>
        /// Permite determinar si actualmente se cuentas con la lectura incial y 
        /// final en el almacen del día de hoy, en caso de no contar con ellas 
        /// se retornara un mensaje de error en un modelo RespuestaDTO
        /// </summary>
        /// <returns>Modelo de tipo RespuestaDTO con el resultado </returns>
        public RespuestaDto HayLectura()
        {
            RespuestaDto respuesta = new RespuestaDto();
            var puntoVenta = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
            var unidadAlmacen = AlmacenGasServicio.ObtenerAlmacen(puntoVenta.IdCAlmacenGas);
            var lecturaInicial = AlmacenGasServicio.BuscarLectura(unidadAlmacen.IdCAlmacenGas, DateTime.Now);
            var lecturaFinal = AlmacenGasServicio.BuscarLectura(unidadAlmacen.IdCAlmacenGas, DateTime.Now, false);
            if (lecturaInicial != null && lecturaFinal != null)
            {
                respuesta.Exito = true;
                respuesta.Mensaje = "Exito si hay lecturas";
            }
            else
            {
                if (lecturaInicial == null)
                {
                    respuesta.Mensaje = "Para poder continuar es necesario haber realizado la lectura inicial del día";
                }
                if (lecturaFinal == null)
                {
                    if (respuesta.Mensaje != null || respuesta.Mensaje == "")
                        respuesta.Mensaje += " y es necesario registrar su lectura final de día";
                    else
                        respuesta.Mensaje = "Para poder continuar es necesario haber realizado la lectura final del día";
                }
            }
            return respuesta;
        }
        /// <summary>
        /// Retorna si actualmente se cuenta conuna lectura inicial registrada en 
        /// la estación, pipa o camioneta para arrancar su día  
        /// </summary>
        /// <returns>RespuestaDTO con el resultado de esta consulta</returns>
        public RespuestaDto VerificarLecturaInicial()
        {
            var usuario = TokenServicio.ObtenerUsuarioAplicacion();
            var operadorChofer = usuario.OperadoresChoferes;
            RespuestaDto respuesta = new RespuestaDto();
            if (operadorChofer != null)
            {
                var puntoVenta = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
                if (puntoVenta != null)
                {
                    var unidadAlmacen = puntoVenta.UnidadesAlmacen;
                    if (unidadAlmacen != null)
                    {
                        var LecturaInicialHoy = LecturaGasServicio.ObtenerUltimaLecturaInicial(unidadAlmacen.IdCAlmacenGas, DateTime.Now);
                        if (LecturaInicialHoy != null)
                            respuesta = new RespuestaDto()
                            {
                                Exito = true,
                                Mensaje = "Hay una lectura inicial "
                            };
                        else
                            respuesta = new RespuestaDto()
                            {
                                Exito = false,
                                Mensaje = "No se ha realizado una lectura inicial"
                            };
                    }
                }
            }
            return respuesta;
        }
        /// <summary>
        /// Permite retornar por medio de la session activa
        /// si es un chofer , su nombre de punto de venta 
        /// </summary>
        /// <returns>Objeto PuntoVentaAsignadoDTO con lso datos encontrados</returns>
        public PuntoVentaAsignadoDTO ObtenerEstacion()
        {
            var usuario = TokenServicio.ObtenerUsuarioAplicacion();
            var operador = PuntoVentaServicio.ObtenerOperador(usuario.IdUsuario);
            //var puntoVenta = PuntoVentaServicio.Obtener(operador.IdOperadorChofer);
            var puntoVenta = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
            var unidadAlmacen = puntoVenta.UnidadesAlmacen;
            PuntoVentaAsignadoDTO pvaDto = new PuntoVentaAsignadoDTO();
            if (unidadAlmacen.IdPipa > 0)
            {
                Pipa pipaAsignada = unidadAlmacen.Pipa;
                pvaDto = PuntoVentaAdapter.ToDTO(usuario, operador, puntoVenta, unidadAlmacen, pipaAsignada);
            }
            if (unidadAlmacen.IdCamioneta > 0)
            {
                Camioneta camionetaAsignada = unidadAlmacen.Camioneta;
                pvaDto = PuntoVentaAdapter.ToDTO(usuario, operador, puntoVenta, unidadAlmacen, camionetaAsignada);
            }
            if (unidadAlmacen.IdEstacionCarburacion > 0)
            {
                EstacionCarburacion estacionAsignada = unidadAlmacen.EstacionCarburacion;
                pvaDto = PuntoVentaAdapter.ToDTO(usuario, operador, puntoVenta, unidadAlmacen, estacionAsignada);
            }
            return pvaDto;
        }
        /// <summary>
        /// GetHayCorte
        /// Permite realizar la busqueda de un corte en la estación 
        /// por una fecha especifica, se envia como parametros ,el dia
        /// el mesy la fecha y se retornara una respuesta de la busqueda
        /// </summary>
        /// <param name="dia">Dia que se desea consultar Ej. 1</param>
        /// <param name="mes">Mes que se desea consultar Ej. 12</param>
        /// <param name="year">Año que se desea consultar Ej. 2018</param>
        /// <returns>Respuesta de dicha busqueda</returns>
        public DatosCortesAntesVentaDTO GetHayCorte(DateTime fecha)
        {
            var puntoVenta = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
            var corte = PuntoVentaServicio.buscarCorte(fecha, puntoVenta.IdCAlmacenGas);
            DatosCortesAntesVentaDTO _respuesta = new DatosCortesAntesVentaDTO();
            if (corte != null)
            {

                _respuesta.HayCorte = true;
                _respuesta.Corte = AnticiposCortesAdapter.ToDTO(corte);
            }
            else
            {

                _respuesta.HayCorte = false;
                _respuesta.Corte = null;

            }
            return _respuesta;
        }
        public List<UnidadAlmacenGas> estacionesInicio(List<AlmacenGasAutoConsumo> autoconsumos, bool estaciones = true, bool pipas = false, bool camionetas = false)
        {
            List<UnidadAlmacenGas> list = new List<UnidadAlmacenGas>();
            if (estaciones)
            {
                foreach (var autoconsumo in autoconsumos)
                {
                    var almacen = AlmacenGasServicio.ObtenerUnidadAlamcenGas(autoconsumo.IdCAlmacenGasSalida);
                    if (almacen.IdEstacionCarburacion != null && almacen.IdEstacionCarburacion > 0)
                        list.Add(almacen);
                }
            }
            if (pipas)
            {
                foreach (var autoconsumo in autoconsumos)
                {
                    var almacen = AlmacenGasServicio.ObtenerUnidadAlamcenGas(autoconsumo.IdCAlmacenGasSalida);
                    if (almacen.IdPipa != null && almacen.IdPipa > 0)
                        list.Add(almacen);
                }
            }
            if (camionetas)
            {
                foreach (var autoconsumo in autoconsumos)
                {
                    var almacen = AlmacenGasServicio.ObtenerUnidadAlamcenGas(autoconsumo.IdCAlmacenGasSalida);
                    if (almacen.IdCamioneta != null && almacen.IdCamioneta > 0)
                        list.Add(almacen);
                }
            }
            return list;
        }
        public DatosAnticiposCorteDto CatalogoVentasAnticiposCorte(int idEstacion, bool esAnticipos, DateTime fecha)
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var unidadAlmacen = PuntoVentaServicio.ObtenerPorUsuarioAplicacion().UnidadesAlmacen;
            Pipa pipa = null;
            EstacionCarburacion estacion = null;
            Camioneta camioneta = null;
            DatosAnticiposCorteDto dto = new DatosAnticiposCorteDto();
            if (unidadAlmacen.IdEstacionCarburacion > 0 && unidadAlmacen.IdEstacionCarburacion != null)
            {
                estacion = unidadAlmacen.EstacionCarburacion;
                var puntoVenta = unidadAlmacen.PuntosVenta.First(x => x.IdCAlmacenGas.Equals(unidadAlmacen.IdCAlmacenGas));
                var anticipos = PuntoVentaServicio.ObtenerAnticipos(unidadAlmacen).FindAll(x => x.FechaCorteAnticipo.Day.Equals(fecha.Day) &&
                x.FechaCorteAnticipo.Month.Equals(fecha.Month) && x.FechaCorteAnticipo.Year.Equals(fecha.Year) && x.IdTipoOperacion.Equals(1)).ToList();
                var ventas = puntoVenta.VentaPuntoDeVenta;
                var ventasActivas = ventas.Where(x => x.Dia.Equals((byte)fecha.Day) && x.Mes.Equals((byte)fecha.Month) && x.Year.Equals((short)fecha.Year));
                var ventasSinCorte = new List<VentaPuntoDeVenta>();
                foreach (var ventaActiva in ventasActivas)
                {
                    //if (ventaActiva.FolioOperacionDia == null)
                    ventasSinCorte.Add(ventaActiva);
                }
                dto = AnticiposCortesAdapter.ToDTOPipa(ventasSinCorte, anticipos, unidadAlmacen, esAnticipos);
            }

            if (unidadAlmacen.IdPipa > 0 && unidadAlmacen.IdPipa != null)
            {
                pipa = unidadAlmacen.Pipa;
                var puntoVenta = unidadAlmacen.PuntosVenta.First(x => x.IdCAlmacenGas.Equals(unidadAlmacen.IdCAlmacenGas));
                var anticipos = PuntoVentaServicio.ObtenerAnticipos(unidadAlmacen).FindAll(x => x.FechaCorteAnticipo.Day.Equals(fecha.Day) &&
                x.FechaCorteAnticipo.Month.Equals(fecha.Month) && x.FechaCorteAnticipo.Year.Equals(fecha.Year) && x.IdTipoOperacion.Equals(1)).ToList();
                var ventas = puntoVenta.VentaPuntoDeVenta;
                var ventasActivas = ventas.Where(x => x.Dia.Equals((byte)fecha.Day) && x.Mes.Equals((byte)fecha.Month) && x.Year.Equals((short)fecha.Year));
                var ventasSinCorte = new List<VentaPuntoDeVenta>();
                foreach (var ventaActiva in ventasActivas)
                {
                    //if (ventaActiva.FolioOperacionDia==null)
                    ventasSinCorte.Add(ventaActiva);
                }
                dto = AnticiposCortesAdapter.ToDTOPipa(ventasSinCorte, anticipos, unidadAlmacen, esAnticipos);

            }

            if (unidadAlmacen.IdCamioneta > 0 && unidadAlmacen.IdCamioneta != null)
            {
                camioneta = unidadAlmacen.Camioneta;
                var puntoVenta = unidadAlmacen.PuntosVenta.First(x => x.IdCAlmacenGas.Equals(unidadAlmacen.IdCAlmacenGas));
                var anticipos = PuntoVentaServicio.ObtenerAnticipos(unidadAlmacen).FindAll(x => x.FechaCorteAnticipo.Day.Equals(fecha.Day) &&
                x.FechaCorteAnticipo.Month.Equals(fecha.Month) && x.FechaCorteAnticipo.Year.Equals(fecha.Year) && x.IdTipoOperacion.Equals(1)).ToList();
                var ventas = puntoVenta.VentaPuntoDeVenta;
                var ventasActivas = ventas.Where(x => x.Dia.Equals((byte)fecha.Day) && x.Mes.Equals((byte)fecha.Month) && x.Year.Equals((short)fecha.Year));
                var ventasSinCorte = new List<VentaPuntoDeVenta>();
                foreach (var ventaActiva in ventasActivas)
                {
                    //if (ventaActiva.FolioOperacionDia == null)
                    ventasSinCorte.Add(ventaActiva);
                }
                dto = AnticiposCortesAdapter.ToDTOPipa(ventasSinCorte, anticipos, unidadAlmacen, esAnticipos);
            }

            return dto;
            /* var pipas = AlmacenGasServicio.ObtenerPipasEmpresa(TokenServicio.ObtenerIdEmpresa());
             var estaciones = AlmacenGasServicio.ObtenerEstacionesEmpresa(TokenServicio.ObtenerIdEmpresa());
             var camionetas = AlmacenGasServicio.ObtenerCamionetasEmpresa(TokenServicio.ObtenerIdEmpresa());
             var pipa = pipas.SingleOrDefault(x => x.IdPipa.Equals(idEstacion));
             var estacion = estaciones.SingleOrDefault(x => x.IdEstacionCarburacion.Equals(idEstacion));
             var camioneta = camionetas.SingleOrDefault(x => x.IdCamioneta.Equals(idEstacion));
             PuntoVenta puntoVenta = null;
             UnidadAlmacenGas almacen = null;
             if (pipa != null)
             {
                 puntoVenta = pipa.UnidadAlmacenGas.First().PuntosVenta.First();
                 almacen = pipa.UnidadAlmacenGas.First();
             }
             else if (estacion != null)
             {
                 puntoVenta = estacion.UnidadAlmacenGas.First().PuntosVenta.First();
                 almacen = estacion.UnidadAlmacenGas.First();
             }
             else if (camioneta != null)
             {
                 puntoVenta = camioneta.UnidadAlmacenGas.First().PuntosVenta.First();
                 almacen = camioneta.UnidadAlmacenGas.First();
             }
             //var puntosVenta = PuntoVentaServicio.ObtenerIdEmp(TokenServicio.ObtenerIdEmpresa()).FirstOrDefault(x => x.IdCAlmacenGas.Equals(almacen.IdCAlmacenGas));

             //var puntosVenta = almacen.PuntosVenta.First();
             var ventas = CajaGeneralServicio.ObtenerVentasPuntosVenta(puntoVenta.IdPuntoVenta).OrderBy(x => x.FechaRegistro).ToList();
             if (fecha != null)
             {
                 ventas = ventas.FindAll(x => x.FechaRegistro.Day.Equals(fecha.Day) && x.FechaRegistro.Month.Equals(fecha.Month) && x.FechaRegistro.Year.Equals(fecha.Year));
             }

             var anticiposDia = PuntoVentaServicio.ObtenerAnticipos(puntoVenta.UnidadesAlmacen, fecha);

             return AnticiposCortesAdapter.ToDTO(ventas, anticiposDia,almacen, esAnticipos);*/
        }
        /// <summary>
        /// Permite realizar la busqueda de los datos para los cortes o anticipos de la sección de mobile
        /// se retornara un objeto DatosBusquedaCortesDTO con los datos encontrados en este. Se requieren como 
        /// parametros el idCAlmacenGas(idEstacion) a consultar, un boolean que determina si es anticipos o corte 
        /// de cajas y la fecha de busqeuda de los datos 
        /// </summary>
        /// <param name="idEstacion">Id del idCAlmacenGas de la pipa, estación de carburación o camioneta </param>
        /// <param name="esAnticipos">Bandera que determina si es anticipos(true) o corte de caja(false)</param>
        /// <param name="fecha">Variable con la fecha en la que se desea realizar el corte o anticipo </param>
        /// <returns>Objeto de tipo DatosBusquedaCortesDTO con los datos del corte o el anticipo a realizar , las ventas y la estación</returns>
        public DatosBusquedaCortesDTO BusquedaAnticipoCorteFecha(int idEstacion, bool esAnticipos, DateTime fecha)
        {
            Usuario usuario = TokenServicio.ObtenerUsuarioAplicacion();
            var unidadAlmacen = PuntoVentaServicio.ObtenerPorUsuarioAplicacion().UnidadesAlmacen;
            Pipa pipa = null;
            EstacionCarburacion estacion = null;
            Camioneta camioneta = null;
            DatosBusquedaCortesDTO dto = new DatosBusquedaCortesDTO();
            #region Datos para la pipa
            if (unidadAlmacen.IdPipa > 0 && unidadAlmacen.IdPipa != null)
            {
                var lectInicial = AlmacenGasServicio.BuscarUltimaLectura(unidadAlmacen.IdCAlmacenGas, TipoEventoEnum.Inicial);
                var lectFinal = AlmacenGasServicio.BuscarUltimaLectura(unidadAlmacen.IdCAlmacenGas, TipoEventoEnum.Final);
                pipa = unidadAlmacen.Pipa;
                var puntoVenta = unidadAlmacen.PuntosVenta.First(x => x.IdCAlmacenGas.Equals(unidadAlmacen.IdCAlmacenGas));
                var cortes = puntoVenta.VentaCorteAnticipoEC.Where(x =>
                    x.TipoOperacion.Equals(2) &&
                    x.FechaCorteAnticipo.Day.Equals(fecha.Day) &&
                    x.FechaCorteAnticipo.Month.Equals(fecha.Month) &&
                    x.FechaCorteAnticipo.Year.Equals(fecha.Year)
                ).ToList();
                var anticipos = puntoVenta.VentaCorteAnticipoEC.Where(x =>
                    x.IdTipoOperacion.Equals(1) &&
                    x.FechaCorteAnticipo.Day.Equals(fecha.Day) &&
                    x.FechaCorteAnticipo.Month.Equals(fecha.Month) &&
                    x.FechaCorteAnticipo.Year.Equals(fecha.Year)
                ).ToList();
                var ventas = puntoVenta.VentaPuntoDeVenta;
                var ventasActivas = ventas.Where(x => x.Dia.Equals((byte)fecha.Day) && x.Mes.Equals((byte)fecha.Month) && x.Year.Equals((short)fecha.Year));
                var ventasSinCorte = new List<VentaPuntoDeVenta>();
                foreach (var ventaActiva in ventasActivas)
                {
                    //if(ventaActiva.FolioOperacionDia == null || ventaActiva.FolioOperacionDia.Equals(ventaActiva.FolioVenta))
                    ventasSinCorte.Add(ventaActiva);
                }

                #region Creo el objeto a retornar 
                if (ventasSinCorte.Count > 0)
                {
                    dto = AnticiposCortesAdapter.ToDTOBuscador(
                            unidadAlmacen,
                            anticipos,
                            cortes,
                            ventasSinCorte,
                            pipa,
                            esAnticipos,
                            lectInicial,
                            lectFinal
                        );
                    #region Verifico si hay datos de anticipos 
                    if (anticipos != null)
                        if (anticipos.Count > 0)
                            dto.hayAnticipos = true;
                        else
                            dto.hayAnticipos = false;
                    else
                        dto.hayAnticipos = false;
                    #endregion
                    #region Verifico si hay cortes 
                    if (cortes != null)
                        if (cortes.Count > 0)
                            dto.hayCortes = true;
                        else
                            dto.hayCortes = false;
                    else
                        dto.hayCortes = false;
                    #endregion
                    #region Verifico si hay ventas en esta fecha 
                    if (ventas != null)
                        if (ventas.Count > 0)
                            dto.hayVentas = true;
                        else
                            dto.hayVentas = false;
                    else
                        dto.hayVentas = false;
                    #endregion
                    dto.Exito = true;
                    dto.Mensaje = "Ok";
                    dto.ModeloValido = true;
                }
                else
                {
                    dto.Exito = false;
                    dto.Mensaje = esAnticipos ? "No se encontraron ventas para realizar un anticipo en la fecha " + fecha.ToString("MM/dd/yyyy") :
                        "No se encontraron ventas para realizar un corte en la fecha " + fecha.ToString("MM/dd/yyyy");
                    dto.ModeloValido = false;
                    dto.EsInsercion = false;
                    dto.EsActulizacion = false;
                }
                #endregion
            }
            #endregion
            #region Datos para la estacion
            if (unidadAlmacen.IdEstacionCarburacion > 0 && unidadAlmacen.IdEstacionCarburacion != null)
            {
                var lectInicial = AlmacenGasServicio.BuscarUltimaLectura(unidadAlmacen.IdCAlmacenGas, TipoEventoEnum.Inicial);
                var lectFinal = AlmacenGasServicio.BuscarUltimaLectura(unidadAlmacen.IdCAlmacenGas, TipoEventoEnum.Final);
                estacion = unidadAlmacen.EstacionCarburacion;
                var puntoVenta = unidadAlmacen.PuntosVenta.First(x => x.IdCAlmacenGas.Equals(unidadAlmacen.IdCAlmacenGas));
                var cortes = puntoVenta.VentaCorteAnticipoEC.Where(x =>
                    x.TipoOperacion.Equals(2) &&
                    x.FechaCorteAnticipo.Day.Equals(fecha.Day) &&
                    x.FechaCorteAnticipo.Month.Equals(fecha.Month) &&
                    x.FechaCorteAnticipo.Year.Equals(fecha.Year)
                ).ToList();
                var anticipos = puntoVenta.VentaCorteAnticipoEC.Where(x =>
                    x.IdTipoOperacion.Equals(1) &&
                    x.FechaCorteAnticipo.Day.Equals(fecha.Day) &&
                    x.FechaCorteAnticipo.Month.Equals(fecha.Month) &&
                    x.FechaCorteAnticipo.Year.Equals(fecha.Year)
                ).ToList();
                var ventas = puntoVenta.VentaPuntoDeVenta;
                var ventasActivas = ventas.Where(x => x.Dia.Equals((byte)fecha.Day) && x.Mes.Equals((byte)fecha.Month) && x.Year.Equals((short)fecha.Year));
                var ventasSinCorte = new List<VentaPuntoDeVenta>();

                foreach (var ventaActiva in ventasActivas)
                {
                    //if (ventaActiva.FolioOperacionDia == null || ventaActiva.FolioOperacionDia.Equals(ventaActiva.FolioVenta))
                    ventasSinCorte.Add(ventaActiva);
                }
                if (ventasSinCorte.Count > 0)
                {
                    dto = AnticiposCortesAdapter.ToDTOBuscador(
                        unidadAlmacen,
                        anticipos,
                        cortes,
                        ventasSinCorte,
                        estacion,
                        esAnticipos,
                        lectInicial,
                        lectFinal
                    );
                    #region Verifico si hay datos de anticipos 
                    if (anticipos != null)
                        if (anticipos.Count > 0)
                            dto.hayAnticipos = true;
                        else
                            dto.hayAnticipos = false;
                    else
                        dto.hayAnticipos = false;
                    #endregion
                    #region Verifico si hay cortes 
                    if (cortes != null)
                        if (cortes.Count > 0)
                            dto.hayCortes = true;
                        else
                            dto.hayCortes = false;
                    else
                        dto.hayCortes = false;
                    #endregion
                    #region Verifico si hay ventas en esta fecha 
                    if (ventas != null)
                        if (ventas.Count > 0)
                            dto.hayVentas = true;
                        else
                            dto.hayVentas = false;
                    else
                        dto.hayVentas = false;
                    #endregion
                    dto.Exito = true;
                    dto.Mensaje = "Ok";
                    dto.ModeloValido = true;
                }
                else
                {
                    dto.Exito = false;
                    dto.Mensaje = esAnticipos ? "No se encontraron ventas para realizar un anticipo en la fecha " + fecha.ToString("MM/dd/yyyy") :
                        "No se encontraron ventas para realizar un corte en la fecha " + fecha.ToString("MM/dd/yyyy");
                    dto.ModeloValido = false;
                    dto.EsInsercion = false;
                    dto.EsActulizacion = false;
                }
            }
            #endregion
            #region Datos para la camioneta
            if (unidadAlmacen.IdCamioneta > 0 && unidadAlmacen.IdCamioneta != null)
            {
                camioneta = unidadAlmacen.Camioneta;
                var puntoVenta = unidadAlmacen.PuntosVenta.First(x => x.IdCAlmacenGas.Equals(unidadAlmacen.IdCAlmacenGas));
                var cortes = puntoVenta.VentaCorteAnticipoEC.Where(x =>
                    x.TipoOperacion.Equals(2) &&
                    x.FechaCorteAnticipo.Day.Equals(fecha.Day) &&
                    x.FechaCorteAnticipo.Month.Equals(fecha.Month) &&
                    x.FechaCorteAnticipo.Year.Equals(fecha.Year)
                ).ToList();
                var anticipos = puntoVenta.VentaCorteAnticipoEC.Where(x =>
                    x.IdTipoOperacion.Equals(1) &&
                    x.FechaCorteAnticipo.Day.Equals(fecha.Day) &&
                    x.FechaCorteAnticipo.Month.Equals(fecha.Month) &&
                    x.FechaCorteAnticipo.Year.Equals(fecha.Year)
                ).ToList();
                var ventas = puntoVenta.VentaPuntoDeVenta;
                var ventasActivas = ventas.Where(x => x.Dia.Equals((byte)fecha.Day) && x.Mes.Equals((byte)fecha.Month) && x.Year.Equals((short)fecha.Year));
                var ventasSinCorte = new List<VentaPuntoDeVenta>();
                foreach (var ventaActiva in ventasActivas)
                {
                    //if (ventaActiva.FolioOperacionDia==null || ventaActiva.FolioOperacionDia.Equals(ventaActiva.FolioVenta))
                    ventasSinCorte.Add(ventaActiva);
                }
                if (ventasSinCorte.Count > 0)
                {
                    dto = AnticiposCortesAdapter.ToDTOBuscador(
                        unidadAlmacen,
                        anticipos,
                        cortes,
                        ventasSinCorte,
                        camioneta,
                        esAnticipos
                    );
                    #region Verifico si hay datos de anticipos 
                    if (anticipos != null)
                        if (anticipos.Count > 0)
                            dto.hayAnticipos = true;
                        else
                            dto.hayAnticipos = false;
                    else
                        dto.hayAnticipos = false;
                    #endregion
                    #region Verifico si hay cortes 
                    if (cortes != null)
                        if (cortes.Count > 0)
                            dto.hayCortes = true;
                        else
                            dto.hayCortes = false;
                    else
                        dto.hayCortes = false;
                    #endregion
                    #region Verifico si hay ventas en esta fecha 
                    if (ventas != null)
                        if (ventas.Count > 0)
                            dto.hayVentas = true;
                        else
                            dto.hayVentas = false;
                    else
                        dto.hayVentas = false;
                    #endregion
                    dto.Exito = true;
                    dto.Mensaje = "Ok";
                    dto.ModeloValido = true;
                }
                else
                {
                    dto.Exito = false;
                    dto.Mensaje = esAnticipos ? "No se encontraron ventas para realizar un anticipo en la fecha " + fecha.ToString("MM/dd/yyyy") :
                        "No se encontraron ventas para realizar un corte en la fecha " + fecha.ToString("MM/dd/yyyy");
                    dto.ModeloValido = false;
                    dto.EsInsercion = false;
                    dto.EsActulizacion = false;
                }
            }
            #endregion
            return dto;
        }
        /// <summary>
        /// Permite determinar si el cliente que se envía de parametro en la consulta 
        /// se le ha permitido una venta extraforanea, este retornara un  objeto 
        /// DTO con el resultado de la conslta
        /// </summary>
        /// <param name="idCliente">Id del cliente a consultar</param>
        /// <returns>Modelo DTO con la respuesta de la consulta </returns>
        public DatosVentaExtraforaneaDTO tieneVentaExtraforanea(short idCliente)
        {
            var cliente = ClienteServicio.Obtener(idCliente);

            var adapter = PuntoVentaAdapter.ToDTO(cliente);
            adapter.Exito = true;
            adapter.ModeloValido = true;
            adapter.Mensaje = "Exito";
            adapter.Id = cliente.IdCliente;
            return adapter;
        }
        public RespuestaDto Calibracion(CalibracionDto dto, bool esFinal)
        {
            var resp = CalibracionServicio.EvaluarClaveOperacion(dto);

            if (resp.Exito) return resp;

            return CalibracionServicio.Calibracion(dto, esFinal);
        }
        public DatosCalibracionDto CatalogoCalibracion(bool esEstacion, bool esPipa)
        {
            var medidores = TipoMedidorGasServicio.Obtener();
            if (esEstacion && esPipa)
            {
                return null;
            }
            else
            {
                if (esEstacion)
                {
                    var estaciones = AlmacenGasServicio.ObtenerEstaciones(TokenServicio.ObtenerIdEmpresa());
                    return CalibracionAdapter.ToDTOEstaciones(estaciones, medidores);
                }
                else if (esPipa)
                {
                    var pipas = AlmacenGasServicio.ObtenerPipas(TokenServicio.ObtenerIdEmpresa());
                    return CalibracionAdapter.ToDTO(pipas, medidores);
                }
            }

            return null;
        }
        public List<UnidadAlmacenGas> estacionesFin(List<AlmacenGasAutoConsumo> autoconsumos, bool estaciones = true, bool pipas = false, bool camionetas = false)
        {
            List<UnidadAlmacenGas> list = new List<UnidadAlmacenGas>();
            if (estaciones)
            {
                foreach (var autoconsumo in autoconsumos)
                {
                    var almacen = AlmacenGasServicio.ObtenerUnidadAlamcenGas(autoconsumo.IdCAlmacenGasEntrada);
                    if (almacen.IdEstacionCarburacion != null && almacen.IdEstacionCarburacion > 0)
                        list.Add(almacen);
                }
            }
            if (pipas)
            {
                foreach (var autoconsumo in autoconsumos)
                {
                    var almacen = AlmacenGasServicio.ObtenerUnidadAlamcenGas(autoconsumo.IdCAlmacenGasEntrada);
                    if (almacen.IdPipa != null && almacen.IdPipa > 0)
                        list.Add(almacen);
                }
            }
            if (camionetas)
            {
                foreach (var autoconsumo in autoconsumos)
                {
                    var almacen = AlmacenGasServicio.ObtenerUnidadAlamcenGas(autoconsumo.IdCAlmacenGasEntrada);
                    if (almacen.IdCamioneta != null && almacen.IdCamioneta > 0)
                        list.Add(almacen);
                }
            }
            return list;
        }
        public DatosTraspasoDto CatalogoTraspaso(bool esPipa)
        {
            var medidores = TipoMedidorGasServicio.Obtener();
            var puntoVenta = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
            var predeterminada = puntoVenta.UnidadesAlmacen;
            var lpipas = AlmacenGasServicio.ObtenerPipasEmpresa(TokenServicio.ObtenerIdEmpresa());
            var lestaciones = AlmacenGasServicio.ObtenerEstacionesEmpresa(TokenServicio.ObtenerIdEmpresa());
            var estaciones = AlmacenGasServicio.ObtenerEstaciones(TokenServicio.ObtenerIdEmpresa());
            var unidadAlmacen = puntoVenta.UnidadesAlmacen;

            if (esPipa)
            {

                var pipa = puntoVenta.UnidadesAlmacen.Pipa;

                var filtradas = lpipas.FindAll(x => !x.IdPipa.Equals(pipa.IdPipa));
                var traspaso = AlmacenGasServicio.Traspasos(puntoVenta.UnidadesAlmacen.IdCAlmacenGas).OrderByDescending(x => x.Orden).FirstOrDefault();
                List<AlmacenGasTraspaso> traspasosEntrada = new List<AlmacenGasTraspaso>();
                foreach (var filtrado in filtradas)
                {
                    var almacenGas = filtrado.UnidadAlmacenGas.FirstOrDefault();
                    var ultimo = AlmacenGasServicio.Traspasos(almacenGas.IdCAlmacenGas);
                    if (ultimo != null)
                        traspasosEntrada.Add(ultimo.FirstOrDefault());
                }
                return TraspasoAdapter.ToDTOPipa(lpipas, filtradas, pipa, medidores, unidadAlmacen, traspaso, traspasosEntrada);
            }

            else
            {
                var estacion = puntoVenta.UnidadesAlmacen.EstacionCarburacion;
                var traspaso = AlmacenGasServicio.Traspasos(puntoVenta.UnidadesAlmacen.IdCAlmacenGas).OrderByDescending(x => x.Orden > 0).FirstOrDefault();
                List<AlmacenGasTraspaso> traspasosEntrada = new List<AlmacenGasTraspaso>();

                foreach (var filtrado in lpipas)
                {
                    var ultimo = AlmacenGasServicio.Traspasos(filtrado.UnidadAlmacenGas.SingleOrDefault().IdCAlmacenGas).OrderByDescending(x => x.Orden > 0).FirstOrDefault();
                    traspasosEntrada.Add(ultimo);
                }
                List<AlmacenGasTraspaso> traspasoEstacion = new List<AlmacenGasTraspaso>();
                foreach (var estacionCarburacion in estaciones)
                {
                    var ultimo = AlmacenGasServicio.Traspasos(estacionCarburacion.IdAlmacenGas.Value).OrderByDescending(x => x.Orden > 0).FirstOrDefault();
                    traspasoEstacion.Add(ultimo);
                }
                return TraspasoAdapter.ToDTOEstacion(lestaciones, lpipas, estacion, medidores, unidadAlmacen, traspaso, traspasosEntrada);
            }

        }
        public RespuestaDto Traspaso(TraspasoDto dto, bool esFinal)
        {
            var resp = TraspasoServicio.EvaluarClaveOperacion(dto);
            if (resp.Exito) return resp;
            return TraspasoServicio.Traspaso(dto, esFinal, TokenServicio.ObtenerIdEmpresa());
        }
        public DatosAnticiposCorteDto Estaciones()
        {
            //var estaciones = EstacionCarburacionServicio.ObtenerTodas(TokenServicio.ObtenerIdEmpresa());

            var dacDTO = new DatosAnticiposCorteDto();
            var unidad = UnidadesEstaciones();
            if (unidad.EsGeneral.Equals(false))
            {
                if (unidad.IdPipa != null)
                {
                    var pipa = new EquipoTransporteDataAccess().BuscarPipa(unidad.IdPipa ?? 0);
                    dacDTO.pipa = AnticiposCortesAdapter.ToDTO(pipa, unidad);
                    dacDTO.EsPipa = true;
                }
                if (unidad.IdCamioneta != null)
                {
                    var camioneta = new EquipoTransporteDataAccess().BuscarCamioneta(unidad.IdCamioneta ?? 0);
                    dacDTO.camioneta = AnticiposCortesAdapter.ToDTO(camioneta, unidad);
                    dacDTO.EsCamioneta = true;
                }
                if (unidad.IdEstacionCarburacion != null)
                {
                    var Estacion = EstacionCarburacionServicio.Obtener(unidad.IdEstacionCarburacion ?? 0);
                    dacDTO.estaciones = AnticiposCortesAdapter.ToDTO(Estacion, unidad);
                    dacDTO.EsEstacion = true;
                }
            }
            //return AnticiposCortesAdapter.ToDTO(estaciones, unidades);
            return dacDTO;
        }
        public List<UnidadAlmacenGas> UnidadesEstaciones(List<EstacionCarburacion> estaciones)
        {
            List<UnidadAlmacenGas> unidades = new List<UnidadAlmacenGas>();
            var unidadesEstaciones = AlmacenGasServicio.ObtenerEstaciones(TokenServicio.ObtenerIdEmpresa());
            foreach (var estacion in estaciones)
            {

                unidades.Add(
                    unidadesEstaciones.Find(
                        x => x.IdEstacionCarburacion.Value.Equals(estacion.IdEstacionCarburacion)
                        && x.IdEstacionCarburacion != null
                    ));
            }

            return unidades;
        }
        public UnidadAlmacenGas UnidadesEstaciones()
        {
            List<UnidadAlmacenGas> unidades = new List<UnidadAlmacenGas>();
            var unidadesEstaciones = AlmacenGasServicio.ObtenerEstaciones(TokenServicio.ObtenerIdEmpresa());

            var usuario = UsuarioAplicacionServicio.Obtener();
            var idCAlmacen = usuario.OperadoresChoferes.SingleOrDefault(x => x.Activo).PuntosVenta.SingleOrDefault(x => x.Activo).IdCAlmacenGas;
            var almacen = AlmacenGasServicio.ObtenerAlmacen(idCAlmacen);
            //if (almacen.EsGeneral.Equals(false))
            //{
            //    if (almacen.IdPipa != null)
            //    {
            //        unidades.Add(almacen);
            //    }
            //    if (almacen.IdCamioneta != null)
            //    {
            //        unidades.Add(almacen);
            //    }
            //    if (almacen.IdEstacionCarburacion != null)
            //    {
            //        unidades.Add(almacen);
            //    }
            //}
            //unidades.Add(unidadesEstaciones.Find(x => x.IdEstacionCarburacion.Value.Equals(estacion.IdEstacionCarburacion)
            //        && x.IdEstacionCarburacion != null
            //    ));
            //}

            return almacen;
        }
        public RespuestaDto anticipo(AnticipoDto dto)
        {
            var resp = VentaServicio.EvaluarClaveOperacion(dto);

            if (resp.Exito) return resp;

            var anticipos = VentaServicio.ObtenerAnticipos(TokenServicio.ObtenerIdEmpresa());
            var usuario = UsuarioAplicacionServicio.Obtener();
            var puntoVenta = usuario.OperadoresChoferes.SingleOrDefault(x => x.Activo).PuntosVenta.SingleOrDefault(x => x.Activo);
            var unidadAlmacen = puntoVenta.UnidadesAlmacen;
            var cortesYanticiposOrden = PuntoVentaServicio.ObtenerCortesAnticipos();

            var estacion = AlmacenGasServicio.ObtenerAlmacen(dto.IdCAlmacenGas);

            return VentaServicio.Anticipo(dto, TokenServicio.ObtenerIdEmpresa(), TokenServicio.ObtenerUsuarioAplicacion(), anticipos, estacion, cortesYanticiposOrden);
        }
        public RespuestaDto corte(CorteDto dto)
        {
            var resp = VentaServicio.EvaluarClaveOperacion(dto);
            if (resp.Exito) return resp;
            var puntoVentaBusqueda = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
            short idCAlmacenGas = puntoVentaBusqueda.IdCAlmacenGas;
            var almacen = AlmacenGasServicio.ObtenerAlmacen(idCAlmacenGas);


            var cortes = VentaServicio.ObtenerCortes(TokenServicio.ObtenerIdEmpresa());
            var estaciones = EstacionCarburacionServicio.ObtenerTodas(TokenServicio.ObtenerIdEmpresa());
            var estacion = estaciones.Find(x => x.IdEstacionCarburacion.Equals(almacen.IdEstacionCarburacion));
            //var almacenes = AlmacenGasServicio.ObtenerAlmacenes(TokenServicio.ObtenerIdEmpresa());
            //var almacen = almacenes.Find(x => x.IdEstacionCarburacion.Value.Equals(puntoVentaBusqueda.IdCAlmacenGas));
            //var puntosVenta = PuntoVentaServicio.ObtenerIdEmp(TokenServicio.ObtenerIdEmpresa());
            //var puntoventa = puntosVenta.Find(x => x.IdCAlmacenGas.Equals(puntoVentaBusqueda.IdCAlmacenGas));
            //var entrega = puntoventa.OperadorChofer.Usuario;

            var lpipas = AlmacenGasServicio.ObtenerPipasEmpresa(TokenServicio.ObtenerIdEmpresa());
            var lestaciones = AlmacenGasServicio.ObtenerEstacionesEmpresa(TokenServicio.ObtenerIdEmpresa());
            var lcamionetas = AlmacenGasServicio.ObtenerCamionetasEmpresa(TokenServicio.ObtenerIdEmpresa());

            var pipa = lpipas.SingleOrDefault(x => x.IdPipa.Equals(almacen.IdPipa));
            var camioneta = lcamionetas.SingleOrDefault(x => x.IdCamioneta.Equals(almacen.IdCamioneta));
            var estacionCarb = lestaciones.SingleOrDefault(x => x.IdEstacionCarburacion.Equals(almacen.IdEstacionCarburacion));
            var cortesYanticiposOrden = PuntoVentaServicio.ObtenerCortesAnticipos();
            PuntoVenta puntoVenta = null;
            UnidadAlmacenGas almacenPunto = null;
            if (pipa != null)
            {
                puntoVenta = pipa.UnidadAlmacenGas.First().PuntosVenta.First();
                almacenPunto = pipa.UnidadAlmacenGas.First();
            }
            else if (estacion != null)
            {
                puntoVenta = estacion.UnidadAlmacenGas.First().PuntosVenta.First();
                almacenPunto = estacion.UnidadAlmacenGas.First();
            }
            else if (camioneta != null)
            {
                puntoVenta = camioneta.UnidadAlmacenGas.First().PuntosVenta.First();
                almacenPunto = camioneta.UnidadAlmacenGas.First();
            }
            //var entrega = puntoVenta.OperadorChofer.Usuario;
            var entrega = UsuarioServicio.Obtener(dto.IdEntrega);
            var corte = VentaServicio.Corte(dto, TokenServicio.ObtenerIdEmpresa(), TokenServicio.ObtenerIdUsuario(), cortes, puntoVenta, almacenPunto, cortesYanticiposOrden);
            #region Insert en la tabla de VentaCajaGeneral
            if (corte.Exito)
            {
                #region Update a la tabla de ventas con el corte 
                foreach (var item in dto.Conceptos)
                {
                    var venta = PuntoVentaServicio.Obtener(item.TiketVenta);
                    var adapter = VentasEstacionesAdapter.ToDTO(item, venta);
                    var conceptos = PuntoVentaServicio.ActualizarVentasCorte(adapter);
                }
                #endregion
                var deContado = PuntoVentaServicio.ObtenerVentasContado(puntoVenta.IdPuntoVenta, dto.Fecha);
                var credito = PuntoVentaServicio.ObtenerVentasCredito(puntoVenta.IdPuntoVenta, dto.Fecha);
                var ventasCajasGral = PuntoVentaServicio.ObtenerVentasCajaGral();

                var corteCajaGeneral = AnticiposCortesAdapter.FromDTO(dto, TokenServicio.ObtenerIdEmpresa(), TokenServicio.ObtenerUsuarioAplicacion(), puntoVenta, puntoVenta.OperadorChofer, entrega, deContado, credito);
                corteCajaGeneral.Orden = (short)orden(ventasCajasGral);
                corteCajaGeneral.OtrasVentas = VentaServicio.CalculoOtrasVentas(deContado, credito);
                return PuntoVentaServicio.InsertMobil(corteCajaGeneral);
            }
            #endregion

            return corte;
        }
        public static int orden(List<VentaCajaGeneral> ventasCajasGral)
        {
            if (ventasCajasGral != null)
                if (ventasCajasGral.Count == 0)
                    return 1;
                else
                    return ventasCajasGral.Last(x => x.Orden > 0).Orden + 1;
            else
                return 1;
        }
        public DatosOtrosDto catalogoOtros()
        {
            var categoria = ProductoServicio.ObtenerCategorias();
            var linea = ProductoServicio.ObtenerLineasProducto();

            var productos = ProductoServicio.ObtenerProductoActivoVenta(TokenServicio.ObtenerIdEmpresa());

            return VentasEstacionesAdapter.ToDTO(categoria, linea, productos);
        }
        public List<DatosGasVentaDto> CatalogosGas(bool esLP, bool esCilindroConGas, bool esCilindro, int IdCliente = 0)
        {
            decimal descuento = 0;
            decimal precio = 0;
            if (!IdCliente.Equals(0))
            {
                var cliente = ClienteServicio.Obtener(IdCliente);
                if (cliente != null)
                {
                    if (!cliente.DescuentoXKilo.Equals(0))
                        if (cliente.EsFijo)
                            precio = cliente.DescuentoXKilo;
                        else
                            descuento = cliente.DescuentoXKilo;
                }
            }
            var pv = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
            var unidad = AlmacenGasServicio.ObtenerUnidadAlamcenGas(pv.IdCAlmacenGas);
            if (esLP)
            {
                var lectInicial = AlmacenGasServicio.ObtenerUltimaLectura(unidad, false);
                var puntoVenta = PuntoVentaServicio.Obtener(unidad);
                var ventas = puntoVenta.VentaPuntoDeVenta.Where(x => x.FechaRegistro.Equals(DateTime.Now));// cambiar a busqueda de fechamas especificas 
                var precios = PuntoVentaServicio.ObtenerPreciosVenta(TokenServicio.ObtenerIdEmpresa());
                var productosGas = ProductoServicio.ObtenerProductoActivoVenta(TokenServicio.ObtenerIdEmpresa(), true);
                var kilosCamioneta = LecturaGasServicio.ObtenerKilosGasCamioneta(unidad.IdCAlmacenGas, DateTime.Now, pv.IdPuntoVenta);

                decimal totalKilos = 0, calculo = 0;

                if (unidad.IdCamioneta > 0)
                {
                    var cilindros = AlmacenGasServicio.ObtenerCilindros(unidad);
                    var precioVenta = PrecioVentaGasServicio.ObtenerPrecioVigente(TokenServicio.ObtenerIdEmpresa());
                    return VentasEstacionesAdapter.ToDTOGas(cilindros, kilosCamioneta, precioVenta, descuento, precio);
                }
                else
                {
                    calculo = ((lectInicial.Porcentaje ?? 0 / 100) * unidad.CapacidadTanqueKg ?? 0) * (decimal)0.54;
                    foreach (var item in ventas)
                    {
                        foreach (var itemDetalle in item.VentaPuntoDeVentaDetalle)
                            totalKilos += itemDetalle.CantidadKg ?? 0;
                    }
                    if (totalKilos > 0)
                        calculo = calculo - totalKilos;
                    return VentasEstacionesAdapter.ToDTO(productosGas.Where(x => x.EsGas).ToList(), precios, calculo, descuento, precio);
                    //return VentasEstacionesAdapter.ToDTO(productosGas.Where(x => x.EsGas).ToList(), precios, calculo);
                    //return VentasEstacionesAdapter.ToDTO(productosGas, precios, kilosCamioneta);
                }
            }
            else if (esCilindroConGas)
            {
                var cilindrosConGas = AlmacenGasServicio.ObtenerCilindros(unidad);
                return VentasEstacionesAdapter.ToDTO(unidad);
            }
            else if (esCilindro)
            {
                var cilindros = AlmacenGasServicio.ObtenerCilindros();
                return VentasEstacionesAdapter.ToDTOC(cilindros);
            }
            return VentasEstacionesAdapter.ToDTO(unidad);
        }
        public List<DatosGasVentaDto> CatalogosGas()
        {
            var cilindros = AlmacenGasServicio.ObtenerCilindros();

            return VentasEstacionesAdapter.ToDTOC(cilindros);
        }
        public ClienteDTO BuscarClientePorRFC(string rfc)
        {
            var clientes = ClienteServicio.BuscarClientePorRFC(rfc);
            if (clientes != null)
                return ClienteAdapter.ToDTO(clientes);
            return new ClienteDTO();
        }
        public string testFuncionNumeroReporte()
        {
            return FechasFunciones.ObtenerClaveUnica();
        }
    }
}
