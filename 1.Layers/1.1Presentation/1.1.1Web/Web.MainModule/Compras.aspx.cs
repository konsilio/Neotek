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
    public partial class Compras : System.Web.UI.Page
    {
        string _tok = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            _tok = Session["StringToken"].ToString();
            if (!IsPostBack)
            {
                if (Session["StringToken"] != null)
                {
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
        protected void btnCompra_Click(object sender, EventArgs e)
        {
            var respuesta = new Seguridad.Servicio.ComprasServicio().Compra(_tok);
            lblMensaje.Text = respuesta.Mensaje;
        }
    }
}