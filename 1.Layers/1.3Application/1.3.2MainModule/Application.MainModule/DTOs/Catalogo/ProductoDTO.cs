using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    [Serializable]
    public class ProductoDTO
    {
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdProducto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdProductoServicioTipo")]
        public short IdProductoServicioTipo { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "TipoProducto")]
        public string TipoProducto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdCategoria")]
        public short IdCategoria { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdProductoLinea")]
        public short IdProductoLinea { get; set; }

        public int IdCuentaContable { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdUnidadMedida")]
        public short IdUnidadMedida { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "UnidadMedida")]
        public string UnidadMedida { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdUnidadMedida2")]
        public short? IdUnidadMedida2 { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Minimos")]
        public decimal? Minimos { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Maximo")]
        public decimal? Maximo { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "UrlImagen")]
        public string UrlImagen { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "PathImagen")]
        public string PathImagen { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "FechaRegistro")]
        public DateTime? FechaRegistro { get; set; }
    }
}
