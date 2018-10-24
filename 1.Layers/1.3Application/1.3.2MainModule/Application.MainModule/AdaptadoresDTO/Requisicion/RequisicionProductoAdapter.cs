using System.Collections.Generic;
using System.Linq;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Requisicion;

namespace Application.MainModule.AdaptadoresDTO.Requisiciones
{
    public static class RequisicionProductoAdapter
    {
        public static RequisicionProductoDTO ToDTO(RequisicionProducto _reqProducto)
        {
            RequisicionProductoDTO reqDTO = new RequisicionProductoDTO()
            {
                IdRequisicion = _reqProducto.IdRequisicion,
                IdProducto = _reqProducto.IdProducto,
                IdTipoProducto = _reqProducto.IdTipoProducto,
                IdCentroCosto = _reqProducto.IdCentroCosto,
                Cantidad = _reqProducto.Cantidad,
                Aplicacion = _reqProducto.Aplicacion,
                RevisionFisica = _reqProducto.RevisionFisica,
                CantidadAlmacenActual = _reqProducto.CantidadAlmacenActual,
                CantidadAComprar = _reqProducto.CantidadAComprar,
                AutorizaEntrega = _reqProducto.AutorizaEntrega,
                AutorizaCompra = _reqProducto.AutorizaCompra
            };
            return reqDTO;
        }
        public static RequisicionProductoAutorizacionDTO ToAutDTO(RequisicionProducto _reqProducto)
        {
            return new RequisicionProductoAutorizacionDTO()
            {
                IdProducto = _reqProducto.IdProducto,
                Producto = _reqProducto.Producto.Descripcion,
                TipoProducto = _reqProducto.Producto.TipoServicioOProducto.Nombre,
                Unidad = _reqProducto.Producto.UnidadMedida.Nombre,
                IdCentroCosto = _reqProducto.IdCentroCosto,
                CentroCosto = _reqProducto.CentroCosto.Descripcion,
                Cantidad = _reqProducto.Cantidad,
                Aplicacion = _reqProducto.Aplicacion,
                CantidadAlmacenActual = _reqProducto.CantidadAlmacenActual.Value,
                CantidadAComprar = _reqProducto.CantidadAComprar.Value,            
                EsActivoVenta = _reqProducto.EsActivoVenta,
                EsGas = _reqProducto.EsGas,
                EsTransporteGas = _reqProducto.EsTransporteGas
            };            
           
        }
        public static List<RequisicionProductoAutorizacionDTO> ToAutDTO(List<RequisicionProducto> _reqProductos)
        {
            List<RequisicionProductoAutorizacionDTO> _prodDTO = _reqProductos.ToList().Select(x => ToAutDTO(x)).ToList();
            return _prodDTO;
        }
        public static List<RequisicionProductoDTO> ToDTO(List<RequisicionProducto> _reqProdcutos)
        {
            List<RequisicionProductoDTO> lReqProd = _reqProdcutos.ToList().Select(x => ToDTO(x)).ToList();
            return lReqProd;
        }
        public static List<RequisicionProductoRevisionDTO> ToRevDTO(List<RequisicionProducto> _reqProdcutos)
        {
            List<RequisicionProductoRevisionDTO> lReqProd = _reqProdcutos.ToList().Select(x => ToRevDTO(x)).ToList();
            return lReqProd;
        }
        public static RequisicionProductoRevisionDTO ToRevDTO(RequisicionProducto _reqProducto)
        {
            RequisicionProductoRevisionDTO reqDTO = new RequisicionProductoRevisionDTO()
            {
                IdProducto = _reqProducto.IdProducto,
                Producto = _reqProducto.Producto.Descripcion,
                IdTipoProducto = _reqProducto.IdTipoProducto,
                Orden = _reqProducto.Orden,
                TipoProducto = _reqProducto.Producto.TipoServicioOProducto.Nombre,
                IdUnidad = _reqProducto.Producto.IdUnidadMedida,
                Unidad = _reqProducto.Producto.UnidadMedida.Nombre,
                CentroCosto = _reqProducto.CentroCosto.Descripcion,
                IdCentroCosto = _reqProducto.IdCentroCosto,
                Cantidad = _reqProducto.Cantidad,
                Aplicacion = _reqProducto.Aplicacion,
                RevisionFisica = _reqProducto.RevisionFisica.HasValue ? _reqProducto.RevisionFisica.Value : false,
                CantidadAlmacenActual = _reqProducto.CantidadAlmacenActual.Value,
                CantidadAComprar = _reqProducto.Cantidad - _reqProducto.CantidadAlmacenActual.Value
            };
            return reqDTO;
        }
        public static RequisicionProducto FromDTO(RequisicionProductoDTO _reqProducto)
        {
            RequisicionProducto reqDTO = new RequisicionProducto()
            {
                IdRequisicion = _reqProducto.IdRequisicion,
                IdProducto = _reqProducto.IdProducto,
                Orden = _reqProducto.Orden,
                IdTipoProducto = _reqProducto.IdTipoProducto,
                IdCentroCosto = _reqProducto.IdCentroCosto,
                Cantidad = _reqProducto.Cantidad,
                Aplicacion = _reqProducto.Aplicacion,
                RevisionFisica = _reqProducto.RevisionFisica,
                CantidadAlmacenActual = _reqProducto.CantidadAlmacenActual,
                CantidadAComprar = _reqProducto.CantidadAComprar,
                AutorizaEntrega = _reqProducto.AutorizaEntrega,
                AutorizaCompra = _reqProducto.AutorizaCompra
            };
            return reqDTO;
        }
        public static List<RequisicionProducto> FromDTO(List<RequisicionProductoDTO> _reqProdcutos)
        {
            List<RequisicionProducto> lReqProd = _reqProdcutos.ToList().Select(x => FromDTO(x)).ToList();
            return lReqProd;
        }
        public static RequisicionProducto FromDTO(RequisicionProductoGridDTO _reqProducto)
        {
            RequisicionProducto reqDTO = new RequisicionProducto()
            {
                IdProducto = _reqProducto.IdProducto,
                IdTipoProducto = _reqProducto.IdTipoProducto,
                IdCentroCosto = _reqProducto.IdCentroCosto,
                Cantidad = _reqProducto.Cantidad,
                Aplicacion = _reqProducto.Aplicacion,

            };
            return reqDTO;
        }
        public static List<RequisicionProducto> FromDTO(List<RequisicionProductoGridDTO> _reqProdcutos)
        {
            List<RequisicionProducto> lReqProd = _reqProdcutos.ToList().Select(x => FromDTO(x)).ToList();
            return lReqProd;
        }
        public static RequisicionProducto FromDTO(RequisicionProductoRevisionDTO _reqProducto)
        {
            RequisicionProducto reqDTO = new RequisicionProducto()
            {
                IdProducto = _reqProducto.IdProducto,
                RevisionFisica = _reqProducto.RevisionFisica,
                CantidadAlmacenActual = _reqProducto.CantidadAlmacenActual,
                CantidadAComprar = _reqProducto.CantidadAComprar
            };
            return reqDTO;
        }
        public static List<RequisicionProducto> FromDTO(List<RequisicionProductoRevisionDTO> _reqProductos)
        {
            List<RequisicionProducto> lprod = _reqProductos.ToList().Select(x => FromDTO(x)).ToList();
            return lprod;
        }
        public static RequisicionProducto FromDTO(RequisicionProdReviPutDTO _reqProducto, RequisicionProducto _entityAnteriro)
        {
            RequisicionProducto _prod = FromEntity(_entityAnteriro);
            _prod.RevisionFisica = _reqProducto.RevisionFisica;
            return _prod;
        }
        public static List<RequisicionProducto> FromDTO(List<RequisicionProdReviPutDTO> _reqProductos, List<RequisicionProducto> _prodEntidadAnteriro)
        {
            List<RequisicionProducto> _lproducto = _reqProductos.ToList().Select(x => FromDTO(x, _prodEntidadAnteriro.ToList().SingleOrDefault(y => y.IdProducto.Equals(x.IdProducto) && y.Orden.Equals(x.Orden)))).ToList();
            return _lproducto;

        }
        public static RequisicionProducto FromDTO(RequisicionProdAutPutDTO _reqProducto)
        {
            RequisicionProducto prod = new RequisicionProducto()
            {
                IdProducto = _reqProducto.IdProducto,
                AutorizaEntrega = _reqProducto.AutorizaEntrega,
                AutorizaCompra = _reqProducto.AutorizaCompra,
                CantidadAComprar = _reqProducto.CantidadAComprar
            };
            return prod;
        }
        public static List<RequisicionProducto> FromDTO(List<RequisicionProdAutPutDTO> _reqProductos)
        {
            List<RequisicionProducto> listaProd = _reqProductos.ToList().Select(x => FromDTO(x)).ToList();
            return listaProd;
        }
        public static RequisicionProducto FromEntity(RequisicionProducto _prodEntity)
        {
            RequisicionProducto _prod = new RequisicionProducto()
            {
                IdRequisicion = _prodEntity.IdRequisicion,
                IdProducto = _prodEntity.IdProducto,
                IdCentroCosto = _prodEntity.IdCentroCosto,
                Orden = _prodEntity.Orden,
                IdTipoProducto = _prodEntity.IdTipoProducto,
                Cantidad = _prodEntity.Cantidad,
                Aplicacion = _prodEntity.Aplicacion,
                RevisionFisica = _prodEntity.RevisionFisica,
                CantidadAlmacenActual = _prodEntity.CantidadAlmacenActual,
                CantidadAComprar = _prodEntity.CantidadAComprar,
                AutorizaEntrega = _prodEntity.AutorizaEntrega,
                AutorizaCompra = _prodEntity.AutorizaCompra,
                EsActivoVenta = _prodEntity.EsActivoVenta,
                EsGas = _prodEntity.EsGas,
                EsTransporteGas = _prodEntity.EsTransporteGas                
            };
            return _prod;
        }
        public static List<RequisicionProducto> FromEntity(List<RequisicionProducto> _prodEntity)
        {
            List<RequisicionProducto> lProd = _prodEntity.ToList().Select(x => FromEntity(x)).ToList();
            return lProd;
        }
    }
}
