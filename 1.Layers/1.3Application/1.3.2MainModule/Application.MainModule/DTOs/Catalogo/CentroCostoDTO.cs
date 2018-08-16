using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    public class CentroCostoDTO
    {
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdCentroCosto")]
        public int IdCentroCosto { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdTipoCentroCosto")]
        public byte IdTipoCentroCosto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdEquipoTransporte")]
        public int IdEquipoTransporte { get; set; }

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
