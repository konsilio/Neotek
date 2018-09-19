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

        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #region categorías Producto
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
                return RedirectToAction("Categoria", CatalogoServicio.ActivarEditar(id.Value, tkn));
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
        public ActionResult LineaProducto()
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            return View();
        }
        [HttpPost]
        public ActionResult CrearLineaProducto(LineaProductoDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.CrearLineaProducto(model, tkn);
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
        #endregion
        #region Unidad Medida Producto

        public ActionResult UnidadMedida()
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            return View();
        }
        [HttpPost]
        public ActionResult CrearUnidadMedida(UnidadMedidaDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.CrearUnidadMedida(model, tkn);
            if (respuesta.Exito)
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
                return View("UnidadMedida");
            }
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
                ViewBag.MensajeError = respuesta.Mensaje;
                return View("UnidadMedida");
            }
        }
        #endregion
        #region Producto

        public ActionResult Producto()
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            return View();
        }
        [HttpPost]
        public ActionResult CrearProducto(ProductoDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            tkn = Session["StringToken"].ToString();
            var respuesta = CatalogoServicio.CrearProducto(model, tkn);
            if (respuesta.Exito)
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
                return View("Producto");
            }
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
                ViewBag.MensajeError = respuesta.Mensaje;
                return View("Producto");
            }
        }
        #endregion
    }
}
