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

namespace Web.MainModule.Catalogos.Vista
{
    public partial class CentrosCosto : System.Web.UI.Page
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
                        CargarGaseras(TokenServicio.ObtenerIdEmpresa(_tok));
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
            if (TokenServicio.ObtenerEsAdministracionCentral(_tok))
                ddlFiltroGasera.DataSource = new EmpresaServicio().Empresas(_tok).Where(x => x.EsAdministracionCentral.Equals(false)).ToList();
            else
                ddlFiltroGasera.DataSource = new EmpresaServicio().Empresas(_tok).Where(x => x.EsAdministracionCentral.Equals(false) && x.IdEmpresa.Equals(idEmpresa)).ToList();
            ddlFiltroGasera.DataTextField = "NombreComercial";
            ddlFiltroGasera.DataValueField = "IdEmpresa";
            ddlFiltroGasera.DataBind();
            if (!TokenServicio.ObtenerEsAdministracionCentral(_tok))
                ddlFiltroGasera.Enabled = false;
            CargarCentrosCosto(short.Parse(ddlFiltroGasera.SelectedValue));
        }
        private void CargarCentrosCosto(short idEmpresa)
        {
            gvCentroCosto.DataSource = ViewState["List<CentroCostoDTO>"] = new CentroCostoServicio().BuscarCentrosCosto(_tok).Where(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
            gvCentroCosto.DataBind();
        }
        private void FiltrarCentrosCosto()
        {
            List<CentroCostoDTO> lcc = (List<CentroCostoDTO>)ViewState["List<CentroCostoDTO>"];
            if (!ddlFiltroCentroCosto.SelectedIndex.Equals(-1))
                lcc = lcc.Where(x => x.IdEmpresa.Equals(short.Parse(ddlFiltroCentroCosto.SelectedValue))).ToList();
            if (string.IsNullOrEmpty(txtFiltroNumero.Text))
                lcc = lcc.Where(x => x.Numero.Equals(txtFiltroNumero.Text)).ToList();
            if (ddlFiltroCentroCosto.SelectedIndex.Equals(-1))
                lcc = lcc.Where(x => x.IdTipoCentroCosto.Equals(byte.Parse(ddlFiltroCentroCosto.SelectedValue))).ToList();

        }
        private void ActivarModificar(int idCetroCosto)
        {
            var centrocosto = ((List<CentroCostoDTO>)ViewState["List<CentroCostoDTO>"]).ToList().SingleOrDefault(x => x.IdCentroCosto.Equals(idCetroCosto));
            txtNumeroCentroCosto.Text = centrocosto.Numero;
            txtDescripcion.Text = centrocosto.Descripcion;
            ddlTipoCentroCosto.SelectedValue = centrocosto.IdTipoCentroCosto.ToString();
            ddlEquipoTransporte.SelectedValue = centrocosto.IdEquipoTransporte.ToString();
            ddlVehiculo.SelectedValue = centrocosto.IdVehiculoUtilitario.ToString();
            ddlUnidadAlmacenGas.SelectedValue = centrocosto.IdCAlmacenGas.ToString();
            ddlEstacionCarburacion.SelectedValue = centrocosto.IdEstacionCarburacion.ToString();
            ddlCamioneta.SelectedValue = centrocosto.IdCamioneta.ToString();
            ddlPipa.SelectedValue = centrocosto.IdPipa.ToString();
            ddlCilindroGas.SelectedValue = centrocosto.IdCilindro.ToString();
            iSimboloBoton.Attributes.Remove("class");
            iSimboloBoton.Attributes.Add("class", "zmdi zmdi-swap");
            ViewState["intidCetroCosto"] = idCetroCosto;
        }
        private void ModificarCentroCosto(int IdCC)
        {
            var ccMod = GenerarCentroCostoMod(IdCC);
            var validacion = ValidadorClases.EnlistaErrores(ccMod);
            if (validacion.ModeloValido)
            {
                ValidarCampos(validacion.MensajesError);//Se manda la lista para limpiar las etiquetas de requerido
                var respuesta = new CentroCostoServicio().ModificarCentroCosto(ccMod, _tok);
                if (respuesta.Exito)
                {
                    DivAlerta.Visible = false;
                    lblMensajeError.Text = string.Empty;
                    CargarCentrosCosto(short.Parse(ddlFiltroGasera.SelectedValue));
                    ViewState["intidCetroCosto"] = null;
                }
                else
                {
                    DivAlerta.Visible = true;
                    lblMensajeError.Text = respuesta.MensajesError[0];
                }
            }
            else
                ValidarCampos(validacion.MensajesError);
        }
        private CentroCostoModificarDto GenerarCentroCostoMod(int IdCentroCosto)
        {
            var cc = ((List<CentroCostoDTO>)ViewState["List<CentroCostoDTO>"]).ToList().SingleOrDefault(x => x.IdCentroCosto.Equals(IdCentroCosto));
            return new CentroCostoModificarDto
            {
                IdCentroCosto = cc.IdCentroCosto,
                IdEmpresa = cc.IdEmpresa,
                IdTipoCentroCosto = byte.Parse(ddlTipoCentroCosto.SelectedValue),
                IdEquipoTransporte = int.Parse(ddlEquipoTransporte.SelectedValue),
                IdVehiculoUtilitario = int.Parse(ddlVehiculo.SelectedValue),
                IdCAlmacenGas = short.Parse(ddlUnidadAlmacenGas.SelectedValue),
                IdEstacionCarburacion = int.Parse(ddlEstacionCarburacion.SelectedValue),
                IdCamioneta = int.Parse(ddlCamioneta.SelectedValue),
                IdPipa = int.Parse(ddlPipa.SelectedValue),
                IdCilindro = int.Parse(ddlCilindroGas.SelectedValue),
                Numero = txtNumeroCentroCosto.Text,
                Descripcion = txtDescripcion.Text
            };
        }
        private CentroCostoCrearDto GenerarCentroCostoCrear()
        {
            return new CentroCostoCrearDto()
            {
                IdEmpresa = short.Parse(ddlFiltroCentroCosto.SelectedValue),
                IdTipoCentroCosto = byte.Parse(ddlTipoCentroCosto.SelectedValue),
                IdEquipoTransporte = int.Parse(ddlEquipoTransporte.SelectedValue),
                IdVehiculoUtilitario = int.Parse(ddlVehiculo.SelectedValue),
                IdCAlmacenGas = short.Parse(ddlUnidadAlmacenGas.SelectedValue),
                IdEstacionCarburacion = int.Parse(ddlEstacionCarburacion.SelectedValue),
                IdCamioneta = int.Parse(ddlCamioneta.SelectedValue),
                IdPipa = int.Parse(ddlPipa.SelectedValue),
                IdCilindro = int.Parse(ddlCilindroGas.SelectedValue),
                Numero = txtNumeroCentroCosto.Text,
                Descripcion = txtDescripcion.Text
            };
        }
        private void EliminarCentroCosto(int idCetroCosto)
        {
            var respuesta = new CentroCostoServicio().EliminarCentroCosto(new CentroCostoEliminarDto { IdCentroCosto = idCetroCosto }, _tok);
            if (respuesta.Exito)
            {
                DivAlerta.Visible = false;
                lblMensajeError.Text = string.Empty;
                CargarCentrosCosto(short.Parse(ddlFiltroGasera.SelectedValue));
            }
            else
            {
                DivAlerta.Visible = true;
                lblMensajeError.Text = respuesta.MensajesError[0];
            }
        }
        private void GuardarNuevoCentroCosto()
        {
            var nuevoCC = GenerarCentroCostoCrear();
            var validacion = ValidadorClases.EnlistaErrores(nuevoCC);
            if (validacion.ModeloValido)
            {
                ValidarCampos(validacion.MensajesError);//Se manda la lista para limpiar las etiquetas de requerido
                var respuesta = new CentroCostoServicio().NuevoCentroCosto(nuevoCC, _tok);
                if (respuesta.Exito)
                {
                    DivAlerta.Visible = false;
                    lblMensajeError.Text = string.Empty;
                    CargarCentrosCosto(short.Parse(ddlFiltroGasera.SelectedValue));
                }
                else
                {
                    DivAlerta.Visible = true;
                    lblMensajeError.Text = respuesta.MensajesError[0];
                }
            }
            else
                ValidarCampos(validacion.MensajesError);
        }
        private void ValidarCampos(List<Result> list)
        {
            if (list.Exists(x => x.IdentidadError.Equals("Numero"))) { reqNumCC.Visible = true; reqNumCC.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Numero")).MensajeError; }
            else reqNumCC.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("Descripcion"))) { reqDescripcion.Visible = true; reqDescripcion.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("IdUsuarioSolicitante")).MensajeError; }
            else reqDescripcion.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("IdTipoCentroCosto"))) { reqTipoCC.Visible = true; reqTipoCC.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("MotivoRequisicion")).MensajeError; }
            else reqTipoCC.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("IdEquipoTransporte"))) { reqEquipo.Visible = true; reqEquipo.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("RequeridoEn")).MensajeError; }
            else reqEquipo.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("IdVehiculoUtilitario"))) { reqVehiculo.Visible = true; reqVehiculo.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Aplicacion")).MensajeError; }
            else reqVehiculo.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("IdCAlmacenGas"))) { reqUnidad.Visible = true; reqUnidad.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Aplicacion")).MensajeError; }
            else reqUnidad.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("IdEstacionCarburacion"))) { reqEstacion.Visible = true; reqEstacion.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Aplicacion")).MensajeError; }
            else reqEstacion.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("IdCamioneta"))) { reqCamnioneta.Visible = true; reqCamnioneta.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Aplicacion")).MensajeError; }
            else reqCamnioneta.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("IdPipa"))) { reqPipa.Visible = true; reqPipa.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Aplicacion")).MensajeError; }
            else reqPipa.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("IdCilindro"))) { reqCilindro.Visible = true; reqCilindro.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Aplicacion")).MensajeError; }
            else reqCilindro.Visible = false;
        }
        protected void gvCentroCosto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
                ActivarModificar(int.Parse(e.CommandArgument.ToString()));

            if (e.CommandName.Equals("Borrar"))
                EliminarCentroCosto(int.Parse(e.CommandArgument.ToString()));
        }
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            if (ViewState["intidCetroCosto"] == null)
                GuardarNuevoCentroCosto();
            else
                ModificarCentroCosto((int)ViewState["intidCetroCosto"]);
        }
    }
}