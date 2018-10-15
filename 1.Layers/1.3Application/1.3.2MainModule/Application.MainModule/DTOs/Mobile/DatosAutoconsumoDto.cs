using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class DatosAutoconsumoDto
    {
        public List<EstacionesDto> EstacionSalida { get; set; }
        public List<EstacionesDto> EstacionEntrada { get; set; }
    }
}
