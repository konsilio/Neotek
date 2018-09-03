using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Web.MainModule.Seguridad.Model;
using Web.MainModule.Requisicion.Model;
using Web.MainModule.OrdenCompra.Model;
using System.Configuration;
using Web.MainModule.Catalogos.Model;
using Web.MainModule.Inventario.Model;

namespace Web.MainModule.Agente
{
    public class AgenteServicios
    {
        private static string UrlBase;
        private string ApiLogin;
        private string ApiListaEmpresasLogin;
        private string ApiCompras;
        private string ApiListaEmpresas;
        private string ApiRequisicion;
        private string ApiUsuarios;
        private string ApiProducto;
        private string ApiCatalgos;
        private string ApiOrdenCompra;
        private string ApiAlmacen;

        public RespuestaAutenticacionDto _respuestaAutenticacion;
        public RespuestaRequisicionDto _respuestaRequisicion;
        public List<EmpresaDTO> _listaEmpresasLogin;
        public List<EmpresaDTO> _listaEmpresas;
        public ComprasDTO _respuestacompra;
        public List<RequisicionDTO> _listaRequisiciones;
        public RequisicionEDTO _requisicionEDTO;
        public List<UsuarioDTO> _listUsuarios;
        public List<ProductoDTO> _listProductos;
        public RequisicionRevisionDTO _requisicionRevisionDTO;
        public RequisicionAutorizacion _requsicionAutorizacion;
        public RequisicionOCDTO _requisicionOrdenCompra;
        public List<ProveedorDTO> _listaProveedores;
        public List<CentroCostoDTO> _listaCentrosCostos;
        public List<CuentaContableDTO> _listaCuentasContable;
        public List<OrdenCompraRespuestaDTO> _listaOrdenesCompraRespuesta;
        public List<OrdenCompraDTO> _listaOrdenCompraDTO;
        public OrdenCompraDTO _ordeCompraDTO;
        public OrdenCompraCrearDTO _ordenCompraCrearDTO;
        public CatalogoRespuestaDTO _respuestaCatalogos;
        public RespuestaDto _respuestaDTO;

        public AgenteServicios()
        {
            UrlBase = ConfigurationManager.AppSettings["WebApiUrlBase"];
        }
        #region Catalogos
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
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                _listaCentrosCostos = emp;
            }
        }

    
        public void BuscarCuentasContables(short idEmpresa, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaCuentasContables"];
            ListaCuentaContable(idEmpresa, tkn).Wait();
        }
        private async Task ListaCuentaContable(short idEmpresa, string token)
        {
            using (var client = new HttpClient())
            {
                List<CuentaContableDTO> emp = new List<CuentaContableDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiCatalgos, idEmpresa)).ConfigureAwait(false);
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
                _listaCuentasContable = emp;
            }
        }
        public void GuardarCuentaContable(CuentaContableCrearDto _cc, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostRegistraCuentaContable"];
            SaveCtaCtble(_cc, token).Wait();
        }
        private async Task SaveCtaCtble(CuentaContableCrearDto _cc, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDto resp = new RespuestaDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiCatalgos, _cc).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
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
                _respuestaDTO = resp;
            }
        }
        public void ModificarCtaCtble(CuentaContableModificarDto _cc, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PutModificaCuentaContable"];
            ModificarCuentaContable(_cc, token).Wait();
        }
        private async Task ModificarCuentaContable(CuentaContableModificarDto _cc, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDto resp = new RespuestaDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _cc).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
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
                _respuestaDTO = resp;
            }
        }
        public void EliminarCtaCtble(CuentaContableEliminarDto _cc, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PutEliminaCuentaContable"];
            EliminarCuentaContable(_cc, token).Wait();
        }
        private async Task EliminarCuentaContable(CuentaContableEliminarDto _cc, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDto resp = new RespuestaDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _cc).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
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
                _respuestaDTO = resp;
            }
        }

        public void GuardarProveedoresNuevo(ProveedorCrearDto dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostRegistraProveedor"];
            GuardarProveedor(dto, tkn).Wait();
        }
        private async Task GuardarProveedor(ProveedorCrearDto _pcDTO, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDto resp = new RespuestaDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
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
                _respuestaDTO = resp;
            }
        }
        public void EliminarProv(ProveedorEliminarDto _prov, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PutEliminaProveedor"];
            EliminarCuentaContable(_prov, token).Wait();
        }
        private async Task EliminarCuentaContable(ProveedorEliminarDto _prov, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDto resp = new RespuestaDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _prov).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
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
                _respuestaDTO = resp;
            }
        }
        public void ModificarProv(ProveedorModificarDto _prov, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PutModificaProveedor"];
            ModificarProveedor(_prov, token).Wait();
        }
        private async Task ModificarProveedor(ProveedorModificarDto _prov, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDto resp = new RespuestaDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _prov).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
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
                _respuestaDTO = resp;
            }
        }

        public void GuardarCentroCosto(CentroCostoCrearDto dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostRegistraProveedor"];
            GuardarCtroCosto(dto, tkn).Wait();
        }
        private async Task GuardarCtroCosto(CentroCostoCrearDto _pcDTO, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDto resp = new RespuestaDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
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
                _respuestaDTO = resp;
            }
        }
        public void EliminarCtroCosto(CentroCostoEliminarDto dto, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutEliminaCentroCosto"];
            EliminarCentroCosto(dto, token).Wait();
        }
        private async Task EliminarCentroCosto(CentroCostoEliminarDto _dto, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDto resp = new RespuestaDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, _dto).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
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
                _respuestaDTO = resp;
            }
        }
        public void ModificarCtroCosto(CentroCostoModificarDto dto, string token)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutModificaCentroCosto"];
            ModificarCentroCosto(dto, token).Wait();
        }
        private async Task ModificarCentroCosto(CentroCostoModificarDto _dto, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDto resp = new RespuestaDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, _dto).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
                    else
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
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
                _respuestaDTO = resp;
            }
        }


        #endregion
        #region Login
        public void ListaEmpresasLogin()
        {
            this.ApiListaEmpresasLogin = ConfigurationManager.AppSettings["GetListaEmpresasLogin"];
            ListaEmp(this.ApiListaEmpresasLogin).Wait();
        }
        public void ListaEmpresas(string _tok)
        {
            this.ApiListaEmpresas = ConfigurationManager.AppSettings["GetListaEmpresas"];
            ListaEmp(this.ApiListaEmpresas, _tok).Wait();
        }        
        private async Task ListaEmp(string api, string token = null)
        {
            using (var client = new HttpClient())
            {
                List<EmpresaDTO> emp = new List<EmpresaDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
        public void Acceder(AutenticacionDto autDto)
        {
            this.ApiLogin = ConfigurationManager.AppSettings["PostLogin"];
            Login(autDto).Wait();
        }
        private async Task Login(AutenticacionDto autDto)
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
        #region Usuarios
        public void BuscarListaUsuarios(short idEmpresa, string tkn)
        {
            this.ApiUsuarios = ConfigurationManager.AppSettings["GetListaUsuarios"];
            GetListaUsuarios(idEmpresa, tkn).Wait();
        }
        private async Task GetListaUsuarios(short IdEmpresa, string Token)
        {
            using (var client = new HttpClient())
            {
                List<UsuarioDTO> lus = new List<UsuarioDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiUsuarios + IdEmpresa.ToString()).ConfigureAwait(false);
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
                _listUsuarios = lus;
            }
        }
        #endregion
        #region Compras
        public void Compras(string token)
        {
            this.ApiCompras = ConfigurationManager.AppSettings["PostCompraGas"];
            Compra(token).Wait();
        }
        private async Task Compra(string token)
        {
            using (var client = new HttpClient())
            {
                ComprasDTO respuesta = new ComprasDTO();
                //HttpResponseMessage resp = new HttpResponseMessage();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiCompras, string.Empty).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                        respuesta = await response.Content.ReadAsAsync<ComprasDTO>();
                    else
                    {
                        //respuesta.Mensaje = "La respuesta a la petición no fue exitosa");
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
                _respuestacompra = respuesta;
            }
        }
        #endregion
        #region Requisicion
        public void GuardarRequisicon(RequisicionEDTO _requi, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PostRequisicion"];
            SaveRequisicon(_requi, token).Wait();
        }
        private async Task SaveRequisicon(RequisicionEDTO _requi, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaRequisicionDto resp = new RespuestaRequisicionDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiRequisicion, _requi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)                    
                        resp = await response.Content.ReadAsAsync<RespuestaRequisicionDto>();                    
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
                _respuestaRequisicion = resp;
            }
        }
        public void ActualizarRequisicionRevision(RequisicionRevPutDTO _requi, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PutActulizarRevision"];
            UpdateRequisicon(_requi, token).Wait();
        }
        public void ActualizarRequisicionAutorizacion(RequisicionAutPutDTO _requi, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PutActulizarAutorizacion"];
            UpdateRequisicon(_requi, token).Wait();
        }
        public void ActualizarRequisicionCancelar(RequisicionCancelaDTO _requi, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PutCancelarRequisicion"];
            UpdateRequisicon(_requi, token).Wait();
        }
        private async Task UpdateRequisicon(RequisicionCancelaDTO _requi, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaRequisicionDto resp = new RespuestaRequisicionDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _requi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaRequisicionDto>();
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
                _respuestaRequisicion = resp;
            }
        }
        private async Task UpdateRequisicon(RequisicionAutPutDTO _requi, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaRequisicionDto resp = new RespuestaRequisicionDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _requi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaRequisicionDto>();
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
                _respuestaRequisicion = resp;
            }
        }
        private async Task UpdateRequisicon(RequisicionRevPutDTO _requi, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaRequisicionDto resp = new RespuestaRequisicionDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _requi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaRequisicionDto>();
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
                _respuestaRequisicion = resp;
            }
        }
        public void BuscarRequisiciones(short idEmpresa, string tkn)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["GetRequisicionesByIdEmpresa"];
            ListaRequisicionesPorIdEmpresa(idEmpresa, tkn).Wait();
        }
        private async Task ListaRequisicionesPorIdEmpresa(short idEmpresa, string token)
        {
            using (var client = new HttpClient())
            {
                List<RequisicionDTO> emp = new List<RequisicionDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                _listaRequisiciones = emp;
            }
        }
        public void BuscarRequisicio(int NumRequisicion, string tkn)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["GetRequisicionByNumRequisicion"];
            RequisicionesPorNumRequisicion(NumRequisicion, tkn).Wait();
        }       
        private async Task RequisicionesPorNumRequisicion(int numReq, string token)
        {
            using (var client = new HttpClient())
            {
                RequisicionRevisionDTO emp = new RequisicionRevisionDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiRequisicion + numReq).ConfigureAwait(false);
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
        public void BuscarRequisicioAuto(int NumRequisicion, string tkn)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["GetRequisicionByNumRequisicionAut"];
            RequisicionesPorNumRequisicionAuto(NumRequisicion, tkn).Wait();
        }       
        private async Task RequisicionesPorNumRequisicionAuto(int numReq, string token)
        {
            using (var client = new HttpClient())
            {
                RequisicionAutorizacion emp = new RequisicionAutorizacion();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiRequisicion + numReq.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<RequisicionAutorizacion>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new RequisicionAutorizacion() { NumeroRequisicion = "0" };
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _requsicionAutorizacion = emp;
            }
        }
        public void EnviarNotificacion(int NumRequisicion, string tkn)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PostEnviarNotificacion"];
            NotificarRequisicion(NumRequisicion, tkn).Wait();
        }
        private async Task NotificarRequisicion(int numReq, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaRequisicionDto emp = new RespuestaRequisicionDto();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiRequisicion + numReq.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<RespuestaRequisicionDto>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new RespuestaRequisicionDto() { Exito = false};
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _respuestaRequisicion = emp;
            }
        }
        public void BuscarProductos(short idEmpresa, string tkn)
        {
            this.ApiProducto = ConfigurationManager.AppSettings["GetListaProductos"];
            ListaProductosPorIdEmpresa(idEmpresa, tkn).Wait();
        }
        private async Task ListaProductosPorIdEmpresa(short idEmpresa, string token)
        {
            using (var client = new HttpClient())
            {
                List<ProductoDTO> emp = new List<ProductoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiProducto + idEmpresa.ToString()).ConfigureAwait(false);
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
                _listProductos = emp;
            }
        }
        public void BuscarProductosAsociados(int idProducto, string tkn)
        {
            this.ApiProducto = ConfigurationManager.AppSettings["GetListaProductosAsociados"];
            ListaProductosAsociados(idProducto, tkn).Wait();
        }
        private async Task ListaProductosAsociados(int idProducto, string token)
        {
            using (var client = new HttpClient())
            {
                List<ProductoDTO> emp = new List<ProductoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiProducto + idProducto.ToString()).ConfigureAwait(false);
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
                _listProductos = emp;
            }
        }
        #endregion
        #region Orden de Compra
        public void BuscarRequisicioOC(int idReq, string tkn)
        {
            this.ApiCompras = ConfigurationManager.AppSettings["GetBuscarReq"];
            RequisicionPorIdReqOC(idReq, tkn).Wait();
        }
        private async Task RequisicionPorIdReqOC(int numReq, string token)
        {
            using (var client = new HttpClient())
            {
                RequisicionOCDTO emp = new RequisicionOCDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCompras + numReq).ConfigureAwait(false);
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
                List<OrdenCompraRespuestaDTO> resp = new List<OrdenCompraRespuestaDTO>();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiOrdenCompra, _oc).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<List<OrdenCompraRespuestaDTO>>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    resp.Add(new OrdenCompraRespuestaDTO { Mensaje = ex.Message, Exito = false });
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _listaOrdenesCompraRespuesta = resp;
            }
        }
        public void BuscarOrdenesCompra(short idEmpresa, string tkn)
        {
            this.ApiCompras = ConfigurationManager.AppSettings["GetOrdenesCompra"];
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
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiCompras,idEmpresa.ToString())).ConfigureAwait(false);
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
                _listaOrdenCompraDTO = emp;
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
                RespuestaDto resp = new RespuestaDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiOrdenCompra, _oc).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
                    else
                    {
                        _respuestaDTO = resp;
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
                _respuestaDTO = resp;
            }
        }
        public void AutorizarOrdenCompra(OrdenCompraAutorizacionDTO _oc, string token)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["PutAutorizarCompra"];
            AutorizarOC(_oc, token).Wait();
        }
        private async Task AutorizarOC(OrdenCompraAutorizacionDTO _oc, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDto resp = new RespuestaDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiOrdenCompra, _oc).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
                    else
                    {
                        _respuestaDTO = resp;
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
                _respuestaDTO = resp;
            }
        }
        public void BuscarOrdenCompra(int idOC, string tkn)
        {
            this.ApiCompras = ConfigurationManager.AppSettings["GetBuscarOrdenCompra"];
            OrdenCompra(idOC, tkn).Wait();
        }
        private async Task OrdenCompra(int idOrdenCompra, string token)
        {
            using (var client = new HttpClient())
            {
                OrdenCompraCrearDTO emp = new OrdenCompraCrearDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Concat(ApiCompras, idOrdenCompra.ToString())).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<OrdenCompraCrearDTO>();
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
                _ordenCompraCrearDTO = emp;
            }
        }
        #endregion
        #region 
        public void GenerarEntradas(List<AlmacenEntradaCrearDTO> _entradas , string tkn)
        {
            this.ApiAlmacen = ConfigurationManager.AppSettings["PostGuardarEntradas"];
            GuardarEntradas(_entradas, tkn).Wait();
        }
        private async Task GuardarEntradas(List<AlmacenEntradaCrearDTO> _entradas, string token)
        {
            using (var client = new HttpClient())
            {
                RespuestaDto resp = new RespuestaDto();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiOrdenCompra, _entradas).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaDto>();
                    else
                    {
                        _respuestaDTO = resp;
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    if (resp.MensajesError != null)
                        resp.MensajesError = new List<string>();
                    resp.MensajesError.Add(ex.Message);
                    resp.Exito = false;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
                _respuestaDTO = resp;
            }
        }
        #endregion
    }
}
