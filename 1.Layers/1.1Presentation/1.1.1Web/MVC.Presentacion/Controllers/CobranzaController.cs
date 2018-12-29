﻿using DevExpress.Web;
using DevExpress.Web.Demos.Mvc;
using DevExpress.Web.Mvc;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Cobranza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class CobranzaController : Controller
    {
        string _tkn = string.Empty;
        // GET: Cobranza
        public ActionResult Index()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            ViewBag.FormasPago = CatalogoServicio.ListaFormaPago(_tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
            List<CargosModel> _model = CobranzaServicio.ObtenerCargos(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);

            return View(_model);
        }

        public ActionResult CrearPedido(CargosModel _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var Id = TokenServicio.ObtenerIdEmpresa(_tkn);
            _model.IdEmpresa = Id;
            var Respuesta = CobranzaServicio.AltaNuevoCargo(_model, Session["StringToken"].ToString());
            if (Respuesta.Exito)
            {
                return RedirectToAction("Index", new { msj = Respuesta.Mensaje });
            }
            else
            {
                TempData["RespuestaDTO"] = Respuesta;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Editar(short? id, CargosModel model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tkn = Session["StringToken"].ToString();
            if (id != null)
                return RedirectToAction("ActualizacionExistencias", AlmacenServicio.ActivarEditarAlmacen(id.Value, _tkn));
            else
            {
                var respuesta = CobranzaServicio.AltaNuevoCargo(model, _tkn);
                if (respuesta.Exito)
                    return RedirectToAction("Index", new { msj = respuesta.Mensaje });
                else
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("Index");
                }
            }
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AbonosPartialUpdate(MVCxGridViewBatchUpdateValues<CargosModel, int> updateValues)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            var id = (int)TempData["intIdOrdenCompra"];
            updateValues.Update = updateValues.Update.Select(x => { x.IdCargo = id; return x; }).ToList();
            TempData["RespuestaDTO"] = CobranzaServicio.AltaNuevoCargo(updateValues.Update, _tkn);
            return RedirectToAction("Index");
        }
        [ValidateInput(false)]
        public ActionResult AbonosPartial()
        {
            var model = new object[0];
            return PartialView("_AbonosPartial", model);
        }
    }
}