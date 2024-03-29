﻿using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class PuntoVentaController : Controller
    {
        // GET: PuntoVenta
        public ActionResult Index()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            if (ViewBag.EsAdmin)
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.ListaPV = CatalogoServicio.ListaPuntosVenta(0, _tkn);
            }
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
                ViewBag.ListaPV = CatalogoServicio.ListaPuntosVentaId(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            }
          //  ViewBag.Usuarios = TempData["Users"];

            //if (TempData["RespuestaDTO"] != null)
            //{
            //    ViewBag.MessageExito = TempData["RespuestaDTO"];
            //}
            //if (TempData["RespuestaDTOError"] != null)
            //{
            //    ViewBag.MessageError = TempData["RespuestaDTOError"];
            //}
            //ViewBag.MessageError = TempData["RespuestaDTOError"];
            if (TempData["RespuestaDTOError"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);

            return View();
        }

        public ActionResult AsignarChofer(short idE, int idPV, string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var lstusuarios = CatalogoServicio.ObtenerUsuarioOperador(idE, _tkn);
            if (lstusuarios.Count() >= 1)
            {
                ViewBag.Usuarios = lstusuarios;
            }
           // TempData["Users"] = lstusuarios;
            //ViewBag.EsSuperUser = TokenServicio.ObtenerEsSuperUsuario(_tkn);
            //if (ViewBag.EsSuperUser)
            //{

            //    ViewBag.ListaPV = CatalogoServicio.ListaPuntosVenta(0, _tkn);
            //}
            //else
            //{
            //    ViewBag.ListaPV = CatalogoServicio.ListaPuntosVentaId(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            //}
            ViewBag.ListaPV = CatalogoServicio.ListaPuntosVenta(idPV, _tkn);
            if (!string.IsNullOrEmpty(msj))
            {
                ViewBag.Msj = msj;
                if (!(bool)TempData["RespuestaDTO"])
                    ViewBag.Tipo = "alert-danger";
                else
                    ViewBag.Tipo = "alert-success";
                //ViewBag.Mensaje = ((RespuestaDTO)TempData["RespuestaDTO"]).Mensaje;
            }
            else
                ViewBag.Tipo = "alert-success";
            return View();
            //return RedirectToAction("Index");
        }

        //public ActionResult AsignarChofer(PuntoVentaModel model, short idE, int id)
        //{
        //    if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
        //    string _tkn = Session["StringToken"].ToString();
        //    var lstusuarios = CatalogoServicio.ObtenerUsuarioOperador(idE, _tkn);
        //    if (lstusuarios.Count() >= 1)
        //    {
        //        ViewBag.Usuarios = lstusuarios;
        //    }
        //    TempData["Users"] = lstusuarios;
        //    ViewBag.EsSuperUser = TokenServicio.ObtenerEsSuperUsuario(_tkn);
        //    if (ViewBag.EsSuperUser)
        //    {
        //        ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
        //        ViewBag.ListaPV = CatalogoServicio.ListaPuntosVenta(0, _tkn);
        //    }
        //    else
        //    {
        //        ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa(_tkn))).NombreComercial;
        //        ViewBag.ListaPV = CatalogoServicio.ListaPuntosVentaId(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
        //    }
        //    //return View("Index");
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public ActionResult Guardar(PuntoVentaModel model)//int idChofer, int idPV)
        {
            string _tkn = Session["StringToken"].ToString();

            List<PuntoVentaModel> mod = CatalogoServicio.ListaPuntosVenta(model.IdPuntoVenta, _tkn);
            PuntoVentaModel nmodel = mod[0];

            var respuesta = CatalogoServicio.ModificarOperador(nmodel, model.IdUsuario, _tkn);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Exito;
                return RedirectToAction("AsignarChofer", new { idE = nmodel.IdEmpresa, idPV = nmodel.IdPuntoVenta, msj = string.Concat("Asignacion exitosa del Operador "),nmodel.OperadorChofer });
            }

            else
            {
                TempData["RespuestaDTO"] = respuesta.Exito;
                return RedirectToAction("AsignarChofer", new { idE = nmodel.IdEmpresa, idPV = nmodel.IdPuntoVenta, msj = respuesta.MensajesError[0] });

            }           
        }

        public JsonResult Guardar(short idEmpresa, int idChofer, int idPV)
        {
            string _tkn = Session["StringToken"].ToString();

            List<PuntoVentaModel> model = CatalogoServicio.ListaPuntosVenta(idPV, _tkn);
            PuntoVentaModel nmodel = model[0];

            var respuesta = CatalogoServicio.ModificarOperador(nmodel, idChofer, _tkn);

            //var JsonInfo = JsonConvert.SerializeObject(list);
            //return Json(JsonInfo, JsonRequestBehavior.AllowGet);
            //if (respuesta.Exito)
            //{
            //    TempData["RespuestaDTO"] = "Cambio Exitoso";//respuesta.Mensaje;    
            //    TempData["RespuestaDTOError"] = null;
            //}
            //else
            //{
            //    TempData["RespuestaDTOError"] = respuesta.Mensaje;
            //}

             //return new JsonResult { Data = new { IsCorrect = respuesta.Exito, Message = respuesta.Mensaje } };
            var JsonInfo = JsonConvert.SerializeObject(respuesta.Mensaje);
            return Json(JsonInfo, JsonRequestBehavior.AllowGet);

            //return Json(new
            //{
            //    redirectUrl = Url.Action("Index", nmodel),
            //    isRedirect = true
            //});

        }
        public ActionResult BorrarPuntoVenta(List<PuntoVentaModel> _ObjModel, short idE, int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            _ObjModel = CatalogoServicio.ListaPuntosVenta(id, _tkn);
            var respuesta = CatalogoServicio.EliminarPuntosVenta(_ObjModel[0], _tkn);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = "Baja Exitosa";//respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta.Mensaje;
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