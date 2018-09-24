using MVC.Presentacion.App_Code;
using MVC.Presentacion.Controllers.Shared;
using MVC.Presentacion.Models.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class OrdenCompraController : MainController
    {
        public ActionResult OrdenCompra(int id)
        {
            if (Session["StringToken"] != null)
            {
                string tkn = Session["StringToken"].ToString();
                ViewBag.CuentasContables = CatalogoServicio.ListaCtaCtble(tkn).Select(cc => new SelectListItem { Value = cc.IdCuentaContable.ToString(), Text = cc.Descripcion }).ToList();
                ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn).Select(p => new SelectListItem { Value = p.IdProveedor.ToString(), Text = p.NombreComercial }).ToList();
                ViewBag.IVAs = CatalogoServicio.ListaIVA();
                ViewBag.IEPs = CatalogoServicio.ListaIEPS();
                return View(OrdenCompraServicio.InitOrdenCompra(id, tkn));
            }
            else
                return View("Index", "Home");
        }
        public ActionResult CrearOrdenCompra(OrdenCompraModel model)
        {
            if (Session["StringToken"] != null)
            {
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
            else
                return View("Index", "Home");
        }
        public ActionResult Ordenes()
        {
            if (Session["StringToken"] != null)
            {
                string tkn = Session["StringToken"].ToString();
                ViewBag.EsAdminCentral = TokenServicio.ObtenerEsAdministracionCentral(tkn);
                ViewBag.Empresas = CatalogoServicio.Empresas(tkn);
                ViewBag.Proveedores = CatalogoServicio.ListaProveedores(tkn);
                ViewBag.Estatus = OrdenCompraServicio.ListaEstatus(tkn);
                return View(OrdenCompraServicio.InitOrdenesCompra(tkn));
            }
            else
                return View("Inicio", "Home");
        }
    }
}
