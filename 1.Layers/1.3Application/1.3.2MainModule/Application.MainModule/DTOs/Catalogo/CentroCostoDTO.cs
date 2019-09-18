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
    public class CentroCostoDTO
    {
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdCentroCosto")]
        public int IdCentroCosto { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        public string Empresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdTipoCentroCosto")]
        public byte IdTipoCentroCosto { get; set; }

        public string TipoCentroCosto { get; set; }

        public int IdEquipoTransporte { get; set; }

        public int IdVehiculoUtilitario { get; set; }
    
        public short IdCAlmacenGas { get; set; }

        public int IdEstacionCarburacion { get; set; }

        public int IdCamioneta { get; set; }

        public int IdPipa { get; set; }

        public int IdCilindro { get; set; }

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
