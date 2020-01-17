using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class AlmacenSalidaProductoDTO
    {
        public int IdRequisicion { get; set; }
        public short Orden { get; set; }
        public int IdAlmacen { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public int IdUsuarioEntrega { get; set; }
        public int IdUsuarioRecibe { get; set; }
        [Required(ErrorMessage = Error.R0002)]        
        [Display(Name = "Cantidad")]
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public string Ubicacion { get; set; }
        public decimal Requeridos { get; set; }
        public decimal CantidadEntregada { get; set; }
        public decimal CantidadActual { get; set; }
        public decimal CantidadAnterior { get; set; }
        public decimal CantidadFinal { get; set; }
        public string UrlDocSalida { get; set; }
        public string PathDocSalida { get; set; }
        public string Observaciones_ { get; set; }
        public bool Autorizado { get; set; }
        public System.DateTime FechaEntrada { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}
