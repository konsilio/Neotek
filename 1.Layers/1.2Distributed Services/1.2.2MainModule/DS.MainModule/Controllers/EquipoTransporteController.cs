using Application.MainModule.Flujos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/cobranza")]
    public class EquipoTransporteController : ApiController
    {
        // GET: EquipoTransporte
        public ActionResult Index()
        {
            return View();
        }
    }
}