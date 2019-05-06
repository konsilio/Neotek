using Application.MainModule.Flujos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/reportes")]
    public class ReportesController : ApiController
    {      
        
        private Reportes _repo;
        public ReportesController()
        {
            _repo = new Reportes();
        }
        [Route("cuentasxpagar/{periodo}")]
        public HttpResponseMessage GetRepoCuentasPorPagar(DateTime periodo)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepCuentasPorPagar(periodo));
        }
    }
}