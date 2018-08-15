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
    public partial class Compra : System.Web.UI.Page
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
                        CargarEmpresas();                       
                        CargarEstatus();
                    }
                    else
                        Salir();
                }
                else
                    Salir();
            }
        }
        private void CargarRequisiciones(short idEmpresa)
        {
            dgRequisisiones.DataSource = ViewState["ListRequisicionDTO"] = new Requisicion.Servicio.RequsicionServicio().BuscarRequisiciones(idEmpresa, Session["StringToken"].ToString()).ToList().OrderByDescending(x => x.IdRequisicion).ToList();
            dgRequisisiones.DataBind();
            if (!Convert.ToBoolean(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "EsAdminCentral").Value))
                dgRequisisiones.Columns[0].Visible = false;
        }
        private void CargarEstatus()
        {
            foreach (Requisicion.Model.RequisiconEstatus r in Enum.GetValues(typeof(Requisicion.Model.RequisiconEstatus)))
            {
                ListItem item = new ListItem(Enum.GetName(typeof(Requisicion.Model.RequisiconEstatus), r).Replace("_", " "), ((byte)r).ToString());
                ddlFiltroEstatus.Items.Add(item);
                
            }
            ddlFiltroEstatus.DataBind();
        }
        private void CargarEmpresas()
        {
            ddlEmpresas.DataSource = new Seguridad.Servicio.ComprasServicio().Empresas(Session["StringToken"].ToString());
            ddlEmpresas.DataValueField = "IdEmpresa";
            ddlEmpresas.DataTextField = "NombreComercial";
            ddlEmpresas.DataBind();
            if (Convert.ToBoolean(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "EsAdminCentral").Value))
            {
                ddlEmpresas.SelectedValue = "-1";
            }
            else
            {
                ddlEmpresas.SelectedValue = TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "EsAdminCentral").Value;
                ddlEmpresas.Enabled = false;
            }
            CargarRequisiciones(short.Parse(ddlEmpresas.SelectedValue));
        }
        private void CargarProveedores(short idEmp)
        {
            ddlFiltroProveedores.DataSource = new Servisio.OrdenCompraServicio().Proveedores(idEmp, Session["StringToken"].ToString());
            ddlFiltroProveedores.DataTextField = "NombreComercial";
            ddlFiltroProveedores.DataValueField = "IdProveedor";
            ddlFiltroProveedores.DataBind();
        }
        private void Salir()
        {
            Response.Redirect("~/Login.aspx");
        }
        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProveedores(short.Parse(ddlEmpresas.SelectedValue));
            CargarRequisiciones(short.Parse(ddlEmpresas.SelectedValue));
        }
        protected void txtNoRequisicion_TextChanged(object sender, EventArgs e)
        {
            List<Requisicion.Model.RequisicionDTO> newList = new List<Requisicion.Model.RequisicionDTO>();
            foreach (var item in (List<Requisicion.Model.RequisicionDTO>)ViewState["ListRequisicionDTO"])
            {
                if (item.NumeroRequisicion.Contains(txtNoRequisicion.Text))
                {
                    newList.Add(item);
                }
            }
            dgRequisisiones.DataSource = newList.ToList().OrderByDescending(x => x.IdRequisicion).ToList();
            dgRequisisiones.DataBind();
        }
        protected void txtNoOrdenCompra_TextChanged(object sender, EventArgs e)
        {

        }
        protected void txtNoOrdenRequisicionOC_TextChanged(object sender, EventArgs e)
        {

        }
        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void dgRequisisiones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgRequisisiones.DataSource = ViewState["ListRequisicionDTO"];
            dgRequisisiones.PageIndex = e.NewPageIndex;
            dgRequisisiones.DataBind();
        }
        protected void dgRequisisiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("OrdenCompra"))            
                Response.Redirect("~/OrdenCompra/Vistas/OrdenCompra.aspx?nr=" + e.CommandArgument.ToString());            
        }        
        protected void gvOrdenCompra_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void gvOrdenCompra_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void gvOrdenCompra_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}