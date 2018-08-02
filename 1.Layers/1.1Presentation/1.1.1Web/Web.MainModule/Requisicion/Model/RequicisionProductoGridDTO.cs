using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.Requisicion.Model
{
    [Serializable]
    public class RequisicionProductoGridDTO : RequisicionProductoDTO
    {       
        public string Producto { get; set; }       
        public string TipoProducto { get; set; }      
        public string CentroCosto { get; set; }          
        public int IdUnidad { get; set; }
        public string Unidad { get; set; }
    }
}