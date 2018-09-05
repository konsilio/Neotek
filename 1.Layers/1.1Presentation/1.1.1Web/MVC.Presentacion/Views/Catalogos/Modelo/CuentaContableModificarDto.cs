using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.MainModule.Catalogos.Model
{
    [Serializable]
    public class CuentaContableModificarDto : CuentaContableCrearDto
    {
        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Cuenta Contable")]
        public int IdCuenta { get; set; }
    }
}