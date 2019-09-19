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
            TokenServicio.ClearTemp(TempData);

            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ViewBag.Vehiculos = CatalogoServicio.Obtener(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            //ViewBag.Asignaciones = TransporteServicio.ListaAsignacion(tkn).ToPagedList(page ?? 1, 20);
            TempData["DataSourceAsignaciones"] = TransporteServicio.ListaAsignacion(tkn).ToList();
            TempData.Keep("DataSourceAsignaciones");
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

        public ActionResult Eliminar(short? Id, short? TV)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = TransporteServicio.EliminarAsignacion(new AsignacionModel { IdVehiculo = Id ?? 0, TipoVehiculo = TV ?? 0 }, tkn);
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
        public ActionResult CB_Asignaciones()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            List<AsignacionModel> model = new List<AsignacionModel>();
            if (TempData["DataSourceAsignaciones"] != null)
            {
                model = (List<AsignacionModel>)TempData["DataSourceAsignaciones"];
                TempData["DataSourceAsignaciones"] = model;
               // TempData.Keep("DataSourceAsignaciones");
            }
            return PartialView("_CB_Asignaciones", model);
        }
    }
}

