using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.EquipoTransporte;
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


            if (TempData["RespuestaDTO"] != null)
            {
                if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
                {
                    ViewBag.Tipo = "alert-danger";
                    ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                }
                else
                {
                    ViewBag.Tipo = "alert-success";
                    ViewBag.Msj = msj;
                }
            }
            return View();
        }
        public ActionResult Buscar(ParqueVehicularModel _model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            return RedirectToAction("Index", new { placa = _model.NumeroPlacas, vehiculo = _model.AliasUnidad });
        }
        public ActionResult Alta(ParqueVehicularModel _model, int? Id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
            //int idCliente = _model != null ? _model.IdCliente : IdCliente.Value;
            // _model.IdTipoPersona = 0; _model.IdRegimenFiscal = 0;
            TempData["ModelAltaCliente"] = _model;

            //ViewBag.Cliente = _model.NombreRfc;
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tkn);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tkn);
            ParqueVehicularModel _lst = EquipoTrServicio.Obtener(Id.Value, _tkn);
            //_lst[0].IdEstadoRep = 0; _lst[0].IdPais = 0;
            //if (model != null && model.IdCliente != 0 && model.Orden != 0)
            //{
            //    ViewBag.EsEdicion = true; ViewBag.Locaciones = model;
            //    _lst[0].IdEstadoRep = ViewBag.Locaciones.IdEstadoRep; _lst[0].IdPais = ViewBag.Locaciones.IdPais;
            //}
            if (TempData["RespuestaDTO"] != null)
            {
                if (!((RespuestaDTO)TempData["RespuestaDTO"]).Exito)
                {
                    ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTO"]);
                    ViewBag.EsEdicion = false; ViewBag.Locaciones = TempData["Locaciones"];
                    // _lst[0].IdEstadoRep = ViewBag.Locaciones.IdEstadoRep; _lst[0].IdPais = ViewBag.Locaciones.IdPais;
                }
                else
                {
                    ViewBag.Msj = ((RespuestaDTO)TempData["RespuestaDTO"]).Mensaje;
                    ViewBag.EsEdicion = false; ViewBag.Locaciones = null;
                    //  _lst[0].IdEstadoRep = 0; _lst[0].IdPais = 0;
                }
            }
            return View(_lst);//
        }
        public ActionResult EditarVehiculo(int id, ParqueVehicularModel model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            if (id != 0)
            {
                return RedirectToAction("Alta", new { id = id, _model = model });//, new { msj = respuesta.Mensaje });
            }
            else
            {
                var respuesta = EquipoTrServicio.Crear(model, _tkn);
                if (respuesta.Exito)
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("Alta", EquipoTrServicio.Obtener(model.IdEquipoTransporte, _tkn));
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

            var respuesta = CatalogoServicio.EliminaEmpresaSel(id, _tkn);
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