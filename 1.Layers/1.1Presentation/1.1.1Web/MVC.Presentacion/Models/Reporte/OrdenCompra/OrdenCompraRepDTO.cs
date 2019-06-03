using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class OrdenCompraRepDTO
    {
        public string NumOrdenCompra { get; set; }
        public string Departamento { get; set; }
        public string NumRequisicion { get; set; }
        public string Requerimiento { get; set; }
        public string Estatus { get; set; }
        public string Pagado { get; set; }
        public DateTime Fecha { get; set; }
    }
}