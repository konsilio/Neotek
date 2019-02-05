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
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public short Cilindros { get; set; }
        public int IdTipoCombustible { get; set; }
        public int IdTipoUnidad { get; set; }
        public bool Activo { get; set; }
        public string AliasUnidad { get; set; }

        public int Id_DetalleEtransporte { get; set; }      
        public Nullable<bool> EsCamioneta { get; set; }
        public Nullable<bool> EsPipa { get; set; }
        public Nullable<bool> EsUtilitario { get; set; }

    }
}
