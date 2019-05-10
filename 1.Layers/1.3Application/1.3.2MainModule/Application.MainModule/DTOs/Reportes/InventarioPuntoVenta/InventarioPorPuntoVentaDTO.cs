using Application.MainModule.DTOs.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class InventarioPorPuntoVentaDTO
    {
        public DateTime Fecha { get; set; }
        public List<PipaDTO> Pipas { get; set; }
        public List<EstacionCarburacionDTO> Estaciones { get; set; }
    }
}
