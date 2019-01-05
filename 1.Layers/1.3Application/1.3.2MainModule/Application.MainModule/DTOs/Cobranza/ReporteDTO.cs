using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Cobranza
{
    public class ReporteDTO
    {
        public List<CarteraVencidaDTO> global { get; set; }
        public List<CargosDTO> reportedet { get; set; }
    }
}
