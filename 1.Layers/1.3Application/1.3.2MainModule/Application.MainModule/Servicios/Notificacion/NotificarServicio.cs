using Application.MainModule.DTOs;
using Application.MainModule.Servicios.Seguridad;
using Utilities.MainModule;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
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
            var destinatarios = ObtenerDestinatarios(roles);
            var correoDto = new CorreoDto()
            {
                De = ObtenerCorreo(usuAplicacion),
                ParaLista = ObtenerCorreo(destinatarios),
                Asunto = string.Format(ConfigurationManager.AppSettings["Asunto_RequisicionRevisarExistencia"], req.NumeroRequisicion),
                Mensaje = CorreoHtmlServicio.RequisicionNueva(req),
            };
           
                var NotDto = new NotificacionDTO()
                {
                    UsuarioKey = FilterFunciones.ConcatenarLista((ObtenerKeysMovile(destinatarios)),','),
                    Mensaje = string.Format(ConfigurationManager.AppSettings["Asunto_RequisicionRevisarExistencia"], req.NumeroRequisicion),
                    Id = req.IdRequisicion
                };
            
            
            Enviar(correoDto);
            if (incluirMensajePush)
                Enviar(NotDto);
            //MensajePushServicio.Enviar(dto);
        }
        public static void OrdenDeCompraNueva(OrdenCompra oc, bool incluirMensajePush = false)
        {
            var usuAplicacion = TokenServicio.ObtenerUsuarioAplicacion();
            var roles = RolServicio.ObtenerRoles(usuAplicacion.Empresa).Where(x => x.CompraAutorizarOCompra).ToList();
            var destinatarios = ObtenerDestinatarios(roles);           

            var NotDto = new NotificacionDTO()
            {
                UsuarioKey = FilterFunciones.ConcatenarLista((ObtenerKeysMovile(destinatarios)), ','),
                Mensaje = string.Format(ConfigurationManager.AppSettings["Asunto_RequisicionRevisarExistencia"], oc.NumOrdenCompra),
                Id = oc.IdOrdenCompra
            };
                      
            if (incluirMensajePush)
                Enviar(NotDto);
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
        private static List<string> ObtenerKeysMovile(List<Usuario> usuarios)
        {
            return usuarios.Select(x => x.MovileKey).ToList();
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

        private static NotificacionDTO Enviar(NotificacionDTO dto)
        {
            var result = "-1";
            var webAddr = "https://fcm.googleapis.com/fcm/send";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=" +  ConfigurationManager.AppSettings["AppNotificacionKeyAutorizacion"]);
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string strNJson = @"{
                    ""registration_ids"": "" [[keyMovil]] "",
                    ""data"": {
                        ""ShortDesc"": ""Some short desc"",
                        ""IncidentNo"": ""any number"",
                        ""Description"": ""detail desc"",
                        ""Tipo"": "" [[Tipo]] "", 
                        ""OrderNo"": "" [[NoOrden]]  ""
                            },
                            ""notification"": {
                                        ""title"": ""SAGAS: [[Titulo]] "",
                            ""text"": "" [[Mensaje]] "", 
                            ""sound"":""default""
                            }
                        }";
                strNJson = strNJson.Replace("[[Titulo]]", dto.Titulo);
                strNJson = strNJson.Replace("[[keyMovil]]", dto.UsuarioKey);
                strNJson = strNJson.Replace("[[Tipo]]", dto.TipoNotificacion);
                strNJson = strNJson.Replace("[[NoOrden]]", dto.Id.ToString());
                strNJson = strNJson.Replace("[[Mensaje]]", dto.Mensaje);

                streamWriter.Write(strNJson);
                streamWriter.Flush();
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    dto.Exito = true;
                }
            }
            catch (Exception ex)
            {
                dto.MensajesError.Add(ex.Message);
                if (ex.InnerException != null)
                    dto.MensajesError.Add(ex.InnerException.Message);
                dto.Exito = false;
            }
            return dto;
        }
    }
}
