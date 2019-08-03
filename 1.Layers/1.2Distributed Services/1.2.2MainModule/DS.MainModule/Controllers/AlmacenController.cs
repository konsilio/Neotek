using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.Flujos;
using DS.MainModule.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/almacen")]
    public class AlmacenController : ApiController
    {
        private Almacenes _almacen;
        public AlmacenController()
        {
            _almacen = new Almacenes();
        }
        [Route("entrada/producto")]
        public HttpResponseMessage PostGuardarEntradas(OrdenCompraEntradasDTO Entradas)
        {
            return RespuestaHttp.crearRespuesta(_almacen.GenerarEntradaProducto(Entradas), Request);
        }
        [Route("salida/producto")]
        public HttpResponseMessage PostGuardarSalida(RequisicionSalidaDTO Salidas)
        {
            return RespuestaHttp.crearRespuesta(_almacen.GenerarSalidaProducto(Salidas), Request);
        }
        [Route("buscar/ordecompraentrada/{IdOrdenCompra}")]
        public HttpResponseMessage GetOrdenCompraEntrada(int IdOrdenCompra)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _almacen.BuscarOrdenCompra(IdOrdenCompra));
        }
        [Route("buscar/productos/{idEmpresa}")]
        public HttpResponseMessage GetProductosAlmacen(short idEmpresa)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _almacen.ProductosAlmacen(idEmpresa));
        }        
        [Route("actualiza/almacenproducto")]
        public HttpResponseMessage PostActulizarAlmacenProducto(AlmacenDTO Update)
        {
            return RespuestaHttp.crearRespuesta(_almacen.ActualizarAlmacen(Update), Request);
        }
        [Route("buscar/registro/{idEmpresa}")]
        public HttpResponseMessage GetRegistroAlmacen(short idEmpresa)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _almacen.RegistroAlmacen(idEmpresa));
        }
        [Route("buscar/almacen/requisicion/{idRequisicon}")]
        public HttpResponseMessage GetRequisiconAlmacen(short idRequisicon)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _almacen.BuscarRequsicionSalida(idRequisicon));
        }
        [AllowAnonymous]
        [Route("aplicar/almacen/descarga")]
        public HttpResponseMessage GetAplicarDescargas()
        { 
            return Request.CreateResponse(HttpStatusCode.OK, _almacen.AplicarDescargas());
        }
        [Route("buscar/remanente/general")]
        public HttpResponseMessage PostRemanenteGeneral(RemanenteDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _almacen.ConsultarRemanenteGeneral(dto));
        }
        [Route("buscar/remanente/puntoventa")]
        public HttpResponseMessage RemanentePorPV(RemanenteDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _almacen.ConsultarRemanentePorPuntoventa(dto));
        }
        [Route("buscar/remanente/tracto")]
        public HttpResponseMessage PostRemanenteTracto(RemanenteDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _almacen.ConsultarRemanenteTracto(dto));
        }
        [Route("buscar/unidadalmacen")]
        public HttpResponseMessage GetListaUnidadAlmacen()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _almacen.ListaUnidadesAlmacen());
        }
    }
}