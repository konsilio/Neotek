using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class EquipoTransporteController : Controller
    {
        string _tkn = string.Empty;
        // GET: EquipoTransporte
        public ActionResult Index(string placa=null,string vehiculo = null, string msj = null)
        {
            return View();
        }
    }
}