using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Claims;
using Security.MainModule.Token_Service;
using Web.MainModule.OrdenCompra.Servisio;
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
        private void CalcularProducto(string idProd)
        {
            foreach (GridViewRow _row in  dgListaproductos.Rows)
            {              
                if ((_row.Cells[0].FindControl("lblidProducto")as Label).Text.Equals(idProd))
                {
                    string _precioString = (_row.Cells[0].FindControl("txtPrecio") as TextBox).Text;
                    string _descuentoString = (_row.Cells[0].FindControl("txtgvDescuento") as TextBox).Text;
                    decimal _CantidadAComprar = decimal.Parse((_row.Cells[0].FindControl("lbldgCantidad") as Label).Text);
                    decimal _Precio = decimal.Parse(_precioString.Equals(string.Empty) ? "0" : _precioString);
                    decimal _descuento = decimal.Parse(_descuentoString.Equals(string.Empty) ? "0" : _descuentoString);
                    (_row.Cells[0].FindControl("lbldgImporte") as Label).Text = ((_CantidadAComprar * _Precio) - (_descuento *(_CantidadAComprar * _Precio)/ 100)).ToString("N2");
                }
            }
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
        protected void dgListaproductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Refresh"))
            {
                CalcularProducto(e.CommandArgument.ToString());
            }
        }
        protected void dgListaproductos_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
    }
}