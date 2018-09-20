using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;

namespace MVC.Presentacion.Controllers
{
    public class ClientesController : Controller
    {
        string _tok = string.Empty;
        // GET: Clientes
        public ActionResult Index()
        {
            string _tkn = Session["StringToken"].ToString();         
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona(_tkn);
            ViewBag.RegimenFiscal = CatalogoServicio.ObtenerRegimenFiscal(_tkn);
            ViewBag.Clientes = CatalogoServicio.ListaClientes(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            
            return View();
        }

        public ActionResult Nuevo()
        {
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona(_tkn);
            ViewBag.RegimenFiscal = CatalogoServicio.ObtenerRegimenFiscal(_tkn);
            ViewBag.Clientes = CatalogoServicio.ListaClientes(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            ViewBag.IdCliente = null;
            return View();
        }
        [HttpPost]
        public ActionResult GuardarCliente(ClientesDto _ojUs)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
               CatalogoServicio.CrearCliente(_ojUs, _tok);
            }

            return RedirectToAction("Index", _ojUs);
        }
        [HttpPost]
        public ActionResult GuardaEdicionCliente(ClientesDto _Obj)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
               // CatalogoServicio.ActualizaEdicionUsuario(_Obj, _tok);
            }

            return RedirectToAction("Index", _Obj);
        }
    }
}