using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
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
        string _tok = string.Empty;
        // GET: Empresas
        public ActionResult Index()
        {
            ViewBag.listaEmpresas = AutenticacionServicio.EmpresasLogin();

            return View();
        }

        // [HttpPost]
        public ActionResult Nueva()
        {
            //Se obtienen los paises
            _tok = Session["StringToken"].ToString();
            //CatalogoServicio pais = new CatalogoServicio();
            //pais.GetPaises(_tok);                       
         
            ////Session["ListaRoles"] = getSelectList<Roles>(ListObject, "Clave", "IdRol");
            //TempData["ListaPaises"] = pais.GetPaises(_tok);

            EmpresaModel Objemp = new EmpresaModel();
            return View(new EmpresaModel());
        }

        public ActionResult Crear(EmpresaModel Objemp)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                //CatalogoServicio lst = new CatalogoServicio();
                //lst.create(Objemp, _tok);
            }
            //return View(Objemp);
            return RedirectToAction("Index");
        }
    }
}