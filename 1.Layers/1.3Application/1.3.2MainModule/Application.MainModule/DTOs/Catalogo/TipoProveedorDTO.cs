using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    public class TipoProveedorDTO
    {     
        public byte IdTipoProveedor { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
