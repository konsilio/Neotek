using DevExpress.Web.Demos.Mvc;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
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
        public ActionResult Nuevo(PedidoModel _model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Estatus = PedidosServicio.ObtenerEstatusPedidos(_tkn).ToList();
            //ViewBag.Pipas    //llenar unidades
            //    ViewBag.Camionetas
            return View(_model);
        }
        [HttpPost]
        public ActionResult CrearPedido(PedidoModel _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var Id = TokenServicio.ObtenerIdEmpresa(_tkn);
            _model.IdTipoPersona = 0;
            _model.IdRegimenFiscal = 0;
            _model.IdEmpresa = Id;
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
        [HttpPost]
        public ActionResult BuscarClientesPedido(PedidoModel _mod)
        {
            string _tkn = Session["StringToken"].ToString();
            string Tel1 = _mod.Telefono1 ?? "";
            string Tel2 = _mod.Telefono2 ?? "";
            string Rfc = _mod.Rfc ?? "";
            var lstClientes = CatalogoServicio.ListaClientes(Tel1, Tel2, Rfc, _tkn).ToList();
            _mod.clientes = lstClientes;
            _mod.Locaciones = CatalogoServicio.ObtenerLocaciones(lstClientes.FirstOrDefault().IdCliente, _tkn);
            //return RedirectToAction("_LocacionesCliente", "Pedidos", new { _model = lstClientes });
            //   return Nuevo(_mod);
            return RedirectToAction("Nuevo", _mod);

        }
        public JsonResult BuscarClientesPedidoDireccion(string Tel1, string Tel2, string Rfc)
        {
            string _tkn = Session["StringToken"].ToString();
            var lstClientes = CatalogoServicio.ListaClientes(Tel1, Tel2, Rfc, _tkn).ToList();
            List<ClienteLocacionMod> _lst = CatalogoServicio.ObtenerLocaciones(lstClientes.FirstOrDefault().IdCliente, _tkn);
            var JsonInfo = JsonConvert.SerializeObject(_lst);
            return Json(JsonInfo, JsonRequestBehavior.AllowGet);
        }

        #region Combos
        public ActionResult _LocacionesCliente(PedidoModel _model)
        {
            _tkn = Session["StringToken"].ToString();
            List<ClienteLocacionMod> _lst = new List<ClienteLocacionMod>();
            string Tel1 = _model.Telefono1 ?? "";
            string Tel2 = _model.Telefono2 ?? "";
            string Rfc = _model.Rfc ?? "";
            //_lst = CatalogoServicio.ObtenerLocaciones(_model.IdCliente, _tkn);
            var lstClientes = CatalogoServicio.ListaClientes(Tel1, Tel2, Rfc, _tkn).ToList();
            _lst = CatalogoServicio.ObtenerLocaciones(lstClientes.Count() > 0 ? lstClientes.FirstOrDefault().IdCliente : 0, _tkn);

            return PartialView(_lst);
        }
        public ActionResult _DatosCliente(ClientesModel _model)
        {
            _tkn = Session["StringToken"].ToString();
            string Tel1 = _model.Telefono1 ?? "";
            string Tel2 = _model.Telefono2 ?? "";
            string Rfc = _model.Rfc ?? "";
            var lstClientes = CatalogoServicio.ListaClientes(Tel1, Tel2, Rfc, _tkn).ToList();

            return PartialView(lstClientes);
        }
        public ActionResult _TipoUnidad(ClientesModel _model)
        {
            var lstClientes = AgregarTUnidades();

            return PartialView(lstClientes);
        }
        public ActionResult _Camionetas(ClientesModel _model)
        {
            _tkn = Session["StringToken"].ToString();
            var Id = TokenServicio.ObtenerIdEmpresa(_tkn);
            //List<CamionetaModel>
            var lst = PedidosServicio.ObtenerCamionetas(Id, _tkn);
            return PartialView(lst);
        }
        public ActionResult _Pipas(ClientesModel _model)
        {
            _tkn = Session["StringToken"].ToString();
            var Id = TokenServicio.ObtenerIdEmpresa(_tkn);
            //List<CamionetaModel>
            var lst = PedidosServicio.ObtenerPipas(Id, _tkn);
            return PartialView(lst);
        }

        public List<Data> AgregarTUnidades()
        {

            var list = new List<Data>();
            list.Add(new Data(0, "Seleccione"));
            list.Add(new Data(1, "Pipa"));
            list.Add(new Data(2, "Camioneta"));         
            
            return list;

        }
        public struct Data
        {
            public Data(int intValue, string strValue)
            {
                IntTipoUndad = intValue;
                StrUnidad = strValue;
            }

            public int IntTipoUndad { get; private set; }
            public string StrUnidad { get; private set; }
        }     
        #endregion
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