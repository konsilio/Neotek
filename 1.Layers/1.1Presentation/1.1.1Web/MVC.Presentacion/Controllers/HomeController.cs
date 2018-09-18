using MVC.Presentacion.App_Code;
using MVC.Presentacion.Controllers.Shared;
using MVC.Presentacion.Models.Seguridad;
using Security.MainModule.Criptografia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace MVC.Presentacion.Controllers
{
    public class HomeController : MainController
    {
        public ActionResult Index(LoginModel model = null)
        {
            Session["StringToken"] = null;
            return View(AutenticacionServicio.InitIndex(model));
        }
        public ActionResult IndexError(LoginModel model)
        {
            model.Empresas = AutenticacionServicio.EmpresasLogin();
            return View(model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult Inicio(LoginModel login)
        {
            if (Session["StringToken"] == null)
            {
                var respuesta = AutenticacionServicio.Autenticar(login.IdEmpresa, login.Usuario, SHA.GenerateSHA256String(login.Password));
                if (respuesta.Exito)
                {
                    Session["StringToken"] = respuesta.token;
                    return View();
                }
                else
                    return View("Index", AutenticacionServicio.InitIndex(respuesta));
            }
            else
                return View();
        }

        public ActionResult Requisicion()
        {
            return View();
        }
        public ActionResult Ordenes()
        {
            return View();
        }
    }
}