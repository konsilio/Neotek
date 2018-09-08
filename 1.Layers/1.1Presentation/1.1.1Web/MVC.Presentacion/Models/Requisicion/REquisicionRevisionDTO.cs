using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    public class RequisicionRevisionDTO : RequisicionDTO
    { 
        public List<RequisicionProductoRevisionDTO> ListaProductos { get; set; }
    }
}