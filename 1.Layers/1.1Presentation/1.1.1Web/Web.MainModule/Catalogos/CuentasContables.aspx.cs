using Application.MainModule.Servicios.Requisicion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities.MainModule;
using Web.MainModule.Catalogos.Model;
using Web.MainModule.Catalogos.Servicio;
using Web.MainModule.Seguridad.Servicio;

namespace Web.MainModule.Catalogos
{
    public partial class CuentasContables : System.Web.UI.Page
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
                        CargarGaseras();
                    }
                    else
                        Salir();
                }
            }
        }
        private void Salir()
        {
            Response.Redirect("../../Login.aspx");
        }
        private void CargarGaseras()
        {
            ddlEmpresas.DataSource = new CuentaContableServicio().BuscarGaseras(_tok).Where(x => x.EsAdministracionCentral.Equals(false)).ToList();
            ddlEmpresas.DataTextField = "NombreComercial";
            ddlEmpresas.DataValueField = "IdEmpresa";
            ddlEmpresas.DataBind();
            if (!TokenServicio.ObtenerEsAdministracionCentral(_tok))            
                ddlEmpresas.Enabled = false;
            
        }
        private void CargarCtasCtbles()
        {
            gvCuentasContables.DataSource = ViewState["List<CuentaContableServicio>"] = new CuentaContableServicio().ListaCtaCtble(_tok);
            gvCuentasContables.DataBind();
        }
        private CuentaContableDTO GenerarCtaCtble()
        {
            CuentaContableDTO cc = new CuentaContableDTO();
            cc.IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tok);
            cc.Numero = txtNumero.Text;
            cc.Descripcion = txtDesc.Text;
            cc.FechaRegistro = DateTime.Today;
            return cc;
        }
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            CatalogoRespuestaDTO crDTO = new CatalogoRespuestaDTO();
            CuentaContableDTO cc = GenerarCtaCtble();
            var Validar = ValidadorClases.EnlistaErrores(cc);
            if (Validar.ModeloValido)
            {
                crDTO = new CuentaContableServicio().GuardarCtaCtble(cc, _tok);
                if (crDTO.Exito)
                {
                    CargarCtasCtbles();
                }
            }
        }
        private void ValidarCampos(List<Result> list)
        {
            if (list.Exists(x => x.IdentidadError.Equals("Numero"))) { reqNum.Visible = true; reqNum.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Numero")).MensajeError; }
            else reqNum.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("Descripcion"))) { reqDesc.Visible = true; reqDesc.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Descripcion")).MensajeError; }
            else reqDesc.Visible = false;
        }
        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void gvCuentasContables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {

            }
            if (e.CommandName.Equals("Borrar"))
            {

            }
        }
    }
}