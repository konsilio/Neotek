using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class AutoConsumoDTO
    {

        public string AlmacenGasSalida { get; set; }
        public string AlmacenGasEntrada { get; set; }
        public string TipoEvento { get; set; }
        public decimal SalidaP5000 { get; set; }
        public string ClaveOperacion { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}
