using Application.MainModule.Flujos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web;
using System.Net.Http;
using DS.MainModule.Results;
using Application.MainModule.DTOs;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/equipotransporte")]
    public class EquipoTransporteController : ApiController
    {
        private Catalogos _eqTransporte;
        public EquipoTransporteController()
        {
            _eqTransporte = new Catalogos();
        }
        // GET: EquipoTransporte
        [Route("buscar/listavehiculos/{id}")]
        public HttpResponseMessage GetListaEquiposTransporte(short id)
        {
            return RespuestaHttp.crearRespuesta(_eqTransporte.ListaEquiposdeTransporte(id), Request);
        }

        [Route("buscar/vehiculo/{id}")]
        public HttpResponseMessage GetVehiculosId(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _eqTransporte.VehiculoId(id));
        }
        [Route("modificar/vehiculo")]
        public HttpResponseMessage PutModificarVehiculo(EquipoTransporteDTO _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _eqTransporte.Modifica(_model));
        }

        [Route("registrar/vehiculo")]
        public HttpResponseMessage PostRegistrarVehiculo(EquipoTransporteDTO _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _eqTransporte.Registra(_model));
        }

        [Route("eliminar/vehiculo/{id}")]
        public HttpResponseMessage PutEliminaVehiculo(int id)
        {
            return RespuestaHttp.crearRespuesta(_eqTransporte.Elimina(id), Request);
        }
    }
}