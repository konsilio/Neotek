using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class ReportesController : Controller
    {
        string tkn = string.Empty;
        public ActionResult CuentasXPagar(CuentasPorPagarModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.Periodo.Equals(DateTime.MinValue))
            {
                ViewData["Periodo"] = model.Periodo;
                ViewData["Reporte"] = TiposReporteConst.CuentasXCobrar;
                TempData["DataSource"] = ReporteServicio.BuscarCuentasPorPagar(model, tkn);
            }
            return View(model);
        }
        public ActionResult InventarioXPuntoVenta(InventarioPorPuntoVentaModel model = null)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddSeconds(75);
            TimeSpan span = endTime.Subtract(startTime);

            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model == null)
                model = new InventarioPorPuntoVentaModel();
            if (model != null && !model.Fecha.Equals(DateTime.MinValue))
            {
                ViewData["Periodo"] = model.Fecha;
                ViewData["Reporte"] = TiposReporteConst.InventarioPorPuntoVenta;
                TempData["DataSource"] = ReporteServicio.BuscarInventarioPorPuntoVenta(model, tkn);
            }
            var pipas = PedidosServicio.ObtenerPipas(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            pipas.RemoveAt(0);
            model.Pipas = pipas.Select(x => { x.Activo = false; return x; }).ToList();
            model.Estaciones = CatalogoServicio.GetListaEstacionCarburacion(tkn).Select(x => { x.Activo = false; return x; }).ToList();
            var camionetas = PedidosServicio.ObtenerCamionetas(TokenServicio.ObtenerIdEmpresa(tkn), tkn).Select(x => { x.Activo = false; return x; }).ToList();
            camionetas.RemoveAt(0);
            model.Camionetas = camionetas;
            return View(model);
        }
        public ActionResult HistoricoPrecioVenta(HistoricoPrecioVentaModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model == null)
                model = new HistoricoPrecioVentaModel();
            ViewData["Reporte"] = TiposReporteConst.HistoricoPrecioVenta;
            if (model != null && !model.FechaFinal.Equals(DateTime.MinValue) && !model.FechaInicial.Equals(DateTime.MinValue))
                TempData["DataSource"] = ReporteServicio.BuscarHistoricoPrecioVenta(model, tkn);

            return View(model);
        }
        public ActionResult CallCenter(PeriodoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.CallCenter;
                TempData["DataSource"] = ReporteServicio.BuscarCallCenter(model, tkn);
            }
            return View(model);
        }
        public ActionResult Requisicion(RequisicionModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFinal.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.Requisicion;
                TempData["DataSource"] = ReporteServicio.BuscarRequisicion(model, tkn);
            }
            return View(model);
        }
        public ActionResult OrdenCompraRep(OrdenCompraModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFinal.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.OrdenCompra;
                TempData["DataSource"] = ReporteServicio.BuscarOrdenCompra(model, tkn);
            }
            return View(model);
        }
        public ActionResult RendimientoVehicular(RendimientoVehicularModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFinal.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.RendimientoVehicular;
                TempData["DataSource"] = ReporteServicio.BuscarRendimientoVehicular(model, tkn);
            }
            return View(model);
        }
        public ActionResult InventarioXConcepto(InventarioXConceptoModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFinal.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.InventarioXConcepto;
                TempData["DataSource"] = ReporteServicio.BuscarInventarioConcepto(model, tkn);
            }
            return View(model);
        }
        public ActionResult HistoricoVsVentas(HistoricoVentasConsulta model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (TempData["year"] != null) model.Years = (List<YearsDTO>)TempData["year"];
            if (model.Years == null) TempData["year"] = model.Years = HistoricoServicio.GetYears(tkn);
            if (model != null && model.Years.Exists(x => x.Seleccionar))
            {
                ViewData["Reporte"] = TiposReporteConst.HistoricoVsVentas;
                TempData["DataSource"] = ReporteServicio.BuscarHistoricoVSVentas(model, tkn);
                TempData["json"] = HistoricoServicio.GetJson(model, tkn);
            }
            return View(model);
        }
        public ActionResult CorteCaja(CorteCajaModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.Fecha.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.CorteCaja;
                TempData["DataSource"] = ReporteServicio.BuscarCorteCaja(model, tkn);
            }
            return View(model);
        }
        public ActionResult GastoVehicular(GastoVehiculoModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.GastoVehicular;
                TempData["DataSource"] = ReporteServicio.BuscarGastoVehicular(model, tkn);
            }
            return View(model);
        }
        public ActionResult Comisiones(PeriodoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.Comision;
                TempData["DataSource"] = ReporteServicio.CalcularComisiones(model, tkn);
            }
            return View(model);
        }
        public ActionResult CuentaConsolidada(CuentasPorPagarModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.Periodo.Equals(DateTime.MinValue))
            {
                ViewData["Periodo"] = model.Periodo;
                ViewData["Reporte"] = TiposReporteConst.CuentaConsolidada;
                TempData["DataSource"] = ReporteServicio.CuentasConsolidadas(model, tkn);
            }
            return View(model);
        }

        //Cubo de inforamcion
        public ActionResult GetGridView(string Tipo)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (Tipo != null)
            {
                ViewData["Reporte"] = Tipo;
                if (Tipo.Equals(TiposReporteConst.CuentasXCobrar))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<CuentasPorPagarDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.InventarioPorPuntoVenta))
                    return View(TiposReporteConst.CuboInvXPunVen, (List<InventarioPorPuntoVentaDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.HistoricoPrecioVenta))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<HistoricoPrecioVentaDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.CallCenter))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<CallCenterDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.Requisicion))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<RequisicionRepDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.OrdenCompra))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<OrdenCompraRepDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.RendimientoVehicular))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<RendimientoVehicularDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.InventarioXConcepto))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<InventarioXConceptoDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.HistoricoVsVentas))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<YearsDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.CorteCaja))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<CorteCajaDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.GastoVehicular))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<GastoVehiculoDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.Comision))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<ComisionDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.CuentaConsolidada))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<CuentaConsolidadaDTO>)TempData["DataSource"]);
                return View(TiposReporteConst.CuboInformacionGeneral);
            }
            return View(TiposReporteConst.CuboInformacionGeneral, new List<string>());
        }
    }
}