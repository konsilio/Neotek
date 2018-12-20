using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Pedidos;
using MVC.Presentacion.Models.Seguridad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class PedidosController : Controller
    {
        string _tkn = string.Empty;
        // GET: Pedidos
        public ActionResult Index(string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            if (!string.IsNullOrEmpty(msj)) ViewBag.Msj = msj;

            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                TempData["RespuestaDTO"] = ViewBag.MensajeError;
            }
            ViewBag.MensajeError = TempData["RespuestaDTO"];

            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;

            List<PedidoModel> lstPmodel = PedidosServicio.ObtenerPedidos(_tkn);
            PedidoModel model = new PedidoModel()
            {
                Pedidos = lstPmodel,
            };

            return View(model);
        }
        public ActionResult Nuevo()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Estatus = PedidosServicio.ObtenerEstatusPedidos(_tkn).ToList();
            //ViewBag.Pipas    //llenar unidades
            //    ViewBag.Camionetas
            return View();
        }

        public ActionResult CrearPedido(PedidoModel _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            var Respuesta = PedidosServicio.AltaNuevoPedido(_model, Session["StringToken"].ToString());
            if (Respuesta.Exito)
            {
                return RedirectToAction("Index", new { msj = Respuesta.Mensaje });
            }
            else
            {
                TempData["RespuestaDTO"] = Respuesta;
                return RedirectToAction("Index");
            }

        }
        public JsonResult BuscarClientesPedido(string Tel1, string Tel2, string Rfc)
        {
            string _tkn = Session["StringToken"].ToString();
            var lstClientes = CatalogoServicio.ListaClientes(Tel1, Tel2, Rfc, _tkn).ToList();

            var JsonInfo = JsonConvert.SerializeObject(lstClientes);
            return Json(JsonInfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AltaCliente()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;

            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona(_tkn);
            ViewBag.Regimen = CatalogoServicio.ObtenerRegimenFiscal(_tkn);

            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                TempData["RespuestaDTO"] = ViewBag.MensajeError;
            }
            ViewBag.MensajeError = TempData["RespuestaDTO"];

            return View();
        }

        [HttpPost]
        public ActionResult GuardarCliente(PedidoModel _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tkn = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.CrearCliente(_model, _tkn);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Nuevo");
            }

        }

        public ActionResult RevisarPedido(int idPedido, string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tkn = Session["StringToken"].ToString();
            if (!string.IsNullOrEmpty(msj)) ViewBag.Msj = msj;

            var model = PedidosServicio.ObtenerIdPedido(idPedido, _tkn);
            return View(model);
        }

        public ActionResult EditarPedido(PedidoModel _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tkn = Session["StringToken"].ToString();
            ViewBag.Estatus = PedidosServicio.ObtenerEstatusPedidos(_tkn).ToList();
            //ViewBag.Pipas    //llenar unidades
            //    ViewBag.Camionetas
            return View(_model);
        }

        public ActionResult GuardarEdicionPedido(PedidoModel _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            var Respuesta = PedidosServicio.ActualizarPedido(_model, Session["StringToken"].ToString());
            if (Respuesta.Exito)
            {
                return RedirectToAction("RevisarPedido", new { id = _model.IdPedido, msj = Respuesta.Mensaje });
            }
            else
            {
                TempData["RespuestaDTO"] = Respuesta;
                return RedirectToAction("RevisarPedido", new { id = _model.IdPedido });
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

//public ActionResult BuscarClientesPedido(string tel1, string tel2, string rfc)
//{
//    if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
//    string _tkn = Session["StringToken"].ToString();
//    var lstClientes = CatalogoServicio.ListaClientes(tel1, tel2, rfc, _tkn);

//    if (TempData["RespuestaDTO"] != null)
//    {
//        ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
//        TempData["RespuestaDTO"] = ViewBag.MensajeError;
//    }
//    ViewBag.MensajeError = TempData["RespuestaDTO"];

//    return Redirect("Nuevo");//();
//}