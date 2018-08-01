using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.Requisicion.Model
{
    [Serializable]
    public class RequisicionProductoGridDTO
    {
        public int IdProducto { get; set; }        
        public string Producto { get; set; }
        public int IdTipoProducto { get; set; }
        public string TipoProducto { get; set; }
        public int IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public decimal Cantidad { get; set; }
        public string Aplicacion { get; set; }
        public int IdUnidad { get; set; }
        public string Unidad { get; set; }
    }
}