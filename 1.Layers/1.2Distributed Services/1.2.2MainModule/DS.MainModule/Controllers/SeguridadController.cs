﻿using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.Flujos;
using DS.MainModule.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DS.MainModule.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/seguridad")]
    public class SeguridadController : ApiController
    {
        private Seguridad _seguridad;

        public SeguridadController()
        {
            _seguridad = new Seguridad();
        }

        [Route("servicio/disponible")]
        public HttpResponseMessage PostServicioDisponible()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new RespuestaDto() { Exito = true });
        }

        [Route("login")]
        public HttpResponseMessage PostLogin(AutenticacionDto autenticacionDto)
        {   
            return Request.CreateResponse(HttpStatusCode.OK, _seguridad.Autenticacion(autenticacionDto));
       }       

        #region Usuarios

        [Route("usuarios/listausuarios")]
        public HttpResponseMessage GetAllUsuarios()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _seguridad.AllUsers());
        }

        [Route("usuarios/listausuarios/{idEmpresa}")]
        public HttpResponseMessage GetListaUsuarios(short idEmpresa)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _seguridad.ListaUsuarios(idEmpresa));
        }

        [Route("registra/usuarios")]
        public HttpResponseMessage PostRegistraUsuario(UsuarioDTO usuarioDto)
        {
            return RespuestaHttp.crearRespuesta(_seguridad.AltaUsuarios(usuarioDto), Request);
        }

        [Route("modifica/usuariocredencial")]
        public HttpResponseMessage PutModificaCredencial(UsuarioDTO usuarioDto)
        {
            return RespuestaHttp.crearRespuesta(_seguridad.ModificaCredencial(usuarioDto), Request);
        }

        [Route("modifica/usuario")]
        public HttpResponseMessage PutModificaUsuario(UsuarioDTO usuarioDto)
        {
            return RespuestaHttp.crearRespuesta(_seguridad.ModificaUsuario(usuarioDto), Request);
        }

        [Route("elimina/usuario/{id}")]
        public HttpResponseMessage PutEliminaUsuario(short id)
        {
            return RespuestaHttp.crearRespuesta(_seguridad.EliminaUsuario(id), Request);
        }

        [Route("asignar/rol")]
        public HttpResponseMessage PostUsuarioRolAgrega(UsuarioRolDto uRDto)
        {
            return RespuestaHttp.crearRespuesta(_seguridad.AsignarRol(uRDto), Request);
        }

        [Route("eliminar/usuariorol")]
        public HttpResponseMessage PutEliminaRol(UsuarioRolDto uRDto)
        {
            return RespuestaHttp.crearRespuesta(_seguridad.EliminaRolAsignado(uRDto), Request);
        }

        #endregion

        #region Roles

        [Route("roles/listaAllRoles")]
        public HttpResponseMessage GetAllRoles()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _seguridad.AllRoles());
        }
        [Route("registra/roles")]
        public HttpResponseMessage PostRegistraRol(RolDto rolDto)
        {
            return RespuestaHttp.crearRespuesta(_seguridad.AltaRoles(rolDto), Request);
        }

        [Route("modifica/nombrerol")]
        public HttpResponseMessage PutModificaRol(RolDto rolDto)
        {
            return RespuestaHttp.crearRespuesta(_seguridad.ModificaRolName(rolDto), Request);
        }

        [Route("modifica/permisos")]
        public HttpResponseMessage PutModificaPermiso(List<RolDto> rolDto)
        {
            return RespuestaHttp.crearRespuesta(_seguridad.ModificaPermisos(rolDto), Request);
        }

        [Route("elimina/rol/{id}")]
        public HttpResponseMessage PutEliminaRol(short id)
        {
            return RespuestaHttp.crearRespuesta(_seguridad.EliminaRol(id), Request);
        }
        #endregion
    }
}
