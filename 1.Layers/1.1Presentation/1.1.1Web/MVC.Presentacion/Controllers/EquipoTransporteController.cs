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
        public ActionResult Index(string placa = null, string vehiculo = null, string msj = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tkn = Session["StringToken"].ToString();
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            ViewBag.IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tkn);
            ViewBag.Vehiculos = CatalogoServicio.Obtener(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            ViewBag.TipoCombustible = CatalogoServicio.ListaCombustibleIdEmp(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            List<EquipoTransporteDTO> _model = CatalogoServicio.Obtener(TokenServicio.ObtenerIdEmpresa(_tkn), placa, vehiculo, _tkn);
            EquipoTransporteDTO model = new EquipoTransporteDTO();
            if (_model.Count > 1)
            {
                model = _model[0];
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
            return RedirectToAction("Index", new { placa = _model.Placas, vehiculo = _model.AliasUnidad });
        }
        public ActionResult Alta(EquipoTransporteDTO _model, int? Id)
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

            //ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            //if (ViewBag.EsAdmin)
            //    ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            //else
            //    ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;

            //EquipoTransporteDTO _lst = CatalogoServicio.Obtener(Id.Value, _tkn);

            //if (TempData["RespuestaDTO"] != null)
            //{
            //    if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
            //    {
            //        ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
            //        ViewBag.EsEdicion = false; ViewBag.Locaciones = TempData["Locaciones"];
            //    }
            //    else
            //    {
            //        ViewBag.Msj = ((RespuestaDTO)TempData["RespuestaDTO"]).Mensaje;
            //        ViewBag.EsEdicion = false; ViewBag.Locaciones = null;
            //    }
            //}
            //return View(_lst);
        }
        public ActionResult EditarVehiculo(int id, EquipoTransporteDTO model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            if (id != 0)
            {
                return RedirectToAction("Alta", new { id = id, _model = model });//, new { msj = respuesta.Mensaje });
            }
            else
            {
                var respuesta = CatalogoServicio.Crear(model, _tkn);
                if (respuesta.Exito)
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("Alta", CatalogoServicio.Obtener(model.IdEquipoTransporte, _tkn));
                }
                else
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("Alta");
                }
            }

        }
        //[HttpPost]
        //public ActionResult GuardarCliente(PedidoModel _model)
        //{
        //    if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
        //    _tkn = Session["StringToken"].ToString();

        //    var respuesta = CatalogoServicio.CrearCliente(_model, _tkn);

        //    if (respuesta.Exito)
        //    {
        //        TempData["RespuestaDTO"] = respuesta.Mensaje;
        //        return RedirectToAction("Nuevo");
        //    }

        //    else
        //    {
        //        TempData["RespuestaDTO"] = respuesta;
        //        return RedirectToAction("AltaCliente");
        //    }

        //}
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