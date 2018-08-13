using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security.MainModule.Token_Service;
using System.Security.Claims;
using Utilities.MainModule;
using System.Globalization;
using System.ComponentModel.DataAnnotations;


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
        private Model.RequisicionCrearDTO CrearReq()
        {
            Model.RequisicionCrearDTO reqCrearDTO = new Model.RequisicionCrearDTO();
            if (Convert.ToBoolean(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "EsAdminCentral").Value))
            {
                reqCrearDTO.IdEmpresa = short.Parse(ddlEmpresas.SelectedValue);
                reqCrearDTO.IdUsuarioSolicitante = int.Parse(ddlSolicitante.SelectedValue);
            }
            else
            {
                reqCrearDTO.IdUsuarioSolicitante = Convert.ToInt32(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "IdUsuario").Value);
                reqCrearDTO.IdEmpresa = Convert.ToInt16(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "IdEmpresa").Value);
            }
            reqCrearDTO.MotivoRequisicion = txtMotivoCompra.Text;
            reqCrearDTO.RequeridoEn = txtRequeridoEn.Text;
            reqCrearDTO.IdRequisicionEstatus = (byte)Model.RequisiconEstatus.Estatus.Creada;
            reqCrearDTO.FechaRequerida = DateTime.Today.AddDays(5); //DateTime.Parse(txtFechaRequerida.Value, new CultureInfo("en-US")), //(es-MX) 
            reqCrearDTO.FechaRegistro = DateTime.Today;
            reqCrearDTO.ListaProductos = (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"];
            return reqCrearDTO;
        }
        private void ValidarCampos(List<Result> list)
        {
            if (list.Exists(x => x.IdentidadError.Equals("FechaRequerida"))) { reqFecha.Visible = true; reqFecha.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("FechaRequerida")).MensajeError; }
            else reqFecha.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("IdUsuarioSolicitante"))) { reqSol.Visible = true; reqSol.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("IdUsuarioSolicitante")).MensajeError; }
            else reqSol.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("MotivoRequisicion"))) { reqMotivo.Visible = true; reqMotivo.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("MotivoRequisicion")).MensajeError; }
            else reqMotivo.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("RequeridoEn"))) { reqReq.Visible = true; reqReq.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("RequeridoEn")).MensajeError; }
            else reqReq.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("ListaProductos"))) { reqGrid.Visible = true; reqGrid.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Aplicacion")).MensajeError; }
            else reqApli.Visible = false;

            if (list.Exists(x => x.IdentidadError.Equals("TipoProducto"))) { reqTipo.Visible = true; reqTipo.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("TipoProducto")).MensajeError; }
            else reqTipo.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("Producto"))) { reqProd.Visible = true; reqProd.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Producto")).MensajeError; }
            else reqProd.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("Cantidad"))) { reqCant.Visible = true; reqCant.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Cantidad")).MensajeError; }
            else reqCant.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("IdCentroCosto"))) { reqCC.Visible = true; reqCC.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("IdCentroCosto")).MensajeError; }
            else reqCC.Visible = false;
            if (list.Exists(x => x.IdentidadError.Equals("Aplicacion"))) { reqApli.Visible = true; reqApli.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Aplicacion")).MensajeError; }
            else reqApli.Visible = false;
        }
        private Model.RequisicionAutPutDTO CrearAut()
        {
            Model.RequisicionAutPutDTO _aut = new Model.RequisicionAutPutDTO();
            _aut.IdRequisicion = (int)ViewState["idRequisicion"];
            _aut.NumeroRequisicion = lblIdRequisicion.Text;
            _aut.FechaAutorizacion = DateTime.Today;
            if (Convert.ToBoolean(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "EsAdminCentral").Value))
                _aut.IdUsuarioAutorizacion = int.Parse(ddlSolicitante.SelectedValue);
            else
                _aut.IdUsuarioAutorizacion = Convert.ToInt32(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "IdUsuario").Value);
            _aut.ListaProductos = (List<Model.RequisicionProdAutPutDTO>)ViewState["ListaRequisicionProdAutPutDTO"];
            return _aut;
        }
        private bool ValidarRevisionAlmacen()
        {
            bool resp = true;
            if (txtOpinion.Text.Equals(string.Empty))
            {
                List<Model.RequisicionProdReviPutDTO> LProdPutDTO = new List<Model.RequisicionProdReviPutDTO>();
                
                foreach (GridViewRow _row in gvProductosRevision.Rows)
                {
                    if (_row.RowType.Equals(DataControlRowType.DataRow))
                        if (!(_row.Cells[0].FindControl("chbRevision") as CheckBox).Checked)
                        {
                            resp = false;
                            break;
                        }
                        else
                            LProdPutDTO.Add(new Model.RequisicionProdReviPutDTO { IdProducto = Int16.Parse((_row.Cells[0].FindControl("lbldgProductoID") as Label).Text), RevisionFisica = true });
                }
                if (!resp)
                {
                    DivCamposPord.Visible = true;
                    lblErrorPord.Text = "Debes revisar todos los productos en el almacen";
                }
                ViewState["LIstaReqProdRevPutDTO"] = LProdPutDTO;
            }
            else
            {
                reqOpinion.Visible = true;
                resp = false;
            }
            return resp;
        }
        private void LimpiarMensajesProd()
        {
            reqTipo.Visible = false;
            reqProd.Visible = false;
            reqCant.Visible = false;
            reqCC.Visible = false;
            reqCC.Visible = false;
            DivCamposPord.Visible = false;
            lblErrorPord.Text = string.Empty;
        }
        private bool ValidarListaprodcutos()
        {
            bool respu = true;
            if (ViewState["ListaRequisicionProductoGridDTO"] != null)
            {
                if (((List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"]).Count.Equals(0))
                {
                    respu = false;
                    reqGrid.Visible = true;
                    reqGrid.Text = Exceptions.MainModule.Validaciones.Error.R0007;
                }
            }
            else
            {
                respu = false;
                respu = false;
                reqGrid.Visible = true;
                reqGrid.Text = Exceptions.MainModule.Validaciones.Error.R0007;
            }
            return respu;
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
            Model.RequisicionRevisionDTO _reqEDTO = new Model.RequisicionRevisionDTO();
            _reqEDTO = new Servicio.RequsicionServicio().BuscarRequisicionByNumRequiRevi(Request.QueryString["nr"].ToString(), Session["StringToken"].ToString());
            ViewState["idRequisicion"] = _reqEDTO.IdRequisicion;
            CargarUsuariosSolicitante(_reqEDTO.IdEmpresa);
            divDatos1.Visible = false;
            divDatos2.Visible = false;
            divOpinion.Visible = true;
            lblRuta.Text = "Requisición / Revisar Existencias";
            lblIdRequisicion.Text = Request.QueryString["nr"].ToString();
            btnCancelar.Enabled = true;
            BtnCrear.Text = "Finalizar";           
            txtFechaRequerida.Enabled = false;
            txtMotivoCompra.Enabled = false;
            txtRequeridoEn.Enabled = false;
            txtFechaRequerida.Text = _reqEDTO.FechaRequerida.ToShortDateString();
            txtMotivoCompra.Text = _reqEDTO.MotivoRequisicion;
            txtRequeridoEn.Text = _reqEDTO.RequeridoEn;
            ddlSolicitante.Enabled = false;
            ddlSolicitante.SelectedValue = _reqEDTO.IdUsuarioSolicitante.ToString();

            dgListaproductos.Visible = false;
            gvProductosRevision.Visible = true;
            gvProductosRevision.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = _reqEDTO.ListaProductos;
            gvProductosRevision.DataBind();
        }
        private void ActivarRevisarAutorizacion()
        {
            Model.RequisicionAutorizacion _reqEDTO = new Model.RequisicionAutorizacion();
            _reqEDTO = new Servicio.RequsicionServicio().BuscarRequisicionByNumRequiAuto(Request.QueryString["nr"].ToString(), Session["StringToken"].ToString());
            ViewState["idRequisicion"] = _reqEDTO.IdRequisicion;
            CargarUsuariosSolicitante(_reqEDTO.IdEmpresa);
            txtOpinion.Text = _reqEDTO.OpinionAlmacen;
            txtOpinion.Enabled = false;
            txtFechaRequerida.Enabled = false;
            txtMotivoCompra.Enabled = false;
            txtRequeridoEn.Enabled = false;
            txtFechaRequerida.Text = _reqEDTO.FechaRequerida.ToShortDateString();
            txtMotivoCompra.Text = _reqEDTO.MotivoRequisicion;
            txtRequeridoEn.Text = _reqEDTO.RequeridoEn;
            divOpinion.Visible = true;
            divOpinion.Disabled = false;
            divDatos1.Visible = false;
            divDatos2.Visible = false;
            lblRuta.Text = "Requisición / Autorización";
            lblIdRequisicion.Text = _reqEDTO.NumeroRequisicion;
            BtnCrear.Text = "Autorizar";
            btnCancelar.Enabled = true;
            ddlSolicitante.Enabled = false;    
            ddlSolicitante.SelectedValue = _reqEDTO.IdUsuarioSolicitante.ToString();
            dgListaproductos.Visible = false;
            gvProductoAut.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = _reqEDTO.ListaProductos;
            gvProductoAut.DataBind();
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
            Model.RequisicionProductoGridDTO prod = lprod.SingleOrDefault(x => x.IdProducto.Equals(id));
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
        private void GuardarRequisicion()
        {
            Servicio.RequsicionServicio serv = new Servicio.RequsicionServicio();
            if (ValidarListaprodcutos())
            {
                Model.RequisicionCrearDTO Edto = CrearReq();
                var validacion = ValidadorClases.EnlistaErrores<Model.RequisicionCrearDTO>(Edto);
                if (validacion.ModeloValido)
                {
                    ValidarCampos(new List<Result>());//Se manda lista vacia para limpiar campos
                    var respuesta = serv.GuardarRequisicion(new Servicio.RequsicionServicio().UnirDtos(Edto), Session["StringToken"].ToString());
                    if (respuesta != null)
                    {
                        if (respuesta.Exito)
                        {
                            lblNoRequisicion.Text = respuesta.NumRequisicion;
                            lblIdRequisicion.Text = respuesta.NumRequisicion;
                            divNoRequi.Visible = true;
                            divCampos.Visible = false;
                            LimpiarCamposProductos();
                            LimpiarMensajesProd();
                        }
                        else
                        {
                            lblErrorCampos.Text = "Ocurrio un error";
                            divCampos.Visible = true;
                        }
                    }
                }
                else
                {
                    ValidarCampos(validacion.MensajesError);
                    lblErrorCampos.Text = "Verifique que los datos esten completos";
                    divCampos.Visible = true;
                }
            }
        }
        private void FinalizarRevision()
        {
            Servicio.RequsicionServicio serv = new Servicio.RequsicionServicio();
            if (ValidarRevisionAlmacen())
            {
                Model.RequisicionRevPutDTO dto = RequisicionRevisionDTO();
                var validacion = ValidadorClases.EnlistaErrores<Model.RequisicionRevPutDTO>(dto);
                if (validacion.ModeloValido)
                {
                    LimpiarMensajesProd();
                    Model.RespuestaRequisicionDto resp = new Model.RespuestaRequisicionDto();
                    resp = serv.ActualizarRequisicionRevision(RequisicionRevisionDTO(), Session["StringToken"].ToString());
                    if (resp.Exito)
                    {
                        lblNoRequisicion.Text = resp.Mensaje.Equals(string.Empty) ? "Correcto" : resp.Mensaje;
                        lblIdRequisicion.Text = resp.NumRequisicion;
                        txtOpinion.Enabled = false;
                        divNoRequi.Visible = true;
                        divCampos.Visible = false;
                        btnCancelar.Enabled = false;
                        BtnCrear.Enabled = false;
                    }
                    else
                    {
                        ValidarCampos(validacion.MensajesError);
                        lblErrorCampos.Text = "Ocurrio un error";
                        divCampos.Visible = true;
                    }
                }
                else
                {
                    ValidarCampos(validacion.MensajesError);
                    lblErrorCampos.Text = "Verifique que los datos esten completos";
                    divCampos.Visible = true;
                }
            }
        }
        private void FinalizarAutorizacion()
        {
            ValidarAutorizados();
            Model.RespuestaRequisicionDto _resp =  new Servicio.RequsicionServicio().ActualizarRequisicionAutorizacion(CrearAut(), Session["StringToken"].ToString());
            if (_resp.Exito)
            {
                lblNoRequisicion.Text = _resp.Mensaje.Equals(string.Empty) ? "Correcto" : _resp.Mensaje;
                lblIdRequisicion.Text = _resp.NumRequisicion;
                btnCancelar.Enabled = false;
                BtnCrear.Enabled = false;
                divNoRequi.Visible = true;
                divCampos.Visible = false;
            }
            else
            {
                lblErrorCampos.Text = "Ocurrio un error";
                divCampos.Visible = true;
            }
        }
        private void ValidarAutorizados()
        {
            List<Model.RequisicionProdAutPutDTO> lProd = new List<Model.RequisicionProdAutPutDTO>();
            foreach (GridViewRow _row in gvProductoAut.Rows)
            {
                lProd.Add(new Model.RequisicionProdAutPutDTO
                {
                    IdProducto =int.Parse((_row.Cells[0].FindControl("lbldgProductoID") as Label).Text),
                    AutorizaCompra = (_row.Cells[0].FindControl("chbAutCompra") as CheckBox).Checked,
                    AutorizaEntrega =(_row.Cells[0].FindControl("chbAutEntrega") as CheckBox).Checked,
                });
            }
            ViewState["ListaRequisicionProdAutPutDTO"] = lProd;
        }
        private Model.RequisicionRevPutDTO RequisicionRevisionDTO()
        {
            Model.RequisicionRevPutDTO requReqvision = new Model.RequisicionRevPutDTO();
            requReqvision.IdRequisicion = (int)ViewState["idRequisicion"];
            requReqvision.NumeroRequisicion = lblIdRequisicion.Text;
            if (Convert.ToBoolean(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "EsAdminCentral").Value))
                requReqvision.IdUsuarioRevision = int.Parse(ddlSolicitante.SelectedValue);
            else
                requReqvision.IdUsuarioRevision = Convert.ToInt32(TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(Session["StringToken"].ToString(), "IdUsuario").Value);
            requReqvision.OpinionAlmacen = txtOpinion.Text;
            requReqvision.FechaRevision = DateTime.Today;
            requReqvision.ListaProductos = (List<Model.RequisicionProdReviPutDTO>)ViewState["LIstaReqProdRevPutDTO"];
            return requReqvision;
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Model.RequisicionProductoGridDTO prod = new Servicio.RequsicionServicio().GenerarProductoGrid(ddlTipoCompra, ddlProdcutos, ddlCentroCostos, txtDetalle.Text, Convert.ToDecimal(txtCantidad.Text != string.Empty ? txtCantidad.Text : "0"));
            var validacion = ValidadorClases.EnlistaErrores<Model.RequisicionProductoGridDTO>(prod);
            if (validacion.ModeloValido)
            {
                ValidarCampos(new List<Result>());//Limpia los campos de error 
                List<Model.RequisicionProductoGridDTO> LProductos = new List<Model.RequisicionProductoGridDTO>();
                if (ValidarProductoRepetido(int.Parse(ddlProdcutos.SelectedValue.ToString())))
                {
                    LProductos = ((List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"]).Where(x => x.IdProducto.Equals(int.Parse(ddlProdcutos.SelectedValue.ToString())))
                         .Select(x => { x.Cantidad = x.Cantidad + decimal.Parse(txtCantidad.Text); x.Aplicacion = x.Aplicacion + "|" + txtDetalle.Text; return x; }).ToList();
                }
                else
                {
                    LProductos = (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"] == null ? new List<Model.RequisicionProductoGridDTO>() : (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"];
                    LProductos = new Servicio.RequsicionServicio().GenerarListaGrid(LProductos, prod);
                }
                dgListaproductos.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = LProductos;
                dgListaproductos.DataBind();
                LimpiarCamposProductos();
                DivCamposPord.Visible = false;
                BtnCrear.Enabled = true;
                btnAgregar.Text = "Agregar";
            }
            else
            {
                ValidarCampos(validacion.MensajesError);
                lblErrorPord.Text = "Verifique que los datos esten completos";
                DivCamposPord.Visible = true;
            }
        }
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            if (BtnCrear.Text.Equals("Crear"))
                GuardarRequisicion();
            if (BtnCrear.Text.Equals("Finalizar"))
                FinalizarRevision();
            if (BtnCrear.Text.Equals("Autorizar"))            
                FinalizarAutorizacion();            
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
        protected void onFocus()
        {
            divCampos.Visible = false;
        }
        protected void ddlSolicitante_SelectedIndexChanged(object sender, EventArgs e)
        {
            onFocus();
        }
        protected void txtMotivoCompra_TextChanged(object sender, EventArgs e)
        {
            onFocus();
        }
        protected void txtRequeridoEn_TextChanged(object sender, EventArgs e)
        {
            onFocus();
        }
        protected void gvProductosRevision_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void gvProductoAut_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvProductoAut_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (decimal.Parse((e.Row.Cells[0].FindControl("lblAlmacen") as Label).Text).Equals(0))            
                (e.Row.Cells[0].FindControl("chbAutEntrega") as CheckBox).Enabled = false;
            if (decimal.Parse((e.Row.Cells[0].FindControl("lblRequiereComp") as Label).Text).Equals(0))
                (e.Row.Cells[0].FindControl("chbAutCompra") as CheckBox).Enabled = false;           
        }
    }
}