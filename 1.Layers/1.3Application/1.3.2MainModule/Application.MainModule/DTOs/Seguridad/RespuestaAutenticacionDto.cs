using Application.MainModule.DTOs.Respuesta;
using System.Collections.Generic;

namespace Application.MainModule.DTOs.Seguridad
{
    public class RespuestaAutenticacionDto : RespuestaDto
    {
        //public string token { get; set; }
        public int IdUsuario { get; set; }
        public string token { get; set; }
        public MenuDto LstRoles { get; set; }
    }
}
