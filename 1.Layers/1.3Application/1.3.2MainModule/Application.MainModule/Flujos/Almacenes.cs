using Application.MainModule.AdaptadoresDTO.Almacenes;
using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.AdaptadoresDTO.Requisiciones;
using Application.MainModule.AdaptadoresDTO.Seguridad;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios.Requisiciones;
using Application.MainModule.Servicios.Seguridad;
using Application.MainModule.Servicios.Ventas;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Constantes;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities.MainModule;

namespace Application.MainModule.Flujos
{
    public class Almacenes
    {
        public RespuestaDto GenerarEntradaProducto(OrdenCompraEntradasDTO dto)
        {
            List<Almacen> _almacen = new List<Almacen>();
            List<Almacen> _almacenCrear = new List<Almacen>();
            List<AlmacenEntradaProducto> entradas = new List<AlmacenEntradaProducto>();
            var oc = OrdenCompraServicio.Buscar(dto.IdOrdenCompra);
            var ProductosOC = ProductosOCAdapter.FromEntity(oc.Productos.ToList());
            foreach (var prod in dto.Productos)
            {
                ProductosOC.FirstOrDefault(x => x.IdProducto.Equals(prod.IdProducto)).CantidadEntregada = prod.Cantidad;
                prod.FechaEntrada = dto.FechaEntrada;
                var _Almacen = ProductoAlmacenServicio.ObtenerAlmacen(prod.IdProducto, dto.IdEmpresa);
                if (_Almacen == null)
                {
                    prod.CantidadAnterior = 0;
                    prod.CantidadFinal = prod.Cantidad;
                    var nuevoAlmacen = ProductoAlmacenServicio.GenaraAlmacenNuevo(prod.IdProducto, dto.IdEmpresa, prod.Cantidad);
                    nuevoAlmacen = ProductoAlmacenServicio.GenerarAlmacenConEntradaProcuto(prod, dto.IdOrdenCompra, nuevoAlmacen);
                    _almacenCrear.Add(nuevoAlmacen);
                }
                else
                {
                    _Almacen.FechaActualizacion = DateTime.Today;
                    prod.CantidadAnterior = _Almacen.Cantidad;
                    var AlmacenActualizar = ProductoAlmacenServicio.AlmacenEntity(_Almacen);
                    AlmacenActualizar.Cantidad = CalcularAlmacenServicio.ObtenerSumaEntradaAlmacen(AlmacenActualizar.Cantidad, prod.Cantidad);
                    prod.CantidadFinal = AlmacenActualizar.Cantidad;
                    _almacen.Add(AlmacenActualizar);
                    var EntradaProd = ProductoAlmacenServicio.GenerarAlmacenEntradaProcuto(prod, dto.IdOrdenCompra, _Almacen);
                    entradas.Add(EntradaProd);
                }
            }
            var ocEntity = OrdenCompraServicio.DeterminarEstatusPorEntradas(OrdenComprasAdapter.FromEntity(oc), ProductosOC);
            return ProductoAlmacenServicio.EntradaAlmcacenProductos(_almacen, _almacenCrear, entradas, ocEntity, ProductosOC);
        }
        public RespuestaDto GenerarSalidaProducto(RequisicionSalidaDTO dto)
        {
            List<Almacen> _almacen = new List<Almacen>();
            List<AlmacenSalidaProducto> Salidas = new List<AlmacenSalidaProducto>();

            var _requisicion = RequisicionServicio.Buscar(dto.IdRequisicion);
            List<RequisicionProducto> _productos = RequisicionProductoAdapter.FromEntity(_requisicion.Productos.ToList());

            foreach (var prod in dto.Productos)
            {
                var _Almacen = ProductoAlmacenServicio.ObtenerAlmacen(prod.IdProducto, dto.IdEmpresa);
                if (_Almacen == null)
                    return ProductoAlmacenServicio.NoExiste();
                else
                {
                    _productos.FirstOrDefault(x => x.IdProducto.Equals(prod.IdProducto)).CantidadEntregada = prod.Cantidad;
                    _Almacen.FechaActualizacion = DateTime.Today;
                    prod.CantidadAnterior = _Almacen.Cantidad;
                    prod.IdUsuarioEntrega = TokenServicio.ObtenerIdUsuario();
                    prod.FechaEntrada = DateTime.Now;
                    var AlmacenActualizar = ProductoAlmacenServicio.AlmacenEntity(_Almacen);
                    AlmacenActualizar.Cantidad = CalcularAlmacenServicio.ObtenerRestaSalidaAlmacen(AlmacenActualizar.Cantidad, prod.Cantidad);
                    prod.CantidadFinal = AlmacenActualizar.Cantidad;
                    if (prod.CantidadFinal < 0)
                        return ProductoAlmacenServicio.CantidadInsuficiente();
                    _almacen.Add(AlmacenActualizar);
                    var SalidaProd = ProductoAlmacenServicio.GenerarAlmacenSalidaProcuto(prod, dto.IdRequisicion, _Almacen);
                    Salidas.Add(SalidaProd);
                }
            }
            _requisicion = RequisicionAdapter.FromEntity(RequisicionServicio.DeterminaEstatusPorSalidas(_requisicion, _productos));
            return ProductoAlmacenServicio.SalidaAlmcacenProductos(_almacen, Salidas, _requisicion, _productos);
        }
        public OrdenCompraEntradasDTO BuscarOrdenCompra(int Id)
        {
            var oc = OrdenCompraServicio.Buscar(Id);
            var req = RequisicionServicio.Buscar(oc.IdRequisicion);
            return ProductoAlmacenServicio.AlmacenEntrada(oc, req);
        }
        public List<AlmacenDTO> ProductosAlmacen(short idEmpresa)
        {
            var permiso = PermisosServicio.PuedeConsultarProductoAlmacen();
            if (!permiso.Exito) return null;
            var prods = ProductoAlmacenServicio.BuscarAlmacen(idEmpresa);
            return AlmacenProductoAdapter.ToDTO(prods);
        }
        public RespuestaDto ActualizarAlmacen(AlmacenDTO dto)
        {
            var permiso = PermisosServicio.PuedeModificarAlmacen();
            if (!permiso.Exito) return null;

            var almacen = ProductoAlmacenServicio.ObtenerAlmacen(dto.IdProducto, dto.IdEmpresa);
            var entity = ProductoAlmacenServicio.AlmacenEntity(almacen);
            var prod = ProductoServicio.ObtenerProducto(dto.IdProducto);
            entity.Ubicacion = dto.Ubicacion;
            entity.FechaActualizacion = DateTime.Now;
            RespuestaDto resp = new RespuestaDto();
            if (dto.Cantidad < entity.Cantidad)
            {
                AlmacenSalidaProductoDTO salida = new AlmacenSalidaProductoDTO
                {
                    IdProducto = dto.IdProducto,
                    Cantidad = CalcularAlmacenServicio.ObtenerDiferneciaMovimiento(dto.Cantidad, entity.Cantidad),
                    CantidadAnterior = almacen.Cantidad,
                    CantidadFinal = dto.Cantidad,
                    Observaciones_ = string.Format(AlmacenConst.Actualizacion, dto.Observaciones),
                };
                entity.Cantidad = dto.Cantidad;
                var SalidaProd = ProductoAlmacenServicio.GenerarAlmacenSalidaProcuto(salida, 0, almacen);
                resp = ProductoAlmacenServicio.SalidaAlmcacenProductos(entity, SalidaProd);
            }
            if (dto.Cantidad > entity.Cantidad)
            {
                AlmacenEntradaDTO entrada = new AlmacenEntradaDTO
                {
                    IdProducto = dto.IdProducto,
                    Cantidad = CalcularAlmacenServicio.ObtenerDiferneciaMovimiento(dto.Cantidad, entity.Cantidad),
                    CantidadAnterior = almacen.Cantidad,
                    CantidadFinal = dto.Cantidad,
                    Observaciones = string.Format(AlmacenConst.Actualizacion, dto.Observaciones),
                };
                entity.Cantidad = dto.Cantidad;
                var EntradaProd = ProductoAlmacenServicio.GenerarAlmacenEntradaProcuto(entrada, 0, almacen);
                resp = ProductoAlmacenServicio.EntradaAlmcacenProductos(entity, EntradaProd);
            }
            if (dto.Cantidad.Equals(entity.Cantidad))
            {
                resp = ProductoAlmacenServicio.Actualiza(entity);
            }
            return resp;
        }
        public List<RegistroDTO> RegistroAlmacen(short idEmpresa)
        {
            var permiso = PermisosServicio.PuedeRegistrarAlmacen();
            if (!permiso.Exito) return null;

            var Entradas = ProductoAlmacenServicio.BuscarEntradasTodo(idEmpresa);
            var Salidas = ProductoAlmacenServicio.BuscarSalidaTodo(idEmpresa);

            return ProductoAlmacenServicio.UnirRegistros(Salidas, Entradas);
        }
        public RequisicionSalidaDTO BuscarRequsicionSalida(int idRequisicion)
        {
            var req = RequisicionServicio.Buscar(idRequisicion);
            return AlmacenAdapter.FromDTO(req);
        }
        public List<AplicaDescargaDto> AplicarDescargas()
        {
            return AlmacenGasServicio.AplicarDescargas();
        }
        public List<RemanenteGeneralDTO> ConsultarRemanenteGeneral(RemanenteDTO dto)
        {
            var permiso = PermisosServicio.PuedeConsultarRemanenteGral();
            if (!permiso.Exito) return null;
            List<RemanenteGeneralDTO> remaGeneral = new List<RemanenteGeneralDTO>();

            var AlmacenPrincipal = AlmacenGasServicio.ObtenerAlmacenPrincipal(dto.IdEmpresa == (short)0 ? TokenServicio.ObtenerIdEmpresa() : dto.IdEmpresa);
            var lectura = AlmacenGasServicio.ObtenerLecturaIncialdelMes(AlmacenPrincipal.IdCAlmacenGas, dto.Fecha.Month, dto.Fecha.Year).OrderByDescending(x => x.FechaAplicacion).FirstOrDefault();
            var descargas = AlmacenGasServicio.ObtenerDescargasTodas(dto.Fecha);
            var pventas = PuntoVentaServicio.ObtenerIdEmp(dto.IdEmpresa);
            int Dias = dto.Fecha.Month.Equals(DateTime.Now.Month) && dto.Fecha.Year.Equals(DateTime.Now.Year) ? DateTime.Now.Day : DateTime.DaysInMonth(dto.Fecha.Year, dto.Fecha.Month);
            dto.Fecha = Convert.ToDateTime(string.Concat(dto.Fecha.Year, "/", dto.Fecha.Month, "/", "1", " ", "00:00:01"));
            for (int i = 1; i < Dias; i++)
            {
                RemanenteGeneralDTO rema = new RemanenteGeneralDTO();
                if (lectura == null)
                    rema.InventarioInicial = 0;
                else
                    rema.InventarioInicial = Math.Round(CalcularAlmacenServicio.ObtenerKgLectura(lectura.UnidadAlmacenGas.CapacidadTanqueLt ?? 0, lectura.Porcentaje ?? 0, (decimal)0.54), 4);

                var descs = descargas.Where(x => x.FechaRegistro.Day.Equals(i)).ToList();
                if (descs != null)
                {
                    foreach (var desc in descs)
                    {
                        var ki = ((desc.CapacidadTanqueKg * (desc.PorcenMagnatelOcularTractorINI / 100)) * (decimal)0.54) ?? 0;
                        rema.AcumuladoCompras += CalculosGenerales.Truncar(CalculosGenerales.DiferenciaEntreDosNumero(ki, desc.MasaKg ?? 0), 2);
                    }
                }
                foreach (var pventa in pventas)
                {
                    if (pventa.UnidadesAlmacen.IdCamioneta != null)
                    {
                        var ventasCamioneta = CajaGeneralServicio.ObtenerVentasPuntosVenta(pventa.IdPuntoVenta);
                        foreach (var venta in ventasCamioneta)
                        {
                            if (venta.FechaRegistro < dto.Fecha)
                                rema.Ventas += venta.VentaPuntoDeVentaDetalle.Sum(x => x.CantidadKg ?? 0);
                        }
                    }
                    if (pventa.UnidadesAlmacen.IdPipa != null)
                    {
                        var ventasPipa = CajaGeneralServicio.ObtenerVentasPuntosVenta(pventa.IdPuntoVenta);
                        foreach (var venta in ventasPipa)
                        {
                            if (venta.FechaRegistro < dto.Fecha)
                                rema.Ventas += venta.VentaPuntoDeVentaDetalle.Sum(x => x.CantidadKg ?? 0);
                        }
                    }
                    if (pventa.UnidadesAlmacen.IdEstacionCarburacion != null)
                    {
                        var ventasCarburacion = CajaGeneralServicio.ObtenerVentasPuntosVenta(pventa.IdPuntoVenta);
                        foreach (var venta in ventasCarburacion)
                        {
                            if (venta.FechaRegistro < dto.Fecha)
                                rema.Carburacion += venta.VentaPuntoDeVentaDetalle.Sum(x => x.CantidadKg ?? 0);
                        }
                    }
                }
                rema.InventarioLibro = Math.Round(rema.InventarioInicial + rema.AcumuladoCompras - rema.Ventas - rema.Carburacion, 4);
                rema.InventarioFisico = Math.Round(AlmacenGasServicio.ObtenerKgInventarioFisico(dto.Fecha, dto.IdEmpresa), 4);
                rema.GasSobrante = Math.Round(CalcularAlmacenServicio.ObtenerGasSobrante(rema.InventarioLibro, rema.InventarioFisico), 4);
                rema.RemanenteDecimal = Math.Round(CalcularAlmacenServicio.ObtenerRemaPorcentaje(rema.Ventas, rema.GasSobrante), 4);

                rema.dia = dto.Fecha.Day;
                rema.Mes = dto.Fecha.Month;
                rema.Anio = dto.Fecha.Year;
                remaGeneral.Add(rema);

                dto.Fecha = dto.Fecha.AddDays(1);
            }
            return remaGeneral;
        }
        public List<RemanenteGeneralDTO> ConsultarRemanenteTracto(RemanenteDTO dto)
        {
            var permiso = PermisosServicio.PuedeConsultarRemanenteGral();
            if (!permiso.Exito) return null;
            List<RemanenteGeneralDTO> remaGeneral = new List<RemanenteGeneralDTO>();

            var AlmacenPrincipal = AlmacenGasServicio.ObtenerAlmacenPrincipal(dto.IdEmpresa == (short)0 ? TokenServicio.ObtenerIdEmpresa() : dto.IdEmpresa);
            var lectura = AlmacenGasServicio.ObtenerLecturaIncialdelMes(AlmacenPrincipal.IdCAlmacenGas, dto.Fecha.Month, dto.Fecha.Year).OrderByDescending(x => x.FechaAplicacion).FirstOrDefault();
            var descargas = AlmacenGasServicio.ObtenerDescargasTodas(dto.Fecha);

            int Dias = dto.Fecha.Month.Equals(DateTime.Now.Month) && dto.Fecha.Year.Equals(DateTime.Now.Year) ? DateTime.Now.Day : DateTime.DaysInMonth(dto.Fecha.Year, dto.Fecha.Month);
            dto.Fecha = Convert.ToDateTime(string.Concat(dto.Fecha.Year, "/", dto.Fecha.Month, "/", "1", " ", "00:00:01"));
            decimal acumulado = 0;
            for (int i = 1; i <= Dias; i++)
            {
                RemanenteGeneralDTO rema = new RemanenteGeneralDTO();
                var descs = descargas.Where(x => x.FechaRegistro.Day.Equals(i)).ToList();
                if (descs != null)
                {
                    foreach (var desc in descs)
                    {
                        var ki = ((desc.CapacidadTanqueKg * (desc.PorcenMagnatelOcularTractorINI / 100)) * (decimal)0.54) ?? 0;
                        rema.RemanenteDecimal = CalculosGenerales.Truncar(CalculosGenerales.DiferenciaEntreDosNumero(ki, desc.MasaKg ?? 0), 2);
                        acumulado += rema.RemanenteDecimal;

                    }
                }
                rema.AcumuladoCompras = acumulado;
                rema.dia = dto.Fecha.Day;
                rema.Mes = dto.Fecha.Month;
                rema.Anio = dto.Fecha.Year;
                remaGeneral.Add(rema);

                dto.Fecha = dto.Fecha.AddDays(1);
            }
            return remaGeneral;
        }
        public List<RemanentePuntoVentaTodosDTO> ConsultarRemanentePorPuntoventa(RemanenteDTO dto)
        {
            List<RemanentePuntoVentaTodosDTO> respuesta = new List<RemanentePuntoVentaTodosDTO>();

            var PuntosVenta = new List<PuntoVenta>();
            if (dto.IdPuntoVenta.Equals(0))
                PuntosVenta = PuntoVentaServicio.ObtenerIdEmp(TokenServicio.ObtenerIdEmpresa());
            else
                PuntosVenta.Add(PuntoVentaServicio.Obtener(dto.IdPuntoVenta));
            foreach (var pventa in PuntosVenta)
            {
                var _calibracion = pventa.UnidadesAlmacen.Calibraciones != null && !pventa.UnidadesAlmacen.Calibraciones.Count.Equals(0) ? pventa.UnidadesAlmacen.Calibraciones.LastOrDefault().Porcentaje : 0;
                List<RemanentePuntoVentaDTO> remaPuntoVenta = new List<RemanentePuntoVentaDTO>();
                RemanentePuntoVentaTodosDTO resp = new RemanentePuntoVentaTodosDTO();
                resp.RemaentePuntoVenta = new List<RemanentePuntoVentaDTO>();
                int Dias = dto.Fecha.Month.Equals(DateTime.Now.Month) && dto.Fecha.Year.Equals(DateTime.Now.Year) ? DateTime.Now.Day : DateTime.DaysInMonth(dto.Fecha.Year, dto.Fecha.Month);
                dto.Fecha = Convert.ToDateTime(string.Concat(dto.Fecha.Year, "/", dto.Fecha.Month, "/", "1", " ", "00:00:01"));
                for (int i = 0; i < Dias; i++)
                {
                    RemanentePuntoVentaDTO rema = new RemanentePuntoVentaDTO();
                    //if (pventa.UnidadesAlmacen.IdCamioneta != null)
                    //{
                    var ventas = CajaGeneralServicio.ObtenerVentasPorCAlmacenGas(pventa.IdCAlmacenGas, dto.Fecha);
                    foreach (var venta in ventas)
                    {
                      
                        rema.Porcentaje = CalcularGasServicio.ObtenerPorcentajeRemanentePtoVenta(pventa.UnidadesAlmacen.IdCAlmacenGas, venta.VentaPuntoDeVentaDetalle.Sum(x => x.CantidadKg ?? 0), dto.Fecha);
                        rema.Remanente += venta.VentaPuntoDeVentaDetalle.Sum(x => x.CantidadKg ?? 0) * _calibracion;
                  
                    }
                  
                    rema.Remanente = CalculosGenerales.Truncar(rema.Remanente, 2);
                    rema.IdPuntoVenta = dto.IdPuntoVenta;
                    rema.NombrePuntoVenta = pventa.UnidadesAlmacen.Numero;
                    rema.Porcentaje = CalculosGenerales.Truncar(rema.Porcentaje, 1);
                    rema.Mes = dto.Fecha.Month;
                    rema.Anio = dto.Fecha.Year;
                    rema.dia = dto.Fecha.Day;
                    remaPuntoVenta.Add(rema);


                    dto.Fecha = dto.Fecha.AddDays(1);
                }
                resp.RemaentePuntoVenta.AddRange(remaPuntoVenta);
                respuesta.Add(resp);
                //if (respuesta.Count.Equals(6))
                //   return respuesta;
            }
            return respuesta;
        }
        public List<RepInventarioPorPuntoVentaDTO> BuscarInvetarioPorPuntoDeVenta(List<Pipa> pipas, List<EstacionCarburacion> estaciones, DateTime fecha)
        {
            List<RepInventarioPorPuntoVentaDTO> repo = new List<RepInventarioPorPuntoVentaDTO>();
            foreach (var p in pipas)
            {
                var lecturas = AlmacenGasServicio.ObtenerLecturas(p.UnidadAlmacenGas.ToList()[0].IdAlmacenGas.Value, fecha);
                foreach (var item in lecturas)
                {
                    if (item.IdTipoEvento.Equals(TipoEventoEnum.Inicial))
                    {
                        RepInventarioPorPuntoVentaDTO r = new RepInventarioPorPuntoVentaDTO();
                        r.ID = Convertir.ConcatenarNumeros(item.IdCAlmacenGas, item.IdOrden);
                        r.NombreVehiculo = AlmacenGasServicio.ObtenerNombreUnidadAlmacenGas(item.UnidadAlmacenGas);
                        r.LecturaInicial = Convert.ToInt32(item.P5000 ?? 0);
                        r.ImagenLI = item.Fotografias.Count.Equals(0) ? string.Empty : item.Fotografias.FirstOrDefault(x => x.IdOrden.Equals(item.IdOrden)).UrlImagen ?? string.Empty;
                        r.Fecha = item.FechaRegistro;
                        repo.Add(r);
                    }
                }
                foreach (var item in lecturas)
                {
                    if (item.IdTipoEvento.Equals(TipoEventoEnum.Final))
                    {
                        if (repo.Exists(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())))
                        {
                            var li = repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).LecturaInicial;
                            var dif = CalculosGenerales.DiferenciaEntreDosNumero(li, item.P5000 ?? 0);
                            repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).LecturaFinal = Convert.ToInt32(item.P5000 ?? 0);
                            repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).ImagenLF = item.Fotografias.Count.Equals(0) ? string.Empty : item.Fotografias.FirstOrDefault(x => x.IdOrden.Equals(item.IdOrden)).UrlImagen ?? string.Empty;
                            repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).Diferencia = dif;
                        }
                    }
                }
            }
            foreach (var e in estaciones)
            {
                var lecturas = AlmacenGasServicio.ObtenerLecturas(e.UnidadAlmacenGas.ToList()[0].IdAlmacenGas.Value, fecha);
                foreach (var item in lecturas)
                {
                    if (item.IdTipoEvento.Equals(TipoEventoEnum.Inicial))
                    {
                        RepInventarioPorPuntoVentaDTO r = new RepInventarioPorPuntoVentaDTO();
                        r.ID = Convertir.ConcatenarNumeros(item.IdCAlmacenGas, item.IdOrden);
                        r.NombreVehiculo = AlmacenGasServicio.ObtenerNombreUnidadAlmacenGas(item.UnidadAlmacenGas);
                        r.LecturaInicial = Convert.ToInt32(item.P5000 ?? 0);
                        r.ImagenLI = item.Fotografias.Count.Equals(0) ? string.Empty : item.Fotografias.FirstOrDefault(x => x.IdOrden.Equals(item.IdOrden)).UrlImagen ?? string.Empty;
                        r.Fecha = item.FechaRegistro;
                        repo.Add(r);
                    }
                }
                foreach (var item in lecturas)
                {
                    if (item.IdTipoEvento.Equals(TipoEventoEnum.Final))
                    {
                        if (repo.Exists(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())))
                        {
                            var li = repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).LecturaInicial;
                            repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).LecturaFinal = Convert.ToInt32(item.P5000 ?? 0);
                            repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).ImagenLF = item.Fotografias.Count.Equals(0) ? string.Empty : item.Fotografias.FirstOrDefault(x => x.IdOrden.Equals(item.IdOrden)).UrlImagen ?? string.Empty;
                            repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).Diferencia = CalculosGenerales.DiferenciaEntreDosNumero(li, item.P5000 ?? 0);
                        }
                    }
                }
            }
            return repo.Select(x => { x.Diferencia = CalcularGasServicio.ObtenerDiferenciaLitros(x.LecturaFinal, x.LecturaInicial); return x; }).ToList();
        }
        public List<UnidadAlmacenGasDTO> ListaUnidadesAlmacen()
        {
            var ua = AlmacenGasServicio.ObtenerPuntosVenta(TokenServicio.ObtenerIdEmpresa());
            return AlmacenGasAdapter.ToDTO(ua);
        }
    }
}
