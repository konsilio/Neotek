using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
   public class PrecioVentaEstatusDTO
    {
        public byte IdPrecioVentaEstatus { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Descripción")]
        public string Descripción { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegsitro { get; set; }
    }
}

