using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
   public class CombustibleDTO
    {
        public int Id_Combustible { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "TipoCombustible")]
        public string TipoCombustible { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        public string DescripcionBusqueda { get; set; }
        public bool Activo { get; set; }
        public short Id_Empresa { get; set; }
    }
}
