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
    public class MantenimientoController : Controller
    {
        string tkn = string.Empty;
        public ActionResult Index(int? page, MantenimientoDetalleModel model = null)
        {
            TokenServicio.ClearTemp(TempData);

            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ViewBag.Vehiculos = CatalogoServicio.Obtener(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            ViewBag.CMantenimiento = TransporteServicio.ListaCatMantenimiento(tkn);
            //ViewBag.Mantenimientos = TransporteServicio.ListaMantenimientos(tkn).ToPagedList(page ?? 1, 20);

            TempData["DataSourceMantenimientos"] = TransporteServicio.ListaMantenimientos(tkn).ToList() ;
           TempData.Keep("DataSourceMantenimientos");

            ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn);
            if (TempData["RespuestaDTO"] != null)
            {
                var Respuesta = (RespuestaDTO)TempData["RespuestaDTO"];
                if (Respuesta.Exito)
                    ViewBag.Msj = Respuesta.Mensaje;
                else
                    ViewBag.MensajeError = Validar(Respuesta);
            }
            if (model != null && model.Id_DetalleMtto != 0) ViewBag.EsEdicion = true;
            return View(model);
        }
        public ActionResult Crear(MantenimientoDetalleModel model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = TransporteServicio.RegistrarMantenimiento(model, tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Index");
        }
        public ActionResult Eliminar(int? id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = TransporteServicio.EliminarMantenimiento(id ?? 0, tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int? id, MantenimientoDetalleModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (id != null)
                return RedirectToAction("Index", TransporteServicio.ActivarEditarMantenimiento(id ?? 0, tkn));
            else
            {
                var respuesta = TransporteServicio.ModificarManteniminento(model, tkn);
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
                        ModelState.AddModelError(error.Key, error.Value);
                if (Resp.MensajesError != null)
                    Mensaje = Resp.MensajesError[0];
            }
            return Mensaje;
        }
        public ActionResult CB_Mantenimientos()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            List<MantenimientoDetalleModel> model = new List<MantenimientoDetalleModel>();
            if (TempData["DataSourceMantenimientos"] != null)
            {
                model = (List<MantenimientoDetalleModel>)TempData["DataSourceMantenimientos"];
                TempData["DataSourceMantenimientos"] = model;
               // TempData.Keep("DataSourceMantenimientos");
            }
            return PartialView("_CB_Mantenimientos", model);
        }
    }
}
