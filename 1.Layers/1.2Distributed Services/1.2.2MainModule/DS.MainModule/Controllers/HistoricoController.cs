using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.MainModule.Flujos;
using System.Web.Http;
using Application.MainModule.DTOs;
using DS.MainModule.Results;
using System.Net.Http;
using System.Net;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/historico")]
    public class HistoricoController : ApiController
    {
        private Historicos _historico;
        public HistoricoController()
        {
            _historico = new Historicos();
        }

        [Route("registar/historicoventas")]
        [HttpPost]
        public HttpResponseMessage PostHistoricoVentas(List<HistoricoVentaDTO> historico)
        {
            return RespuestaHttp.crearRespuesta(_historico.CrearCatalogo(historico), Request);
        }

        [Route("consultar/historicoventas")]
        [HttpGet]
        public HttpResponseMessage GetHistoricoVentas()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _historico.TodoCatalogo());
        }

        [Route("consultar/historicoventasid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetHistoricoById(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _historico.TodoCatalogo(id));
        }

        [Route("modificar/historicoventas")]
        [HttpPut]
        public HttpResponseMessage PutHistorico(HistoricoVentaDTO historico)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _historico.ModificarCatalogo(historico));
        }

        [Route("eleiminar/historicoventas/{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteHistorico(HistoricoVentaDTO historico)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _historico.EliminarCatalogo(historico));
        }

        [Route("consultar/years")]
        [HttpGet]
        public HttpResponseMessage GetYears(YearDTO years)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _historico.ObtenerElementosDistintos());
        }
        [Route("consultar/json")]
        [HttpPost]
        public HttpResponseMessage GetJson(HistoricoConsultaDTO json)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _historico.GenerarJsonConsulta(json));
        }
        [Route("consultar/yearstotales")]
        [HttpPost]
        public HttpResponseMessage GetVentasTotales(HistoricoConsultaDTO dto2)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _historico.VentasConsultas(dto2));
        }
    }
}