using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Models.Ventas;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Newtonsoft.Json;
using System.Collections.Generic;
using DevExpress.Web.Mvc;
//using Application.MainModule.DTOs.Ventas;

namespace MVC.Presentacion.Controllers
{
    public class CajaGeneralController : Controller
    {
        string _tkn = string.Empty;
        public ActionResult Index()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            _tkn = Session["StringToken"].ToString();
            ViewBag.Liquidaciones = VentasServicio.BuscarLiquidacionesDelDia(_tkn);
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
            if (TempData["RespuestaDTO"] != null)
                ViewBag.MessageExito = ((RespuestaDTO)TempData["RespuestaDTO"]).Mensaje;
            return View();
        }
        public ActionResult Liquidar(CorteCajaDTO _model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            _tkn = Session["StringToken"].ToString();
            if (TempData["RespuestaDTO"] != null)
            {
                var resp = (RespuestaDTO)TempData["RespuestaDTO"];
                if (resp.Exito)
                    ViewBag.MessageExito = resp.Mensaje;
                else
                    ViewBag.MensajeError = Validar(resp);
            }
            if (_model == null)
                _model = new CorteCajaDTO();
            if (_model.Tickets == null || _model.Tickets.Count.Equals(0))
                if (TempData["DatosLiquidacion"] != null)
                {
                    _model = (CorteCajaDTO)TempData["DatosLiquidacion"];
                    TempData.Keep("DatosLiquidacion");
                }
            return View(_model);
        }
        public ActionResult CallBackLiquidaciones()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            _tkn = Session["StringToken"].ToString();
            List<VentaCajaGeneralDTO> Liquidaciones = VentasServicio.BuscarLiquidacionesDelDia(_tkn);
            return PartialView("_Liquidaciones", Liquidaciones);
        }
        public ActionResult Buscar(CorteCajaDTO _model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            _tkn = Session["StringToken"].ToString();
            if (_model != null && _model.FolioOperacionDia != null)
                TempData["Model"] = _model;
            _model = VentasServicio.ListaVentasCajaGralCamioneta(_model.FolioOperacionDia, _tkn);
            if (_model != null)
                TempData["DatosLiquidacion"] = _model;
            if (_model.Tickets != null && _model.Tickets.Count == 0)
                TempData["RespuestaDTOError"] = "No existe la clave solicitada";
            if (_model.Tickets != null)
            {
                if (_model.Tickets.Count != 0)
                {
                    TempData["FolioVenta"] = _model.Tickets.FirstOrDefault().FolioVenta;
                }
                TempData.Keep("DatosLiquidacion");
            }
            return RedirectToAction("Liquidar", _model);
        }
        public ActionResult BatchEditingPartial()
        {
            TempData.Keep("DatosLiquidacion");
            return PartialView("_Tikets", ((CorteCajaDTO)TempData["DatosLiquidacion"]).Tickets);
        }
        public ActionResult Consultar(CajaGeneralModel _model, int? page)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            _tkn = Session["StringToken"].ToString();
            var Pagina = page ?? 1;
            ViewBag.CajaGeneral = VentasServicio.ListaVentasCajaGralId(_model.IdEmpresa, _tkn).ToPagedList(Pagina, 20);
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            return View("Index");
        }
        public ActionResult GuardarLiquidar(CorteCajaDTO dto)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            string _tok = Session["StringToken"].ToString();
            var respuesta = VentasServicio.GuardarLiquidacion(dto, _tok);
            TempData["RespuestaDTO"] = respuesta;
            if (respuesta.Exito)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Liquidar", dto);
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
                Mensaje = Resp.Mensaje;
                if (Resp.MensajesError != null)
                    Mensaje += Resp.MensajesError[0];
            }
            return Mensaje;
        }
        [ValidateInput(false)]
        public ActionResult FormaDePagoPartialUpdate(MVCxGridViewBatchUpdateValues<VentaPuntoVentaDTO, string> updateValues)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            List<VentaPuntoVentaDTO> update = new List<VentaPuntoVentaDTO>();
            var model = (CorteCajaDTO)TempData["DatosLiquidacion"];
            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                    update.Add(product);
            }
            TempData["RespuestaDTO"] = VentasServicio.ActualizarTicketsLiquidacion(update, _tkn);
            return RedirectToAction("Buscar", model);
        }
        public static List<SelectListItem> ListaFormaPago()
        {
            List<SelectListItem> Pago = new List<SelectListItem>();
            Pago.Add(new SelectListItem { Value = string.Empty, Text = string.Empty });
            Pago.Add(new SelectListItem { Value = "Cheques", Text = "Cheques" });
            Pago.Add(new SelectListItem { Value = "Transferencias", Text = "Transferencias" });
            return Pago;
        }

        public ActionResult EdicionTickets(VentaPuntoVentaDTO _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            _tkn = Session["StringToken"].ToString();
            if (TempData["RespuestaDTO"] != null)
            {
                var Respuesta = ((RespuestaDTO)TempData["RespuestaDTO"]);
                if (Respuesta.Exito)
                    ViewBag.Mensaje = Respuesta.Mensaje;
                else
                    ViewBag.MensajeError = Respuesta.Mensaje;
            }                
            if (_model == null)
                _model = new VentaPuntoVentaDTO();
            if (_model.Detalle == null || _model.Detalle.Count.Equals(0))
                if (TempData["Ticket"] != null)
                {
                    _model = (VentaPuntoVentaDTO)TempData["Ticket"];
                    TempData.Keep("Ticket");
                }
            return View(_model);
        }
        public ActionResult DetallePartialUpdate(MVCxGridViewBatchUpdateValues<VPuntoVentaDetalleDTO, string> updateValues)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            List<VPuntoVentaDetalleDTO> update = new List<VPuntoVentaDetalleDTO>();
            var model = (CorteCajaDTO)TempData["DatosLiquidacion"];
            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                {
                    product.FolioOperacion = ((VentaPuntoVentaDTO)TempData["Ticket"]).FolioVenta;
                    TempData.Keep("Ticket");
                    update.Add(product);
                }
            }
            TempData["RespuestaDTO"] = VentasServicio.ActualizarDetalleTicket(update, _tkn);
            TempData["Ticket"] = VentasServicio.BuscarTicket(((VentaPuntoVentaDTO)TempData["Ticket"]).FolioVenta, _tkn);
            return RedirectToAction("EdicionTickets");
        }
        public ActionResult BuscarTicket(VentaPuntoVentaDTO _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            _tkn = Session["StringToken"].ToString();

            if (_model != null && _model.FolioVenta != null)
                TempData["Model"] = _model;
            _model = VentasServicio.BuscarTicket(_model.FolioVenta, _tkn);
            if (_model != null)
                TempData["Ticket"] = _model;
            if (string.IsNullOrEmpty(_model.FolioVenta))
            {
                TempData["RespuestaDTO"] = new RespuestaDTO()
                {
                    Exito = false,
                    Mensaje = "Folio no encontrado"
                };
            }
            else
            {
                TempData["RespuestaDTO"] = new RespuestaDTO()
                {
                    Exito = true,
                    Mensaje = "Folio encontrado"
                };
            }
            TempData.Keep("Ticket");
            //var _model = VentasServicio.BuscarTicket(ticket.FolioVenta, _tkn);
            return RedirectToAction("EdicionTickets", _model);
        }
        public ActionResult BorrarTicket(string Folio)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            _tkn = Session["StringToken"].ToString();
            var resp = VentasServicio.BorrarTicket(Folio, _tkn);
            TempData["RespuestaDTO"] = resp;
            TempData.Keep("Ticket");
            if (!resp.Exito)
                return RedirectToAction("EdicionTickets");
            else
            {
                TempData["Ticket"] = null;
                return RedirectToAction("EdicionTickets");
            }
        }
        public ActionResult BatchEditingDetallePartial()
        {
            TempData.Keep("Ticket");
            return PartialView("_Detalles", ((VentaPuntoVentaDTO)TempData["Ticket"]).Detalle);
        }
        public ActionResult GuardarTicket(VentaPuntoVentaDTO dto)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            string _tok = Session["StringToken"].ToString();
            var respuesta = VentasServicio.ActualizarTicket(dto, _tok);
            TempData["RespuestaDTO"] = respuesta;
            TempData["Ticket"]= VentasServicio.BuscarTicket(dto.FolioVenta, _tok);
            return RedirectToAction("EdicionTickets");
        }
    }
}