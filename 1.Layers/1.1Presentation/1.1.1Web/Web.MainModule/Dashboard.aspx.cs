﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.MainModule
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensaje.Text= ((Seguridad.Model.RespuestaAutenticacionDto)Session["RespuestaAutenticacionDto"]).Mensaje;
        }
    }
}