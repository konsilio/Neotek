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
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            RespuestaDTO Resp = new RespuestaDTO();
            ViewBag.listaEmpresas = CatalogoServicio.Empresas(_tok);

            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MessageExito = TempData["RespuestaDTO"];
            }
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = TempData["RespuestaDTOError"];
            }

            ViewBag.MessageError = TempData["RespuestaDTOError"];
            return View();
        }
        //views
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
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            //if (ModelState.IsValid)
            //{
            var respuesta = CatalogoServicio.create(Objemp, UrlLogotipo180px, UrlLogotipo500px, UrlLogotipo1000px, _tok);
            //}
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = "Alta Exitosa";//respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index", Objemp);
            }
                
            else
            {
                TempData["RespuestaDTOError"] = respuesta.Mensaje;
                return RedirectToAction("Index", Objemp);   
            }
        }

        //views
        public ActionResult ActualizaParametros(int id)
        {

            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            Empresa em = new Empresa();
            ViewBag.Empresas = CatalogoServicio.FiltrarEmpresa(em, id, _tkn).Empresas.ToList();
            return View();
        }
        //views
        public ActionResult EditarEmpresa(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            Empresa em = new Empresa();
            ViewBag.Empresas = CatalogoServicio.FiltrarEmpresa(em, id, _tkn).Empresas.ToList();
            //Se obtienen los paises         
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tkn);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tkn);
            return View("Nueva");
        }

        public ActionResult BorrarEmpresa(short id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            Empresa em = new Empresa();
            var respuesta = CatalogoServicio.EliminaEmpresaSel(id, _tkn);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = "Baja Exitosa";//respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["RespuestaDTOError"] = respuesta.Mensaje;
                return RedirectToAction("Index");
            }
        }
        
        [HttpPost]
        public ActionResult Actualiza(EmpresaConfiguracion _Obj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            //if (ModelState.IsValid)
            //{
            var respuesta = CatalogoServicio.ActualizaConfigEmpresa(_Obj, _tok);
            //}
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = "Cambio Exitoso";//respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index", _Obj);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta.Mensaje;
                return RedirectToAction("Index", _Obj);
            }            
        }

        [HttpPost]
        public ActionResult GuardaEdicionEmpresa(EmpresaDTO _Obj, HttpPostedFileBase UrlLogotipo180px, HttpPostedFileBase UrlLogotipo500px, HttpPostedFileBase UrlLogotipo1000px)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));

            _tok = Session["StringToken"].ToString();
            //if (ModelState.IsValid)
            //{
            var respuesta = CatalogoServicio.ActualizaEdicionEmpresa(_Obj, UrlLogotipo180px, UrlLogotipo500px, UrlLogotipo1000px, _tok);
            //}
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = "Cambio Exitoso";//respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index", _Obj);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta.Mensaje;
                return RedirectToAction("Index", _Obj);
            }
        }
        private string Validar(RespuestaDTO Resp = null)
        {
            string Mensaje = string.Empty;
            ModelState.Clear();
            if (Resp != null)
            {
                if (Resp.ModelStatesStandar != null)
                    foreach (var error in Resp.ModelStatesStandar.ToList())
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                if (Resp.MensajesError != null)
                    Mensaje = Resp.MensajesError[0];
            }
            return Mensaje;
        }

    }
}