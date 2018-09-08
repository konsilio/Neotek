using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    [Serializable]
    public class PaisDTO
    {
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdPais")]
        public byte IdPais { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Pais")]
        public string Pais { get; set; }
    }
}
