using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security.MainModule.Criptografia;

namespace Web.MainModule
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Session["StringToken"] != null) Session["StringToken"] = null;
                CargarEmpresas();
            }
        }
        
        protected void btnIniciar_Click(object sender, EventArgs e)
        {
            Autenticar();
        }
        private void Autenticar()
        {
            var respuesta = new Seguridad.Servicio.AutenticacionServicio().Autenticar(Convert.ToInt16(ddlRazon.SelectedValue), Email.Text, SHA.GenerateSHA256String(Password.Text));
            if (respuesta.Exito)
            {
                Session["StringToken"] = respuesta.token;
                Response.Redirect("~/Dashboard.aspx");                
            }
            else
            {
                divMensaje.Visible = true;
                lblMensaje.Text = respuesta.Mensaje;
            }
        }
        private void CargarEmpresas()
        {
            //ddlRazon.DataSource = null;
            ddlRazon.DataSource = new Seguridad.Servicio.AutenticacionServicio().EmpresasLogin();
            ddlRazon.DataValueField = "IdEmpresa";
            ddlRazon.DataTextField = "NombreComercial";
            ddlRazon.DataBind();
            ddlRazon.SelectedValue = "-1";
        }
    }
}