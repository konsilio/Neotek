using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security.MainModule.Token_Service;
using System.Security.Claims;

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
                    _tok  =  Session["StringToken"].ToString();
                    Claim _autenticado = TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(_tok, "Autenticado");
                    if (Convert.ToBoolean(_autenticado.Value))
                    {
                        dgListaproductos.DataBind();
                        //Habilitar opciones segun el rol
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
            LProductos = new Serivicio.RequsicionServicio().GenerarListaGrid(LProductos, new Serivicio.RequsicionServicio().GenerarProductoGrid(ddlTipoCompra ,ddlProdcutos, ddlCentroCostos, txtDetalle.Text, Convert.ToDecimal(txtCantidad.Text)));
            dgListaproductos.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = LProductos;
            dgListaproductos.DataBind();
        }

        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            Serivicio.RequsicionServicio serv = new Serivicio.RequsicionServicio();
            if (ValidarCampos())
            {
                Model.RequisicionEDTO Edto = serv.UnirDtos(new Model.RequisicionDTO {
                    //IdRequisicion = 0,
                    IdUsuarioSolicitante = 1,
                    IdEmpresa = 1,
                    NumeroRequisicion = string.Empty,
                    IdRequisicionEstatus = 1,
                    FechaRequerida = DateTime.Today.AddDays(5),
                    MotivoRequisicion = txtMotivoCompra.Text,
                    RequeridoEn = txtRequeridoEn.Text,
                    FechaRegistro = DateTime.Today,
                    //IdUsuarioRevision = 0,
                    //OpinionAlmacen = string.Empty,
                    //MotivoCancelacion = string.Empty,
                    //IdUsuarioAutorizacion = 0,
                    //FechaAutorizacion = DateTime.Today,
                    //FechaRevision = DateTime.Today
                    }, serv.ToDTO((List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"]));
                var respuesta = serv.GuardarRequisicion(Edto, Session["StringToken"].ToString());
                if (respuesta.Exito)
                {
                    lblNombreEmpresa.Text = respuesta.Mensaje;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#ModalNumRequi').modal('show');</script>", false);
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
    }
}