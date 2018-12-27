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
                if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
                {
                    ViewBag.Tipo = "alert-danger";
                    ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                    TempData["RespuestaDTO"] = ViewBag.MensajeError;
                    ViewBag.MensajeError = TempData["RespuestaDTO"];
                }
                else
                {
                    ViewBag.Tipo = "alert-success";
                }
            }

            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;

            List<PedidoModel> lstPmodel = PedidosServicio.ObtenerPedidos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            PedidoModel model = new PedidoModel()
            {
                Pedidos = lstPmodel,
            };
            if (TempData["PedidosPorCliente"] != null)
            {
                var rfc = ((List<ClientesModel>)TempData["PedidosPorCliente"]).FirstOrDefault().Rfc;
                model = new PedidoModel()
                {
                    Pedidos = lstPmodel.Where(x => x.Rfc.Equals(rfc)).ToList(),
                };

                if (model.Pedidos == null)
                    ViewBag.MensajeError = msj;
            }
            if (TempData["Msj"] != null)
            {
                ViewBag.MensajeError = TempData["Msj"];
            }
            return View(model);
        }
        public ActionResult Nuevo(PedidoModel _model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Estatus = PedidosServicio.ObtenerEstatusPedidos(_tkn).ToList();
            if (TempData["RespuestaDTO"] != null)
            {
                if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
                {
                    ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                }
                else
                {
                    ViewBag.Msj = TempData["RespuestaDTO"];
                }
            }
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
        public ActionResult Buscar(PedidoModel _mod)
        {
            string _tkn = Session["StringToken"].ToString();
            string Tel1 = _mod.Telefono1 ?? "";
            string Tel2 = _mod.Telefono2 ?? "";
            string Rfc = _mod.Rfc ?? "";
            var lstClientes = CatalogoServicio.ListaClientes(Tel1, Tel2, Rfc, _tkn).ToList();
            if (lstClientes.Count > 0)
                TempData["PedidosPorCliente"] = lstClientes;
            else TempData["Msj"] = "No se encontraron registros";
            return RedirectToAction("Index", "Pedidos");

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
            if (lstClientes.Count > 0)
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
        public ActionResult AltaClienteDireccion(PedidoModel _model, ClienteLocacionMod model, int? IdCliente)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
            int idCliente = _model != null ? _model.IdCliente : IdCliente.Value;
            _model.IdTipoPersona = 0; _model.IdRegimenFiscal = 0;
            TempData["ModelAltaCliente"] = _model;

            ViewBag.Cliente = _model.NombreRfc;
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tkn);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tkn);
            List<ClienteLocacionMod> _lst = CatalogoServicio.ObtenerLocaciones(idCliente, _tkn);

            if (model != null && model.IdCliente != 0 && model.Orden != 0)
            {
                ViewBag.EsEdicion = true; ViewBag.Locaciones = model;
            }
            if (TempData["RespuestaDTO"] != null)
            {
                if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
                {
                    ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                    ViewBag.EsEdicion = false; ViewBag.Locaciones = TempData["Locaciones"];
                }
                else
                {
                    ViewBag.Msj = ((RespuestaDTO)TempData["RespuestaDTO"]).Mensaje;
                    ViewBag.EsEdicion = false; ViewBag.Locaciones = null;
                }
            }
            return View(_lst);
        }
        public ActionResult EditarClienteLoc(int? id, short? idOrden, ClienteLocacionMod model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            if (id != 0 && id != null)
            {
                return RedirectToAction("AltaClienteDireccion", CatalogoServicio.ObtenerModel(idOrden.Value, id.Value, _tkn));
            }
            else
            {
                var respuesta = CatalogoServicio.ModificarClienteLocacion(model, _tkn);
                if (respuesta.Exito)
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("AltaClienteDireccion", CatalogoServicio.ObtenerModel(model.Orden, model.IdCliente, _tkn));
                }
                else
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("AltaCliente");
                }
            }
            //ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tkn);
            ////Se obtienen los estados 
            //ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tkn);
            //ViewBag.Locaciones = CatalogoServicio.ObtenerModel(idOrden, id, _tkn);

            //if (TempData["RespuestaDTO"] != null)
            //{
            //    ViewBag.MessageExito = TempData["RespuestaDTO"];
            //}
            //if (TempData["RespuestaDTOError"] != null)
            //{
            //    ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
            //    TempData["RespuestaDTOError"] = ViewBag.MessageError;
            //}

            //ViewBag.MessageError = TempData["RespuestaDTOError"];
            //return View();
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
                return RedirectToAction("Nuevo");
            }

            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("AltaCliente");
            }

        }
        public ActionResult GuardarLocaciones(ClienteLocacionMod _Obj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tkn = Session["StringToken"].ToString();
            if (TempData["ModelAltaCliente"] != null && _Obj.IdCliente == 0)
            {
                var respuesta = CatalogoServicio.CrearCliente((ClientesModel)TempData["ModelAltaCliente"], _tkn);
                if (respuesta.Exito)
                {
                    _Obj.IdCliente = respuesta.Id;
                    var respuestaLocacion = CatalogoServicio.RegistraLocaciones(_Obj, _tkn);
                    if (respuestaLocacion.Exito)
                    {
                        TempData["RespuestaDTO"] = respuestaLocacion;
                        return RedirectToAction("AltaClienteDireccion");
                    }
                    else
                    {
                        TempData["RespuestaDTO"] = respuesta;
                        return RedirectToAction("AltaCliente");
                    }
                }
                else
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("AltaClienteDireccion", "Pedidos");
                }
            }
            else
            {
                var respuestaLocacion = CatalogoServicio.RegistraLocaciones(_Obj, _tkn);
                TempData["RespuestaDTO"] = respuestaLocacion;
                TempData["Locaciones"] = _Obj;
                return RedirectToAction("AltaClienteDireccion", new { IdCliente = _Obj.IdCliente });

            }

        }
        public ActionResult RevisarPedido(int? idPedido, string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tkn = Session["StringToken"].ToString();
            if (!string.IsNullOrEmpty(msj)) ViewBag.Msj = msj;

            var model = PedidosServicio.ObtenerIdPedido(idPedido.Value, _tkn);
            if (TempData["RespuestaDTO"] != null)
            {
                if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
                {
                    ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                }

            }
            return View(model);
        }
        public ActionResult EditarPedido(int idPedido)//
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tkn = Session["StringToken"].ToString();
            ViewBag.Estatus = PedidosServicio.ObtenerEstatusPedidos(_tkn).ToList();
            var model = PedidosServicio.ObtenerIdPedido(idPedido, _tkn);
            model.Cantidad = model.Cantidad.Replace("Kg", "");
            ViewBag.Camionetas = PedidosServicio.ObtenerCamionetas(model.IdEmpresa, _tkn);
            ViewBag.Pipas = PedidosServicio.ObtenerPipas(model.IdEmpresa, _tkn);
            return View(model);
        }
        public ActionResult EditarCliente(int idPedido)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tkn = Session["StringToken"].ToString();
            ViewBag.Estatus = PedidosServicio.ObtenerEstatusPedidos(_tkn).ToList();
            var model = PedidosServicio.ObtenerIdPedido(idPedido, _tkn);
            model.Cantidad = model.Cantidad.Replace("Kg", "");
            ViewBag.Camionetas = PedidosServicio.ObtenerCamionetas(model.IdEmpresa, _tkn);
            ViewBag.Pipas = PedidosServicio.ObtenerPipas(model.IdEmpresa, _tkn);
            return View(model);
        }
        public ActionResult GuardarEdicionPedido(PedidoModel _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            _model.IdTipoPersona = 0;
            _model.IdRegimenFiscal = 0;

            var Respuesta = PedidosServicio.ActualizarPedido(_model, Session["StringToken"].ToString());
            if (Respuesta.Exito)
            {
                return RedirectToAction("RevisarPedido", new { idPedido = _model.IdPedido, msj = Respuesta.Mensaje });
            }
            else
            {
                TempData["RespuestaDTO"] = Respuesta;
                return RedirectToAction("RevisarPedido", new { idPedido = _model.IdPedido });
            }
        }
        public JsonResult GuardarEncuesta(List<EncuestaModel> calificacion)
        {
            string _tkn = Session["StringToken"].ToString();
            var lstEncuesta = PedidosServicio.AltaEncuestaPedido(calificacion, _tkn);

            var JsonInfo = JsonConvert.SerializeObject(lstEncuesta);
            return Json(JsonInfo, JsonRequestBehavior.AllowGet);
        }

        public class Estrellas
        {
            public int IdPedido { get; set; }
            public int IdPregunta { get; set; }
            public string Respuesta { get; set; }
        }
        public ActionResult CancelarPedido(int idPedido, string MotivoCancela = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var _model = PedidosServicio.ObtenerIdPedido(idPedido, _tkn);
            _model.IdTipoPersona = 0;
            _model.IdRegimenFiscal = 0;
            _model.Pedidos = null;
            _model.MotivoCancelacion = MotivoCancela;

            var Respuesta = PedidosServicio.EliminarPedido(_model, Session["StringToken"].ToString());
            ViewData["RespuestaDTO"] = Respuesta;
            return RedirectToAction("Index");
            //if (Respuesta.Exito)
            //{
            //    return RedirectToAction("Index", new { msj = Respuesta.Mensaje });
            //}
            //else
            //{
            //    TempData["RespuestaDTO"] = Respuesta;
            //    return RedirectToAction("Index");
            //}

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
                {
                    if (Resp.MensajesError.Count > 1)
                        Mensaje = Resp.MensajesError[0] + " " + Resp.MensajesError[1];
                }

            }
            return Mensaje;
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

        public List<EncuestaModel> AModel(List<Estrellas> _lst)
        {
            var List = new List<EncuestaModel>();
            var item = new EncuestaModel();
            var resp = "";
            foreach (var x in _lst)
            {
                resp = x.Respuesta;
                string res = resp.Substring(resp.Length - 1, 1);
                item.IdPedido = x.IdPedido;
                item.IdPregunta = x.IdPregunta;
                item.Respuesta = res;//Convert.ToByte(res);
                List.Add(item);
            }

            return List;
        }
        #endregion
    }
}
