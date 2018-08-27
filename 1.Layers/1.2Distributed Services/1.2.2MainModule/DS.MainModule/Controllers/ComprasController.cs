using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.Flujos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/compras")]
    public class ComprasController : ApiController
    {
        private Compras _compras;

        public ComprasController()
        {
            _compras = new Compras();
        }

        [Route("gas")]
        public HttpResponseMessage PostCompraGas(string empty)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _compras.ComprarGas());
        }
        [Route("buscar/requisicion/{idReq}")]
        public HttpResponseMessage GetBuscarReq(int idReq)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _compras.BuscarRequisicion(idReq));
        }
        [Route("guardar/ordencompra")]
        public HttpResponseMessage PostGenerarOrdenesCompra(OrdenCompraCrearDTO ocDTO)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _compras.GenerarOrdenesCompra(ocDTO));
        }
        [Route("cancelar/ordencompra")]
        public HttpResponseMessage PutCancelarOrdenCompra(OrdenCompraDTO ocDTO)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _compras.CancelarOrdenCompra(ocDTO));
        }
        [Route("autoroizar/ordencompra")]
        public HttpResponseMessage PutAutorizarCompra(OrdenCompraDTO ocDTo)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _compras.AutorizarOrdenCompra(ocDTo));
        }
    }
}
