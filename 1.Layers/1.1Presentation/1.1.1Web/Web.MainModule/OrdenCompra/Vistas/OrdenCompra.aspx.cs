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
using Web.MainModule.Inventario.Model;

namespace Web.MainModule.OrdenCompra.Vistas
{
    public partial class OrdenCompra : Page
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
                        if (Request.QueryString["oc"] != null)
                            CargarOrdenCompra(int.Parse(Request.QueryString["oc"].ToString()));
                        //else
                            //Salir();
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
            RequisicionOCDTO reqDto = new OrdenCompraServicio().DatosRequisicion(id, _token);
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
        private void CargarOrdenCompra(int id)
        {
            var Oc = new OrdenCompraServicio().OrdenCompraPorId(id, _token);
            if (Oc.IdOrdenCompraEstatus.Equals(Convert.ToByte((byte)OrdenCompraEstatus.Espera_de_Autorización)))            
                ActivarAutorizar(Oc);
            if (Oc.IdOrdenCompraEstatus.Equals((byte)OrdenCompraEstatus.Proceso_de_Compra))
                ActivarAlmacen(Oc);
            if (Oc.IdOrdenCompraEstatus.Equals((byte)OrdenCompraEstatus.Proceso_de_Compra) && Oc.EsGas && Oc.EsTransporteGas)
                ActivarAlmacen(Oc);
            if (Oc.IdOrdenCompraEstatus.Equals((byte)OrdenCompraEstatus.Compra_Cancelada) || Oc.IdOrdenCompraEstatus.Equals((byte)OrdenCompraEstatus.Compra_Exitosa))
                ActivarVerOrdenCompra(Oc);
        }
        private void ActivarAutorizar(OrdenCompraCrearDTO oc)
        {
            var reqDto = new OrdenCompraServicio().DatosRequisicion(oc.IdRequisicion, _token);
            ViewState["OrdenCompraCrearDTO"] = oc;
            txtFechaRequerida.Text = reqDto.FechaRequerida.Date.ToString();
            TxtSolicitante.Text = reqDto.UsuarioSolicitante;
            txtMotivoCompra.Text = reqDto.MotivoRequisicion;
            txtRequeridoEn.Text = reqDto.RequeridoEn;
            lblNumRequisicion.Text = reqDto.NumeroRequisicion;            

            lblNunOrdenCompra.Text = oc.NumOrdenCompra;
            lblNunOrdenCompra.Visible = true;      
            gvProdcutosOrdenCompra.DataSource =oc.Productos;
            gvProdcutosOrdenCompra.DataBind();
            lblProveedor.Text = oc.Proveedor;
            divCrearOC.Visible = false;
            divAutorizarOC.Visible = true;
            //BtnCrear.Text = "Actualizar";
            lblbtnCrear.Text = "Autorizar";
        }
        private void ActivarAlmacen(OrdenCompraDTO oc)
        {
            lblProveedor.Text = oc.Proveedor;
            divAutorizarOC.Visible = true;
            lblbtnCrear.Text = "Aceptar";
            //gvProdcutosOrdenCompra.DataSource = oc.Productos;
            //gvProdcutosOrdenCompra.DataBind();
        }
        private void ActivarVerOrdenCompra(OrdenCompraDTO oc)
        {

        }
        private void ActivarComplementoGas(OrdenCompraDTO oc)
        {
            lblProveedor.Text = oc.Proveedor;
            divDatosComplementoGas.Visible = true;
            divGvvComplementoGas.Visible = true;
            divCrearOC.Visible = false;
        }
        private void Crear()
        {
            foreach (var item in new OrdenCompraServicio().GenerarOrdenesCompra(GenerarOrdenCompra(), _token))
            {
                if (item.Exito)
                {
                    lblMensajeOrdenCompra.Text = lblMensajeOrdenCompra.Text + "N° de Orden de Compra: " + item.NumOrdenCompra;
                }
            }
        }
        private void Autorizar()
        {
            var resp = new OrdenCompraServicio().AutorizarOrdenCompra(new OrdenCompraAutorizacionDTO { IdOrdenCompra = ((OrdenCompraCrearDTO)ViewState["OrdenCompraCrearDTO"]).IdOrdenCompra, UsuarioAutoriza = TokenServicio.ObtenerIdUsuario(_token) }, _token);
            if (resp.Exito)
            {
                btnok.Attributes.Remove("class");
                btnok.Attributes.Add("class", "btn btn-raised btn-primary btn-round disabled");
                lblMensajeOrdenCompra.Text = resp.Mensaje;
                Session["OrdenCompraCrearDTO"] = null;
            }
            else
            {
                divMensajeError.Visible = true;
                lblErrorCampos.Text = resp.MensajesError[0];

            }
        }
        private void ActualizarAlmacen()
        {
            //if (true)
            //{
            //    var resp = 
            //}
        }
        private List<AlmacenEntradaCrearDTO> GenerarEntradasAlmacen()
        {
            List<AlmacenEntradaCrearDTO> _lEntradas = new List<AlmacenEntradaCrearDTO>();
            foreach (GridViewRow _r in gvEntrada.Rows)
            {
                OrdenCompraCrearDTO oc = (OrdenCompraCrearDTO)ViewState["OrdenCompraCrearDTO"];
                _lEntradas.Add(new AlmacenEntradaCrearDTO
                {
                    IdEmpresa = oc.IdEmpresa,
                    IdOrdenCompra = oc.IdEmpresa,
                    IdProduto = int.Parse((_r.Cells[0].FindControl("") as Label).Text),
                    Cantidad = decimal.Parse((_r.Cells[0].FindControl("") as TextBox).Text),
                    Observaciones = (_r.Cells[0].FindControl("") as TextBox).Text
                });
            }

            return _lEntradas;
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
            foreach (enumIEPS r in Enum.GetValues(typeof(enumIEPS)))
            {
                ListItem item = new ListItem(Enum.GetName(typeof(enumIEPS), r).ToUpper(), ((byte)r).ToString());
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
                p.Cantidad = decimal.Parse((_row.FindControl("lbldgCantidad") as Label).Text);
                decimal _descuento = ((p.Precio * p.Cantidad) * (p.Descuento / 100));
                decimal subtotal = (p.Precio * p.Cantidad) - (_descuento);
                decimal iva = ((subtotal) * (p.IVA / 100));
                decimal ieps = ((subtotal) * (p.IEPS / 100));
                p.Importe = subtotal + iva + ieps;
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
            Response.Redirect("~/OrdenCompra/Vistas/Compras.aspx");
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            OrdenCompraDTO _ocCancelar = new OrdenCompraDTO();
            if (Request.QueryString["oc"] != null)            
                _ocCancelar.IdOrdenCompra = int.Parse(Request.QueryString["oc"].ToString());     
            _ocCancelar.IdOrdenCompraEstatus = (byte)OrdenCompraEstatus.Compra_Cancelada;
            var resp = new OrdenCompraServicio().CancelarOrdenCompra(_ocCancelar, _token);
        }
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            
            if (lblbtnCrear.Text.Equals("Crear"))
            {
                Crear();
            }
            if (lblbtnCrear.Text.Equals("Autorizar"))
            {
                Autorizar();
            }

            if (lblbtnCrear.Text.Equals("Aceptar"))
            {
                ActualizarAlmacen();
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
        protected void btnGuardarDatosPapeleta_Click(object sender, EventArgs e)
        {

        }
    }
}