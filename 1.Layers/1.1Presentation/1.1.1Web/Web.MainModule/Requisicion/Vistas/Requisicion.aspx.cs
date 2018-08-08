using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security.MainModule.Token_Service;
using System.Security.Claims;
using System.Globalization;

namespace Web.MainModule.Requisicion.Vista
{
    public partial class Requisicion : Page
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
                        if (Request.QueryString["nr"] != null)
                        {
                            if (Request.QueryString["Sts"] != null)
                            {
                                RquisicionAlternativa(Request.QueryString["nr"].ToString(), Convert.ToInt32(Request.QueryString["Sts"]));
                            }
                            dgListaproductos.DataBind();
                        }
                        else
                        {
                            if (Convert.ToBoolean(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "EsAdminCentral").Value))
                            {
                                ddlEmpresas.DataSource = new Servicio.RequsicionServicio().Empresas(Session["StringToken"].ToString());
                                ddlEmpresas.DataTextField = "NombreComercial";
                                ddlEmpresas.DataValueField = "IdEmpresa";
                                ddlEmpresas.DataBind();
                                ddlEmpresas.Visible = true;
                                lblNombreEmpresa.Visible = false;
                                CargarUsuariosSolicitante(Int16.Parse(ddlEmpresas.SelectedValue));
                                CargarProductos(Int16.Parse(ddlEmpresas.SelectedValue));
                            }
                            else
                            {
                                CargarUsuariosSolicitante(Convert.ToInt16(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "IdEmpresa").Value));
                                CargarProductos(Convert.ToInt16(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "IdEmpresa").Value));
                            }
                            dgListaproductos.DataBind();
                        }
                    }
                    else
                        Salir();
                }
                else
                    Salir();
            }
            else
            {

            }
        }
        private void Salir()
        {
            Response.Redirect("../../Login.aspx");
        }
        private Model.RequisicionDTO CrearReq()
        {
            return new Model.RequisicionDTO()
            {
                IdUsuarioSolicitante = Convert.ToInt32(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "IdUsuario").Value),
                IdEmpresa = Convert.ToInt16(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "IdEmpresa").Value),
                NumeroRequisicion = string.Empty,
                IdRequisicionEstatus = 1,
                FechaRequerida = DateTime.Today.AddDays(5), //DateTime.Parse(txtFechaRequerida.Value, new CultureInfo("en-US")), //(es-MX) 
                MotivoRequisicion = txtMotivoCompra.Text,
                RequeridoEn = txtRequeridoEn.Text,
                FechaRegistro = DateTime.Today,
            };
        }
        private bool ValidarCampos()
        {
            bool correcto = true;
            if (txtFechaRequerida.Value.Equals(string.Empty)) correcto = false;
            if (ddlSolicitante.SelectedValue.Equals(-1)) correcto = false;
            if (txtMotivoCompra.Text.Equals(string.Empty)) correcto = false;
            if (txtRequeridoEn.Text.Equals(string.Empty)) correcto = false;
            if (ViewState["ListaRequisicionProductoGridDTO"] == null) correcto = false;
            else if (((List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"]).Count.Equals(0)) correcto = false;
            return correcto;
        }
        private void LimpiarCampos()
        {
            ddlEmpresas.SelectedValue = "0";
            ddlSolicitante.SelectedValue = "0";
            txtMotivoCompra.Text = string.Empty;
            txtRequeridoEn.Text = string.Empty;
        }
        private bool ValidarCamposProductos()
        {
            bool correcto = true;
            if (ddlTipoCompra.SelectedValue.Equals(0)) correcto = false;
            if (ddlProdcutos.SelectedValue.Equals(0)) correcto = false;
            if (txtCantidad.Text.Equals(string.Empty)) correcto = false;
            if (ddlCentroCostos.SelectedValue.Equals(0)) correcto = false;
            if (txtDetalle.Text.Equals(string.Empty)) correcto = false;
            return correcto;
        }
        private void LimpiarCamposProductos()
        {
            ddlTipoCompra.SelectedIndex = 0;
            ddlProdcutos.SelectedIndex = 0;
            txtCantidad.Text = string.Empty;
            ddlCentroCostos.SelectedIndex = 0;
            txtDetalle.Text = string.Empty;
        }
        private bool ValidarProductoRepetido(int idProducto)
        {
            bool resp = false;
            if (ViewState["ListaRequisicionProductoGridDTO"] != null)
                resp = ((List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"]).Exists(x => x.IdProducto.Equals(idProducto));
            return resp;
        }
        private void RquisicionAlternativa(string NumRueq, int Estatus)
        {
            if (Estatus.Equals(1))
                ActivarRevisarExistencias();
            else
                ActivarRevisarAutorizacion();

        }
        private void ActivarRevisarExistencias()
        {
            Model.RequisicionEDTO _reqEDTO = new Model.RequisicionEDTO();
            _reqEDTO = new Servicio.RequsicionServicio().BuscarRequisicionByNumRequi(Request.QueryString["nr"].ToString(), Session["StringToken"].ToString());

            CargarUsuariosSolicitante(_reqEDTO.IdEmpresa);

            divDatos1.Visible = false;
            divDatos2.Visible = false;
            divOpinion.Visible = true;
            lblRuta.Text = "Requisición / Revisar Existencias";
            lblIdRequisicion.Text = Request.QueryString["nr"].ToString();
            btnCancelar.Enabled = true;
            BtnCrear.Text = "Finalizar";
            ddlSolicitante.Enabled = false;
            txtFechaRequerida.Disabled = true;
            txtMotivoCompra.Enabled = false;
            txtRequeridoEn.Enabled = false;
            txtFechaRequerida.Value = _reqEDTO.FechaRequerida.ToShortDateString();
            txtMotivoCompra.Text = _reqEDTO.MotivoRequisicion;
            txtRequeridoEn.Text = _reqEDTO.RequeridoEn;
            ddlSolicitante.SelectedValue = _reqEDTO.IdUsuarioSolicitante.ToString();

            dgListaproductos.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = new Servicio.RequsicionServicio().ToGridDTO(_reqEDTO.ListaProductos);
            dgListaproductos.DataBind();
            dgListaproductos.Columns[5].Visible = false;
            dgListaproductos.Columns[6].Visible = true;
            dgListaproductos.Columns[7].Visible = true;
        }
        private void ActivarRevisarAutorizacion()
        {
            Model.RequisicionEDTO _reqEDTO = new Model.RequisicionEDTO();
            _reqEDTO = new Servicio.RequsicionServicio().BuscarRequisicionByNumRequi(Request.QueryString["nr"].ToString(), Session["StringToken"].ToString());

            divOpinion.Visible = true;
            divOpinion.Disabled = true;
            divDatos1.Visible = false;
            divDatos2.Visible = false;
            lblRuta.Text = "Requisición / Autorización";
            BtnCrear.Text = "Autorizar";
            btnCancelar.Enabled = true;
            ddlSolicitante.Enabled = false;
            txtFechaRequerida.Disabled = true;
            txtMotivoCompra.Enabled = false;
            txtRequeridoEn.Enabled = false;

            dgListaproductos.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = new Servicio.RequsicionServicio().ToGridDTO(_reqEDTO.ListaProductos);
            dgListaproductos.DataBind();
            dgListaproductos.Columns[5].Visible = false;
            dgListaproductos.Columns[8].Visible = true;
            dgListaproductos.Columns[9].Visible = true;
            dgListaproductos.Columns[10].Visible = true;
        }
        private void BorrarProductoLista(int id)
        {
            List<Model.RequisicionProductoGridDTO> lprod = (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"];
            Model.RequisicionProductoGridDTO prod = lprod.SingleOrDefault(x => x.IdProducto.Equals(id));
            lprod.Remove(prod);
            if (lprod.Count.Equals(0))
            {
                ViewState["ListaRequisicionProductoGridDTO"] = new List<Model.RequisicionProductoGridDTO>();
                dgListaproductos.DataSource = null;
            }
            else
                dgListaproductos.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = lprod;

            dgListaproductos.DataBind();
        }
        private void EditarProductoLista(int id)
        {
            List<Model.RequisicionProductoGridDTO> lprod = (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"];
            Model.RequisicionProductoGridDTO prod = lprod.SingleOrDefault(x => x.IdTipoProducto.Equals(id));
            lprod.Remove(prod);
            ddlTipoCompra.SelectedValue = prod.IdTipoProducto.ToString();
            ddlProdcutos.SelectedValue = prod.IdProducto.ToString();
            txtCantidad.Text = prod.Cantidad.ToString();
            ddlCentroCostos.SelectedValue = prod.IdCentroCosto.ToString();
            txtDetalle.Text = prod.Aplicacion;
            BtnCrear.Enabled = false;
            btnAgregar.Text = "Terminar edicion";
            if (lprod.Count.Equals(0))
                ViewState["ListaRequisicionProductoGridDTO"] = new List<Model.RequisicionProductoGridDTO>();
            else
                dgListaproductos.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = lprod;
        }
        private void CargarUsuariosSolicitante(short idEmpresa)
        {
            ddlSolicitante.DataSource = new Servicio.RequsicionServicio().ListaUsuarios(idEmpresa, Session["StringToken"].ToString());
            ddlSolicitante.Items.Insert(0, new ListItem { Text = "Solicitante...", Value = "0" });
            ddlSolicitante.DataTextField = "NombreUsuario";
            ddlSolicitante.DataValueField = "IdUsuario";
            ddlSolicitante.DataBind();
        }
        private void CargarProductos(short idEmpresa)
        {
            ddlProdcutos.DataSource = new Servicio.RequsicionServicio().ListaPriductos(idEmpresa, Session["StringToken"].ToString());
            ddlProdcutos.Items.Insert(0, new ListItem { Text = "Producto...", Value = "0" });
            ddlProdcutos.DataTextField = "Descripcion";
            ddlProdcutos.DataValueField = "IdProducto";
            ddlProdcutos.DataBind();
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposProductos())
            {
                List<Model.RequisicionProductoGridDTO> LProductos = new List<Model.RequisicionProductoGridDTO>();
                if (ValidarProductoRepetido(int.Parse(ddlProdcutos.SelectedValue.ToString())))
                {
                    LProductos = ((List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"]).Where(x => x.IdProducto.Equals(int.Parse(ddlProdcutos.SelectedValue.ToString())))
                         .Select(x => { x.Cantidad = x.Cantidad + decimal.Parse(txtCantidad.Text); x.Aplicacion = x.Aplicacion + "|" + txtDetalle.Text; return x; }).ToList();
                }
                else
                {
                    LProductos = (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"] == null ? new List<Model.RequisicionProductoGridDTO>() : (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"];
                    LProductos = new Servicio.RequsicionServicio().GenerarListaGrid(LProductos, new Servicio.RequsicionServicio().GenerarProductoGrid(ddlTipoCompra, ddlProdcutos, ddlCentroCostos, txtDetalle.Text, Convert.ToDecimal(txtCantidad.Text)));
                }
                dgListaproductos.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = LProductos;
                dgListaproductos.DataBind();
                LimpiarCamposProductos();
                BtnCrear.Enabled = true;
                btnAgregar.Text = "Agregar";
            }
        }
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            Servicio.RequsicionServicio serv = new Servicio.RequsicionServicio();
            if (BtnCrear.Text.Equals("Crear"))
            {
                if (ValidarCampos())
                {
                    Model.RequisicionEDTO Edto = serv.UnirDtos(CrearReq(), serv.ToDTO((List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"]));
                    var respuesta = serv.GuardarRequisicion(Edto, Session["StringToken"].ToString());
                    if (respuesta != null)
                    {
                        if (respuesta.Exito)
                        {
                            lblNoRequisicion.Text = respuesta.NumRequisicion;
                            lblIdRequisicion.Text = respuesta.NumRequisicion;
                            divNoRequi.Visible = true;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Aviso", "alert('" + respuesta.Mensaje + "')", true);
                            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('" + respuesta.Mensaje + "');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Aviso", "alert('Verifique los datos')", true);
                }
            }
            if (BtnCrear.Text.Equals("Finalizar"))
            {
                serv.ActualizarRequisicionRevision(new Model.RequisicionEDTO { }, Session["StringToken"].ToString());
            }
        }
        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlEmpresas.SelectedValue.Equals(-1))
            {
                CargarUsuariosSolicitante(Convert.ToInt16(ddlEmpresas.SelectedValue));
                CargarProductos(Convert.ToInt16(ddlEmpresas.SelectedValue));
            }
        }
        protected void dgListaproductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Borrar"))
            {
                BorrarProductoLista(int.Parse(e.CommandArgument.ToString()));
            }
            if (e.CommandName.Equals("Editar"))
            {
                EditarProductoLista(int.Parse(e.CommandArgument.ToString()));
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalCancelar", "$('#ModalCancelar').modal();", true);
            upModalCancelar.Update();
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Compras/Vistas/Compras.aspx");
        }
    }
}