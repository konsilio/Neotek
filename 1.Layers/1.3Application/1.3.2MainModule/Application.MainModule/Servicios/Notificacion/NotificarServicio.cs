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
using Mail.MainModule.DTO;
using Infrastructure.Data.Agentes;
using Sagas.MainModule.ObjetosValor.Constantes;
using Application.MainModule.Servicios.AccesoADatos;

namespace Application.MainModule.Servicios.Notificacion
{
    public static class NotificarServicio
    {
        public static void RequisicionNueva(Requisicion req, bool incluirMensajePush = false)
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
                try
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
                catch (Exception ){                   
                }
               
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
        public static void ConfirmacionPago(OrdenCompra oc)
        {
            var usuAplicacion = TokenServicio.ObtenerUsuarioAplicacion();
            var roles = RolServicio.ObtenerRoles(usuAplicacion.Empresa).Where(x => x.CompraAtiendeServicioOCompra).ToList();
            var destinatarios = ObtenerDestinatarios(roles);

            var correoDto = new CorreoDto()
            {
                De = ObtenerCorreo(usuAplicacion),
                ParaLista = ObtenerCorreo(destinatarios),
                Asunto = string.Format(ConfigurationManager.AppSettings["Asunto_SolicitudPago"], oc.NumOrdenCompra),
                Mensaje = CorreoHtmlServicio.OrdenCompraNueva(oc),
            };
            Enviar(correoDto);
            //if (!incluirMensajePush)
            //{
            //    var Autorizacion = new KeyValuePair<string, string>("key", string.Concat("=", ConfigurationManager.AppSettings["AppNotificacionKeyAutorizacion"]));
            //    var js = new FBNotificacionDTO()
            //    {
            //        registration_ids = ObtenerKeysMovile(destinatarios).ToArray(),
            //        data = new Data
            //        {
            //            OrderNo = oc.NumOrdenCompra.ToString(),
            //            Tipo = NotificacionPushConst.RT001
            //        },
            //        notification = new Notification
            //        {
            //            text = string.Format(ConfigurationManager.AppSettings["Asunto_SolicitudPago"], oc.NumOrdenCompra),
            //            title = NotificacionPushConst.R0001
            //        }
            //    };
            //    Enviar(js, Autorizacion);
            //}
        }
        public static void ProductoAutorizado(Requisicion req, List<RequisicionProducto> Prod, bool incluirMensajePush = false)
        {
            var usuAplicacion = TokenServicio.ObtenerUsuarioAplicacion();
            //var roles = RolServicio.ObtenerRoles(usuAplicacion.Empresa).Where(x => x.CompraAutorizarOCompra).ToList();
            //var destinatarios = ObtenerDestinatarios(roles);
            var destinatarios = new UsuarioDataAccess().Buscar(TokenServicio.ObtenerIdEmpresa()).Where(x => x.IdUsuario.Equals(req.IdUsuarioSolicitante)).ToList();
            var correoDto = new CorreoDto()
            {
                De = ObtenerCorreo(usuAplicacion),
                ParaLista = ObtenerCorreo(destinatarios),
                Asunto = ConfigurationManager.AppSettings["Asunto_ProductoListoParaEntrega"],
                Mensaje = CorreoHtmlServicio.ProductoAutorizadoEntrega(req),
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
                        text = string.Format(ConfigurationManager.AppSettings["Asunto_ProductoListoParaEntrega"], req.NumeroRequisicion),
                        title = NotificacionPushConst.R0001
                    }
                };
                Enviar(js, Autorizacion);
            }
        }
        private static List<Usuario> ObtenerDestinatarios(List<Rol> roles)
        {
            var destinatarios = new List<Usuario>();
            foreach (var rol in roles)
            {
                if (rol.UsuariosRoles != null && rol.UsuariosRoles.Count > 0)
                {
                    destinatarios.AddRange(rol.UsuariosRoles.Select(ru => ru.Usuario).ToList());
                }
            }            
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
            if (!string.IsNullOrEmpty(dto.De) && dto.ParaLista.Count > 0)
            {
                EnviarCorreosServicio.Enviar(dto);
            }
        }
        private static FBNotificacionDTO Enviar(FBNotificacionDTO dto, KeyValuePair<string, string> Autorizacion)
        {
            var agente = new AgenteServicio();
            agente.PostMethod(dto, ConfigurationManager.AppSettings["urlFireBase_Base"], ConfigurationManager.AppSettings["urlFireBase_EnviarNotificacionSerivicio"], Autorizacion);
            return dto;
        }
    }
}
