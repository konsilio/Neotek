using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile
{
    public class DatosCalibracionDto
    {
        public List<EstacionesDto> estaciones { get; set; }
        public List<MedidorDto> medidores { get; set; }
    }
}
