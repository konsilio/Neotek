using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
   public class TipoPersonaDTO
    {
        public byte IdTipoPersona { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}
