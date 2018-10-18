using Application.MainModule.Servicios.Requisiciones;
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
                    if (TokenServicios.ObtenerAutenticado(_tok))
                    {
                        CargarGaseras(TokenServicios.ObtenerIdEmpresa(_tok));
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
        private void CargarGaseras(short idEmpresa)
        {
            ddlEmpresas.DataSource = new CuentaContableServicio().BuscarGaseras(_tok).Where(x => x.EsAdministracionCentral.Equals(false)).ToList();
            ddlEmpresas.DataTextField = "NombreComercial";
            ddlEmpresas.DataValueField = "IdEmpresa";
            ddlEmpresas.DataBind();
            if (!TokenServicios.ObtenerEsAdministracionCentral(_tok))
                ddlEmpresas.Enabled = false;
            CargarCtasCtbles(short.Parse(ddlEmpresas.SelectedValue));
        }
        private void CargarCtasCtbles(short idEmpresa)
        {
            gvCuentasContables.DataSource = ViewState["List<CuentaContableDTO>"] = new CuentaContableServicio().ListaCtaCtble(idEmpresa , _tok);
            gvCuentasContables.DataBind();
            if (!TokenServicios.ObtenerEsAdministracionCentral(_tok))
                gvCuentasContables.Columns[0].Visible = false;
        }
        private CuentaContableCrearDto GenerarCtaCtble()
        {
            CuentaContableCrearDto cc = new CuentaContableCrearDto();
            cc.IdEmpresa = TokenServicios.ObtenerIdEmpresa(_tok);
            cc.Numero = txtNumero.Text;
            cc.Descripcion = txtDesc.Text;
            return cc;
        }
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            if (ViewState["CuentaContableModificarDto"] == null)
            {
                GuardarNuevaCtaContable();
            }
            else
            {
                ModificarCtaContable();
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
            CargarGaseras(short.Parse(ddlEmpresas.SelectedValue));
        }
        protected void gvCuentasContables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
                ActicarModificar(int.Parse(e.CommandArgument.ToString()));

            if (e.CommandName.Equals("Borrar"))
                EliminarCtaContable(int.Parse(e.CommandArgument.ToString()));

        }
        private CuentaContableModificarDto CrearCCmod(int id)
        {
            var cc = ((List<CuentaContableDTO>)ViewState["List<CuentaContableDTO>"]).SingleOrDefault(x => x.IdCuentaContable.Equals(id));
            return new CuentaContableModificarDto()
            {
                IdCuenta = cc.IdCuentaContable,
                Descripcion = cc.Descripcion,
                IdEmpresa = cc.IdEmpresa,
                Numero = cc.Numero
            };
        }
        private CuentaContableEliminarDto CrearCCEliminar(int id)
        {
            var cc = ((List<CuentaContableDTO>)ViewState["List<CuentaContableDTO>"]).SingleOrDefault(x => x.IdCuentaContable.Equals(id));
            return new CuentaContableEliminarDto()
            {
                IdCuenta = cc.IdCuentaContable
            };
        }
        private void EliminarCtaContable(int idCC)
        {
            var resp = new CuentaContableServicio().EliminarCtaContable(CrearCCEliminar(idCC), _tok);
            if (resp.Exito)
            {
                CargarCtasCtbles(short.Parse(ddlEmpresas.SelectedValue));
                DivAlerta.Visible = false;
            }
            else
            {
                if (resp.MensajesError != null)
                    lblErrorPord.Text = resp.MensajesError[0];
                else
                    lblErrorPord.Text = "Algo salio mal";
                DivAlerta.Visible = true;
            }
        }
        private void CargarDatosParaModificar(int idCC)
        {
            var _cc = CrearCCmod(idCC);
            txtNumero.Text = _cc.Numero;
            txtDesc.Text = _cc.Descripcion;
            ViewState["CuentaContableModificarDto"] = _cc;
        }
        private void ModificarCtaContable()
        {
            CuentaContableModificarDto ccmDTO = (CuentaContableModificarDto)ViewState["CuentaContableModificarDto"];
            ccmDTO.Numero = txtNumero.Text;
            ccmDTO.Descripcion = txtDesc.Text;
            var resp = new CuentaContableServicio().ModificarCtaContable(ccmDTO, _tok);
            if (resp.Exito)
            {
                CargarCtasCtbles(short.Parse(ddlEmpresas.SelectedValue));
                DivAlerta.Visible = false;
                ViewState["CuentaContableModificarDto"] = null;
            }
            else
            {
                if (resp.MensajesError != null)
                    lblErrorPord.Text = resp.MensajesError[0];
                else
                    lblErrorPord.Text = "Algo salio mal";
                DivAlerta.Visible = true;
            }
            txtDesc.Text = string.Empty;
            txtNumero.Text = string.Empty;
            iSimboloBoton.Attributes.Remove("class");
            iSimboloBoton.Attributes.Add("class", "zmdi zmdi-plus");
        }
        private void GuardarNuevaCtaContable()
        {
            CuentaContableCrearDto cc = GenerarCtaCtble();
            var Validar = ValidadorClases.EnlistaErrores(cc);
            if (Validar.ModeloValido)
            {
                var resp = new CuentaContableServicio().GuardarCtaCtble(cc, _tok);
                if (resp.Exito)
                {
                    CargarCtasCtbles(short.Parse(ddlEmpresas.SelectedValue));
                    txtDesc.Text = string.Empty;
                    txtNumero.Text = string.Empty;
                }
                else
                {
                    if (resp.MensajesError != null)
                        lblErrorPord.Text = resp.MensajesError[0];
                    else
                        lblErrorPord.Text = "Algo salio mal";
                    DivAlerta.Visible = true;
                }
            }
        }
        private void ActicarModificar(int idCC)
        {
            CargarDatosParaModificar(idCC);
            iSimboloBoton.Attributes.Remove("class");
            iSimboloBoton.Attributes.Add("class", "zmdi zmdi-swap");
        }
        protected void txtNumeroCtaCtble_TextChanged(object sender, EventArgs e)
        {
            if (ViewState["List<CuentaContableDTO>"] != null)
            {
                List<CuentaContableDTO> lcc = ((List<CuentaContableDTO>)ViewState["List<CuentaContableDTO>"]).ToList().Where(x => x.Numero.Equals(txtNumeroCtaCtble.Text)).ToList();
                gvCuentasContables.DataSource = lcc;
                gvCuentasContables.DataBind();
                if (!TokenServicios.ObtenerEsAdministracionCentral(_tok))
                    gvCuentasContables.Columns[0].Visible = false;
            }
        }
    }
}