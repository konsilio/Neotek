using Exceptions.MainModule.Validaciones;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.MainModule.DTOs.Almacen
{
    [Serializable]
    public class AlmacenEntradaDTO
    {        
        public int IdAlmacen { get; set; }
        public int IdProducto { get; set; }    
        public string TipoProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Requeridos { get; set; }
        public string UnidadMedida { get; set; }
        public string Aplicacion { get; set; }
        public int IdUsuarioRecibe { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Cantidad")]
        public decimal Cantidad { get; set; }
        public string UrlDocEntrada { get; set; }
        public string PathDocEntrada { get; set; }
        public string Observaciones_ { get; set; }
        public System.DateTime FechaEntrada { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}
