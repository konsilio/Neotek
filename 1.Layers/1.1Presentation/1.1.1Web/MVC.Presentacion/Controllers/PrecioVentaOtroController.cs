﻿using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class PrecioVentaOtroController : Controller
    {
        // GET: PrecioVentaOtro
        public ActionResult Index()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            if (ViewBag.EsAdmin)
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.ListaPV = CatalogoServicio.ListaPrecioVenta(0, _tkn);
            }
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
                ViewBag.ListaPV = CatalogoServicio.ListaPrecioVentaIdEmpresa(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            }

            ViewBag.ListaStatus = CatalogoServicio.ListaTipoFecha(_tkn);
            //if (TempData["RespuestaDTO"] != null)
            //{
            //    ViewBag.MessageExito = TempData["RespuestaDTO"];
            //}
            //if (TempData["RespuestaDTOError"] != null)
            //{
            //    ViewBag.MessageError = TempData["RespuestaDTOError"];
            //}
            //ViewBag.MessageError = TempData["RespuestaDTOError"];

            ViewBag.Categoria = CatalogoServicio.ListaPrecioVenta(0, _tkn).GroupBy(x => x.Categoria).Select(x => x.FirstOrDefault());
            ViewBag.Linea = CatalogoServicio.ListaPrecioVenta(0, _tkn).GroupBy(x => x.Linea).Select(x => x.FirstOrDefault());
            ViewBag.Producto = CatalogoServicio.ListaPrecioVenta(0, _tkn).GroupBy(x => x.Producto).Select(x => x.FirstOrDefault());
            if (TempData["RespuestaDTOError"] != null) ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);

            return View();
        }

        [HttpPost]
        public ActionResult Registrar(PrecioVentaModel _ObjModel)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.RegistrarPrecio(_ObjModel, _tok);

            if (respuesta.Exito)
            {
                //TempData["RespuestaDTO"] = "Cambio Exitoso";
                //TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index", _ObjModel);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Index", _ObjModel);
            }

        }
        public ActionResult EditarPrecioVentaOtro(short id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.ListaPV = CatalogoServicio.ListaPrecioVenta(id, _tkn);

            return View();
        }

        [HttpPost]
        public ActionResult ActualizarPrecioVentaOtro(PrecioVentaModel _Obj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.ModificarPrecioVenta(_Obj, _tok);

            if (respuesta.Exito)
            {
                //TempData["RespuestaDTO"] = "Cambio Exitoso";//respuesta.Mensaje;
                //TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index", _Obj);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Index", _Obj);
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