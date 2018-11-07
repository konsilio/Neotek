using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
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


        public ActionResult Index(short? idempresa)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            //ViewBag.listaEmpresas = AutenticacionServicio.EmpresasLogin();
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tok);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tok);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tok).SingleOrDefault().NombreComercial;
            ViewBag.listaEmpresas = CatalogoServicio.Empresas(_tok);            
            
            var _IdEmpresa = idempresa ?? ((List<EmpresaDTO>)ViewBag.listaEmpresas).FirstOrDefault().IdEmpresa;
            ViewBag.IdEmpresa = _IdEmpresa;
            List<RolDto> lstGral = CatalogoServicio.ObtenerRoles(_tok, _IdEmpresa);
            List<RolCompras> _lstCompra = CatalogoServicio.getListcompras(lstGral);
            List<RolRequsicion> _lstReq = CatalogoServicio.getListrequisicion(lstGral);
            List<RolMovilCompra> _lstMC = CatalogoServicio.getListmc(lstGral);
            PartialViewModel rolCat = new PartialViewModel()
            {
                //ListaRolesCat = lstGral,
                ListaRoles = lstGral,
                ListaRolesCom = _lstCompra,
                ListaRequsicion = _lstReq,
                ListaMovilCompra = _lstMC,
            };

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

            return View(rolCat);
        }

        public ActionResult AgregarNuevoRol(RolDto ObjRol)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.AgregarRoles(ObjRol, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta.Mensaje;
                return RedirectToAction("Index");
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
                TempData["RespuestaDTO"] = respuesta.Mensaje;
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

            var respuesta = CatalogoServicio.ActualizaNombreRol(rol, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Index");
            }
        }

        public ActionResult GuardarPermisos(RolDto objrol)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));

            _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.ActualizaPermisos(objrol, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Index");
            }

        }

        public ActionResult GuardarPermisosCompra(RolDto objrol)
        {
            _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.ActualizaPermisosCompra(objrol, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Index");
            }
        }

        public ActionResult GuardarPerMovilCompra(RolDto objrol)
        {
            _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.ActualizaPermisosMovilCompra(objrol, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
                return RedirectToAction("Index");
            }

        }
        public ActionResult GuardarPermisoRequisicion(RolDto objrol)
        {
            _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.ActualizaPermisosRequisicion(objrol, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;//.Mensaje;
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