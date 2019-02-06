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
    public class EquipoTransporteController : Controller
    {
        string _tkn = string.Empty;
        // GET: EquipoTransporte
        public ActionResult Index(int? id, string placa = null, string vehiculo = null, string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            if (!string.IsNullOrEmpty(msj)) ViewBag.Msj = msj;
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            ViewBag.IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tkn);
            ViewBag.Vehiculos = CatalogoServicio.Obtener(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            ViewBag.TipoCombustible = CatalogoServicio.ListaCombustibleIdEmp(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            ViewBag.TipoUnidad = CatalogoServicio.ListaUnidadIdEmp(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);

            EquipoTransporteDTO model = new EquipoTransporteDTO();
            if (id != 0 && id != null)
            {
                model = CatalogoServicio.Obtener(id.Value, _tkn);
                ViewBag.EsEdicion = true;
            }

            if ((placa != "" && placa != null) || (vehiculo != "" && vehiculo != null))
            {
                model.AliasUnidadBusq = vehiculo;
                model.PlacasBusq = placa;
                ViewBag.Vehiculos = CatalogoServicio.Obtener(TokenServicio.ObtenerIdEmpresa(_tkn), placa, vehiculo, _tkn);               
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
                    ViewBag.Msj = msj;
                }
            }
            return View(model);
        }
        public ActionResult Buscar(EquipoTransporteDTO _model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            return RedirectToAction("Index", new { placa = _model.PlacasBusq, vehiculo = _model.AliasUnidadBusq });
        }
        public ActionResult Alta(EquipoTransporteDTO _model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString(); _model.IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tkn);
            var respuesta = CatalogoServicio.Crear(_model, _tkn);
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
        public ActionResult GuardarEditar(EquipoTransporteDTO model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.Modificar(model, _tkn);
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
        public ActionResult EditarVehiculo(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            return RedirectToAction("Index", new { id = id });
        }
        public ActionResult BorrarVehiculo(short id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            _tkn = Session["StringToken"].ToString();
            if (!TokenServicio.ObtenerEsAdministracionCentral(_tkn))
            {
                TempData["RespuestaDTOError"] = CatalogoServicio.SinPermisos();
                return RedirectToAction("Index");
            }

            var respuesta = CatalogoServicio.Eliminar(id, _tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Index", new { msj = respuesta.Mensaje });
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