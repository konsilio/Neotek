﻿using DevExpress.Web.Mvc;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class RemanenteController : Controller
    {
        string tkn = string.Empty;
        // GET: Remanente
        public ActionResult DashBoard(RemanenteModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            if (TempData["RemanenteDTO"] != null)
            {
                ViewBag.RemaGeneral = (List<RemanenteGeneralDTO>)TempData["RemanenteDTO"];
                ViewBag.RemaGeneralFinal = ((List<RemanenteGeneralDTO>)TempData["RemanenteDTO"]).LastOrDefault();
            }
            if (TempData["RemanentePtoVentaDTO"] != null)
                ViewBag.RemaPuntoVenta = (List<RemanentePuntoVentaTodosDTO>)TempData["RemanentePtoVentaDTO"];
            if (TempData["RemanenteTractoDTO"] != null)            
                ViewBag.RemaTracto = (List<RemanenteGeneralDTO>)TempData["RemanenteTractoDTO"];
            
            
            return View(model);
        }
        public ActionResult Buscar(RemanenteModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (model.IdTipo.Equals(1)) TempData["RemanenteDTO"] = AlmacenServicio.BuscarRemanente(model, tkn);
            if (model.IdTipo.Equals(2)) TempData["RemanentePtoVentaDTO"] = AlmacenServicio.BuscarRemanentePuntoVenta(model, tkn);
            if (model.IdTipo.Equals(3)) TempData["RemanenteTractoDTO"] = AlmacenServicio.BuscarRemanenteTracto(model, tkn);

            return RedirectToAction("DashBoard", model);
        }
        public ActionResult cbPuntosventaPartial(RemanenteModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ViewData["IdPuntoVenta"] = model.IdPuntoVenta;
            return PartialView("_cbPuntosventaPartial", CatalogoServicio.ListaPuntosVentaId(TokenServicio.ObtenerIdEmpresa(tkn), tkn));
        }
    }
}
