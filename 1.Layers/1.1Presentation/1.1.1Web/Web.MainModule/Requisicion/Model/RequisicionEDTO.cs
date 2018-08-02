using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.Requisicion.Model
{
    public class RequisicionEDTO : RequisicionDTO
    {
        public List<RequisicionProductoEDTO> ListaProductos { get; set; }
    }
}