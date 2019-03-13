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
            return Request.CreateResponse(HttpStatusCode.OK, _eqTransporte.ModificaEquipoTrasnporte(_model));
        }

        [Route("registrar/vehiculo")]
        public HttpResponseMessage PostRegistrarVehiculo(EquipoTransporteDTO _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _eqTransporte.RegistraEquipoTransporte(_model));
        }

        [Route("eliminar/vehiculo/{id}")]
        public HttpResponseMessage PutEliminaVehiculo(int id)
        {
            return RespuestaHttp.crearRespuesta(_eqTransporte.EliminaEquipoTransporte(id), Request);
        }

        #region RecargaCombustible
        [Route("buscar/recargascombustible/{id}")]
        public HttpResponseMessage GetRecargasCombustible(short id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new RecargaCombustible().Todo(id));
        }
        [Route("modificar/recargacombustible")]
        public HttpResponseMessage PutModificarRecargaCombustible(RecargaCombustibleDTO _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new RecargaCombustible().Modificar(_model));
        }

        [Route("registrar/recargacombustible")]
        public HttpResponseMessage PostRegistrarRecargaCombustible(RecargaCombustibleDTO _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new RecargaCombustible().Crear(_model));
        }
        [Route("eliminar/recargacombustible")]
        public HttpResponseMessage PutEliminaRecargaCombustible(RecargaCombustibleDTO _model)
        {
            return RespuestaHttp.crearRespuesta(new RecargaCombustible().Eliminar(_model), Request);
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
        public HttpResponseMessage PutEliminaAsignacion(TransporteDTO _model)
        {
            return RespuestaHttp.crearRespuesta(_eqTransporte.EliminarAsignacion(_model), Request);
        }
        #endregion

        #region Mantenimiento
        [Route("buscar/mantenimiento")]
        public HttpResponseMessage GetMantenimiento()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Mantenimiento().TodoCatalogo());
        }
        [Route("registrar/mantenimiento")]
        public HttpResponseMessage PostRegistrarMantenimiento(MantenimientoDTO _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Mantenimiento().CrearCatalogo(_model));
        }

        [Route("eliminar/mantenimiento/{id}")]
        public HttpResponseMessage PutEliminaMantenimiento(MantenimientoDTO _model)
        {
            return RespuestaHttp.crearRespuesta(new Mantenimiento().EliminarCatalogo(_model), Request);
        }
        #endregion

        #region MantenimientoDetalle
        [Route("buscar/mantenimientodetalle")]
        public HttpResponseMessage GetMantenimientoDetalle()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Mantenimiento().Todo());
        }
        [Route("registrar/mantenimientodetalle")]
        public HttpResponseMessage PostRegistrarMantenimientoDetalle(MantenimientoDetalleDTO _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Mantenimiento().Crear(_model));
        }
        [Route("modificar/mantenimientodetalle")]
        public HttpResponseMessage PutModificarMantenimientoDetalle(MantenimientoDetalleDTO _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Mantenimiento().Modificar(_model));
        }

        [Route("eliminar/mantenimientodetalle/{id}")]
        public HttpResponseMessage PutEliminaMantenimientoDetalle(int id)
        {
            return RespuestaHttp.crearRespuesta(new Mantenimiento().Borrar(id), Request);
        }
        #endregion
    }
}