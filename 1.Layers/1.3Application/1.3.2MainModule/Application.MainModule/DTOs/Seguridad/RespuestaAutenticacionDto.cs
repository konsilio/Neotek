using Application.MainModule.DTOs.Respuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Seguridad
{
    public class RespuestaAutenticacionDto : RespuestaDto
    {
        public string token { get; set; }
        public int IdUsuario { get; set; }
    }
}
