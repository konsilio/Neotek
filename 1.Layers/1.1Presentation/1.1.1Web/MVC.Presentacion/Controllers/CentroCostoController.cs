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
    public class CentroCostoController : MainController
    {
        string tkn = string.Empty;
        public ActionResult CentroCosto()
        {
            if (Session["StringToken"] != null)
            {
                tkn = Session["StringToken"].ToString();
                ViewBag.TiposCentrosCosto = CatalogoServicio.BuscarTipoCentrosCosto(tkn);
                ViewBag.EstacionesCarburacion = CatalogoServicio.GetListaEstacionCarburacion(tkn);
                ViewBag.UnidadAlmacenGas = CatalogoServicio.GetListaUnidadAlmcenGas(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
                ViewBag.EquipoTransporte = CatalogoServicio.GetListaEquiposTransporte(tkn);
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
                return View(CatalogoServicio.InitCentroCosto(tkn));
            }
            else
                return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
        }
        public ActionResult Crear(CentroCostoModel model)
        {
            if (Session["StringToken"] != null)
            {
                tkn = Session["StringToken"].ToString();
                var respuesta = CatalogoServicio.CrearCentroCosto(model, tkn);
                if (respuesta.Exito)
                {
                    return CentroCosto();
                }
                else
                {
                    ViewBag.MensajeError = respuesta.Mensaje;
                    return View("CentroCosto", CatalogoServicio.InitCentroCosto(tkn));
                }
            }
            else
                return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
        }
        public ActionResult ActivarEditar(int? id, CentroCostoModel model)
        {
            if (Session["StringToken"] != null)
            {
                tkn = Session["StringToken"].ToString();
                ViewBag.TiposCentrosCosto = CatalogoServicio.BuscarTipoCentrosCosto(tkn);
                ViewBag.EstacionesCarburacion = CatalogoServicio.GetListaEstacionCarburacion(tkn);
                ViewBag.UnidadAlmacenGas = CatalogoServicio.GetListaUnidadAlmcenGas(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
                ViewBag.EquipoTransporte = CatalogoServicio.GetListaEquiposTransporte(tkn);
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
                if (id != null)
                {
                    ViewBag.EsEdicion = true;
                    return View("CentroCosto", CatalogoServicio.ActivarModificar(id.Value, model, tkn));
                }
                else
                {
                    var respuesta = CatalogoServicio.EditarCentroCosto(model, tkn);
                    if (!respuesta.Exito)
                    {
                        ViewBag.MensajeError = respuesta.Mensaje;
                        return View("CentroCosto", CatalogoServicio.InitCentroCosto(tkn));
                    }
                    else
                        return View("CentroCosto", CatalogoServicio.InitCentroCosto(tkn));
                }
            }
            else
                return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
        }
        public ActionResult Eliminar(int id)
        {
            if (Session["StringToken"] != null)
            {
                tkn = Session["StringToken"].ToString();
                ViewBag.TiposCentrosCosto = CatalogoServicio.BuscarTipoCentrosCosto(tkn);
                ViewBag.EstacionesCarburacion = CatalogoServicio.GetListaEstacionCarburacion(tkn);
                ViewBag.UnidadAlmacenGas = CatalogoServicio.GetListaUnidadAlmcenGas(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
                ViewBag.EquipoTransporte = CatalogoServicio.GetListaEquiposTransporte(tkn);
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
                var respuesta = CatalogoServicio.BorrarCentroCosto(id, tkn);
                if (respuesta.Exito)
                {
                    return View("CentroCosto", CatalogoServicio.InitCentroCosto(tkn));
                }
                else
                {
                    ViewBag.MensajeError = respuesta.Mensaje;
                    return View("CentroCosto", CatalogoServicio.InitCentroCosto(tkn));
                }
            }
            else
                return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
        }
    }
}
