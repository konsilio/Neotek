using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class DatosAnticiposCorteDto
    {
        public List<EstacionesDto> estaciones { get; set; }
        public List<AnticipoDto> anticipos { get; set; }
        public List<CorteDto> cortes { get; set; }
    }
}
