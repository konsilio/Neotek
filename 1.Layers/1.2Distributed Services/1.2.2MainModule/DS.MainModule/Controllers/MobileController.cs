using Application.MainModule.DTOs.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Application.MainModule.Flujos;
using Application.MainModule.DTOs.Respuesta;
using DS.MainModule.Results;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/mobile")]
    public class MobileController : ApiController
    {
        private Seguridad _seguridad;
        private Catalogos _catalogos;

        public MobileController()
        {
            _seguridad = new Seguridad();
            _catalogos = new Catalogos();
        }

        [Route("servicio/disponible")]
        public HttpResponseMessage PostServicioDisponible()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new RespuestaDto() { Exito = true });
        }

        [AllowAnonymous]
        [Route("login")]
        public HttpResponseMessage PostLogin(AutenticacionDto autenticacionDto)
        {
            return RespuestaHttp.crearRepuesta(_seguridad.AutenticacionMobile(autenticacionDto), Request);
        }

    }
}
