using MVC.Presentacion.App_Code;
using MVC.Presentacion.Controllers.Shared;
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
    public class CentroCostoController : MainController
    {
        string tkn = string.Empty;
        public ActionResult CentroCosto(byte? id, string mjs = null)
        {
            TokenServicio.ClearTemp(TempData);

            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (mjs != null)
            {
                ViewBag.Mjs = mjs;
            }         
            ViewBag.TiposCentrosCosto = CatalogoServicio.BuscarTipoCentrosCosto(tkn);
            ViewBag.EstacionesCarburacion = CatalogoServicio.GetListaEstacionCarburacion(tkn);
            ViewBag.UnidadAlmacenGas = CatalogoServicio.GetListaUnidadAlmcenGas(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            ViewBag.EquipoTransporte = CatalogoServicio.GetListaEquiposTransporte(tkn);
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            ModelState.Clear();
            if (TempData["RespuestaDTO"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
            if (id != null)
            {
                ViewBag.EsEdicion = true;
                return View(CatalogoServicio.ActivarModificar(id.Value, (CentroCostoModel)TempData["Model"], tkn));
            }
            else
            {
                TempData["DataSourceCentros"] = CatalogoServicio.InitCentroCosto(tkn).CentrosCostos.ToList();
                TempData.Keep("DataSourceCentros");
                return View(CatalogoServicio.InitCentroCosto(tkn));
            }

        }
        public ActionResult Crear(CentroCostoModel model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.CrearCentroCosto(model, tkn);
            if (respuesta.Exito)            
                return RedirectToAction("CentroCosto", new { mjs = "Proceso exitoso" } );            
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                TempData["Model"] = model;
                return RedirectToAction("CentroCosto");
            }
        }
        public ActionResult ActivarEditar(byte? id, CentroCostoModel model)
        {
            if (Session["StringToken"] != null)
            {
                tkn = Session["StringToken"].ToString();
                if (id != null)
                {
                    TempData["Model"] = model;
                    return RedirectToAction("CentroCosto",  new { id = id.Value });
                }
                else
                {
                    var respuesta = CatalogoServicio.EditarCentroCosto(model, tkn);
                    if (!respuesta.Exito)
                    {
                        //ViewBag.MensajeError = respuesta.Mensaje;
                        //return View("CentroCosto", CatalogoServicio.InitCentroCosto(tkn));
                        TempData["RespuestaDTO"] = respuesta;
                        TempData["Model"] = model;
                        return RedirectToAction("CentroCosto");
                    }
                    else
                        return RedirectToAction("CentroCosto", new { mjs = respuesta.Mensaje });//View("CentroCosto", CatalogoServicio.InitCentroCosto(tkn));
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
                    TempData["DataSourceCentros"] = CatalogoServicio.InitCentroCosto(tkn).CentrosCostos.ToList();
                    TempData.Keep("DataSourceCentros");
                    return View("CentroCosto", CatalogoServicio.InitCentroCosto(tkn));
                }
                else
                {
                    TempData["DataSourceCentros"] = CatalogoServicio.InitCentroCosto(tkn).CentrosCostos.ToList();
                    TempData.Keep("DataSourceCentros");
                    ViewBag.MensajeError = respuesta.Mensaje;
                    return View("CentroCosto", CatalogoServicio.InitCentroCosto(tkn));
                }
            }
            else
                return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
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


        public ActionResult CB_CentroCostos()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            List<CentroCostoDTO> model = new List<CentroCostoDTO>();
            if (TempData["DataSourceCentros"] != null)
            {
                model = (List<CentroCostoDTO>)TempData["DataSourceCentros"];
                TempData["DataSourceCentros"] = model;
                //TempData.Keep("DataSourceCentros");
            }
            return PartialView("_CB_CentroCostos", model);
        }


    }
}
