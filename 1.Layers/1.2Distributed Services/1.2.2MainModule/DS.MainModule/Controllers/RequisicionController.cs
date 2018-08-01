using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Application.MainModule.Flujos;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/requisicion")]
    public class RequisicionController:ApiController
    {
        private Requisicion _Requisicion;
       
        [Route("savereq")]
        public HttpResponseMessage PutRequisicion(Requisicion req)
        {

        }
    }
}