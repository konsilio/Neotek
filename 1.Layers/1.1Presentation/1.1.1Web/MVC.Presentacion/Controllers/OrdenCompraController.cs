using DevExpress.Web.Mvc;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Controllers.Shared;
using MVC.Presentacion.Models.OrdenCompra;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Newtonsoft.Json;


using System;
using System.Collections.Generic;

namespace MVC.Presentacion.Controllers
{
    public class OrdenCompraController : MainController
    {
        string tkn = string.Empty;
        public ActionResult OrdenCompra(int? id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            int idOc = id ?? 0;
            ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn);
            ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn);
            var model = OrdenCompraServicio.InitOrdenCompra(idOc, tkn);
            if (!model.EsGasTransporte)
            {
                ViewBag.IVAs = CatalogoServicio.ListaIVA();
                ViewBag.IEPs = CatalogoServicio.ListaIEPS();
            }
            ViewBag.EsGasTransporte = model.EsGasTransporte;
            TempData["OrdenCompraModel"] = model;
            if (TempData["RespuestaDTO"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
            return View(model);
        }
        public ActionResult OrdenCompraAutorizacion(int? id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            int idOc = id ?? 0;
            var model = OrdenCompraServicio.BuscarOrdenCompra(idOc, tkn);
            ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn).Select(cc => new SelectListItem { Value = cc.IdCuentaContable.ToString(), Text = cc.Descripcion }).ToList();
            //ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn).Select(p => new SelectListItem { Value = p.IdProveedor.ToString(), Text = p.NombreComercial }).ToList();
            ViewBag.IVAs = CatalogoServicio.ListaIVA();
            ViewBag.IEPs = CatalogoServicio.ListaIEPS();
            ViewBag.Estatus = model.IdOrdenCompraEstatus;
            if (model.EsGas || model.EsTransporteGas)
                ViewBag.EsGasTransporte = true;
            else
                ViewBag.EsGasTransporte = false;
            if (TempData["RespuestaDTO"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
            return View(model);
        }
        public ActionResult CrearOrdenCompra(OrdenCompraModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            var Respuesta = OrdenCompraServicio.GenerarOrdenCompra(model, Session["StringToken"].ToString());
            if (Respuesta.Exito)
            {
                return RedirectToAction("Ordenes", new { msj = Respuesta.Mensaje });
            }
            else
            {
                TempData["RespuestaDTO"] = Respuesta;
                return RedirectToAction("OrdenCompra", new { id = model.IdRequisicion });
            }
        }
        public ActionResult Buscar(int? pageO, int? pageR, string msj = null, OrdenesCompraModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (!string.IsNullOrEmpty(msj)) ViewBag.Msj = msj;
            ViewBag.EsAdminCentral = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn);
            ViewBag.Estatus = OrdenCompraServicio.ListaEstatus(tkn);
            if (model != null)
                model = OrdenCompraServicio.InitOrdenesCompraFiltros(tkn, model);
            else
                model = OrdenCompraServicio.InitOrdenesCompra(tkn);

            if (pageO == null) pageO = 1;
            if (pageR == null) pageR = 1;
            ViewBag.Ordenes = model.OrdenesCompra.OrderByDescending(x => x.IdRequisicion).ToPagedList(pageO.Value, 10);
            ViewBag.Requisiciones = model.Requisiciones.ToPagedList(pageR.Value, 10);
            return View("Ordenes", model);
        }
        public ActionResult Ordenes(int? pageO, int? pageR, string msj = null, OrdenesCompraModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (!string.IsNullOrEmpty(msj)) ViewBag.Msj = msj;
            ViewBag.EsAdminCentral = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            //ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn);
            //ViewBag.Estatus = OrdenCompraServicio.ListaEstatus(tkn);
            if (model != null)
                model = OrdenCompraServicio.InitOrdenesCompra(tkn);

            if (pageO == null) pageO = 1;
            if (pageR == null) pageR = 1;
            ViewBag.Ordenes = model.OrdenesCompra;
            ViewBag.Requisiciones = model.Requisiciones.ToPagedList(pageR.Value, 10);
            return View(model);
        }
        public ActionResult CB_Ordenes()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();

            List<OrdenCompraDTO> model = OrdenCompraServicio.InitOrdenesCompra(tkn).OrdenesCompra.OrderByDescending(x => x.IdRequisicion).ToList();
            return PartialView("_CB_Ordenes", model);
        }
        public ActionResult Autorizar(int? id, OrdenCompraDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            int IdOC = id ?? 0;

            var respuesta = OrdenCompraServicio.AutorizarOrdenCompra(model, tkn);
            if (respuesta.Exito)
                return RedirectToAction("Ordenes");
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraAutorizacion", new { id = model.IdOrdenCompra });
            }
        }
        public ActionResult EntradaMercancia(int? idOrden, EntradaMercanciaModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            int idOC = idOrden ?? 0;
            if (model == null || model.IdOrdenCompra == 0)
                model = OrdenCompraServicio.EntradaMercancialModel(idOC, tkn);
            model.FechaEntrada = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            if (TempData["RespuestaDTO"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);

            return View(model);
        }
        public ActionResult RegistrarEntrada(EntradaMercanciaModel model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = OrdenCompraServicio.RegistrarEntrada(model, tkn);
            if (respuesta.Exito)
                return RedirectToAction("Ordenes", new { msj = respuesta.Mensaje });
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("EntradaMercancia", model);
            }
        }
        public ActionResult OrdenCompraComplemento(int? id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var complemeto = OrdenCompraServicio.InitComplemento(id ?? 0, tkn);
            if (complemeto.FolioFiscalUUID != null && complemeto.FolioFactura != null)
            { ViewBag.enabled = true; }
            else
                ViewBag.enabled = false;
            ViewBag.Pagos = OrdenCompraServicio.SolicitarPagos(id ?? 0, tkn);
            ViewBag.IVAs = CatalogoServicio.ListaIVA();
            ViewBag.IEPs = CatalogoServicio.ListaIEPS();
            ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn);
            ViewBag.CentrosCosto = CatalogoServicio.BuscarCentrosCosto(tkn);
            ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn);

            TempData["intIdOrdenCompra"] = id ?? 0; ;
            if (TempData["RespuestaDTO"] != null)
            {
                var Respuesta = (RespuestaDTO)TempData["RespuestaDTO"];
                if (!Respuesta.Exito)
                    ViewBag.MensajeError = Validar(Respuesta);
                else
                    ViewBag.Msj = Respuesta.Mensaje;
            }
            return View(complemeto);
        }
        [HttpPost]
        public ActionResult ActualizarDatosFactura(OrdenCompraDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();

            var respuesta = OrdenCompraServicio.RegistrarDatosFactura(model, tkn);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraComplemento", new { id = model.IdOrdenCompra });

            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraComplemento", new { id = model.IdOrdenCompra });
            }
        }
        [HttpPost]
        public ActionResult SolicitarPago(OrdenCompraDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();

            var respuesta = OrdenCompraServicio.SolicitarPago(model, tkn);
            if (respuesta.Exito)
                return RedirectToAction("Ordenes");
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraComplemento", new { id = model.IdOrdenCompra });
            }
        }
        [HttpPost]
        public ActionResult SolicitarPagoExpedidor(OrdenCompraComplementoGasDTO model = null)
        {
            if (Session["StringToken"] == null) RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = OrdenCompraServicio.SolicitarPagoExpedidor(model, tkn);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraComplementoGas", new { id = model.OrdenCompraExpedidor.IdOrdenCompra });
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraComplementoGas", new { id = model.OrdenCompraExpedidor.IdOrdenCompra });
            }
        }
        [HttpPost]
        public ActionResult SolicitarPagoPorteador(OrdenCompraComplementoGasDTO model = null)
        {
            if (Session["StringToken"] == null) RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = OrdenCompraServicio.SolicitarPagoPorteador(model, tkn);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraComplementoGas", new { id = model.OrdenCompraPorteador.IdOrdenCompra });
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraComplementoGas", new { id = model.OrdenCompraPorteador.IdOrdenCompra });
            }
        }
        public ActionResult GuardarDatosExpedidor(OrdenCompraComplementoGasDTO model = null)
        {
            if (Session["StringToken"] == null) RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = OrdenCompraServicio.ConfirmarDatosExpedidor(model, tkn);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraComplementoGas", new { id = model.OrdenCompraExpedidor.IdOrdenCompra });
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraComplementoGas", new { id = model.OrdenCompraExpedidor.IdOrdenCompra });
            }
        }
        [HttpPost]
        public ActionResult GuardarDatosPorteador(OrdenCompraComplementoGasDTO model = null)
        {
            if (Session["StringToken"] == null) RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = OrdenCompraServicio.ConfirmarDatosPorteador(model, tkn);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraComplementoGas", new { id = model.OrdenCompraPorteador.IdOrdenCompra });
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraComplementoGas", new { id = model.OrdenCompraPorteador.IdOrdenCompra });
            }
        }
        public ActionResult GuardarDatosPapeleta(OrdenCompraComplementoGasDTO model = null)
        {
            if (Session["StringToken"] == null) RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = OrdenCompraServicio.ConfirmarDatosPapeleta(model, tkn);

            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("OrdenCompraComplementoGas", new { id = model.OrdenCompraPorteador.IdOrdenCompra });
        }
        public ActionResult OrdenCompraPago(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var ocp = OrdenCompraServicio.InitOrdenCompraPago(id, tkn);
            ViewBag.FormasPago = CatalogoServicio.ListaFormaPago(tkn);
            if (TempData["RespuestaDTO"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
            return View(ocp);
        }

        public ActionResult ConfirmarPago(OrdenCompraPagoDTO dto = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = OrdenCompraServicio.ConfirmarPago(dto, tkn);
            if (respuesta.Exito)
                return RedirectToAction("Ordenes");
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraPago", new { id = dto.IdOrdenCompra });
            }
        }
        public ActionResult OrdenCompraComplementoGas(int? id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            int IdOC = id ?? 0;
            TempData["intIdOrdenCompra"] = IdOC;
            var complemeto = OrdenCompraServicio.InitComplementoGas(IdOC, tkn);
            ViewBag.IVAs = CatalogoServicio.ListaIVA();
            ViewBag.IEPs = CatalogoServicio.ListaIEPS();
            ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn);
            ViewBag.CentrosCosto = CatalogoServicio.BuscarCentrosCosto(tkn);
            ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn);
            ViewBag.enabled = false;
            ViewBag.Expedidor = false;
            if (complemeto.OrdenCompraPorteador != null)
            {
                complemeto.OrdenCompraPorteador.Iva = Convert.ToDecimal(complemeto.OrdenCompraPorteador.Iva.Value.ToString().Replace(".0000", ""));
                if (complemeto.OrdenCompraPorteador.Total != 0 && complemeto.OrdenCompraPorteador.Casetas != 0 && complemeto.OrdenCompraPorteador.Casetas != null)
                    ViewBag.enabled = true;
            }
            if (complemeto.OrdenCompraExpedidor != null)
            {
                complemeto.OrdenCompraExpedidor.Iva = Convert.ToDecimal(complemeto.OrdenCompraExpedidor.Iva.Value.ToString().Replace(".0000", ""));
                if (complemeto.OrdenCompraExpedidor.MontBelvieuDlls != null && complemeto.OrdenCompraExpedidor.TarifaServicioPorGalonDlls != null)
                    ViewBag.Expedidor = true;
            }

           //ViewBag.Complemeto = complemeto;
            TempData["intIdOrdenCompra"] = id ?? 0;
            if (TempData["RespuestaDTO"] != null)
            {
                var Respuesta = (RespuestaDTO)TempData["RespuestaDTO"];
                if (!Respuesta.Exito)
                    ViewBag.MensajeError = Validar(Respuesta);
                else
                    ViewBag.Msj = Respuesta.Mensaje;
            }
            return View(complemeto);
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

        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<ProductoOCDTO, int> updateValues)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var model = (OrdenCompraModel)TempData["OrdenCompraModel"];
            model.OrdenCompraProductos = new List<ProductoOCDTO>();
            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                    model.OrdenCompraProductos.Add(product);
            }
            return CrearOrdenCompra(model);
        }
        [ValidateInput(false)]
        public ActionResult BatchEditingPartial()
        {
            return PartialView("ProductosOCPartial");
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProductosComplementoGasPartialUpdate(MVCxGridViewBatchUpdateValues<OrdenCompraProductoDTO, int> updateValues, [ModelBinder(typeof(DevExpressEditorsBinder))] OrdenCompraProductoDTO ocpDTO)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var id = (int)TempData["intIdOrdenCompra"];
            //updateValues.Update = updateValues.Update.Select(x => { x.IdOrdenCompra = id; return x; }).ToList();
            TempData["RespuestaDTO"] = OrdenCompraServicio.ActualizaProductosOrdenCompra(updateValues.Update, tkn);
            return RedirectToAction("OrdenCompraComplementoGas", new { id = id });
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProductosComplementoPartialUpdate(MVCxGridViewBatchUpdateValues<OrdenCompraProductoDTO, int> updateValues)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var id = (int)TempData["intIdOrdenCompra"];
            updateValues.Update = updateValues.Update.Select(x => { x.IdOrdenCompra = id; return x; }).ToList();
            TempData["RespuestaDTO"] = OrdenCompraServicio.ActualizaProductosOrdenCompra(updateValues.Update, tkn);
            return RedirectToAction("OrdenCompraComplemento", new { id = id });
        }

        [ValidateInput(false)]
        public ActionResult ProductComplementoPartial()
        {
            var model = new object[0];
            return PartialView("_ProductComplementoPartial", model);
        }
    }
}
