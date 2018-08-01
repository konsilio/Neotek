using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Claims;
using Security.MainModule.Token_Service;

namespace Web.MainModule
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var _tok = Session["StringToken"].ToString();
            if (!IsPostBack)
            {
                if (Session["StringToken"] != null)
                {
                    Claim _autenticado = TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(_tok, "Autenticado");

                    if (Convert.ToBoolean(_autenticado.Value))
                    {
                        lblUsuario.Text = "Usuario: " + TokenGenerator.GetClaimsValueFromJwtSecurityToken(_tok, "NombreUsuario");
                        //Habilitar opciones segun el rol
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
    }
}