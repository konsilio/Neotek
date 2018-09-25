using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class PuntoVentaController : Controller
    {
        // GET: PuntoVenta
        public ActionResult Index()//, short id = 0
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            ViewBag.EsSuperUser = TokenServicio.ObtenerEsSuperUsuario(_tkn);
            if (ViewBag.EsSuperUser)
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn);
                ViewBag.ListaPV = CatalogoServicio.ListaPuntosVenta(_tkn);

            }
            else
            {
                ViewBag.Empresas = CatalogoServicio.Empresas(_tkn).SingleOrDefault(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa(_tkn))).NombreComercial;
                ViewBag.ListaPV = CatalogoServicio.ListaPuntosVenta(_tkn);
            }
            //if (id != 0)
            //{
            //    var lst = CatalogoServicio.ListaUsuarios(id, _tkn);
            //    if (lst.Count >= 1)
            //    {
            //        ViewBag.Usuarios = lst;
            //    }

            //}

            if (TempData["RespuestaDTO"] != null)
            {
                ViewBag.MessageExito = TempData["RespuestaDTO"];
            }
            if (TempData["RespuestaDTOError"] != null)
            {
                ViewBag.MessageError = TempData["RespuestaDTOError"];
            }

            ViewBag.MessageError = TempData["RespuestaDTOError"];
            return View();
        }

        public ActionResult AsignarChofer(PuntoVentaModel model, short idE, int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home", AutenticacionServicio.InitIndex(new Models.Seguridad.LoginModel()));
            string _tkn = Session["StringToken"].ToString();
            var lst = CatalogoServicio.ListaUsuarios(idE, _tkn);
            if (lst.Count >= 1)
            {
                ViewBag.Usuarios = lst;
            }

            return View("Index", new { model });
        }
    }
}