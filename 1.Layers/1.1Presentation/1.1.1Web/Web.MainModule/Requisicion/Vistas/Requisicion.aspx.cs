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
        List<Model.RequisicionProductoGridDTO> LProductos = new List<Model.RequisicionProductoGridDTO>();
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
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            LProductos = (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"] == null ? new List<Model.RequisicionProductoGridDTO>() : (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"];
            LProductos = new Servicio.RequsicionServicio().GenerarListaGrid(LProductos, new Servicio.RequsicionServicio().GenerarProductoGrid(ddlTipoCompra, ddlProdcutos, ddlCentroCostos, txtDetalle.Text, Convert.ToDecimal(txtCantidad.Text)));
            dgListaproductos.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = LProductos;
            dgListaproductos.DataBind();
        }
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            Servicio.RequsicionServicio serv = new Servicio.RequsicionServicio();
            if (BtnCrear.Text.Equals("Crear"))
            {              
                if (ValidarCampos())
                {
                    Model.RequisicionEDTO Edto = serv.UnirDtos(new Model.RequisicionDTO
                    {
                        IdUsuarioSolicitante = Convert.ToInt32(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "IdUsuario").Value),
                        IdEmpresa = Convert.ToInt16(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "IdEmpresa").Value),
                        NumeroRequisicion = string.Empty,
                        IdRequisicionEstatus = 1,
                        FechaRequerida = DateTime.Today.AddDays(5), //DateTime.Parse(txtFechaRequerida.Value, new CultureInfo("en-US")), //(es-MX) 
                        MotivoRequisicion = txtMotivoCompra.Text,
                        RequeridoEn = txtRequeridoEn.Text,
                        FechaRegistro = DateTime.Today,
                    }, serv.ToDTO((List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"]));
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
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Aviso", "alert('" + respuesta.Mensaje + "')", true);
                    }
                }
            }
            if (BtnCrear.Text.Equals("Finalizar"))
            {
                serv.ActualizarRequisicionRevision(new Model.RequisicionEDTO { }, Session["StringToken"].ToString());
            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Compras/Vistas/Compras.aspx");
        }
        private bool ValidarCampos()
        {
            return true;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalCancelar", "$('#ModalCancelar').modal();", true);
            upModalCancelar.Update();
        }
        private void RquisicionAlternativa(string NumRueq, int Estatus)
        {
            //ActivarRevisarExistencias();
            if (Estatus.Equals(1))
            {
                ActivarRevisarExistencias();
            }
            else
            {
                ActivarRevisarAutorizacion();
            }
        }
        private void ActivarRevisarExistencias()
        {
            Model.RequisicionEDTO _reqEDTO = new Model.RequisicionEDTO();
            _reqEDTO = new Servicio.RequsicionServicio().BuscarRequisicionByNumRequi(Request.QueryString["nr"].ToString(), Session["StringToken"].ToString());

            divDatos1.Visible = false;
            divDatos2.Visible = false;
            divOpinion.Visible = true;
            lblRuta.Text = "Requisición / Revisar Existencias";
            lblIdRequisicion.Text = Request.QueryString["nr"].ToString();
            btnCancelar.Enabled = true;
            BtnCrear.Text = "Finalizar";
            txtFechaRequerida.Disabled = true;
            txtSolicitante.Enabled = false;
            txtMotivoCompra.Enabled = false;
            txtRequeridoEn.Enabled = false;
            txtFechaRequerida.Value = _reqEDTO.FechaRequerida.ToShortDateString();
            txtMotivoCompra.Text = _reqEDTO.MotivoRequisicion;
            txtRequeridoEn.Text = _reqEDTO.RequeridoEn;
            txtSolicitante.Text = _reqEDTO.IdUsuarioSolicitante.ToString();// Buscar al solicitante por el ID

            dgListaproductos.DataSource = ViewState["ListaRequisicionProductoEDTO"] = new Servicio.RequsicionServicio().ToGridDTO(_reqEDTO.ListaProductos);
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
            txtFechaRequerida.Disabled = true;
            txtSolicitante.Enabled = false;
            txtMotivoCompra.Enabled = false;
            txtRequeridoEn.Enabled = false;

            dgListaproductos.DataSource = ViewState["ListaRequisicionProductoEDTO"] = _reqEDTO.ListaProductos;
            dgListaproductos.DataBind();
            dgListaproductos.Columns[5].Visible = false;
            dgListaproductos.Columns[8].Visible = true;
            dgListaproductos.Columns[9].Visible = true;
            dgListaproductos.Columns[10].Visible = true;
        }
    }
}