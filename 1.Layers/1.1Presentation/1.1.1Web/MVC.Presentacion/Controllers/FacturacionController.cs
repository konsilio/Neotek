using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Facturacion;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if(model.IdCliente !=  0 || model.RFC != null || model.Ticket != null)
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
            //Inicializamos la lista de tickets validando si ya existe
            //y agregar las nuevas busquedas
            if (_mod.Tickets == null)
                _mod.Tickets = new List<VentaPuntoVentaDTO>();

           if (!string.IsNullOrEmpty(_mod.Ticket))
                _mod.Tickets.Add(FacturacionServicio.ObtenerTicket(_mod.Ticket));
            else
                _mod.Tickets.AddRange(FacturacionServicio.ObtenerTickets(_mod));
            TempData["ListaTickets"] = _mod.Tickets;
            return RedirectToAction("Index", _mod);
        }
        public ActionResult Facturar(FacturacionModel _mod)
        {
            //verificar si las facturas agregadas pertenecen al mismo cliente
            var cliente = _mod.Tickets[0].IdCliente;
            foreach (var tick in _mod.Tickets.Where(x => x.seleccionar).ToList())
            {
                if (tick.IdCliente != cliente)
                {
                    TempData["RespuestaDTO"] = new RespuestaDTO() { Exito = false, MensajesError = new List<string>() { "Los tickets no pertenecer al mismo cliente." } };
                    return RedirectToAction("Index", _mod);
                }
            }
            ViewBag.Disabled = "disabled";
            ClientesModel Cliente = CatalogoServicio.ListaClientes(36, 0, 0, "", "", "").FirstOrDefault();//_mod[0].IdCliente
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona("").Where(x => x.IdTipoPersona == Cliente.IdTipoPersona);
            ViewBag.Regimen = CatalogoServicio.ObtenerRegimenFiscal("").Where(x => x.IdRegimenFiscal == Cliente.IdRegimenFiscal);
            return View(Cliente);
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
