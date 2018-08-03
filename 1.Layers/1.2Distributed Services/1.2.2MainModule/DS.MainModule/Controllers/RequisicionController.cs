using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.Flujos;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/requisicion")]
    public class RequisicionController : ApiController
    {
        private Requisicion _requisicion;

        public RequisicionController()
        {
            _requisicion = new Requisicion();
        }
       
        [Route("guardar/requisicion")]
        public HttpResponseMessage PostRequisicion(RequisicionEDTO req)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _requisicion.InsertRequisicionNueva(req));   
        }
        [Route("buscar/requisiciones/{idEmpresa}")]
        public HttpResponseMessage GetRequisicionesByIdEmpresa(short idEmpresa)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _requisicion.BuscarRequisicionesPorEmpresa(idEmpresa));
        }
    }
}