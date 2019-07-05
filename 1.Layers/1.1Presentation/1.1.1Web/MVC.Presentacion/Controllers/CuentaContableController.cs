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
        public ActionResult CuentaContable()
        {
            if (Session["StringToken"] != null)
            {
                string token = Session["StringToken"].ToString();
                ViewBag.Empresas = CatalogoServicio.Empresas(token);
                return View(CatalogoServicio.InitCtaContable(token));
            }
            else
                return View(AutenticacionServicio.InitIndex(new LoginModel()));

        }
        public ActionResult Crear(CuentaContableModel model)
        {
            if (Session["StringToken"] != null)
            {
                string token = Session["StringToken"].ToString();
                var respuesta = CatalogoServicio.GuardarCuentaContable(model, token);
                if (respuesta.Exito)
                {
                    ViewBag.Empresas = CatalogoServicio.Empresas(token);
                    return View("CuentaContable", CatalogoServicio.InitCtaContable(token));
                }
                else
                {
                    ViewBag.Empresas = CatalogoServicio.Empresas(token);
                    ViewBag.MensajeError = respuesta.Mensaje;
                    return View("CuentaContable", model);
                }
            }
            else
                return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
        }
        public ActionResult ActivarEditar(int? id, CuentaContableModel model)
        {
            if (Session["StringToken"] != null)
            {
                string token = Session["StringToken"].ToString();
                if (id != null)
                {
                    ViewBag.Empresas = CatalogoServicio.Empresas(token);
                    ViewBag.EsEdicion = true;
                    return View("CuentaContable", CatalogoServicio.ActivarModifiarCuentaContable(id.Value, model, token));
                }
                else
                {
                    var respuesta = CatalogoServicio.EditarCuentaContable(model, token);
                    if (respuesta.Exito)
                    {
                        model.Numero = string.Empty;
                        model.Descripcion = string.Empty;
                        ViewBag.Empresas = CatalogoServicio.Empresas(token);
                        return RedirectToAction("CuentaContable");
                    }
                    else
                    {
                        ViewBag.Empresas = CatalogoServicio.Empresas(token);
                        ViewBag.MensajeError = respuesta.Mensaje;
                        return View("CuentaContable", model);

                    }
                }
            }
            else
                return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
        }
        public ActionResult Eliminar(int id)
        {
            if (Session["StringToken"] != null)
            {
                string token = Session["StringToken"].ToString();
                var resp = CatalogoServicio.BorrarCuentaContable(id, token);
                if (resp.Exito)
                {
                    ViewBag.Empresas = CatalogoServicio.Empresas(token);
                    return View("CuentaContable", CatalogoServicio.InitCtaContable(token));
                }
                else
                {
                    ViewBag.Empresas = CatalogoServicio.Empresas(token);
                    ViewBag.MensajeError = resp.MensajesError[0] != null ? resp.MensajesError[0] : "Ocurrio un error";
                    return View(CatalogoServicio.InitCtaContable(token));
                }
            }
            else
                return View(AutenticacionServicio.InitIndex(new LoginModel()));
        }
        public ActionResult CuentaContableAutorizado(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["RespuestaDTOError"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
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
            if (Session["StringToken"] != null)
            {
                string token = Session["StringToken"].ToString();
                var respuesta = CatalogoServicio.GuardarCtaCtbleAutorizado(model, token);
                if (respuesta.Exito)
                    return RedirectToAction("CuentaContable", CatalogoServicio.InitCtaContable(token));
                else
                {
                    TempData["RespuestaDTOError"] = respuesta;
                    return View("CuentaContableAutorizado", model);
                }
            }
            else
                return View(AutenticacionServicio.InitIndex(new LoginModel()));
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
