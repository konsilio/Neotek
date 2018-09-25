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
            ViewBag.Clientes = CatalogoServicio.ListaClientes(0,"","", _tkn);

            return View();
        }

        public ActionResult Nuevo()
        {
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona(_tkn);
            ViewBag.RegimenFiscal = CatalogoServicio.ObtenerRegimenFiscal(_tkn);
            ViewBag.Clientes = CatalogoServicio.ListaClientes(0, "", "", _tkn);
            ViewBag.IdCliente = null;
            return View();
        }
        [HttpPost]
        public ActionResult GuardarCliente(ClientesModel _ojUs)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.CrearCliente(_ojUs, _tok);
            }

            return RedirectToAction("Index", _ojUs);
        }
        public ActionResult EditarCliente(int id)
       {
            string _tkn = Session["StringToken"].ToString();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona(_tkn);
            ViewBag.RegimenFiscal = CatalogoServicio.ObtenerRegimenFiscal(_tkn);
            ViewBag.Clientes = CatalogoServicio.ListaClientes(0, "", "", _tkn);
            ViewBag.IdCliente = CatalogoServicio.ListaClientes(id, "", "", _tkn);
            return View("Nuevo");
        }

        [HttpPost]
        public ActionResult GuardaEdicionCliente(ClientesDto _Obj)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.ModificarCliente(_Obj, _tok);
            }

            return RedirectToAction("Index", _Obj);
        }


        public ActionResult BorrarClientes(ClientesModel _Obj, int id)
        {
            string _tkn = Session["StringToken"].ToString();
            CatalogoServicio.EliminarCliente(id, _tkn);

            ViewBag.Clientes = CatalogoServicio.ListaClientes(0, "", "", _tkn);
            return RedirectToAction("Index", _Obj);
        }


        public ActionResult EditarLocaciones(int id)
        {
            string _tkn = Session["StringToken"].ToString();
            ViewBag.IdCliente = CatalogoServicio.ListaClientes(id, "", "", _tkn);
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tkn);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tkn);
            //llenar locaciones
            ViewBag.Locaciones = CatalogoServicio.ObtenerLocaciones(id, _tkn);
            List<ClienteLocacionMod> _lst = CatalogoServicio.ObtenerLocaciones(id, _tkn);
            return View(_lst);
        }

        [HttpPost]
        public ActionResult GuardarLocaciones(ClienteLocacionMod _Obj)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.RegistraLocaciones(_Obj, _tok);
            }

            return RedirectToAction("Index", _Obj);
        }

        //[HttpPost]
        public ActionResult EditarClienteLoc(ClienteLocacionMod _ObjModel, int id, short idOrden)
        {
            string _tkn = Session["StringToken"].ToString();
            ViewBag.IdCliente = CatalogoServicio.ListaClientes(id, "", "", _tkn);
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tkn);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tkn);
            ViewBag.Locaciones = CatalogoServicio.ObtenerModel(idOrden, id, _tkn);
            return View();

        }

        [HttpPost]
        public ActionResult ActualizarLocacion(ClienteLocacionMod _ObjModel)//, int id, short idOrden
        {
            string _tkn = Session["StringToken"].ToString();
           
            CatalogoServicio.ModificarClienteLocacion(_ObjModel, _tkn);          
            return RedirectToAction("EditarLocaciones", "Clientes", new { id = _ObjModel.IdCliente });
        }
        public ActionResult BorrarClienteLoc(ClienteLocacionMod _ObjModel, int id, short idOrden)
        {
            string _tkn = Session["StringToken"].ToString();
            //CatalogoServicio.EliminarCliente(id, _tkn);
            _ObjModel = CatalogoServicio.ObtenerModel(idOrden, id, _tkn);
            CatalogoServicio.EliminarClienteLocacion(_ObjModel, _tkn);

            return RedirectToAction("EditarLocaciones", "Clientes", new { id = _ObjModel.IdCliente });
        }

        public ActionResult Buscar(ClientesModel filterObj)
        {
            string _tkn = Session["StringToken"].ToString();
          
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.TipoPersona = CatalogoServicio.ObtenerTiposPersona(_tkn);
            ViewBag.RegimenFiscal = CatalogoServicio.ObtenerRegimenFiscal(_tkn);
            ViewBag.Clientes = CatalogoServicio.ListaClientes(0, filterObj.Rfc, filterObj.RazonSocial, _tkn);
 
            return View("Index", filterObj);
        }
    }
}