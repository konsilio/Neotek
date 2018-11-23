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

namespace Application.MainModule.Flujos
{
    public class Mobile
    {

        public RespuestaOrdenesCompraDTO ConsultarOrdenesCompra(short IdEmpresa, bool EsGas, bool EsActivoVenta, bool EsTransporteGas)
        {
           return MobileOrdenesCompraServicio.Consultar(IdEmpresa,EsGas,EsActivoVenta,EsTransporteGas);
        }

        public RespuestaDto  ConsultarOCAlternativa(int IdOrdenCompra)
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

            return EntradaGasServicio.RegistrarPapeleta(AlmacenAdapter.FromDto(papeletaDto));
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

            return LecturaGasServicio.Lectura(lcdto,true);
        }

        public DatosTomaLecturaDto ConsultaDatosTomaLectura(bool esEstacion, bool esPipa, bool esCamioneta, bool esFinalizar)
        {
            if(esEstacion)
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

            return RecargaGasServicio.Recarga(ridto,true);
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
               return ClientesServicio.Registar(cliente,TokenServicio.ObtenerIdEmpresa());
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
            int orden = Orden(ventas,venta.Fecha);
            var adapter = VentasEstacionesAdapter.FromDTO(venta, cliente, punto_venta,orden, TokenServicio.ObtenerIdEmpresa());

            adapter.OperadorChofer = operador.Nombre + " " + operador.Apellido1 + " " + operador.Apellido2;
            adapter.FolioVenta = venta.FolioVenta;
            //adapter.FolioOperacionDia = venta.FolioVenta;
            adapter.FechaRegistro = DateTime.Now;
            adapter.Dia = (byte) venta.Fecha.Day;
            adapter.Mes = (byte) venta.Fecha.Month;
            adapter.Year = (short) venta.Fecha.Year;
            adapter.FechaAplicacion = venta.Fecha;
            adapter.DatosProcesados = false;
            adapter.RequiereFactura = venta.Factura;
            adapter.VentaACredito = venta.Credito;
            adapter.ClienteConCredito = venta.TieneCredito;

            if (venta.SinNumero || venta.IdCliente==0)
            {
                Cliente clienteGenerico = ClienteServicio.BuscarClientePorRFC("XAXX010101000");
                adapter.IdCliente = clienteGenerico.IdCliente;
                adapter.RFC = clienteGenerico.Rfc;
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
                return AlmacenRecargaAdapter.ToDTO(almacenesAlternos, camionetasDTO, tipoMedidores);
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

        public RespuestaDto Autoconsumo(AutoconsumoDTO dto,bool esFinal)
        {
            var resp = AutoconsumoServicio.EvaluarAutoconsumo(dto);

            if (resp.Exito) return resp;
            
            return AutoconsumoServicio.Autoconsumo(dto,esFinal);
        }

        public DatosAutoconsumoDto CatalogoAutoconsumo(bool esEstacion, bool esInventario, bool esPipas,bool esFinal)
        {
            var medidores = TipoMedidorGasServicio.Obtener();
            var puntoVenta = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
            var pipas = AlmacenGasServicio.ObtenerPipas(puntoVenta.IdEmpresa);
            var camionetas = AlmacenGasServicio.ObtenerCamionetas(puntoVenta.IdEmpresa);
            var almacenes = AlmacenGasServicio.ObtenerAlmacenes(puntoVenta.IdEmpresa);
            var predeterminado = puntoVenta.UnidadesAlmacen;
            var autoconsumos = AlmacenGasServicio.ObtenerAutoConsumosNoProcesadas();
            if (esEstacion)
            {
                
                if (esFinal)
                {
                    var estacionesInicioEnInicial = estacionesInicio(autoconsumos);
                    var estacionesFinEnInicial = estacionesFin(autoconsumos,false,true,true);
                    return AlmacenAutoconsumoAdapter.ToDTOFinal(estacionesInicioEnInicial, estacionesFinEnInicial, medidores);
                }
                else
                    return AlmacenAutoconsumoAdapter.ToDTO(almacenes, predeterminado, pipas, camionetas, medidores);
                
            }else if (esInventario)
            {
                if (esFinal)
                {
                    var estacionesInicioEnInicial = estacionesInicio(autoconsumos,false,true,true);
                    return AlmacenAutoconsumoAdapter.ToDTOInventarioGeneral(estacionesInicioEnInicial, medidores);
                }
                else
                    return AlmacenAutoconsumoAdapter.ToDTOInventarioGeneral(pipas, camionetas, medidores);
            }else if (esPipas)
            {

                if (esFinal)
                {
                    var estacionesInicioEnInicial = estacionesInicio(autoconsumos, false, true, true);
                    var estacionesFinEnInicial = estacionesFin(autoconsumos, false, true, true);
                    return AlmacenAutoconsumoAdapter.ToDTOFinal(estacionesInicioEnInicial, estacionesFinEnInicial, medidores);
                }
                else
                    return AlmacenAutoconsumoAdapter.ToDTO(almacenes, predeterminado, pipas, camionetas, medidores);
            }
            return null;
        }

        public List<UnidadAlmacenGas> estacionesInicio(List<AlmacenGasAutoConsumo> autoconsumos,bool estaciones = true,bool pipas = false,bool camionetas = false)
        {
            List<UnidadAlmacenGas> list = new List<UnidadAlmacenGas>();
            if (estaciones) {
                foreach (var autoconsumo in autoconsumos)
                {
                    var almacen = AlmacenGasServicio.ObtenerUnidadAlamcenGas(autoconsumo.IdCAlmacenGasSalida);
                    if (almacen.IdEstacionCarburacion != null && almacen.IdEstacionCarburacion>0)
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

        public DatosAnticiposCorteDto CatalogoVentasAnticiposCorte(int idEstacion, bool esAnticipos,DateTime fecha)
        {
            var almacen = AlmacenGasServicio.ObtenerEstaciones(TokenServicio.ObtenerIdEmpresa()).FirstOrDefault(x => x.IdEstacionCarburacion.Equals(idEstacion));
            var puntosVenta = PuntoVentaServicio.ObtenerIdEmp(TokenServicio.ObtenerIdEmpresa()).FirstOrDefault(x => x.IdCAlmacenGas.Equals(almacen.IdCAlmacenGas));
            var ventas = CajaGeneralServicio.ObtenerVentasPuntosVenta(puntosVenta.IdPuntoVenta).OrderBy(x=>x.FechaRegistro).ToList();
            if (fecha!=null)
            {
                ventas = ventas.FindAll(x => x.FechaRegistro.Day.Equals(fecha.Day) && x.FechaRegistro.Month.Equals(fecha.Month) && x.FechaRegistro.Year.Equals(fecha.Year));
            }
           
            
            return AnticiposCortesAdapter.ToDTO(ventas, esAnticipos);
        }

        public RespuestaDto Calibracion(CalibracionDto dto, bool esFinal)
        {
            var resp = CalibracionServicio.EvaluarClaveOperacion(dto);

            if (resp.Exito) return resp;

            return CalibracionServicio.Calibracion(dto,esFinal);
        }

        public DatosCalibracionDto CatalogoCalibracion(bool esEstacion, bool esPipa)
        {
            var medidores = TipoMedidorGasServicio.Obtener();
            if (esEstacion)
            {
                var estaciones = AlmacenGasServicio.ObtenerEstaciones(TokenServicio.ObtenerIdEmpresa());
                return CalibracionAdapter.ToDTO(estaciones, medidores);
            }
            else if (esPipa)
            {
                var pipas = AlmacenGasServicio.ObtenerPipas(TokenServicio.ObtenerIdEmpresa());
                return CalibracionAdapter.ToDTO(pipas, medidores);
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
            var pipas = AlmacenGasServicio.ObtenerPipas(TokenServicio.ObtenerIdEmpresa());
            var estaciones = AlmacenGasServicio.ObtenerEstaciones(TokenServicio.ObtenerIdEmpresa());
            var puntoVenta = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
            var predeterminada = puntoVenta.IdCAlmacenGas;
            if (esPipa)
                return TraspasoAdapter.ToDTO(pipas,predeterminada, medidores);
            else
                return TraspasoAdapter.ToDTO(estaciones,pipas,predeterminada,medidores);
        }
        public RespuestaDto Traspaso(TraspasoDto dto,bool esFinal)
        {
            var resp = TraspasoServicio.EvaluarClaveOperacion(dto);
            if (resp.Exito) return resp;
            return TraspasoServicio.Traspaso(dto,esFinal,TokenServicio.ObtenerIdEmpresa());
        }
        public DatosAnticiposCorteDto Estaciones()
        {
            var estaciones = EstacionCarburacionServicio.ObtenerTodas(TokenServicio.ObtenerIdEmpresa());
            var unidades = UnidadesEstaciones(estaciones);

            return AnticiposCortesAdapter.ToDTO(estaciones,unidades);
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
                        && x.IdEstacionCarburacion!=null
                    ));
            }
           
            return unidades;
        }

        public RespuestaDto anticipo(AnticipoDto dto)
        {
            var resp = VentaServicio.EvaluarClaveOperacion(dto);
             
            if (resp.Exito) return resp;

            var anticipos = VentaServicio.ObtenerAnticipos(TokenServicio.ObtenerIdEmpresa());
            var estacion = AlmacenGasServicio.ObtenerAlmacen(dto.IdCAlmacenGas);

            return VentaServicio.Anticipo(dto,TokenServicio.ObtenerIdEmpresa(), TokenServicio.ObtenerUsuarioAplicacion(), anticipos, estacion);
        }

        public RespuestaDto corte(CorteDto dto)
        {
            var resp = VentaServicio.EvaluarClaveOperacion(dto);
            if (resp.Exito) return resp;

            var cortes = VentaServicio.ObtenerCortes(TokenServicio.ObtenerIdEmpresa());
            var estaciones = EstacionCarburacionServicio.ObtenerTodas(TokenServicio.ObtenerIdEmpresa());
            var estacion = estaciones.Find(x => x.IdEstacionCarburacion.Equals(dto.IdCAlmacenGas));
            var almacenes = AlmacenGasServicio.ObtenerAlmacenes(TokenServicio.ObtenerIdEmpresa());
            var almacen = almacenes.Find(x =>x.IdEstacionCarburacion.Value.Equals(dto.IdCAlmacenGas));
            var puntosVenta = PuntoVentaServicio.ObtenerIdEmp(TokenServicio.ObtenerIdEmpresa());
            var puntoventa = puntosVenta.Find(x => x.IdCAlmacenGas.Equals(almacen.IdCAlmacenGas));
            var entrega = puntoventa.OperadorChofer.Usuario;
            
            var corte = VentaServicio.Corte(dto, TokenServicio.ObtenerIdEmpresa(), TokenServicio.ObtenerIdUsuario(), cortes, puntoventa,almacen);

           /* if (corte.Exito)
            {
                var deContado = PuntoVentaServicio.ObtenerVentasContado(puntoventa.IdPuntoVenta, dto.Fecha);
                var credito = PuntoVentaServicio.ObtenerVentasCredito(puntoventa.IdPuntoVenta, dto.Fecha);

                var corteCajaGeneral = AnticiposCortesAdapter.FromDTO(dto, TokenServicio.ObtenerIdEmpresa(), TokenServicio.ObtenerUsuarioAplicacion(), puntoventa, puntoventa.OperadorChofer, entrega, deContado,credito);
                return PuntoVentaServicio.InsertMobil(corteCajaGeneral);
            }*/

            return corte;
        }

        public DatosOtrosDto catalogoOtros()
        {
            var categoria = ProductoServicio.ObtenerCategorias();
            var linea = ProductoServicio.ObtenerLineasProducto();
            var productos = ProductoServicio.ObtenerProductoActivoVenta(TokenServicio.ObtenerIdEmpresa());

            return VentasEstacionesAdapter.ToDTO(categoria,linea,productos);
        }

        public List<DatosGasVentaDto> CatalogosGas(bool esLP, bool esCilindroConGas, bool esCilindro)
        {
            var productosGas = ProductoServicio.ObtenerProductoActivoVenta(TokenServicio.ObtenerIdEmpresa(),true);
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
            return  ClienteAdapter.ToDTO(ClienteServicio.BuscarClientePorRFC(rfc));
        }
    }
}
