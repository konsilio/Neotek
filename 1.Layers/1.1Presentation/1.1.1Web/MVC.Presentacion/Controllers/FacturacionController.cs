using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Facturacion;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class FacturacionController : Controller
    {
        //string _tkn = string.Empty;
        // GET: Facturacion
        public ActionResult Index(FacturacionModel model = null)
        {
            if (TempData["ListaTickets"] != null) model.Tickets = (List<VentaPuntoVentaDTO>)TempData["ListaTickets"];
            if (model.IdCliente != 0 || model.RFC != null || model.Ticket != null)
                ViewBag.CFDIs = FacturacionServicio.ObtenerCFDIs(model);
            if (TempData["RespuestaDTO"] != null)
            {
                var Respuesta = (RespuestaDTO)TempData["RespuestaDTO"];
                if (Respuesta.Exito)
                    ViewBag.Msj = Respuesta.Mensaje;
                else
                    ViewBag.MensajeError = Validar(Respuesta);
            }
            return View(model);
        }
        public ActionResult Buscar(FacturacionModel _mod)
        {
            if (_mod.Tickets == null)
                _mod.Tickets = new List<VentaPuntoVentaDTO>();
            var NuevaBusqueda = new List<VentaPuntoVentaDTO>();

            if (!string.IsNullOrEmpty(_mod.Ticket))
                NuevaBusqueda.Add(FacturacionServicio.ObtenerTicket(_mod.Ticket));
            else
                NuevaBusqueda.AddRange(FacturacionServicio.ObtenerTickets(_mod));

            _mod.Tickets = FacturacionServicio.DescartarRepetidos(NuevaBusqueda, _mod.Tickets);
            TempData["ListaTickets"] = _mod.Tickets;
            return RedirectToAction("Index", _mod);
        }
        public ActionResult DatosCliente(FacturacionModel _mod)
        {
            if (TempData["RespuestaDTO"] != null)
            {
                var Respuesta = (RespuestaDTO)TempData["RespuestaDTO"];
                if (Respuesta.Exito)
                    ViewBag.Msj = Respuesta.Mensaje;
                else
                    ViewBag.MensajeError = Validar(Respuesta);
            }
            //verificar si las facturas agregadas pertenecen al mismo cliente
            var idCliente = 0;
            idCliente = _mod.Tickets[0].IdCliente;
            foreach (var tick in _mod.Tickets.Where(x => x.seleccionar).ToList())
            {
                if (tick.IdCliente != idCliente)
                {
                    TempData["RespuestaDTO"] = new RespuestaDTO() { Exito = false, MensajesError = new List<string>() { "Los tickets no pertenecer al mismo cliente." } };
                    return RedirectToAction("Index", _mod);
                }
            }
            ViewBag.EsGenerico = "false";
            ClientesModel Cliente = CatalogoServicio.ObtenerCliente(idCliente);
            if (Cliente.Rfc.Equals("XAXX010101000"))
            {
                ViewBag.EsGenerico = "true";
                Cliente = new ClientesModel();
            }
            TempData["FacturacionModel"] = _mod;
            TempData["List<VentaPuntoVentaDTO>"] = _mod.Tickets;
            ViewBag.Paises = CatalogoServicio.GetPaises();
            ViewBag.Estados = CatalogoServicio.GetEstados();
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona();
            ViewBag.Regimen = CatalogoServicio.ObtenerRegimenFiscal();
            if (Cliente.Locaciones != null && Cliente.Locaciones.Count > 0)
                Cliente.Locacion = Cliente.Locaciones[0];
            return View("Facturar",Cliente);
        }
        public ActionResult Facturar(FacturacionModel _mod)
        {
            FacturacionModel fac = (FacturacionModel)TempData["FacturacionModel"];
            List<VentaPuntoVentaDTO> tiks = (List<VentaPuntoVentaDTO>)TempData["List<VentaPuntoVentaDTO>"];
            fac.IdUsoCFDI = _mod.IdUsoCFDI;
            fac.IdFormaPago = _mod.IdFormaPago;
            fac.Tickets = tiks;
            TempData["FacturacionModel"] = fac;
            TempData["List<VentaPuntoVentaDTO>"] = tiks;

            TempData["RespuestaDTO"] = FacturacionServicio.GenerarFacturas(fac);    

            return RedirectToAction("Index", _mod);
        }
        public ActionResult GuardaEdicionCliente(ClientesModel _Obj)
        {
            var respuesta = CatalogoServicio.ModificarCliente(_Obj);
            TempData["RespuestaDTO"] = respuesta;

            FacturacionModel fac = (FacturacionModel)TempData["FacturacionModel"];
            List<VentaPuntoVentaDTO> tiks = (List<VentaPuntoVentaDTO>)TempData["List<VentaPuntoVentaDTO>"];
            TempData["FacturacionModel"] = fac;
            TempData["List<VentaPuntoVentaDTO>"] = tiks;

            return RedirectToAction("Facturar", fac);
        }
        public ActionResult GuardarNuevoCliente(ClientesModel _ojUs)
        {
            var respuesta = CatalogoServicio.CrearCliente(_ojUs);
            TempData["RespuestaDTO"] = respuesta;

            FacturacionModel fac = (FacturacionModel)TempData["FacturacionModel"];
            List<VentaPuntoVentaDTO> tiks = (List<VentaPuntoVentaDTO>)TempData["List<VentaPuntoVentaDTO>"];
            fac.Tickets = tiks;
            TempData["FacturacionModel"] = fac;
            TempData["List<VentaPuntoVentaDTO>"] = tiks;

            return RedirectToAction("Facturar", fac);
        }
        public ActionResult ContinuarGenerarFactura()
        {
            FacturacionModel fac = (FacturacionModel)TempData["FacturacionModel"];
            List<VentaPuntoVentaDTO> tiks = (List<VentaPuntoVentaDTO>)TempData["List<VentaPuntoVentaDTO>"];
            TempData["FacturacionModel"] = fac;
            TempData["List<VentaPuntoVentaDTO>"] = tiks;            

            ViewBag.ListaUsosCFDI = CatalogoServicio.ObtenerUsoCFDI();
            ViewBag.ListaFormasPago = CatalogoServicio.ListaFormaPago();

            return View("DatosCFDI", fac);
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
                    if (Resp.MensajesError.Count > 1)
                        Mensaje = Resp.MensajesError[0] + " " + Resp.MensajesError[1];
                    else
                        Mensaje = Resp.MensajesError[0];                
            }
            return Mensaje;
        }
    }
}
