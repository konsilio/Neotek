using GasMundial.Presentacion.MVC.Controllers.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasMundial.Presentacion.MVC.Controllers.Home
{
    public class HomeController : MainController
    {
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}