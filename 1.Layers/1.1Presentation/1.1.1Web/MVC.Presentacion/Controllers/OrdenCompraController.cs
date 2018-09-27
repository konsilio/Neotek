using MVC.Presentacion.App_Code;
using MVC.Presentacion.Controllers.Shared;
using MVC.Presentacion.Models.OrdenCompra;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Newtonsoft.Json;
using MVC.Presentacion.Models.Seguridad;

namespace MVC.Presentacion.Controllers
{
    public class OrdenCompraController : MainController
    {
        string tkn = string.Empty;
        public ActionResult OrdenCompra(int? id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            int idOc = id ?? 0;
            var model = OrdenCompraServicio.InitOrdenCompra(idOc, tkn);
            ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn).Select(cc => new SelectListItem { Value = cc.IdCuentaContable.ToString(), Text = cc.Descripcion }).ToList();
            ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn).Select(p => new SelectListItem { Value = p.IdProveedor.ToString(), Text = p.NombreComercial }).ToList();
            ViewBag.IVAs = CatalogoServicio.ListaIVA();
            ViewBag.IEPs = CatalogoServicio.ListaIEPS();
            return View(model);
        }
        public ActionResult OrdenCompraAutorizacion(int? id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            int idOc = id ?? 0;
            var model = OrdenCompraServicio.BuscarOrdenCompra(idOc, tkn);
            ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn).Select(cc => new SelectListItem { Value = cc.IdCuentaContable.ToString(), Text = cc.Descripcion }).ToList();
            ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn).Select(p => new SelectListItem { Value = p.IdProveedor.ToString(), Text = p.NombreComercial }).ToList();
            ViewBag.IVAs = CatalogoServicio.ListaIVA();
            ViewBag.IEPs = CatalogoServicio.ListaIEPS();
            ViewBag.Estatus = model.IdOrdenCompraEstatus;
            return View(model);
        }
        public ActionResult CrearOrdenCompra(OrdenCompraModel model)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            var Respuesta = OrdenCompraServicio.GenerarOrdenCompra(model, Session["StringToken"].ToString());
            if (Respuesta.Exito)
            {
                string tkn = Session["StringToken"].ToString();
                ViewBag.EsAdminCentral = TokenServicio.ObtenerEsAdministracionCentral(tkn);
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
                ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn);
                ViewBag.Estatus = OrdenCompraServicio.ListaEstatus(tkn);
                return View(OrdenCompraServicio.InitOrdenesCompra(tkn));
            }
            else
            {
                string tkn = Session["StringToken"].ToString();
                ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn).Select(cc => new SelectListItem { Value = cc.IdCuentaContable.ToString(), Text = cc.Descripcion }).ToList();
                ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn).Select(p => new SelectListItem { Value = p.IdProveedor.ToString(), Text = p.NombreComercial }).ToList();
                ViewBag.IVAs = CatalogoServicio.ListaIVA();
                ViewBag.IEPs = CatalogoServicio.ListaIEPS();
                return View(model);
            }
        }
        public ActionResult Ordenes(int? pageO, int? pageR)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ViewBag.EsAdminCentral = TokenServicio.ObtenerEsAdministracionCentral(tkn);
            ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
            ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn);
            ViewBag.Estatus = OrdenCompraServicio.ListaEstatus(tkn);
            var model = OrdenCompraServicio.InitOrdenesCompra(tkn);
            if (pageO == null) pageO = 1;
            if (pageR == null) pageR = 1;
            ViewBag.Ordenes = model.OrdenesCompra.ToPagedList(pageO.Value, 20);
            ViewBag.Requisiciones = model.Requisiciones.ToPagedList(pageR.Value, 20);
            return View();
        }
        public ActionResult Autorizar(int? id, OrdenCompraDTO model)
        {
            if (Session["StringToken"] == null) return View(AutenticacionServicio.InitIndex(new LoginModel()));
            tkn = Session["StringToken"].ToString();
            int IdOC = id ?? 0;

            var respuesta = OrdenCompraServicio.AutorizarOrdenCompra(model, tkn);
            if (respuesta.Exito)
                return RedirectToAction("Ordenes");
            else
            {
                TempData["RespuestaDTO"] = respuesta;
                return RedirectToAction("OrdenCompraAutorizacion", new { id = model.IdOrdenCompra });
            }
        }
    }
}
