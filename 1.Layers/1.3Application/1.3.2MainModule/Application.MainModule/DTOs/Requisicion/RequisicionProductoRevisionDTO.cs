﻿using Exceptions.MainModule.Validaciones;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.MainModule.DTOs.Requisicion
{
    [Serializable]
    public class RequisicionProductoRevisionDTO
    {
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdProducto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Producto")]
        public string Producto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdTipoProducto")]
        public int IdTipoProducto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "TipoProducto")]
        public string TipoProducto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdCentroCosto")]
        public int IdCentroCosto { get; set; }

        public short Orden { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "CentroCosto")]
        public string CentroCosto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdUnidad")]
        public int IdUnidad { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Unidad")]
        public string Unidad { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(typeof(decimal), "0.0001", "9999999", ErrorMessage = Error.R0005)]
        [Display(Name = "Cantidad")]
        public decimal Cantidad { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(500, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Aplicacion")]
        public string Aplicacion { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "RevisionFisica")]
        public bool RevisionFisica { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "CantidadAlmacenActual")]
        public decimal CantidadAlmacenActual  { get; set;   }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "CantidadAComprar")]
        public decimal CantidadAComprar { get; set; }     
    }
}