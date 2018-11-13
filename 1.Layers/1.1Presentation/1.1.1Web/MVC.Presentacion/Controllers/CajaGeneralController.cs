using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Models.Ventas;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Newtonsoft.Json;

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
            ViewBag.CboxConcepto = VentasServicio.ListaVentasCajaGral(_tkn, "").Select(x => x.TipoMovimiento).Distinct();
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            if (ViewBag.EsAdmin)
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.CajaGeneral = VentasServicio.ListaVentasCajaGral(_tkn, "").ToPagedList(Pagina, 20);

            }
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
                ViewBag.CajaGeneral = VentasServicio.ListaVentasCajaGralId(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn).ToPagedList(Pagina, 20);
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

        public ActionResult Liquidar()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));

            if (TempData["RespuestaCajaGral"] != null)
            {
                ViewBag.CajaGeneralCamioneta = TempData["RespuestaCajaGral"];
                ViewBag.SalidaGas = TempData["RespuestaSalidaGas"];
                ViewBag.SalidaGasCilindro = TempData["RespuestaSalidaGasCilindro"];
            }
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
            return View();
        }
        public ActionResult Buscar(CajaGeneralCamionetaModel _model, int? page)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            var Pagina = page ?? 1;

            ViewBag.CajaGeneralCamioneta = VentasServicio.ListaVentasCajaGralCamioneta(_model.FolioOperacionDia, _tkn).ToPagedList(Pagina, 10);
            CajaGeneralCamionetaModel nMod = (CajaGeneralCamionetaModel)ViewBag.CajaGeneralCamioneta[0];
            ViewBag.SalidaGas = VentasServicio.ListaVentasMovimientosGas(nMod, _tkn);//.ToPagedList(Pagina, 10);
            ViewBag.SalidaGasCilindro = VentasServicio.ListaVentasMovimientosGasC(nMod, _tkn).GroupBy(x=> x.CantidadKg).Select(grp => grp.First());//.ToPagedList(Pagina, 10);

            if (ViewBag.CajaGeneralCamioneta.Count == 0)
            { TempData["RespuestaDTOError"] = "No existe la clave solicitada"; }
            else
            {
                TempData["RespuestaCajaGral"] = ViewBag.CajaGeneralCamioneta;
                TempData["RespuestaSalidaGas"] = ViewBag.SalidaGas;
                TempData["RespuestaSalidaGasCilindro"] = ViewBag.SalidaGasCilindro;
            }
            return RedirectToAction("Liquidar");
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

        public ActionResult GuardarLiquidar(CajaGeneralCamionetaModel _ObjModel)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tok = Session["StringToken"].ToString();

            var respuesta = VentasServicio.CrearGuardarLiquidacion(_ObjModel, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta.Mensaje;
                TempData["RespuestaDTOError"] = null;
                return RedirectToAction("Liquidar");
            }

            else
            {
                TempData["RespuestaDTOError"] = respuesta;
                return RedirectToAction("Liquidar");
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