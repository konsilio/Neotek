using DevExpress.Web.Mvc;
using MVC.Presentacion.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class RemanenteController : Controller
    {
        string tkn = string.Empty;
        // GET: Remanente
        public ActionResult DashBoard()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            //ViewBag.UnidadesVenta = CatalogoServicio.ListaPuntosVentaId(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            return View();
        }
        public ActionResult Buscar()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();            

            return View();
        }

        public ActionResult cbPuntosventaPartial()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            
            return PartialView("_cbPuntosventaPartial", CatalogoServicio.ListaPuntosVentaId(TokenServicio.ObtenerIdEmpresa(tkn), tkn));
        }
    }
}
