using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile
{
    public class DatosTraspasoDto
    {
        //public short predeterminada { get; set; }
        public PipaDto PipaPredeterminada { get; set; }
        public List<PipaDto> pipas { get; set; }
        public List<PipaDto> pipaEntrada { get; set; }
        public List<EstacionesDto> estaciones { get; set; }
        public EstacionesDto EstacionPredeterminada { get; set; }
        public List<MedidorDto> medidores { get; set; }
    }
}
