using Application.MainModule.DTOs.Seguridad;
using Security.MainModule.Token_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Seguridad
{
    public static class AutenticarServicio
    {
        public static RespuestaAutenticacionDto AutenticarUsuario(AutenticacionDto autenticacionDto)
        {
            var respuesta = new RespuestaAutenticacionDto()
            {
                token = TokenGenerator.GenerateTokenJwt(autenticacionDto.Usuario)
            };

            return respuesta;
        }

        //private static string GenerarToken()
        //{

        //}
    }
}
