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
        
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Equipo Transporte")]
        public Nullable<int> IdEquipoTransporte { get; set; }
                
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Vehiculo utilitario")]
        public Nullable<int> IdVehiculoUtilitario { get; set; }

        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Unidad de Alamcen de gas")]
        public Nullable<short> IdCAlmacenGas { get; set; }

        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Estación de carburación")]
        public Nullable<int> IdEstacionCarburacion { get; set; }

        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Camioneta")]
        public Nullable<int> IdCamioneta { get; set; }

        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Pipa")]
        public Nullable<int> IdPipa { get; set; }

        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Cilindro de gas")]
        public Nullable<int> IdCilindro { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Nombre o Descripción")]
        public string Descripcion { get; set; }
    }
}