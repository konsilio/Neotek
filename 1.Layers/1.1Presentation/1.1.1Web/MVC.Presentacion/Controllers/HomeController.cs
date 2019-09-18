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
                    if (respuesta.LstRoles.Ventas)
                        ViewBag.VentasRema = DashBoardServicio.DashBoardRemanente(Session["StringToken"].ToString());
                    if (respuesta.LstRoles.CallCenter)
                        ViewBag.CallCenter = DashBoardServicio.DashBoardCallCenter(Session["StringToken"].ToString());
                    if (respuesta.LstRoles.AlmacenES)
                        ViewBag.Anden = DashBoardServicio.DashBoardAnden(Session["StringToken"].ToString());
                    if (respuesta.LstRoles.CreditoCobranza)
                        ViewBag.Cartera = DashBoardServicio.DashBoardCartera(Session["StringToken"].ToString());
                    if (respuesta.LstRoles.ReporteCorteCaja)
                        ViewBag.Caja = DashBoardServicio.DashBoardCajaGeneral(Session["StringToken"].ToString());
                }
                else
                    return View("Index", AutenticacionServicio.InitIndex(respuesta));
            }
            else
            {
                var respuesta = (MenuDto)Session["Roles"];
                if (respuesta.Ventas)
                    ViewBag.VentasRema = DashBoardServicio.DashBoardRemanente(Session["StringToken"].ToString());
                if (respuesta.CallCenter)
                    ViewBag.CallCenter = DashBoardServicio.DashBoardCallCenter(Session["StringToken"].ToString());
                if (respuesta.AlmacenES)
                    ViewBag.Anden = DashBoardServicio.DashBoardAnden(Session["StringToken"].ToString());
                if (respuesta.CreditoCobranza)
                    ViewBag.Cartera = DashBoardServicio.DashBoardCartera(Session["StringToken"].ToString());
                if (respuesta.ReporteCorteCaja)
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
    }
}