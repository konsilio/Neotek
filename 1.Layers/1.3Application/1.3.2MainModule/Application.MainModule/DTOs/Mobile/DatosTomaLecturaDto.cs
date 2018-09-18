using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class DatosTomaLecturaDto
    {
        public List<AlmacenDto> Almacenes { get; set; }
        public List<MedidorDto> Medidores { get; set; }
    }
}
