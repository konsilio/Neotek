using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    public class RequisicionAutPutDTO : RequisicionDTO
    {
             public List<RequisicionProdAutPutDTO> ListaProductos { get; set; }
    }
}