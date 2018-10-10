using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class CajaGeneralController : Controller
    {
        // GET: CajaGeneral
        public ActionResult Index()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            ViewBag.EsSuperUser = TokenServicio.ObtenerEsSuperUsuario(_tkn);

            if (ViewBag.EsSuperUser)
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.CajaGeneral = VentasServicio.ListaVentasCajaGral(_tkn);
            }
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa(_tkn))).NombreComercial;
                ViewBag.CajaGeneral = VentasServicio.ListaVentasCajaGralId(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            }

            ViewBag.ListaEntidad = CatalogoServicio.ListaTipoFecha(_tkn);
            ViewBag.ListaConcepto = CatalogoServicio.ListaTipoFecha(_tkn);
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

            return View();
        }

        [HttpPost]
        public ActionResult Liquidar(CajaGeneralModel _ObjModel)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tok = Session["StringToken"].ToString();           
            return View();
        }
        public ActionResult Buscar(CajaGeneralCamionetaModel _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            ViewBag.CajaGeneralCamioneta = VentasServicio.ListaVentasCajaGralCamioneta(_model.FolioOperacionDia,_tkn);
            return View("Liquidar");
        }
  
        public ActionResult GuardarLiquidar(CajaGeneralModel _ObjModel)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tok = Session["StringToken"].ToString();

            var respuesta = VentasServicio.CrearGuardarLiquidacion(_ObjModel, _tok);
           
            if (respuesta.Exito)
            {
                //TempData["RespuestaDTO"] = "Alta Exitosa";//respuesta.Mensaje;
                //TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index", _ObjModel);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Index", _ObjModel);
            }
        }

        public ActionResult Estacion()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            ViewBag.EsSuperUser = TokenServicio.ObtenerEsSuperUsuario(_tkn);

            if (ViewBag.EsSuperUser)
            {
                ViewBag.CajaGralEstacion = null;//CatalogoServicio.ListaPrecioVenta(0, _tkn);
            }
            else
            {
                ViewBag.CajaGralEstacion = null;//CatalogoServicio.ListaPrecioVentaIdEmpresa(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            }

            //ViewBag.ListaEntidad = CatalogoServicio.ListaTipoFecha(_tkn);
            //ViewBag.ListaConcepto = CatalogoServicio.ListaTipoFecha(_tkn);
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

        public ActionResult Pipa()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            ViewBag.EsSuperUser = TokenServicio.ObtenerEsSuperUsuario(_tkn);

            if (ViewBag.EsSuperUser)
            {
                //ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                //ViewBag.ListaPV = CatalogoServicio.ListaPrecioVenta(0, _tkn);
            }
            else
            {
                //ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa(_tkn))).NombreComercial;
                //ViewBag.ListaPV = CatalogoServicio.ListaPrecioVentaIdEmpresa(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            }

            //ViewBag.ListaEntidad = CatalogoServicio.ListaTipoFecha(_tkn);
            //ViewBag.ListaConcepto = CatalogoServicio.ListaTipoFecha(_tkn);
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