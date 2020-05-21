using DevExpress.Web.Mvc;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Controllers.Shared;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class CuentaContableController : Controller
    {
        string tkn = string.Empty;
        public ActionResult CuentaContable(CuentaContableModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            string token = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(token);
            if (model.IdCuentaContable != 0)
                ViewBag.EsEdicion = true;

            if (TempData["RespuestaDTO"] != null)
            {
                var resp = (RespuestaDTO)TempData["RespuestaDTO"];
                if (resp.Exito)
                    ViewBag.Msj = resp.Mensaje;
                else
                    ViewBag.MensajeError = Validar(resp);
            }
            return View(model);
        }
        public ActionResult Crear(CuentaContableModel model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.GuardarCuentaContable(model, tkn);
            TempData["RespuestaDTO"] = respuesta;
            if (respuesta.Exito)
                return RedirectToAction("CuentaContable");
            else
                return RedirectToAction("CuentaContable", model);
        }
        public ActionResult ActivarEditar(int? id, CuentaContableModel model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            if (id != null)
                return RedirectToAction("CuentaContable", CatalogoServicio.ActivarModifiarCuentaContable(id.Value, tkn));
            else
            {
                var respuesta = CatalogoServicio.EditarCuentaContable(model, tkn);
                TempData["RespuestaDTO"] = respuesta;
                if (respuesta.Exito)
                    return RedirectToAction("CuentaContable");
                else
                    return View("CuentaContable", model);
            }
        }
        public ActionResult Eliminar(int id)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.BorrarCuentaContable(id, tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("CuentaContable");

        }
        public ActionResult CuentaContableAutorizado(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["RespuestaDTO"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
            if (id.Equals(0))
                return RedirectToAction("CuentaContable", CatalogoServicio.InitCtaContable(tkn));
            else
            {
                var cc = CatalogoServicio.ListaCtaCtble(tkn).SingleOrDefault(x => x.IdCuentaContable.Equals(id));
                if (cc == null)
                    return RedirectToAction("CuentaContable", CatalogoServicio.InitCtaContable(tkn));

                CuentaContableAutorizadoDTO model = new CuentaContableAutorizadoDTO();
                model.IdCuentaContable = cc.IdCuentaContable;
                model.DescripcionCuenta = cc.Descripcion;
                model.Fecha = DateTime.Now;
                model.Autorizado = 0;
                return View(model);
            }
        }
        public ActionResult CrearAutorizado(CuentaContableAutorizadoDTO model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.GuardarCtaCtbleAutorizado(model, tkn);
            TempData["RespuestaDTO"] = respuesta;
            if (respuesta.Exito)
                return RedirectToAction("CuentaContable", CatalogoServicio.InitCtaContable(tkn));
            else
                return View("CuentaContableAutorizado", model);
        }
        public ActionResult Grid()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            return PartialView("_CuentasContables", CatalogoServicio.ListaCtaCtble(tkn));
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
