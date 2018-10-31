using DevExpress.Web.Mvc;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Controllers.Shared;
using MVC.Presentacion.Models.Catalogos;
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
        public ActionResult CuentaContable()
        {
            if (Session["StringToken"] != null)
            {
                string token = Session["StringToken"].ToString();
                ViewBag.Empresas = CatalogoServicio.Empresas(token);
                return View(CatalogoServicio.InitCtaContable(token));
            }
            else
                return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));

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
                    return View("CuentaContable",CatalogoServicio.InitCtaContable(token));
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
                        return View("CuentaContable", CatalogoServicio.InitCtaContable(token));
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
                    return View("CuentaContable" ,CatalogoServicio.InitCtaContable(token));
                }
                else
                {
                    ViewBag.Empresas = CatalogoServicio.Empresas(token);
                    ViewBag.MensajeError = resp.MensajesError[0] != null ? resp.MensajesError[0] : "Ocurrio un error";
                    return View(CatalogoServicio.InitCtaContable(token));
                }
            }
            else
                return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
        }
    }
}
