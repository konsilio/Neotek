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
    public class ProductoCrearDto
    {
        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "TIpo Producto/Servicio")]
        public short IdProductoServicioTipo { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Cuenta contable")]
        public int IdCuentaContable { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Categoría")]
        public short IdCategoria { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Línea del producto")]
        public short IdProductoLinea { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Unidad de medida")]
        public short IdUnidadMedida { get; set; }

        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.C0007)]
        [Display(Name = "La unidad de medida alterna")]
        public Nullable<short> IdUnidadMedida2 { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(500, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Es Activo de venta")]
        public bool EsActivoVenta { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Es Gas")]
        public bool EsGas { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Es Transporte de gas")]
        public bool EsTransporteGas { get; set; }
                
        [Range(minimum: 0, maximum: long.MaxValue, ErrorMessage = Error.C0008)]
        [Display(Name = "Almacén Mínimo")]
        public Nullable<decimal> Minimos { get; set; }

        [Range(minimum: 0, maximum: long.MaxValue, ErrorMessage = Error.C0008)]
        [Display(Name = "Almacén Mínimo")]
        public Nullable<decimal> Maximo { get; set; }
    }
}
