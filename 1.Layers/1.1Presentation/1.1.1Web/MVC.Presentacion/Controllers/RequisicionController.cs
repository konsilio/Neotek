using MVC.Presentacion.Controllers.Shared;
using MVC.Presentacion.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using MVC.Presentacion.Models.Requisicion;

namespace MVC.Presentacion.Controllers
{
    public class RequisicionController : MainController
    {
        public ActionResult Requisicion(RequisicionModel model = null)
        {
            if (Session["StringToken"] != null)
                return View(RequisicionServicio.InitRequisicion(Session["StringToken"].ToString()));
            else
                return View("Index");
           
        }
        public ActionResult Filtrar(RequisicionesModel model = null)
        {
            if (Session["StringToken"] != null)
                return View();
            else
                return View("Index");
        }        
        public ActionResult Requisiciones()
        {
            if (Session["StringToken"] != null)
                return View(RequisicionServicio.InitRequisiciones(Session["StringToken"].ToString()));
            else
                return View("Index");            
        }
        public ActionResult Agregar(RequisicionProductoNuevoDTO prod)
        {
            return View("Requisicion", RequisicionServicio.AgregarProducto(prod, Session["StringToken"].ToString()));
        }
    }
}
