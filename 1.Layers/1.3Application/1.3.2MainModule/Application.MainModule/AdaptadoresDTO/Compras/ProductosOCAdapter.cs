﻿using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Compras
{
    public class ProductosOCAdapter
    {
        public static ProductoOCDTO ToDTO(Sagas.MainModule.Entidades.RequisicionProducto _prod)
        {
            ProductoOCDTO Prod = new ProductoOCDTO();
            Prod.IdProducto = _prod.IdProducto;
            Prod.Producto = _prod.Producto.Descripcion;
            Prod.TipoProducto = _prod.Producto.TipoServicioOProducto.Nombre;
            Prod.CantidadAComprar = _prod.CantidadAComprar.Value;
            Prod.Unidad = _prod.Producto.UnidadMedida.Nombre;
            Prod.Aplicacion = _prod.Aplicacion;
            Prod.CentroCosto = _prod.CentroCosto.Descripcion;
            return Prod;
        }
        public static List<ProductoOCDTO> ToDTO(List<Sagas.MainModule.Entidades.RequisicionProducto> _prods)
        {
            List<ProductoOCDTO> Prods = _prods.ToList().Select(x => ToDTO(x)).ToList();
            return Prods;
        }
        public static OrdenCompraProductoDTo ToDTO(ProductoOCDTO _prod)
        {
            OrdenCompraProductoDTo _prodDTO = new OrdenCompraProductoDTo()
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
        public static OrdenCompraProductoDTo ToDTO(OrdenCompraProductoCrearDTO _prod)
        {
            OrdenCompraProductoDTo _prodDTO = new OrdenCompraProductoDTo()
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
        OrdenCompraProductoCrearDTO
    }
}
