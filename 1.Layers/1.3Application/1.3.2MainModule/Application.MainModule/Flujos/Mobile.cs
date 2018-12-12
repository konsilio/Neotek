﻿using Application.MainModule.AdaptadoresDTO.Mobile;
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
            var ReporteAlmacen = AlmacenGasServicio.ReporteDia(fecha, idCAlmacenGas);
            return ReporteAlmacen;
        }


        public RespuestaDto Venta(VentaDTO venta)
        {
            var resp = VentaServicio.BuscarFolioVenta(venta);
            if (resp.Exito) return resp;

            var punto_venta = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
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

            return PuntoVentaServicio.InsertMobile(adapter);
        }


        public int Orden(List<VentaPuntoDeVenta> ventas,DateTime fechaVenta)
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
                    item.NombreAlmacen= camionetas.Find(x => x.IdCamioneta.Value.Equals(x.IdCamioneta)).Camioneta.Nombre;
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

            if (usuarios!=null && usuarios.Count != 0)
            {
                foreach (var usuario in usuarios)
                {
                    var usuariosRoles = usuario.UsuarioRoles;
                    foreach (var usuarioRol in usuariosRoles)
                    {
                        var rolAcciones = RolServicio.Obtener(usuarioRol);
                        if(rolAcciones.CatLiquidarCajaGeneral)
                        {
                            var buscar = usuariosLiquidar.Find(x => x.IdUsuario.Equals(usuario.IdUsuario));
                            if (buscar==null)
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
                pvaDto= PuntoVentaAdapter.ToDTO(usuario,operador,puntoVenta,unidadAlmacen,pipaAsignada);
            }
            if (unidadAlmacen.IdCamioneta > 0)
            {
                Camioneta camionetaAsignada = unidadAlmacen.Camioneta;
                pvaDto = PuntoVentaAdapter.ToDTO(usuario, operador, puntoVenta, unidadAlmacen, camionetaAsignada);
            }
            if (unidadAlmacen.IdEstacionCarburacion>0)
            {
                EstacionCarburacion estacionAsignada = unidadAlmacen.EstacionCarburacion;
                pvaDto = PuntoVentaAdapter.ToDTO(usuario,operador,puntoVenta,unidadAlmacen,estacionAsignada);
            }
            return pvaDto;
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
                var anticipos = PuntoVentaServicio.ObtenerAnticipos(unidadAlmacen).FindAll(x=>x.FechaCorteAnticipo.Day.Equals(fecha.Day) &&
                x.FechaCorteAnticipo.Month.Equals(fecha.Month) && x.FechaCorteAnticipo.Year.Equals(fecha.Year) && x.IdTipoOperacion.Equals(1)).ToList();
                var ventas = puntoVenta.VentaPuntoDeVenta;
                var ventasActivas = ventas.Where(x => x.Dia.Equals((byte)fecha.Day) && x.Mes.Equals((byte)fecha.Month) && x.Year.Equals((short)fecha.Year));
                var ventasSinCorte = new List<VentaPuntoDeVenta>();
                foreach (var ventaActiva in ventasActivas)
                {
                    if (ventaActiva.FolioOperacionDia == null)
                        ventasSinCorte.Add(ventaActiva);
                }
                dto = AnticiposCortesAdapter.ToDTOPipa(ventasSinCorte, anticipos, unidadAlmacen, esAnticipos);
            }

            if (unidadAlmacen.IdPipa > 0 && unidadAlmacen.IdPipa != null)
            {
                pipa = unidadAlmacen.Pipa;
                var puntoVenta = unidadAlmacen.PuntosVenta.First(x=>x.IdCAlmacenGas.Equals(unidadAlmacen.IdCAlmacenGas));
                var anticipos = PuntoVentaServicio.ObtenerAnticipos(unidadAlmacen).FindAll(x => x.FechaCorteAnticipo.Day.Equals(fecha.Day) &&
                x.FechaCorteAnticipo.Month.Equals(fecha.Month) && x.FechaCorteAnticipo.Year.Equals(fecha.Year) && x.IdTipoOperacion.Equals(1)).ToList();
                var ventas = puntoVenta.VentaPuntoDeVenta;
                var ventasActivas = ventas.Where(x => x.Dia.Equals((byte)fecha.Day) && x.Mes.Equals((byte)fecha.Month) && x.Year.Equals((short)fecha.Year));
                var ventasSinCorte = new List<VentaPuntoDeVenta>();
                foreach (var ventaActiva in ventasActivas)
                {
                    if (ventaActiva.FolioOperacionDia==null)
                        ventasSinCorte.Add(ventaActiva);
                }
                dto = AnticiposCortesAdapter.ToDTOPipa(ventasSinCorte, anticipos, unidadAlmacen, esAnticipos); 

            }
                
            if (unidadAlmacen.IdCamioneta > 0 && unidadAlmacen.IdCamioneta != null)
            {
                camioneta = unidadAlmacen.Camioneta;
                var puntoVenta = unidadAlmacen.PuntosVenta.First(x => x.IdCAlmacenGas.Equals(unidadAlmacen.IdCAlmacenGas));
                var anticipos = PuntoVentaServicio.ObtenerAnticipos(unidadAlmacen).FindAll(x => x.FechaCorteAnticipo.Day.Equals(fecha.Day) &&
                x.FechaCorteAnticipo.Month.Equals(fecha.Month) && x.FechaCorteAnticipo.Year.Equals(fecha.Year) && x.IdTipoOperacion.Equals(1) ).ToList();
                var ventas = puntoVenta.VentaPuntoDeVenta;
                var ventasActivas = ventas.Where(x => x.Dia.Equals((byte)fecha.Day) && x.Mes.Equals((byte)fecha.Month) && x.Year.Equals((short)fecha.Year));
                var ventasSinCorte = new List<VentaPuntoDeVenta>();
                foreach (var ventaActiva in ventasActivas)
                {
                    if (ventaActiva.FolioOperacionDia == null)
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
            }else
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
                var traspaso = AlmacenGasServicio.Traspasos(puntoVenta.UnidadesAlmacen.IdCAlmacenGas).OrderByDescending(x=>x.Orden).FirstOrDefault();
                List<AlmacenGasTraspaso> traspasosEntrada = new List<AlmacenGasTraspaso>();
                foreach (var filtrado in filtradas)
                {
                    var ultimo = AlmacenGasServicio.Traspasos(filtrado.UnidadAlmacenGas.SingleOrDefault().IdCAlmacenGas).OrderByDescending(x=>x.Orden).First();
                    traspasosEntrada.Add(ultimo);
                }
                return TraspasoAdapter.ToDTOPipa(lpipas,filtradas,pipa,medidores, unidadAlmacen,traspaso, traspasosEntrada);
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
                return TraspasoAdapter.ToDTOEstacion(lestaciones, lpipas, estacion, medidores,unidadAlmacen, traspaso, traspasosEntrada);
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
                }
                if (unidad.IdCamioneta != null)
                {
                    var camioneta = new EquipoTransporteDataAccess().BuscarCamioneta(unidad.IdCamioneta ?? 0);
                    dacDTO.camioneta = AnticiposCortesAdapter.ToDTO(camioneta, unidad);

                }
                if (unidad.IdEstacionCarburacion != null)
                {
                    var Estacion = EstacionCarburacionServicio.Obtener(unidad.IdEstacionCarburacion ?? 0);
                    dacDTO.estaciones = AnticiposCortesAdapter.ToDTO(Estacion, unidad);
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
            var idCAlmacen = usuario.OperadoresChoferes.SingleOrDefault(x=>x.Activo).PuntosVenta.SingleOrDefault(x=>x.Activo).IdCAlmacenGas;
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
            var puntoVenta = usuario.OperadoresChoferes.SingleOrDefault(x=>x.Activo).PuntosVenta.SingleOrDefault(x=>x.Activo);
            var unidadAlmacen = puntoVenta.UnidadesAlmacen;
            var cortesYanticiposOrden = PuntoVentaServicio.ObtenerCortesAnticipos();

            var estacion = AlmacenGasServicio.ObtenerAlmacen(dto.IdCAlmacenGas);

            return VentaServicio.Anticipo(dto, TokenServicio.ObtenerIdEmpresa(), TokenServicio.ObtenerUsuarioAplicacion(), anticipos, estacion, cortesYanticiposOrden);
        }

        public RespuestaDto corte(CorteDto dto)
        {
            var resp = VentaServicio.EvaluarClaveOperacion(dto);
            if (resp.Exito) return resp;

            var cortes = VentaServicio.ObtenerCortes(TokenServicio.ObtenerIdEmpresa());
            var estaciones = EstacionCarburacionServicio.ObtenerTodas(TokenServicio.ObtenerIdEmpresa());
            var estacion = estaciones.Find(x => x.IdEstacionCarburacion.Equals(dto.IdCAlmacenGas));
            var almacenes = AlmacenGasServicio.ObtenerAlmacenes(TokenServicio.ObtenerIdEmpresa());
            var almacen = almacenes.Find(x => x.IdEstacionCarburacion.Value.Equals(dto.IdCAlmacenGas));
            var puntosVenta = PuntoVentaServicio.ObtenerIdEmp(TokenServicio.ObtenerIdEmpresa());
            var puntoventa = puntosVenta.Find(x => x.IdCAlmacenGas.Equals(almacen.IdCAlmacenGas));
            //var entrega = puntoventa.OperadorChofer.Usuario;

            var lpipas = AlmacenGasServicio.ObtenerPipasEmpresa(TokenServicio.ObtenerIdEmpresa());
            var lestaciones = AlmacenGasServicio.ObtenerEstacionesEmpresa(TokenServicio.ObtenerIdEmpresa());
            var lcamionetas = AlmacenGasServicio.ObtenerCamionetasEmpresa(TokenServicio.ObtenerIdEmpresa());

            var pipa = lpipas.SingleOrDefault(x => x.IdPipa.Equals(dto.IdCAlmacenGas));
            var camioneta = lcamionetas.SingleOrDefault(x => x.IdCamioneta.Equals(dto.IdCAlmacenGas));
            var estacionCarb = lestaciones.SingleOrDefault(x => x.IdEstacionCarburacion.Equals(dto.IdCAlmacenGas));
            var cortesYanticiposOrden = PuntoVentaServicio.ObtenerCortesAnticipos();
            PuntoVenta puntoVenta = null;
            UnidadAlmacenGas almacenPunto = null; 
            if (pipa != null)
            {
                puntoVenta = pipa.UnidadAlmacenGas.First().PuntosVenta.First();
                almacenPunto = pipa.UnidadAlmacenGas.First();
            }
            else if(estacion!= null)
            {
                puntoVenta = estacion.UnidadAlmacenGas.First().PuntosVenta.First();
                almacenPunto = estacion.UnidadAlmacenGas.First();
            }
            else if(camioneta!= null)
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

        public List<DatosGasVentaDto> CatalogosGas(bool esLP, bool esCilindroConGas, bool esCilindro)
        {
            var productosGas = ProductoServicio.ObtenerProductoActivoVenta(TokenServicio.ObtenerIdEmpresa(), true);
            var unidad = AlmacenGasServicio.ObtenerCamionetas(TokenServicio.ObtenerIdEmpresa());
            var camioneta = unidad[0];
            var cilindrosConGas = AlmacenGasServicio.ObtenerCilindros(camioneta);
            var precios = PuntoVentaServicio.ObtenerPreciosVenta(TokenServicio.ObtenerIdEmpresa());
            var cilindros = AlmacenGasServicio.ObtenerCilindros();

            if (esLP)
                return VentasEstacionesAdapter.ToDTO(productosGas, precios);
            else if (esCilindroConGas)
                return VentasEstacionesAdapter.ToDTO(camioneta);
            else if (esCilindro)
                return VentasEstacionesAdapter.ToDTOC(cilindros);

            return VentasEstacionesAdapter.ToDTO(camioneta);
        }
        public List<DatosGasVentaDto> CatalogosGas()
        {
            var cilindros = AlmacenGasServicio.ObtenerCilindros();
            return VentasEstacionesAdapter.ToDTOC(cilindros);
        }
        public ClienteDTO BuscarClientePorRFC(string rfc)
        {
            return ClienteAdapter.ToDTO(ClienteServicio.BuscarClientePorRFC(rfc));
        }
    }
}
