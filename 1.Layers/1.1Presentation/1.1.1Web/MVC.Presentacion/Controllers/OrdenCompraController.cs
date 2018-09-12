using MVC.Presentacion.App_Code;
using MVC.Presentacion.Controllers.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class OrdenCompraController : MainController
    {
        public ActionResult OrdenCompra(int id)
        {
            if (Session["StringToken"] != null)
            {
                string tkn = Session["SringToken"].ToString();
                return View();
            }
            else
                return View("Index", "Home");
        }
        public ActionResult Ordenes()
        {
            if (Session["StringToken"] != null)
            {
                string tkn = Session["StringToken"].ToString();
                ViewBag.EsAdminCentral = TokenServicio.ObtenerEsAdministracionCentral(tkn);
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
                return View(OrdenCompraServicio.InitOrdenesCompra(tkn));
            }
            else
                return View("Inicio", "Home");
        }
    }
}
