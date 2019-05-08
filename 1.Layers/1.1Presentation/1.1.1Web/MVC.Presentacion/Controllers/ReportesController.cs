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
            if (model != null && !model.Periodo.Equals(DateTime.MinValue))
            {
                ViewData["Periodo"] = model.Periodo;
                ViewData["Reporte"] = TiposReporteConst.CuentasXCobrar;
                ViewData["DataSource"] = ReporteServicio.BuscarCuentasPorPagar(model, tkn);
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
            return View("_CuboDeInformacionPartial", (List<CuentasPorPagarDTO>)ViewData["DataSource"]);
        }
    }
}