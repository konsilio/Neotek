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
        public ActionResult Index(int? page, RecargaCombustibleModel model = null)
        {
            TokenServicio.ClearTemp(TempData);

            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ViewBag.Vehiculos = CatalogoServicio.Obtener(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            ViewBag.Combustibles = CatalogoServicio.ListaCombustible(tkn);
            //ViewBag.Recargas = TransporteServicio.ListaRecargaCombustible(tkn).ToPagedList(page ?? 1, 20);

            TempData["DataSourceRecargas"] = TransporteServicio.ListaRecargaCombustible(tkn).ToList();
            TempData.Keep("DataSourceRecargas");

            ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn);
            if (TempData["RespuestaDTO"] != null)
            {
                var Respuesta = (RespuestaDTO)TempData["RespuestaDTO"];
                if (Respuesta.Exito)
                    ViewBag.Msj = Respuesta.Mensaje;
                else
                    ViewBag.MensajeError = Validar(Respuesta);
            }
            if (model != null && model.Id_DetalleRecargaComb != 0) ViewBag.EsEdicion = true;

            return View(model);
        }
        public ActionResult Crear(RecargaCombustibleModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = TransporteServicio.GuardarRecargaCombustible(model, tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Index");

        }
        public ActionResult Eliminar(int? id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = TransporteServicio.EliminarRecargaCombustible(new RecargaCombustibleModel { Id_DetalleRecargaComb = id ?? 0 }, tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Index");
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


        public ActionResult CB_Recargas()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            List<RecargaCombustibleModel> model = new List<RecargaCombustibleModel>();
            if (TempData["DataSourceRecargas"] != null)
            {
                model = (List<RecargaCombustibleModel>)TempData["DataSourceRecargas"];
                TempData["DataSourceRecargas"] = model;
                //TempData.Keep("DataSourceRecargas");
            }
            return PartialView("_CB_Recargas", model);
        }


    }
}
