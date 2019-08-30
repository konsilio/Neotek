using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class PrecioVentaController : Controller
    {
        string _tok = string.Empty;
        // GET: PrecioVenta
        public ActionResult Index(string msj = null)
        {
            TokenServicio.ClearTemp(TempData);

            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();

            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            ViewBag.IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tkn);
            if (ViewBag.EsAdmin)
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.ListaPV = CatalogoServicio.ListaPrecioVenta(0, _tkn);
            }
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault().NombreComercial;
                ViewBag.ListaPV = CatalogoServicio.ListaPrecioVentaIdEmpresa(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            }

            TempData["DataSourcePrecioVentas"] = ViewBag.ListaPV;
            TempData.Keep("DataSourcePrecioVentas");

            ViewBag.ListaStatus = CatalogoServicio.ListaTipoFecha(_tkn);
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
            var model = new PrecioVentaModel { IdEmpresa = (short)ViewBag.IdEmpresa };
            return View(model);
        }

        [HttpPost]
        public ActionResult Registrar(PrecioVentaModel _ObjModel)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.RegistrarPrecio(_ObjModel, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Index", new { msj = respuesta.Mensaje });
            }

            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Index");
            }

        }

        public ActionResult EditarPrecioVenta(short id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tkn);
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);//ViewBag.ListaPV
            PrecioVentaModel ent = CatalogoServicio.ListaPrecioVenta(id, _tkn).FirstOrDefault();

            return View(ent);
        }

        public ActionResult BorrarPrecioVenta(PrecioVentaModel _Obj, short id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            _Obj = CatalogoServicio.ListaPrecioVenta(id, _tkn)[0];
            var respuesta = CatalogoServicio.EliminarPrecioVenta(_Obj, _tkn);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Index", new { msj = respuesta.Mensaje });
            }

            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult ActualizarPrecioVenta(PrecioVentaModel _Obj)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tok = Session["StringToken"].ToString();

            var respuesta = CatalogoServicio.ModificarPrecioVenta(_Obj, _tok);

            if (respuesta.Exito)
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Index", new { msj = respuesta.Mensaje });
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Index");
            }
        }

        public JsonResult GetConfiguracionEmpresa(short idEmpresa)
        {
            string _tkn = Session["StringToken"].ToString();
            var list = CatalogoServicio.Empresas(_tkn).SingleOrDefault(x => x.IdEmpresa.Equals(idEmpresa)).FactorLitrosAKilos;

            var JsonInfo = JsonConvert.SerializeObject(list);
            return Json(JsonInfo, JsonRequestBehavior.AllowGet);
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
                    if (Resp.MensajesError.Count() > 1)
                        Mensaje = Resp.MensajesError[0] + " " + Resp.MensajesError[1] != null ? Resp.MensajesError[1] : "";
                    else
                        Mensaje = Resp.MensajesError[0];
                        }

                if (Mensaje == "")
                    Mensaje = Resp.Mensaje;
            }
            return Mensaje;
        }


        public ActionResult CB_PrecioVentas()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            _tok = Session["StringToken"].ToString();
            List<PrecioVentaModel> model = new List<PrecioVentaModel>();
            if (TempData["DataSourcePrecioVentas"] != null)
            {
                model = (List<PrecioVentaModel>)TempData["DataSourcePrecioVentas"];
                TempData["DataSourcePrecioVentas"] = model;
                //TempData.Keep("DataSourcePrecioVentas");
            }
            return PartialView("_CB_PrecioVentas", model);
        }


    }
}