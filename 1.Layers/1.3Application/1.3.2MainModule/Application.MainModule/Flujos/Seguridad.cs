using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Seguridad
    {
        public RespuestaAutenticacionDto Autenticacion(AutenticacionDto autenticacionDto)
        {
            return AutenticarServicio.AutenticarUsuario(autenticacionDto);
        }
        public RespuestaAutenticacionMobileDto AutenticacionMobile(LoginFbDTO autenticacionDto)
        {
            var responce = AutenticarServicio.AutenticarUsuarioMobile(autenticacionDto);
            if (!responce.Exito)
                return responce;
            var usuario = UsuarioServicio.Obtener(responce.IdUsuario);
            usuario = UsuarioAdapter.FromEntity(usuario);
            usuario.MovileKey = autenticacionDto.FbToken;
            var respuesta = UsuarioServicio.Actualizar(usuario);
            if (!respuesta.Exito)
            {
                responce.Exito = respuesta.Exito;
                responce.EsActulizacion = respuesta.EsActulizacion;
                responce.Codigo = respuesta.Codigo;
                responce.Mensaje = respuesta.Mensaje;
                responce.MensajesError = respuesta.MensajesError;                
            }

            return responce;
        }
    }
}
