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
using Newtonsoft.Json;

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
               
        public ActionResult Nueva()
        {
            EmpresaModel em = new EmpresaModel();
            _tok = Session["StringToken"].ToString();
            //Se obtienen los paises         
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tok);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tok);
            
            return View(em);
        }

        [HttpPost]
        public ActionResult Crear(EmpresaModel Objemp)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {                
                CatalogoServicio.create(Objemp, _tok);            
            }
            //return View(Objemp);
            return RedirectToAction("Index", Objemp);
        }

        public ActionResult ActualizaParametros(int id)
        {
            Empresa em = new Empresa();
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.FiltrarEmpresa(em, id, _tkn).Empresas.ToList();          
            return View();
        }

        [HttpPost]
        public ActionResult Actualiza(EmpresaConfiguracion _Obj)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.ActualizaConfigEmpresa(_Obj, _tok);
            }
            //return View(Objemp);
            return RedirectToAction("Index", _Obj);
        }
    }
}