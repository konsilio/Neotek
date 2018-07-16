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
    public class SeguridadController : ApiController
    {
        private Seguridad _seguridad;

        public SeguridadController()
        {
            _seguridad = new Seguridad();
        }

        [Route("api/seguridad/login")]
        public HttpResponseMessage PostLogin(AutenticacionDto autenticacionDto)
        {   ;
            return Request.CreateResponse(HttpStatusCode.OK, _seguridad.Autenticacion(autenticacionDto));
        }
    }
}
