﻿using Application.MainModule.DTOs;
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
using Mail.MainModule.DTO;
using Infrastructure.Data.Agentes;
using Sagas.MainModule.ObjetosValor.Constantes;

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
            Enviar(correoDto);
            if (!incluirMensajePush)
            {
                var Autorizacion = new KeyValuePair<string, string>("key", string.Concat("=", ConfigurationManager.AppSettings["AppNotificacionKeyAutorizacion"]));
                var js = new FBNotificacionDTO()
                {
                    registration_ids = ObtenerKeysMovile(destinatarios).ToArray(),
                    data = new Data
                    {
                        OrderNo = req.IdRequisicion.ToString(),
                        Tipo = NotificacionPushConst.RT001
                    },
                    notification = new Notification
                    {
                        text = string.Format(ConfigurationManager.AppSettings["Asunto_RequisicionRevisarExistencia"], req.NumeroRequisicion),
                        title = NotificacionPushConst.R0001
                    }
                };
                Enviar(js, Autorizacion);
            }
        }
        public static void OrdenDeCompraNueva(OrdenCompra oc, bool incluirMensajePush = false)
        {
            var usuAplicacion = TokenServicio.ObtenerUsuarioAplicacion();
            var roles = RolServicio.ObtenerRoles(usuAplicacion.Empresa).Where(x => x.CompraAutorizarOCompra).ToList();
            var destinatarios = ObtenerDestinatarios(roles);

            var correoDto = new CorreoDto()
            {
                De = ObtenerCorreo(usuAplicacion),
                ParaLista = ObtenerCorreo(destinatarios),
                Asunto = string.Format(ConfigurationManager.AppSettings["Asunto_RequisicionRevisarExistencia"], oc.NumOrdenCompra),
                Mensaje = CorreoHtmlServicio.OrdenCompraNueva(oc),
            };
            Enviar(correoDto);
            if (!incluirMensajePush)
            {
                var Autorizacion = new KeyValuePair<string, string>("key", string.Concat("=", ConfigurationManager.AppSettings["AppNotificacionKeyAutorizacion"]));
                var js = new FBNotificacionDTO()
                {
                    registration_ids = ObtenerKeysMovile(destinatarios).ToArray(),
                    data = new Data
                    {
                        OrderNo = oc.NumOrdenCompra.ToString(),
                        Tipo = NotificacionPushConst.RT001
                    },
                    notification = new Notification
                    {
                        text = string.Format(ConfigurationManager.AppSettings["Asunto_RequisicionRevisarExistencia"], oc.NumOrdenCompra),
                        title = NotificacionPushConst.R0001
                    }
                };
                Enviar(js, Autorizacion);
            }
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
            return usuarios.Where(x => x.MovileKey != null).Select(y => y.MovileKey).ToList();
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

        private static FBNotificacionDTO Enviar(FBNotificacionDTO dto, KeyValuePair<string, string> Autorizacion)
        {
            var agente = new AgenteServicio();
           
            
            agente.PostMethod(dto, "https://fcm.googleapis.com/", "fcm/send", Autorizacion);
            return dto;


            //var result = "-1";
            //var webAddr = "https://fcm.googleapis.com/fcm/send";
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            //httpWebRequest.ContentType = "application/json";
            ////httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=" +  ConfigurationManager.AppSettings["AppNotificacionKeyAutorizacion"]);
            //httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=" + "AAAArTr6G44:APA91bFujr3tEdesRnwtYGkUtABZeZudWn0kVhD383ts2HWyo4RvzRFgK28POs2IYxjbTQnqMwa9rjJN30Xpogjtz_KV6QuwFFJFyqqQxXOLwkbBCZQPWmgFnBvep_jh7YcEfJ_rmFhnal8gE4i3Uo3U4MeI-uAQQg");
            //httpWebRequest.Method = "POST";
            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //string strNJson = @"{
            //    ""registration_ids"": ""fUrVmoad8oM:APA91bGQ9YR4WbE5oxALMw-dvqkbbMg5iKt3_dAbAyHzJSy-BTNNdlCsQ_sZwxCpDT-P3SmgCWto-v6FMeQLLIu-L0EEdyajTRBeWJmaKKjdjEZYuuHwuHWcnvuAOhDXK5Ldpb7-FnbHCk6LqjjkZ2HTkXrlGYYbsA"",
            //    ""data"": {
            //        ""ShortDesc"": ""Some short desc"",
            //        ""IncidentNo"": ""any number"",
            //        ""Description"": ""detail desc"",
            //        ""Tipo"": ""R"", 
            //        ""OrderNo"": ""1""
            //            },
            //            ""notification"": {
            //                        ""title"": ""SAGAS: Alerta"",
            //            ""text"": ""Mensaje"", 
            //            ""sound"":""default""
            //            }
            //        }";


            //strNJson = strNJson.Replace("[[Titulo]]", dto.Titulo);
            //strNJson = strNJson.Replace("[[keyMovil]]", dto.UsuarioKey);
            //strNJson = strNJson.Replace("[[Tipo]]", dto.TipoNotificacion);
            //strNJson = strNJson.Replace("[[NoOrden]]", dto.Id.ToString());
            //strNJson = strNJson.Replace("[[Mensaje]]", dto.Mensaje);

            //streamWriter.Write(js);
            //streamWriter.Flush();

            //}
            //try
            //{

            //    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //    {
            //        result = streamReader.ReadToEnd();
            //        dto.Exito = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    dto.MensajesError.Add(ex.Message);
            //    if (ex.InnerException != null)
            //        dto.MensajesError.Add(ex.InnerException.Message);
            //    dto.Exito = false;
            //}

        }
    }
}
