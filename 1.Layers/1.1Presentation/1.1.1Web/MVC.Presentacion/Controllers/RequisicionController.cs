using MVC.Presentacion.Controllers.Shared;
using MVC.Presentacion.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using MVC.Presentacion.Models.Requisicion;

namespace MVC.Presentacion.Controllers
{
    public class RequisicionController : MainController
    {
        public ActionResult Requisicion(RequisicionModel model = null)
        {
            if (Session["StringToken"] != null)
            {
                if (model != null)
                {
                    
                }
                else
                    ViewBag.btnCrear = "Crear";
                string _tkn = Session["StringToken"].ToString();
                ViewBag.btnCrear = "Crear";
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);        
                return View(RequisicionServicio.InitRequisicion(_tkn));
            }
            else
                return View("Index");
        }
        public ActionResult RequisicionRevision(int? idRequisicon)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Filtrar(RequisicionesModel model = null)
        {
            if (Session["StringToken"] != null)
                return View();
            else
                return View("Index", "Home");
        }        
        public ActionResult Requisiciones()
        {
            if (Session["StringToken"] != null)
                return View(RequisicionServicio.InitRequisiciones(Session["StringToken"].ToString()));
            else
                return View("Index", "Home");            
        }
        
        public ActionResult Agregar(RequisicionModel model)
        {
            if (TempData["ListProductosRequisicion"] != null)            
                model.RequisicionProductos = (List<RequisicionProductoNuevoDTO>)TempData["ListProductosRequisicion"];            
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            var newModel = RequisicionServicio.AgregarProducto(model, Session["StringToken"].ToString());
            TempData["ListProductosRequisicion"] = model.RequisicionProductos;
            return View("Requisicion", newModel);
        }
        public ActionResult Editar(RequisicionModel model)
        {
            if (TempData["ListProductosRequisicion"] != null)
                model.RequisicionProductos = (List<RequisicionProductoNuevoDTO>)TempData["ListProductosRequisicion"];
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);

            return View("Requisicion", model);
        }
        public ActionResult CrearRequisicion(RequisicionModel model)
        {
            if (TempData["ListProductosRequisicion"] != null)
                model.RequisicionProductos = (List<RequisicionProductoNuevoDTO>)TempData["ListProductosRequisicion"];
            string _tkn = Session["StringToken"].ToString();
            var respuesta = RequisicionServicio.GuardarRequisicion(model, _tkn);
            if (respuesta.Exito)
            {
                return View("Requisiciones", "Requisicion");
            }
            else
            {
                ViewBag.btnCrear = "Crear";
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
                ViewBag.MensajeError(respuesta.Mensaje);
                TempData["ListProductosRequisicion"] = model.Productos;
                return View("Requisicion", model);

            }            
        }
        public ActionResult RequisicionChecarRevicion(RequisicionModel model, string cbRevision, bool checkResp = false)
        {
            if (TempData["ListProductosRevicion"] != null)
                model.RequisicionRevicion.ListaProductos = ((List<RequisicionProductoRevisionDTO>)TempData["ListProductosRevicion"]);

            return View("Requisicion", model);
        }
    }
}
