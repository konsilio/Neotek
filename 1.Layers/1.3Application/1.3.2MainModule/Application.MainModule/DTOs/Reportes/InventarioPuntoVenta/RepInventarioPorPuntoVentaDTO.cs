using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
     public class RepInventarioPorPuntoVentaDTO
    {
        public int ID { get; set; }
        public string NombreVehiculo { get; set; }
        public decimal LecturaInicial { get; set; }
        public decimal LecturaFinal { get; set; }
        public string ImagenLI { get; set; }
        public string ImagenLF { get; set; }
        public decimal Diferencia { get; set; }
        public DateTime Fecha { get; set; }
    }
}
