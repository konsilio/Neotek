using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Exceptions.MainModule.Validaciones;
namespace MVC.Presentacion.Models.Almacen
{
    public class RemanenteModel
    {
        [Required(ErrorMessage =Error.R0002)]
        public short IdEmpresa { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        public int IdTipo { get; set; }
        
        public int IdPuntoVenta { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        public DateTime Fecha { get; set; }
        
    }
}