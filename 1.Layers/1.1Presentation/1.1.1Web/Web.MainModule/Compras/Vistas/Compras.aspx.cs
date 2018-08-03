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
                        CargarEmpresas();
                        CargarRequisiciones();
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
            //lblMensaje.Text = respuesta.Mensaje;
        }

        protected void btnNuevaReq_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Requisicion/Vistas/Requisicion.aspx");
        }
        private void CargarEmpresas()
        {
            //ddlRazon.DataSource = null;
            ddlEmpresas.DataSource = new Seguridad.Servicio.ComprasServicio().Empresas(_tok);
            ddlEmpresas.DataValueField = "IdEmpresa";
            ddlEmpresas.DataTextField = "NombreComercial";
            ddlEmpresas.DataBind();
            ddlEmpresas.SelectedValue = "-1";
        }
        private void CargarRequisiciones()
        {
            dgRequisisiones.DataSource = new Requisicion.Serivicio.RequsicionServicio().BuscarRequisiciones(Convert.ToInt16(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(_tok, "IdEmpresa").Value), Session["StringToken"].ToString());
            dgRequisisiones.DataBind();
        }
    }
}