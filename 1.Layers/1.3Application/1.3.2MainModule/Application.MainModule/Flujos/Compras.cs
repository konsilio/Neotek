using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.Servicios;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Compras
    {
        public OrdenCompraRespuestaDTO ComprarGas()
        {
            UsuarioAplicacionServicio.Obtener();

            return new OrdenCompraRespuestaDTO()
            {
                Exito = true
            };
        }
        public RequisicionOCDTO BuscarRequisicion(int idRequisicion)
        {
            return OrdenCompraServicio.BuscarRequisicion(idRequisicion);
        }
        public List<OrdenCompraRespuestaDTO> GenerarOrdenesCompra(OrdenCompraCrearDTO oc)
        {

            string apiKey = "AIzaSyAmt7VWV6PGYouqEHEFi2n6sJHmJUuwzRk";
            string key = "fUrVmoad8oM:APA91bGQ9YR4WbE5oxALMw-dvqkbbMg5iKt3_dAbAyHzJSy-BTNNdlCsQ_sZwxCpDT-P3SmgCWto-v6FMeQLLIu-L0EEdyajTRBeWJmaKKjdjEZYuuHwuHWcnvuAOhDXK5Ldpb7-FnbHCk6LqjjkZ2HTkXrlGYYbsA";
            
            SendNotificationFromFirebaseCloud(key, apiKey);

            List<OrdenCompraRespuestaDTO> lrOC = new List<OrdenCompraRespuestaDTO>();
            //List<OrdenCompra> locDTO = OrdenCompraServicio.IdentificarOrdenes(oc);
            //locDTO = OrdenCompraServicio.AsignarProductos(oc.Productos, locDTO);
            //locDTO = OrdenCompraServicio.CalcularTotales(locDTO);
            //foreach (var ocDTO in locDTO)
            //{
            //    ocDTO.NumOrdenCompra = FolioServicio.GeneraNumerOrdenCompra(ocDTO);
            //    lrOC.Add(OrdenCompraServicio.GuardarOrdenCompra(ocDTO));               
            //}

            return lrOC;
        }
        public static String SendNotificationFromFirebaseCloud(string key,string apiKey)
        {
            var result = "-1";
            var webAddr = "https://fcm.googleapis.com/fcm/send";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=" + "AAAArTr6G44:APA91bFujr3tEdesRnwtYGkUtABZeZudWn0kVhD383ts2HWyo4RvzRFgK28POs2IYxjbTQnqMwa9rjJN30Xpogjtz_KV6QuwFFJFyqqQxXOLwkbBCZQPWmgFnBvep_jh7YcEfJ_rmFhnal8gE4i3Uo3U4MeI-uAQQg");
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string strNJson = @"{
                    ""to"": ""fUrVmoad8oM:APA91bGQ9YR4WbE5oxALMw-dvqkbbMg5iKt3_dAbAyHzJSy-BTNNdlCsQ_sZwxCpDT-P3SmgCWto-v6FMeQLLIu-L0EEdyajTRBeWJmaKKjdjEZYuuHwuHWcnvuAOhDXK5Ldpb7-FnbHCk6LqjjkZ2HTkXrlGYYbsA"",
                    ""data"": {
                        ""ShortDesc"": ""Some short desc"",
                        ""IncidentNo"": ""any number"",
                        ""Description"": ""detail desc""
                            },
                            ""notification"": {
                                        ""title"": ""ServiceNow: Incident No. number"",
                            ""text"": ""This is Notification"",
                            ""sound"":""default""
                            }
                        }";
                strNJson.Replace("[[key]]", key);

                streamWriter.Write(strNJson);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
    }
}
