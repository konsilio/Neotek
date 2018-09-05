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
            return View(AutenticacionServicio.InitIndex(model));
        }

        public ActionResult IndexError(LoginModel model)
        {
            model.Empresas = AutenticacionServicio.EmpresasLogin();
            

            //ViewBag.listaEmpresas = AutenticacionServicio.EmpresasLogin();
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
          
        public ActionResult Inicio(AutenticacionDTO login)
        {    
            var respuesta = AutenticacionServicio.Autenticar(login.IdEmpresa, login.Usuario, SHA.GenerateSHA256String(login.Password));
            if (respuesta.Exito)
            {
                Session["StringToken"] = respuesta.token;
                return View();
                //return new JsonResult
                //{
                //    Data = new { IsCorrect = true, Message = "", IsModelError = false, location = Url.Action("Inicio", "Home") }
                //};
            }
            else
            {
                
                return View("Index", AutenticacionServicio.InitIndex(respuesta));
                //return new JsonResult
                //{
                //    Data = new
                //    {
                //        IsCorrect = false,
                //        Message = respuesta.Mensaje,
                //        IsModelError = true,
                //        view = RenderRazorViewToString("Index", new LoginModel
                //        {
                //            Empresas = AutenticacionServicio.EmpresasLogin(),
                //            Respuesta = respuesta
                //        })
                //    },
                //};
            }            
        }
    }
}