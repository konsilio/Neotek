using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.Flujos;
using DS.MainModule.Results;
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
        [Route("buscar/ordencompra/{idOC}")]
        public HttpResponseMessage GetBuscarOrdenCompra(int idOC)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _compras.BuscarOrdenCompra(idOC));
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
        /// <summary>
        /// Cambia de estatus a Autorizada
        /// </summary>
        /// <param name="idOrdenCompra"></param>
        /// <returns></returns>
        [Route("autoroizar/ordencompra")]
        public HttpResponseMessage PutAutorizarCompra(OrdenCompraAutorizacionDTO oc)
        {
            return RespuestaHttp.crearRespuesta(_compras.AutorizarOrdenCompra(oc), Request);
        }
        [Route("buscar/ordenescompra/{idEmpresa}")]
        public HttpResponseMessage GetOrdenesCompra(short idEmpresa)
        {
            return RespuestaHttp.crearRespuesta(_compras.ListaOrdenCompra(idEmpresa), Request);
        }
    }
}
