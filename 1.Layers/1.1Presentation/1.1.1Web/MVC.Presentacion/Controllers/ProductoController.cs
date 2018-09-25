using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using System.Web.Script.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Newtonsoft.Json;

namespace MVC.Presentacion.Controllers
{
    public class ProductoController : Controller
    {
        string tkn = string.Empty;
        #region Categorías Producto
        public ActionResult Categoria(int? page, CategoriaProductoDTO model = null)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            RespuestaDTO Resp = new RespuestaDTO();
            tkn = Session["StringToken"].ToString();
            var Pagina = page ?? 1;
            ViewBag.Categorias = CatalogoServicio.ListaCategorias(tkn).ToPagedList(Pagina, 20); ;
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            if (TempData["RespuestaDTO"] != null)
                Resp = (RespuestaDTO)TempData["RespuestaDTO"];
            ModelState.Clear();
            if (model != null)
                if (model.IdCategoria != 0)
                    ViewBag.EsEdicion = true;
            if (Resp != null)
            {
                if (Resp.ModelStatesStandar != null)
                    foreach (var error in Resp.ModelStatesStandar.ToList())
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                if (Resp.MensajesError != null)
                    ViewBag.MensajeError = Resp.MensajesError[0];
            }
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn).SingleOrDefault().NombreComercial;
            return View(model);
        }
        [HttpPost]
        public ActionResult CrearCategoria(CategoriaProductoDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.CrearCategoriaProducto(model, tkn);
            if (respuesta.Exito)
                return RedirectToAction("Categoria");
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Categoria", new { respuesta, model });
            }
        }

        public ActionResult EliminarCategoria(short id)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminiarCategoriaProducto(new CategoriaProductoDTO { IdCategoria = id }, tkn);
            if (respuesta.Exito)
            {

                return RedirectToAction("Categoria");
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Categoria");
            }
        }
        public ActionResult EditarCategoria(short? id, CategoriaProductoDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            if (id != null)
                return RedirectToAction("Categoria", CatalogoServicio.ActivarEditarCategoria(id.Value, tkn));
            else
            {
                var respuesta = CatalogoServicio.ModificarCategoriaProducto(model, tkn);
                if (respuesta.Exito)
                    return RedirectToAction("Categoria");
                else
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("Categoria");
                }
            }
        }
        #endregion
        #region Linea Producto
        public ActionResult LineaProducto(int? page, LineaProductoDTO model = null)
        {
            if (Session["StringToken"] == null) return View("Index", "Home", AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            RespuestaDTO Resp = new RespuestaDTO();
            var Pagina = page ?? 1;
            ViewBag.Lineas = CatalogoServicio.ListaLineasProducto(tkn).ToPagedList(Pagina, 20); ;
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            if (TempData["RespuestaDTO"] != null)
                Resp = (RespuestaDTO)TempData["RespuestaDTO"];
            ModelState.Clear();
            if (model != null)
                if (model.IdProductoLinea != 0)
                    ViewBag.EsEdicion = true;
            if (Resp != null)
            {
                if (Resp.ModelStatesStandar != null)
                    foreach (var error in Resp.ModelStatesStandar.ToList())
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                if (Resp.MensajesError != null)
                    ViewBag.MensajeError = Resp.MensajesError[0];
            }
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn).SingleOrDefault().NombreComercial;
            return View(model);
        }
        [HttpPost]
        public ActionResult CrearLineaProducto(LineaProductoDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.CrearLineaProducto(model, tkn);
            if (respuesta.Exito)
                return RedirectToAction("LineaProducto");
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("LineaProducto", new { respuesta, model });
            }
        }
        public ActionResult EliminarLineaProducto(short id)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminiarLineaProducto(new LineaProductoDTO { IdProductoLinea = id }, tkn);
            if (respuesta.Exito)
            {

                return RedirectToAction("LineaProducto");
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("LineaProducto");
            }
        }
        public ActionResult EditarLineaProducto(short? id, LineaProductoDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            if (id != null)
                return RedirectToAction("LineaProducto", CatalogoServicio.ActivarEditarLineaProducto(id.Value, tkn));
            else
            {
                var respuesta = CatalogoServicio.ModificarLineaProducto(model, tkn);
                if (respuesta.Exito)
                    return RedirectToAction("LineaProducto");
                else
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("LineaProducto");
                }
            }
        }
        #endregion
        #region Unidada de Medida
        public ActionResult UnidadMedida(int? page, UnidadMedidaDTO model = null)
        {

            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            RespuestaDTO Resp = new RespuestaDTO();
            var Pagina = page ?? 1;
            ViewBag.Unidades = CatalogoServicio.ListaUnidadesMedida(tkn).ToPagedList(Pagina, 20); ;
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            if (TempData["RespuestaDTO"] != null)
                Resp = (RespuestaDTO)TempData["RespuestaDTO"];
            ModelState.Clear();
            if (model != null)
                if (model.IdUnidadMedida != 0)
                    ViewBag.EsEdicion = true;
            if (Resp != null)
            {
                if (Resp.ModelStatesStandar != null)
                    foreach (var error in Resp.ModelStatesStandar.ToList())
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                if (Resp.MensajesError != null)
                    ViewBag.MensajeError = Resp.MensajesError[0];
            }
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn).SingleOrDefault().NombreComercial;
            return View(model);
        }
        [HttpPost]
        public ActionResult CrearUnidadMedida(UnidadMedidaDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.CrearUnidadMedida(model, tkn);
            if (respuesta.Exito)
                return RedirectToAction("UnidadMedida");
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("UnidadMedida", new { respuesta, model });
            }
        }
        public ActionResult EliminarUnidadMedida(short id)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminiarUnidadMedida(new UnidadMedidaDTO { IdUnidadMedida = id }, tkn);
            if (respuesta.Exito)
            {

                return RedirectToAction("UnidadMedida");
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("UnidadMedida");
            }
        }
        public ActionResult EditarUnidadMedida(short? id, UnidadMedidaDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            if (id != null)
                return RedirectToAction("UnidadMedida", CatalogoServicio.ActivarEditarUnidadMedida(id.Value, tkn));
            else
            {
                var respuesta = CatalogoServicio.ModificarUnidadMedida(model, tkn);
                if (respuesta.Exito)
                    return RedirectToAction("UnidadMedida");
                else
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("UnidadMedida");
                }
            }
        }
        #endregion
        #region Producto
        public ActionResult Producto(int? page, short? idempresa, ProductoDTO model = null)
        {
            RespuestaDTO Resp = new RespuestaDTO();
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var Pagina = page ?? 1;
            ViewBag.Productos = CatalogoServicio.ListaProductos(tkn).ToPagedList(Pagina, 20);
            if (idempresa != null)
                ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn).Where(x => x.IdEmpresa.Equals(idempresa)).ToList();
            else
                ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn);
            ViewBag.Categorias = CatalogoServicio.ListaCategorias(tkn);
            ViewBag.LineasProducto = CatalogoServicio.ListaLineasProducto(tkn);
            ViewBag.UnidadesMedida = CatalogoServicio.ListaUnidadesMedida(tkn);
            ViewBag.UnidadesMedida2 = CatalogoServicio.ListaUnidadesMedida(tkn);
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            if (TempData["RespuestaDTO"] != null)
                Resp = (RespuestaDTO)TempData["RespuestaDTO"];
            ModelState.Clear();
            if (model != null)
                if (model.IdProducto != 0)
                    ViewBag.EsEdicion = true;
            if (Resp != null)
            {
                if (Resp.ModelStatesStandar != null)
                    foreach (var error in Resp.ModelStatesStandar.ToList())
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                if (Resp.MensajesError != null)
                    ViewBag.MensajeError = Resp.MensajesError[0];
            }
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn).SingleOrDefault().NombreComercial;
            return View(model);
        }
        public JsonResult GetBuscarCuentasCtbls(short idEmpresa)
        {
            tkn = Session["StringToken"].ToString();
            //var jsonSerialiser = new JavaScriptSerializer();
            //var contactInfo = jsonSerialiser.Serialize(CatalogoServicio.ListaCtaCtble(tkn).Select(x => x.IdEmpresa.Equals(idEmpresa)).ToList());
            var list = CatalogoServicio.ListaCtaCtble(tkn).Where(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
            var JsonInfo = JsonConvert.SerializeObject(list);
            return Json(JsonInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CrearProducto(ProductoDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.CrearProducto(model, tkn);
            if (respuesta.Exito)
                return RedirectToAction("Producto");
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Producto", model);
            }
        }
        public ActionResult EliminarProducto(short id)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminiarProducto(new ProductoDTO { IdProducto = id }, tkn);
            if (respuesta.Exito)
            {

                return RedirectToAction("Producto");
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Producto");
            }
        }
        public ActionResult EditarProducto(short? id, ProductoDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            if (id != null)
                return RedirectToAction("Producto", CatalogoServicio.ActivarEditarProducto(id.Value, tkn));
            else
            {
                var respuesta = CatalogoServicio.ModificarProducto(model, tkn);
                if (respuesta.Exito)
                    return RedirectToAction("Producto");
                else
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("Producto");
                }
            }
        }
        #endregion
        #region Proveedor
        public ActionResult Proveedores(int? page, ProveedorDTO model = null)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var Pagina = page ?? 1;
            ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn).ToPagedList(Pagina, 20);
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            ViewBag.TipoProveedores = CatalogoServicio.ListaTipoProveedor(tkn);
            ViewBag.Estados = CatalogoServicio.GetEstados(tkn);
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn).SingleOrDefault().NombreComercial;
            return View(model);
        }
        public ActionResult Proveedor(ProveedorDTO model = null)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            RespuestaDTO Resp = new RespuestaDTO();
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            ViewBag.TipoProveedores = CatalogoServicio.ListaTipoProveedor(tkn);
            ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn);
            ViewBag.Estados = CatalogoServicio.GetEstados(tkn);
            ViewBag.Paises = CatalogoServicio.GetPaises(tkn);
            ViewBag.Bancos = CatalogoServicio.ListaBanco(tkn);
            ViewBag.FormasPago = CatalogoServicio.ListaFormaPago(tkn);
            ViewBag.RegimenesFiscales = CatalogoServicio.ObtenerRegimenFiscal(tkn);
            ViewBag.TipoPersonas = CatalogoServicio.ObtenerTiposPersona(tkn);
            if (TempData["RespuestaDTO"] != null)
                Resp = (RespuestaDTO)TempData["RespuestaDTO"];
            ModelState.Clear();
            if (model != null)
                if (model.IdProveedor != 0)
                    ViewBag.EsEdicion = true;
            if (Resp != null)
            {
                if (Resp.ModelStatesStandar != null)
                    foreach (var error in Resp.ModelStatesStandar.ToList())
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                if (Resp.MensajesError != null)
                    ViewBag.MensajeError = Resp.MensajesError[0];
            }
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn).SingleOrDefault().NombreComercial;

            return View(model);
        }
        [HttpPost]
        public ActionResult CrearProveedor(ProveedorDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.CrearProveedor(model, tkn);
            if (respuesta.Exito)
                return RedirectToAction("Proveedores");
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Proveedor", model);
            }
        }

        public ActionResult EliminarProveedor(short id)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminiarProveedor(new ProveedorDTO { IdProveedor = id }, tkn);
            if (respuesta.Exito)
            {
                return RedirectToAction("Proveedores");
            }
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("Proveedor");
            }
        }
        public ActionResult EditarProveedor(short? id, ProveedorDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            if (id != null)
                return RedirectToAction("Proveedor", CatalogoServicio.ActivarEditarProveedor(id.Value, tkn));
            else
            {
                var respuesta = CatalogoServicio.ModificarProveedor(model, tkn);
                if (respuesta.Exito)
                    return RedirectToAction("Proveedores");
                else
                {
                    TempData["RespuestaDTO"] = respuesta;
                    return RedirectToAction("Proveedor", model);
                }
            }
        }
        #endregion      

    }
}
