using MVC.Presentacion.App_Code;
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
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            ViewBag.listaEmpresas = AutenticacionServicio.EmpresasLogin();
            return View();
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
        [HttpPost]
        public ActionResult Inicio(AutenticacionDTO login)
        {
    
            var respuesta = AutenticacionServicio.Autenticar(login.IdEmpresa, login.Usuario, SHA.GenerateSHA256String(login.Password));
            if (respuesta.Exito)
            {
                Session["StringToken"] = respuesta.token;
                //  Response.Redirect("~/DashBoard/Vista/Dashboard.aspx");
                return View();
            }           

           return View();
            
        }
    }
}