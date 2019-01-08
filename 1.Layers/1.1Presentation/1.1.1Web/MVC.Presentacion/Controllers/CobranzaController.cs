using DevExpress.Web;
using DevExpress.Web.Demos.Mvc;
using DevExpress.Web.Mvc;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Cobranza;
using MVC.Presentacion.Models.Seguridad;
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
                _model = CobranzaServicio.ObtenerCargosFilter(fecha1.Value, fecha2.Value, Cliente.Value, rfc, null,TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            }
            else
                _model = CobranzaServicio.ObtenerCargos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);

            if (TempData["RespuestaDTO"] != null)
            {
                if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
                {
                    ViewBag.Tipo = "alert-danger";
                    ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                    //TempData["RespuestaDTO"] = ViewBag.MensajeError;
                    //ViewBag.MensajeError = TempData["RespuestaDTO"];
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
            return RedirectToAction("CreditoRecuperado", new { fecha1 = _model.FechaRango1, fecha2 = _model.FechaRango2, Cliente = _model.IdCliente, ticket = _model.Ticket });
        }
        public ActionResult CreditoRecuperado(DateTime? fecha1, DateTime? fecha2, int? Cliente, string ticket = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            ViewBag.FormasPago = CatalogoServicio.ListaFormaPago(_tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
            List<CargosModel> _model = new List<CargosModel>();
            if (fecha1 != null || fecha2 != null || Cliente != null || ticket != null)
            {
                _model = CobranzaServicio.ObtenerCargosFilter(fecha1.Value, fecha2.Value, Cliente.Value, null,ticket, TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            }
            else
                _model = CobranzaServicio.ObtenerCargos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);

            return View(_model);
        }
        public ActionResult CarteraVencida(int? idCliente, DateTime? fecha, CargosModel model)
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
            if(model.IdEmpresa == 0) { model.IdEmpresa = ViewBag.IdEmpresa; }
            if(idCliente!=null) { model.IdCliente = idCliente.Value; ViewBag.IdCliente = idCliente; }
            DateTime dt = new DateTime();
            if(model.FechaRango1.Year == 1) { model.FechaRango1 = dt; }
            ReporteModel _model = CobranzaServicio.ObtenerListaCartera(_tkn, model );
            if(ViewBag.IdCliente!= null) { ViewBag.IdCliente = ViewBag.IdCliente + " " + _model.reportedet[0].Nombre; }
            return View(_model);
        }
        public ActionResult BuscarCartera(CargosModel _model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            return RedirectToAction("CarteraVencida", new { idCliente = _model.IdCliente, fecha = _model.FechaRango1});
        }
        public ActionResult CrearPedido(CargosModel _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var Id = TokenServicio.ObtenerIdEmpresa(_tkn);
            _model.IdEmpresa = Id;
            var Respuesta = CobranzaServicio.AltaNuevoCargo(_model, Session["StringToken"].ToString());
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
        [HttpPost, ValidateInput(false)]
        public ActionResult AbonosPartialUpdateItem([ModelBinder(typeof(DevExpressEditorsBinder))] CargosModel _model, string contactId)//[ModelBinder(typeof(DevExpressEditorsBinder))] updateValues)//
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tkn = Session["StringToken"].ToString();
            var Id = TokenServicio.ObtenerIdEmpresa(_tkn);
            // var _model = new List<CargosModel>();
            _model.IdEmpresa = Id;
            var Respuesta = CobranzaServicio.AltaNuevoCargo(_model, Session["StringToken"].ToString());
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
        public ActionResult AbonosPartialUpdateItem()
        {
            return RedirectToAction("_AbonosPartial");
        }
        [ValidateInput(false)]
        public ActionResult AbonosPartialUpdate(MVCxGridViewBatchUpdateValues<CargosModel, int> updateValues)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            updateValues.Update[0].Abonos.IdCargo = updateValues.Update[0].IdCargo;
            List<AbonosModel> _lst = new List<AbonosModel>();
            _lst.Add(updateValues.Update[0].Abonos);
            var respuesta = CobranzaServicio.AltaAbonos(_lst, _tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Index", new { msj = respuesta.Mensaje });
        }
        [ValidateInput(false)]
        public ActionResult EditModesUpdatePartial([ModelBinder (typeof (DevExpressEditorsBinder))]  AbonosModel product)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            var respuesta = CobranzaServicio.AltaAbono(product, _tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("_AbonosPartial", new { msj = respuesta.Mensaje });
        }
        [ValidateInput(false)]
        public ActionResult AbonosPartial()
        {
            _tkn = Session["StringToken"].ToString();
            ViewBag.FormasPago = CatalogoServicio.ListaFormaPago(_tkn);
            var model = CobranzaServicio.ObtenerCargos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            return PartialView("_AbonosPartial", model);
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
                        Mensaje = Resp.MensajesError[0];
                }
            }
            return Mensaje;
        }

    }
}