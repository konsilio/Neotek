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
    public class CombustibleController : Controller
    {
        string _tkn = string.Empty;
        // GET: Combustible
        public ActionResult Index(int? id, string desc = null, string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            if (!string.IsNullOrEmpty(msj)) ViewBag.Msj = msj;

            CombustibleModel model = new CombustibleModel();
            if (id != 0 && id != null)
            {
                model = CatalogoServicio.ListaCombustibleID(id.Value, _tkn);
                ViewBag.EsEdicion = true;
            }
            if (TempData["RespuestaDTO"] != null)
            {
                if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
                {
                    ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                }
                else
                {
                    ViewBag.Tipo = "alert-success";
                }
            }
            ViewBag.Combustibles = CatalogoServicio.ListaCombustibleIdEmp(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            if (desc != "" && desc != null)
            {
                model.DescripcionBusqueda = desc;           
                ViewBag.Combustibles = CatalogoServicio.ListaCombustibleFiltrado(desc,_tkn);
                if(ViewBag.Combustibles.Count==0) ViewBag.Msj = "No se encontraron resultados"; ViewBag.Tipo = "alert-danger";
            }
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            return View(model);
        }
        public ActionResult Guardar(CombustibleModel _Obj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tkn = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.CrearCombustible(_Obj, _tkn);
            TempData["RespuestaDTO"] = respuesta;
            if (respuesta.Exito)
            {
                return RedirectToAction("Index", new { msj = respuesta.Mensaje });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult GuardarEditar(CombustibleModel model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.ModificarCombustible(model, _tkn);
            TempData["RespuestaDTO"] = respuesta;
            if (respuesta.Exito)
            {
                return RedirectToAction("Index", new { msj = respuesta.Mensaje });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Editar(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tkn = Session["StringToken"].ToString();
            return RedirectToAction("Index", new { id = id });
        }
        public ActionResult Borrar(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminiarCombustible(id, _tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Index", new { msj = respuesta.Mensaje });
        }
        public ActionResult Buscar(CombustibleModel _model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            return RedirectToAction("Index", new { desc = _model.DescripcionBusqueda });
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
                {
                    if (Resp.MensajesError.Count > 1)
                        Mensaje = Resp.MensajesError[0] + " " + Resp.MensajesError[1];
                    else
                        Mensaje = Resp.MensajesError[0];
                }
            }
            return Mensaje;
        }
    }
}