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
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tok);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tok);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tok).SingleOrDefault().NombreComercial;
            ViewBag.listaEmpresas = CatalogoServicio.Empresas(_tok);

            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MessageExito = TempData["RespuestaDTO"];
            }
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
                //ViewBag.MessageError = TempData["RespuestaDTOError"];
            }
            //ViewBag.MessageError = TempData["RespuestaDTOError"];

            return View();
        }
        //view
        public ActionResult Nueva(string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            //if (!TokenServicio.ObtenerEsAdministracionCentral(_tok))
            //{
            //    TempData["RespuestaDTOError"] = CatalogoServicio.SinPermisos();
            //    return RedirectToAction("Index");
            //}
            //Se obtienen los paises         
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tok);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tok);
            if (TempData["model"] != null)
            {
                if (TempData["RespuestaDTO"] != null) ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                ViewBag.EsEdicion = null;
                return View((EmpresaModel)TempData["model"]);
            }
            if (TempData["modelEditar"] != null)
            {              
                if (TempData["RespuestaDTO"] != null) ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                ViewBag.EsEdicion = true;
                ViewBag.Empresa = TempData["modelEditar"];
                return View((EmpresaModel)TempData["modelEditar"]);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Crear(EmpresaModel Objemp, HttpPostedFileBase UrlLogotipo180px, HttpPostedFileBase UrlLogotipo500px, HttpPostedFileBase UrlLogotipo1000px)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            Objemp.IdPais = Objemp.IdPaisSec;
            var respuesta = CatalogoServicio.create(Objemp, UrlLogotipo180px, UrlLogotipo500px, UrlLogotipo1000px, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                return RedirectToAction("Index");
            }

            else
            {
                //TempData["RespuestaDTO"] = respuesta;
                var ms =  Validar(respuesta);
                TempData["model"] = Objemp;            
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Nueva",  new { Objemp, msj = ms});
            }
        }

        //view
        public ActionResult ActualizaParametros(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            EmpresaModel em = new EmpresaModel();
            ViewBag.Empresas = CatalogoServicio.FiltrarEmpresa(em, id, _tkn);

            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
                TempData["RespuestaDTOError"] = ViewBag.MessageError;
            }
            ViewBag.MessageError = TempData["RespuestaDTOError"];
            return View();
        }
        //view
        public ActionResult EditarEmpresa(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            EmpresaModel em = new EmpresaModel();
            TempData["modelEditar"] = CatalogoServicio.FiltrarEmpresa(em, id, _tkn);
           
            return RedirectToAction("Nueva");
        }

        public ActionResult BorrarEmpresa(short id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            if (!TokenServicio.ObtenerEsAdministracionCentral(_tok))
            {
                TempData["RespuestaDTOError"] = CatalogoServicio.SinPermisos();
                return RedirectToAction("Index");
            }
            Empresa em = new Empresa();
            var respuesta = CatalogoServicio.EliminaEmpresaSel(id, _tok);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Actualiza(EmpresaConfiguracion _Obj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.ActualizaConfigEmpresa(_Obj, _tok);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("ActualizaParametros", "Empresas", new { _Obj.IdEmpresa });
            }
        }

        [HttpPost]
        public ActionResult GuardaEdicionEmpresa(EmpresaModel _Obj, HttpPostedFileBase UrlLogotipo180px, HttpPostedFileBase UrlLogotipo500px, HttpPostedFileBase UrlLogotipo1000px)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            _Obj.IdEstadoRep = _Obj.IdEstadoRepSec;
            _Obj.IdPais = _Obj.IdPaisSec;
            var respuesta = CatalogoServicio.ActualizaEdicionEmpresa(_Obj, UrlLogotipo180px, UrlLogotipo500px, UrlLogotipo1000px, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index", _Obj);
            }

            else
            {
                TempData["RespuestaDTO"] = respuesta;
                TempData["modelEditar"] = _Obj;
                return RedirectToAction("Nueva");
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
                if (Resp.MensajesError != null && Resp.MensajesError.Count != 0)
                    Mensaje = Resp.MensajesError[0];

                if (Mensaje == "")
                    Mensaje = Resp.Mensaje;
            }
            return Mensaje;
        }

    }
}