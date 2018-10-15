﻿using MVC.Presentacion.Controllers.Shared;
using MVC.Presentacion.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using MVC.Presentacion.Models.Requisicion;
using Exceptions.MainModule.Validaciones;
using MVC.Presentacion.Models.Seguridad;
using PagedList;
using Newtonsoft.Json;

namespace MVC.Presentacion.Controllers
{
    public class RequisicionController : MainController
    {
        string tkn = string.Empty;
        public ActionResult Requisiciones(int? page, string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            TempData["ListProductosRequisicion"] = null;
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            if (!string.IsNullOrEmpty(msj)) ViewBag.NumeroRequisicion = msj;
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn).SingleOrDefault().NombreComercial;
            var Pagina = page ?? 1;
            var model = RequisicionServicio.InitRequisiciones(Session["StringToken"].ToString());
            ViewBag.Requisiciones = model.Requisiciones.ToPagedList(Pagina, 20);
            return View(model);
        }
        public ActionResult Requisicion(RequisicionDTO model = null)
        {
            if (Session["StringToken"] == null) return View("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            ViewBag.EsNueva = true;
            ViewBag.EsAdminCentral = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            ViewBag.Productos = CatalogoServicio.ListaProductos(tkn);
            ViewBag.CentrosCostos = CatalogoServicio.BuscarCentrosCosto(tkn);
            if (ViewData["RespuestaDTO"] != null)            
                Validar((RespuestaDTO)ViewData["RespuestaDTO"]);            
            return View(RequisicionServicio.InitRequisicion(tkn));
        }
        public ActionResult RequisicionAlternativa(int? id, byte? estatus)
        {
            if (Session["StringToken"] == null) return View("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var model = RequisicionServicio.RquisicionAlternativa(id.Value, estatus.Value, tkn);
            ViewBag.EsNueva = false;
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            if (model.RequisicionEstatus.Equals(RequisicionEstatusEnum.Creada))
            {
                ViewBag.reqOpinion = model.RequisicionRevision.OpinionAlmacen;
                ViewBag.btnCrear = "Finalizar";
                ViewBag.formactionBtnCrear = "Revicion";
                ViewBag.OtraAccion = "R";
            }
            else
            {
                ViewBag.reqOpinion = model.RequisicionRevision.OpinionAlmacen;
                ViewBag.btnCrear = "Autorizar";
                ViewBag.formactionBtnCrear("Autorizar");
            }
            return View("Requisicion", model);
        }
        public ActionResult RequisicionRevision(int? id, byte? estatus)
        {
            if (Session["StringToken"] == null) return View("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var model = RequisicionServicio.RequisicionRevision(id.Value, estatus.Value, tkn);
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            return View("RequisicionRevision", model);
        }
        public ActionResult RequisicionAutorizacion(int? id, byte? estatus)
        {
            if (Session["StringToken"] == null) return View("Index", "Home");

            tkn = Session["StringToken"].ToString();
            var model = RequisicionServicio.RequisicionAutorizacion(id.Value, estatus.Value, tkn);
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            if (ViewData["RespuestaDTO"] != null)
            {
                ViewBag.MensajeError = Validar((RespuestaDTO)ViewData["RespuestaDTO"]);
            }
            return View("RequisicionAutorizacion", model);
        }
        public ActionResult Revision(RequisicionRevisionModel model = null)
        {
            if (Session["StringToken"] == null) return View("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var respuesta = RequisicionServicio.FinalizarRevision(model, tkn);
            if (respuesta.Exito)
                return RedirectToAction("Requisiciones", new { msj = string.Concat("Revision exitosa de ", model.NumeroRequisicion)});
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
                ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
                ViewBag.MensajeError = Validar(respuesta);
                return RedirectToAction("RequisicionRevision", model);
            }
        }
        public ActionResult Autorizacion(RequisicionAutorizacionModel model = null)
        {
            if (Session["StringToken"] == null) return View("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = RequisicionServicio.FinalizarAutorizacion(model, tkn);
            if (respuesta.Exito)
                return RedirectToAction("Requisiciones", new { msj = string.Concat("Autorizacion exitosa ", model.NumeroRequisicion) });
            else
            {
                ViewData["RespuestaDTO"] = respuesta;
                return RedirectToAction("RequisicionAutorizacion", model);
            }

        }
        public ActionResult Agregar(RequisicionDTO model)
        {
            if (TempData["ListProductosRequisicion"] != null)
                model.Productos = (List<RequisicionProductoDTO>)TempData["ListProductosRequisicion"];
            tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            ViewBag.EsNueva = true;
            ViewBag.EsAdminCentral = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            ViewBag.Productos = CatalogoServicio.ListaProductos(tkn);
            ViewBag.CentrosCostos = CatalogoServicio.BuscarCentrosCosto(tkn);
            var newModel = RequisicionServicio.AgregarProducto(model, Session["StringToken"].ToString());
            TempData["ListProductosRequisicion"] = model.Productos;
            return View("Requisicion", newModel);
        }
        public ActionResult Editar(RequisicionDTO model, int id)
        {
            if (Session["StringToken"] == null) return View("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var newModel = RequisicionServicio.ActivarEditar(model, id, (List<RequisicionProductoDTO>)TempData["ListProductosRequisicion"], tkn);
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            ViewBag.EsNueva = true;
            ViewBag.Productos = CatalogoServicio.ListaProductos(tkn);
            ViewBag.CentrosCostos = CatalogoServicio.BuscarCentrosCosto(tkn);
            TempData["ListProductosRequisicion"] = newModel.Productos;
            return View("Requisicion", model);
        }
        public ActionResult Borrar(RequisicionDTO model, int id)
        {
            if (Session["StringToken"] == null) return View("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var newModel = RequisicionServicio.ActivarBorrar(model, id, (List<RequisicionProductoDTO>)TempData["ListProductosRequisicion"], tkn);
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            ViewBag.EsNueva = true;
            ViewBag.Productos = CatalogoServicio.ListaProductos(tkn);
            ViewBag.CentrosCostos = CatalogoServicio.BuscarCentrosCosto(tkn);
            TempData["ListProductosRequisicion"] = newModel.Productos;
            return View("Requisicion", model);
        }
        public ActionResult CrearRequisicion(RequisicionDTO model)
        {
            if (TempData["ListProductosRequisicion"] != null)
                model.Productos = (List<RequisicionProductoDTO>)TempData["ListProductosRequisicion"];
            tkn = Session["StringToken"].ToString();
            var respuesta = RequisicionServicio.GuardarRequisicion(model, tkn);
            if (respuesta.Exito)
            {
                TempData["ListProductosRequisicion"] = null;
                return RedirectToAction("Requisiciones", new { msj = string.Concat("Se genero: ", respuesta.Mensaje)});
            }
            else
            {
                ViewBag.EsNueva = true;
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
                ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
                ViewBag.MensajeError = Validar(respuesta);
                ViewBag.Productos = CatalogoServicio.ListaProductos(tkn);
                ViewBag.CentrosCostos = CatalogoServicio.BuscarCentrosCosto(tkn);
                TempData["ListProductosRequisicion"] = model.Productos;
                return View("Requisicion", model);
            }
        }
        public ActionResult CrearCancelar(RequisicionModel model)
        {
            return View();
        }
        public ActionResult RequisicionChecarRevicion(RequisicionModel model, string cbRevision, bool checkResp = false)
        {
            if (TempData["ListProductosRevicion"] != null)
                model.RequisicionRevision.ListaProductos = ((List<RequisicionProductoRevisionDTO>)TempData["ListProductosRevicion"]);

            return View("Requisicion", model);
        }
        public JsonResult GetProductos(short idTipo)
        {
            tkn = Session["StringToken"].ToString();
            var list = CatalogoServicio.ListaProductos(tkn).Where(x => x.IdProductoServicioTipo.Equals(idTipo)).ToList();
            var JsonInfo = JsonConvert.SerializeObject(list);
            return Json(JsonInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnidadMedida(int idPord)
        {
            tkn = Session["StringToken"].ToString();
            var unidad = CatalogoServicio.ListaProductos(tkn).SingleOrDefault(x => x.IdProducto.Equals(idPord)).UnidadMedida;
            var JsonInfo = JsonConvert.SerializeObject(unidad);
            return Json(JsonInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BuscarPorNumeroRequisicion(string numRequisicon, short idEmpresa)
        {
            tkn = Session["StringToken"].ToString();
            var list = RequisicionServicio.BuscarRequisiciones(idEmpresa, tkn)
                .Where(req => req.NumeroRequisicion.Contains(numRequisicon))
                .OrderByDescending(x => x.IdRequisicion).ToList();
            var JsonInfo = JsonConvert.SerializeObject(list);
            return Json(JsonInfo, JsonRequestBehavior.AllowGet);
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
