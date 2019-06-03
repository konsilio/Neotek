using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.EquipoTransporte
{
    public class RecargaCombustibleDTO
    {
        public int Id_DetalleRecargaComb { get; set; }
        public int Id_Vehiculo { get; set; }
        public bool EsCamioneta { get; set; }
        public bool EsPipa { get; set; }
        public bool EsUtilitario { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Kilometraje Actual")]
        public int KilometrajeActual { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Kilometraje Recorrido")]
        public decimal KilometrajeRecorrido { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Litros Recargados")]
        public decimal LitrosRecargados { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Tipo Combustible")]
        public int IdTipoCombustible { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Vehiculo")]
        public string Vehiculo { get; set; }
        public string Chofer { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Fecha de Recarga")]
        public DateTime FechaRecarga { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Monto")]
        public decimal Monto { get; set; }
    }
}
