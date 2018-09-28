using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class PrecioVentaController : Controller
    {
        // GET: PrecioVenta
        public ActionResult Index()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);

            ViewBag.EsSuperUser = TokenServicio.ObtenerEsSuperUsuario(_tkn);
            if (ViewBag.EsSuperUser)
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.ListaPV = CatalogoServicio.ListaPuntosVenta(0, _tkn);
            }
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa(_tkn))).NombreComercial;
                ViewBag.ListaPV = CatalogoServicio.ListaPuntosVentaId(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            }

            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MessageExito = TempData["RespuestaDTO"];
            }
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = TempData["RespuestaDTOError"];
            }

            ViewBag.MessageError = TempData["RespuestaDTOError"];
            return View();
        }
        [HttpPost]

        public ActionResult Registrar(ClientesModel _ObjModel)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tok = Session["StringToken"].ToString();
           
             var respuesta = CatalogoServicio.CrearCliente(_ObjModel, _tok);
            
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = "Cambio Exitoso";//respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index", _ObjModel);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta.Mensaje;
                return RedirectToAction("Index", _ObjModel);
            }
            
        }
    }
}