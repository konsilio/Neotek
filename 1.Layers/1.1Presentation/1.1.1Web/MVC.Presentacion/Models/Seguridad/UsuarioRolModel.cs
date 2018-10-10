using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Seguridad
{
    public class UsuarioRolModel
    {
        //public int IdUsuario { get; set; }
        //public short IdRol { get; set; }
        //public string Descripcion { get; set; }
        //public string Usuario { get; set; }
        //public string Role { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Rol")]
        public short IdRol { get; set; }

        [StringLength(1, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}