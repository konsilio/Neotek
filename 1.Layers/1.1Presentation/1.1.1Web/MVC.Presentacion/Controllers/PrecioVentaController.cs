using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
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

            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            ViewBag.IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tkn);
            if (ViewBag.EsAdmin)
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.ListaPV = CatalogoServicio.ListaPrecioVenta(0, _tkn);
            }
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
                ViewBag.ListaPV = CatalogoServicio.ListaPrecioVentaIdEmpresa(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            }

            ViewBag.ListaStatus = CatalogoServicio.ListaTipoFecha(_tkn);

            //if (TempData["RespuestaDTO"] != null)
            //{
            //    ViewBag.MessageExito = TempData["RespuestaDTO"];
            //}
            //if (TempData["RespuestaDTOError"] != null)
            //{
            //    ViewBag.MessageError = TempData["RespuestaDTOError"];
            //}
            //ViewBag.MessageError = TempData["RespuestaDTOError"];
            if (TempData["RespuestaDTOError"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
            var model = new PrecioVentaModel { IdEmpresa = (short)ViewBag.IdEmpresa };
            return View(model);
        }

        [HttpPost]
        public ActionResult Registrar(PrecioVentaModel _ObjModel)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tok = Session["StringToken"].ToString();
           
             var respuesta = CatalogoServicio.RegistrarPrecio(_ObjModel, _tok);
            
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = "Alta Exitosa";//respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult EditarPrecioVenta(short id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();        
                     
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.ListaPV = CatalogoServicio.ListaPrecioVenta(id, _tkn);
                                 
            return View();
        }

        public ActionResult BorrarPrecioVenta(PrecioVentaModel _Obj, short id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            _Obj = CatalogoServicio.ListaPrecioVenta(id, _tkn)[0];
            var respuesta = CatalogoServicio.EliminarPrecioVenta(_Obj, _tkn);
         
            if (respuesta.Exito)
            {
                //TempData["RespuestaDTO"] = "Baja Exitosa";//respuesta.Mensaje;
                //TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Index");
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
                //TempData["RespuestaDTO"] = "Cambio Exitoso";//respuesta.Mensaje;
                //TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Index");
            }
        }

        public JsonResult GetConfiguracionEmpresa(short idEmpresa)
        {
            string _tkn = Session["StringToken"].ToString();
            var list = CatalogoServicio.Empresas(_tkn).SingleOrDefault(x => x.IdEmpresa.Equals(idEmpresa)).FactorLitrosAKilos;
            
            var JsonInfo = JsonConvert.SerializeObject(list);
            return Json(JsonInfo, JsonRequestBehavior.AllowGet);
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