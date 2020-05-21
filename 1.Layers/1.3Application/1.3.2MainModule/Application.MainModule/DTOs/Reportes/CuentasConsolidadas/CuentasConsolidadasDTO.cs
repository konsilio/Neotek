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
        public string CantidadAutorizada { get; set; }
        public string CantidadGastada { get; set; }
        public string CantidadPagada { get; set; }
        public string Diferencia { get; set; }
    }
}
