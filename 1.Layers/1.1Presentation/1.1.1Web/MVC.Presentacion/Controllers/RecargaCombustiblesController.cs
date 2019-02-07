using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Seguridad;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class RecargaCombustiblesController : Controller
    {
        string tkn = string.Empty;
        public ActionResult Index(int? page)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ViewBag.Vehiculos = CatalogoServicio.Obtener(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            ViewBag.Asignaciones = TransporteServicio.ListaAsignacion(tkn).ToPagedList(page ?? 1, 20);
            if (TempData["RespuestaDTO"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
            return View();
        }
        public ActionResult Crear(RecargaCombustibleModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = TransporteServicio.GuardarRecargaCombustible(model, tkn);
            if (respuesta.Exito)
                return RedirectToAction("Index");
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Index");
            }
        }
        public ActionResult Eliminar(int? id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = TransporteServicio.EliminarRecargaCombustible(new RecargaCombustibleModel { Id_DetalleRecargaComb = id ?? 0 }, tkn);
            if (respuesta.Exito)
                return RedirectToAction("Index");
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Index");
            }
        }
        public ActionResult Modificar(int? id, RecargaCombustibleModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (id != null)
                return RedirectToAction("Index", TransporteServicio.ActivarEditarRecarga(id ?? 0, tkn));
            else
            {
                var respuesta = TransporteServicio.EditarRecargaCombustible(model, tkn);
                if (respuesta.Exito)
                    return RedirectToAction("Index");
                else
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("Index");
                }
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
                        ModelState.AddModelError(error.Key, error.Value);
                if (Resp.MensajesError != null)
                    Mensaje = Resp.MensajesError[0];
            }
            return Mensaje;
        }
    }
}
