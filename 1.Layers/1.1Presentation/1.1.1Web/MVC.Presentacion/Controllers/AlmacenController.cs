using DevExpress.Web.Mvc;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Seguridad;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using MVC.Presentacion.Models;
using System;

namespace MVC.Presentacion.Controllers
{
    public class AlmacenController : Controller
    {
        string tkn = string.Empty;
        public ActionResult ActualizacionExistencias(int? page, AlmacenDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();            
            var Pagina = page ?? 1;          
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            ViewBag.Productos = AlmacenServicio.BuscarProductosAlmacen(TokenServicio.ObtenerIdEmpresa(tkn), tkn).ToPagedList(Pagina, 20);
            if (TempData["RespuestaDTO"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
            if (model != null && model.IdProductoLinea != 0) ViewBag.EsEdicion = true;            
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn).SingleOrDefault().NombreComercial;
            return View(model);
        }       
        public ActionResult Editar(short? id, AlmacenDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            if (id != null)
                return RedirectToAction("ActualizacionExistencias", AlmacenServicio.ActivarEditarAlmacen(id.Value, tkn));
            else
            {
                var respuesta = AlmacenServicio.ModificarAlmacen(model, tkn);
                if (respuesta.Exito)
                    return RedirectToAction("ActualizacionExistencias");
                else
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("ActualizacionExistencias");
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
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                if (Resp.MensajesError != null)
                    Mensaje = Resp.MensajesError[0];
            }
            return Mensaje;
        }

        [ValidateInput(false)]
        public ActionResult gvProductosPartial()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var model = AlmacenServicio.BuscarProductosAlmacen(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            return PartialView("_gvProductosPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult gvProductosPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] MVC.Presentacion.Models.AlmacenDTO item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_gvProductosPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult gvProductosPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] MVC.Presentacion.Models.AlmacenDTO item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_gvProductosPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult gvProductosPartialDelete(System.Int32 IdProducto)
        {
            var model = new object[0];
            if (IdProducto >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_gvProductosPartial", model);
        }
    }
}
