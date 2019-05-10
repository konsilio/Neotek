using Application.MainModule.DTOs;
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
        [Route("cuentasxpagar")]
        public HttpResponseMessage PostRepoCuentasPorPagar(CuentasPorPagarDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepCuentasPorPagar(dto.Periodo));
        }
        [Route("inventarioxpuntoventa")]
        public HttpResponseMessage PostInventarioPorPuntoVenta(InventarioPorPuntoVentaDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepInventarioPorPuntoVenta(dto));
        }
        [Route("historicoprecios")]
        public HttpResponseMessage PostHistorioPrecios(HistoricoPrecioDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepHistorioPrecios(dto));
        }
    }
}