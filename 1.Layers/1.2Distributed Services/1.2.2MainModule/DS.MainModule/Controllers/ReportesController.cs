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
        public HttpResponseMessage PostCallCenter(PeriodoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepCallCenter(dto));
        }
        [Route("ventas")]
        public HttpResponseMessage PostVentas(PeriodoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepVentas(dto));
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
        [Route("rendimientovehicularCamioneta")]
        public HttpResponseMessage PostRendimientoVehicularCamionetas(PeriodoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepRendimientoVehicularCamionetas(dto));
        }
        [Route("Autoconsumos")]
        public HttpResponseMessage PostAutoConsumos(PeriodoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepAutoConsumos(dto));
        }
        [Route("DescuentosXClientes")]
        public HttpResponseMessage PostDescuentosXClientes(PeriodoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepDescuentosXClientes(dto));
        }
        [Route("CreditoRecuperadoClientes")]
        public HttpResponseMessage PostCreditoRecuperadoClientes(PeriodoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepCreditoRecuperadoClientes(dto));
        }
        [Route("CreditoOtorgadoClientes")]
        public HttpResponseMessage PostCreditoOtorgadoClientes(PeriodoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepCreditoOtorgadoClientes(dto));
        }
      
        [Route("CreditoXCliente")]
        public HttpResponseMessage PostCreditoXCliente(PeriodoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepCreditoXCliente(dto));
        }
        [Route("CreditoXClienteMensual")]
        public HttpResponseMessage PostCreditoXClienteMensual(PeriodoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepCreditoXClienteMensual(dto));
        }
        
        [Route("VentasXPuntoVenta")]
        public HttpResponseMessage PostVentasXPuntoVenta(VentasXPuntoVenta dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.VentasXPuntoVenta(dto));
        }
        [Route("EquipoDeTransporte")]
        public HttpResponseMessage PostEquipoDeTransporte(PeriodoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.EquipoDeTransporte(dto));
        }
        [Route("rendimientovehicularPipas")]
        public HttpResponseMessage PostRendimientoVehicularPipas(PeriodoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepRendimientoVehicularPipas(dto));
        }
        [Route("inventarioxconcepto")]
        public HttpResponseMessage PostInventarioPorConcepto(InventarioXConceptoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepInventarioPorConcepto(dto));
        }
        [Route("cortecaja")]
        public HttpResponseMessage PostCorteCaja(CajaGralDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepCorteCaja(dto));
        }
        [Route("gastoxvehiculo")]
        public HttpResponseMessage PostGastoXVehiculo(GastoVehicularDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepGastoXVehiculo(dto));
        }
        [Route("comisiones")]
        public HttpResponseMessage PostComisiones(PeriodoDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepComisiones(dto));
        }
        [Route("cuentasconsolidadas")]
        public HttpResponseMessage PostCuentasConsolodadas(CuentasPorPagarDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.RepCuentasConsolidadas(dto.Periodo));
        }
        [Route("dashboard/remanente")]
        public HttpResponseMessage GetDashRemanente()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.DashAdministracionVentaVSRema());
        }
        [Route("dashboard/callcenter")]
        public HttpResponseMessage GetDashCallCenter()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.DashCallCenter());
        }
        [Route("dashboard/anden")]
        public HttpResponseMessage GetDashAnden()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.DashAnden());
        }
        [Route("dashboard/cartera")]
        public HttpResponseMessage GetCartera()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.DashCartera());
        }
        [Route("dashboard/cajageneral")]
        public HttpResponseMessage GetCajaGeneral()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repo.DashCaja());
        }
    }
}