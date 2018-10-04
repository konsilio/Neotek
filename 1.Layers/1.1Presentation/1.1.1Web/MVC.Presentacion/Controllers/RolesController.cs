using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class RolesController : Controller
    {
        string _tok = string.Empty;
        // GET: Roles
        public ActionResult Index()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            ViewBag.listaEmpresas = AutenticacionServicio.EmpresasLogin();
            RolDto rol = new RolDto()
            {
                ListaRoles = CatalogoServicio.ObtenerTodosRoles(_tok)
            };

            PartialViewModel rolCat = new PartialViewModel()
            {
                ListaRolesCat = CatalogoServicio.ObtenerRolesCat(_tok),
                ListaRoles = CatalogoServicio.ObtenerTodosRoles(_tok),
                ListaRolesCom = CatalogoServicio.ObtenerRolesCom(_tok),
                ListaRequsicion = CatalogoServicio.ObtenerRolesReq(_tok),
                ListaMovilCompra = CatalogoServicio.ObtenerRolesMovilCompra(_tok),

            };

            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MessageExito = TempData["RespuestaDTO"];
            }
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = TempData["RespuestaDTOError"];
            }

            ViewBag.MessageError = TempData["RespuestaDTOError"];
            return View(rolCat);
        }

        public ActionResult AgregarNuevoRol(RolDto ObjRol)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            //if (ModelState.IsValid)
            //{
            var respuesta = CatalogoServicio.AgregarRoles(ObjRol, _tok);
            //}
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = "Alta Exitosa";//respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index", ObjRol);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta.Mensaje;
                return RedirectToAction("Index", ObjRol);
            }
            
        }

        //vista editar Roles - View
        public ActionResult ActualizaNombreRol(int id)
        {
            string _tkn = Session["StringToken"].ToString();
            ViewBag.IdRol = CatalogoServicio.ObtenerRolesId(id, _tkn);
            return View();
        }

        //Operacion borrar roles
        public ActionResult BorrarRol(short id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminaRolSel(id, _tkn);
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

        //Actualizar nombre ROL- funcionalidad --evento Guardar--
        public ActionResult GuardarCambioRol(RolDto rol)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            //if (ModelState.IsValid)
            //{
            var respuesta = CatalogoServicio.ActualizaNombreRol(rol, _tok);
            //}
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = "Alta Exitosa";//respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta.Mensaje;
                return RedirectToAction("Index");
            }           
        }

        public ActionResult GuardarPermisos(RolDto objrol)
        {
            _tok = Session["StringToken"].ToString();
            //if (ModelState.IsValid)
            //{
                var respuesta = CatalogoServicio.ActualizaPermisos(objrol, _tok);
            //}
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = "Alta Exitosa";//respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta.Mensaje;
                return RedirectToAction("Index");
            }
           
        }

        public ActionResult GuardarPermisosCompra(RolDto objrol)
        {
            _tok = Session["StringToken"].ToString();
            //if (ModelState.IsValid)
            //{
            var respuesta = CatalogoServicio.ActualizaPermisosCompra(objrol, _tok);
            //}
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = "Alta Exitosa";//respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta.Mensaje;
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public JsonResult SaveList(string ItemList)
        {
            string[] arr = ItemList.Split(',');

            foreach (var id in arr)
            {
                var currentId = id;
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}