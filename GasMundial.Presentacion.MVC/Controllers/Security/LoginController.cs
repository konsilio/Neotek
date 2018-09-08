using GasMundial.Presentacion.MVC.Controllers.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasMundial.Presentacion.MVC.Controllers.Security
{
    public class LoginController : MainController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
    }
}