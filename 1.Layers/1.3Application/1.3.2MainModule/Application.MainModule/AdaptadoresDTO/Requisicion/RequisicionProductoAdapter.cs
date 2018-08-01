using System.Collections.Generic;
using System.Linq;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Requisicion;

namespace Application.MainModule.AdaptadoresDTO.Requisicion
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
        public static List<RequisicionProductoDTO> ToDTO(List<RequisicionProducto> _reqProdcutos)
        {
            List<RequisicionProductoDTO> lReqProd = _reqProdcutos.ToList().Select(x => ToDTO(x)).ToList();
            return lReqProd;
        }
        public static RequisicionProducto FromDTO(RequisicionProductoDTO _reqProducto)
        {
            RequisicionProducto reqDTO = new RequisicionProducto()
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
        public static List<RequisicionProducto> FromDTO(List<RequisicionProductoDTO> _reqProdcutos)
        {
            List<RequisicionProducto> lReqProd = _reqProdcutos.ToList().Select(x => FromDTO(x)).ToList();
            return lReqProd;
        }
    }
}
