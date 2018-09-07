using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    public class RequisicionEDTO :RequisicionDTO
    {
        public List<RequisicionProductoEDTO> ListaProductos { get; set; }
    }
}