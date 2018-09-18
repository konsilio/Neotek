using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using Security.MainModule.Criptografia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;

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
            ViewBag.Empresas = null;
            return View(em);
        }     

        [HttpPost]
        public ActionResult Crear(EmpresaModel Objemp, HttpPostedFileBase UrlLogotipo180px, HttpPostedFileBase UrlLogotipo500px, HttpPostedFileBase UrlLogotipo1000px)
        {
           
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.create(Objemp, UrlLogotipo180px, UrlLogotipo500px, UrlLogotipo1000px, _tok);
            }
                    
            return RedirectToAction("Index", Objemp);
        }

        public ActionResult ActualizaParametros(int id)
        {
            Empresa em = new Empresa();
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.FiltrarEmpresa(em, id, _tkn).Empresas.ToList();
            return View();
        }

        public ActionResult EditarEmpresa(int id)
        {
            Empresa em = new Empresa();
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.FiltrarEmpresa(em, id, _tkn).Empresas.ToList();
            //Se obtienen los paises         
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tkn);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tkn);
            return View("Nueva");
        }

        public ActionResult BorrarEmpresa(short id)
        {
            Empresa em = new Empresa();
            string _tkn = Session["StringToken"].ToString();
            //  ViewBag.Empresas = CatalogoServicio.FiltrarEmpresa(em, id, _tkn).Empresas.ToList();
            CatalogoServicio.EliminaEmpresaSel(id, _tkn);
            //return View();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Actualiza(EmpresaConfiguracion _Obj)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.ActualizaConfigEmpresa(_Obj, _tok);
            }

            return RedirectToAction("Index", _Obj);
        }

        [HttpPost]
        public ActionResult GuardaEdicionEmpresa(EmpresaDTO _Obj, HttpPostedFileBase UrlLogotipo180px, HttpPostedFileBase UrlLogotipo500px, HttpPostedFileBase UrlLogotipo1000px)

        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.ActualizaEdicionEmpresa(_Obj, UrlLogotipo180px, UrlLogotipo500px, UrlLogotipo1000px, _tok);
                // CatalogoServicio.create(_Obj, UrlLogotipo180px, UrlLogotipo500px, UrlLogotipo1000px, _tok);
            }

            return RedirectToAction("Index", _Obj);
        }
        
    }
}