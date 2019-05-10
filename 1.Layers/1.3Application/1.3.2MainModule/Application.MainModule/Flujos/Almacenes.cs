using Application.MainModule.AdaptadoresDTO.Almacenes;
using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.AdaptadoresDTO.Requisiciones;
using Application.MainModule.AdaptadoresDTO.Seguridad;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Almacen;
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
            //Validar Permisos

            var prods = ProductoAlmacenServicio.BuscarAlmacen(idEmpresa);
            return AlmacenProductoAdapter.ToDTO(prods);
        }
        public RespuestaDto ActualizarAlmacen(AlmacenDTO dto)
        {
            //Validar permisos
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
            //Validar Permisos

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
            List<RemanenteGeneralDTO> remaGeneral = new List<RemanenteGeneralDTO>();
            if (TokenServicio.ObtenerEsAdministracionCentral())
            {
                return new List<RemanenteGeneralDTO>();
            }
            var AlmacenPrincipal = AlmacenGasServicio.ObtenerAlmacenPrincipal(dto.IdEmpresa == (short)0 ? TokenServicio.ObtenerIdEmpresa() : dto.IdEmpresa);
            var lectura = AlmacenGasServicio.ObtenerLecturaIncialdelMes(AlmacenPrincipal.IdCAlmacenGas, dto.Fecha.Month, dto.Fecha.Year).OrderByDescending(x => x.FechaAplicacion).FirstOrDefault();
            var descargas = AlmacenGasServicio.ObtenerDescargasTodas();
            var pventas = PuntoVentaServicio.ObtenerIdEmp(dto.IdEmpresa);        

            for (int i = 0; i < DateTime.DaysInMonth(dto.Fecha.Year, dto.Fecha.Month); i++)
            {
                dto.Fecha.AddDays(i);
                RemanenteGeneralDTO rema = new RemanenteGeneralDTO();
                rema.InventarioInicial = CalcularAlmacenServicio.ObtenerKgLectura(lectura.UnidadAlmacenGas.CapacidadTanqueLt ?? 0, lectura.Porcentaje ?? 0, (decimal)0.54);

                rema.AcumuladoCompras = descargas.Where(c => c.FechaRegistro < dto.Fecha).Sum(x => x.MasaKg) ?? 0;
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
                rema.InventarioLibro = rema.InventarioInicial + rema.AcumuladoCompras - rema.Ventas - rema.Carburacion;
                rema.InventarioFisico = AlmacenGasServicio.ObtenerKgInventarioFisico(dto.Fecha, dto.IdEmpresa);
                rema.GasSobrante = CalcularAlmacenServicio.ObtenerGasSobrante(rema.InventarioLibro, rema.InventarioFisico);
                rema.RemanenteDecimal = CalcularAlmacenServicio.ObtenerRemaPorcentaje(rema.Ventas, rema.GasSobrante);

                rema.dia = dto.Fecha.Day;
                rema.Mes = dto.Fecha.Month;
                rema.Anio = dto.Fecha.Year;

                remaGeneral.Add(rema);
            }
            return remaGeneral;
        }
        public List<RepInventarioPorPuntoVentaDTO> BuscarInvetarioPorPuntoDeVenta(List<Pipa> pipas, List<EstacionCarburacion> estaciones)
        {
            List<RepInventarioPorPuntoVentaDTO> repo = new List<RepInventarioPorPuntoVentaDTO>();
           
            foreach (var p in pipas)
            {
                var lecturas = AlmacenGasServicio.ObtenerLecturas(p.UnidadAlmacenGas.ToList()[0].IdAlmacenGas.Value);
                foreach (var item in lecturas)
                {
                    if (item.IdTipoEvento.Equals(TipoEventoEnum.Inicial))
                    {
                        RepInventarioPorPuntoVentaDTO r = new RepInventarioPorPuntoVentaDTO();
                        r.ID = Convertir.ConcatenarNumeros(item.IdCAlmacenGas, item.IdOrden);
                        r.NombreVehiculo = AlmacenGasServicio.ObtenerNombreUnidadAlmacenGas(item.UnidadAlmacenGas);
                        r.LecturaInicial = item.P5000 ?? 0;
                        r.ImagenLI = item.Fotografias.Count.Equals(0) ? string.Empty :  item.Fotografias.SingleOrDefault(x => x.IdOrden.Equals(item.IdOrden)).UrlImagen ?? string.Empty;
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
                            repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).LecturaFinal = item.P5000 ?? 0;
                            repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).ImagenLF = item.Fotografias.SingleOrDefault(x => x.IdOrden.Equals(item.IdOrden)).UrlImagen ?? string.Empty;
                            repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).Diferencia = CalculosGenerales.DiferenciaEntreDosNumero(li ,item.P5000 ?? 0);
                        }
                    }
                }
            }
            foreach (var e in estaciones)
            {
                var lecturas = AlmacenGasServicio.ObtenerLecturas(e.UnidadAlmacenGas.ToList()[0].IdAlmacenGas.Value);
                foreach (var item in lecturas)
                {
                    if (item.IdTipoEvento.Equals(TipoEventoEnum.Inicial))
                    {
                        RepInventarioPorPuntoVentaDTO r = new RepInventarioPorPuntoVentaDTO();
                        r.ID = Convertir.ConcatenarNumeros(item.IdCAlmacenGas, item.IdOrden);
                        r.NombreVehiculo = AlmacenGasServicio.ObtenerNombreUnidadAlmacenGas(item.UnidadAlmacenGas);
                        r.LecturaInicial = item.P5000 ?? 0;
                        r.ImagenLI = item.Fotografias.SingleOrDefault(x => x.IdOrden.Equals(item.IdOrden)).UrlImagen ?? string.Empty;
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
                            repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).LecturaFinal = item.P5000 ?? 0;
                            repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).ImagenLF = item.Fotografias.SingleOrDefault(x => x.IdOrden.Equals(item.IdOrden)).UrlImagen ?? string.Empty;
                            repo.FirstOrDefault(x => x.Fecha.ToShortDateString().Equals(item.FechaRegistro.ToShortDateString())).Diferencia = CalculosGenerales.DiferenciaEntreDosNumero(li, item.P5000 ?? 0);
                        }
                    }
                }
            }

            return repo;
        }
    }
}
