using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Requisicion;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Compras
{
    public class ProductosOCAdapter
    {
        public static ProductoOCDTO ToDTO(RequisicionProducto _prod)
        {
            return new ProductoOCDTO()
            {
                IdProducto = _prod.IdProducto,
                Producto = _prod.Producto.Descripcion,
                TipoProducto = _prod.Producto.TipoServicioOProducto.Nombre,
                CantidadAComprar = _prod.CantidadAComprar.Value,
                Unidad = _prod.Producto.UnidadMedida.Nombre,
                Aplicacion = _prod.Aplicacion,
                CentroCosto = _prod.CentroCosto.Descripcion,
                IdCentroCosto = _prod.CentroCosto.IdCentroCosto,
                EsActivoVenta = _prod.EsActivoVenta,
                EsGas = _prod.EsGas,
                EsTransporteGas = _prod.EsTransporteGas
            };

        }
        public static List<ProductoOCDTO> ToDTO(List<RequisicionProducto> _prods)
        {
            List<ProductoOCDTO> Prods = _prods.ToList().Select(x => ToDTO(x)).ToList();
            return Prods;
        }
        public static OrdenCompraProductoDTO ToDTO(ProductoOCDTO _prod)
        {
            OrdenCompraProductoDTO _prodDTO = new OrdenCompraProductoDTO()
            {
                IdProducto = _prod.IdProducto,
                ProductoServicioTipo = _prod.ProductoServicioTipo,
                Producto = _prod.Producto,
                Categoria = _prod.Categoria,
                Linea = _prod.Linea,
                UnidadMedida = _prod.Unidad,
                UnidadMedida2 = _prod.UnidadMedida2,
                Descripcion = _prod.Descripcion,
                Precio = _prod.Precio,
                Descuento = _prod.Descuento,
                IVA = _prod.IVA,
                IEPS = _prod.IEPS,
                Importe = _prod.Importe,
                EsActivoVenta = _prod.EsActivoVenta,
                EsGas = _prod.EsGas,
            };
            return _prodDTO;
        }
        public static OrdenCompraProductoDTO ToDTO(OrdenCompraProductoCrearDTO _prod)
        {
            OrdenCompraProductoDTO _prodDTO = new OrdenCompraProductoDTO()
            {
                IdProducto = _prod.IdProducto,
                ProductoServicioTipo = _prod.ProductoServicioTipo,
                Producto = _prod.Producto,
                Categoria = _prod.Categoria,
                Linea = _prod.Linea,
                UnidadMedida = _prod.UnidadMedida,
                UnidadMedida2 = _prod.UnidadMedida2,
                Descripcion = _prod.Descripcion,
                Precio = _prod.Precio,
                Descuento = _prod.Descuento,
                IVA = _prod.IVA,
                IEPS = _prod.IEPS,
                Importe = _prod.Importe,
                EsActivoVenta = _prod.EsActivoVenta,
                EsGas = _prod.EsGas,
            };
            return _prodDTO;
        }
        public static OrdenCompraProducto FromDTO(OrdenCompraProductoCrearDTO _prod)
        {
            Producto p = new ProductoDataAccess().BuscarProducto(_prod.IdProducto);
            OrdenCompraProducto _prodDTO = new OrdenCompraProducto()
            {
                IdProducto = _prod.IdProducto,
                ProductoServicioTipo = p.TipoServicioOProducto.Nombre,
                IdCentroCosto = _prod.IdCentroCosto,
                Producto = p.Descripcion,
                Categoria = _prod.Categoria,
                Cantidad = _prod.Cantidad,
                Linea = _prod.Linea,
                UnidadMedida = p.UnidadMedida.Acronimo,
                UnidadMedida2 = _prod.UnidadMedida2,
                Descripcion = _prod.Descripcion,
                Precio = _prod.Precio,
                Descuento = _prod.Descuento,
                IVA = _prod.IVA,
                IEPS = _prod.IEPS,
                Importe = _prod.Importe,
                EsActivoVenta = _prod.EsActivoVenta,
                EsGas = _prod.EsGas,
            };
            return _prodDTO;
        }
        public static OrdenCompraProductoCrearDTO ToDTOc(OrdenCompraProducto _prod)
        {
            var prodRequ = RequisicionServicio.BuscarRequisiconProductoPorId(_prod.IdProducto, _prod.OrdenCompra.IdRequisicion);
            OrdenCompraProductoCrearDTO _prodDTO = new OrdenCompraProductoCrearDTO()
            {
                IdProducto = _prod.IdProducto,
                ProductoServicioTipo = _prod.ProductoServicioTipo,
                Producto = _prod.Producto,
                TipoProducto = _prod.ProductoServicioTipo,
                Categoria = _prod.Categoria,
                Linea = _prod.Linea,
                UnidadMedida = _prod.UnidadMedida,
                UnidadMedida2 = _prod.UnidadMedida2,
                Descripcion = prodRequ.Aplicacion,
                Precio = _prod.Precio,
                Cantidad = _prod.Cantidad,
                CantidadRequerida = prodRequ.Cantidad,
                IdCentroCosto = _prod.IdCentroCosto,
                CentroCosto = _prod.CentroCosto.Numero,
                IdCuentaContable = _prod.OrdenCompra.IdCuentaContable,
                CuentaContable = CuentaContableServicio.ObtenerCuentaContable(_prod.OrdenCompra.IdCuentaContable).Descripcion,//BuscarNombre 
                Descuento = _prod.Descuento,
                IVA = _prod.IVA,
                IEPS = _prod.IEPS,
                Importe = _prod.Importe,
                EsActivoVenta = _prod.EsActivoVenta,
                EsGas = _prod.EsGas,

            };
            return _prodDTO;
        }
        public static List<OrdenCompraProductoCrearDTO> ToDTO(List<OrdenCompraProducto> _prods)
        {
            List<OrdenCompraProductoCrearDTO> Prods = _prods.ToList().Select(x => ToDTOc(x)).ToList();
            return Prods;
        }
        public static OrdenCompraProductoDTO ToDTOx(OrdenCompraProducto p)
        {
            return new OrdenCompraProductoDTO()
            {
                IdProducto = p.IdProducto,
                IdOrdenCompra = p.IdOrdenCompra,               
                ProductoServicioTipo = p.ProductoServicioTipo,
                Producto = p.Producto,                
                Categoria = p.Categoria,
                Linea = p.Linea,
                UnidadMedida = p.UnidadMedida,
                UnidadMedida2 = p.UnidadMedida2,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Cantidad = p.Cantidad,              
                IdCentroCosto = p.IdCentroCosto,               
                Descuento = p.Descuento,
                IVA = p.IVA,
                IEPS = p.IEPS,
                Importe = p.Importe,
                EsActivoVenta = p.EsActivoVenta,
                EsGas = p.EsGas               
            };
        }
        public static List<OrdenCompraProductoDTO> ToDTOx(List<OrdenCompraProducto> _prods)
        {
          return _prods.ToList().Select(x => ToDTOx(x)).ToList();
        }
    }
}
