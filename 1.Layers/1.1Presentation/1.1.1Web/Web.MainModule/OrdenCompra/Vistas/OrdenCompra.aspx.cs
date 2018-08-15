using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Claims;
using Security.MainModule.Token_Service;

namespace Web.MainModule.OrdenCompra.Vistas
{
    public partial class OrdenCompra : System.Web.UI.Page
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
                        if (Request.QueryString["nr"] != null)
                            CargarDatosRequisicon(int.Parse(Request.QueryString["nr"].ToString()));
                        else
                            Salir();
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
        private void CargarDatosRequisicon(int id)
        {

        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        protected void BtnCrear_Click(object sender, EventArgs e)
        {

        }
    }
}