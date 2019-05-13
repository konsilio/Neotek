using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class RequisicionRepDTO
    {
        public string NumRequisicon { get; set; }
        public string Departamento { get; set; }
        public string Estatus { get; set; }
        public string Requisicion { get; set; }
        public DateTime Fecha { get; set; }
    }
}