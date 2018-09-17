using MVC.Presentacion.App_Code;
using MVC.Presentacion.Controllers.Shared;
using MVC.Presentacion.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class CuentaContableController : Controller
    {
        public ActionResult CuentaContable()
        {
            if (Session["StringToken"] != null)
            {
                string token = Session["StringToken"].ToString();
                ViewBag.Empresas = CatalogoServicio.Empresas(token);
                return View(CatalogoServicio.InitCtaContable(token));
            }
            else
                return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));

        }
        public ActionResult Crear(CuentaContableModel model)
        {

            return View();
        }
        public ActionResult Modificar(CuentaContableModel model)
        {
            if (Session["StringToken"] != null)
            {
                string token = Session["StringToken"].ToString();
                ViewBag.Empresas = CatalogoServicio.Empresas(token);
                return View(CatalogoServicio.InitCtaContable(token));
            }
            else
                return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
        }
        public ActionResult Eliminar(CuentaContableModel model)
        {

            return View();
        }
    }
}
