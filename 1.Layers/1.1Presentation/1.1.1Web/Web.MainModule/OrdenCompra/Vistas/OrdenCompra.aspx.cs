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
    public partial class OrdenCompra : System.Web.UI.Page
    {
        private string _token;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StringToken"] != null)
            {
                _token = Session["StringToken"].ToString();
                if (!IsPostBack)
                {
                    if (TokenServicio.ObtenerAutenticado(_token))
                    {
                        if (Request.QueryString["nr"] != null)
                            CargarDatosRequisicon(int.Parse(Request.QueryString["nr"].ToString()));
                        else
                            Salir();
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
        private void CargarDatosRequisicon(int id)
        {
            Model.RequisicionOCDTO reqDto = new OrdenCompraServicio().DatosRequisicion(id, _token);
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
                ListItem item = new ListItem(Enum.GetName(typeof(Model.enumIVA), r).ToUpper(), ((byte)r).ToString());
                listaIVAs.Add(item);
            }
            return listaIVAs;
        }
        private List<ListItem> IEPSs()
        {
            List<ListItem> listaIVAs = new List<ListItem>();
            foreach (Model.enumIEPS r in Enum.GetValues(typeof(Model.enumIEPS)))
            {
                ListItem item = new ListItem(Enum.GetName(typeof(Model.enumIEPS), r).ToUpper(), ((byte)r).ToString());
                listaIVAs.Add(item);
            }
            return listaIVAs;
        }        
        private List<OrdenCompraProductoCrearDTO> ObtenerProductosGrid()
        {
            List<OrdenCompraProductoCrearDTO> lp = new List<OrdenCompraProductoCrearDTO>();
            foreach (GridViewRow _row in dgListaproductos.Rows)
            {
                string precio = (_row.FindControl("txtPrecio") as TextBox).Text;
                string descuento = (_row.FindControl("txtgvDescuento") as TextBox).Text;
                OrdenCompraProductoCrearDTO p = new OrdenCompraProductoCrearDTO();
                p.IdProducto = int.Parse((_row.FindControl("lblidProducto") as Label).Text);
                p.IdCentroCosto = int.Parse((_row.FindControl("lbldgidCentroCosto") as Label).Text);
                p.IdCuentaContable = int.Parse((_row.FindControl("ddlCuentaContable") as DropDownList).SelectedValue);
                p.IdProveedor = int.Parse((_row.FindControl("ddlProveedor") as DropDownList).SelectedValue);
                p.Precio = decimal.Parse(precio == "" ? "0" : precio);
                p.Descuento = decimal.Parse(descuento == "" ? "0": descuento );
                p.IVA = decimal.Parse((_row.FindControl("ddlGvIVA") as DropDownList).Text.Replace("IVA", ""));
                p.IEPS = decimal.Parse((_row.FindControl("ddlGvIEPS") as DropDownList).Text.Replace("IEPS", ""));
                p.Importe = decimal.Parse((_row.FindControl("lbldgImporte") as Label).Text);
                lp.Add(p);
            }
            return lp;
        }
        private OrdenCompraCrearDTO GenerarOrdenCompra()
        {
            OrdenCompraCrearDTO oc = new OrdenCompraCrearDTO();
            oc.IdRequisicion = int.Parse(Request.QueryString["nr"].ToString());
            oc.Productos = ObtenerProductosGrid();
            oc.IdOrdenCompraEstatus = 2;
            return oc;
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            foreach (var item in new OrdenCompraServicio().GenerarOrdenesCompra(GenerarOrdenCompra(), _token))
            {
                if (item.Exito)
                {
                    txtMensajeOrdenCompra.Text = txtMensajeOrdenCompra.Text + "N° de Orden de Compra: " + item.NumOrdenCompra + "n\r\"";
                }               
            } 
        }
        protected void dgListaproductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region Cargar dropDownList IVA
            DropDownList ddlIVA = e.Row.Cells[0].FindControl("ddlGvIVA") as DropDownList;
            if (ddlIVA != null)
            {
                //ddlIVA.DataSource = null;
                ddlIVA.DataSource = IVAs();
                ddlIVA.DataBind();
            }
            #endregion
            #region Cargar dropDownList IEPS
            DropDownList ddlIEPS = e.Row.Cells[0].FindControl("ddlGvIEPS") as DropDownList;
            if (ddlIEPS != null)
            {
                ddlIEPS.DataSource = null;
                ddlIEPS.DataSource = IEPSs();
                ddlIEPS.DataBind();
            }
            #endregion
            #region Cargar dropDownList Cuenta contable
            DropDownList ddlCtaContable = e.Row.Cells[0].FindControl("ddlCuentaContable") as DropDownList;
            if (ddlCtaContable != null)
            {
                //Descomentar cuando las Cuentas conables este listas
                //ddlCtaContable.DataSource = new OrdenCompraServicio().ListaCuentasContables(_token);
                //ddlCtaContable.DataTextField = "IdCuentaContable";
                //ddlCtaContable.DataValueField = "Descripcion";
                ddlCtaContable.Items.Add(new ListItem("CuentaContable1", "1"));
                ddlCtaContable.DataBind();
            }          
            #endregion
            #region Cargar dropDownList Proveedores
            DropDownList ddlProvee = e.Row.Cells[0].FindControl("ddlProveedor") as DropDownList;
            if (ddlProvee != null)
            {
                ddlProvee.DataSource = new OrdenCompraServicio().Proveedores(_token);
                ddlProvee.DataTextField = "NombreComercial";
                ddlProvee.DataValueField = "IdProveedor";
                ddlProvee.DataBind();
            }            
            #endregion
        }      
        protected void dgListaproductos_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnSolicitarPagoExpedidor_Click(object sender, EventArgs e)
        {

        }

        protected void btnSolicitarPaogPorteador_Click(object sender, EventArgs e)
        {

        }
    }
}