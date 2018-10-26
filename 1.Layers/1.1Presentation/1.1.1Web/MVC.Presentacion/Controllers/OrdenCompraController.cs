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
            var model = OrdenCompraServicio.InitOrdenCompra(idOc, tkn);          
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
            ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn).Select(p => new SelectListItem { Value = p.IdProveedor.ToString(), Text = p.NombreComercial }).ToList();
            ViewBag.IVAs = CatalogoServicio.ListaIVA();
            ViewBag.IEPs = CatalogoServicio.ListaIEPS();
            ViewBag.Estatus = model.IdOrdenCompraEstatus;
            if (TempData["RespuestaDTO"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
            return View(model);
        }
        public ActionResult CrearOrdenCompra(List<ProductoOCDTO>  model = null)
        {
            //if (Session["StringToken"] == null)
                return RedirectToAction("Index", "Home");
            //var Respuesta = OrdenCompraServicio.GenerarOrdenCompra(model, Session["StringToken"].ToString());
            //if (Respuesta.Exito)
            //{               
            //    return RedirectToAction("Ordenes", new { msj = Respuesta.Mensaje });
            //}
            //else
            //{
            //    TempData["RespuestaDTO"] = Respuesta;
            //    return RedirectToAction("OrdenCompra", new { id = model.IdRequisicion });
            //}
        }
        public ActionResult Ordenes(int? pageO, int? pageR, string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (!string.IsNullOrEmpty(msj)) ViewBag.Msj = msj;
            ViewBag.EsAdminCentral = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn);
            ViewBag.Estatus = OrdenCompraServicio.ListaEstatus(tkn);
            var model = OrdenCompraServicio.InitOrdenesCompra(tkn);
            if (pageO == null) pageO = 1;
            if (pageR == null) pageR = 1;
            ViewBag.Ordenes = model.OrdenesCompra.OrderByDescending(x => x.IdRequisicion ).ToPagedList(pageO.Value, 20);
            ViewBag.Requisiciones = model.Requisiciones.ToPagedList(pageR.Value, 20);
            return View();
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
            if (respuesta.Exito )
                return RedirectToAction("Ordenes", new { msj = respuesta.Mensaje });
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("EntradaMercancia", model);
            }
        }
        public ActionResult OrdenCompraComplemento(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var complemeto = OrdenCompraServicio.InitComplemento(id, tkn);
            ViewBag.Proveedor = complemeto.Proveedor;
            if (TempData["RespuestaDTO"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
            return View(complemeto);
        }
        [HttpPost]
        public JsonResult SolicitarPagoExpedidor(OrdenCompraComplementoGasDTO model = null)
        {
            if (Session["StringToken"] == null) RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = OrdenCompraServicio.SolicitarPagoExpedidor(model, tkn);
            if (respuesta.Exito)
            {
                var js = JsonConvert.SerializeObject(respuesta);
                return Json(js, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                RedirectToAction("OrdenCompraComplementoGas", model.IdOrdenCompraExpedidor);
                return new JsonResult();
            }           
        }
        [HttpPost]
        public JsonResult SolicitarPagoPorteador(OrdenCompraComplementoGasDTO model = null)
        {
            if (Session["StringToken"] == null) RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = OrdenCompraServicio.SolicitarPagoPorteador(model, tkn);
            if (respuesta.Exito)
            {
                var js = JsonConvert.SerializeObject(respuesta);
                return Json(js, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                RedirectToAction("OrdenCompraComplementoGas", model.IdOrdenCompraPorteador);
                return new JsonResult();
            }
        }
        public JsonResult GuardarDatosExpedidor(OrdenCompraComplementoGasDTO model = null)
        {
            if (Session["StringToken"] == null) RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = OrdenCompraServicio.SolicitarPagoExpedidor(model, tkn);
            if (respuesta.Exito)
            {
                var js = JsonConvert.SerializeObject(respuesta);
                return Json(js, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                RedirectToAction("OrdenCompraComplementoGas", model.IdOrdenCompraExpedidor);
                return new JsonResult();
            }
        }
        [HttpPost]
        public JsonResult GuardarDatosPorteador(OrdenCompraComplementoGasDTO model = null)
        {
            if (Session["StringToken"] == null) RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = OrdenCompraServicio.SolicitarPagoPorteador(model, tkn);
            if (respuesta.Exito)
            {
                var js = JsonConvert.SerializeObject(respuesta);
                return Json(js, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                RedirectToAction("OrdenCompraComplementoGas", model.IdOrdenCompraPorteador);
                return new JsonResult();
            }
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
            var complemeto = OrdenCompraServicio.InitComplementoGas(IdOC, tkn);
            ViewBag.IVAs = CatalogoServicio.ListaIVA();
            ViewBag.IEPs = CatalogoServicio.ListaIEPS();
            ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn).Select(cc => new SelectListItem { Value = cc.IdCuentaContable.ToString(), Text = cc.Descripcion }).ToList();
            ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn).Select(p => new SelectListItem { Value = p.IdProveedor.ToString(), Text = p.NombreComercial }).ToList();
            if (TempData["RespuestaDTO"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);            
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
        public ActionResult ProductoOCPartial(OrdenCompraModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();        
            ViewBag.IVAs = CatalogoServicio.ListaIVA();
            ViewBag.IEPs = CatalogoServicio.ListaIEPS();
            ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn);
            ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn);
            //var model = OrdenCompraServicio.InitOrdenCompra(IdOC, tkn).OrdenCompraProductos;
            return PartialView("_ProductoOCPartial", model.OrdenCompraProductos);
        }   
       
        [HttpPost, ValidateInput(false)]
        public ActionResult ProductoOCPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] List<ProductoOCDTO> items)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_ProductoOCPartial", model);
        }
        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<ProductoOCDTO, int> updateValues)
        {

            return View();
        }
    }
}
