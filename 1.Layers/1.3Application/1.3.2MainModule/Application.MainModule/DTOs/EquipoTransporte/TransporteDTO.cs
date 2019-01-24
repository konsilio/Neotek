using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Transporte
{
   public class TransporteDTO
    {
        public short IdEmpresa { get; set; }
        public int IdChofer { get; set; }
        public string Chofer { get; set; }
        public int IdVehiculo { get; set; }
        public string Vehiculo { get; set; }
    }
}
