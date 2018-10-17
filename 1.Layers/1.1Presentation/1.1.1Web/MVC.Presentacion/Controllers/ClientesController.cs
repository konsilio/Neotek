﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;

namespace MVC.Presentacion.Controllers
{
    public class ClientesController : Controller
    {
        string _tok = string.Empty;
        // GET: Clientes
        public ActionResult Index()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona(_tkn);
            ViewBag.RegimenFiscal = CatalogoServicio.ObtenerRegimenFiscal(_tkn);
            ViewBag.Clientes = CatalogoServicio.ListaClientes(0,"","", _tkn);
            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MessageExito = TempData["RespuestaDTO"];
            }
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
                TempData["RespuestaDTOError"] = ViewBag.MessageError;
            }
            ViewBag.MessageError = TempData["RespuestaDTOError"];

            return View();
        }

        public ActionResult Nuevo()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona(_tkn);
            ViewBag.Regimen = CatalogoServicio.ObtenerRegimenFiscal(_tkn);
            ViewBag.Clientes = CatalogoServicio.ListaClientes(0, "", "", _tkn);
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
                TempData["RespuestaDTOError"] = ViewBag.MessageError;
            }
            ViewBag.MessageError = TempData["RespuestaDTOError"];

            return View();
        }
        [HttpPost]
        public ActionResult GuardarCliente(ClientesModel _ojUs)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            
            var respuesta = CatalogoServicio.CrearCliente(_ojUs, _tok);
            
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Nuevo");
            }
     
        }
        public ActionResult EditarCliente(int id)
       {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona(_tkn);
            ViewBag.RegimenFiscal = CatalogoServicio.ObtenerRegimenFiscal(_tkn);          
            ViewBag.IdCliente = CatalogoServicio.ListaClientes(id, "", "", _tkn);
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
                TempData["RespuestaDTOError"] = ViewBag.MessageError;
            }
            ViewBag.MessageError = TempData["RespuestaDTOError"];
            return View();
        }

        [HttpPost]
        public ActionResult GuardaEdicionCliente(ClientesDto _Obj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            
            var respuesta = CatalogoServicio.ModificarCliente(_Obj, _tok);
          
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;              
                return RedirectToAction("EditarCliente", "Clientes", new { id = _Obj.IdCliente });
            }
        }
        
        public ActionResult BorrarClientes(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminarCliente(id, _tkn);
            ViewBag.Clientes = CatalogoServicio.ListaClientes(0, "", "", _tkn);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("Index");
            }      
        }

        public ActionResult EditarLocaciones(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.IdCliente = CatalogoServicio.ListaClientes(id, "", "", _tkn);
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tkn);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tkn);
            //llenar locaciones
            ViewBag.Locaciones = CatalogoServicio.ObtenerLocaciones(id, _tkn);
            List<ClienteLocacionMod> _lst = CatalogoServicio.ObtenerLocaciones(id, _tkn);
            if (_lst.Count() > 0) { ViewBag.LocacionOrden = _lst.OrderByDescending(x => x.Orden).Select(x => x.Orden).First(); }
            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MessageExito = TempData["RespuestaDTO"];
            }
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
                TempData["RespuestaDTOError"] = ViewBag.MessageError;
            }

            ViewBag.MessageError = TempData["RespuestaDTOError"];
            

            return View(_lst);
        }

        [HttpPost]
        public ActionResult GuardarLocaciones(ClienteLocacionMod _Obj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            
            var respuesta = CatalogoServicio.RegistraLocaciones(_Obj, _tok);
            
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;                
                return RedirectToAction("EditarLocaciones", "Clientes", new { id = _Obj.IdCliente });
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("EditarLocaciones", "Clientes", new { id = _Obj.IdCliente });
            }
         
        }
                
        public ActionResult EditarClienteLoc(int id, short idOrden)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.IdCliente = CatalogoServicio.ListaClientes(id, "", "", _tkn);
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tkn);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tkn);
            ViewBag.Locaciones = CatalogoServicio.ObtenerModel(idOrden, id, _tkn);

            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MessageExito = TempData["RespuestaDTO"];
            }
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
                TempData["RespuestaDTOError"] = ViewBag.MessageError;
            }

            ViewBag.MessageError = TempData["RespuestaDTOError"];
            return View();
        }

        [HttpPost]
        public ActionResult ActualizarLocacion(ClienteLocacionMod _ObjModel)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.ModificarClienteLocacion(_ObjModel, _tkn);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("EditarLocaciones", "Clientes", new { id = _ObjModel.IdCliente });
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("EditarClienteLoc", "Clientes", new { id = _ObjModel.IdCliente , idOrden = _ObjModel.Orden });
            }
           
        }
        public ActionResult BorrarClienteLoc(int id, short idOrden)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();        
           ClienteLocacionMod _ObjModel = CatalogoServicio.ObtenerModel(idOrden, id, _tkn);
            var respuesta = CatalogoServicio.EliminarClienteLocacion(_ObjModel, _tkn);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("EditarLocaciones", "Clientes", new { id = _ObjModel.IdCliente });
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("EditarLocaciones", "Clientes", new { id = _ObjModel.IdCliente });
            }
           
        }

        public ActionResult Buscar(ClientesModel filterObj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
          
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona(_tkn);
            ViewBag.RegimenFiscal = CatalogoServicio.ObtenerRegimenFiscal(_tkn);
            ViewBag.Clientes = CatalogoServicio.ListaClientes(0, filterObj.Rfc, filterObj.RazonSocial, _tkn);
 
            return View("Index", filterObj);
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