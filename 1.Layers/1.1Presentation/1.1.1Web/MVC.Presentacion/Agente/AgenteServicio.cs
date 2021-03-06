﻿using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Almacen;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Cobranza;
using MVC.Presentacion.Models.Facturacion;
using MVC.Presentacion.Models.OrdenCompra;
using MVC.Presentacion.Models.Pedidos;
using MVC.Presentacion.Models.Requisicion;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MVC.Presentacion.Agente
{
    public class AgenteServicio
    {
        private static string UrlBase;
        private string ApiLogin;
        private string ApiCatalgos;
        private string ApiRequisicion;
        private string ApiOrdenCompra;
        private string ApiRoute = string.Empty;

        public RespuestaDTO _RespuestaDTO;
        public RespuestaAutenticacionDto _respuestaAutenticacion;
        public RespuestaRequisicionDTO _respuestaRequisicion;
        public OrdenCompraDTO _ordeCompraDTO;
        public RequisicionRevisionDTO _requisicionRevisionDTO;
        public RequisicionAutorizacionDTO _requsicionAutorizacion;
        public CatalogoRespuestaDTO _respuestaCatalogos;
        public RequisicionDTO _requisicion;
        public RequisicionOCDTO _requisicionOrdenCompra;
        public EntradaMercanciaModel _entradaMercancia;
        public OperadorChoferModel Operador;
        public OrdenCompraComplementoGasDTO _complementoGas;
        public RequisicionSalidaDTO _RequisicionSalida;
        public CargosModel _Cargo;
        public ReporteModel _repCartera;
        public PedidoModel _Pedido;
        public RegistrarPedidoModel _RegPedido;
        public CombustibleModel _Combustible;
        public EquipoTransporteDTO _Vehiculos;
        public VentaPuntoVentaDTO _VentaDTO;
        public CFDIDTO _CFDIDTO;
        public ClientesDto _ClienteDTO;
        public ClientesModel _ClienteModel;
        public HistoricoVentaModel _HistoricoVentaDTO;
        public EgresoDTO _EgresoDTO;
        public AdministracionDTO _AdministracionDTO;
        public AndenDTO _AndenDTO;
        public CarteraDTO _CarteraDTO;
        public string _Json;

        public List<ClienteLocacionMod> _cteLocacion;
        public List<RequisicionDTO> _listaRequisicion;
        public List<EmpresaDTO> _listaEmpresas;
        public List<PaisModel> _listaPaises;
        public List<RequisicionEstatusDTO> _listaRequisicionEstatus;
        public List<UsuarioDTO> _listaUsuarios;
        public List<CentroCostoDTO> _listaCentroCosto;
        public List<ProductoDTO> _listaProductos;
        public List<OrdenCompraDTO> _listaOrdenCompra;
        public List<OrdenCompraEstatusDTO> _listaOrdenCompraEstatus;
        public List<ProveedorDTO> _listaProveedores;
        public List<CuentaContableDTO> _listaCuentasContables;
        public List<EstacionCarburacionDTO> _listaEstacionCarburacion;
        public List<UnidadAlmacenGasDTO> _listaUnidadAlmacenGas;
        public List<EquipoTransporteDTO> _listaEquipoTransporte;
        public List<TipoCentroCostoDTO> _listaTipoCentroCosto;
        public List<CategoriaProductoDTO> _listaCategoriasProducto;
        public List<LineaProductoDTO> _listaLineasProducto;
        public List<UnidadMedidaDTO> _listaUnidadesMedida;
        public List<RolDto> _lstaAllRoles;
        public List<RolMovilCompra> _lstaRolesMovilCom;
        public List<RolCompras> _lstaRolesCom;
        public List<RolRequsicion> _lstaRolesReq;
        public List<TipoPersonaModel> _lstaTipoPersona;
        public List<RegimenFiscalModel> _lstaRegimenFiscal;
        public List<ClientesDto> _lstaClientes;
        public List<ClientesModel> _lstaClientesMod;
        public List<UsuariosModel> _lstUserEmp;
        public List<EstadosRepModel> _listaEstados;
        public List<TipoProveedorDTO> _listaTipoProveedor;
        public List<BancoDTO> _listaBanco;
        public List<FormaPagoDTO> _listaFormaPago;
        public List<PuntoVentaModel> _listaPuntosV;
        public List<OperadorChoferModel> _listaOperadoresUsuarios;
        public List<OrdenCompraPagoDTO> _listaOrdenCompraPago;
        public List<PrecioVentaModel> _listaPreciosV;
        public List<EstatusTipoFechaModel> _listaEstatus;
        public List<CajaGeneralModel> _listaCajaGral;
        public List<CajaGeneralCamionetaModel> _listaCajaGralCamioneta;
        public List<VentaCorteAnticipoModel> _listaCajaGralEstacion;
        public List<AlmacenDTO> _listaAlmacen;
        public List<RegistroDTO> _listaRegistroAlmacen;
        public List<MovimientosGasModel> _ListaMovimientosGas;
        public List<MovimientosGasCilindros> _ListaMovimientosGasC;
        public List<PedidoModel> _ListaPedidos;
        public List<EstatusPedidoModel> _ListaEstatusP;
        public List<CamionetaModel> _ListaCamionetas;
        public List<PipaModel> _ListaPipas;
        public List<CargosModel> _ListaCargos;
        public List<RemanenteGeneralDTO> _ListaRemanenteGenaral;
        public List<EquipoTransporteDTO> _ListaVehiculos;
        public List<CombustibleModel> _ListaCombustibles;
        public List<TipoUnidadModel> _ListaTiposUnidad;
        public List<RecargaCombustibleModel> _ListaRecargasCombustible;
        public List<MantenimientoModel> _ListaMantenimientos;
        public List<MantenimientoDetalleModel> _ListaMantenimientoDetalle;
        public List<AsignacionModel> _ListaAsignaciones;
        public List<MedidorDTO> _ListaMedidores;
        public List<FacturacionModel> _ListainfoTicket;
        public List<VentaPuntoVentaDTO> _ListaVenta;
        public List<CFDIDTO> _ListaCFDIs;
        public List<HistoricoVentaModel> _ListHistoricoVenta;
        public List<YearsDTO> _ListYears;
        public List<UsoCFDIDTO> _ListaUsoCFDI;
        public List<MetodoPagoDTO> _ListaMetodPago;
        public List<YearsVentasTotalesDTO> _yearVentasTortales;
        public List<EgresoDTO> _ListaEgreso;
        public List<CuentasPorPagarDTO> _ListaCuentasPorPagar;
        public List<InventarioPorPuntoVentaDTO> _ListaInventarioPuntoVenta;
        public List<HistoricoPrecioVentaDTO> _ListaHistoricoPrecioVenta;
        public List<CallCenterDTO> _ListaCallCenter;
        public List<RequisicionRepDTO> _ListaRequisicion;
        public List<OrdenCompraRepDTO> _ListaOrdenCompra;
        public List<RendimientoVehicularDTO> _ListaRendimientoVehicular;
        public List<InventarioXConceptoDTO> _ListaInventarioConcepto;
        public List<CorteCajaDTO> _ListaCorteCaja;
        public List<GastoVehiculoDTO> _ListaGastoVehicular;

        public AgenteServicio()
        {
            UrlBase = ConfigurationManager.AppSettings["WebApiUrlBase"];
        }

        #region Catalogos
        #region roles
        public void BuscarRolesRequisicion(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaRoles"];
            GetListaRolesReq(tkn).Wait();
        }
        private async Task GetListaRolesReq(string Token)
        {
            using (var client = new HttpClient())
            {
                List<RolRequsicion> lus = new List<RolRequsicion>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RolRequsicion>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RolRequsicion>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaRolesReq = (from x in lus where x.NombreRol != "Super Usuario" select x).ToList();
            }
        }
        public void BuscarRolesMovilCompras(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaRoles"];
            GetListaRolesMovilCom(tkn).Wait();
        }
        private async Task GetListaRolesMovilCom(string Token)
        {
            using (var client = new HttpClient())
            {
                List<RolMovilCompra> lus = new List<RolMovilCompra>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RolMovilCompra>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RolMovilCompra>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaRolesMovilCom = (from x in lus where x.NombreRol != "Super Usuario" select x).ToList();
            }
        }
        public void BuscarRolesCompras(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaRoles"];
            GetListaRolesCom(tkn).Wait();
        }
        private async Task GetListaRolesCom(string Token)
        {
            using (var client = new HttpClient())
            {
                List<RolCompras> lus = new List<RolCompras>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RolCompras>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RolCompras>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaRolesCom = (from x in lus where x.NombreRol != "Super Usuario" select x).ToList();
            }
        }
        public void BuscarRoles(string tkn, short emp)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaRoles"];
            GetListaRoles(tkn, emp).Wait();
        }
        private async Task GetListaRoles(string Token, short emp)
        {
            using (var client = new HttpClient())
            {
                List<RolDto> lus = new List<RolDto>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + emp).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RolDto>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RolDto>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaAllRoles = (from x in lus where x.NombreRol != "Super Usuario" select x).ToList();
            }
        }
        public void BuscarTodosRoles(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaRoles"];
            GetListaTodosRoles(tkn).Wait();
        }
        private async Task GetListaTodosRoles(string Token)
        {
            using (var client = new HttpClient())
            {
                List<RolDto> lus = new List<RolDto>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RolDto>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RolDto>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaAllRoles = (from x in lus where x.NombreRol != "Super Usuario" select x).ToList();
            }
        }
        public void BuscarRolId(int id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaRoles"];
            GetListaIdRol(id, tkn).Wait();
        }
        private async Task GetListaIdRol(int id, string Token)
        {
            using (var client = new HttpClient())
            {
                List<RolDto> lus = new List<RolDto>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RolDto>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RolDto>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaAllRoles = (from x in lus where x.IdRol == id select x).ToList();
            }
        }
        public void GuardarNuevoRol(RolDto dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraRol"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void GuardarModificacionRol(RolDto dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaRoles"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void GuardarPermisos(List<RolDto> dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaPermisos"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarRol(short dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutEliminarRol"];
            EliminarRolSeleccionado(dto, tkn).Wait();
        }
        private async Task EliminarRolSeleccionado(short _pcDTO, string token)
        {

            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos + _pcDTO.ToString(), "").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        #endregion
        #region Empresa
        public void ListaEmpresasLogin()
        {
            this.ApiLogin = ConfigurationManager.AppSettings["GetListaEmpresasLogin"];
            ListaEmp(this.ApiLogin).Wait();
        }
        public void ListaEmpresasLogin(string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaEmpresas"];
            ListaEmp(ApiCatalgos, token).Wait();
        }
        private async Task ListaEmp(string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<EmpresaDTO> emp = new List<EmpresaDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<EmpresaDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<EmpresaDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaEmpresas = emp;
            }
        }
        public void GuardarEmpresaNueva(EmpresaModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraEmpresas"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void EliminarEmpresa(short dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutEliminarEmpresa"];
            EliminarEmpresaSeleccionada(dto, tkn).Wait();
        }
        private async Task EliminarEmpresaSeleccionada(short _pcDTO, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos + _pcDTO.ToString(), "").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        public void GuardarEmpresaConfiguracion(EmpresaConfiguracion dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEmpresaConfig"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void GuardarEmpresaEdicion(EmpresaModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaEmpresas"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        #endregion
        #region Usuarios
        public void BuscarListaUsuarios(short idEmpresa, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaUsuarios"];
            GetListaUsuarios(idEmpresa, tkn).Wait();
        }
        private async Task GetListaUsuarios(short IdEmpresa, string Token)
        {
            using (var client = new HttpClient())
            {
                List<UsuarioDTO> lus = new List<UsuarioDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + IdEmpresa.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<UsuarioDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<UsuarioDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaUsuarios = lus;
            }
        }
        public void BuscarUsuarioId(int id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaUsuarios"];
            GetListaIdUsuario(id, tkn).Wait();
        }
        private async Task GetListaIdUsuario(int id, string Token)
        {
            using (var client = new HttpClient())
            {
                List<UsuariosModel> lus = new List<UsuariosModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<UsuariosModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<UsuariosModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstUserEmp = (from x in lus where x.IdUsuario == id select x).ToList();
            }
        }
        public void BuscarTodosUsuarios(int id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaUsuarios"];
            GetListaTodosUsuarios(id, tkn).Wait();
        }
        private async Task GetListaTodosUsuarios(int id, string Token)
        {
            using (var client = new HttpClient())
            {
                List<UsuariosModel> lus = new List<UsuariosModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<UsuariosModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<UsuariosModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                if (id != 0)
                {
                    _lstUserEmp = (from x in lus where x.IdUsuario == id select x).ToList();
                }
                else
                    _lstUserEmp = lus;
            }
        }
        public void FiltrarUsuarios(int idEmpresa, int idUser, string mail, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaUsuarios"];
            GetListaFiltroUsuarios(idEmpresa, idUser, mail, tkn).Wait();
        }
        private async Task GetListaFiltroUsuarios(int idEmpresa, int idUser, string mail, string Token)
        {
            using (var client = new HttpClient())
            {
                List<UsuariosModel> lus = new List<UsuariosModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<UsuariosModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<UsuariosModel>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _lstUserEmp = lus;
                if (idEmpresa != 0)
                {
                    _lstUserEmp = (from x in lus where x.IdEmpresa == idEmpresa select x).ToList();
                    if (idUser != 0 && idUser != -1)
                    {
                        _lstUserEmp = (from x in lus where x.IdUsuario == idUser select x).ToList();

                    }
                    if (!String.IsNullOrEmpty(mail) && mail != "0")
                    {
                        _lstUserEmp = (from x in lus where x.Email1 == mail select x).ToList();

                    }
                }

            }
        }
        public void GuardarNuevoUsuario(UsuarioDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraUsuarios"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void GuardarCredenciales(UsuarioDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaCredencial"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void GuardarRolesAsig(UsuarioRolModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostAsignarRol"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void GuardarUsuarioEdicion(UsuarioDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaUsuarios"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarUsuario(short dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutEliminarUsuario"];
            EliminarUsuarioSeleccionado(dto, tkn).Wait();
        }
        public void EliminarRolesAsig(UsuariosModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaUsuarioRol"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void EliminarRolesAsig(UsuarioRolModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaUsuarioRol"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        private async Task EliminarUsuarioSeleccionado(short _pcDTO, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos + _pcDTO.ToString(), "").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        #endregion
        #region Clientes
        public void BuscarTiposPersona(string tkn = null)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetTiposPersona"];
            GetTiposPersona(tkn).Wait();
        }
        private async Task GetTiposPersona(string Token = null)
        {
            using (var client = new HttpClient())
            {
                List<TipoPersonaModel> lus = new List<TipoPersonaModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (Token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<TipoPersonaModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<TipoPersonaModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaTipoPersona = lus;
            }
        }
        public void BuscarRegimenFiscal(string tkn = null)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetRegimenFiscal"];
            GetRegimen(tkn).Wait();
        }
        private async Task GetRegimen(string Token = null)
        {
            using (var client = new HttpClient())
            {
                List<RegimenFiscalModel> lus = new List<RegimenFiscalModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (Token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RegimenFiscalModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RegimenFiscalModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaRegimenFiscal = lus;
            }
        }
        public void BuscarListaClientes(short idEmpresa, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaClientes"];
            GetListaClientes(idEmpresa, tkn).Wait();
        }
        private async Task GetListaClientes(short idEmpresa, string Token)
        {
            using (var client = new HttpClient())
            {
                List<ClientesDto> lus = new List<ClientesDto>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<ClientesDto>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<ClientesDto>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }

                var item = AgregaritemCliente();
                item.AddRange(lus);
                _lstaClientes = item;
            }
        }
        public List<ClientesDto> AgregaritemCliente()
        {
            ClientesDto rol = new ClientesDto();
            rol.Cliente = "Seleccione";
            List<ClientesDto> Paises = new List<ClientesDto>();
            Paises.Add(rol);
            return Paises;
        }
        public void BuscarCliente(int id)//short idEmpresa, 
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetCliente"];
            GetCliente(id).Wait();
        }
        private async Task GetCliente(int id, string token = null)
        {
            using (var client = new HttpClient())
            {
                ClientesModel c = new ClientesModel();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiCatalgos, id.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        c = await response.Content.ReadAsAsync<ClientesModel>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    c = new ClientesModel();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ClienteModel = c;
            }
        }
        public void BuscarListaClientes(int id, int TipoPersona, int regimen, string rfc, string nombre, string tkn)//short idEmpresa, 
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetClientes"];
            GetListaClientes(id, TipoPersona, regimen, rfc, nombre, tkn).Wait();
        }
        private async Task GetListaClientes(int id, int TipoPersona, int regimen, string rfc, string nombre, string Token)
        {
            using (var client = new HttpClient())
            {
                List<ClientesDto> lus = new List<ClientesDto>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<ClientesDto>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<ClientesDto>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }

                if (id != 0 || (rfc != "" && rfc != null) || (nombre != "" && nombre != null) || (TipoPersona != 0) || (regimen != 0))
                {
                    if (id != 0)
                    {
                        lus = (from x in lus where x.IdCliente == id select x).ToList();
                    }

                    if (rfc != "" && rfc != null)
                    {
                        lus = (from x in lus where x.Rfc == rfc select x).ToList();
                    }

                    if (nombre != "" && nombre != null)
                    {
                        lus = (from x in lus where x.RazonSocial == nombre || (x.Nombre + " " + x.Apellido1) == nombre select x).ToList();
                    }
                    if (TipoPersona != 0)
                    {
                        lus = (from x in lus where x.IdTipoPersona == TipoPersona select x).ToList();
                    }

                    if (regimen != 0)
                    {
                        lus = (from x in lus where x.IdRegimenFiscal == regimen select x).ToList();
                    }

                }

                _lstaClientes = lus;
            }
        }
        public void BuscarListaClientesMod(int cliente, string tel1, int pedido, string rfc, string tkn)//short idEmpresa, 
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetClientes"];
            GetListaClientesMod(cliente, tel1, pedido, rfc, tkn).Wait();
        }
        private async Task GetListaClientesMod(int cliente, string tel1, int numP, string rfc, string Token)
        {
            using (var client = new HttpClient())
            {
                List<ClientesModel> lus = new List<ClientesModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    if (tel1 != "" || rfc != "" || numP != 0 || cliente != 0)
                    {
                        HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                        if (response.IsSuccessStatusCode)
                            lus = await response.Content.ReadAsAsync<List<ClientesModel>>();
                        else
                        {
                            client.CancelPendingRequests();
                            client.Dispose();
                        }
                        if (cliente != 0)
                            lus = (from x in lus where x.IdCliente.Equals(cliente) select x).ToList();

                        if (tel1 != "" && tel1 != null)
                            lus = (from x in lus
                                   where x.Telefono1.Equals(tel1)
                                      || x.Celular.Equals(tel1)
                                      || x.Celular1.Equals(tel1)
                                   select x).ToList();

                        if (rfc != "" && rfc != null)
                            lus = (from x in lus where x.Rfc.Equals(rfc) select x).ToList();

                        if (numP > 0)
                        {
                            //  lus = (from x in lus where x.Id == tel2 select x).ToList();
                        }

                    }

                }
                catch (Exception)
                {
                    lus = new List<ClientesModel>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _lstaClientesMod = lus;

            }
        }

        public void BuscarClientesRfcTel(ClientesDto mod, string tkn)//short idEmpresa, 
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutClientesRfcTel"];
            PutClientesRfcTel(mod, tkn).Wait();
        }
        private async Task PutClientesRfcTel(ClientesDto dto, string Token)
        {
            using (var client = new HttpClient())
            {
                List<ClientesDto> lus = new List<ClientesDto>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {                    
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, dto).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                            lus = await response.Content.ReadAsAsync<List<ClientesDto>>();
                        else
                        {
                            client.CancelPendingRequests();
                            client.Dispose();
                        }  

                }
                catch (Exception)
                {
                    lus = new List<ClientesDto>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _lstaClientes = lus;

            }
        }

        public void BuscarListaLocaciones(int id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaLocacion"];
            GetListaLocaciones(id, tkn).Wait();
        }
        private async Task GetListaLocaciones(int id, string Token)
        {
            using (var client = new HttpClient())
            {
                List<ClienteLocacionMod> lus = new List<ClienteLocacionMod>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<ClienteLocacionMod>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<ClienteLocacionMod>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }

                _cteLocacion = lus;

                //  SetEdoPais(lus,Token);
            }
        }
        public void GuardarNuevoCliente(ClientesModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraClientes"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void EditarCliente(ClientesDto dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaClientes"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarCliente(int id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutEliminaClientes"];
            EliminarClienteSeleccionado(id, tkn).Wait();
        }
        private async Task EliminarClienteSeleccionado(int _id, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos + _id.ToString(), "").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        public void GuardarClienteLocacion(ClienteLocacionMod dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraClienteLoc"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void EditarClienteLocacion(ClienteLocacionMod dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaClienteLocacion"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarClienteLocacion(ClienteLocacionMod dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaClienteLocacion"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void GuardarNuevoCliente(ClientesModel dto)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraClientesAutoConsumo"];
            LLamada(dto, string.Empty, MetodoRestConst.Post, true).Wait();
        }
        public void EditarCliente(ClientesModel dto)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaClientesAutoConsumo"];
            LLamada(dto, string.Empty, MetodoRestConst.Put, true).Wait();
        }
        #endregion
        #region Puntos de Venta
        public void BuscarListaPuntosVenta(int idPV, string tkn)//short idEmpresa, 
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetPuntosVenta"];
            GetListaPV(idPV, tkn).Wait();
        }
        private async Task GetListaPV(int idPV, string Token)
        {
            using (var client = new HttpClient())
            {
                List<PuntoVentaModel> lus = new List<PuntoVentaModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<PuntoVentaModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<PuntoVentaModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }

                if (idPV != 0)
                {
                    _listaPuntosV = (from x in lus where x.IdPuntoVenta == idPV select x).ToList();
                }
                else
                {
                    _listaPuntosV = lus;
                }
            }
        }
        public void BuscarListaPuntosVentaId(short _idEmpresa, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetPuntosVentaId"];
            GetListaPVId(tkn, _idEmpresa).Wait();
        }
        private async Task GetListaPVId(string Token, short idEmpresa)
        {
            using (var client = new HttpClient())
            {
                List<PuntoVentaModel> lus = new List<PuntoVentaModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiCatalgos, idEmpresa)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<PuntoVentaModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<PuntoVentaModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaPuntosV = lus;
            }
        }
        public void EliminarPuntosVenta(PuntoVentaModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaPuntosVenta"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void BuscarUsarioOperador(short id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetUsuariosPuntoVenta"];
            GetUsuariosOpe(id, tkn).Wait();
        }
        private async Task GetUsuariosOpe(short id, string Token)
        {
            using (var client = new HttpClient())
            {
                List<OperadorChoferModel> lus = new List<OperadorChoferModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<OperadorChoferModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<OperadorChoferModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaOperadoresUsuarios = lus;
            }
        }
        public void BuscarIdChofer(int id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetOperadorIdUsuario"];
            GetIdChofer(id, tkn).Wait();
        }
        private async Task GetIdChofer(int id, string Token)
        {
            using (var client = new HttpClient())
            {
                OperadorChoferModel lus = new OperadorChoferModel();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<OperadorChoferModel>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new OperadorChoferModel();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                Operador = lus;
            }
        }
        public void EditarPuntoVenta(PuntoVentaModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaOperador"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        #endregion
        #region Precio de Venta Gas
        public void BuscarListaPrecioVenta(short idPrecioV, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaPrecioVenta"];
            GetListaPreciosV(idPrecioV, tkn).Wait();
        }
        private async Task GetListaPreciosV(short idPV, string Token)
        {
            using (var client = new HttpClient())
            {
                List<PrecioVentaModel> lus = new List<PrecioVentaModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<PrecioVentaModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<PrecioVentaModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }

                if (idPV != 0)
                {
                    _listaPreciosV = (from x in lus where x.IdPrecioVenta == idPV select x).ToList();
                }
                else
                {
                    _listaPreciosV = lus;
                }
            }
        }
        public void BuscarListaEstatus(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaEstatusPV"];
            GetListaEstatus(tkn).Wait();
        }
        private async Task GetListaEstatus(string Token)
        {
            using (var client = new HttpClient())
            {
                List<EstatusTipoFechaModel> lus = new List<EstatusTipoFechaModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<EstatusTipoFechaModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<EstatusTipoFechaModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }

                _listaEstatus = lus;

            }
        }
        public void BuscarListaPreciosVentaIdEmpresa(short idEmpresa, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaPrecioVIdEmpresa"];
            GetListaPrecioVIdE(tkn, idEmpresa).Wait();
        }
        private async Task GetListaPrecioVIdE(string Token, short idEmpresa)
        {
            using (var client = new HttpClient())
            {
                List<PrecioVentaModel> lus = new List<PrecioVentaModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + idEmpresa.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<PrecioVentaModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<PrecioVentaModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaPreciosV = lus;
            }
        }
        public void EliminarPrecioVenta(PrecioVentaModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaPreciosVenta"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void GuardarPrecioVenta(PrecioVentaModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraPrecioVenta"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void ModificarPrecioVenta(PrecioVentaModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaPreciosVenta"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        #endregion
        #region Caja General
        public List<PaisModel> AddSelect()
        {
            PaisModel rol = new PaisModel();
            rol.Pais = "Seleccione";
            List<PaisModel> Paises = new List<PaisModel>();
            Paises.Add(rol);
            return Paises;
        }
        public void BuscarListaVentaCajaGral(string tkn, string type)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaCajaGral"];
            GetListaCajaGral(tkn, type).Wait();
        }
        private async Task GetListaCajaGral(string Token, string type)
        {
            using (var client = new HttpClient())
            {
                List<CajaGeneralModel> lus = new List<CajaGeneralModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<CajaGeneralModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<CajaGeneralModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }

                // _listaCajaGral = lus.OrderByDescending(x=> x.FechaAplicacion).ToList();
                if (type == "Entidad")
                {
                    _listaCajaGral = (from x in lus
                                      orderby x.PuntoVenta
                                      select x).Distinct().ToList();
                }

                else
                {
                    _listaCajaGral = lus.OrderByDescending(x => x.FechaAplicacion).ToList();
                    _listaCajaGral = _listaCajaGral.OrderByDescending(x => x.Orden).ToList();
                }
            }
        }
        public void BuscarListaVentaCajaGralIdE(short idE, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaCajaGralId"];
            GetListaCajaGral(idE, tkn).Wait();
        }
        private async Task GetListaCajaGral(short id, string Token)
        {
            using (var client = new HttpClient())
            {
                List<CajaGeneralModel> lus = new List<CajaGeneralModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<CajaGeneralModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<CajaGeneralModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }

                if (id != 0)
                {
                    _listaCajaGral = (from x in lus where x.IdEmpresa == id select x).ToList();
                }
                else
                {
                    _listaCajaGral = lus;
                }
            }
        }
        public void BuscarListaCajaGralCamioneta(string cveReporte, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaCajaGralCamioneta"];
            GetListaCajaGralCamioneta(cveReporte, tkn).Wait();
        }
        private async Task GetListaCajaGralCamioneta(string cveRep, string Token)
        {
            using (var client = new HttpClient())
            {
                List<CajaGeneralCamionetaModel> lus = new List<CajaGeneralCamionetaModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + cveRep).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<CajaGeneralCamionetaModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<CajaGeneralCamionetaModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }


                _listaCajaGralCamioneta = lus;

            }
        }
        public void BuscarListaCajaGralEstacion(string cveReporte, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaCajaGralEstacion"];
            GetListaCajaGralEstacion(cveReporte, tkn).Wait();
        }
        private async Task GetListaCajaGralEstacion(string cveRep, string Token)
        {
            using (var client = new HttpClient())
            {
                List<VentaCorteAnticipoModel> lus = new List<VentaCorteAnticipoModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + cveRep).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<VentaCorteAnticipoModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<VentaCorteAnticipoModel>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }

                _listaCajaGralEstacion = lus;

            }
        }
        public void GuardarLiquidacion(CajaGeneralCamionetaModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutLiquidarCajaGral"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void GuardarLiquidacionEst(VentaCorteAnticipoModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutLiquidarCajaGralEst"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void BuscarListaMovGas(CajaGeneralCamionetaModel reporte, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutListaMovGas"];
            GetListaMovimientosGas(reporte, tkn).Wait();
        }
        private async Task GetListaMovimientosGas(CajaGeneralCamionetaModel rep, string Token)
        {
            using (var client = new HttpClient())
            {
                List<MovimientosGasModel> lus = new List<MovimientosGasModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, rep).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<MovimientosGasModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<MovimientosGasModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaMovimientosGas = lus;

            }
        }
        public void BuscarListaMovGasCilindros(MovimientosGasCilindros reporte, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutListaMovGasCilindros"];
            GetListaMovimientosGasC(reporte, tkn).Wait();
        }
        private async Task GetListaMovimientosGasC(MovimientosGasCilindros rep, string Token)
        {
            using (var client = new HttpClient())
            {
                List<MovimientosGasCilindros> lus = new List<MovimientosGasCilindros>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, rep).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<MovimientosGasCilindros>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<MovimientosGasCilindros>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }

                _ListaMovimientosGasC = lus;
            }
        }
        #endregion
        #region Paises
        public void BuscarPaises(string tkn = null)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaPaises"];
            ListaPaises(this.ApiCatalgos, tkn).Wait();
        }

        public List<PaisModel> AgregaritemP()
        {
            PaisModel rol = new PaisModel();
            rol.Pais = "Seleccione";
            List<PaisModel> Paises = new List<PaisModel>();
            Paises.Add(rol);
            return Paises;
        }
        private async Task ListaPaises(string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<PaisModel> emp = new List<PaisModel>();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<PaisModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<PaisModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                var item = AgregaritemP();
                item.AddRange(emp);
                _listaPaises = item;
            }
        }

        #endregion
        #region Estados

        public List<EstadosRepModel> AgregaritemE()
        {
            EstadosRepModel rol = new EstadosRepModel();
            rol.Estado = "Seleccione";
            List<EstadosRepModel> Edos = new List<EstadosRepModel>();
            Edos.Add(rol);

            return Edos;

        }
        public void BuscarEstados(string tkn = null)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaEstadosR"];
            ListaEstados(this.ApiCatalgos, tkn).Wait();
        }

        private async Task ListaEstados(string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<EstadosRepModel> emp = new List<EstadosRepModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    //HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    HttpResponseMessage response = await client.GetAsync(api).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<EstadosRepModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<EstadosRepModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                var item = AgregaritemE();
                item.AddRange(emp);
                _listaEstados = item;

            }
        }

        #endregion
        #region Tipo Proveedor
        public void ListaTipoProveedor(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaTipoProveedores"];
            GetListaTipoProveedor(tkn).Wait();
        }
        private async Task GetListaTipoProveedor(string Token)
        {
            using (var client = new HttpClient())
            {
                List<TipoProveedorDTO> list = new List<TipoProveedorDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<TipoProveedorDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<TipoProveedorDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaTipoProveedor = list;
            }
        }
        #endregion
        #region Banco
        public void ListaBanco(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaBancos"];
            GetListaBanco(tkn).Wait();
        }
        private async Task GetListaBanco(string Token)
        {
            using (var client = new HttpClient())
            {
                List<BancoDTO> list = new List<BancoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<BancoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<BancoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaBanco = list;
            }
        }
        #endregion
        #region Forma de Pago
        public void ListaFormaPago(string tkn = null)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaFormasPago"];
            GetListaFormaPago(tkn).Wait();
        }
        private async Task GetListaFormaPago(string Token = null)
        {
            using (var client = new HttpClient())
            {
                List<FormaPagoDTO> list = new List<FormaPagoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (Token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<FormaPagoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<FormaPagoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaFormaPago = list;
            }
        }
        #endregion
        #region Centros de costos
        public void BuscarCentrosCostos(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetCentrosCostos"];
            ListaCentrosCosto(tkn).Wait();
        }
        private async Task ListaCentrosCosto(string token)
        {
            using (var client = new HttpClient())
            {
                List<CentroCostoDTO> emp = new List<CentroCostoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<CentroCostoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<CentroCostoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaCentroCosto = emp;
            }
        }
        public void BuscarCentrosCostos(string tkn, bool EsExterno)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetCentrosCostos"];
            ListaCentrosCosto(tkn).Wait();
        }
        private async Task ListaCentrosCosto(string token, bool EsExterno)
        {
            using (var client = new HttpClient())
            {
                List<CentroCostoDTO> emp = new List<CentroCostoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiCatalgos, EsExterno)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<CentroCostoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<CentroCostoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaCentroCosto = emp;
            }
        }
        public void GuardarCentroCosto(CentroCostoCrearDTO dto, string tkn)
        {
            //this.ApiCatalgos = ConfigurationManager.AppSettings["PostRegistraCentroCosto"];
            //GuardarCtroCosto(dto, tkn).Wait();

            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraCentroCosto"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        private async Task GuardarCtroCosto(CentroCostoCrearDTO _pcDTO, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        public void EliminarCtroCosto(CentroCostoEliminarDTO dto, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutEliminaCentroCosto"];
            EliminarCentroCosto(dto, token).Wait();
        }
        private async Task EliminarCentroCosto(CentroCostoEliminarDTO _dto, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, _dto).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        public void ModificarCtroCosto(CentroCostoModificarDTO dto, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutModificaCentroCosto"];
            ModificarCentroCosto(dto, token).Wait();
        }
        private async Task ModificarCentroCosto(CentroCostoModificarDTO _dto, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, _dto).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        public void BuscarListaTipoCentroCosto(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetTipoCentroCostos"];
            GetListaTipoCentroCosto(tkn).Wait();
        }
        private async Task GetListaTipoCentroCosto(string Token)
        {
            using (var client = new HttpClient())
            {
                List<TipoCentroCostoDTO> list = new List<TipoCentroCostoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<TipoCentroCostoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<TipoCentroCostoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaTipoCentroCosto = list;
            }
        }
        #endregion
        #region Productos
        public void BuscarProductos(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaProductos"];
            ListaProductosPorIdEmpresa(tkn).Wait();
        }
        private async Task ListaProductosPorIdEmpresa(string token)
        {
            using (var client = new HttpClient())
            {
                List<ProductoDTO> emp = new List<ProductoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<ProductoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<ProductoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaProductos = emp;
            }
        }
        public void GuardarProducto(ProductoDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraProducto"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void ModificarProducto(ProductoDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaProducto"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarProducto(ProductoDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaProducto"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        #endregion
        #region Proveedor
        public void BuscarProveedores(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaProveedores"];
            ListaProveedores(tkn).Wait();
        }
        private async Task ListaProveedores(string token)
        {
            using (var client = new HttpClient())
            {
                List<ProveedorDTO> emp = new List<ProveedorDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<ProveedorDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<ProveedorDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaProveedores = emp;
            }
        }
        public void GuardarProveedor(ProveedorDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraProveedor"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void ModificarProveedor(ProveedorDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaProveedor"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarProveedor(ProveedorDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaProveedor"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        #endregion
        #region Cuentas Contables
        public void BuscarCuentasContables(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaCuentasContables"];
            ListaCuentaContable(tkn).Wait();
        }
        private async Task ListaCuentaContable(string token)
        {
            using (var client = new HttpClient())
            {
                List<CuentaContableDTO> emp = new List<CuentaContableDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiCatalgos)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<CuentaContableDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<CuentaContableDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaCuentasContables = emp;
            }
        }
        public void GuardarCuentaContable(CuentaContableCrearDTO _cc, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostRegistraCuentaContable"];
            SaveCtaCtble(_cc, token).Wait();
        }
        private async Task SaveCtaCtble(CuentaContableCrearDTO _cc, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiCatalgos, _cc).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        public void ModificarCtaCtble(CuentaContableModificarDTO _cc, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PutModificaCuentaContable"];
            ModificarCuentaContable(_cc, token).Wait();
        }
        private async Task ModificarCuentaContable(CuentaContableModificarDTO _cc, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _cc).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        public void EliminarCtaCtble(CuentaContableEliminarDTO _cc, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PutEliminaCuentaContable"];
            EliminarCuentaContable(_cc, token).Wait();
        }
        private async Task EliminarCuentaContable(CuentaContableEliminarDTO _cc, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _cc).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        #endregion
        #region EstacionCarburacion
        public void BuscarListaEstacionCarburacion(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaEstacionCarburacion"];
            GetListaEstacionCarburacion(tkn).Wait();
        }
        private async Task GetListaEstacionCarburacion(string Token)
        {
            using (var client = new HttpClient())
            {
                List<EstacionCarburacionDTO> list = new List<EstacionCarburacionDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<EstacionCarburacionDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<EstacionCarburacionDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaEstacionCarburacion = list;
            }
        }
        #endregion
        #region Unidad Almacen Gas
        public void BuscarListaUnidadAlmacenGas(short idEmpresa, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaUnidadAlmcenGas"];
            GetListaUnidadAlmacenGas(idEmpresa, tkn).Wait();
        }
        private async Task GetListaUnidadAlmacenGas(short IdEmpresa, string Token)
        {
            using (var client = new HttpClient())
            {
                List<UnidadAlmacenGasDTO> list = new List<UnidadAlmacenGasDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + IdEmpresa.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<UnidadAlmacenGasDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<UnidadAlmacenGasDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaUnidadAlmacenGas = list;
            }
        }
        #endregion
        #region Equipo de transporte
        public void BuscarListaEquipoTransporte(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaEquiposTransporte"];
            GetListaEquipoTransporte(tkn).Wait();
        }
        private async Task GetListaEquipoTransporte(string Token)
        {
            using (var client = new HttpClient())
            {
                List<EquipoTransporteDTO> list = new List<EquipoTransporteDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<EquipoTransporteDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<EquipoTransporteDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaEquipoTransporte = list;
            }
        }
        public void ListaVehiculos(short id, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaEquiposTransporte"];
            Vehiculos(id, ApiCatalgos, token).Wait();
        }
        private async Task Vehiculos(short id, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<EquipoTransporteDTO> pedidos = new List<EquipoTransporteDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        pedidos = await response.Content.ReadAsAsync<List<EquipoTransporteDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    pedidos = new List<EquipoTransporteDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaVehiculos = pedidos;
            }
        }
        public void ListaVehiculosFiltrar(short id, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaEquiposTransporte"];
            VehiculosFiltrar(id, ApiCatalgos, token).Wait();
        }
        private async Task VehiculosFiltrar(short id, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<EquipoTransporteDTO> dto = new List<EquipoTransporteDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        dto = await response.Content.ReadAsAsync<List<EquipoTransporteDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    dto = new List<EquipoTransporteDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                //if (Placas != ""&& Placas != null)                
                //    pedidos = (from x in pedidos where x.Placas == Placas select x).ToList();

                //if (Nombre != "" && Nombre != null)                
                //    pedidos = (from x in pedidos where x.AliasUnidad.Contains(Nombre) select x).ToList();


                _ListaVehiculos = dto;
            }
        }
        public void ObtenerVehiculoId(int id, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetVehiculoId"];
            VehiculoId(ApiCatalgos, id, token).Wait();
        }
        private async Task VehiculoId(string api, int id, string token = null)
        {
            using (var client = new HttpClient())
            {
                EquipoTransporteDTO pedido = new EquipoTransporteDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        pedido = await response.Content.ReadAsAsync<EquipoTransporteDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    pedido = new EquipoTransporteDTO();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _Vehiculos = pedido;
            }
        }
        public void GuardarVehiculo(EquipoTransporteDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistrarVehiculo"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void EditarVehiculo(EquipoTransporteDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificarVehiculo"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarVehiculo(int id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutEliminarVehiculo"];
            EliminarVehiculoSeleccionado(id, tkn).Wait();
        }
        private async Task EliminarVehiculoSeleccionado(int _id, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos + _id.ToString(), "").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        #endregion
        #region Producto Categoria
        public void GuardarCategoria(CategoriaProductoDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraCategoriaProducto"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void ModificarCategoria(CategoriaProductoDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaCategoriaProducto"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarCategoria(CategoriaProductoDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaCategoriaProducto"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void ListaCategoriasProducto(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetCategoriasProducto"];
            GetListaCategoriasProducto(tkn).Wait();
        }
        private async Task GetListaCategoriasProducto(string Token)
        {
            using (var client = new HttpClient())
            {
                List<CategoriaProductoDTO> list = new List<CategoriaProductoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<CategoriaProductoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<CategoriaProductoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaCategoriasProducto = list;
            }
        }
        #endregion
        #region Linea Producto
        public void GuardarLineaProducto(LineaProductoDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraLineaProducto"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void ModificarLineaProducto(LineaProductoDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaLineaProducto"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarLineaProducto(LineaProductoDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaLineaProducto"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void ListaLienasProducto(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetLineasProducto"];
            GetListaLienasProducto(tkn).Wait();
        }
        private async Task GetListaLienasProducto(string Token)
        {
            using (var client = new HttpClient())
            {
                List<LineaProductoDTO> list = new List<LineaProductoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<LineaProductoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<LineaProductoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaLineasProducto = list;
            }
        }
        #endregion
        #region Unidad de Medidad
        public void GuardarUnidadMedida(UnidadMedidaDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraUnidadMedida"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void ModificarUnidadMedida(UnidadMedidaDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaUnidadMedida"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarUnidadMedida(UnidadMedidaDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaUnidadMedida"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();

        }
        public void ListaUnidadesMedida(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetUnidadMedidas"];
            GetListaUnidadesMedida(tkn).Wait();
        }
        private async Task GetListaUnidadesMedida(string Token)
        {
            using (var client = new HttpClient())
            {
                List<UnidadMedidaDTO> list = new List<UnidadMedidaDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<UnidadMedidaDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<UnidadMedidaDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaUnidadesMedida = list;
            }
        }
        #endregion
        #region Combustible
        public void GetListaCombustible(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaCCombustible"];
            GetListaCCombustible(tkn).Wait();
        }
        private async Task GetListaCCombustible(string Token)
        {
            using (var client = new HttpClient())
            {
                List<CombustibleModel> list = new List<CombustibleModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<CombustibleModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<CombustibleModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaCombustibles = list;
            }
        }
        public void GetListaTiposUnidad(short idempresa, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetTiposUnidad"];
            GetListaTiposUnidadIdE(idempresa, tkn).Wait();
        }
        private async Task GetListaTiposUnidadIdE(short idempresa, string Token)
        {
            using (var client = new HttpClient())
            {
                List<TipoUnidadModel> list = new List<TipoUnidadModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + idempresa.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<TipoUnidadModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<TipoUnidadModel>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _ListaTiposUnidad = list;
            }
        }
        public void GetListaCombustibleIdE(short idempresa, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetCCombustibleIdEmpr"];
            GetListaCCombustibleIdE(idempresa, tkn).Wait();
        }
        private async Task GetListaCCombustibleIdE(short idempresa, string Token)
        {
            using (var client = new HttpClient())
            {
                List<CombustibleModel> list = new List<CombustibleModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + idempresa.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<CombustibleModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<CombustibleModel>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _ListaCombustibles = list;
            }
        }
        public void ListaCombustibleFiltrar(CombustibleModel dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutCCombustibleFiltro"];
            GetListaCCombustibleFiltro(dto, tkn).Wait();
        }
        private async Task GetListaCCombustibleFiltro(CombustibleModel dto, string Token)
        {
            using (var client = new HttpClient())
            {
                List<CombustibleModel> list = new List<CombustibleModel>();
                RespuestaDTO resp = new RespuestaDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, dto).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<CombustibleModel>>();
                    else
                    {
                        _RespuestaDTO = resp;
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<CombustibleModel>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _ListaCombustibles = list;
            }
        }
        public void GetListaCombustibleID(int id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetCCombustibleID"];
            GetListaCCombustibleID(id, tkn).Wait();
        }
        private async Task GetListaCCombustibleID(int id, string Token)
        {
            using (var client = new HttpClient())
            {
                CombustibleModel list = new CombustibleModel();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<CombustibleModel>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new CombustibleModel();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _Combustible = list;
            }
        }
        public void GuardarCombustible(CombustibleModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraCombustible"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void ModificarCombustible(CombustibleModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaCombustible"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarCombustible(int idCombustible, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaCombustible"];
            EliminarCCombustible(idCombustible, tkn).Wait();
        }
        private async Task EliminarCCombustible(int _id, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos + _id.ToString(), "").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        #endregion
        #region Medidores
        public void GetListaMedidores(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetObtenerMedidores"];
            GetListaCMedidores(tkn).Wait();
        }
        private async Task GetListaCMedidores(string Token)
        {
            using (var client = new HttpClient())
            {
                List<MedidorDTO> list = new List<MedidorDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<MedidorDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<MedidorDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaMedidores = list;
            }
        }
        #endregion
        #region Uso CFDI
        public void ListaUsoCFDI(string tkn = null)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaUsoCFDI"];
            GetListaUsoCFDI(tkn).Wait();
        }
        private async Task GetListaUsoCFDI(string Token = null)
        {
            using (var client = new HttpClient())
            {
                List<UsoCFDIDTO> list = new List<UsoCFDIDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (Token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<UsoCFDIDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<UsoCFDIDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaUsoCFDI = list;
            }
        }
        #endregion
        #region Metodo de Pago
        public void ListaMetodosPago(string tkn = null)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaMetodosPago"];
            GetListaMetodosPago(tkn).Wait();
        }
        private async Task GetListaMetodosPago(string Token = null)
        {
            using (var client = new HttpClient())
            {
                List<MetodoPagoDTO> list = new List<MetodoPagoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (Token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<MetodoPagoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<MetodoPagoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaMetodPago = list;
            }
        }
        #endregion
        #region Egreso
        public void BuscarListaEgreso(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetEgresos"];
            GetListaEgreso(tkn).Wait();
        }
        private async Task GetListaEgreso(string Token)
        {
            using (var client = new HttpClient())
            {
                List<EgresoDTO> list = new List<EgresoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<EgresoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<EgresoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaEgreso = list;
            }
        }

        public void ObteneEgresoId(int id, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetEgreso"];
            EgresoID(ApiCatalgos, id, token).Wait();
        }
        private async Task EgresoID(string api, int id, string token = null)
        {
            using (var client = new HttpClient())
            {
                EgresoDTO entidad = new EgresoDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        entidad = await response.Content.ReadAsAsync<EgresoDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    entidad = new EgresoDTO();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _EgresoDTO = entidad;
            }
        }
        public void GuardarEgreso(EgresoDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistraEgreso"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void EditarEgreso(EgresoDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificaEgreso"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarEgreso(int id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutEliminaEgreso"];
            EliminarVehiculoSeleccionado(id, tkn).Wait();
        }

        #endregion

        #endregion
        #region Login
        public void Acceder(AutenticacionDTO autDto)
        {
            this.ApiLogin = ConfigurationManager.AppSettings["PostLogin"];
            Login(autDto).Wait();
        }
        private async Task Login(AutenticacionDTO autDto)
        {
            using (var client = new HttpClient())
            {
                RespuestaAutenticacionDto respuesta = new RespuestaAutenticacionDto();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiLogin, autDto).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        respuesta = await response.Content.ReadAsAsync<RespuestaAutenticacionDto>();
                    else
                    {
                        respuesta.Mensaje = "Usuario y/o contraseña incorrectos";
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    respuesta.Mensaje = ex.Message.ToString();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaAutenticacion = respuesta;
            }
        }
        #endregion
        #region Requisicion
        public void BuscarRequisicionEstatus(string Tkn)
        {
            ApiRequisicion = ConfigurationManager.AppSettings["GetRequisicionEstatus"];
            ListaRequisicionesEstatus(Tkn).Wait();
        }
        private async Task ListaRequisicionesEstatus(string token)
        {
            using (var client = new HttpClient())
            {
                List<RequisicionEstatusDTO> emp = new List<RequisicionEstatusDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiRequisicion).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<RequisicionEstatusDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<RequisicionEstatusDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaRequisicionEstatus = emp;
            }
        }
        public void BuscarRequisiciones(short idEmpresa, string tkn)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["GetRequisicionesByIdEmpresa"];
            ListaRequisiciones(idEmpresa, tkn).Wait();
        }
        private async Task ListaRequisiciones(short idEmpresa, string token)
        {
            using (var client = new HttpClient())
            {
                List<RequisicionDTO> emp = new List<RequisicionDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiRequisicion + idEmpresa.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<RequisicionDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<RequisicionDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaRequisicion = emp;
            }
        }
        public void GuardarRequisicion(RequisicionDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRequisicion"];
            LLamada(dto, token, MetodoRestConst.Post).Wait();
        }
        public void ActualizarRequisicionRevision(RequisicionRevPutDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutActulizarRevision"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void ActualizarRequisicionAutorizacion(RequisicionAutPutDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutActulizarAutorizacion"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void ActualizarRequisicionCancelar(RequisicionCancelaDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutCancelarRequisicion"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void RequisicionRevision(int IdRequisicion, string tkn)
        {
            ApiRequisicion = ConfigurationManager.AppSettings["GetRequisicionByNumRequisicion"];
            BuscarRequisicioRevision(IdRequisicion, tkn).Wait();
        }
        private async Task BuscarRequisicioRevision(int IdRequisicion, string token)
        {
            using (var client = new HttpClient())
            {
                RequisicionRevisionDTO emp = new RequisicionRevisionDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiRequisicion, IdRequisicion)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<RequisicionRevisionDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new RequisicionRevisionDTO() { NumeroRequisicion = "0" };
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _requisicionRevisionDTO = emp;
            }
        }
        public void BuscarRequisicioAuto(int IdRequisicion, string tkn)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["GetRequisicionByNumRequisicionAut"];
            RequisicionAuto(IdRequisicion, tkn).Wait();
        }
        private async Task RequisicionAuto(int IdReq, string token)
        {
            using (var client = new HttpClient())
            {
                RequisicionAutorizacionDTO emp = new RequisicionAutorizacionDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiRequisicion, IdReq.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<RequisicionAutorizacionDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new RequisicionAutorizacionDTO() { NumeroRequisicion = "0" };
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _requsicionAutorizacion = emp;
            }
        }
        #endregion
        #region Orden de Compra
        public void BuscarRequisicionOC(int idReq, string tkn)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetBuscarReq"];
            RequisicionPorIdReqOC(idReq, tkn).Wait();
        }
        private async Task RequisicionPorIdReqOC(int numReq, string token)
        {
            using (var client = new HttpClient())
            {
                RequisicionOCDTO emp = new RequisicionOCDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiOrdenCompra + numReq).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<RequisicionOCDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new RequisicionOCDTO() { NumeroRequisicion = "0" };
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _requisicionOrdenCompra = emp;
            }
        }
        public void GuardarOrdenesCompra(OrdenCompraCrearDTO ocDTO, string token)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["PostGenerarOrdenesCompra"];
            SaveOrdenCompra(ocDTO, token).Wait();
        }
        private async Task SaveOrdenCompra(OrdenCompraCrearDTO _oc, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiOrdenCompra, _oc).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    resp.Exito = false;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        public void BuscarOrdenesCompra(short idEmpresa, string tkn)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetOrdenesCompra"];
            ListaOrdenCompra(idEmpresa, tkn).Wait();
        }
        private async Task ListaOrdenCompra(short idEmpresa, string token)
        {
            using (var client = new HttpClient())
            {
                List<OrdenCompraDTO> emp = new List<OrdenCompraDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiOrdenCompra, idEmpresa.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<OrdenCompraDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<OrdenCompraDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaOrdenCompra = emp;
            }
        }
        public void BuscarOrdenesCompraEntrada(int idOC, string tkn)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetOrdenCompraEntrada"];
            GetOrdenCompraEntrada(idOC, tkn).Wait();
        }
        private async Task GetOrdenCompraEntrada(int idOC, string token)
        {
            using (var client = new HttpClient())
            {
                EntradaMercanciaModel emp = new EntradaMercanciaModel();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiOrdenCompra, idOC.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<EntradaMercanciaModel>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new EntradaMercanciaModel();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _entradaMercancia = emp;
            }
        }
        public void CancelarOrdenCompra(OrdenCompraDTO _oc, string token)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["PutCancelarOrdenCompra"];
            CancelarOC(_oc, token).Wait();
        }
        private async Task CancelarOC(OrdenCompraDTO _oc, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiOrdenCompra, _oc).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        _RespuestaDTO = resp;
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }
        public void AutorizarOrdenCompra(OrdenCompraDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutAutorizarCompra"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void BuscarOrdenCompra(int idOC, string tkn)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetBuscarOrdenCompra"];
            OrdenCompra(idOC, tkn).Wait();
        }
        private async Task OrdenCompra(int idOrdenCompra, string token)
        {
            using (var client = new HttpClient())
            {
                OrdenCompraDTO emp = new OrdenCompraDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiOrdenCompra, idOrdenCompra.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<OrdenCompraDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new OrdenCompraCrearDTO();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ordeCompraDTO = emp;
            }
        }
        public void BuscarOrdenCompraEstatus(string tkn)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetOrdenCompraEstatus"];
            ListaOrdenCompraEstatus(tkn).Wait();
        }
        private async Task ListaOrdenCompraEstatus(string token)
        {
            using (var client = new HttpClient())
            {
                List<OrdenCompraEstatusDTO> emp = new List<OrdenCompraEstatusDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiOrdenCompra).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<OrdenCompraEstatusDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<OrdenCompraEstatusDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaOrdenCompraEstatus = emp;
            }
        }
        public void RegistrarEntrada(EntradaMercanciaModel dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostGuardarEntradas"];
            LLamada(dto, token, MetodoRestConst.Post).Wait();
        }
        public void EnviarConfirmarPago(OrdenCompraPagoDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutConfirmarPago"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void EnviarSolicitudPago(OrdenCompraDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutSolicitarPago"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void EnviarSolicitudPagoExpedidor(OrdenCompraComplementoGasDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutSolicitarPagoExpedidor"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void EnviarSolicitudPagoPorteador(OrdenCompraComplementoGasDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutSolicitarPagoPorteador"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void GuardarDatosPorteador(OrdenCompraComplementoGasDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostGuardarPagoPorteador"];
            LLamada(dto, token, MetodoRestConst.Post).Wait();
        }
        public void GuardarDatosExpedidor(OrdenCompraComplementoGasDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutDatosOrdenCompraExpedidor"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void ConfirmarDatosPapeleta(OrdenCompraComplementoGasDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutGuardarDatosPapeleta"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void EnviarDatosFactura(OrdenCompraDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutDatosFactura"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void BuscarListaPagos(int oc, string tkn)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetListaPagos"];
            GetListaPago(oc, tkn).Wait();
        }
        public void ActualizarProductosOC(List<OrdenCompraProductoDTO> dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutAutorizarProductoOordenCompra"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        private async Task GetListaPago(int idoc, string Token)
        {
            using (var client = new HttpClient())
            {
                List<OrdenCompraPagoDTO> list = new List<OrdenCompraPagoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiOrdenCompra, idoc.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<OrdenCompraPagoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<OrdenCompraPagoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaOrdenCompraPago = list;
            }
        }
        public void BuscarComplementoGas(int idoc, string token)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetOrdenCompraComplementoGas"];
            BuscarOrdenCompraComplementoGas(idoc, token).Wait();
        }
        private async Task BuscarOrdenCompraComplementoGas(int idOC, string token)
        {
            using (var client = new HttpClient())
            {
                OrdenCompraComplementoGasDTO emp = new OrdenCompraComplementoGasDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiOrdenCompra, idOC.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<OrdenCompraComplementoGasDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new OrdenCompraComplementoGasDTO();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _complementoGas = emp;
            }
        }
        #endregion
        #region Almacen
        public void BuscarProductosAlmacen(short idEmpresa, string tkn)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetProductosAlmacen"];
            ListaAlmacenProdcutos(idEmpresa, tkn).Wait();
        }
        private async Task ListaAlmacenProdcutos(short idEmpresa, string token)
        {
            using (var client = new HttpClient())
            {
                List<AlmacenDTO> emp = new List<AlmacenDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiOrdenCompra, idEmpresa.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<AlmacenDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<AlmacenDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaAlmacen = emp;
            }
        }
        public void ActualizarAlmacen(AlmacenDTO dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostActulizarAlmacenProducto"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void BuscarRegistroAlmacen(int id, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["GetRegistroAlmacen"];
            GetRequisicionAlmacen(id, tkn).Wait();
        }
        private async Task GetRequisicionAlmacen(int id, string Token)
        {
            using (var client = new HttpClient())
            {
                List<RegistroDTO> list = new List<RegistroDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiRoute, id.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<RegistroDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<RegistroDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _listaRegistroAlmacen = list;
            }
        }
        public void BuscarRequsicionSalida(int id, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["GetrequisicionAlmacen"];
            GetListaRegistroSalida(id, tkn).Wait();
        }
        private async Task GetListaRegistroSalida(int id, string Token)
        {
            using (var client = new HttpClient())
            {
                RequisicionSalidaDTO dto = new RequisicionSalidaDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiRoute, id.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        dto = await response.Content.ReadAsAsync<RequisicionSalidaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    dto = new RequisicionSalidaDTO();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _RequisicionSalida = dto;
            }
        }
        public void RegistrarSalida(RequisicionSalidaDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostGuardarSalida"];
            LLamada(dto, token, MetodoRestConst.Post).Wait();
        }
        public void BuscarRemanenteGeneral(RemanenteModel model, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRemanenteGeneral"];
            ListaRemanenteGeneral(model, tkn).Wait();
        }
        private async Task ListaRemanenteGeneral(RemanenteModel model, string token)
        {
            using (var client = new HttpClient())
            {
                List<RemanenteGeneralDTO> emp = new List<RemanenteGeneralDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiRoute, model).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<RemanenteGeneralDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<RemanenteGeneralDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaRemanenteGenaral = emp;
            }
        }

        #endregion
        #region Pedidos
        public void ListaPedidos(short id, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaPedidos"];
            Pedidos(id, ApiCatalgos, token).Wait();
        }
        private async Task Pedidos(short id, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<PedidoModel> pedidos = new List<PedidoModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        pedidos = await response.Content.ReadAsAsync<List<PedidoModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    //ex.Message;
                    pedidos = new List<PedidoModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaPedidos = pedidos;
            }
        }
        public void ListaPedidosFiltro(short id, int idpedido, string rfc, string tel1, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaPedidos"];
            PedidosFiltro(id, idpedido, rfc, tel1, ApiCatalgos, token).Wait();
        }
        private async Task PedidosFiltro(short id, int idpedido, string rfc, string tel1, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<PedidoModel> pedidos = new List<PedidoModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        pedidos = await response.Content.ReadAsAsync<List<PedidoModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    //ex.Message;
                    pedidos = new List<PedidoModel>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                if (idpedido > 0)
                {
                    pedidos = (from x in pedidos where x.IdPedido == idpedido select x).ToList();
                }
                if (rfc != "" && rfc != null)
                {
                    pedidos = (from x in pedidos where x.cliente.Rfc == rfc select x).ToList();
                }

                if (tel1 != "" && tel1 != null)
                {
                    pedidos = (from x in pedidos where x.cliente.Telefono1 == tel1 || x.cliente.Celular1.Equals(tel1) || x.cliente.Celular.Equals(tel1) select x).ToList();
                }
                _ListaPedidos = pedidos;
            }
        }
        public void ObtenerPedidoId(int id, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetPedidoId"];
            PedidoId(ApiCatalgos, id, token).Wait();
        }
        private async Task PedidoId(string api, int id, string token = null)
        {
            using (var client = new HttpClient())
            {
                RegistrarPedidoModel pedido = new RegistrarPedidoModel();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        pedido = await response.Content.ReadAsAsync<RegistrarPedidoModel>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    pedido = new RegistrarPedidoModel();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _RegPedido = pedido;
            }
        }
        public void BuscarEstatusPedido(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetEstatusPedido"];
            GetEstatusPedido(tkn).Wait();
        }
        private async Task GetEstatusPedido(string Token)
        {
            using (var client = new HttpClient())
            {
                List<EstatusPedidoModel> lus = new List<EstatusPedidoModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<EstatusPedidoModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<EstatusPedidoModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaEstatusP = lus;
            }
        }
        public void GuardarEdicionPedido(RegistrarPedidoModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificarPedido"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void GuardarNuevoPedido(RegistrarPedidoModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistrarPedido"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void GuardarEncuesta(List<EncuestaModel> dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistrarEncuesta"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void CancelarNuevoPedido(RegistrarPedidoModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutCancelarPedido"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void BuscarCamionetas(short id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetCamionetas"];
            GetUnidadCamioneta(id, tkn).Wait();
        }
        private async Task GetUnidadCamioneta(short id, string Token)
        {
            using (var client = new HttpClient())
            {
                List<CamionetaModel> lus = new List<CamionetaModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + id).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<CamionetaModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<CamionetaModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                var item = AgregarUnidadC();
                item.AddRange(lus);
                _ListaCamionetas = item;
            }
        }
        public void BuscarPipas(short id, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetPipas"];
            GetUnidadPipas(id, tkn).Wait();
        }
        private async Task GetUnidadPipas(short id, string Token)
        {
            using (var client = new HttpClient())
            {
                List<PipaModel> lus = new List<PipaModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<PipaModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<PipaModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                var item = AgregarUnidadP();
                item.AddRange(lus);
                _ListaPipas = item;
            }
        }
        public List<CamionetaModel> AgregarUnidadC()
        {
            CamionetaModel c = new CamionetaModel();
            c.Nombre = "Seleccione";
            List<CamionetaModel> unidades = new List<CamionetaModel>();
            unidades.Add(c);
            return unidades;
        }
        public List<PipaModel> AgregarUnidadP()
        {
            PipaModel c = new PipaModel();
            c.Nombre = "Seleccione";
            List<PipaModel> unidades = new List<PipaModel>();
            unidades.Add(c);
            return unidades;
        }
        #endregion
        #region Cargos
        public void ListaCargosFilter(DateTime fecha1, DateTime fecha2, int Cliente, string rfc, string ticket, short id, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaCargos"];//GetListaCargos//GetListaCRecuperada
            Cargos(fecha1, fecha2, Cliente, rfc, ticket, id, ApiCatalgos, token).Wait();
        }
        private async Task Cargos(DateTime fecha1, DateTime fecha2, int Cliente, string rfc, string ticket, short id, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<CargosModel> cargos = new List<CargosModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api + id.ToString()).ConfigureAwait(false);
                    // HttpResponseMessage response = await client.PutAsJsonAsync(api, _mod).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        cargos = await response.Content.ReadAsAsync<List<CargosModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    cargos = new List<CargosModel>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                if (Cliente != 0)
                {
                    cargos = (from x in cargos where x.IdCliente == Cliente select x).ToList();
                }
                if (fecha1.Day != 1 && fecha1.Month != 1 && fecha1.Year != 1)
                {
                    cargos = (from x in cargos where x.FechaRegistro.Date >= fecha1.Date select x).ToList();
                }
                if (fecha2.Day != 1 && fecha2.Month != 1 && fecha2.Year != 1)
                {
                    cargos = (from x in cargos where x.FechaRegistro.Date <= fecha2 select x).ToList();
                }
                if (rfc != null)
                {
                    cargos = (from x in cargos where x.Rfc == rfc select x).ToList();
                }
                if (ticket != null)
                {
                    cargos = (from x in cargos where x.Ticket == ticket select x).ToList();
                }
                _ListaCargos = cargos;
            }
        }

        public void ListaCargosFilter(CargosModel _mod, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutCreditoRecuperado"];//GetListaCargos//GetListaCRecuperada
            Cargos(_mod, ApiCatalgos, token).Wait();
        }
        private async Task Cargos(CargosModel _mod, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<CargosModel> cargos = new List<CargosModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    // HttpResponseMessage response = await client.GetAsync(api + id.ToString()).ConfigureAwait(false);
                    HttpResponseMessage response = await client.PutAsJsonAsync(api, _mod).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        cargos = await response.Content.ReadAsAsync<List<CargosModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    cargos = new List<CargosModel>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                //if (Cliente != 0)
                //{
                //    cargos = (from x in cargos where x.IdCliente == Cliente select x).ToList();
                //}
                //if (fecha1.Day != 1 && fecha1.Month != 1 && fecha1.Year != 1)
                //{
                //    cargos = (from x in cargos where x.FechaRegistro.Date >= fecha1.Date select x).ToList();
                //}
                //if (fecha2.Day != 1 && fecha2.Month != 1 && fecha2.Year != 1)
                //{
                //    cargos = (from x in cargos where x.FechaRegistro.Date <= fecha2 select x).ToList();
                //}
                //if (rfc != null)
                //{
                //    cargos = (from x in cargos where x.Rfc == rfc select x).ToList();
                //}
                //if (ticket != null)
                //{
                //    cargos = (from x in cargos where x.Ticket == ticket select x).ToList();
                //}
                _ListaCargos = cargos;
            }
        }
        public void ListaCargos(short id, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaCargos"];
            Cargos(id, ApiCatalgos, token).Wait();
        }
        private async Task Cargos(short id, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<CargosModel> cargos = new List<CargosModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        cargos = await response.Content.ReadAsAsync<List<CargosModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    cargos = new List<CargosModel>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _ListaCargos = cargos;
            }
        }
        public void ListaCRecuperada(short id, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaCRecuperada"];
            CreditoRec(id, ApiCatalgos, token).Wait();
        }
        private async Task CreditoRec(short id, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<CargosModel> cargos = new List<CargosModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        cargos = await response.Content.ReadAsAsync<List<CargosModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    cargos = new List<CargosModel>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _ListaCargos = cargos;
            }
        }

        public void ListaCartera(CargosModel dto, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutListaCartera"];
            CarteraVencida(ApiCatalgos, dto, token).Wait();
        }
        private async Task CarteraVencida(string api, CargosModel dto, string token = null)
        {
            using (var client = new HttpClient())
            {
                ReporteModel cargos = new ReporteModel();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(api, dto).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        cargos = await response.Content.ReadAsAsync<ReporteModel>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    cargos = new ReporteModel();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _repCartera = cargos;
            }
        }
        public void ObtenerCargoId(int id, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetCargoId"];
            CargoId(ApiCatalgos, id, token).Wait();
        }
        private async Task CargoId(string api, int id, string token = null)
        {
            using (var client = new HttpClient())
            {
                CargosModel cargo = new CargosModel();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        cargo = await response.Content.ReadAsAsync<CargosModel>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    cargo = new CargosModel();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _Cargo = cargo;
            }
        }
        public void GuardarEdicionCargo(CargosModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificarPedido"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void GuardarNuevoAbono(CargosModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistrarAbono"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void GuardarNuevoAbono(List<AbonosModel> dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistrarAbonosLst"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        #endregion
        #region EquipoTransporte
        public void BuscarRecargasCombustible(string tkn, short idEmpresa)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetRecargasCombustible"];
            ListaRecargasCombistiblePorIdEmpresa(tkn, idEmpresa).Wait();
        }
        private async Task ListaRecargasCombistiblePorIdEmpresa(string token, short idEmpresa)
        {
            using (var client = new HttpClient())
            {
                List<RecargaCombustibleModel> emp = new List<RecargaCombustibleModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiCatalgos, idEmpresa.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<RecargaCombustibleModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<RecargaCombustibleModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaRecargasCombustible = emp;
            }
        }
        public void GuardarRecargaCombustible(RecargaCombustibleModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistrarRecargaCombustible"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void ModificarRecargaCombustible(RecargaCombustibleModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificarRecargaCombustible"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarRecargaCombustible(RecargaCombustibleModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaRecargaCombustible"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void BuscarCatalogoMantenimiento(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetMantenimiento"];
            ListaCatalogoMantenimiento(tkn).Wait();
        }
        private async Task ListaCatalogoMantenimiento(string token)
        {
            using (var client = new HttpClient())
            {
                List<MantenimientoModel> emp = new List<MantenimientoModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<MantenimientoModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<MantenimientoModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaMantenimientos = emp;
            }
        }

        public void BuscarMantenimiento(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetMantenimientoDetalle"];
            ListaMantenimiento(tkn).Wait();
        }
        private async Task ListaMantenimiento(string token)
        {
            using (var client = new HttpClient())
            {
                List<MantenimientoDetalleModel> emp = new List<MantenimientoDetalleModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<MantenimientoDetalleModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<MantenimientoDetalleModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaMantenimientoDetalle = emp;
            }
        }
        public void GuardarMantenimiento(MantenimientoDetalleModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistrarMantenimientoDetalle"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void ModificaMantenimiento(MantenimientoDetalleModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutModificarMantenimientoDetalle"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void EliminarMantenimiento(int id, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaMantenimientoDetalle"];
            EliminarMantenimientoDetalle(id, tkn).Wait();
        }
        private async Task EliminarMantenimientoDetalle(int _id, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos + _id.ToString(), "").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }

        public void BuscarAsignaciones(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetAsigancion"];
            ListaAsignaciones(tkn).Wait();
        }
        private async Task ListaAsignaciones(string token)
        {
            using (var client = new HttpClient())
            {
                List<AsignacionModel> emp = new List<AsignacionModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<AsignacionModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new List<AsignacionModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaAsignaciones = emp;
            }
        }
        public void GuardarAsignacion(AsignacionModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistrarAsignacion"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }

        public void EliminarAsignacion(AsignacionModel dto, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutEliminaAsignacion"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        #endregion
        #region Faturacion
        public void ListaTickets(FacturacionModel _mod)
        {
            if (_mod.IdCliente > 0)
            {
                this.ApiCatalgos = ConfigurationManager.AppSettings["GetTicketsByCliente"];
                BuscarTickets(_mod.IdCliente.ToString(), ApiCatalgos).Wait();
            }
            else
            {
                this.ApiCatalgos = ConfigurationManager.AppSettings["GetTicketsByRFC"];
                BuscarTickets(_mod.RFC, ApiCatalgos).Wait();
            }
        }
        private async Task BuscarTickets(string filtro, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<VentaPuntoVentaDTO> ventas = new List<VentaPuntoVentaDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(api, filtro)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        ventas = await response.Content.ReadAsAsync<List<VentaPuntoVentaDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    ventas = new List<VentaPuntoVentaDTO>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _ListaVenta = ventas;
            }
        }
        public void ListaTickets(FacturacionGlobalModel _mod, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostTicket"];
            BuscarTickets(_mod, ApiCatalgos, token).Wait();
        }
        private async Task BuscarTickets(FacturacionGlobalModel mod, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<VentaPuntoVentaDTO> ventas = new List<VentaPuntoVentaDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiRoute, mod).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        ventas = await response.Content.ReadAsAsync<List<VentaPuntoVentaDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    ventas = new List<VentaPuntoVentaDTO>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _ListaVenta = ventas;
            }
        }
        public void ListaTicket(string ticket)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetTicket"];
            BuscarTicket(ticket, ApiCatalgos).Wait();
        }
        private async Task BuscarTicket(string ticket, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                VentaPuntoVentaDTO venta = new VentaPuntoVentaDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(api, ticket)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        venta = await response.Content.ReadAsAsync<VentaPuntoVentaDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    venta = new VentaPuntoVentaDTO();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _VentaDTO = venta;
            }
        }
        public void ListaCFDIs(FacturacionModel _mod)
        {
            if (_mod.IdCliente > 0)
            {
                this.ApiCatalgos = ConfigurationManager.AppSettings["GetCFDIByCliente"];
                BuscarCFDIs(_mod.IdCliente.ToString(), ApiCatalgos, null).Wait();
            }
            else
            {
                this.ApiCatalgos = ConfigurationManager.AppSettings["GetCFDIByRFC"];
                BuscarCFDIs(_mod.RFC, ApiCatalgos).Wait();
            }
        }
        private async Task BuscarCFDIs(string filtro, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<CFDIDTO> CFDIs = new List<CFDIDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(api, filtro)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        CFDIs = await response.Content.ReadAsAsync<List<CFDIDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    CFDIs = new List<CFDIDTO>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _ListaCFDIs = CFDIs;
            }
        }
        public void ListaCFDIs(string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetCFDIs"];
            BuscarCFDIs(ApiCatalgos, token).Wait();
        }
        private async Task BuscarCFDIs(string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<CFDIDTO> CFDIs = new List<CFDIDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        CFDIs = await response.Content.ReadAsAsync<List<CFDIDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    CFDIs = new List<CFDIDTO>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _ListaCFDIs = CFDIs;
            }
        }
        private async Task BuscarCFDIs(FacturacionGlobalModel filtro, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<CFDIDTO> CFDIs = new List<CFDIDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(api, filtro)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        CFDIs = await response.Content.ReadAsAsync<List<CFDIDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    CFDIs = new List<CFDIDTO>();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _ListaCFDIs = CFDIs;
            }
        }
        public void BuscarCFDI(string ticket)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetCFDI"];
            BusquedaCFDI(ticket, ApiCatalgos).Wait();
        }
        private async Task BusquedaCFDI(string ticket, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                CFDIDTO dto = new CFDIDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(api, ticket)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        dto = await response.Content.ReadAsAsync<CFDIDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    dto = new CFDIDTO();
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _CFDIDTO = dto;
            }
        }
        public void PostRegistrarCFDILst(List<CFDIDTO> ticket)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistrarCFDILst"];
            LLamada(ticket, string.Empty, MetodoRestConst.Post, true).Wait();
        }
        public void PostRegistrarCFDIGlobal(FacturacionGlobalModel model, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostRegistrarCFDIGlobal"];
            LLamada(model, token, MetodoRestConst.Post, false).Wait();
        }
        public void FacturarPago(int id, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["GetTimbrarPago"];
            LLamada(id, token, MetodoRestConst.Get, false).Wait();
        }

        #endregion
        #region HistoricoVentas
        public void GetListaHistoricos(string token)
        {
            ApiRoute = ConfigurationManager.AppSettings["GetHistoricoVentas"];
            Historicos(ApiRoute, token).Wait();
        }
        private async Task Historicos(string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<HistoricoVentaModel> historico = new List<HistoricoVentaModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        historico = await response.Content.ReadAsAsync<List<HistoricoVentaModel>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    //ex.Message;
                    historico = new List<HistoricoVentaModel>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }

                _ListHistoricoVenta = historico;
            }
        }
        public void GetHistoricoById(int id, string token)
        {
            ApiRoute = ConfigurationManager.AppSettings["GetHistoricoById"];
        }
        public async Task HistoricoId(int id, string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                HistoricoVentaModel historico = new HistoricoVentaModel();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api + id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        historico = await response.Content.ReadAsAsync<HistoricoVentaModel>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    //ex.Message;
                    historico = new HistoricoVentaModel();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }

                _HistoricoVentaDTO = historico;
            }

        }
        public void GuardarNuevoHistorico(List<HistoricoVentaModel> dto, string tkn)
        {
            ApiRoute = ConfigurationManager.AppSettings["PostHistoricoVentas"];
            LLamada(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void EliminarHistorico(HistoricoVentaModel dto, string tkn)
        {
            ApiRoute = ConfigurationManager.AppSettings["DeleteHistorico"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void ObtenerElementosDistintos(string tkn)
        {
            ApiRoute = ConfigurationManager.AppSettings["GetYears"];
            Years(ApiRoute, tkn).Wait();
        }
        private async Task Years(string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<YearsDTO> Year = new List<YearsDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        Year = await response.Content.ReadAsAsync<List<YearsDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Year = new List<YearsDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }

                _ListYears = Year;
            }
        }
        public void GetVentasTotales(HistoricoVentasConsulta dto, string tkn)

        {
            ApiRoute = ConfigurationManager.AppSettings["GetYearsTotales"];
            GetVentas(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public async Task GetVentas<T>(T _dto, string token, string Tipo, bool EsAnonimo = false)
        {
            using (var client = new HttpClient())
            {
                List<YearsDTO> resp = new List<YearsDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!EsAnonimo)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                try
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    if (Tipo.Equals(MetodoRestConst.Post))
                        response = await client.PostAsJsonAsync(ApiRoute, _dto).ConfigureAwait(false);
                    if (Tipo.Equals(MetodoRestConst.Put))
                        response = await client.PutAsJsonAsync(ApiRoute, _dto).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<List<YearsDTO>>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<List<YearsDTO>>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _ListYears = resp;
            }
        }
        public void GetJson(HistoricoVentasConsulta dto, string tkn)
        {
            ApiRoute = ConfigurationManager.AppSettings["GetJsonConsulta"];
            GetJson(dto, tkn, MetodoRestConst.Post).Wait();
        }
        public void GetJsonCallCenter(string tkn)
        {
            ApiRoute = ConfigurationManager.AppSettings["GetDashCallCenter"];
            GetJson(tkn).Wait();
        }
        public async Task GetJson<T>(T _dto, string token, string Tipo, bool EsAnonimo = false)
        {
            using (var client = new HttpClient())
            {
                string resp = string.Empty;
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!EsAnonimo)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                try
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    if (Tipo.Equals(MetodoRestConst.Post))
                        response = await client.PostAsJsonAsync(ApiRoute, _dto).ConfigureAwait(false);
                    if (Tipo.Equals(MetodoRestConst.Put))
                        response = await client.PutAsJsonAsync(ApiRoute, _dto).ConfigureAwait(false);
                    if (Tipo.Equals(MetodoRestConst.Get))
                        response = await client.GetAsync(ApiRoute).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<string>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<string>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _Json = resp;
            }
        }
        public async Task GetJson(string token)
        {
            using (var client = new HttpClient())
            {
                string resp = string.Empty;
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync(ApiRoute).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<string>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<string>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _Json = resp;
            }
        }
        public void DashBoardRemanenteJson(string tkn)
        {
            ApiRoute = ConfigurationManager.AppSettings["GetDashRemanente"];
            GetDashRemanente(tkn).Wait();
        }
        public async Task GetDashRemanente(string token)
        {
            using (var client = new HttpClient())
            {
                AdministracionDTO resp = new AdministracionDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync(ApiRoute).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<AdministracionDTO>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<AdministracionDTO>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _AdministracionDTO = resp;
            }
        }
        public void DashAnden(string tkn)
        {
            ApiRoute = ConfigurationManager.AppSettings["GetDashAnden"];
            GetDashAnden(tkn).Wait();
        }
        public async Task GetDashAnden(string token)
        {
            using (var client = new HttpClient())
            {
                AndenDTO resp = new AndenDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync(ApiRoute).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<AndenDTO>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<AndenDTO>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _AndenDTO = resp;
            }
        }
        public void ActualizarHistorico(HistoricoVentaModel dto, string tkn)
        {
            ApiRoute = ConfigurationManager.AppSettings["PutHistorico"];
            LLamada(dto, tkn, MetodoRestConst.Put).Wait();
        }
        public void DashCajaGeneral(string tkn)
        {
            ApiRoute = ConfigurationManager.AppSettings["GetCajaGeneral"];
            GetDashCajaGeneral(tkn).Wait();
        }
        public async Task GetDashCajaGeneral(string token)
        {
            using (var client = new HttpClient())
            {
                AdministracionDTO resp = new AdministracionDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync(ApiRoute).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<AdministracionDTO>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<AdministracionDTO>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _AdministracionDTO = resp;
            }
        }
        public void DashCartera(string tkn)
        {
            ApiRoute = ConfigurationManager.AppSettings["GetCartera"];
            GetDashCartera(tkn).Wait();
        }
        public async Task GetDashCartera(string token)
        {
            using (var client = new HttpClient())
            {
                CarteraDTO resp = new CarteraDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync(ApiRoute).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<CarteraDTO>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<CarteraDTO>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _CarteraDTO = resp;
            }
        }
        #endregion
        #region Reportes
        #region CuentasPorPagar
        public void BuscaCuentasPorPagar(CuentasPorPagarModel model, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostRepoCuentasPorPagar"];
            ListaCuentasPorPagar(model, this.ApiCatalgos, tkn).Wait();
        }
        private async Task ListaCuentasPorPagar(CuentasPorPagarModel model, string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<CuentasPorPagarDTO> list = new List<CuentasPorPagarDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(api, model).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<CuentasPorPagarDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<CuentasPorPagarDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaCuentasPorPagar = list;
            }
        }
        #endregion
        #region Inventario por punto de venta
        public void BuscaInventarioPorPuntoVenta(InventarioPorPuntoVentaModel model, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostInventarioPorPuntoVenta"];
            PostCuentasPorPagar(model, this.ApiCatalgos, tkn).Wait();
        }

        private async Task PostCuentasPorPagar(InventarioPorPuntoVentaModel model, string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<InventarioPorPuntoVentaDTO> list = new List<InventarioPorPuntoVentaDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(api, model).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<InventarioPorPuntoVentaDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<InventarioPorPuntoVentaDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaInventarioPuntoVenta = list;
            }
        }
        #endregion
        #region Historio Precio Venta
        public void BuscarHistoricoPrecioVenta(HistoricoPrecioVentaModel model, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostHistorioPrecios"];
            PostHitoricoPrecioVenta(model, this.ApiCatalgos, tkn).Wait();
        }
        private async Task PostHitoricoPrecioVenta(HistoricoPrecioVentaModel model, string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<HistoricoPrecioVentaDTO> list = new List<HistoricoPrecioVentaDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(api, model).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<HistoricoPrecioVentaDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<HistoricoPrecioVentaDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaHistoricoPrecioVenta = list;
            }
        }
        #endregion
        #region Call Center
        public void BuscarCallCenter(CallCenterModel model, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostCallCenter"];
            PostCallCenter(model, this.ApiCatalgos, tkn).Wait();
        }
        private async Task PostCallCenter(CallCenterModel model, string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<CallCenterDTO> list = new List<CallCenterDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(api, model).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<CallCenterDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<CallCenterDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaCallCenter = list;
            }
        }
        #endregion
        #region Requisicion
        public void BuscarRequisicion(Models.RequisicionModel model, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostRepoRequisicion"];
            PostRequisicion(model, this.ApiCatalgos, tkn).Wait();
        }
        private async Task PostRequisicion(Models.RequisicionModel model, string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<RequisicionRepDTO> list = new List<RequisicionRepDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(api, model).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<RequisicionRepDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<RequisicionRepDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaRequisicion = list;
            }
        }

        #endregion
        #region OrdenCompra
        public void BuscarOrdenCompra(Models.OrdenCompraModel model, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostOrdenCompra"];
            PostOrdenCompra(model, this.ApiCatalgos, tkn).Wait();
        }
        private async Task PostOrdenCompra(Models.OrdenCompraModel model, string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<OrdenCompraRepDTO> list = new List<OrdenCompraRepDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(api, model).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<OrdenCompraRepDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<OrdenCompraRepDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaOrdenCompra = list;
            }
        }

        #endregion
        #region RendimientoVehicular
        public void BuscarRendimientoVehicular(RendimientoVehicularModel model, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostRendimientoVehicular"];
            PostRendimientoVehicular(model, this.ApiCatalgos, tkn).Wait();
        }
        private async Task PostRendimientoVehicular(RendimientoVehicularModel model, string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<RendimientoVehicularDTO> list = new List<RendimientoVehicularDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(api, model).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<RendimientoVehicularDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<RendimientoVehicularDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaRendimientoVehicular = list;
            }
        }
        #endregion
        #region Inventario Por Concepto
        public void BuscarInventarioPorConcepto(InventarioXConceptoModel model, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostInventarioPorConcepto"];
            PostInventarioConcepto(model, this.ApiCatalgos, tkn).Wait();
        }
        private async Task PostInventarioConcepto(InventarioXConceptoModel model, string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<InventarioXConceptoDTO> list = new List<InventarioXConceptoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(api, model).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<InventarioXConceptoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<InventarioXConceptoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaInventarioConcepto = list;
            }
        }
        #endregion
        #region Corte de Caja
        public void BuscarRepoCorteCaja(CorteCajaModel model, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostCorteCaja"];
            PostCorteCaja(model, this.ApiCatalgos, tkn).Wait();
        }
        private async Task PostCorteCaja(CorteCajaModel model, string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<CorteCajaDTO> list = new List<CorteCajaDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(api, model).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<CorteCajaDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<CorteCajaDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaCorteCaja = list;
            }
        }
        #endregion
        #region Gasto por vehiculo
        public void BuscarRepoGastoVehicular(GastoVehiculoModel model, string tkn)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostGastoXVehiculo"];
            PostGastoXVehiculo(model, tkn).Wait();
        }
        private async Task PostGastoXVehiculo(GastoVehiculoModel model, string token)
        {
            using (var client = new HttpClient())
            {
                List<GastoVehiculoDTO> list = new List<GastoVehiculoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiRoute, model).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        list = await response.Content.ReadAsAsync<List<GastoVehiculoDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    list = new List<GastoVehiculoDTO>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _ListaGastoVehicular = list;
            }
        }
        #endregion
        #endregion

        private async Task LLamada<T>(T _dto, string token, string Tipo, bool EsAnonimo = false)
        {
            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!EsAnonimo)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                try
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    if(Tipo.Equals(MetodoRestConst.Get))
                        response = await client.GetAsync(string.Concat(ApiRoute, _dto.ToString())).ConfigureAwait(false);
                    if (Tipo.Equals(MetodoRestConst.Post))
                        response = await client.PostAsJsonAsync(ApiRoute, _dto).ConfigureAwait(false);
                    if (Tipo.Equals(MetodoRestConst.Put))
                        response = await client.PutAsJsonAsync(ApiRoute, _dto).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDTO>();
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Mensaje = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _RespuestaDTO = resp;
            }
        }

    }
    public static class MetodoRestConst
    {
        public const string Post = "Post";
        public const string Put = "Put";
        public const string Get = "Get";
        public const string Delete = "Delete";
    }
}