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
    public class ProductoController : Controller
    {
        string tkn = string.Empty;        
        #region Categorías Producto
        public ActionResult Categoria(CategoriaProductoDTO model = null)
        {
            RespuestaDTO Resp = new RespuestaDTO();
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            ViewBag.Categorias = CatalogoServicio.ListaCategorias(tkn);
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
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn).SingleOrDefault(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa(tkn))).NombreComercial;
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
        public ActionResult LineaProducto(LineaProductoDTO model = null)
        {
            RespuestaDTO Resp = new RespuestaDTO();
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            ViewBag.Lineas = CatalogoServicio.ListaLineasProducto(tkn);
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
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn).SingleOrDefault(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa(tkn))).NombreComercial;
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
        public ActionResult UnidadMedida(UnidadMedidaDTO model = null)
        {
            RespuestaDTO Resp = new RespuestaDTO();
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            ViewBag.Unidades = CatalogoServicio.ListaUnidadesMedida(tkn);
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
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn).SingleOrDefault(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa(tkn))).NombreComercial;
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
        public ActionResult Producto(ProductoDTO model = null)
        {
            RespuestaDTO Resp = new RespuestaDTO();
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            ViewBag.Productos = CatalogoServicio.ListaProductos(tkn);
            ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(TokenServicio.ObtenerIdEmpresa(tkn) ,tkn);
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
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn).SingleOrDefault(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa(tkn))).NombreComercial;
            return View(model);
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
                return RedirectToAction("Producto", new { respuesta, model });
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
    }
}
