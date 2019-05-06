using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class EgresoController : Controller
    {
        // GET: Egreso
        string tkn = string.Empty;
        public ActionResult Index(EgresoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ViewBag.Egresos = CatalogoServicio.ListaEgresos(tkn);
            ViewBag.CentroCostos = CatalogoServicio.BuscarCentrosCosto(tkn);
            ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn);

            if (TempData["RespuestaDTO"] != null)
            {
                var Respuesta = (RespuestaDTO)TempData["RespuestaDTO"];
                if (Respuesta.Exito)
                    ViewBag.Msj = Respuesta.Mensaje;
                else
                    ViewBag.MensajeError = Validar(Respuesta);
            }
            if (model != null && model.IdEgreso != 0) ViewBag.EsEdicion = true;
            return View(model);
        }
        public ActionResult Crear(EgresoDTO model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.CrearEgreso(model, tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Index");
        }
        public ActionResult Eliminar(int? id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminiarEgreso(id ?? 0, tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int? id, EgresoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (id != null)
                return RedirectToAction("Index", CatalogoServicio.ActivarEditarEgreso(id ?? 0, tkn));
            else
            {
                var respuesta = CatalogoServicio.ModificarEgreso(model, tkn);
                TempData["RespuestaDTO"] = respuesta;
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
                    Mensaje = Resp.MensajesError[0];
            }
            return Mensaje;
        }
    }
}