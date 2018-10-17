using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Compras;
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
        private Almacen _almacen;
        public AlmacenController()
        {
            _almacen = new Almacen();
        }
        [Route("entrada/producto")]
        public HttpResponseMessage PostGuardarEntradas(OrdenCompraEntradasDTO Entradas)
        {
            return RespuestaHttp.crearRespuesta(_almacen.GenerarEntradaProducto(Entradas), Request);
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
        //[Route("salida/producto")]
        //public HttpResponseMessage PostGuardarSalida(AlmacenSalidaProductoDTO Entradas)
        //{
        //    return RespuestaHttp.crearRespuesta(_almacen.GenerarSalidaProducto(Entradas), Request);
        //}
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
    }
}