using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.Flujos;
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
    }
}
