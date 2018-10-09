using System;
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

        public ActionResult Nuevo()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona(_tkn);
            ViewBag.Regimen = CatalogoServicio.ObtenerRegimenFiscal(_tkn);
            ViewBag.Clientes = CatalogoServicio.ListaClientes(0, "", "", _tkn);
        
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
                //TempData["RespuestaDTO"] = "Alta Exitosa";//respuesta.Mensaje;
                //TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index", _ojUs);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Index", _ojUs);
            }
     
        }
        public ActionResult EditarCliente(int id)
       {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona(_tkn);
            ViewBag.RegimenFiscal = CatalogoServicio.ObtenerRegimenFiscal(_tkn);
            ViewBag.Clientes = CatalogoServicio.ListaClientes(0, "", "", _tkn);
            ViewBag.IdCliente = CatalogoServicio.ListaClientes(id, "", "", _tkn);
            //return View("Nuevo");
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
        
        public ActionResult BorrarClientes(ClientesModel _Obj, int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminarCliente(id, _tkn);
            ViewBag.Clientes = CatalogoServicio.ListaClientes(0, "", "", _tkn);
            if (respuesta.Exito)
            {
                //TempData["RespuestaDTO"] = "Baja Exitosa";//respuesta.Mensaje;
                //TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index", _Obj);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Index", _Obj);
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
                //TempData["RespuestaDTO"] = "Alta Exitosa";//respuesta.Mensaje;
                //TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index", _Obj);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Index", _Obj);
            }
         
        }
                
        public ActionResult EditarClienteLoc(ClienteLocacionMod _ObjModel, int id, short idOrden)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.IdCliente = CatalogoServicio.ListaClientes(id, "", "", _tkn);
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tkn);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tkn);
            ViewBag.Locaciones = CatalogoServicio.ObtenerModel(idOrden, id, _tkn);
            return View();
        }

        [HttpPost]
        public ActionResult ActualizarLocacion(ClienteLocacionMod _ObjModel)//, int id, short idOrden
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.ModificarClienteLocacion(_ObjModel, _tkn);

            if (respuesta.Exito)
            {
                //TempData["RespuestaDTO"] = "Cambio Exitoso";//respuesta.Mensaje;
                //TempData["RespuestaDTOError"] = null;
                return RedirectToAction("EditarLocaciones", "Clientes", new { id = _ObjModel.IdCliente });
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("EditarLocaciones", "Clientes", new { id = _ObjModel.IdCliente });
            }
           
        }
        public ActionResult BorrarClienteLoc(ClienteLocacionMod _ObjModel, int id, short idOrden)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();        
            _ObjModel = CatalogoServicio.ObtenerModel(idOrden, id, _tkn);
            var respuesta = CatalogoServicio.EliminarClienteLocacion(_ObjModel, _tkn);
            if (respuesta.Exito)
            {
                //TempData["RespuestaDTO"] = "Cambio Exitoso";//respuesta.Mensaje;
                //TempData["RespuestaDTOError"] = null;
                return RedirectToAction("EditarLocaciones", "Clientes", new { id = _ObjModel.IdCliente });
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
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