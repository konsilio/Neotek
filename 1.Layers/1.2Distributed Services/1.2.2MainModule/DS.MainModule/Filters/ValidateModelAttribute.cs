using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using DS.MainModule.Results;
using System.Web.Http.ModelBinding;

namespace DS.MainModule.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                RespuestaDto respuesta = new RespuestaDto()
                { ModelStates = actionContext.ModelState, ModelStatesStandar = new Dictionary<string, string>() };
                foreach (var values in respuesta.ModelStates.Values)
                    foreach (var error in values.Errors)
                    {
                        respuesta.MensajesError.Add(error.ErrorMessage);
                    }
                for (int index = 0; index < respuesta.ModelStates.Keys.Count; index++)
                {
                    if (!String.IsNullOrEmpty(respuesta.ModelStates.Values.ToArray()[index].Errors.ToList().FirstOrDefault().ErrorMessage))
                        respuesta.ModelStatesStandar.Add(respuesta.ModelStates.Keys.ToArray()[index].Split('.')[1], respuesta.ModelStates.Values.ToArray()[index].Errors.ToList().FirstOrDefault().ErrorMessage);
                }
                actionContext.Response = RespuestaHttp.crearRespuesta(respuesta, actionContext.Request);
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.ActionContext.ModelState.IsValid == false)
            {
                RespuestaDto respuesta = new RespuestaDto()
                { ModelStates = actionExecutedContext.ActionContext.ModelState, ModelStatesStandar = new Dictionary<string, string>() };
                foreach (var values in respuesta.ModelStates.Values)
                    foreach (var error in values.Errors)
                        respuesta.MensajesError.Add(error.ErrorMessage);
                for (int index = 0; index < respuesta.ModelStates.Keys.Count; index++)
                {
                    if (!String.IsNullOrEmpty(respuesta.ModelStates.Values.ToArray()[index].Errors.ToList().FirstOrDefault().ErrorMessage))
                        respuesta.ModelStatesStandar.Add(respuesta.ModelStates.Keys.ToArray()[index].Split('.')[1], respuesta.ModelStates.Values.ToArray()[index].Errors.ToList().FirstOrDefault().ErrorMessage);
                }
                actionExecutedContext.ActionContext.Response = RespuestaHttp.crearRespuesta(respuesta, actionExecutedContext.ActionContext.Request);
            }
        }
        public class RespuestaDto
        {
            public RespuestaDto()
            {
                MensajesError = new List<string>();
            }

            public bool Exito { get; set; }
            public bool EsInsercion { get; set; }
            public bool EsActulizacion { get; set; }
            public string Mensaje { get; set; }
            public int Id { get; set; }
            public string Codigo { get; set; }
            public bool ModeloValido { get; set; }
            public List<string> MensajesError { get; set; }
            public string RedirigirUrl { get; set; }
            public ModelStateDictionary ModelStates { get; set; }
            //public System.Web.Mvc.ModelStateDictionary ModelStatesMVC { get; set; }
            public Dictionary<string, string> ModelStatesStandar { get; set; }
        }
    }
}
