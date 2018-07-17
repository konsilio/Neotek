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
            // Buscamos la existencia del usuario, validando su contraseña
            // var usuario = ObtenerUsuario()

            //if(usuario == null)
            //    return new RespuestaAutenticacionDto()
            //    {
            //        token = string.Empty,

            //    };

            var respuesta = new RespuestaAutenticacionDto()
            {
                token = TokenGenerator.GenerateTokenJwt(autenticacionDto.Usuario, autenticacionDto.Password, "30")
            };

            return respuesta;
        }

        //private static string GenerarToken()
        //{

        //}
    }
}
