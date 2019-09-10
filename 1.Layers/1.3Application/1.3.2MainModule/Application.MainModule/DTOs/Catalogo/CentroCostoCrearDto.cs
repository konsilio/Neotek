using Exceptions.MainModule.Validaciones;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.MainModule.DTOs.Catalogo
{
    public class CentroCostoCrearDto
    {
        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Empresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Tipo centro de costo")]
        public byte IdTipoCentroCosto { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        
        public Nullable<int> IdEquipoTransporte { get; set; }
      
        public Nullable<int> IdVehiculoUtilitario { get; set; }

        public Nullable<short> IdCAlmacenGas { get; set; }

        public Nullable<int> IdEstacionCarburacion { get; set; }

       
        public Nullable<int> IdCamioneta { get; set; }

        public Nullable<int> IdPipa { get; set; }

        public Nullable<int> IdCilindro { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}