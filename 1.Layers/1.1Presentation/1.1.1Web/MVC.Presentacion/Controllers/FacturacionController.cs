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
        public ActionResult Index()
        {
            if (TempData["ListaTickets"] != null) ViewBag.Tickets = (List<VentaPuntoVentaDTO>)TempData["LisaTickets"];           
        
            
            return View();
        }

        public ActionResult Buscar(FacturacionModel _mod)
        {
            //Inicializamos la lista de tickets validando si ya existe
            //y agregar las nuevas busquedas
            List<VentaPuntoVentaDTO> tickets = new List<VentaPuntoVentaDTO>();
            if (TempData["ListaTickets"] == null)
                TempData["LisaTickets"] = tickets;
            else
                tickets = (List<VentaPuntoVentaDTO>)TempData["LisaTickets"];

            if (!_mod.Ticket.Equals(string.Empty))
                tickets.Add(FacturacionServicio.ObtenerTicket(_mod.Ticket));
            else
                tickets.AddRange(FacturacionServicio.ObtenerTickets(_mod));

            return RedirectToAction("Index");
        }
        public ActionResult Facturar(List<FacturacionModel> _mod)
        {
            //if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            //_tkn = Session["StringToken"].ToString();
            //verificar si las facturas agregadas pertenecen al mismo cliente
            var cliente = _mod[0].IdCliente;
            foreach (var id in _mod)
            {
                if (id.IdCliente != cliente)
                {
                    return RedirectToAction("Index", new { msj = "Los tickets deben pertenecer al mismo cliente.", type = "alert" });
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
