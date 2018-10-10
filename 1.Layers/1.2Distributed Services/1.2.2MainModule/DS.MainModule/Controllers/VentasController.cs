using Application.MainModule.Flujos;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/ventas")]
    public class VentasController : ApiController
    {
        private Ventas _ventas;
        public VentasController()
        {
            _ventas = new Ventas();
        }

        #region Caja General
        [Route("buscar/listacajageneral/{idEmpresa}")]
        public HttpResponseMessage GetListaCajaGralId(short idEmpresa)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.CajaGeneralIdEmpresa(idEmpresa));
        }

        [Route("buscar/listacajageneral")]
        public HttpResponseMessage GetListaCajaGeneral()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.CajaGeneral());
        }

        [Route("buscar/listacajageneralcamioneta/{cve}")]
        public HttpResponseMessage GetListaCajaGralCamioneta(string cve)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.CajaGeneralCamioneta(cve));
        }
        #endregion
    }
}