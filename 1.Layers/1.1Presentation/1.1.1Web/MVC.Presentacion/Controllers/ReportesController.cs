﻿using MVC.Presentacion.App_Code;
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
            if (model != null && !model.Periodo.Equals(DateTime.MinValue))
            {
                ViewData["Periodo"] = model.Periodo;
                ViewData["Reporte"] = TiposReporteConst.CuentasXCobrar;
                ViewData["DataSource"] = ReporteServicio.BuscarCuentasPorPagar(model, tkn);
            }
            return View(model);
        }
        public ActionResult InventarioXPuntoVenta(InventarioPorPuntoVentaModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (model == null)            
                model = new InventarioPorPuntoVentaModel();           
            model.Pipas = PedidosServicio.ObtenerPipas(TokenServicio.ObtenerIdEmpresa(tkn), tkn).Select(x => { x.Activo = false; return x;}).ToList();
            model.Estaciones = CatalogoServicio.GetListaEstacionCarburacion(tkn).Select(x => { x.Activo = false; return x; }).ToList();
            if (model != null && !model.Fecha.Equals(DateTime.MinValue))
            {
                ViewData["Periodo"] = model.Fecha;
                ViewData["Reporte"] = TiposReporteConst.InventarioPorPuntoVenta;
                ViewData["DataSource"] = ReporteServicio.BuscarInventarioPorPuntoVenta(model, tkn);
            }
            return View(model);
        }
        public ActionResult HistoricoPrevioVenta(HistoricoPrecioVentaModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (model == null)
                model = new HistoricoPrecioVentaModel();           
            if (model != null && !model.FechaFinal.Equals(DateTime.MinValue) && !model.FechaInicial.Equals(DateTime.MinValue))
            {
                //ViewData["Periodo"] = model.Fecha;
                ViewData["Reporte"] = TiposReporteConst.HistoricoPrecioVenta;
                ViewData["DataSource"] = ReporteServicio.BuscarHistoricoPrecioVenta(model, tkn);
            }
            return View(model);
        }
        public ActionResult GetGridView(string Tipo)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ViewData["Reporte"] = Tipo;
            if (Tipo.Equals(TiposReporteConst.CuentasXCobrar))
                return View("_CuboDeInformacionPartial", (List<CuentasPorPagarDTO>)ViewData["DataSource"]);
            if (Tipo.Equals(TiposReporteConst.InventarioPorPuntoVenta))
                return View("_CuboDeInformacionPartial", (List<InventarioPorPuntoVentaDTO>)ViewData["DataSource"]);
            if (Tipo.Equals(TiposReporteConst.HistoricoPrecioVenta))
                return View("_CuboDeInformacionPartial", (List<HistoricoPrecioVentaDTO>)ViewData["DataSource"]);

            return View("_CuboDeInformacionPartial");
        }
    }
}