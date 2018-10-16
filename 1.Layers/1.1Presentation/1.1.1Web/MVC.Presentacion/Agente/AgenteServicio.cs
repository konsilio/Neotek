using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.OrdenCompra;
using MVC.Presentacion.Models.Requisicion;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

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
        public List<RolCat> _lstaRolesCat;
        public List<RolMovilCompra> _lstaRolesMovilCom;
        public List<RolCompras> _lstaRolesCom;
        public List<RolRequsicion> _lstaRolesReq;
        public List<TipoPersonaModel> _lstaTipoPersona;
        public List<RegimenFiscalModel> _lstaRegimenFiscal;
        public List<ClientesDto> _lstaClientes;
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
        public void BuscarRolesCat(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaRoles"];
            GetListaRolesCat(tkn).Wait();
        }

        private async Task GetListaRolesCat(string Token)
        {
            using (var client = new HttpClient())
            {
                List<RolCat> lus = new List<RolCat>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        lus = await response.Content.ReadAsAsync<List<RolCat>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    lus = new List<RolCat>();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _lstaRolesCat = (from x in lus where x.NombreRol != "Super Usuario" select x).ToList();
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
        public void GuardarEmpresaEdicion(EmpresaDTO dto, string tkn)
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
                }
                if (idUser != 0)
                {
                    _lstUserEmp = (from x in lus where x.IdUsuario == idUser select x).ToList();
                }


                if (!String.IsNullOrEmpty(mail))
                {
                    _lstUserEmp = (from x in lus where x.Email1 == mail select x).ToList();
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

        public void GuardarRolesAsig(UsuariosModel dto, string tkn)
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

        public void BuscarTiposPersona(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetTiposPersona"];
            GetTiposPersona(tkn).Wait();
        }

        private async Task GetTiposPersona(string Token)
        {
            using (var client = new HttpClient())
            {
                List<TipoPersonaModel> lus = new List<TipoPersonaModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
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

        public void BuscarRegimenFiscal(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetRegimenFiscal"];
            GetRegimen(tkn).Wait();
        }

        private async Task GetRegimen(string Token)
        {
            using (var client = new HttpClient())
            {
                List<RegimenFiscalModel> lus = new List<RegimenFiscalModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
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
        public void BuscarListaClientes(int id, string rfc, string nombre, string tkn)//short idEmpresa, 
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetClientes"];
            GetListaClientes(id, rfc, nombre, tkn).Wait();
        }
        private async Task GetListaClientes(int id, string rfc, string nombre, string Token)
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

                if (id != 0 || rfc != "" || (nombre != "" && nombre != null))
                {
                    if (id != 0)
                    {
                        _lstaClientes = (from x in lus where x.IdCliente == id select x).ToList();
                    }

                    if (rfc != "")
                    {
                        _lstaClientes = (from x in lus where x.Rfc == rfc select x).ToList();
                    }

                    if (nombre != "" && nombre != null)
                    {
                        _lstaClientes = (from x in lus where x.RazonSocial == nombre || (x.Nombre + " " + x.Apellido1) == nombre select x).ToList();
                    }
                }

                else
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
        
        public void BuscarListaPuntosVentaId(short idEmpresa, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetPuntosVentaId"];
            GetListaPVId(tkn, idEmpresa).Wait();
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
        public void BuscarListaVentaCajaGral(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaCajaGral"];
            GetListaCajaGral(tkn).Wait();
        }
        private async Task GetListaCajaGral(string Token)
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

                _listaCajaGral = lus.OrderByDescending(x=> x.FechaAplicacion).ToList();
                
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

        public void BuscarListaCajaGralCamioneta(string cveReporte,string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaCajaGralCamioneta"];
            GetListaCajaGralCamioneta(cveReporte, tkn).Wait();
        }
        private async Task GetListaCajaGralCamioneta(string cveRep,string Token)
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
        

        #endregion
        #region Paises
        public void BuscarPaises(string tkn)
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
        private async Task ListaPaises(string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<PaisModel> emp = new List<PaisModel>();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
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
        public void BuscarEstados(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaEstadosR"];
            ListaEstados(this.ApiCatalgos, tkn).Wait();
        }

        private async Task ListaEstados(string api, string token)
        {
            using (var client = new HttpClient())
            {
                List<EstadosRepModel> emp = new List<EstadosRepModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
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
        public void ListaFormaPago(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaFormasPago"];
            GetListaFormaPago(tkn).Wait();
        }
        private async Task GetListaFormaPago(string Token)
        {
            using (var client = new HttpClient())
            {
                List<FormaPagoDTO> list = new List<FormaPagoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
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
        public void GuardarCentroCosto(CentroCostoCrearDTO dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostRegistraCentroCosto"];
            GuardarCtroCosto(dto, tkn).Wait();
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
                catch (Exception ex)
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
        public void GuardarRequisicon(RequisicionDTO dto, string token)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
        public void EnviarConfirmarPago (OrdenCompraPagoDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PutConfirmarPago"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void EnviarSolicitudPago(OrdenCompraPagoDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostGenerarPago"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void EnviarSolicitudPagoExpedidor(OrdenCompraComplementoGasDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostGenerarPago"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void EnviarSolicitudPagoPorteador(OrdenCompraComplementoGasDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostGenerarPago"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void GuardarDatosPorteador(OrdenCompraComplementoGasDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostGenerarPago"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void GuardarDatosExpedidor(OrdenCompraComplementoGasDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostGenerarPago"];
            LLamada(dto, token, MetodoRestConst.Put).Wait();
        }
        public void ConfirmarDatosPapeleta(OrdenCompraComplementoGasDTO dto, string token)
        {
            this.ApiRoute = ConfigurationManager.AppSettings["PostGenerarPago"];
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
                catch (Exception ex)
                {
                    emp = new OrdenCompraComplementoGasDTO();
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _complementoGas = emp;
            }
        }
        #endregion

        private async Task LLamada<T>(T _dto, string token, string Tipo)
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
                    HttpResponseMessage response = new HttpResponseMessage();
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