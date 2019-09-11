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
        public string LecturaInicial { get; set; }
        public string LecturaFinal { get; set; }
        public string ImagenLI { get; set; }
        public string ImagenLF { get; set; }
        public string Diferencia { get; set; }
        //public decimal Porcentaje { get; set; }
        public DateTime Fecha { get; set; }
    }
}
