using Application.MainModule.DTOs.Cobranza;
using Application.MainModule.Flujos;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/cobranza")]
    public class CobranzaController : ApiController
    {
        private Cobranza _cobranza;
        public CobranzaController()
        {
            _cobranza = new Cobranza();
        }

        [Route("buscar/listacargos/{id}")]
        public HttpResponseMessage GetListaCargosEmpresa(short id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _cobranza.ListaCargos(id));
        }
        [Route("buscar/cargos/{id}")]
        public HttpResponseMessage GetPedidosId(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _cobranza.CargoId(id));
        }

        [Route("registrar/abono")]
        public HttpResponseMessage PostRegistrarAbonos(AbonosDTO _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _cobranza.Registra(_model));
        }
        [Route("registrar/abonos")]
        public HttpResponseMessage PostRegistrarAbonosLst(List<AbonosDTO> _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _cobranza.Registra(_model));
        }

    }
}