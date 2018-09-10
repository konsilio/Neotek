using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.OrdenCompra;
using MVC.Presentacion.Models.Requisicion;
using MVC.Presentacion.Models.Seguridad;
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

        public RespuestaDTO _respuestaDTO;
        public RespuestaAutenticacionDto _respuestaAutenticacion;
        public RespuestaRequisicionDTO _respuestaRequisicion;
        public OrdenCompraDTO _ordeCompraDTO;
        public RequisicionRevisionDTO _requisicionRevisionDTO;
        public RequisicionAutorizacionDTO _requsicionAutorizacion;
        public CatalogoRespuestaDTO _respuestaCatalogos;

        public List<RequisicionDTO> _listaRequisicion;
        public List<EmpresaDTO> _listaEmpresas;
        public List<PaisModel> _listaPaises;
        public List<RequisicionEstatusDTO> _listaRequisicionEstatus;
        public List<UsuarioDTO> _listaUsuarios;
        public List<CentroCostoDTO> _listaCentroCosto;
        public List<ProductoDTO> _listProductos;
        public List<OrdenCompraDTO> _listaOrdenCompraDTO;    

        public AgenteServicio()
        {
            UrlBase = ConfigurationManager.AppSettings["WebApiUrlBase"];
        }
        #region Paises
        public void BuscarPaises(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaPaises"];
            ListaPaises(this.ApiCatalgos,tkn).Wait();
        }

        private async Task ListaPaises(string api,string token)
        {
            using (var client = new HttpClient())
            {
                List<PaisModel> emp = new List<PaisModel>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {
                    //HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
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
                _listaPaises = emp;
            }
        }

        #endregion      

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
        
        public void GuardarEmpresaNueva(EmpresaModel dto, string tkn)
        {               
            this.ApiCatalgos = ConfigurationManager.AppSettings["PostRegistraEmpresas"];
            GuardarEmpresa(dto, tkn).Wait();
        }
        private async Task GuardarEmpresa(EmpresaModel _pcDTO, string token)
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
                _respuestaDTO = resp;
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
        public void GuardarRequisicon(RequisicionEDTO _requi, string token)
        {
            this.ApiRequisicion = ConfigurationManager.AppSettings["PostRequisicion"];
            SaveRequisicon(_requi, token).Wait();
        }
        private async Task SaveRequisicon(RequisicionEDTO _requi, string token)
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
                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiRequisicion, _requi).ConfigureAwait(false);
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
                _respuestaDTO = resp;
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
                RespuestaRequisicionDTO resp = new RespuestaRequisicionDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _requi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaRequisicionDTO>();
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
                RespuestaRequisicionDTO resp = new RespuestaRequisicionDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _requi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaRequisicionDTO>();
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
                RespuestaRequisicionDTO resp = new RespuestaRequisicionDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiRequisicion, _requi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<RespuestaRequisicionDTO>();
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
    }
}