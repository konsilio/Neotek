using MVC.Presentacion.Models.Catalogos;
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
        string _tok = string.Empty;

        public RespuestaDTO _respuestaDTO;
        public List<EmpresaDTO> _listaEmpresas;
        public List<PaisModel> _listaPaises;
        public RespuestaAutenticacionDto _respuestaAutenticacion;
        public CatalogoRespuestaDTO _respuestaCatalogos;

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
    }
}