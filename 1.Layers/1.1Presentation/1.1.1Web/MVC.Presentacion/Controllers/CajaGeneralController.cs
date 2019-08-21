using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Models.Ventas;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MVC.Presentacion.Controllers
{
    public class CajaGeneralController : Controller
    {
        // GET: CajaGeneral
        public ActionResult Index(int? page)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            var Pagina = page ?? 1;
            ViewBag.CboxEntidad = VentasServicio.ListaVentasCajaGral(_tkn, "Entidad").Select(x => x.PuntoVenta).Distinct();

            ViewBag.CboxConcepto = VentasServicio.ListaVentasCajaGral(_tkn, "").Select(x => x.Concepto).Distinct();
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            if (ViewBag.EsAdmin)
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.CajaGeneral = VentasServicio.ListaVentasCajaGral(_tkn, "").OrderByDescending(x => x.FechaAplicacion).ToPagedList(Pagina, 20);//.OrderByDescending(y => y.Orden).ToList();
            }
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
                ViewBag.CajaGeneral = VentasServicio.ListaVentasCajaGralId(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn).OrderByDescending(x => x.FechaAplicacion).OrderByDescending(y => y.Orden).ToList().ToPagedList(Pagina, 20);
            }

            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MessageExito = TempData["RespuestaDTO"];
            }
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MensajeError = Validar((RespuestaDTO)TempData["RespuestaDTOError"]);
                TempData["RespuestaDTOError"] = ViewBag.MessageError;
            }
            ViewBag.MessageError = TempData["RespuestaDTOError"];

            return View();
        }
        public ActionResult Liquidar(CorteCajaDTO _model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
           
            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MessageExito = TempData["RespuestaDTO"];
            }
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MensajeError = TempData["RespuestaDTOError"];
            }
            else
                ViewBag.MessageError = TempData["RespuestaDTOError"];
            if (_model == null)
                _model = new CorteCajaDTO();
            if (_model.Tickets == null || _model.Tickets.Count.Equals(0))
                if (TempData["DatosLiquidacion"] != null)
                    _model = (CorteCajaDTO)TempData["DatosLiquidacion"];
            return View(_model);
        }
        public ActionResult Buscar(CorteCajaDTO _model = null)
        {
            if (_model != null && _model.FolioOperacionDia != null)
                TempData["Model"] = _model;
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
                      
            _model = VentasServicio.ListaVentasCajaGralCamioneta(_model.FolioOperacionDia, _tkn);
            TempData["DatosLiquidacion"] = _model;
            if (_model.Tickets != null && _model.Tickets.Count == 0)
                TempData["RespuestaDTOError"] = "No existe la clave solicitada";
           
            return RedirectToAction("Liquidar", _model);
        }
        public ActionResult Consultar(CajaGeneralModel _model, int? page)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var Pagina = page ?? 1;
            ViewBag.CajaGeneral = VentasServicio.ListaVentasCajaGralId(_model.IdEmpresa, _tkn).ToPagedList(Pagina, 20);
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);

            return View("Index");
        }
        public ActionResult GuardarLiquidar(CorteCajaDTO dto)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tok = Session["StringToken"].ToString();

            //var respuesta = VentasServicio.CrearGuardarLiquidacion(_ObjModel, _tok);
            var respuesta = VentasServicio.GuardarLiquidacion(dto, _tok);
            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("Liquidar", dto);
            }
        }
        public ActionResult Estacion()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            if (TempData["RespuestaCajaGralEst"] != null)
            {
                ViewBag.CajaGeneralEstacion = TempData["RespuestaCajaGralEst"];
            }
            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MessageExito = TempData["RespuestaDTO"];
            }
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MensajeError = TempData["RespuestaDTOError"];
            }
            ViewBag.MessageError = TempData["RespuestaDTOError"];
            return View();
        }
        public ActionResult BuscarEstacion(VentaCorteAnticipoModel _model, int? page)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var Pagina = page ?? 1;
            ViewBag.CajaGeneralEstacion = VentasServicio.ListaVentasCajaGralEstacion(_model.FolioOperacion, _tkn).ToPagedList(Pagina, 10);

            if (ViewBag.CajaGeneralEstacion.Count == 0)
            {
                TempData["RespuestaDTOError"] = "No existe la clave solicitada";
            }
            else { TempData["RespuestaCajaGralEst"] = ViewBag.CajaGeneralEstacion; }
            return RedirectToAction("Estacion");

        }
        public ActionResult GuardarLiquidaEstacion(VentaCorteAnticipoModel _ObjModel)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tok = Session["StringToken"].ToString();

            var respuesta = VentasServicio.GuardarLiquidacionEstacion(_ObjModel, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Estacion");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("Estacion");
            }
        }
        public ActionResult Pipa(int? page)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            ViewBag.EsSuperUser = TokenServicio.ObtenerEsSuperUsuario(_tkn);
            var Pagina = page ?? 1;
            if (ViewBag.EsSuperUser)
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.CajaGeneral = VentasServicio.ListaVentasCajaGral(_tkn, "").ToPagedList(Pagina, 20);
            }
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa(_tkn))).NombreComercial;
                ViewBag.ListaPV = CatalogoServicio.ListaPrecioVentaIdEmpresa(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn).ToPagedList(Pagina, 20);
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