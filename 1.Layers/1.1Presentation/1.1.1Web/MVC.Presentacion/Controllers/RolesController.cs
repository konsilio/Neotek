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
            _tok = Session["StringToken"].ToString();
            ViewBag.listaEmpresas = AutenticacionServicio.EmpresasLogin();
            RolDto rol = new RolDto()
            {
                ListaRoles = CatalogoServicio.ObtenerTodosRoles(_tok)
            };            
            return View(rol);
        }

        public ActionResult AgregarNuevoRol(RolDto ObjRol)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.AgregarRoles(ObjRol, _tok);
            }

            return RedirectToAction("Index", ObjRol);
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
            string _tkn = Session["StringToken"].ToString();
            CatalogoServicio.EliminaRolSel(id, _tkn);
            return RedirectToAction("Index");
        }

        //Actualizar nombre ROL- funcionalidad --evento Guardar--
        public ActionResult GuardarCambioRol(RolDto rol)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.ActualizaNombreRol(rol, _tok);
            }
            return RedirectToAction("Index");
        }

        public ActionResult GuardarPermisos(RolDto objrol)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.ActualizaPermisos(objrol, _tok);
            }
            return RedirectToAction("Index");
        }
    }
}