using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Web.MainModule.Seguridad.Model;
using Web.MainModule.Requisicion.Model;

using System.Configuration;

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

        public RespuestaAutenticacionDto _respuestaAutenticacion;
        public RespuestaRequisicionDto _respuestaRequisicion;
        public List<EmpresaDTO> _listaEmpresasLogin;
        public List<EmpresaDTO> _listaEmpresas;
        public ComprasDTO _respuestacompra;

        public AgenteServicios()
        {
            UrlBase = ConfigurationManager.AppSettings["WebApiUrlBase"];
        }
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
                if(!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(api).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        emp = await response.Content.ReadAsAsync<List<EmpresaDTO>>();
                    else
                    {
                        //respuesta.Mensaje = "La respuesta a la petición no fue exitosa");
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
        #region MetodoComentado
        //private async Task ListaEmpresasLog()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        List<EmpresaDTO> emp = new List<EmpresaDTO>();
        //        client.BaseAddress = new Uri(UrlBase);
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
        //        try
        //        {
        //            HttpResponseMessage response = await client.GetAsync(ApiListaEmpresasLogin).ConfigureAwait(false);
        //            if (response.IsSuccessStatusCode)
        //                emp = await response.Content.ReadAsAsync<List<EmpresaDTO>>();
        //            else
        //            {
        //                //respuesta.Mensaje = "La respuesta a la petición no fue exitosa");
        //                client.CancelPendingRequests();
        //                client.Dispose();
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            emp = new List<EmpresaDTO>();
        //            client.CancelPendingRequests();
        //            client.Dispose(); ;
        //        }
        //        _listaEmpresasLogin = emp;
        //    }
        //}     
        #endregion
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
                _respuestaAutenticacion = respuesta;
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
                    {
                        resp = await response.Content.ReadAsAsync<RespuestaRequisicionDto>();
                    }
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
        #endregion
    }
}
