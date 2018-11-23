using System;
using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile
{
    public class DatosAnticiposCorteDto
    {
        public List<EstacionesDto> estaciones { get; set; }
        public List<AnticipoDto> anticipos { get; set; }
        public List<CorteDto> cortes { get; set; }
        public List<DateTime> fechasCorte { get; set; }
        public decimal TotalAnticiposCorte { get; set; }
    }
}
