using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class CuentaConsolidadaDTO
    {
        public string Concepto { get; set; }
        public string CantidadAutorizada { get; set; }
        public string CantidadGastada { get; set; }
        public string CantidadPagada { get; set; }
        public string Diferencia { get; set; }
    }
}