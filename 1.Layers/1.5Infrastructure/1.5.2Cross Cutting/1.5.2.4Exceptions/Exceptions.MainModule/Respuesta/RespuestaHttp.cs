using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.MainModule.Respuesta
{
    public class RespuestaHttp
    {
        public HttpResponseMessage crearRepuesta<T>(T dto, HttpRequestMessage request)
        {
            HttpResponseMessage respuestaHttp;
            //if (respuesta.Guardado)
            if (dto.EsInsercion)
                respuestaHttp = request.CreateResponse(HttpStatusCode.Created, dto);
            else
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, dto);
            //else
            //{
            //    respuestaHttp = request.CreateResponse(HttpStatusCode.NotFound, respuesta);
            //}
            return respuestaHttp;
        }

        public HttpResponseMessage crearRepuesta(UsuarioADDto usuarioDto, HttpRequestMessage request)
        {
            if (usuarioDto.Existe)
                respuestaHttp = request.CreateResponse(HttpStatusCode.OK, usuarioDto);
            else
                respuestaHttp = request.CreateResponse(HttpStatusCode.NotFound, usuarioDto);
            return respuestaHttp;
        }

        public HttpResponseMessage crearRepuesta(string respuesta, HttpRequestMessage request)
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent(respuesta);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
