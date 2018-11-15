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
    public class UsuariosController : Controller
    {
        string _tok = string.Empty;
        // GET: Usuarios
        public ActionResult Index(UsuarioDTO modelo = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;

            UsuarioDTO model = new UsuarioDTO();
            ViewBag.ListaUsuarios = CatalogoServicio.ObtenerTodosUsuarios(0, _tkn);
            if (modelo.IdUsuario != 0)
                model.Listausuarios = CatalogoServicio.FiltrarBusquedaUsuario(modelo, _tkn);
            else
                model.Listausuarios = CatalogoServicio.ObtenerTodosUsuarios(0, _tkn);
            if (TempData["RespuestaDTO"] != null) ViewBag.MessageExito = TempData["RespuestaDTO"];
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
            //    TempData["RespuestaDTOError"] = ViewBag.MessageError;
            }
          //  ViewBag.MessageError = TempData["RespuestaDTOError"];
            return View(model);
        }

        public ActionResult Nuevo()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            //Se obtienen los paises         
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tok);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tok);
            ViewBag.Empresas = CatalogoServicio.Empresas(_tok);
            //ViewBag.IdUser = CatalogoServicio.ObtenerIdUsuario(0, _tok);

            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
            }
            ViewBag.MessageError = TempData["RespuestaDTOError"];

            return View();
        }
        [HttpPost]
        public ActionResult GuardarUsuario(UsuarioDTO _ojUs)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.CrearUsuario(_ojUs, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("Nuevo");
            }
        }
        //vista ActualizaCredenciales-View
        public ActionResult ActualizaCredenciales(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.IdUser = CatalogoServicio.ObtenerIdUsuario(id, _tkn);

            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
                TempData["RespuestaDTOError"] = ViewBag.MessageError;
            }
            ViewBag.MessageError = TempData["RespuestaDTOError"];
            return View();
        }
        //guarda credenciales - operacion
        public ActionResult GuardarCredenciales(UsuarioDTO objUser)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.ActualizaCredencialesUser(objUser, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("ActualizaCredenciales", "Usuarios", new { id = objUser.IdUsuario });
            }
        }
        //vista altas y bajas de Roles - View
        public ActionResult ActualizaRoles(int id, string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.IdUser = CatalogoServicio.ObtenerIdUsuario(id, _tkn);
            ViewBag.AllRoles = CatalogoServicio.ObtenerTodosRoles(_tkn);
            //if (TempData["RespuestaDTO"] != null)
            //{
            //    if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
            //        ViewBag.Tipo = "alert-danger";
            //    ViewBag.Mensaje = ((RespuestaDTO)TempData["RespuestaDTO"]).Mensaje;
            //}
            //else
            //    ViewBag.Tipo = "alert-success";

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
            //if (TempData["RespuestaDTO"] != null)
            //{
            //    ViewBag.MessageExito = TempData["RespuestaDTO"];
            //}
            //if (TempData["RespuestaDTOError"] != null)
            //{
            //    ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
            //    TempData["RespuestaDTOError"] = ViewBag.MessageError;
            //}

            //ViewBag.MessageError = TempData["RespuestaDTOError"];
            return View();
        }
        //guarda Roles asignado al usuario - operacion
        public ActionResult GuardarRol(UsuarioRolModel objUser)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.AgregarRolAlUsuario(objUser, _tok);

            if (respuesta.Exito)
            {
                //TempData["RespuestaDTO"] = respuesta.Mensaje;
                //TempData["RespuestaDTOError"] = null;
                //return RedirectToAction("ActualizaRoles", "Usuarios", new { });
                TempData["RespuestaDTO"] = respuesta.Exito;
                return RedirectToAction("ActualizaRoles", new { id = objUser.IdUsuario, msj = string.Concat("Asignacion exitosa de Rol ", objUser.IdRol) });

            }

            else
            {
                //TempData["RespuestaDTOError"] = respuesta.Mensaje;
                TempData["RespuestaDTO"] = respuesta.Exito;
                //ViewData["RespuestaDTO"] = ViewBag.MensajeError;
                return RedirectToAction("ActualizaRoles", "Usuarios", new { id = objUser.IdUsuario, msj = respuesta.MensajesError[0] });
            }

        }
        //muestra vista para edicion de usuario seleccionado
        public ActionResult EditarUsuario(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();
            //Se obtienen los paises         
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tok);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tok);
            ViewBag.Empresas = CatalogoServicio.Empresas(_tok);
            ViewBag.IdUser = CatalogoServicio.ObtenerTodosUsuarios(id, _tok);
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
                //    TempData["RespuestaDTOError"] = ViewBag.MessageError;
            }
            return View("Nuevo");
        }
        [HttpPost]
        public ActionResult GuardaEdicionUsuario(UsuarioDTO _Obj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.ActualizaEdicionUsuario(_Obj, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("EditarUsuario","Usuarios",new {id=_Obj.IdUsuario });
            }

        }
        public ActionResult BorrarUsuario(short id)
        {
            
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            //// AMGO
            
            //var usrs = CatalogoServicio.ObtenerTodosUsuarios(0, _tkn);
            //var usr = usrs.ToList<UsuariosModel>().FirstOrDefault(x => x.IdUsuario.Equals(id));
            //if (usr.EsSuperAdmin)
            //{
            //    TempData["RespuestaDTO"] = "No es posible borrar un usuario con rol SuperAdmin.";
            //    TempData["RespuestaDTOError"] = null;
            //    return RedirectToAction("Index");
            //}
            //// AMGO

            var respuesta = CatalogoServicio.EliminaUsuarioSel(id, _tkn);
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
        public ActionResult BorrarRol(UsuarioRolModel objUser, short id, int idUsr, string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.EliminarRolAlUsuario(objUser, idUsr, id, _tok);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Exito;
                return RedirectToAction("ActualizaRoles", new { id = objUser.IdUsuario, msj = string.Concat("Eliminación exitosa del Rol ", objUser.IdRol) });
            }

            else
            {
                TempData["RespuestaDTO"] = respuesta.Exito;
                return RedirectToAction("ActualizaRoles", "Usuarios", new { id = objUser.IdUsuario, msj = respuesta.MensajesError[0] });
            }

        }
        public ActionResult Buscar(UsuarioDTO filterObj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            //UsuariosModel rolCat = new UsuariosModel()
            //{
            //    Listausuarios = CatalogoServicio.FiltrarBusquedaUsuario(filterObj, _tkn)
            //};
            //filterObj.Listausuarios = CatalogoServicio.FiltrarBusquedaUsuario(filterObj, _tkn);
            return RedirectToAction("Index", "Usuarios" , filterObj);
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