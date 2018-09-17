using MVC.Presentacion.App_Code;
using MVC.Presentacion.Controllers.Shared;
using MVC.Presentacion.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class CentroCostoController : MainController
    {
        string tkn = string.Empty;
        public ActionResult CentroCosto()
        {

            if (Session["StringToken"] != null)
            {
                tkn = Session["StringToken"].ToString();
                ViewBag.TiposCentrosCosto = CatalogoServicio.BuscarTipoCentrosCosto(tkn);
                ViewBag.EstacionesCarburacion = CatalogoServicio.GetListaEstacionCarburacion(tkn);
                ViewBag.UnidadAlmacenGas = CatalogoServicio.GetListaUnidadAlmcenGas(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
                ViewBag.EquipoTransporte = CatalogoServicio.GetListaEquiposTransporte(tkn);
                return View(CatalogoServicio.InitCentroCosto(tkn));
            }
            else
            {
                return View();
            }
        }
        public ActionResult Crear(CentroCostoModel model)
        {

            return View();
        }
        public ActionResult Modificar()
        {
            return View();
        }
        public ActionResult Eliminar()
        {
            return View();
        }
    }
}
