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
        public ActionResult Index()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.listaEmpresas = AutenticacionServicio.EmpresasLogin();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.Usuarios = CatalogoServicio.ObtenerTodosUsuarios(0, _tkn);
            UsuariosModel rolCat = new UsuariosModel()
            {
                Listausuarios = CatalogoServicio.ObtenerTodosUsuarios(0, _tkn)
            };
            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MessageExito = TempData["RespuestaDTO"];
            }
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
            }

            ViewBag.MessageError = TempData["RespuestaDTOError"];


            return View(rolCat);
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
            // ViewBag.IdUser.Count() = 0;

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
                return RedirectToAction("Index", _ojUs);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("Nuevo", _ojUs);
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
                return RedirectToAction("Index", objUser);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("ActualizaCredenciales", "Usuarios", new { id = objUser.IdUsuario });
            }
        }

        //vista altas y bajas de Roles - View
        public ActionResult ActualizaRoles(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.IdUser = CatalogoServicio.ObtenerIdUsuario(id, _tkn);
            ViewBag.AllRoles = CatalogoServicio.ObtenerTodosRoles(_tkn);
            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MessageExito = TempData["RespuestaDTO"];
            }
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
            }

            ViewBag.MessageError = TempData["RespuestaDTOError"];
            return View();
        }

        //guarda Roles asignado al usuario - operacion
        public ActionResult GuardarRol(UsuariosModel objUser)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.AgregarRolAlUsuario(objUser, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("ActualizaRoles", "Usuarios", new { id = objUser.IdUsuario });
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta.Mensaje;
                return RedirectToAction("ActualizaRoles", "Usuarios", new { id = objUser.IdUsuario });
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
                return RedirectToAction("Index", _Obj);
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("Nuevo", _Obj);
            }

        }

        public ActionResult BorrarUsuario(short id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
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

        public ActionResult BorrarRol(UsuarioRolModel objUser, short id, int idUsr)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.EliminarRolAlUsuario(objUser, idUsr, id, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("ActualizaRoles", "Usuarios", new { id = objUser.IdUsuario });
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("ActualizaRoles", "Usuarios", new { id = objUser.IdUsuario });

            }

        }

        public ActionResult Buscar(UsuariosModel filterObj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            UsuariosModel rolCat = new UsuariosModel()
            {
                Listausuarios = CatalogoServicio.FiltrarBusquedaUsuario(filterObj, _tkn)
            };
            ViewBag.listaEmpresas = AutenticacionServicio.EmpresasLogin();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.Usuarios = CatalogoServicio.ObtenerTodosUsuarios(0, _tkn);
            return View("Index", rolCat);
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