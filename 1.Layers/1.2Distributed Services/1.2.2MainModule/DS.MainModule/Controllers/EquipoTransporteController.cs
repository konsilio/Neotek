using Application.MainModule.Flujos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/equipotransporte")]
    public class EquipoTransporteController : ApiController
    {
        private EquipoTransporte _eqTransporte;
        public EquipoTransporteController()
        {
            _eqTransporte = new EquipoTransporte();
        }
        // GET: EquipoTransporte

    }
}