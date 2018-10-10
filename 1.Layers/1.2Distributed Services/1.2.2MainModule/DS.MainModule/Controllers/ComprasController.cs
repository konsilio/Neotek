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
        /// <summary>
        ///Busca los datos de la requisicion necesarios para generar una orden de compra  
        /// </summary>
        /// <param name="idReq"></param>
        /// <returns></returns>
        [Route("buscar/requisicion/{idReq}")]
        public HttpResponseMessage GetBuscarReq(int idReq)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _compras.BuscarRequisicion(idReq));
        }
        /// <summary>
        /// Busca una orden de compra por su IdOrdenCompra
        /// </summary>
        /// <param name="idOC"></param>
        /// <returns></returns>
        [Route("buscar/ordencompra/{idOC}")]
        public HttpResponseMessage GetBuscarOrdenCompra(int idOC)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _compras.BuscarOrdenCompra(idOC));
        }
        [Route("buscar/ordencompra/complemento/gas/{idOC}")]
        public HttpResponseMessage GetOrdenCompraComplementoGas(int idOC)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _compras.BuscarComplementoGas(idOC));
        }
        /// <summary>
        /// Genera las ordenes de compra y les asigna los productos necesarios segun los distintos proveedore y los guarda en la base de datos
        /// </summary>
        /// <param name="ocDTO"></param>
        /// <returns></returns>
        [Route("guardar/ordencompra")]
        public HttpResponseMessage PostGenerarOrdenesCompra(OrdenCompraCrearDTO ocDTO)
        {
            return RespuestaHttp.crearRespuesta(_compras.GenerarOrdenesCompra(ocDTO), Request);
        }
        /// <summary>
        /// La orden de compra es actualizda a un estatus de cancelacion.
        /// </summary>
        /// <param name="ocDTO"></param>
        /// <returns></returns>
        [Route("cancelar/ordencompra")]
        public HttpResponseMessage PutCancelarOrdenCompra(OrdenCompraDTO ocDTO)
        {
            return RespuestaHttp.crearRespuesta(_compras.CancelarOrdenCompra(ocDTO), Request);
        }
        /// <summary>
        /// Cambia de estatus a Autorizada
        /// </summary>
        /// <param name="idOrdenCompra"></param>
        /// <returns></returns>
        [Route("autorizar/ordencompra")]
        public HttpResponseMessage PutAutorizarCompra(OrdenCompraDTO oc)
        {
            return RespuestaHttp.crearRespuesta(_compras.AutorizarOrdenCompra(oc), Request);
        }
        [Route("buscar/ordenescompra/{idEmpresa}")]
        public HttpResponseMessage GetOrdenesCompra(short idEmpresa)
        {
            return RespuestaHttp.crearRespuesta(_compras.ListaOrdenCompra(idEmpresa), Request);
        }
        [Route("buscar/ordenescompra/estatus")]
        public HttpResponseMessage GetEstatus()
        {
            return RespuestaHttp.crearRespuesta(_compras.ListaEstatus(), Request);
        }
        [Route("actualiza/ordencompra/datosfactura")]
        public HttpResponseMessage PutDatosFactura(OrdenCompraDTO oc)
        {
            return RespuestaHttp.crearRespuesta(_compras.ActualizarOrdenCompraFactura(oc), Request);
        }
        [Route("guardar/confirmacionpago")]
        public HttpResponseMessage PutConfirmarPago(OrdenCompraPagoDTO oc)
        {
            return RespuestaHttp.crearRespuesta(_compras.ConfirmarPago(oc), Request);
        }
        [Route("guardar/pago")]
        public HttpResponseMessage PostGenerarPago(OrdenCompraPagoDTO oc)
        {
            return RespuestaHttp.crearRespuesta(_compras.CrearOrdenCompraPago(oc), Request);
        }
        [Route("buscar/pagos/{idOC}")]
        public HttpResponseMessage GetListaPagos(int idOC)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _compras.BuscarPagos(idOC));
        }
        [Route("solictud/pago/expedidor")]
        public HttpResponseMessage PutSolicitarPagoExpedidor(ComplementoGasDTO oc)
        {
            return RespuestaHttp.crearRespuesta(_compras.SolicitarPagoExipedidor(oc), Request);
        }
        [Route("solictud/pago/porteador")]
        public HttpResponseMessage PutSolicitarPagoPorteador(ComplementoGasDTO oc)
        {
            return RespuestaHttp.crearRespuesta(_compras.SolicitarPagoPorteador(oc), Request);
        }
        [Route("solictud/pago/mercancia")]
        public HttpResponseMessage PutSolicitarPago(ComplementoGasDTO oc)
        {
            return RespuestaHttp.crearRespuesta(_compras.SolicitarPagoPorteador(oc), Request);
        }
    }
}
