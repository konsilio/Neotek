using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    [Serializable]
    public class CuentaContableDTO
    {
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdCuentaContable")]
        public int IdCuentaContable { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Empresa")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Numero")]
        public string Numero { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get; set; }
    }
}