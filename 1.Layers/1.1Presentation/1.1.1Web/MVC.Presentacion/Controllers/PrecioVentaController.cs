using MVC.Presentacion.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class PrecioVentaController : Controller
    {
        // GET: PrecioVenta
        public ActionResult Index()
        {
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            return View();
        }
    }
}