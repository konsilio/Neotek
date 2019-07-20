using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Pedidos;
using MVC.Presentacion.Models.Seguridad;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class PedidosController : Controller
    {
        string _tkn = string.Empty;
        // GET: Pedidos
        public ActionResult Index(int? idpedido, string msj = null, string tel1 = null, string rfc = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            if (!string.IsNullOrEmpty(msj))
                ViewBag.Msj = msj; ViewBag.Tipo = "alert-success";

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
                    ViewBag.Tipo = "alert-success";
            }
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;

           
            PedidoModel model = new PedidoModel();
            //model.Pedidos = PedidosServicio.ObtenerPedidos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);

            //if (idpedido > 0 || (tel1 != null && tel1 != "") || (rfc != null && rfc != ""))
            //{
            //    model.Pedidos = PedidosServicio.ObtenerPedidosFiltro(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn, idpedido, rfc, tel1);
            //    if (model.Pedidos.Count == 0)
            //    {
            //        ViewBag.Msj = "No se encontraron resultados"; ViewBag.Tipo = "alert-danger";
            //    }
            //}
            if (TempData["Msj"] != null)
                ViewBag.MensajeError = TempData["Msj"];

            return View(model);
        }
        public ActionResult Nuevo(int? id, RegistrarPedidoModel _model = null, string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Estatus = PedidosServicio.ObtenerEstatusPedidos(_tkn).ToList();

            ViewBag.Unidades = AgregarTUnidades();
            if (TempData["RespuestaDTO"] != null)
            {
                if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
                    ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                else
                    ViewBag.Msj = TempData["RespuestaDTO"];
            }
            if (msj != "" || msj != null)
                ViewBag.MensajeError = msj;
            if (id != null && id != 0)
            {
                var cliente = CatalogoServicio.ObtenerCliente(id.Value, _tkn);
                _model.Rfc = cliente.Rfc;
            }
            if (id != null)
            {
                _model.IdCliente = id ?? 0;
                List<ClientesDto> lc = new List<ClientesDto>();
                lc.Add(CatalogoServicio.ObtenerClienteDto(id ?? 0, _tkn));
                ViewBag.ListaCliente = lc;
                ViewBag.ListaDomicilios = CatalogoServicio.ObtenerLocaciones(id ?? 0, _tkn);
            }
            return View(_model);
        }
        [HttpPost]
        public ActionResult CrearPedido(RegistrarPedidoModel _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var Id = TokenServicio.ObtenerIdEmpresa(_tkn);
            _model.IdEmpresa = (short)Id;
            var Respuesta = PedidosServicio.AltaNuevoPedido(_model, Session["StringToken"].ToString());
            if (Respuesta.Exito)
            {
                return RedirectToAction("Index", new { msj = Respuesta.Mensaje });
            }
            else
            {
                TempData["RespuestaDTO"] = Respuesta;
                return RedirectToAction("Nuevo");
            }
        }
        public ActionResult Buscar(PedidoModel _mod)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            if (_mod.cliente != null)
                return RedirectToAction("Index", new { idpedido = _mod.IdPedido, tel1 = _mod.cliente.Telefono1, rfc = _mod.cliente.Rfc });
            else
                return RedirectToAction("Index");
        }
        public JsonResult BuscarClientesPedido(string Tel1, string Rfc)
        {
            string _tkn = Session["StringToken"].ToString();
            var lstClientes = CatalogoServicio.Clientes(Tel1, Rfc, _tkn);

            var JsonInfo = JsonConvert.SerializeObject(lstClientes);
            return Json(JsonInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BuscarClientesPedidoDireccion(string Tel1, string Rfc)
        {
            string _tkn = Session["StringToken"].ToString();
            var lstClientes = CatalogoServicio.Clientes(Tel1, Rfc, _tkn);

            List<ClienteLocacionMod> _lst = new List<ClienteLocacionMod>();
            if (lstClientes.Count() > 0)
                _lst = CatalogoServicio.ObtenerLocaciones(lstClientes.FirstOrDefault().IdCliente, _tkn);
            var JsonInfo = JsonConvert.SerializeObject(_lst);
            return Json(JsonInfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Terminar(PedidoModel model)
        {

            return RedirectToAction("Nuevo","Pedidos" ,new { id = model.cliente.IdCliente });
        }
        public ActionResult AltaCliente(PedidoModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tkn);
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
            if (TempData["ErrorModel"] != null)
                model = (PedidoModel)TempData["ErrorModel"];
            else
                model.cliente = PedidosServicio.ClienteGenerico(_tkn);
            return View(model);
        }
        public ActionResult AltaClienteDireccion(PedidoModel _model, int? IdCliente, int? id, short? idOrden, string msj = null, string msjValid = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;

            PedidoModel modelP = new PedidoModel();
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tkn);
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tkn);
            ClienteLocacionMod clm = new ClienteLocacionMod();
            List<ClienteLocacionMod> _lst = new List<ClienteLocacionMod>();

            if (IdCliente > 0)
            {
                if (_model.IdPedido > 0)
                    ViewBag.Cliente = _model.NombreRfc;
                else
                    ViewBag.Cliente = TempData["Cliente"];
                _model = new PedidoModel();
                clm.IdPais = 0; clm.IdEstadoRep = 0;
                ClientesModel cm = new ClientesModel();
                _model.cliente = cm;
                _model.cliente.IdCliente = IdCliente.Value;
                _model.cliente.Locacion = clm;
                modelP.cliente = _model.cliente;
                modelP.cliente.Locaciones = CatalogoServicio.ObtenerLocaciones(IdCliente.Value, _tkn);

            }
            if (_model.cliente != null && IdCliente == null)
            {
                if (_model.cliente.IdEmpresa == 0) { _model.cliente.IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tkn); }
                var respuesta = CatalogoServicio.CrearCliente(_model.cliente, _tkn);
                if (respuesta.Exito)
                {
                    _model.cliente.IdCliente = respuesta.Id;
                    ViewBag.Cliente = _model.cliente.Nombre + " " + _model.cliente.Apellido1 + " " + _model.cliente.Apellido2 + " " + _model.cliente.Rfc;
                    TempData["Cliente"] = ViewBag.Cliente;
                }
                else
                {
                    TempData["RespuestaDTO"] = respuesta;
                    TempData["ErrorModel"] = _model;
                    return RedirectToAction("AltaCliente");
                }
                clm.IdPais = 0; clm.IdEstadoRep = 0;
                _model.cliente.Locacion = clm;
                _model.cliente.Locaciones = _lst;
                modelP.cliente = _model.cliente;
            }
            if (id > 0)
            {
                if (msj == "modificacion")
                {
                    clm.IdPais = 0; clm.IdEstadoRep = 0;
                }
                else
                { clm = CatalogoServicio.ObtenerModel(idOrden.Value, id.Value, _tkn); ViewBag.EsEdicion = true; }

                _model = new PedidoModel();
                ViewBag.Cliente = TempData["Cliente"];
                _lst = CatalogoServicio.ObtenerLocaciones(id.Value, _tkn);
                ClientesModel cm = new ClientesModel();
                _model.cliente = cm;
                _model.cliente.Locacion = clm;
                _model.cliente.Locaciones = _lst;
                _model.cliente.IdCliente = id.Value;
                modelP.cliente = _model.cliente;

            }


            if (TempData["RespuestaDTO"] != null)
            {
                if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
                {
                    ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                }
                else
                {
                    ViewBag.Msj = ((RespuestaDTO)TempData["RespuestaDTO"]).Mensaje;
                }
            }

            if (msjValid != null)
                ViewBag.MensajeError = msjValid;
            return View(modelP);
        }
        public ActionResult EditarClienteLoc(int? id, short? idOrden, PedidoModel model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            if (id != 0 && id != null)
            {
                return RedirectToAction("AltaClienteDireccion", new { id = id, idOrden = idOrden });
            }
            else
            {
                var respuesta = CatalogoServicio.ModificarClienteLocacion(model.cliente.Locacion, _tkn);
                if (respuesta.Exito)
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("AltaClienteDireccion", new { id = model.cliente.Locacion.IdCliente, idOrden = model.cliente.Locacion.Orden, msj = "modificacion" });//CatalogoServicio.ObtenerModel(model.Orden, model.IdCliente, _tkn));
                }
                else
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("AltaClienteDireccion", new { id = id, idOrden = idOrden });//AltaCliente
                }
            }
        }
        [HttpPost]
        public ActionResult GuardarCliente(ClientesModel _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
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
        public ActionResult GuardarLocaciones(PedidoModel _Obj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            _tkn = Session["StringToken"].ToString();

            if (_Obj.cliente.Locacion.IdPais != (byte)1 && _Obj.cliente.Locacion.IdEstadoRep == (byte)0)
            {
                return RedirectToAction("AltaClienteDireccion", new { IdCliente = _Obj.cliente.IdCliente, msjValid = "Error. debe seleccionar un Estado" });
            }
            else
            {
                _Obj.cliente.Locacion.IdCliente = _Obj.cliente.IdCliente;
                if (_Obj.cliente.Locacion.IdPais == 0) { _Obj.cliente.Locacion.IdPais = 1; }
                _Obj.cliente.Locacion.Orden = (short)CatalogoServicio.ObtenerLocaciones(_Obj.cliente.IdCliente, _tkn).Count();
                var respuestaLocacion = CatalogoServicio.RegistraLocaciones(_Obj.cliente.Locacion, _tkn);
                TempData["RespuestaDTO"] = respuestaLocacion;
                return RedirectToAction("AltaClienteDireccion", new { IdCliente = _Obj.cliente.IdCliente });

            }

        }
        public ActionResult BorrarClienteLoc(int id, short idOrden)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ClienteLocacionMod _ObjModel = CatalogoServicio.ObtenerModel(idOrden, id, _tkn);
            var respuesta = CatalogoServicio.EliminarClienteLocacion(_ObjModel, _tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("AltaClienteDireccion");
        }
        public ActionResult RevisarPedido(int? idPedido, string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tkn = Session["StringToken"].ToString();
            if (!string.IsNullOrEmpty(msj)) ViewBag.Msj = msj;

            var model = PedidosServicio.ObtenerIdPedido(idPedido ?? 0, _tkn);
            if (TempData["RespuestaDTO"] != null)
            {
                if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
                {
                    ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                }
            }
            msj = null;
            return View(model);
        }
        public ActionResult EditarPedido(int idPedido)
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
        public ActionResult GuardarEdicionPedido(RegistrarPedidoModel _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            //_model.cliente.IdTipoPersona = 0;
            //_model.cliente.IdRegimenFiscal = 0;

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
        public ActionResult CancelarPedido(int idPedido, string MotivoCancela = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var _model = PedidosServicio.ObtenerIdPedido(idPedido, _tkn);
            //_model.cliente.IdTipoPersona = 0;
            //_model.cliente.IdRegimenFiscal = 0;
            //_model.Pedidos = null;
            _model.MotivoCancelacion = MotivoCancela;

            var Respuesta = PedidosServicio.EliminarPedido(_model, Session["StringToken"].ToString());
            ViewData["RespuestaDTO"] = Respuesta;
            return RedirectToAction("Index");
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
                    else
                        if (Resp.MensajesError.Count > 0)
                        Mensaje = Resp.MensajesError[0];
                }
            }
            return Mensaje;
        }
        public ActionResult Pedidos()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            var model = PedidosServicio.ObtenerPedidos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn).OrderByDescending(x => x.FechaRegistroPedido);
            return PartialView("_Pedidos", model);
        }
        #region Combos
        public ActionResult ComboBoxPartialPais()
        {
            _tkn = Session["StringToken"].ToString();
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tkn);
            List<ClienteLocacionMod> model = new List<ClienteLocacionMod>();
            return PartialView("_ComboBoxPartialPais", model);
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
            var lst = PedidosServicio.ObtenerCamionetas(Id, _tkn);
            return PartialView(lst);
        }
        public ActionResult _Pipas(ClientesModel _model)
        {
            _tkn = Session["StringToken"].ToString();
            var Id = TokenServicio.ObtenerIdEmpresa(_tkn);
            var lst = PedidosServicio.ObtenerPipas(Id, _tkn);
            return PartialView(lst);
        }
        public List<Data> AgregarTUnidades()
        {
            var list = new List<Data>();
            //list.Add(new Data(0, "Seleccione"));
            list.Add(new Data(1, "Pipa"));
            list.Add(new Data(2, "Camioneta"));

            return list;
        }
        public struct Data
        {
            public Data(int intValue, string strValue)
            {
                IntTipoUnidad = intValue;
                TipoUnidad = strValue;
            }

            public int IntTipoUnidad { get; private set; }
            public string TipoUnidad { get; private set; }
        }
        #endregion
    }
}
