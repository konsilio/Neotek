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
        public ActionResult Requisiciones()
        {
            if (Session["StringToken"] != null)
                return View(RequisicionServicio.InitRequisiciones(Session["StringToken"].ToString()));
            else
                return View("Index", "Home");
        }
        public ActionResult Requisicion(RequisicionModel model = null)
        {
            if (Session["StringToken"] != null)
            {
                string _tkn = Session["StringToken"].ToString();
                ViewBag.EsNueva = true;
                ViewBag.EsAdminCentral = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
                return View(RequisicionServicio.InitRequisicion(_tkn));
            }
            else
                return View("Index", "Home");
        }
        public ActionResult RequisicionAlternativa(int id, byte estatus)
        {
            if (Session["StringToken"] != null)
            {
                string _tkn = Session["StringToken"].ToString();
                var model = RequisicionServicio.RquisicionAlternativa(id, estatus, _tkn);
                ViewBag.EsNueva = false;
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
                if (model.RequisicionEstatus.Equals(RequisicionEstatusEnum.Creada))
                {
                    ViewBag.reqOpinion = model.RequisicionRevision.OpinionAlmacen;
                    ViewBag.btnCrear = "Finalizar";
                    ViewBag.formactionBtnCrear = "Revicion";
                }
                else
                {
                    ViewBag.reqOpinion = model.RequisicionRevision.OpinionAlmacen;
                    ViewBag.btnCrear = "Autorizar";
                    ViewBag.formactionBtnCrear("Autorizar");
                }
                return View("Requisicion", model);
            }
            else
                return View("Index", "Home");
        }
        public ActionResult Revision(RequisicionModel model)
        {
            if (Session["StringToken"] != null)
            {
                string _tkn = Session["StringToken"].ToString();
                var respuesta = RequisicionServicio.FinalizarRevision(model, _tkn);
                if (respuesta.Exito)
                    return View("Requisiciones", RequisicionServicio.InitRequisiciones(_tkn));
                else
                {
                    return View();
                }
            }
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
            ViewBag.EsNueva = true;
            ViewBag.EsAdminCentral = TokenServicio.ObtenerEsAdministracionCentral(_tkn);
            var newModel = RequisicionServicio.AgregarProducto(model, Session["StringToken"].ToString());
            TempData["ListProductosRequisicion"] = model.RequisicionProductos;
            return View("Requisicion", newModel);
        }
        public ActionResult Editar(RequisicionModel model, int id)
        {
            if (Session["StringToken"] != null)
            {
                string _tkn = Session["StringToken"].ToString();

                var newModel = RequisicionServicio.ActivarEditar(model, id, (List<RequisicionProductoNuevoDTO>)TempData["ListProductosRequisicion"], _tkn);
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
                ViewBag.EsNueva = true;
                TempData["ListProductosRequisicion"] = newModel.RequisicionProductos;
                return View("Requisicion", model);
            }
            else
                return View("Index", "Home");
        }
        public ActionResult Borrar(RequisicionModel model, int id)
        {
            if (Session["StringToken"] != null)
            {
                string _tkn = Session["StringToken"].ToString();

                var newModel = RequisicionServicio.ActivarBorrar(model, id, (List<RequisicionProductoNuevoDTO>)TempData["ListProductosRequisicion"], _tkn);
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
                ViewBag.EsNueva = true;
                TempData["ListProductosRequisicion"] = newModel.RequisicionProductos;
                return View("Requisicion", model);
            }
            else
                return View("Index", "Home");
        }
        public ActionResult CrearRequisicion(RequisicionModel model)
        {
            if (TempData["ListProductosRequisicion"] != null)
                model.RequisicionProductos = (List<RequisicionProductoNuevoDTO>)TempData["ListProductosRequisicion"];
            string _tkn = Session["StringToken"].ToString();
            var respuesta = RequisicionServicio.GuardarRequisicion(model, _tkn);
            if (respuesta.Exito)
                return View("Requisiciones", RequisicionServicio.InitRequisiciones(_tkn));
            else
            {
                ViewBag.EsNueva = true;
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
                ViewBag.MensajeError(respuesta.MensajesError[0]);
                TempData["ListProductosRequisicion"] = model.Productos;
                return View("Requisicion", model);
            }
        }
        public ActionResult CrearCancelar(RequisicionModel model)
        {
            return View();
        }
        public ActionResult RequisicionChecarRevicion(RequisicionModel model, string cbRevision, bool checkResp = false)
        {
            if (TempData["ListProductosRevicion"] != null)
                model.RequisicionRevision.ListaProductos = ((List<RequisicionProductoRevisionDTO>)TempData["ListProductosRevicion"]);

            return View("Requisicion", model);
        }

    }
}
