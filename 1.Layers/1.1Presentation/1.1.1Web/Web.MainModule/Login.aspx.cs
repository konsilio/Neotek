using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.MainModule
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarEmpresas();
        }

        protected void btnIniciar_Click(object sender, EventArgs e)
        {
            new Seguridad.Servicio.AutenticacionServicio().Autenticar(1, Email.Text, Password.Text);
            
        }
        private void CargarEmpresas()
        {
            foreach (var item in new Seguridad.Servicio.AutenticacionServicio().EmpresasLogin())
            {
                ddlRazon.Items.Add(new ListItem( item.RazonSocial, item.IdEmpresa.ToString()));
                ddlRazon.DataBind();
            }
            //ddlRazon.DataSource = new Seguridad.Servicio.AutenticacionServicio().EmpresasLogin();
            //ddlRazon.DataValueField = "IdEmpresa";
            //ddlRazon.DataTextField = "NombreComercial";
            //ddlRazon.DataBind();
        }
    }     
}