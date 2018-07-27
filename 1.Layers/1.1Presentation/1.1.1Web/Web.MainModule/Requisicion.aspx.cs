﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security.MainModule.Token_Service;
using System.Security.Claims;

namespace Web.MainModule
{
    public partial class Requisicion : System.Web.UI.Page
    {
        string _tok = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["StringToken"] != null)
                {
                    _tok = Session["StringToken"].ToString();
                    Claim _autenticado = TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(_tok, "Autenticado");
                    if (Convert.ToBoolean(_autenticado.Value))
                    {
                        //Habilitar opciones segun el rol
                    }
                    else
                        Salir();
                }
                else
                    Salir();
            }
        }
        private void Salir()
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}