using Application.MainModule.DTOs.Almacen;
using Application.MainModule.Flujos;
using DS.MainModule.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/almacen")]
    public class AlmacenController : ApiController
    {
        private Almacen _almacen;
        public AlmacenController()
        {
            _almacen = new Almacen();
        }
        [Route("entrada/producto")]
        public HttpResponseMessage PostGuardarEntradas(List<AlmacenCrearEntradaDTO> Entradas)
        {
            return RespuestaHttp.crearRespuesta(_almacen.GenerarEntradaProducto(Entradas), Request);
        }
    }
}