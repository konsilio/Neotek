using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Agentes
{
    public class AgenteServicio
    {
        public string respuesta = string.Empty;

        public void PostMethod<T>(T obj, string urlBase, string uri, KeyValuePair<string, string> Authorization, bool aplicationJson = true)
        {
            PostMetod(obj, urlBase, uri, Authorization, aplicationJson).Wait();
        }

        private async Task PostMetod<T>(T obj, string urlBase, string uri, KeyValuePair<string, string> Authorization, bool aplicationJson)
        {
            using (var client = new HttpClient())
            {             
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Accept.Clear();         
                if (aplicationJson)
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if(!string.IsNullOrEmpty(Authorization.Key))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Authorization.Key, Authorization.Value);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(uri, obj).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        respuesta = await response.Content.ReadAsStringAsync();
                    else
                    {
                        client.CancelPendingRequests();
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    respuesta = ex.Message;
                    client.CancelPendingRequests();
                    client.Dispose();
                }
            }
        }
    }
}
