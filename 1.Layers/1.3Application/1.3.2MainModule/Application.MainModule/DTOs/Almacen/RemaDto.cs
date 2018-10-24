using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class RemaDto
    {
        public decimal RemaKg { get; set; }
        public decimal RemaLt { get; set; }
        public decimal RemaAcumDiaKg { get; set; }
        public decimal RemaAcumDiaLt { get; set; }
        public decimal RemaAcumMesKg { get; set; }
        public decimal RemaAcumMesLt { get; set; }
        public decimal RemaAcumAnioKg { get; set; }
        public decimal RemaAcumAnioLt { get; set; }
    }
}
