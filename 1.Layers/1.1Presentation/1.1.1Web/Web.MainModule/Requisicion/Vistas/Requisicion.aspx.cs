using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities.MainModule;
using Web.MainModule.Requisicion.Model;
using Web.MainModule.Requisicion.Servicio;
using Web.MainModule.Seguridad.Servicio;

namespace Web.MainModule.Requisicion.Vista
{
    public partial class Requisicion : Page
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
                        if (Request.QueryString["nr"] != null)
                        {
                            if (Request.QueryString["Sts"] != null)
                                RquisicionAlternativa(Request.QueryString["nr"].ToString(), Convert.ToInt32(Request.QueryString["Sts"]));
                            else
                                Salir();
                        }
                        else
                        {
                            CargarEmpresas();
                            if (Convert.ToBoolean(TokenServicios.ObtenerEsAdministracionCentral(_tok)))
                            {
                                CargarUsuariosSolicitante(short.Parse(ddlEmpresas.SelectedValue));
                                CargarCentrosCosto(short.Parse(ddlEmpresas.SelectedValue));
                            }
                            else
                            {
                                ddlEmpresas.Enabled = false;
                                CargarUsuariosSolicitante(TokenServicios.ObtenerIdEmpresa(_tok));
                                CargarCentrosCosto(TokenServicios.ObtenerIdEmpresa(_tok));
                            }
                        }
                        dgListaproductos.DataBind();
                    }
                    else
                        Salir();
                }
            }
            else
                Salir();
        }
        private void CargarEmpresas()
        {
            ddlEmpresas.DataSource = new RequisicionServicio().Empresas(_tok).Where(x => x.EsAdministracionCentral.Equals(false)).ToList();
            ddlEmpresas.DataTextField = "NombreComercial";
            ddlEmpresas.DataValueField = "IdEmpresa";
            ddlEmpresas.DataBind();
            CargarServicios();
        }
        private void CargarServicios()
        {
            //Llenar servicios desde BD

            CargarProductos(short.Parse(ddlTipoCompra.SelectedValue));
        }
        private void CargarCentrosCosto(short idEmpresa)
        {
            ddlCentroCostos.DataSource = new RequisicionServicio().ListaCentroCostos(_tok).ToList().Where(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
            ddlCentroCostos.DataTextField = "Descripcion";
            ddlCentroCostos.DataValueField = "IdCentroCosto";
            ddlCentroCostos.DataBind();
        }
        private void Salir()
        {
            Response.Redirect("../../Login.aspx");
        }
        private Model.RequisicionCrearDTO CrearReq()
        {
            Model.RequisicionCrearDTO reqCrearDTO = new Model.RequisicionCrearDTO();
            if (Convert.ToBoolean(TokenServicios.ObtenerEsAdministracionCentral(_tok)))
            {
                reqCrearDTO.IdEmpresa = short.Parse(ddlEmpresas.SelectedValue);
                reqCrearDTO.IdUsuarioSolicitante = int.Parse(ddlSolicitante.SelectedValue);
            }
            else
            {
                reqCrearDTO.IdUsuarioSolicitante = TokenServicios.ObtenerIdUsuario(_tok);
                reqCrearDTO.IdEmpresa = TokenServicios.ObtenerIdEmpresa(_tok);
            }
            reqCrearDTO.MotivoRequisicion = txtMotivoCompra.Text;
            reqCrearDTO.RequeridoEn = txtRequeridoEn.Text;
            reqCrearDTO.IdRequisicionEstatus = (byte)Model.RequisiconEstatus.Creada;
            reqCrearDTO.FechaRequerida = txtFechaRequerida.GetDate;
            reqCrearDTO.FechaRegistro = DateTime.Today;
            reqCrearDTO.ListaProductos = (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"];
            return reqCrearDTO;
        }
        private void ValidarCampos(List<Result> list)
        {
            if (list.Exists(x => x.IdentidadError.Equals("Numero"))) { reqFecha.Visible = true; reqFecha.Text = list.SingleOrDefault(x => x.IdentidadError.Equals("Numero")).MensajeError; }
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
            if (TokenServicios.ObtenerEsAdministracionCentral(_tok))
                _aut.IdUsuarioAutorizacion = int.Parse(ddlSolicitante.SelectedValue);
            else
                _aut.IdUsuarioAutorizacion = TokenServicios.ObtenerIdUsuario(_tok);
            _aut.ListaProductos = (List<Model.RequisicionProdAutPutDTO>)ViewState["ListaRequisicionProdAutPutDTO"];
            _aut.IdRequisicionEstatus = (byte)Model.RequisiconEstatus.Autorizacion_finalizada;
            return _aut;
        }
        private Model.RequisicionCancelaDTO CrearCancela()
        {
            Model.RequisicionCancelaDTO CancelaDTO = new Model.RequisicionCancelaDTO();
            if (Request.QueryString["Sts"] != null)
            {
                CancelaDTO.IdRequisicion = (int)ViewState["idRequisicion"];
                CancelaDTO.NumeroRequisicion = lblIdRequisicion.Text;
                CancelaDTO.MotivoCancelacion = txtMotivoCancela.Text;
                CancelaDTO.IdRequisicionEstatus = (byte)Model.RequisiconEstatus.Cerrada;
                CancelaDTO.FechaAutorizacion = DateTime.Today;
                if (Request.QueryString["Sts"].Equals("1"))
                    CancelaDTO.IdUsuarioRevision = TokenServicios.ObtenerIdUsuario(_tok);
                else
                    CancelaDTO.IdUsuarioAutorizacion = TokenServicios.ObtenerIdUsuario(_tok);
            }
            return CancelaDTO;
        }
        private void EnviarCorreo(int idReq)
        {

        }
        private bool ValidarRevisionAlmacen()
        {
            bool resp = true;
            if (!txtOpinion.Text.Equals(string.Empty))
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
        private RespuestaOperacionDto ValidarProdNuevo()
        {
            Model.RequisicionProductoGridDTO prod = CrearProductoGrid();
            var validacion = ValidadorClases.EnlistaErrores(prod);
            if (validacion.ModeloValido)
            {
                if (prod.IdTipoProducto.Equals(2))
                {
                    if (!(prod.Cantidad > 0))
                    {
                        validacion.MensajesError.Add(new Result { IdentidadError = "Cantidad", MensajeError = Exceptions.MainModule.Validaciones.Error.R0003 });
                        validacion.ModeloValido = false;
                    }
                }
            }
            if (validacion.ModeloValido)
                ValidarCampos(new List<Result>());//Se manda lista vacia para limpiar campos
            else
                ValidarCampos(validacion.MensajesError);
            return validacion;
        }
        private RespuestaOperacionDto ValidarReqNuevo()
        {
            Model.RequisicionCrearDTO Edto = CrearReq();
            var validacion = ValidadorClases.EnlistaErrores<Model.RequisicionCrearDTO>(Edto);
            if (ValidarListaprodcutos())
            {
                if (validacion.ModeloValido)
                {
                    if (Edto.FechaRequerida == DateTime.MinValue)
                    {
                        validacion.MensajesError.Add(new Result { IdentidadError = "FechaRequerida", MensajeError = Exceptions.MainModule.Validaciones.Error.S0001 });
                        validacion.ModeloValido = false;
                    }
                    else if (Edto.FechaRequerida <= DateTime.Today)
                    {
                        validacion.MensajesError.Add(new Result { IdentidadError = "FechaRequerida", MensajeError = Exceptions.MainModule.Validaciones.Error.R0011 });
                        validacion.ModeloValido = false;
                    }

                }
            }
            if (validacion.ModeloValido)
                ValidarCampos(new List<Result>());//Se manda lista vacia para limpiar campos
            else
                ValidarCampos(validacion.MensajesError);

            return validacion;
        }
        private bool ValidarProductoRepetido(int idProducto, int idCentroCosto)
        {
            bool resp = false;
            if (ViewState["ListaRequisicionProductoGridDTO"] != null)
                resp = ((List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"]).Exists(x => x.IdProducto.Equals(idProducto) && x.IdCentroCosto.Equals(idCentroCosto));
            return resp;
        }
        private bool ValidarAutorizacion()
        {
            bool correcto = true;
            foreach (GridViewRow _row in gvProductoAut.Rows)
            {
                if ((_row.FindControl("chbAutCompra") as CheckBox).Checked)
                {
                    if (decimal.Parse((_row.Cells[0].FindControl("txtRequiereComp") as TextBox).Text) > decimal.Parse((_row.FindControl("lbldgCantidad") as Label).Text))
                    {
                        correcto = false;
                    }
                }
            }
            return correcto;
        }
        private void RquisicionAlternativa(string NumRueq, int Estatus)
        {
            CargarEmpresas();
            ddlEmpresas.Enabled = false;
            if (Estatus.Equals(1))
                ActivarRevisarExistencias();
            else
                ActivarRevisarAutorizacion();
        }
        private void ActivarRevisarExistencias()
        {
            RequisicionRevisionDTO _reqEDTO = new RequisicionRevisionDTO();
            _reqEDTO = new RequisicionServicio().BuscarRequisicionByNumRequiRevi(int.Parse(Request.QueryString["nr"].ToString()), _tok);
            ViewState["idRequisicion"] = _reqEDTO.IdRequisicion;
            CargarUsuariosSolicitante(_reqEDTO.IdEmpresa);
            divDatos1.Visible = false;
            divDatos2.Visible = false;
            divOpinion.Visible = true;
            lblRuta.Text = "Requisición / Revisar Existencias";
            lblIdRequisicion.Text = _reqEDTO.NumeroRequisicion;
            btnCancelar.Enabled = true;
            BtnCrear.Text = "Finalizar";
            lblbtnCrear.Text = "Finalizar";
            txtFechaRequerida.Enabled = false;
            txtMotivoCompra.Enabled = false;
            txtRequeridoEn.Enabled = false;
            txtFechaRequerida.Text = _reqEDTO.FechaRequerida.ToShortDateString();
            txtMotivoCompra.Text = _reqEDTO.MotivoRequisicion;
            txtRequeridoEn.Text = _reqEDTO.RequeridoEn;
            ddlSolicitante.Enabled = false;
            ddlSolicitante.SelectedValue = _reqEDTO.IdUsuarioSolicitante.ToString();
            btnCancel.Attributes.Remove("class");
            btnCancel.Attributes.Add("class", "btn btn-raised btn-primary btn-round");

            dgListaproductos.Visible = false;
            gvProductosRevision.Visible = true;
            gvProductosRevision.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = _reqEDTO.ListaProductos;
            gvProductosRevision.DataBind();
        }
        private bool ValidarRequisiconGas()
        {
            bool EsGas = false;
            var prod = ((List<RequisicionProductoAutorizacionDTO>)ViewState["ListaRequisicionProductoGridDTO"]).Where(x => x.EsGas || x.EsTransporteGas).ToList();
            if (!prod.Count.Equals(0))            
                EsGas = true;            
            return EsGas;
        }        
        private void ActivarRevisarAutorizacion()
        {
            Model.RequisicionAutorizacion _reqEDTO = new Model.RequisicionAutorizacion();
            _reqEDTO = new Servicio.RequisicionServicio().BuscarRequisicionByNumRequiAuto(int.Parse(Request.QueryString["nr"].ToString()), _tok);
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
            lblbtnCrear.Text = "Finalizar";
            btnCancelar.Enabled = true;
            btnCancel.Attributes.Remove("class");
            btnCancel.Attributes.Add("class", "btn btn-raised btn-primary btn-round");
            ddlSolicitante.Enabled = false;
            ddlSolicitante.SelectedValue = _reqEDTO.IdUsuarioSolicitante.ToString();
            dgListaproductos.Visible = false;
            gvProductoAut.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = _reqEDTO.ListaProductos;
            gvProductoAut.DataBind();
            if (ValidarRequisiconGas())
            {
                gvProductoAut.Columns[7].Visible = false;
                divOpinion.Visible = false;
            }
           
            
        }
        private void BorrarProductoLista(int id, int cc)
        {
            List<Model.RequisicionProductoGridDTO> lprod = (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"];
            Model.RequisicionProductoGridDTO prod = lprod.SingleOrDefault(x => x.IdProducto.Equals(id) && x.IdCentroCosto.Equals(cc));
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
        private void EditarProductoLista(int id, int cc)
        {
            List<Model.RequisicionProductoGridDTO> lprod = (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"];
            Model.RequisicionProductoGridDTO prod = lprod.SingleOrDefault(x => x.IdProducto.Equals(id) && x.IdCentroCosto.Equals(cc));
            lprod.Remove(prod);
            ddlTipoCompra.SelectedValue = prod.IdTipoProducto.ToString();
            CargarProductos(short.Parse(prod.IdTipoProducto.ToString()));
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
            ddlSolicitante.DataSource = new Servicio.RequisicionServicio().ListaUsuarios(idEmpresa, _tok);
            ddlSolicitante.Items.Insert(0, new ListItem { Text = "Solicitante...", Value = "0" });
            ddlSolicitante.DataTextField = "Nombre";
            ddlSolicitante.DataValueField = "IdUsuario";
            ddlSolicitante.DataBind();
        }
        private void CargarProductos(short idTipoServicio)
        {
            if (Convert.ToBoolean(TokenServicios.ObtenerEsAdministracionCentral(_tok)))
                ddlProdcutos.DataSource = ViewState["ProductosDTO"] = new RequisicionServicio().ListaProductos(short.Parse(ddlEmpresas.SelectedValue), _tok).Where(x => x.IdProductoServicioTipo.Equals(idTipoServicio)).ToList();
            else
                ddlProdcutos.DataSource = ViewState["ProductosDTO"] = new RequisicionServicio().ListaProductos(TokenServicios.ObtenerIdEmpresa(_tok), _tok).Where(x => x.IdProductoServicioTipo.Equals(idTipoServicio)).ToList();
            ddlProdcutos.DataTextField = "Descripcion";
            ddlProdcutos.DataValueField = "IdProducto";
            ddlProdcutos.DataBind();
            MostrarUnidad();
        }
        private void GuardarRequisicion()
        {
            Servicio.RequisicionServicio serv = new Servicio.RequisicionServicio();
            var validacionRequisicion = ValidarReqNuevo();
            if (validacionRequisicion.ModeloValido)
            {
                var respuesta = serv.GuardarRequisicion(new Servicio.RequisicionServicio().UnirDtos(CrearReq()), _tok);
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
                        BtnCrear.Enabled = false;
                        btnAgregar.Enabled = false;
                        dgListaproductos.Enabled = false;
                        btnok.Attributes.Remove("class");
                        btnok.Attributes.Add("class", "btn btn-raised btn-primary btn-round disabled");
                        btnCancel.Attributes.Remove("class");
                        btnCancel.Attributes.Add("class", "btn btn-raised btn-primary btn-round disabled");
                        btnAgregar.CssClass = "btn btn danger btn simple btn round btn sm disabled";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                        Response.Redirect("~/Compras/Vistas/Compras.aspx");
                    }
                    else
                    {
                        lblErrorCampos.Text = Exceptions.MainModule.Validaciones.Error.R0001;
                        divCampos.Visible = true;
                    }
                }
            }
            else
            {
                lblErrorCampos.Text = Exceptions.MainModule.Validaciones.Error.R0012;
                divCampos.Visible = true;
            }
        }
        private void FinalizarRevision()
        {
            Servicio.RequisicionServicio serv = new Servicio.RequisicionServicio();
            if (ValidarRevisionAlmacen())
            {
                Model.RequisicionRevPutDTO dto = RequisicionRevisionDTO();
                var validacion = ValidadorClases.EnlistaErrores<Model.RequisicionRevPutDTO>(dto);
                if (validacion.ModeloValido)
                {
                    LimpiarMensajesProd();
                    Model.RespuestaRequisicionDto resp = new Model.RespuestaRequisicionDto();
                    resp = serv.ActualizarRequisicionRevision(dto, _tok);
                    if (resp.Exito)
                    {
                        lblNoRequisicion.Text = resp.Mensaje.Equals(string.Empty) ? "Correcto" : resp.Mensaje;
                        lblIdRequisicion.Text = resp.NumRequisicion;
                        txtOpinion.Enabled = false;
                        reqOpinion.Visible = false;
                        divNoRequi.Visible = true;
                        divCampos.Visible = false;
                        btnCancelar.Enabled = false;
                        BtnCrear.Enabled = false;
                        gvProductosRevision.Enabled = false;
                        btnCancel.Attributes.Remove("class");
                        btnCancel.Attributes.Add("class", "btn btn-raised btn-primary btn-round disabled");
                        Response.Redirect("~/Compras/Vistas/Compras.aspx");
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
            GenerarAutorizados();
            if (ValidarAutorizacion())
            {
                Model.RespuestaRequisicionDto _resp = new RequisicionServicio().ActualizarRequisicionAutorizacion(CrearAut(), _tok);
                if (_resp.Exito)
                {
                    lblNoRequisicion.Text = _resp.Mensaje.Equals(string.Empty) ? "Autorizacion finalizada" : _resp.Mensaje;
                    lblIdRequisicion.Text = _resp.NumRequisicion;
                    btnCancelar.Enabled = false;
                    BtnCrear.Enabled = false;
                    divNoRequi.Visible = true;
                    divCampos.Visible = false;
                    gvProductoAut.Enabled = false;
                    btnCancel.Attributes.Remove("class");
                    btnCancel.Attributes.Add("class", "btn btn-raised btn-primary btn-round disabled");
                    Response.Redirect("~/Compras/Vistas/Compras.aspx");
                }
                else
                {
                    lblErrorCampos.Text = _resp.Mensaje;
                    divCampos.Visible = true;
                }
            }
            else
            {
                lblErrorCampos.Text = "No puede autorizar mas cantidad de la requerida. \n\rDeberá generar una nueva requisición si así lo desea";
                divCampos.Visible = true;
            }
        }
        private void GenerarAutorizados()
        {
            //int definirStatus = 0;
            List<Model.RequisicionProdAutPutDTO> lProd = new List<Model.RequisicionProdAutPutDTO>();
            foreach (GridViewRow _row in gvProductoAut.Rows)
            {
                lProd.Add(new Model.RequisicionProdAutPutDTO
                {
                    IdProducto = int.Parse((_row.Cells[0].FindControl("lbldgProductoID") as Label).Text),
                    AutorizaCompra = (_row.Cells[0].FindControl("chbAutCompra") as CheckBox).Checked,
                    AutorizaEntrega = (_row.Cells[0].FindControl("chbAutEntrega") as CheckBox).Checked,
                    CantidadAComprar = decimal.Parse((_row.Cells[0].FindControl("txtRequiereComp") as TextBox).Text),
                    CantidadRequerida = decimal.Parse((_row.Cells[0].FindControl("lbldgCantidad") as Label).Text),
                });
            }
            ViewState["ListaRequisicionProdAutPutDTO"] = lProd;
        }
        private Model.RequisicionRevPutDTO RequisicionRevisionDTO()
        {
            Model.RequisicionRevPutDTO requRevision = new Model.RequisicionRevPutDTO();
            requRevision.IdRequisicion = (int)ViewState["idRequisicion"];
            requRevision.NumeroRequisicion = lblIdRequisicion.Text;
            if (TokenServicios.ObtenerEsAdministracionCentral(_tok))
                requRevision.IdUsuarioRevision = int.Parse(ddlSolicitante.SelectedValue);
            else
                requRevision.IdUsuarioRevision = TokenServicios.ObtenerIdUsuario(_tok);
            requRevision.OpinionAlmacen = txtOpinion.Text;
            requRevision.FechaRevision = DateTime.Today;
            requRevision.ListaProductos = (List<Model.RequisicionProdReviPutDTO>)ViewState["LIstaReqProdRevPutDTO"];
            requRevision.IdRequisicionEstatus = (byte)Model.RequisiconEstatus.Revision_exitosa;
            return requRevision;
        }
        private void MostrarUnidad()
        {
            Model.ProductoDTO prod = ((List<Model.ProductoDTO>)ViewState["ProductosDTO"]).SingleOrDefault(x => x.IdProducto.Equals(int.Parse(ddlProdcutos.SelectedValue)));
            lblCantidadUnidad.Text = "Cantidad: " + prod.UnidadMedida;
        }
        private Model.RequisicionProductoGridDTO CrearProductoGrid()
        {
            Model.RequisicionProductoGridDTO prod = new RequisicionServicio().GenerarProductoGrid(
            ddlTipoCompra,
            ddlProdcutos,
            ddlCentroCostos,
            txtDetalle.Text,
            Convert.ToDecimal(txtCantidad.Text != string.Empty ? txtCantidad.Text : "0"),
            ((List<Model.ProductoDTO>)ViewState["ProductosDTO"]));
            return prod;
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            var validacion = ValidarProdNuevo();
            if (validacion.ModeloValido)
            {
                ValidarCampos(new List<Result>());//Limpia los campos de error 
                List<Model.RequisicionProductoGridDTO> LProductos = new List<Model.RequisicionProductoGridDTO>();
                if (ValidarProductoRepetido(int.Parse(ddlProdcutos.SelectedValue.ToString()), int.Parse(ddlCentroCostos.SelectedValue)))
                {
                    LProductos = ((List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"]);
                    foreach (var item in LProductos.Where((x => x.IdProducto.Equals(int.Parse(ddlProdcutos.SelectedValue.ToString()))
                        && x.IdCentroCosto.Equals(int.Parse(ddlCentroCostos.SelectedValue)))))
                    {
                        item.Cantidad = item.Cantidad + decimal.Parse(txtCantidad.Text == string.Empty ? "0" : txtCantidad.Text);
                        item.Aplicacion = item.Aplicacion + "|" + txtDetalle.Text;
                    }
                }
                else
                {
                    LProductos = (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"] == null ? new List<Model.RequisicionProductoGridDTO>() : (List<Model.RequisicionProductoGridDTO>)ViewState["ListaRequisicionProductoGridDTO"];
                    LProductos = new RequisicionServicio().GenerarListaGrid(LProductos, CrearProductoGrid(), _tok);
                }
                dgListaproductos.DataSource = ViewState["ListaRequisicionProductoGridDTO"] = LProductos;
                dgListaproductos.DataBind();
                LimpiarCamposProductos();
                DivCamposPord.Visible = false;
                BtnCrear.Enabled = true;
                btnAgregar.Text = "Agregar +";
            }
            else
            {
                lblErrorPord.Text = Exceptions.MainModule.Validaciones.Error.R0012;
                DivCamposPord.Visible = true;
            }
        }
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            if (BtnCrear.Text.Equals("Si"))
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
            }
        }
        protected void dgListaproductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Borrar"))
            {
                BorrarProductoLista(int.Parse(e.CommandArgument.ToString().Split('|')[0]), int.Parse(e.CommandArgument.ToString().Split('|')[1]));
            }
            if (e.CommandName.Equals("Editar"))
            {
                EditarProductoLista(int.Parse(e.CommandArgument.ToString().Split('|')[0]), int.Parse(e.CommandArgument.ToString().Split('|')[1]));
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Model.RequisicionCancelaDTO _canDTO = CrearCancela();
            var error = ValidadorClases.EnlistaErrores(_canDTO);
            if (error.ModeloValido)
            {
                var respCancelar = new Servicio.RequisicionServicio().CancalarRequisicion(_canDTO, _tok);
                if (respCancelar.Exito)
                {
                    btnCancelar.Enabled = false;
                    BtnCrear.Enabled = false;
                    txtOpinion.Enabled = false;
                    divNoRequi.Visible = true;
                    lblNoRequisicion.Text = "Cancelada correctamente";
                    btnCancel.Attributes.Remove("class");
                    btnCancel.Attributes.Add("class", "btn btn-raised btn-primary btn-round disabled");
                }
                else
                {
                    lblErrorCampos.Text = "Ocurrio un error";
                    divCampos.Visible = true;
                }
            }
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
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                if (decimal.Parse((e.Row.Cells[0].FindControl("lblAlmacen") as Label).Text).Equals(0))
                    (e.Row.Cells[0].FindControl("chbAutEntrega") as CheckBox).Enabled = false;
                if ((e.Row.Cells[0].FindControl("lbldgTipo") as Label).Text.Equals("Servicio"))
                {
                    (e.Row.Cells[0].FindControl("lbldgCantidad") as Label).Visible = false;
                    (e.Row.Cells[0].FindControl("lblCantidadNA") as Label).Visible = true;                   

                    (e.Row.Cells[0].FindControl("lbldgUnidad") as Label).Visible = false;
                    (e.Row.Cells[0].FindControl("lblUnidadNA") as Label).Visible = true;     

                    (e.Row.Cells[0].FindControl("lblAlmacen") as Label).Visible = false;
                    (e.Row.Cells[0].FindControl("lblAlmacenNA") as Label).Visible = true;

                    (e.Row.Cells[0].FindControl("txtRequiereComp") as TextBox).Visible = false;
                    (e.Row.Cells[0].FindControl("lblCantidadAComprarNA") as Label).Visible = true;

                }
            }
        }
        protected void ddlProdcutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarUnidad();
        }
        protected void ddlTipoCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProductos(short.Parse(ddlTipoCompra.SelectedValue));
            if (ddlTipoCompra.SelectedValue.Equals("3"))
                txtCantidad.Enabled = false;
            else
                txtCantidad.Enabled = true;
        }
        protected void gvProductoAut_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void dgListaproductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                if ((e.Row.Cells[0].FindControl("lbldgTipo") as Label).Text.Equals("Servicio"))
                {
                    (e.Row.Cells[0].FindControl("lblCantidadNA") as Label).Visible = true;
                    (e.Row.Cells[0].FindControl("lblUnidadNA") as Label).Visible = true;
                    (e.Row.Cells[0].FindControl("lbldgCantidad") as Label).Visible = false;
                    (e.Row.Cells[0].FindControl("lbldgUnidad") as Label).Visible = false;
                }
            }
        }
    }
}