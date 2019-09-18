using Exceptions.MainModule.Validaciones;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.MainModule.DTOs.EquipoTransporte
{
    public class MantenimientoDetalleDTO
    {
        public int Id_DetalleMtto { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "FechaMtto")]
        public DateTime FechaMtto { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "id_vehiculo")]
        public int id_vehiculo { get; set; }
        public string Vehiculo { get; set; }
        public bool EsCamioneta { get; set; }
        public bool EsPipa { get; set; }
        public bool EsUtilitario { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Id_tipomtto")]
        public int Id_tipomtto { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "DescripcionMtto")]
        public string DescripcionMtto { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Kilometraje_Actual")]
        public int Kilometraje_Actual { get; set; }
        public string NumeroOC { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Monto")]
        public decimal Monto { get; set; }
        public int IdCuentaContable { get; set; }
    }
}
