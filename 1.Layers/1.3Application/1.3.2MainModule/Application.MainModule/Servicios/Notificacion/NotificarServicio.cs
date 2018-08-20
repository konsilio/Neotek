using Application.MainModule.DTOs;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Notificacion
{
    public static class NotificarServicio
    {
        public static void RequisicionNueva(Sagas.MainModule.Entidades.Requisicion req, bool incluirMensajePush = false)
        {
            var usuAplicacion = TokenServicio.ObtenerUsuarioAplicacion();
            var roles = RolServicio.ObtenerRoles(usuAplicacion.Empresa).Where(x => x.RequisicionRevisarExistencia).ToList();
            
            var correoDto = new CorreoDto()
            {
                De = ObtenerCorreo(usuAplicacion),
                ParaLista = ObtenerCorreo(ObtenerDestinatarios(roles)),
                Asunto = string.Format(ConfigurationManager.AppSettings["Asunto_RequisicionRevisarExistencia"], req.NumeroRequisicion),
                Mensaje = CorreoHtmlServicio.RequisicionNueva(req),
            };

            //if (incluirMensajePush)
            //{
            //}

            //Enviar(correoDto);
            //MensajePushServicio.Enviar(dto);
        }

        private static List<Usuario> ObtenerDestinatarios(List<Rol> roles)
        {
            var destinatarios = new List<Usuario>();
            roles.ToList().ForEach(x => destinatarios.AddRange(x.ListaUsuarios));            
            return destinatarios.Distinct().ToList();
        }

        private static List<string> ObtenerCorreo(List<Usuario> usuarios)
        {            
            return usuarios.Select(x => ObtenerCorreo(x)).ToList();
        }

        private static string ObtenerCorreo(Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Email1))
                return usuario.Email1;

            if (!string.IsNullOrEmpty(usuario.Email2))
                return usuario.Email2;

            if (!string.IsNullOrEmpty(usuario.Email3))
                return usuario.Email3;

            return string.Empty;
        }

        private static void Enviar(CorreoDto dto)
        {
            EnviarCorreosServicio.Enviar(dto);            
        }

        //private static void Enviar(pushDto dto)
        //{
        //    MensajePushServicio.Enviar(dto);
        //}
    }
}
