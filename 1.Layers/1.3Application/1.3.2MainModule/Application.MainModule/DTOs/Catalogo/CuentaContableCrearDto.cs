using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    public class CuentaContableCrearDto
    {
        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Identificador de la cuenta")]
        public string Numero { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Nombre de la cuenta")]
        public string Descripcion { get; set; }
    }
}
