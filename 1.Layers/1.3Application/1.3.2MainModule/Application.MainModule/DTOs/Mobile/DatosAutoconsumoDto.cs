using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile
{
    public class DatosAutoconsumoDto
    {
        public List<EstacionesDto> EstacionEntrada { get; set; }
        public List<EstacionesDto> EstacionSalida { get; set; }

        public EstacionesDto Predeterminada { get; set; }
    }
}
