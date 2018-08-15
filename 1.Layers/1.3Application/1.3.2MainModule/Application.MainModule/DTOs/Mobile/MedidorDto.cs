using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class MedidorDto
    {
        public short IdTipoMedidor { get; set; }
        public string NombreTipoMedidor { get; set; }
        public byte CantidadFotografias { get; set; }
    }
}
