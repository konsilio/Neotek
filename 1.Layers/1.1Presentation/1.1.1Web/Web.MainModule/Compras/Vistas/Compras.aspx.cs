using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Claims;
using Security.MainModule.Token_Service;
using Web.MainModule.Seguridad.Servicio;

namespace Web.MainModule
{
    public partial class Compras : System.Web.UI.Page
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
                        CargarRequisiciones();
                        CargarEstatus();
                    }
                    else
                        Salir();
                }
            }
            else
                Salir();
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
            ddlEmpresas.DataSource = new ComprasServicio().Empresas(_tok).Where(x => x.EsAdministracionCentral.Equals(false)).ToList();
            ddlEmpresas.DataValueField = "IdEmpresa";
            ddlEmpresas.DataTextField = "NombreComercial";
            ddlEmpresas.DataBind();
            if (TokenServicio.ObtenerEsAdministracionCentral(_tok))
            {
                ddlEmpresas.SelectedValue = "-1";
            }
            else
            {
                ddlEmpresas.SelectedValue = TokenServicio.ObtenerIdEmpresa(_tok).ToString();
                ddlEmpresas.Enabled = false;
            }
        }
        private void CargarEstatus()
        {
            foreach (Requisicion.Model.RequisiconEstatus r in Enum.GetValues(typeof(Requisicion.Model.RequisiconEstatus)))
            {
                ListItem item = new ListItem(Enum.GetName(typeof(Requisicion.Model.RequisiconEstatus), r).Replace("_", " "), ((byte)r).ToString());
                ddlFiltroEstatus.Items.Add(item);
            }
        }
        private void CargarRequisiciones()
        {
            dgRequisisiones.DataSource = ViewState["ListRequisicionDTO"] = new Requisicion.Servicio.RequisicionServicio().BuscarRequisiciones(short.Parse(ddlEmpresas.SelectedValue), Session["StringToken"].ToString()).ToList().OrderByDescending(x => x.IdRequisicion).ToList();
            dgRequisisiones.DataBind();
            if (!Convert.ToBoolean(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "EsAdminCentral").Value))
                dgRequisisiones.Columns[0].Visible = false;
            // ModificargvRequisiciones();
        }
        protected void dgRequisisiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("VerRequi"))
            {
                Response.Redirect("~/Requisicion/Vistas/Requisicion.aspx?nr=" + e.CommandArgument.ToString() + "&Sts=1");
            }
            if (e.CommandName.Equals("VerAut"))
            {
                Response.Redirect("~/Requisicion/Vistas/Requisicion.aspx?nr=" + e.CommandArgument.ToString() + "&Sts=2");
            }
        }
        protected void dgRequisisiones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgRequisisiones.DataSource = ViewState["ListRequisicionDTO"];
            dgRequisisiones.PageIndex = e.NewPageIndex;
            dgRequisisiones.DataBind();
        }
        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlEmpresas.SelectedValue.Equals("0"))
            {
                CargarRequisiciones();
            }
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

        protected void dgRequisisiones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                Label lblEstatus = (e.Row.Cells[0].FindControl("lblIdRequisicionEstatus") as Label);
                if (int.Parse(lblEstatus.Text).Equals(1))                
                    (e.Row.Cells[0].FindControl("lbDgOjo") as LinkButton).Visible = true;                
                if (int.Parse(lblEstatus.Text).Equals(3) || int.Parse(lblEstatus.Text).Equals(4))                
                    (e.Row.Cells[0].FindControl("lbAutoriza") as LinkButton).Visible = true;                
                if (int.Parse(lblEstatus.Text).Equals(7))
                {
                    (e.Row.Cells[0].FindControl("lbAutoriza") as LinkButton).Visible = false;
                    (e.Row.Cells[0].FindControl("lbDgOjo") as LinkButton).Visible = false;
                    (e.Row.Cells[0].FindControl("lbDgPDF") as LinkButton).Visible = false;
                }
            }
        }
        protected void ddlFiltroEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Requisicion.Model.RequisicionDTO> newList = new List<Requisicion.Model.RequisicionDTO>();
            foreach (var item in (List<Requisicion.Model.RequisicionDTO>)ViewState["ListRequisicionDTO"])
            {
                if (item.IdRequisicionEstatus.ToString().Equals(ddlFiltroEstatus.SelectedValue))
                {
                    newList.Add(item);
                }
            }
            dgRequisisiones.DataSource = newList.ToList().OrderByDescending(x => x.IdRequisicion).ToList();
            dgRequisisiones.DataBind();
        }              
    }
}