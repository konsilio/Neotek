using Application.MainModule.DTOs.Ventas;
using Application.MainModule.Flujos;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/ventas")]
    public class VentasController : ApiController
    {
        private Ventas _ventas;
        public VentasController()
        {
            _ventas = new Ventas();
        }

        #region Caja General
        [Route("buscar/listacajageneral/{idEmpresa}")]
        public HttpResponseMessage GetListaCajaGralId(short idEmpresa)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.CajaGeneralIdEmpresa(idEmpresa));
        }
        [Route("buscar/listacajageneral")]
        public HttpResponseMessage GetListaCajaGeneral()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.CajaGeneral());
        }
        [Route("buscar/listatickets/{folio}")]
        public HttpResponseMessage GetListaTickets(string folio)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.CajaGeneral(folio));
        }
        [Route("buscar/listamovimientosgaspipa")]
        public HttpResponseMessage PutListaMovimientosGasPipa(CajaGeneralDTO Dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.MovimientosGas(Dto.IdCAlmacenGas, Dto.IdEmpresa, Dto.Year, Dto.Mes, Dto.Dia, Dto.Orden, Dto.FechaAplicacion,Dto.FolioOperacionDia));

            //string iDate = "29/11/2018";
            //DateTime oDate = Convert.ToDateTime(iDate);
            //return Request.CreateResponse(HttpStatusCode.OK, _ventas.MovimientosGas((short)5, (short)2, (short)2018, (byte)11, (byte)29, (byte)11, oDate, "118434D0DA2"));
            
        }
        [Route("buscar/listamovimientosgaspipa")]
        public HttpResponseMessage GerListaMovimientos(CajaGeneralDTO Dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.MovimientosGas(Dto.IdCAlmacenGas, Dto.IdEmpresa, Dto.Year, Dto.Mes, Dto.Dia, Dto.Orden, Dto.FechaAplicacion, Dto.FolioOperacionDia));

            //string iDate = "29/11/2018";
            //DateTime oDate = Convert.ToDateTime(iDate);
            //return Request.CreateResponse(HttpStatusCode.OK, _ventas.MovimientosGas((short)5, (short)2, (short)2018, (byte)11, (byte)29, (byte)11, oDate, "118434D0DA2"));

        }
        [Route("buscar/listamovimientosgascilindros")]
        public HttpResponseMessage PutListaMovimientosGasC(VPuntoVentaDetalleDTO Dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.MovimientosGasCilindro(Dto.IdEmpresa, Dto.Year, Dto.Mes, Dto.Dia, Dto.Orden));
        }      
        [Route("Modifica/liquidarcajageneral")]
        public HttpResponseMessage PutLiquidarReporte(VentaPuntoVentaDTO vpvDto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.GuardarReporteLiquidado(vpvDto));
        }
        [Route("generar/liquidar")]
        public HttpResponseMessage PutLiquidar(CajaGeneralDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.GenerarLiquidacion(dto.FolioOperacionDia));
        }
        [Route("Modifica/liquidarcajageneralest")]
        public HttpResponseMessage PutLiquidarReporteEstacion(VentaCorteAnticipoDTO cestDto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.GuardarReporteLiquidadoEst(cestDto));
        }
        [Route("buscar/listapuntosventaliquidacion")]
        public HttpResponseMessage GetPuntosVentaLiquidacion()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.ObtenerPuntosVentaLiquidacion());
        }
        [Route("buscar/listaliquidaciones")]
        public HttpResponseMessage GetLiquidacionesDelDia()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _ventas.ObtenerLiquidaciones());
        }
        #endregion
    }
}