﻿using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class UsuariosController : Controller
    {
        string _tok = string.Empty;
        // GET: Usuarios
        public ActionResult Index()
        {
            string _tkn = Session["StringToken"].ToString();
            ViewBag.listaEmpresas = AutenticacionServicio.EmpresasLogin();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
            ViewBag.Usuarios = CatalogoServicio.ObtenerTodosUsuarios(_tkn);

            return View();
        }

        public ActionResult Nuevo()
        {
            _tok = Session["StringToken"].ToString();
            //Se obtienen los paises         
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tok);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tok);
            ViewBag.Empresas = CatalogoServicio.Empresas(_tok);
            ViewBag.IdUser = null;
            return View();
        }

        [HttpPost]
        public ActionResult GuardarUsuario(UsuarioDTO _ojUs)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.CrearUsuario(_ojUs, _tok);
            }

            return RedirectToAction("Index", _ojUs);
        }

        //vista ActualizaCredenciales-View
        public ActionResult ActualizaCredenciales(int id)
        {
            string _tkn = Session["StringToken"].ToString();
            ViewBag.IdUser = CatalogoServicio.ObtenerIdUsuario(id, _tkn);

            return View();
        }
        //guarda credenciales - operacion
        public ActionResult GuardarCredenciales(UsuarioDTO objUser)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.ActualizaCredencialesUser(objUser, _tok);
            }

            return RedirectToAction("Index", objUser);
        }

        //vista altas y bajas de Roles - View
        public ActionResult ActualizaRoles(int id)
        {
            string _tkn = Session["StringToken"].ToString();
            ViewBag.IdUser = CatalogoServicio.ObtenerIdUsuario(id, _tkn);
            ViewBag.CurrentRolUser = CatalogoServicio.ObtenerTodosUsuarios(_tkn);
            ViewBag.AllRoles = CatalogoServicio.ObtenerTodosRoles(_tkn);
            return View();
        }

        //guarda Roles asignado al usuario - operacion
        public ActionResult GuardarRol(UsuariosModel objUser)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.AgregarRolAlUsuario(objUser, _tok);
            }

            return RedirectToAction("Index", objUser);
        }

        //muestra vista para edicion de usuario seleccionado
        public ActionResult EditarUsuario(int id)
        {
            _tok = Session["StringToken"].ToString();
            //Se obtienen los paises         
            ViewBag.ListaPaises = CatalogoServicio.GetPaises(_tok);
            //Se obtienen los estados 
            ViewBag.ListaEstados = CatalogoServicio.GetEstados(_tok);
            ViewBag.Empresas = CatalogoServicio.Empresas(_tok);
            ViewBag.IdUser = CatalogoServicio.ObtenerIdUsuario(id, _tok);
            return View("Nuevo");
        }

        [HttpPost]
        public ActionResult GuardaEdicionUsuario(UsuarioDTO _Obj)
        {
            _tok = Session["StringToken"].ToString();
            if (ModelState.IsValid)
            {
                CatalogoServicio.ActualizaEdicionUsuario(_Obj, _tok);
            }

            return RedirectToAction("Index", _Obj);
        }

        public ActionResult BorrarUsuario(short id)
        {
            string _tkn = Session["StringToken"].ToString();
            CatalogoServicio.EliminaUsuarioSel(id, _tkn);

            return RedirectToAction("Index");
        }

        //BorrarRol

        [HttpPost]
        public ActionResult BorrarRol(int id)
        {
            //Empresa em = new Empresa();
            //string _tkn = Session["StringToken"].ToString();
            ////  ViewBag.Empresas = CatalogoServicio.FiltrarEmpresa(em, id, _tkn).Empresas.ToList();
            //CatalogoServicio.EliminaEmpresaSel(id, _tkn);

            return RedirectToAction("Index");
        }

        //[HttpPost]
        public ActionResult Buscar(UsuariosModel filterObj)
        {
            string _tkn = Session["StringToken"].ToString();
            CatalogoServicio.FiltrarBusquedaUsuario(filterObj, _tkn);
            return RedirectToAction("Index");
        }
    }
}