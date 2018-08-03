﻿using System;
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
                        if (Request.QueryString["NoRequisicion"] != null)
                        {
                            ActivarRevisarExistencias();
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
            LProductos = new Serivicio.RequsicionServicio().GenerarListaGrid(LProductos, new Serivicio.RequsicionServicio().GenerarProductoGrid(ddlTipoCompra, ddlProdcutos, ddlCentroCostos, txtDetalle.Text, Convert.ToDecimal(txtCantidad.Text)));
            dgListaproductos.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = LProductos;
            dgListaproductos.DataBind();
        }
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            Serivicio.RequsicionServicio serv = new Serivicio.RequsicionServicio();
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
        protected void btnRegresar_Click(object sender, EventArgs e)
        {

        }
        private bool ValidarCampos()
        {
            return true;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        private void ActivarRevisarExistencias()
        {
            lblRuta.Text = "Requisición / Revisar Existencias";
            lblIdRequisicion.Text = Request.QueryString["NoRequisicion"].ToString();
            divDatos1.Visible = false;
            divDatos2.Visible = false;
            txtFechaRequerida.Disabled = true;
            txtSolicitante.Enabled = false;
            txtMotivoCompra.Enabled = false;
            txtRequeridoEn.Enabled = false;
            divOpinion.Visible = true;
        }
        private void ActivarRevisarAutorizacion()
        {
            lblRuta.Text = "Requisición / Autorización";
            divDatos1.Visible = false;
            divDatos2.Visible = false;
            txtFechaRequerida.Disabled = true;
            txtSolicitante.Enabled = false;
            txtMotivoCompra.Enabled = false;
            txtRequeridoEn.Enabled = false;
            divOpinion.Visible = true;
            divOpinion.Disabled = true;
        }
    }
}