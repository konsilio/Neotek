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
    public class EmpresasController : Controller
    {
        // GET: Empresas
        public ActionResult Index()
        {
            ViewBag.listaEmpresas = AutenticacionServicio.EmpresasLogin();

            return View();         
        }
    }
}