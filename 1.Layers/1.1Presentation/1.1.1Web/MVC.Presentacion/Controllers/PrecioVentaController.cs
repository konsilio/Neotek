using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
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
        [HttpPost]

        public ActionResult Registrar(ClientesModel _ojUs)
        {
           string _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
               // CatalogoServicio.CrearCliente(_ojUs, _tok);
            }

            return RedirectToAction("Index", _ojUs);
        }
    }
}