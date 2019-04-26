using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class EquipoTransporteDTO
    {
        public int IdEquipoTransporte { get; set; }
        public short IdEmpresa { get; set; }
        public Nullable<int> IdVehiculoUtilitario { get; set; }
        public Nullable<int> IdCamioneta { get; set; }
        public Nullable<int> IdPipa { get; set; }
        public Nullable<int> IdEstacion { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public string Descripcion { get; set; }

        //Info Vehicular
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Numero de Identificacion")]
        public string NumIdVehicular { get; set; }//NumeroIdentificacion
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Numero de Placas")]
        public string Placas { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Numero de Motor")]
        public string NumMotor { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Vehiculo")]
        public string DescVehiculo { get; set; }//Vehiculo 
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Marca")]
        public string Marca { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Color")]
        public string Color { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Cilindros")]
        public short Cilindros { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Tipo de Combustible")]
        public int IdTipoCombustible { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Tipo de Unidad")]
        public int IdTipoUnidad { get; set; }
        public decimal CapacidadLts { get; set; }
        public decimal CapacidadKg { get; set; }
        public short IdTipoMedidor { get; set; }
        public bool Activo { get; set; }
        public bool EsForaneo { get; set; }
        public string AliasUnidad { get; set; }
        public int IdEquipoTransporteDetalle { get; set; }   

        public Nullable<bool> EsCamioneta { get; set; }
        public Nullable<bool> EsPipa { get; set; }
        public Nullable<bool> EsUtilitario { get; set; }

    }
}
