using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class RemanenteDto
    {
        public decimal RemanenteKg { get; set; }
        public decimal RemanenteLt { get; set; }
        public decimal RemanenteAcumuladoDiaKg { get; set; }
        public decimal RemanenteAcumuladoDiaLt { get; set; }
        public decimal RemanenteAcumuladoMesKg { get; set; }
        public decimal RemanenteAcumuladoMesLt { get; set; }
        public decimal RemanenteAcumuladoAnioKg { get; set; }
        public decimal RemanenteAcumuladoAnioLt { get; set; }
    }
}
