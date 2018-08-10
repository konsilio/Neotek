
using Application.MainModule.DTOs.Compras;
using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
   public static class OrdenesCompraAdapter
    {
        public static RespuestaOrdenesCompraDTO ToDTO(OrdenCompra ordenCompra)
        {
            return new RespuestaOrdenesCompraDTO()
            {
                IdOrdenCompra = ordenCompra.IdOrdenCompra,
                NumOrdenCompra = ordenCompra.NumOrdenCompra,
                Ieps = ordenCompra.Ieps,
                FechaRequisicion = ordenCompra.Requisicion.FechaRequerida,
                Productos = ToDTO(ordenCompra.Productos.ToList()),
            };
        }

        public static ProductoDTO ToDTO (OrdenCompraProducto ordenCompraProducto)
        {
            return new ProductoDTO()
            {
                Cantidad = ordenCompraProducto.Cantidad,
                Descuento = ordenCompraProducto.Descuento,
                IdOrdenCompra = ordenCompraProducto.IdOrdenCompra,
                IEPS = ordenCompraProducto.IEPS,
                Importe = ordenCompraProducto.Importe,
                IVA = ordenCompraProducto.IVA,
                Precio = ordenCompraProducto.Precio,
                Producto = ordenCompraProducto.Producto,
                UnidadMedida = ordenCompraProducto.UnidadMedida,
            };
        }
        public static List<ProductoDTO> ToDTO(List<OrdenCompraProducto> ordenCompraProducto)
        {
            return ordenCompraProducto.ToList().Select(x => ToDTO(x)).ToList();
        }

        public static List<RespuestaOrdenesCompraDTO> ToDTO(List<OrdenCompra> listOrdenCompra)
        {
            return listOrdenCompra.ToList().Select(x => ToDTO(x)).ToList();
        }
    }
}
