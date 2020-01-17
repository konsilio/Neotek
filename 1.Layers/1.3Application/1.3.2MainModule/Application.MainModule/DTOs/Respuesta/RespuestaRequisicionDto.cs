using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Respuesta
{
    public class RespuestaRequisicionDto 
    {
        public int IdRequisicion { get; set; }
        public string NumRequisicion { get; set; }
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
    }
}
