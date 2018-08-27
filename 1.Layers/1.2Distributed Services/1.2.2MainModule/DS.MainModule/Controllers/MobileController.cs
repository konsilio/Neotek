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
using Application.MainModule.DTOs.Mobile;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/mobile")]
    public class MobileController : ApiController
    {
        private Seguridad _seguridad;
        private Catalogos _catalogos;
        private Mobile _mobile;

        public MobileController()
        {
            _seguridad = new Seguridad();
            _catalogos = new Catalogos();
            _mobile = new Mobile();
        }

        [Route("servicio/disponible")]
        public HttpResponseMessage PostServicioDisponible()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new RespuestaDto() { Exito = true });
        }

        [AllowAnonymous]
        [Route("login")]
        public HttpResponseMessage PostLogin(LoginFbDTO autenticacionDto)
        {
            return RespuestaHttp.crearRespuesta(_seguridad.AutenticacionMobile(autenticacionDto), Request);
        }

        [Route("lista/ordenes/compra")]
        public HttpResponseMessage GetListaOrdenesCompra(short IdEmpresa, bool EsGas, bool EsActivoVenta, bool EsTransporteGas)
        {
            return RespuestaHttp.crearRespuesta(_mobile.ConsultarOrdenesCompra(IdEmpresa,EsGas,EsActivoVenta,EsTransporteGas), Request);
        }

        [Route("obtener/menu")]
        public HttpResponseMessage GetObtenerMenu()
        {
            return RespuestaHttp.crearRespuesta(_mobile.ObtenerMenu(), Request);
        }

        [Route("obtener/medidores")]
        public HttpResponseMessage GetObtenerMedidores()
        {
            return RespuestaHttp.crearRespuesta(_mobile.ObtenerMedidores(), Request);
        }

        [Route("obtener/almacenes")]
        public HttpResponseMessage GetObtenerAlmacenesGas()
        {
            return RespuestaHttp.crearRespuesta(_mobile.ObtenerAlmacenesGas(), Request);
        }

        [Route("registrar/papeleta")]
        public HttpResponseMessage PostRegistrarPapeleta(PapeletaDTO papeletaDTO)
        {
            return RespuestaHttp.crearRespuesta(_mobile.RegistrarPapeleta(papeletaDTO), Request);
        }

        [Route("iniciar/descarga")]
        public HttpResponseMessage PostInicializarDescarga(DescargaDto desDto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.InicializarDescarga(desDto), Request);
        }

        [Route("finalizar/descarga")]
        public HttpResponseMessage PostFinalizarDescarga(DescargaDto desDto)
        {
            return RespuestaHttp.crearRespuesta(_mobile.FinalizarDescarga(desDto), Request);
        }
    }
}
