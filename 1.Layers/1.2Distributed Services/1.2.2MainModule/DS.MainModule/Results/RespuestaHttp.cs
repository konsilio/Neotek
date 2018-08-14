using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;

namespace DS.MainModule.Results
{
    public static class RespuestaHttp
    {
        public static HttpResponseMessage crearRepuesta(RespuestaDto respuesta, HttpRequestMessage request)
        {
            HttpResponseMessage respuestaHttp;

            if (respuesta.Exito)
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, respuesta);
            else
                respuestaHttp = request.CreateResponse(HttpStatusCode.BadRequest, respuesta);

            return respuestaHttp;
        }

        public static HttpResponseMessage crearRepuesta(DS.MainModule.Filters.ValidateModelAttribute.RespuestaDto respuesta, HttpRequestMessage request)
        {
            HttpResponseMessage respuestaHttp;

            if (respuesta.Exito)
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, respuesta);
            else
                respuestaHttp = request.CreateResponse(HttpStatusCode.BadRequest, respuesta);

            return respuestaHttp;
        }

        public static HttpResponseMessage crearRepuesta(RespuestaAutenticacionMobileDto respuesta, HttpRequestMessage request)
        {
            HttpResponseMessage respuestaHttp;

            if (respuesta.Exito)
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, respuesta);
            else
                respuestaHttp = request.CreateResponse(HttpStatusCode.BadRequest, respuesta);

            return respuestaHttp;
        }

        public static HttpResponseMessage crearRepuesta(RespuestaOrdenesCompraDTO respuesta, HttpRequestMessage request)
        {
            HttpResponseMessage respuestaHttp;

            if (respuesta.Exito)
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, respuesta);
            else
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, respuesta);

            return respuestaHttp;
        }        

        //public HttpResponseMessage crearRepuesta(RespuestaDto respuesta, HttpRequestMessage request)
        //{
        //    HttpResponseMessage respuestaHttp;

        //    if (respuesta.Exito)
        //        if (respuesta.EsInsercion)                
        //        respuestaHttp = request.CreateResponse(HttpStatusCode.Created, respuesta);
        //    else
        //        respuestaHttp = request.CreateResponse(HttpStatusCode.OK, respuesta);
        //    else
        //        respuestaHttp = request.CreateResponse(HttpStatusCode.BadRequest, respuesta);

        //    return respuestaHttp;
        //}
    }
}