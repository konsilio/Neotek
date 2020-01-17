using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace Utilities.MainModule
{
    public static class ValidadorClases
    {
        public static RespuestaOperacionDto EnlistaErrores<T>(T dto)
        {
            var respuesta = new RespuestaOperacionDto();

            var _validacion = new ValidationContext(dto, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, _validacion, results);

            foreach (var error in results)
            {
                respuesta.MensajesError.Add(new Result { IdentidadError = error.MemberNames.SingleOrDefault(), MensajeError = error.ErrorMessage });
            }

            respuesta.ModeloValido = isValid;

            return respuesta;
        }
    }
    
    public class RespuestaOperacionDto
    {
        public RespuestaOperacionDto()
        {
            MensajesError = new List<Result>();
        }
        public int? Id { get; set; }
        public bool Guardado { get; set; }
        public bool EsInsercion { get; set; }
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public bool ModeloValido { get; set; }
        public List<Result> MensajesError { get; set; }
        public string RedirigirUrl { get; set; }
        public ModelStateDictionary ModelStates { get; set; }
    }
    public class Result
    {
        public Result() { }
        public string MensajeError { get; set;}
        public string IdentidadError { get; set; }
    } 
}
