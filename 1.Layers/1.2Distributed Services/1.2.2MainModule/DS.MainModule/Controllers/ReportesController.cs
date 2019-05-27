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
        [Route("callcenter")]
        public HttpResponseMessage PostCallCenter(CallCenterDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepCallCenter(dto));
        }
        [Route("requisicion")]
        public HttpResponseMessage PostRequisicion(RequisicionModelDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepRequisicion(dto));
        }
        [Route("ordencompra")]
        public HttpResponseMessage PostOrdenCompra(OrdenCompraModelDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepOrdenCompra(dto));
        }
        [Route("rendimientovehicular")]
        public HttpResponseMessage PostRendimientoVehicular(RendimientoVehicularDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepRendimientoVehicular(dto));
        }
        [Route("inventarioxconcepto")]
        public HttpResponseMessage PostInventarioPorConcepto(InventarioXConceptoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepInventarioPorConcepto(dto));
        }
        [Route("cortecaja")]
        public HttpResponseMessage PostCorteCaja(CajaGeneralDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepCorteCaja(dto));
        }
        [Route("gastoxvehiculo")]
        public HttpResponseMessage PostGastoXVehiculo(GastoVehicularDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepGastoXVehiculo(dto));
        }
        [Route("dashboard/remanente")]
        public HttpResponseMessage GetDashRemanente()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.DashAdministracionVentaVSRema());
        }
    }
}