using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class AlmacenDTO
    {
        public int IdAlmacen { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Empresa")]
        public short IdEmpresa { get; set; }
        public int IdProducto { get; set; }
        public short IdCategoria { get; set; }
        public short IdProductoLinea { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public string ProductoLinea { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Cantidad")]
        public decimal Cantidad { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
