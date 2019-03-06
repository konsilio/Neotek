using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Facturacion;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class FacturacionController : Controller
    {
        string _tkn = string.Empty;
        // GET: Facturacion
        public ActionResult Index(DateTime? fechaVenta, int? Cliente, string rfc = null, string ticket = null, string msj = null, string type = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            ViewBag.IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tkn);

            List<FacturacionModel> _model = new List<FacturacionModel>();
            if (fechaVenta != null || Cliente != null || rfc != null || ticket != null)
            {
                FacturacionModel _filtro = new FacturacionModel();
                _filtro.IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tkn);
                _filtro.FechaVenta = fechaVenta.Value;
                _filtro.IdCliente = Cliente.Value;
                _filtro.Rfc = rfc;
                _filtro.Ticket = ticket;
                _model = FacturacionServicio.ObtenerInfoTicket(_filtro, _tkn);

                if (ViewBag.Model.Count == 0)
                {
                    ViewBag.MensajeError = "No se encontraron resultados de la venta..";
                }
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
                    ViewBag.Msj = msj;
                }
            }

            if (_model.Count > 0)
            {
                ViewBag.Model = _model;
            }
            else
            {
                FacturacionModel _filtro = new FacturacionModel();
                _model.Add(_filtro);
            }

            return View(_model);
        }

        public ActionResult Buscar(List<FacturacionModel> _mod)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            return RedirectToAction("Index", new { fechaVenta = _mod[0].FechaVenta, Cliente = _mod[0].IdCliente, rfc = _mod[0].Rfc, ticket = _mod[0].Ticket });

        }
        public ActionResult Facturar(List<FacturacionModel> _mod)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            //verificar si las facturas agregadas pertenecen al mismo cliente
            var cliente = _mod[0].IdCliente;
            foreach (var id in _mod)
            {
                if (id.IdCliente != cliente)
                {
                    return RedirectToAction("Index", new { msj = "Los tickets deben pertenecer al mismo cliente.", type = "alert" });
                }
            }
            //ViewBag.disable = "false";
            //ViewBag.btndisable = "";
            ViewBag.Disabled = "disabled";
            ClientesModel Cliente = CatalogoServicio.ListaClientes(36, 0, 0, "", "", _tkn).FirstOrDefault();//_mod[0].IdCliente
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona(_tkn).Where(x => x.IdTipoPersona == Cliente.IdTipoPersona);
            ViewBag.Regimen = CatalogoServicio.ObtenerRegimenFiscal(_tkn).Where(x => x.IdRegimenFiscal == Cliente.IdRegimenFiscal); 
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
