using MVC.Presentacion.Controllers.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class OrdenCompraController : MainController
    {
        public ActionResult OrdenCompra()
        {
            return View();
        }
    }
}
