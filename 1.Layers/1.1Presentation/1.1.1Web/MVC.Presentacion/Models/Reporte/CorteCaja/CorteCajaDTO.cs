using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class CorteCajaDTO
    {
        public string Descripcion { get; set; }
        public double Cantidad { get; set; }
        public string Unidad { get; set; }
        public double TotalVenta { get; set; }
    }
}