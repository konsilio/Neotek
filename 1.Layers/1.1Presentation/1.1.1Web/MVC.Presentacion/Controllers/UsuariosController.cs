using MVC.Presentacion.App_Code;
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
        // GET: Usuarios
        public ActionResult Index()
        {
            string _tkn = Session["StringToken"].ToString();
            ViewBag.listaEmpresas = AutenticacionServicio.EmpresasLogin();
            ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
       //     ViewBag.Usuarios = CatalogoServicio.ListaUsuarios(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);
            ViewBag.Usuarios = CatalogoServicio.ObtenerTodosUsuarios(_tkn);


            
            return View();
        }

        public ActionResult Nuevo()
        {
          
            return View();
        }

        //vista ActualizaCredenciales
        public ActionResult ActualizaCredenciales(int id)
        {
           // ViewBag.Usuarios = CatalogoServicio.ObtenerIdUsuario(TokenServicio.ObtenerIdEmpresa(_tkn), _tkn);

            
            return View();
        }

        public ActionResult GuardarCredenciales()
        {
            return View();
        }

    }
}