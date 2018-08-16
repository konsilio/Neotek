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
            Model.RequisicionOCDTO reqDto = new Servisio.OrdenCompraServicio().DatosRequisicion(id, Session["StringToken"].ToString());
            txtFechaRequerida.Text = reqDto.FechaRequerida.Date.ToString();
            TxtSolicitante.Text = reqDto.UsuarioSolicitante;
            txtMotivoCompra.Text = reqDto.MotivoRequisicion;
            txtRequeridoEn.Text = reqDto.RequeridoEn;
            lblNumRequisicion.Text = reqDto.NumeroRequisicion;
            lblNombreEmpresa.Text = reqDto.NombreComercial;
            dgListaproductos.DataSource = ViewState["ListaProdcutoOC"] = reqDto.Productos;
            dgListaproductos.Visible = true;
            dgListaproductos.DataBind();
        }
        private List<ListItem> IVAs()
        {
            List<ListItem> listaIVAs = new List<ListItem>();
            foreach (Model.enumIVA r in Enum.GetValues(typeof(Model.enumIVA)))
            {
                ListItem item = new ListItem(Enum.GetName(typeof(Model.enumIVA), r), ((byte)r).ToString());
                listaIVAs.Add(item);
            }
            return listaIVAs;
        }
        private List<ListItem> IEPSs()
        {
            List<ListItem> listaIVAs = new List<ListItem>();
            foreach (Model.enumIEPS r in Enum.GetValues(typeof(Model.enumIEPS)))
            {
                ListItem item = new ListItem(Enum.GetName(typeof(Model.enumIEPS), r), ((byte)r).ToString());
                listaIVAs.Add(item);
            }
            return listaIVAs;
        }
        private List<Model.OrdenCompraDTO> GenerarOCPorProveedor()
        {
            List<Model.OrdenCompraDTO> lOrdenCompra = new List<Model.OrdenCompraDTO>();
            foreach (GridViewRow _row in dgListaproductos.Rows)
            {
                if (!lOrdenCompra.Exists(x => x.IdProveedor.Equals(int.Parse((_row.Cells[0].FindControl("ddlProveedores") as DropDownList).SelectedValue))))
                {
                    lOrdenCompra.Add(new Model.OrdenCompraDTO
                    {
                        IdProveedor = int.Parse((_row.Cells[0].FindControl("ddlProveedores") as DropDownList).SelectedValue),
                        IdCentroCosto = int.Parse((_row.Cells[0].FindControl("lblIdCentroCosto") as Label).Text),
                        IdRequisicion = int.Parse(Request.QueryString["nr"])
                    });
                }
            }
            return lOrdenCompra;
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            List<Model.OrdenCompraDTO> lOrdenCompra = GenerarOCPorProveedor();
            foreach (Model.OrdenCompraDTO oc in lOrdenCompra)
            {
                foreach (GridViewRow _row in dgListaproductos.Rows)
                {
                    if ((_row.Cells[0].FindControl("ddlProveedores") as DropDownList).SelectedValue.Equals(oc.IdProveedor))
                    {
                        oc.Productos.Add(new Model.ProdcutoOC
                        {

                        });
                    }
                }
            }
        }
        protected void dgListaproductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList ddlIVA = e.Row.Cells[0].FindControl("ddlGvIVA") as DropDownList;
            if (ddlIVA != null)
            {
                ddlIVA.DataSource = IVAs();
                ddlIVA.DataBind();
            }
            DropDownList ddlIEPS = e.Row.Cells[0].FindControl("ddlGvIEPS") as DropDownList;
            if (ddlIEPS != null)
            {
            
                ddlIEPS.DataSource = IEPSs();
                ddlIEPS.DataBind();
            }
        }
        protected void dgListaproductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void dgListaproductos_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
    }
}