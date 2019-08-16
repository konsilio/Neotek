using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Ventas
{
    public class VentasPipaDto
    {
        public string Concepto { get; set; }//GAS
        public decimal P5000Inicial { get; set; }
        public decimal P5000Final { get; set; }
        public decimal CantidadLt { get; set; }
        public decimal Venta { get; set; }//money   
    }
}