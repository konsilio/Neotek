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
         
            if (!IsPostBack)
            {
                if (Session["StringToken"] != null)
                {
                    Claim _autenticado = TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "Autenticado");

                    if (Convert.ToBoolean(_autenticado.Value))
                    {
                        LlenarDatosUsuario();                   
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void LlenarDatosUsuario()
        {
            lblUsuario.Text = TokenGenerator.GetClaimsValueFromJwtSecurityToken(Session["StringToken"].ToString(), "NombreUsuario");

        }
    }
}