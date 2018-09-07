using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    public class RequisicionRevPutDTO :RequisicionDTO
    { 
        public List<RequisicionProdReviPutDTO> ListaProductos { get; set; }
    }
}