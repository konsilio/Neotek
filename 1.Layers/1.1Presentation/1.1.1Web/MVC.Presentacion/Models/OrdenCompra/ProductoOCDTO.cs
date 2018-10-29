using DevExpress.Web.Demos;
using Exceptions.MainModule.Validaciones;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Requisicion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.OrdenCompra
{
    [Serializable]
    public class ProductoOCDTO : OrdenCompraProductoDTO
    {       
        public static List<ProductoOCDTO> GetEditableProducts()
        {
            List<ProductoOCDTO> products = (List<ProductoOCDTO>)HttpContext.Current.Session["Products"];
            if (products == null)
            {
                //products = OrdenCompraServicio.DatosRequisicion(1195, HttpContext.Current.Session["StringToken"].ToString()).ProductosOC.ToList();
                HttpContext.Current.Session["Products"] = products;
            }
            return products;
        }
        public static ProductoOCDTO GetEditableProduct(int productID)
        {
            return (from product in GetEditableProducts() where product.IdProducto == productID select product).FirstOrDefault();
        }
    }
}