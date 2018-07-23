using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Seguridad
{
    public class AutenticacionDto
    {
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [StringLength(500, MinimumLength = 1, ErrorMessage = Error.S0002)]
        [Display(Name = "Nombre de usuario")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [StringLength(250, MinimumLength = 8, ErrorMessage = Error.S0002)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}
