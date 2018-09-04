using Application.MainModule.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities.MainModule;
using Web.MainModule.Catalogos.Model;
using Web.MainModule.Catalogos.Servicio;
using Web.MainModule.Seguridad.Servicio;

namespace Web.MainModule.Catalogos
{
    public partial class Empresas : System.Web.UI.Page
    {
        string _tok = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StringToken"] != null)
            {
                _tok = Session["StringToken"].ToString();
                if (!IsPostBack)
                {
                    if (TokenServicio.ObtenerAutenticado(_tok))
                    {
                        CargarEmpresas();
                        CargarGvEmpresas();
                        ddlStatus.Items.Insert(0, new ListItem("En funciones", ""));
                    }                  
                }
            }
        }

        private void CargarEmpresas()
        {
            ddlEmpresas.DataSource = new EmpresaSvo().BuscarEmpresas(_tok).Where(x => x.EsAdministracionCentral.Equals(false)).ToList();
            ddlEmpresas.DataTextField = "NombreComercial";
            ddlEmpresas.DataValueField = "IdEmpresa";
            ddlEmpresas.DataBind();
            if (!TokenServicio.ObtenerEsAdministracionCentral(_tok))
                ddlEmpresas.Enabled = false;

            ddlEmpresas.Items.Insert(0, new ListItem("Seleccione...", ""));
        }
        private void CargarGvEmpresas()
        {
            try
            {
                gvEmpresas.DataSource = ViewState["List<EmpresaSvo>"] = new EmpresaSvo().BuscarEmpresas(_tok);
                gvEmpresas.DataBind();
            }
            catch {
                Exception ex;
                
            }

        }
        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevaEmpresa.aspx");
        }
        protected void gvEmpresas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {

            }
            if (e.CommandName.Equals("Borrar"))
            {

            }
        }
    }
}