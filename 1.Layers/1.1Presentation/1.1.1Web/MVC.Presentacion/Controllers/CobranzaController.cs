using DevExpress.Web.Mvc;
using MVC.Presentacion.Models.Cobranza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class CobranzaController : Controller
    {
        string _tkn = string.Empty;
        // GET: Cobranza
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AbonosPartialUpdate(MVCxGridViewBatchUpdateValues<CargosModel, int> updateValues)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            var id = (int)TempData["intIdOrdenCompra"];
            updateValues.Update = updateValues.Update.Select(x => { x.IdCargo = id; return x; }).ToList();
            //TempData["RespuestaDTO"] = OrdenCompraServicio.ActualizaProductosOrdenCompra(updateValues.Update, tkn);
            return RedirectToAction("Index");
        }
        [ValidateInput(false)]
        public ActionResult AbonosPartial()
        {
            var model = new object[0];
            return PartialView("_AbonosPartial", model);
        }
    }
}