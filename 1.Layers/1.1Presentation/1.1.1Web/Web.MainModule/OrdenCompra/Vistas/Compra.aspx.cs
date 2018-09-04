using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Claims;
using Security.MainModule.Token_Service;
using Web.MainModule.OrdenCompra.Servicio;
using Web.MainModule.Seguridad.Servicio;
using Web.MainModule.OrdenCompra.Model;

namespace Web.MainModule.OrdenCompra.Vistas
{
    public partial class Compra : System.Web.UI.Page
    {
        string _token = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StringToken"] != null)
                _token = Session["StringToken"].ToString();
            else
                Salir();

            if (!IsPostBack)
            {
                if (TokenServicio.ObtenerAutenticado(_token))
                {
                    CargarEmpresas();
                    CargarEstatus();
                    CargarProveedores();
                    CargarOrdenesCompra();
                }
                else
                    Salir();
            }
        }
        private void CargarRequisiciones(short idEmpresa)
        {
            dgRequisisiones.DataSource = ViewState["ListRequisicionDTO"] = new Requisicion.Servicio.RequisicionServicio().BuscarRequisiciones(idEmpresa, Session["StringToken"].ToString()).ToList().
                                                Where(y => y.IdRequisicionEstatus.Equals(10)).ToList().OrderByDescending(x => x.IdRequisicion).ToList();
            dgRequisisiones.DataBind();
            if (!Convert.ToBoolean(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "EsAdminCentral").Value))
                dgRequisisiones.Columns[0].Visible = false;
        }
        private void CargarEstatus()
        {
            ddlFiltroEstatus.DataSource = new OrdenCompraServicio().ListaOCEstatus(_token);
            ddlFiltroEstatus.DataTextField = "Descripcion";
            ddlFiltroEstatus.DataValueField = "IdOrdenCompraEstatus";
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
        private void CargarProveedores()
        {
            ddlFiltroProveedores.DataSource = new Servicio.OrdenCompraServicio().Proveedores(Session["StringToken"].ToString());
            ddlFiltroProveedores.DataTextField = "NombreComercial";
            ddlFiltroProveedores.DataValueField = "IdProveedor";
            ddlFiltroProveedores.DataBind();
        }
        private void CargarOrdenesCompra()
        {
            gvOrdenCompra.DataSource = ViewState["OrdenCompraDTO"] = new OrdenCompraServicio().ObtenerOrdenesCompra(short.Parse(ddlEmpresas.SelectedValue), _token).ToList().OrderByDescending(x => x.NumOrdenCompra).ToList();
            gvOrdenCompra.DataBind();
        }
        private void CargarOrdenesCompra(List<OrdenCompraDTO> lista)
        {
            gvOrdenCompra.DataSource = lista;
            gvOrdenCompra.DataBind();
        }
        private void Salir()
        {
            Response.Redirect("~/Login.aspx");
        }
        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarRequisiciones(short.Parse(ddlEmpresas.SelectedValue));
            CargarOrdenesCompra();
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
            List<OrdenCompraDTO> newList = (List<OrdenCompraDTO>)ViewState["OrdenCompraDTO"];
            if (txtNoOrdenRequisicionOC.Equals(string.Empty))
                newList = newList.Where(x => x.NumOrdenCompra.Equals(txtNoOrdenCompra.Text)).ToList();
            else
                newList = newList.Where(x => x.NumOrdenCompra.Equals(txtNoOrdenCompra.Text) && x.NumeroRequisicion.Equals(txtNoOrdenRequisicionOC.Text)).ToList();

            CargarOrdenesCompra(newList);
        }
        protected void txtNoOrdenRequisicionOC_TextChanged(object sender, EventArgs e)
        {
            List<OrdenCompraDTO> newList = (List<OrdenCompraDTO>)ViewState["OrdenCompraDTO"];
            if (txtNoOrdenCompra.Equals(string.Empty))
                newList = newList.Where(x => x.NumeroRequisicion.Equals(txtNoOrdenRequisicionOC.Text)).ToList();
            else
                newList = newList.Where(x => x.NumeroRequisicion.Equals(txtNoOrdenRequisicionOC.Text) && x.NumOrdenCompra.Equals(txtNoOrdenCompra.Text)).ToList();
            CargarOrdenesCompra(newList);
        }
        protected void dgRequisisiones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgRequisisiones.DataSource = ViewState["ListRequisicionDTO"];
            dgRequisisiones.PageIndex = e.NewPageIndex;
            dgRequisisiones.DataBind();
        }
        protected void dgRequisisiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Requisicion"))
                Response.Redirect("~/OrdenCompra/Vistas/OrdenCompra.aspx?nr=" + e.CommandArgument.ToString());
        }
        protected void gvOrdenCompra_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrdenCompra.DataSource = ViewState["OrdenCompraDTO"];
            gvOrdenCompra.PageIndex = e.NewPageIndex;
            gvOrdenCompra.DataBind();
        }
        protected void gvOrdenCompra_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("OrdenCompra"))            
                Response.Redirect("~/OrdenCompra/Vistas/OrdenCompra.aspx?oc=" + e.CommandArgument.ToString());
            
            if (e.CommandName.Equals("AgregarMerca"))            
                Response.Redirect("~/OrdenCompra/Vistas/OrdenCompra.aspx?oc=" + e.CommandArgument.ToString());
            
            if (e.CommandName.Equals("Ver"))            
                Response.Redirect("~/OrdenCompra/Vistas/OrdenCompra.aspx?oc=" + e.CommandArgument.ToString());            
        }
        protected void gvOrdenCompra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                int estatus = int.Parse((e.Row.Cells[0].FindControl("lblgvIdEstatus") as Label).Text);
                if (estatus.Equals(OrdenCompraEstatusEnum.Espera_autorizacion))
                    (e.Row.Cells[0].FindControl("lbAutOC") as LinkButton).Visible = true;
                if (estatus.Equals(OrdenCompraEstatusEnum.Proceso_compra)) { 
                    (e.Row.Cells[0].FindControl("lbAgregarMercancia") as LinkButton).Visible = true;
                    (e.Row.Cells[0].FindControl("lbPDF") as LinkButton).Visible = true;
                }
                if (estatus.Equals(OrdenCompraEstatusEnum.Compra_exitosa) || estatus.Equals(OrdenCompraEstatusEnum.Compra_cancelada))
                    (e.Row.Cells[0].FindControl("lbVisualizarOC") as LinkButton).Visible = true;
            }
        }
        protected void ddlFiltroEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List<Requisicion.Model.RequisicionDTO> newList = new List<Requisicion.Model.RequisicionDTO>();
            //foreach (var item in (List<Requisicion.Model.RequisicionDTO>)ViewState["ListaOrdenesCompra"])
            //{
            //    if (item.IdRequisicionEstatus.ToString().Equals(ddlFiltroEstatus.SelectedValue))
            //    {
            //        newList.Add(item);
            //    }
            //}
            //gvOrdenCompra.DataSource = newList.ToList().OrderByDescending(x => x.IdRequisicion).ToList();
            //gvOrdenCompra.DataBind();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            FiltrarOrdenesCompra();
        }
        private void FiltrarOrdenesCompra()
        {
            List<OrdenCompraDTO> newList = (List<OrdenCompraDTO>)ViewState["OrdenCompraDTO"];
            #region Por estatus
            if (ddlFiltroEstatus.SelectedValue != "0")
                newList = newList.Where(x => x.IdOrdenCompraEstatus.Equals(byte.Parse(ddlFiltroEstatus.SelectedValue))).ToList();
            #endregion
            #region Por proveedor (Solo administracion Central)
            if (ddlFiltroProveedores.SelectedValue != "0")
                newList = newList.Where(x => x.IdProveedor.Equals(int.Parse(ddlFiltroProveedores.SelectedValue))).ToList();
            #endregion

            #region Por Fecha de registro           
            if (dtpFechaRegiistrDe.HasValue)
                newList = newList.Where(x => x.FechaRegistro >= dtpFechaRegiistrDe.GetDate).ToList();
            if (dtpFechaRegiistrA.HasValue)
                newList = newList.Where(x => x.FechaRegistro <= dtpFechaRegiistrA.GetDate).ToList();
            #endregion

            #region Por Fecha de sequisicion
            if (dtpFechaRequisicionDe.HasValue)
                newList = newList.Where(x => x.FechaRequerida >= dtpFechaRequisicionDe.GetDate).ToList();
            if (dtpFechaRequisicionA.HasValue)
                newList = newList.Where(x => x.FechaRequerida <= dtpFechaRequisicionA.GetDate).ToList();
            #endregion

            if (newList.Count.Equals(0))
                gvOrdenCompra.EmptyDataText = Exceptions.MainModule.Validaciones.Error.R0010;
            gvOrdenCompra.DataSource = newList.ToList().OrderByDescending(x => x.IdRequisicion).ToList();
            gvOrdenCompra.DataBind();
        }
    }
}