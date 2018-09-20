using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.MainModule.Catalogos.Servicio;
using Web.MainModule.Seguridad.Servicio;

namespace Web.MainModule.Catalogos
{
    public partial class Proveedor : System.Web.UI.Page
    {
        string _tok = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StringToken"] != null)
            {
                _tok = Session["StringToken"].ToString();
                if (!IsPostBack)
                {
                    if (TokenServicios.ObtenerAutenticado(_tok))
                    {
                        CargarEmpresas(TokenServicios.ObtenerIdEmpresa(_tok));
                        CargarEmpresasFiltro(TokenServicios.ObtenerIdEmpresa(_tok));
                    }
                    else
                        Salir();
                }
            }
        }
        private void Salir()
        {
            Response.Redirect("../../Login.aspx");
        }      
        private void CargarEmpresas(short idEmpresa)
        {
            if (TokenServicios.ObtenerEsAdministracionCentral(_tok))
                ddlEmpresas.DataSource = new EmpresaServicio().Empresas(_tok).Where(x => x.EsAdministracionCentral.Equals(false)).ToList();
            else
                ddlEmpresas.DataSource = new EmpresaServicio().Empresas(_tok).Where(x => x.EsAdministracionCentral.Equals(false) && x.IdEmpresa.Equals(idEmpresa)).ToList();
            ddlEmpresas.DataTextField = "NombreComercial";
            ddlEmpresas.DataValueField = "IdEmpresa";
            ddlEmpresas.DataBind();
            if (!TokenServicios.ObtenerEsAdministracionCentral(_tok))
                ddlEmpresas.Enabled = false;
        }
        private void CargarEmpresasFiltro(short idEmpresa)
        {
            if (TokenServicios.ObtenerEsAdministracionCentral(_tok))
                ddlFiltroEmpresa.DataSource = new EmpresaServicio().Empresas(_tok).Where(x => x.EsAdministracionCentral.Equals(false)).ToList();
            else
                ddlFiltroEmpresa.DataSource = new EmpresaServicio().Empresas(_tok).Where(x => x.EsAdministracionCentral.Equals(false) && x.IdEmpresa.Equals(idEmpresa)).ToList();
            ddlFiltroEmpresa.DataTextField = "NombreComercial";
            ddlFiltroEmpresa.DataValueField = "IdEmpresa";
            ddlFiltroEmpresa.DataBind();
            if (!TokenServicios.ObtenerEsAdministracionCentral(_tok))
                ddlFiltroEmpresa.Enabled = false;
            CargarListaProveedor();
        }
        private void CargarListaProveedor()
        {
            gvProveedor.DataSource = new ProveedorServicio().CargarProveedores(_tok).Where(x => x.IdEmpresa.Equals(short.Parse(ddlEmpresas.SelectedValue))).ToList();
            gvProveedor.DataBind();
        }

        protected void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            divDatos.Visible = true;
            divPrincipal.Visible = false;
        }

        protected void gvProveedor_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}