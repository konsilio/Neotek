using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using MVC.Presentacion.Models.Seguridad;

namespace MVC.Presentacion.Controllers
{
    public class AsignacionController : Controller
    {
        string tkn = string.Empty;
        public ActionResult Index(int? page, AsignacionModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ViewBag.Vehiculos = CatalogoServicio.Obtener(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            ViewBag.Asignaciones = TransporteServicio.ListaAsignacion(tkn).ToPagedList(page ?? 1, 20);
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
        public ActionResult Crear(AsignacionModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            model.IdEmpresa = TokenServicio.ObtenerIdEmpresa(tkn);
            var respuesta = TransporteServicio.GuardarAsignacion(model, tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int? id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = TransporteServicio.EliminarAsignacion(new AsignacionModel { IdAsignacion = id ?? 0 }, tkn);
            TempData["RespuestaDTO"] = respuesta;
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

