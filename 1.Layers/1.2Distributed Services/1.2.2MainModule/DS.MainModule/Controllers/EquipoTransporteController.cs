using Application.MainModule.Flujos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web;
using System.Net.Http;
using DS.MainModule.Results;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.EquipoTransporte;
using Application.MainModule.DTOs.Transporte;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/equipotransporte")]
    public class EquipoTransporteController : ApiController
    {
        private Catalogos _eqTransporte;
        private RecargaCombustible _rc;

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
        #region RecargaCombustible
        [Route("buscar/recargascombustible/{id}")]
        public HttpResponseMessage GetRecargasCombustible(short id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _rc.Todo(id));
        }
        [Route("modificar/recargacombustible")]
        public HttpResponseMessage PutModificarRecargaCombustible(RecargaCombustibleDTO _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _rc.Modificar(_model));
        }

        [Route("registrar/recargacombustible")]
        public HttpResponseMessage PostRegistrarRecargaCombustible(RecargaCombustibleDTO _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _rc.Crear(_model));
        }

        [Route("eliminar/recargacombustible/{id}")]
        public HttpResponseMessage PutEliminaRecargaCombustible(int id)
        {
            return RespuestaHttp.crearRespuesta(_rc.Eliminar(id), Request);
        }
        #endregion
        #region Asignacion
        [Route("buscar/asignacion")]
        public HttpResponseMessage GetAsigancion()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _eqTransporte.BuscarAsignaciones());
        }
        [Route("registrar/asignacion")]
        public HttpResponseMessage PostRegistrarAsignacion(TransporteDTO _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _eqTransporte.RegistraAsignacion(_model));
        }

        [Route("eliminar/asignacion/{id}")]
        public HttpResponseMessage PutEliminaAsignacion(TransporteDTO _modeld)
        {
            return RespuestaHttp.crearRespuesta(_eqTransporte.EliminarAsignacion(_modeld), Request);
        }
        #endregion
    }
}