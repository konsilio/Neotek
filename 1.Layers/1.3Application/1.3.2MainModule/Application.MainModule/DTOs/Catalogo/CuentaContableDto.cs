using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Exceptions.MainModule.Validaciones;

namespace Application.MainModule.DTOs.Catalogo
{
    [Serializable]
    public class CuentaContableDto
    {
        public int IdCuentaContable { get; set; }
        public short IdEmpresa { get; set; }
        public string Empresa { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "numero")]

        public string Numero { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "descripcion")]
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}
