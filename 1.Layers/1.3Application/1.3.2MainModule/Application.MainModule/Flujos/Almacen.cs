using Application.MainModule.AdaptadoresDTO.Almacen;
using Application.MainModule.AdaptadoresDTO.Requisicion;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacen;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios.Requisiciones;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Constantes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.MainModule.Flujos
{
    public class Almacen
    {
        public RespuestaDto GenerarEntradaProducto(OrdenCompraEntradasDTO dto)
        {
            List<Sagas.MainModule.Entidades.Almacen> _almacen = new List<Sagas.MainModule.Entidades.Almacen>();
            List<Sagas.MainModule.Entidades.Almacen> _almacenCrear = new List<Sagas.MainModule.Entidades.Almacen>();
            List<AlmacenEntradaProducto> entradas = new List<AlmacenEntradaProducto>();
            var ProductosOC = OrdenCompraServicio.Buscar(dto.IdOrdenCompra).Productos.ToList();

            foreach (var prod in dto.Productos)
            {
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
                    prod.CantidadFinal = CalcularAlmacenServicio.ObtenerSumaEntradaAlmacen(AlmacenActualizar.Cantidad, prod.Cantidad); ;
                    _almacen.Add(AlmacenActualizar);
                    var EntradaProd = ProductoAlmacenServicio.GenerarAlmacenEntradaProcuto(prod, dto.IdOrdenCompra, _Almacen);
                    entradas.Add(EntradaProd);
                }
            }
            var respEntrada = ProductoAlmacenServicio.EntradaAlmcacenProductos(_almacen, _almacenCrear, entradas);
            if (respEntrada.Exito)
            {
                return new Compras().FinalizarEntradaProductoOrdenCompra(new DTOs.OrdenCompraDTO { IdOrdenCompra = dto.IdOrdenCompra });
            }
            return respEntrada;
        }
        public RespuestaDto GenerarSalidaProducto(RequisicionSalidaDTO dto)
        {
            List<Sagas.MainModule.Entidades.Almacen> _almacen = new List<Sagas.MainModule.Entidades.Almacen>();
            List<AlmacenSalidaProducto> Salidas = new List<AlmacenSalidaProducto>();
            var _requisicion = RequisicionAdapter.FromEntity(RequisicionServicio.Buscar(dto.IdRequisicion));
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
                    var AlmacenActualizar = ProductoAlmacenServicio.AlmacenEntity(_Almacen);
                    AlmacenActualizar.Cantidad = CalcularAlmacenServicio.ObtenerRestaSalidaAlmacen(AlmacenActualizar.Cantidad, prod.Cantidad);
                    prod.CantidadFinal = CalcularAlmacenServicio.ObtenerRestaSalidaAlmacen(AlmacenActualizar.Cantidad, prod.Cantidad);
                    if (prod.CantidadFinal < 0)
                        return ProductoAlmacenServicio.CantidadInsuficiente();
                    _almacen.Add(AlmacenActualizar);
                    var SalidaProd = ProductoAlmacenServicio.GenerarAlmacenSalidaProcuto(prod, dto.IdRequisicion, _Almacen);
                    Salidas.Add(SalidaProd);
                }
            }
            _requisicion = RequisicionServicio.DeterminaEstatusPorSalidas(_requisicion, _productos);
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
    }
}
