using DevExpress.Web;
using DevExpress.Web.Mvc;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Cobranza;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class CobranzaController : Controller
    {
        string _tkn = string.Empty;
        // GET: Cobranza
        public ActionResult Index(DateTime? fecha1, DateTime? fecha2, int? Cliente, string rfc = null, string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            ViewBag.IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tkn);
            ViewBag.FormasPago = CatalogoServicio.ListaFormaPago(_tkn);

            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
            List<CargosModel> _model = new List<CargosModel>();
            if (fecha1 != null || fecha2 != null || Cliente != null || rfc != null)
            {
                _model = CobranzaServicio.ObtenerCargosFilter(fecha1.Value, fecha2.Value, Cliente.Value, rfc, null, TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
                // _model[0].FechaRango1 = fecha1.Value.Date;
                //_model[0].FechaRango2 = fecha2.Value.Date;
                if (_model.Count() > 0)
                {
                    //if (fecha1.Value.Year != 1)

                    //if (fecha2.Value.Year != 1)

                    //if (Cliente != null && Cliente != 0)
                    //    _model[0].IdCliente = Cliente.Value;
                    //if (rfc != null)
                    //    _model[0].Ticket = rfc.ToString();
                }
                else
                    ViewBag.MensajeError = "No se encontraron resultados..";
            }
            else
                _model = CobranzaServicio.ObtenerCargos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);

            if (TempData["RespuestaDTO"] != null)
            {
                if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
                {
                    ViewBag.Tipo = "alert-danger";
                    ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                }
                else
                {
                    ViewBag.Tipo = "alert-success";
                    ViewBag.Msj = msj;
                }
            }
            return View(_model);
        }
        public ActionResult Buscar(CargosModel _model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            return RedirectToAction("Index", new { fecha1 = _model.FechaRango1, fecha2 = _model.FechaRango2, Cliente = _model.IdCliente, rfc = _model.Rfc });
        }
        public ActionResult BuscarCredito(CargosModel _model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            return RedirectToAction("CreditoRecuperado", new { fecha1 = _model.FechaRango1, fecha2 = _model.FechaRango2, Cliente = _model.IdCliente, empresa = _model.IdEmpresa, ticket = _model.Ticket });
        }
        public ActionResult CreditoRecuperado(DateTime? fecha1, DateTime? fecha2, int? Cliente, short? empresa, string ticket = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            ViewBag.FormasPago = CatalogoServicio.ListaFormaPago(_tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
            ReporteCreditosRecuperado _modelList = new ReporteCreditosRecuperado();
            List<CargosModel> _model = new List<CargosModel>();
            CargosModel _mod = new CargosModel();

            if ((fecha1 != null && fecha1.Value.Year != 1) || (fecha1 != null && fecha2.Value.Year != 1) || (Cliente != null && Cliente != 0) || ticket != null)
            {
                _mod.FechaRango1 = fecha1.Value;
                _mod.FechaRango2 = fecha2.Value;
                _mod.IdCliente = Cliente.Value;
                _mod.Ticket = ticket; _mod.IdEmpresa = empresa.Value;
                _modelList = CobranzaServicio.ObtenerCargosFilter(_mod, _tkn);
                if (_modelList.reporteCargos.Count() == 0)
                {
                    _modelList.reporteCargos.Add(_mod);
                    ViewBag.MensajeError = "No se encontraron resultados..";
                }
            }
            else
                _modelList = CobranzaServicio.ObtenerCargosFilter(_mod, _tkn);

            if (_modelList.reporteCargos.Count() == 0)
            {
                DateTime dt = new DateTime();
                _mod.FechaRango1 = dt; _mod.FechaRango2 = dt;
                _modelList.reporteCargos.Add(_mod);
            }
            if (TempData["RespuestaDTO"] != null)
            {
                if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
                {
                    ViewBag.Tipo = "alert-danger";
                    ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                }
                else
                {
                    ViewBag.Tipo = "alert-success";
                    ViewBag.Msj = ((RespuestaDTO)TempData["RespuestaDTO"]).Mensaje;
                }
            }
            TempData["LstCargos"] = _modelList.reporteCargos;
            TempData["LstAbonos"] = _modelList.reporteAbonos;

            return View(_modelList);
        }
        public ActionResult MasterDetailMasterPartial()
        {
            //if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            //_tkn = Session["StringToken"].ToString();
            //CargosModel _mod = new CargosModel();
            //List<AbonosModel> _model = (List<AbonosModel>)TempData["LstCargos"];//CobranzaServicio.ObtenerCargos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);//CobranzaServicio.ObtenerCargosFilter(_mod, _tkn);
            //return PartialView("MasterDetailMasterPartial", _model);
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();

            if (TempData["LstAbonos"] != null)
            {
                TempData.Keep("LstAbonos");
                return PartialView((List<AbonosModel>)TempData["LstAbonos"]);
            }
            else
                return PartialView(new List<AbonosModel>());
        }

        public ActionResult MasterDetailDetailCRPartial(string customerID)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            ViewData["CustomerID"] = customerID;
            //List<CargosModel> _model = CobranzaServicio.ObtenerCargosFilter(_mod, _tkn).Where(x => x.IdCliente == int.Parse(customerID)).ToList();
            //List<CargosModel> mod = ((List<CargosModel>)TempData["LstCargos"]).Where(x => x.IdCargo == int.Parse(customerID)).ToList();AbonosModel
            List<AbonosModel> mod = ((List<AbonosModel>)TempData["LstAbonos"]).Where(x => x.IdCargo == int.Parse(customerID)).ToList();

            return PartialView("MasterDetailDetailCRPartial", mod);
        }
        public ActionResult ListaCreditoRecuperado(int IdCargo)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            CargosModel _mod = new CargosModel();

            return PartialView("DetalleVentas", ((List<CargosModel>)TempData["Cargos"]));
        }
        public ActionResult FacturarAbono(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();

            TempData["RespuestaDTO"] = FacturacionServicio.GenerarFacturasAbono(id, _tkn);
            return RedirectToAction("CreditoRecuperado");
        }
        public ActionResult CarteraVencida(int? idCliente, DateTime? fecha, ReporteModel model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            ViewBag.IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
            ViewBag.Clientes = CatalogoServicio.ListaClientes(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            DateTime dt = new DateTime();
            CargosModel m = new CargosModel();
            if (model.reportedet == null)
             m.IdEmpresa = ViewBag.IdEmpresa; 
            if (idCliente != null || fecha != null)
            {
                m.IdCliente = idCliente ?? 0;
                if (idCliente != null && idCliente != 0)
                    ViewBag.IdCliente = idCliente; 
                m.FechaRango1 = fecha ?? dt;
            }

            ReporteModel _model = CobranzaServicio.ObtenerListaCartera(_tkn, m);
            if (_model.reportedet.Count > 0)
            {
                if (fecha != null)
                    _model.reportedet[0].FechaRango1 = fecha.Value; 
                _model.reportedet[0].IdEmpresa = ViewBag.IdEmpresa;
            }
            else
            {
                //ViewBag.MensajeError = "No se encontraron resultados..";
                CargosModel cm = new CargosModel();
                cm.FechaRango1 = m.FechaRango1;
                _model.reportedet.Add(cm);
            }

            if (ViewBag.IdCliente != null && idCliente.Value != 0 && idCliente != null)
                ViewBag.IdCliente = ViewBag.IdCliente + " " + _model.reportedet[0].Cliente ?? _model.reportedet[0].Nombre; 
            TempData["CarteraVencida"] = _model.reportedet;
            return View(_model);
        }
        public ActionResult _CarteraVencidaPartial()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();

            if (TempData["CarteraVencida"] != null)
            {
                TempData.Keep("CarteraVencida");
                return PartialView((List<CargosModel>)TempData["CarteraVencida"]);
            }
            else
                return PartialView(new List<CargosModel>());
        }
        public ActionResult BuscarCartera(CargosModel _model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            return RedirectToAction("CarteraVencida", new { idCliente = _model.IdCliente, fecha = _model.FechaRango1 });
        }
        [ValidateInput(false)]
        public ActionResult AbonosPartialUpdate(MVCxGridViewBatchUpdateValues<CargosModel, int> updateValues)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            updateValues.Update[0].Abono.IdCargo = updateValues.Update[0].IdCargo;
            List<AbonosModel> _lst = new List<AbonosModel>();
            _lst.Add(updateValues.Update[0].Abono);
            var respuesta = CobranzaServicio.AltaAbonos(_lst, _tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Index", new { msj = respuesta.Mensaje });
        }
        [ValidateInput(false)]
        public ActionResult _AbonosPartial()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            ViewBag.FormasPago = CatalogoServicio.ListaFormaPago(_tkn);
            var model = CobranzaServicio.ObtenerCargos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            return PartialView(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] CargosModel m)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            m.Abono.IdCargo = m.IdCargo;
            var Respuesta = CobranzaServicio.AltaNuevoCargo(m, _tkn);
            TempData["RespuestaDTO"] = Respuesta;
            if (Respuesta.Exito)
            {
                return RedirectToAction("Index", new { msj = Respuesta.Mensaje });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult FacturacionGlobal(FacturacionGlobalModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            if (TempData["BusquedaTickets"] != null) model.Tickets = (List<VentaPuntoVentaDTO>)TempData["BusquedaTickets"];
            if (TempData["TiketsAgregados"] != null)
            {
                ViewBag.TiketsAgregados = (List<VentaPuntoVentaDTO>)TempData["TiketsAgregados"];
                TempData["TiketsAgregados"] = ViewBag.TiketsAgregados;
            }
            if (TempData["RespuestaDTO"] != null)
            {
                var Respuesta = (RespuestaDTO)TempData["RespuestaDTO"];
                if (Respuesta.Exito)
                {
                    ViewBag.Msj = Respuesta.Mensaje;
                    TempData["TiketsAgregados"] = null;
                }
                else
                    ViewBag.MensajeError = Validar(Respuesta);
            }
            ViewBag.CFDIs = FacturacionServicio.ObtenerCFDIs(_tkn);
            return View(model);
        }
        public ActionResult BuscarTikets(FacturacionGlobalModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            TempData["BusquedaTickets"] = CobranzaServicio.ObtenerTickets(model, _tkn);
            model.Tickets = (List<VentaPuntoVentaDTO>)TempData["BusquedaTickets"];

            return RedirectToAction("FacturacionGlobal", model);
        }
        public ActionResult AgregarTikets(FacturacionGlobalModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();

            TempData["TiketsAgregados"] = model.Tickets.Where(x => x.seleccionar).ToList();
            return RedirectToAction("FacturacionGlobal", model);
        }
        public ActionResult BorrarTicket(string Folio)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();

            var TiketsAgregados = (List<VentaPuntoVentaDTO>)TempData["TiketsAgregados"];
            TempData["TiketsAgregados"] = TiketsAgregados.Where(x => !x.FolioVenta.Equals(Folio)).ToList();


            return RedirectToAction("FacturacionGlobal");
        }
        public ActionResult Facturar(FacturacionGlobalModel _mod)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            _mod.Tickets = (List<VentaPuntoVentaDTO>)TempData["TiketsAgregados"];
            TempData["TiketsAgregados"] = _mod.Tickets;

            TempData["RespuestaDTO"] = FacturacionServicio.GenerarFacturasGlobal(_mod, _tkn);
            return RedirectToAction("FacturacionGlobal", _mod);
        }
        private string Validar(RespuestaDTO Resp = null)
        {
            string Mensaje = string.Empty;
            ModelState.Clear();
            if (Resp != null)
            {
                if (Resp.ModelStatesStandar != null)
                    foreach (var error in Resp.ModelStatesStandar.ToList())
                        ModelState.AddModelError(error.Key, error.Value);
                if (Resp.MensajesError != null)
                {
                    if (Resp.MensajesError.Count > 1)
                        Mensaje = Resp.MensajesError[0] + " " + Resp.MensajesError[1];
                    else
                        Mensaje = Resp.MensajesError[0];
                }
            }
            return Mensaje;
        }
    }
}