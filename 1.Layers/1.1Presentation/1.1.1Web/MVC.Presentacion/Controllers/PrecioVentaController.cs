using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class PrecioVentaController : Controller
    {
        // GET: PrecioVenta
        public ActionResult Index()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
         
            ViewBag.EsSuperUser = TokenServicio.ObtenerEsSuperUsuario(_tkn);
            if (ViewBag.EsSuperUser)
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.ListaPV = CatalogoServicio.ListaPrecioVenta(0, _tkn);
            }
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa(_tkn))).NombreComercial;
                ViewBag.ListaPV = CatalogoServicio.ListaPrecioVentaIdEmpresa(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            }

            ViewBag.ListaStatus = CatalogoServicio.ListaTipoFecha(_tkn);
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

        [HttpPost]
        public ActionResult Registrar(PrecioVentaModel _ObjModel)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tok = Session["StringToken"].ToString();
           
             var respuesta = CatalogoServicio.RegistrarPrecio(_ObjModel, _tok);
            
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = "Cambio Exitoso";//respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index", _ObjModel);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta.Mensaje;
                return RedirectToAction("Index", _ObjModel);
            }
            
        }

        public ActionResult EditarPrecioVenta(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();        
                     
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.ListaPV = CatalogoServicio.ListaPrecioVenta(id, _tkn);
                                 
            return View();
        }

        public ActionResult BorrarPrecioVenta(PrecioVentaModel _Obj, int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            _Obj = CatalogoServicio.ListaPrecioVenta(id, _tkn)[0];
            var respuesta = CatalogoServicio.EliminarPrecioVenta(_Obj, _tkn);
         
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = "Baja Exitosa";//respuesta.Mensaje;
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
        public ActionResult ActualizarPrecioVenta(PrecioVentaModel _Obj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
           string _tok = Session["StringToken"].ToString();
          
            var respuesta = CatalogoServicio.ModificarPrecioVenta(_Obj, _tok);
         
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

        public JsonResult GetConfiguracionEmpresa(short idEmpresa)
        {
            string _tkn = Session["StringToken"].ToString();
            var list = CatalogoServicio.Empresas(_tkn).SingleOrDefault(x => x.IdEmpresa.Equals(idEmpresa)).FactorLitrosAKilos;

            var JsonInfo = JsonConvert.SerializeObject(list);
            return Json(JsonInfo, JsonRequestBehavior.AllowGet);
        }
               
    }
}