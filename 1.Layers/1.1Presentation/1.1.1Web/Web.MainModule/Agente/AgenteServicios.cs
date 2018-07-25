using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Web.MainModule.Seguridad.Model;

using System.Configuration;

namespace Web.MainModule.Agente
{
    public class AgenteServicios 
    {
        private static string UrlBase;
        private string ApiLogin;
        private string ApiListaEmpresas;
                
        public RespuestaAutenticacionDto _respuestaAutenticacion;
        public List<EmpresaDTO> _listaEmpresas;

        public AgenteServicios()
        {
            UrlBase = ConfigurationManager.AppSettings["WebApiUrlBase"];            
        }

        #region Login

       
        public void ListaEmpresasLogin()
        {
            this.ApiListaEmpresas = ConfigurationManager.AppSettings["GetListaEmpresasLogin"];
            ListaEmpresas().Wait();
        }

        private async Task ListaEmpresas()
        {
            using (var client = new HttpClient())
            {
               List<EmpresaDTO> emp = new List<EmpresaDTO>();
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ApiListaEmpresas).ConfigureAwait(false);
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

    }

}
