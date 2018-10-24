using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.Flujos;
using DS.MainModule.Results;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/requisicion")]
    public class RequisicionController : ApiController
    {
        private Requisiciones _requisicion;

        public RequisicionController()
        {
            _requisicion = new Requisiciones();
        }

        [Route("guardar/requisicion")]
        public HttpResponseMessage PostRequisicion(RequisicionDTO req)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _requisicion.InsertRequisicionNueva(req));
        }
        [Route("buscar/requisiciones/{idEmpresa}")]
        public HttpResponseMessage GetRequisicionesByIdEmpresa(short idEmpresa)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _requisicion.BuscarRequisicionesPorEmpresa(idEmpresa));
        }
        [Route("buscar/requisicion/{numRequi}")]
        public HttpResponseMessage GetRequisicionByNumRequisicion(int numRequi)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _requisicion.BuscarRequisicion(numRequi));
        }
        [Route("buscar/requisicion/autorizacion/{numRequi}")]
        public HttpResponseMessage GetRequisicionByNumRequisicionAut(int numRequi)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _requisicion.BuscarRequisicionAuto(numRequi));
        }
        [Route("update/requisicion/revision")]
        public HttpResponseMessage PutActulizarRevision(RequisicionRevPutDTO req)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _requisicion.ActualizarRequisicionRevision(req));
        }
        [Route("update/requisicion/autorizacion")]
        public HttpResponseMessage PutActulizarAutorizacion(RequisicionAutPutDTO req)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _requisicion.ActualizarRequisicionAutorizacion(req));
        }
        [Route("cancela/requisicion")]
        public HttpResponseMessage PutCancelarRequisicion(RequisicionDTO req)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _requisicion.CancelarRequisicion(req));
        }
        [Route("buscar/requisicion/estatus")]
        public HttpResponseMessage GetRequisicionEstatus()
        {
            return RespuestaHttp.crearRespuesta(_requisicion.ListaEstatus(), Request);
        }
    }
}