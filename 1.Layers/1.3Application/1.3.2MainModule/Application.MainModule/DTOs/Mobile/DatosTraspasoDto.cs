using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile
{
    public class DatosTraspasoDto
    {
        public short predeterminada { get; set; }
        public List<PipaDto> pipas { get; set; }
        public List<EstacionesDto> estaciones { get; set; }
        public List<MedidorDto> medidores { get; set; }
    }
}
