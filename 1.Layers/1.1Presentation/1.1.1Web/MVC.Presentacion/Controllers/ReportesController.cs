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
                ViewBag.CuentasPorPagarDTO = ReporteServicio.BuscarCuentasPorPagar(model, tkn);
            return View(model);
        }
        public ActionResult GetGridView(CuentasPorPagarModel model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            //CuentasPorPagarModel model = new CuentasPorPagarModel()
            //{ Periodo = periodo };
            return View(ReporteServicio.BuscarCuentasPorPagar(model, tkn));
        }
    }
}