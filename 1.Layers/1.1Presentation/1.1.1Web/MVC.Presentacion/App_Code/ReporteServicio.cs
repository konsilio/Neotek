using MVC.Presentacion.Agente;
using MVC.Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class ReporteServicio
    {
        public static List<CuentasPorPagarDTO> BuscarCuentasPorPagar(CuentasPorPagarModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscaCuentasPorPagar(model, token);
            return respuestaReq._ListaCuentasPorPagar;
        }
        public static List<InventarioPorPuntoVentaDTO> BuscarInventarioPorPuntoVenta(InventarioPorPuntoVentaModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscaInventarioPorPuntoVenta(model, token);
            return respuestaReq._ListaInventarioPuntoVenta;
        }
        public static List<HistoricoPrecioVentaDTO> BuscarHistoricoPrecioVenta(HistoricoPrecioVentaModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarHistoricoPrecioVenta(model, token);
            return respuestaReq._ListaHistoricoPrecioVenta;
        }
        public static List<CallCenterDTO> BuscarCallCenter(CallCenterModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarCallCenter(model, token);
            return respuestaReq._ListaCallCenter;
        }
        public static List<RequisicionRepDTO> BuscarRequisicion(RequisicionModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarRequisicion(model, token);
            return respuestaReq._ListaRequisicion;
        }
        public static List<OrdenCompraRepDTO> BuscarOrdenCompra(OrdenCompraModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarOrdenCompra(model, token);
            return respuestaReq._ListaOrdenCompra;
        }
        public static List<RendimientoVehicularDTO> BuscarRendimientoVehicular(RendimientoVehicularModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarRendimientoVehicular(model, token);
            return respuestaReq._ListaRendimientoVehicular;
        }
        public static List<InventarioXConceptoDTO> BuscarInventarioConcepto(InventarioXConceptoModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarInventarioPorConcepto(model, token);
            return respuestaReq._ListaInventarioConcepto;
        }
        public static List<HistoricoVentaModel> BuscarHistoricoVSVentas(HistoricoVentasConsulta model, string tkn)
        {
            AgenteServicio agenteServico = new AgenteServicio();
            agenteServico.GetListaHistoricos(tkn);
            return agenteServico._ListHistoricoVenta;
        }
    }
}