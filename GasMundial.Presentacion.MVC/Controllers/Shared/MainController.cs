using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasMundial.Presentacion.MVC.Controllers.Shared
{
    public class MainController : Controller
    {
        #region Propiedades

      //  public Sesion UserApp { get; set; }

        #endregion

        #region Constructor

        protected string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

                return sw.GetStringBuilder().ToString();
            }
        }
                

        #endregion

        #region Session Manage

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // vars
            bool SessionExpired = false;
            bool IsLogin = false;
            bool IsExternalProcess = false;
            bool IsChangePassword = false;

            //verify if is login
            IsLogin = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.Equals("Login");
            IsExternalProcess = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.Equals("Authorization");

            //there's no session
            if (filterContext.HttpContext.Session == null)
            {
                SessionExpired = true;
            }

            //session exists and there is not user
            if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session["CurrentUser"] == null)
            {
                SessionExpired = true;
            }
            else
            {
              //  this.UserApp = (Sesion)Session["CurrentUser"];
            }

            //when session had expired and the method is not the redirect
            if (SessionExpired && !IsLogin && !IsExternalProcess && !IsChangePassword)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 302;
                    filterContext.Result = new EmptyResult();
                    return;
                }
                else
                {
                    filterContext.Result = RedirectToAction("Index", "Login");
                    return;
                }
            }
            else
            {
                //otherwise continue with action
                base.OnActionExecuting(filterContext);
            }
        }

        public ActionResult Menu()
        {
            string view = "_Menu";
            if (Session["CurrentUser"] != null)
            {
                //Sesion current = ((Sesion)(Session["CurrentUser"]));
                return PartialView(view, null);
            }
            else
            {
                return PartialView(view, null);
            }
        }

        #endregion
    }
}