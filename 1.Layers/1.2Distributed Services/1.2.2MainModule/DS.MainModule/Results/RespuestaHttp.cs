using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Compras;

namespace DS.MainModule.Results
{
    public static class RespuestaHttp
    {
        public static HttpResponseMessage crearRespuesta(RespuestaDto respuesta, HttpRequestMessage request)
        {
            HttpResponseMessage respuestaHttp;

            if (respuesta.Exito)
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, respuesta);
            else
                respuestaHttp = request.CreateResponse(HttpStatusCode.BadRequest, respuesta);

            return respuestaHttp;
        }

        internal static HttpResponseMessage crearRespuesta(RespuestaDto respuestaDto, object request)
        {
            throw new NotImplementedException();
        }

        public static HttpResponseMessage crearRespuesta(DS.MainModule.Filters.ValidateModelAttribute.RespuestaDto respuesta, HttpRequestMessage request)
        {
            HttpResponseMessage respuestaHttp;

            if (respuesta.Exito)
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, respuesta);
            else
                respuestaHttp = request.CreateResponse(HttpStatusCode.BadRequest, respuesta);

            return respuestaHttp;
        }

        public static HttpResponseMessage crearRespuesta(RespuestaAutenticacionMobileDto respuesta, HttpRequestMessage request)
        {
            HttpResponseMessage respuestaHttp;

            if (respuesta.Exito)
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, respuesta);
            else
                respuestaHttp = request.CreateResponse(HttpStatusCode.BadRequest, respuesta);

            return respuestaHttp;
        }

        public static HttpResponseMessage crearRespuesta(RespuestaOrdenesCompraDTO  respuesta, HttpRequestMessage request)
        {
            HttpResponseMessage respuestaHttp;

            if (respuesta.Exito)
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, respuesta);
            else
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, respuesta);

            return respuestaHttp;
        }
        public static HttpResponseMessage crearRespuesta(OrdenCompraRespuestaDTO respuesta, HttpRequestMessage request)
        {
            HttpResponseMessage respuestaHttp;

            if (respuesta.Exito)
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, respuesta);
            else
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, respuesta);

            return respuestaHttp;
        }

        public static HttpResponseMessage crearRespuesta(DatosTomaLecturaDto respuesta, HttpRequestMessage request)
        {
            return request.CreateResponse(HttpStatusCode.OK, respuesta);           
        }

        public static HttpResponseMessage crearRespuesta<T>(List<T> respuesta, HttpRequestMessage request)
        {
            return request.CreateResponse(HttpStatusCode.OK, respuesta);
        }

        public static HttpResponseMessage crearRespuesta(DatosTipoPersonaDto respuesta, HttpRequestMessage request)
        {
            return request.CreateResponse(HttpStatusCode.OK, respuesta);
        }

        public static HttpResponseMessage crearRespuesta(ReporteDiaDTO respuesta, HttpRequestMessage request)
        {
            return request.CreateResponse(HttpStatusCode.OK, respuesta);
        }

        public static HttpResponseMessage crearRespuesta(DatosRecargaDto respuesta, HttpRequestMessage request)
        {
            return request.CreateResponse(HttpStatusCode.OK, respuesta);
        }

        public static HttpResponseMessage crearRespuesta(DatosAutoconsumoDto respuesta, HttpRequestMessage request)
        {
            return request.CreateResponse(HttpStatusCode.OK, respuesta);
        }

        public static HttpResponseMessage crearRespuesta(DatosCalibracionDto respuesta, HttpRequestMessage request)
        {
            return request.CreateResponse(HttpStatusCode.OK, respuesta);
        }
    }
}