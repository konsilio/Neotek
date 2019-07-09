using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class CuentasConsolidadasDTO
    {
        public string Concepto { get; set; }
        public decimal CantidadAutorizada { get; set; }
        public decimal CantidadGastada { get; set; }
        public decimal Diferencia { get; set; }
    }
}
