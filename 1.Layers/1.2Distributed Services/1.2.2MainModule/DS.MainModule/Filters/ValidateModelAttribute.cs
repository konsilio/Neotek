using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using DS.MainModule.Results;

namespace DS.MainModule.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                RespuestaDto respuesta = new RespuestaDto()
                { ModelStates = actionContext.ModelState };

                foreach (var values in respuesta.ModelStates.Values)
                    foreach (var error in values.Errors)
                        respuesta.MensajesError.Add(error.ErrorMessage);

                actionContext.Response = RespuestaHttp.crearRepuesta(respuesta, actionContext.Request);
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.ActionContext.ModelState.IsValid == false)
            {
                RespuestaDto respuesta = new RespuestaDto()
                { ModelStates = actionExecutedContext.ActionContext.ModelState };

                foreach (var values in respuesta.ModelStates.Values)
                    foreach (var error in values.Errors)
                        respuesta.MensajesError.Add(error.ErrorMessage);

                actionExecutedContext.ActionContext.Response = RespuestaHttp.crearRepuesta(respuesta, actionExecutedContext.ActionContext.Request);
            }
        }

        public class RespuestaDto
        {
            public RespuestaDto()
            {
                MensajesError = new List<string>();
            }

            public bool Exito { get; set; }
            public int? Id { get; set; }
            public bool Guardado { get; set; }
            public bool EsInsercion { get; set; }
            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public bool ModeloValido { get; set; }
            public List<string> MensajesError { get; set; }
            public string RedirigirUrl { get; set; }
            public ModelStateDictionary ModelStates { get; set; }
        }
    }
}
