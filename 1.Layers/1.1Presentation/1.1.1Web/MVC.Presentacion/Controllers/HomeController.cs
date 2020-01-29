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
            Session["Perfil"] = null;

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
                Session["Perfil"] = null;
                var respuesta = AutenticacionServicio.Autenticar(login.IdEmpresa, login.Usuario, SHA.GenerateSHA256String(login.Password));
                if (respuesta.Exito)
                {
                    Session["StringToken"] = respuesta.token;
                    Session["Perfil"] = respuesta.Mensaje;
                    Session["Roles"] = respuesta.LstRoles;
                    ViewBag.VentasRema = DashBoardServicio.DashBoardRemanente(Session["StringToken"].ToString());
                    ViewBag.CallCenter = DashBoardServicio.DashBoardCallCenter(Session["StringToken"].ToString());
                    ViewBag.Anden = DashBoardServicio.DashBoardAnden(Session["StringToken"].ToString());
                    ViewBag.Cartera = DashBoardServicio.DashBoardCartera(Session["StringToken"].ToString());
                    ViewBag.Caja = DashBoardServicio.DashBoardCajaGeneral(Session["StringToken"].ToString());
                }
                else
                    return View("Index", AutenticacionServicio.InitIndex(respuesta));
            }
            else
            {
                //if (EsAdmin)
                ViewBag.VentasRema = DashBoardServicio.DashBoardRemanente(Session["StringToken"].ToString());
                //if (EsCallCenter)
                ViewBag.CallCenter = DashBoardServicio.DashBoardCallCenter(Session["StringToken"].ToString());
                //if (EsAnden)
                ViewBag.Anden = DashBoardServicio.DashBoardAnden(Session["StringToken"].ToString());
                //if (EsAnden)
                ViewBag.Cartera = DashBoardServicio.DashBoardCartera(Session["StringToken"].ToString());
                //if (EsCajaGeneral)
                ViewBag.Caja = DashBoardServicio.DashBoardCajaGeneral(Session["StringToken"].ToString());
            }
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
        public ActionResult Mantenimiento()
        {
            return View();
        }
    }
}