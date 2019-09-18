using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Exceptions.MainModule.Validaciones;

namespace MVC.Presentacion.Models
{
    public class AsignacionModel
    {
        public int IdAsignacion { get; set; }
        public short IdEmpresa { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Chofer")]
        public int IdChofer { get; set; }
        public string Chofer { get; set; }
        public short IdVehiculo { get; set; }
        public string Vehiculo { get; set; }
        public short TipoVehiculo { get; set; }
    }
}