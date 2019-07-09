using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class CuentaConsolidadaDTO
    {
        public string Concepto { get; set; }
        public decimal CantidadAutorizada { get; set; }
        public decimal CantidadGastada { get; set; }
        public decimal Diferencia { get; set; }
    }
}