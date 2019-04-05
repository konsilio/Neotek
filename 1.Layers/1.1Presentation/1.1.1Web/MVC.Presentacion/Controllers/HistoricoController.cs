using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Models.Historico;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class HistoricoController : Controller
    {
        string tkn = string.Empty;
        public ActionResult Index(int? page, HistoricoVentaModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();

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
        public ActionResult Crear(HistoricoVentaModel model)
        {

            return RedirectToAction("Index");
        }
        public ActionResult Eliminar(int? id)
        {

            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int? id, HistoricoVentaModel model = null)
        {

            return RedirectToAction("Index");
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
                    Mensaje = Resp.MensajesError[0];
            }
            return Mensaje;
        }
    }
}
