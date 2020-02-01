using DevExpress.Web.Mvc;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MVC.Presentacion.Controllers
{
    public class ProductoController : Controller
    {
        string tkn = string.Empty;
        #region Categorías Producto
        public ActionResult Categoria(CategoriaProductoDTO model = null)
        {
            TokenServicio.ClearTemp(TempData);

            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ModelState.Clear();
            TempData["DataSourceCategorias"] = CatalogoServicio.ListaCategorias(tkn).ToList();
            TempData.Keep("DataSourceCategorias");
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            if (TempData["RespuestaDTO"] != null)
            {
                var resp = (RespuestaDTO)TempData["RespuestaDTO"];
                if (resp.Exito)
                    ViewBag.Msj = resp.Mensaje;
                else
                    ViewBag.MensajeError = Validar(resp);
            }
            if (model != null && model.IdCategoria != 0) ViewBag.EsEdicion = true;
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
            TempData["RespuestaDTO"] = respuesta;
            if (respuesta.Exito)
                return RedirectToAction("Categoria");
            else
                return RedirectToAction("Categoria", new { respuesta, model });
        }
        public ActionResult EliminarCategoria(short id)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminiarCategoriaProducto(new CategoriaProductoDTO { IdCategoria = id }, tkn);

            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Categoria");
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
                TempData["RespuestaDTO"] = respuesta;
                if (respuesta.Exito)
                    return RedirectToAction("Categoria");
                else
                    return RedirectToAction("Categoria", CatalogoServicio.ActivarEditarCategoria(id.Value, tkn));

            }
        }
        #endregion
        #region Linea Producto
        public ActionResult LineaProducto(LineaProductoDTO model = null)
        {
            TokenServicio.ClearTemp(TempData);

            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ModelState.Clear();
            TempData["DataSourceLineas"] = CatalogoServicio.ListaLineasProducto(tkn).ToList();
            TempData.Keep("DataSourceLineas");
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            if (TempData["RespuestaDTO"] != null)
            {
                var resp = (RespuestaDTO)TempData["RespuestaDTO"];
                if (resp.Exito)
                    ViewBag.Msj = resp.Mensaje;
                else
                    ViewBag.MensajeError = Validar(resp);
            }
            if (model != null && model.IdProductoLinea != 0) ViewBag.EsEdicion = true;
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
            TempData["RespuestaDTO"] = respuesta;
            if (respuesta.Exito)
                return RedirectToAction("LineaProducto");
            else
                return RedirectToAction("LineaProducto", model);
        }
        public ActionResult EliminarLineaProducto(short id)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminiarLineaProducto(new LineaProductoDTO { IdProductoLinea = id }, tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("LineaProducto");
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
                TempData["RespuestaDTO"] = respuesta;
                if (respuesta.Exito)
                    return RedirectToAction("LineaProducto");
                else
                    return RedirectToAction("LineaProducto", CatalogoServicio.ActivarEditarLineaProducto(model.IdProductoLinea, tkn));
            }
        }
        #endregion
        #region Unidada de Medida
        public ActionResult UnidadMedida(UnidadMedidaDTO model = null)
        {
            TokenServicio.ClearTemp(TempData);

            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ModelState.Clear();
            TempData["DataSourceUnidades"] = CatalogoServicio.ListaUnidadesMedida(tkn).ToList();
            TempData.Keep("DataSourceUnidades");
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            if (TempData["RespuestaDTO"] != null)
            {
                var resp = (RespuestaDTO)TempData["RespuestaDTO"];
                if (resp.Exito)
                    ViewBag.Msj = resp.Mensaje;
                else
                    ViewBag.MensajeError = Validar(resp);
            }
            if (model != null && model.IdUnidadMedida != 0) ViewBag.EsEdicion = true;
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
            TempData["RespuestaDTO"] = respuesta;
            if (respuesta.Exito)
                return RedirectToAction("UnidadMedida");
            else
                return RedirectToAction("UnidadMedida", model);
        }
        public ActionResult EliminarUnidadMedida(short id)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminiarUnidadMedida(new UnidadMedidaDTO { IdUnidadMedida = id }, tkn);
            TempData["RespuestaDTO"] = respuesta;
            if (respuesta.Exito)
                return RedirectToAction("UnidadMedida");
            else
                return RedirectToAction("UnidadMedida");
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
                TempData["RespuestaDTO"] = respuesta;
                if (respuesta.Exito)
                    return RedirectToAction("UnidadMedida");
                else
                    return RedirectToAction("UnidadMedida", CatalogoServicio.ActivarEditarUnidadMedida(model.IdUnidadMedida, tkn));
            }
        }
        #endregion
        #region Producto
        public ActionResult Producto(short? idempresa, ProductoDTO model = null)
        {
            TokenServicio.ClearTemp(TempData);

            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var _idEmpresa = idempresa ?? 0;
            if (!_idEmpresa.Equals(0))
            {
                TempData["DataSourceProductos"] = CatalogoServicio.ListaProductos(tkn).Where(x => x.IdEmpresa.Equals(_idEmpresa)).ToList();
                TempData.Keep("DataSourceProductos");
            }
            else
            {
                TempData["DataSourceProductos"] = CatalogoServicio.ListaProductos(tkn);
                TempData.Keep("DataSourceProductos");
            }
            if (idempresa != null)
                ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn).Where(x => x.IdEmpresa.Equals(idempresa)).ToList();
            else
                ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn);
            ViewBag.IdEmpresa = _idEmpresa;
            ViewBag.Categorias = CatalogoServicio.ListaCategorias(tkn);
            ViewBag.LineasProducto = CatalogoServicio.ListaLineasProducto(tkn);
            ViewBag.UnidadesMedida = CatalogoServicio.ListaUnidadesMedida(tkn);
            ViewBag.UnidadesMedida2 = CatalogoServicio.ListaUnidadesMedida(tkn);
            ViewBag.EsAdmin = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            if (TempData["RespuestaDTO"] != null)
            {
                var resp = (RespuestaDTO)TempData["RespuestaDTO"];
                if (resp.Exito)
                    ViewBag.Msj = resp.Mensaje;
                else
                    ViewBag.MensajeError = Validar(resp);
            }
            if (model != null && model.IdProducto != 0) ViewBag.EsEdicion = true;
            if (ViewBag.EsAdmin)
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            else
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn).SingleOrDefault().NombreComercial;

            return View(model);
        }
        public JsonResult GetBuscarCuentasCtbls(short idEmpresa)
        {
            tkn = Session["StringToken"].ToString();
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
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Producto", model);
        }
        public ActionResult EliminarProducto(short id)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminiarProducto(new ProductoDTO { IdProducto = id }, tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Producto");
        }
        public ActionResult EditarProducto(int? id, ProductoDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            if (id != null)
                return RedirectToAction("Producto", CatalogoServicio.ActivarEditarProducto(id.Value, tkn));
            else
            {
                var respuesta = CatalogoServicio.ModificarProducto(model, tkn);
                TempData["RespuestaDTO"] = respuesta;
                if (respuesta.Exito)
                    return RedirectToAction("Producto");
                else
                    return RedirectToAction("Producto", CatalogoServicio.ActivarEditarProducto(model.IdProducto, tkn));

            }
        }
        #endregion
        #region Proveedor
        public ActionResult Proveedores(ProveedorDTO model = null)
        {
            TokenServicio.ClearTemp(TempData);

            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            TempData["DataSourceProveedores"] = CatalogoServicio.ListaProveedores(tkn).ToList();
            TempData.Keep("DataSourceProveedores");
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
            {
                var resp = (RespuestaDTO)TempData["RespuestaDTO"];
                if (resp.Exito)
                    ViewBag.Msj = resp.Mensaje;
                else
                    ViewBag.MensajeError = Validar(resp);
            }
            if (model != null) if (model.IdProveedor != 0) ViewBag.EsEdicion = true;
            ViewBag.MensajeError = Validar(Resp);
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
            TempData["RespuestaDTO"] = respuesta;
            if (respuesta.Exito)
                return RedirectToAction("Proveedores");
            else
                return RedirectToAction("Proveedor", model);
        }
        public ActionResult EliminarProveedor(short id)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.EliminiarProveedor(new ProveedorDTO { IdProveedor = id }, tkn);
            TempData["RespuestaDTO"] = respuesta;
            return RedirectToAction("Proveedor");
        }
        public ActionResult EditarProveedor(int? id, ProveedorDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            if (id != null)
                return RedirectToAction("Proveedor", CatalogoServicio.ActivarEditarProveedor(id.Value, tkn));
            else
            {
                var respuesta = CatalogoServicio.ModificarProveedor(model, tkn);
                TempData["RespuestaDTO"] = respuesta;
                if (respuesta.Exito)
                    return RedirectToAction("Proveedores");
                else
                    return RedirectToAction("Proveedor", CatalogoServicio.ActivarEditarProveedor(model.IdProveedor, tkn));
            }
        }
        #endregion
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

        public ActionResult ComboBoxPartial()
        {
            return PartialView("_ComboBoxPartial");
        }

        #region GridDEvExpress CallBack

        public ActionResult CB_Productos()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            List<ProductoDTO> model = new List<ProductoDTO>();
            if (TempData["DataSourceProductos"] != null)
            {
                model = (List<ProductoDTO>)TempData["DataSourceProductos"];
                TempData["DataSourceProductos"] = model;
                //TempData.Keep("DataSourceProductos");
            }
            return PartialView("_CB_Productos", model);
        }

        public ActionResult CB_Lineas()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            List<LineaProductoDTO> model = new List<LineaProductoDTO>();
            if (TempData["DataSourceLineas"] != null)
            {
                model = (List<LineaProductoDTO>)TempData["DataSourceLineas"];
                TempData["DataSourceLineas"] = model;
                //TempData.Keep("DataSourceLineas");
            }
            return PartialView("_CB_Lineas", model);
        }

        public ActionResult CB_Categorias()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            List<CategoriaProductoDTO> model = new List<CategoriaProductoDTO>();
            if (TempData["DataSourceCategorias"] != null)
            {
                model = (List<CategoriaProductoDTO>)TempData["DataSourceCategorias"];
                TempData["DataSourceCategorias"] = model;
                //TempData.Keep("DataSourceCategorias");
            }
            return PartialView("_CB_Categorias", model);
        }

        public ActionResult CB_Unidades()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            List<UnidadMedidaDTO> model = new List<UnidadMedidaDTO>();
            if (TempData["DataSourceUnidades"] != null)
            {
                model = (List<UnidadMedidaDTO>)TempData["DataSourceUnidades"];
                TempData["DataSourceUnidades"] = model;
                // TempData.Keep("DataSourceUnidades");
            }
            return PartialView("_CB_Unidades", model);
        }



        public ActionResult CB_Proveedores()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            List<ProveedorDTO> model = new List<ProveedorDTO>();
            if (TempData["DataSourceProveedores"] != null)
            {
                model = (List<ProveedorDTO>)TempData["DataSourceProveedores"];
                TempData["DataSourceProveedores"] = model;
                // TempData.Keep("DataSourceProveedores");
            }
            return PartialView("_CB_Proveedores", model);
        }


        #endregion


    }
}
