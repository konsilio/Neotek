using DevExpress.Web;
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
                _model = CobranzaServicio.ObtenerCargosFilter(fecha1.Value, fecha2.Value, Cliente.Value, rfc, null, TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
                _model[0].FechaRango1 = fecha1.Value.Date;
                _model[0].FechaRango2 = fecha2.Value.Date;
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
                _model = CobranzaServicio.ObtenerCargosFilter(fecha1.Value, fecha2.Value, Cliente.Value, null, ticket, TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
                _model[0].FechaRango1 = fecha1.Value.Date;
                _model[0].FechaRango2 = fecha2.Value.Date;
            }
            else
                _model = CobranzaServicio.ObtenerCargos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);

            return View(_model);
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
            {m.IdEmpresa = ViewBag.IdEmpresa;}
            if (idCliente != null || fecha != null ) {
                m.IdCliente = idCliente ?? 0;
                if (idCliente != null  && idCliente!= 0) { ViewBag.IdCliente = idCliente; }
                 m.FechaRango1 = fecha??dt; } 
            
            ReporteModel _model = CobranzaServicio.ObtenerListaCartera(_tkn, m);
            if(_model.reportedet.Count>0) { _model.reportedet[0].FechaRango1 = fecha.Value; _model.reportedet[0].IdEmpresa = ViewBag.IdEmpresa; }
            if (ViewBag.IdCliente != null && idCliente.Value != 0 && idCliente != null) { ViewBag.IdCliente = ViewBag.IdCliente + " " + _model.reportedet[0].Nombre; }

            return View(_model);
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