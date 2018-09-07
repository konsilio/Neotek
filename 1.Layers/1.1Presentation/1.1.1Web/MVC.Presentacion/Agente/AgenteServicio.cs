using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Requisicion;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        public RespuestaDTO _respuestaDTO;
        public RespuestaAutenticacionDto _respuestaAutenticacion;
        public List<RequisicionDTO> _listaRequisicion;
        public List<EmpresaDTO> _listaEmpresas;
        public List<RequisicionEstatusDTO> _listaRequisicionEstatus;
        public List<UsuarioDTO> _listaUsuarios;
        public List<CentroCostoDTO> _listaCentroCosto;
        public List<ProductoDTO> _listProductos;


        public AgenteServicio()
        {
            UrlBase = ConfigurationManager.AppSettings["WebApiUrlBase"];
        }

        #region Catalogos
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
        public void BuscarProductos(short idEmpresa, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaProductos"];
            ListaProductosPorIdEmpresa(idEmpresa, tkn).Wait();
        }
        private async Task ListaProductosPorIdEmpresa(short idEmpresa, string token)
        {
            using (var client = new HttpClient())
            {
                List<ProductoDTO> emp = new List<ProductoDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos + idEmpresa.ToString()).ConfigureAwait(false);
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
        #endregion
    }
}