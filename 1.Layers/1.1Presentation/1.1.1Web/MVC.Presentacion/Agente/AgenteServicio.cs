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
        public RequisicionDTO _requisicion;

        public List<RequisicionDTO> _listaRequisicion;
        public List<EmpresaDTO> _listaEmpresas;
        public List<EmpresaConfiguracion> _EmpresasConfiguracion;
        public List<PaisModel> _listaPaises; 
        public List<EstadosRepModel> _listaEstados; 
        // public RespuestaAutenticacionDto _respuestaAutenticacion;
        public CatalogoRespuestaDTO _respuestaCatalogos;

        public List<RequisicionEstatusDTO> _listaRequisicionEstatus;
        public List<UsuarioDTO> _listaUsuarios;
        public List<CentroCostoDTO> _listaCentroCosto;
        public List<ProductoDTO> _listProductos;
        public List<OrdenCompraDTO> _listaOrdenCompra;
        public List<OrdenCompraEstatusDTO> _listaOrdenCompraEstatus;

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
      
        #region Estados
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
                _listaEstados = emp;
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

        public void GuardarEmpresaConfiguracion(EmpresaConfiguracion dto, string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["PutEmpresaConfig"];
            GuardarEmpresaConfig(dto, tkn).Wait();
        }
        private async Task GuardarEmpresaConfig(EmpresaConfiguracion _pcDTO, string token)
        {

            using (var client = new HttpClient())
            {
                RespuestaDTO resp = new RespuestaDTO();

                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                try
                {//PostAsJsonAsync
                    HttpResponseMessage response = await client.PutAsJsonAsync(ApiCatalgos, _pcDTO).ConfigureAwait(false);
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

        public void BuscarTodosUsuarios(string tkn)
        {
            this.ApiCatalgos = ConfigurationManager.AppSettings["GetListaUsuarios"];
            GetListaTodosUsuarios(tkn).Wait();
        }

        private async Task GetListaTodosUsuarios(string Token)
        {
            using (var client = new HttpClient())
            {
                List<UsuarioDTO> lus = new List<UsuarioDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiCatalgos).ConfigureAwait(false);
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

        #region Orden de Compra
        public void BuscarRequisicioOC(int idReq, string tkn)
        {
            this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetBuscarReq"];
            RequisicionPorIdReqOC(idReq, tkn).Wait();
        }
        private async Task RequisicionPorIdReqOC(int numReq, string token)
        {
            using (var client = new HttpClient())
            {
                RequisicionDTO emp = new RequisicionDTO();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiOrdenCompra + numReq).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<RequisicionDTO>();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception)
                {
                    emp = new RequisicionDTO() { NumeroRequisicion = "0" };
                    client.CancelPendingRequests();
                    client.Dispose(); ;
                }
                _requisicion = emp;
            }
        }
        //public void GuardarOrdenesCompra(OrdenCompraCrearDTO ocDTO, string token)
        //{
        //    this.ApiOrdenCompra = ConfigurationManager.AppSettings["PostGenerarOrdenesCompra"];
        //    SaveOrdenCompra(ocDTO, token).Wait();
        //}
        //private async Task SaveOrdenCompra(OrdenCompraCrearDTO _oc, string token)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        List<OrdenCompraRespuestaDTO> resp = new List<OrdenCompraRespuestaDTO>();

        //        client.BaseAddress = new Uri(UrlBase);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //        try
        //        {
        //            HttpResponseMessage response = await client.PostAsJsonAsync(ApiOrdenCompra, _oc).ConfigureAwait(false);
        //            if (response.IsSuccessStatusCode)
        //                resp = await response.Content.ReadAsAsync<List<OrdenCompraRespuestaDTO>>();
        //            else
        //            {
        //                client.CancelPendingRequests();
        //                client.Dispose();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            resp.Add(new OrdenCompraRespuestaDTO { Mensaje = ex.Message, Exito = false });
        //            client.CancelPendingRequests();
        //            client.Dispose();
        //        }
        //        _listaOrdenesCompraRespuesta = resp;
        //    }
        //}
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
        //public void AutorizarOrdenCompra(OrdenCompraAutorizacionDTO _oc, string token)
        //{
        //    this.ApiOrdenCompra = ConfigurationManager.AppSettings["PutAutorizarCompra"];
        //    AutorizarOC(_oc, token).Wait();
        //}
        //private async Task AutorizarOC(OrdenCompraAutorizacionDTO _oc, string token)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        RespuestaDto resp = new RespuestaDto();

        //        client.BaseAddress = new Uri(UrlBase);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
        //        try
        //        {
        //            HttpResponseMessage response = await client.PutAsJsonAsync(ApiOrdenCompra, _oc).ConfigureAwait(false);
        //            if (response.IsSuccessStatusCode)
        //                resp = await response.Content.ReadAsAsync<RespuestaDto>();
        //            else
        //            {
        //                _respuestaDTO = resp;
        //                client.CancelPendingRequests();
        //                client.Dispose();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            resp.Mensaje = ex.Message;
        //            client.CancelPendingRequests();
        //            client.Dispose();
        //        }
        //        _respuestaDTO = resp;
        //    }
        //}
        //public void BuscarOrdenCompra(int idOC, string tkn)
        //{
        //    this.ApiOrdenCompra = ConfigurationManager.AppSettings["GetBuscarOrdenCompra"];
        //    OrdenCompra(idOC, tkn).Wait();
        //}
        //private async Task OrdenCompra(int idOrdenCompra, string token)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        OrdenCompraCrearDTO emp = new OrdenCompraCrearDTO();
        //        client.BaseAddress = new Uri(UrlBase);
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
        //        try
        //        {
        //            HttpResponseMessage response = await client.GetAsync(string.Concat(ApiOrdenCompra, idOrdenCompra.ToString())).ConfigureAwait(false);
        //            if (response.IsSuccessStatusCode)
        //                emp = await response.Content.ReadAsAsync<OrdenCompraCrearDTO>();
        //            else
        //            {
        //                client.CancelPendingRequests();
        //                client.Dispose();
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            emp = new OrdenCompraCrearDTO();
        //            client.CancelPendingRequests();
        //            client.Dispose(); ;
        //        }
        //        _ordenCompraCrearDTO = emp;
        //    }
        //}
        //public void BuscarOrdenCompraEstatus(string tkn)
        //{
        //    this.ApiCompras = ConfigurationManager.AppSettings["GetOrdenCompraEstatus"];
        //    ListaOrdenCompraEstatus(tkn).Wait();
        //}
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
        #endregion
    }
}