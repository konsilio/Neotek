﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Exceptions.MainModule.Validaciones;
using System;

namespace Application.MainModule.DTOs.Requisicion
{

    public class RequisicionCrearDTO
    {
        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdUsuarioSolicitante")]
        public int IdUsuarioSolicitante { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(500, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "MotivoRequisicion")]
        public string MotivoRequisicion { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(500, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "RequeridoEn")]
        public string RequeridoEn { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: byte.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdRequisicionEstatus")]
        public byte IdRequisicionEstatus { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "FechaRequerida")]
        public System.DateTime FechaRequerida { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "FechaRegistro")]
        public System.DateTime FechaRegistro { get; set; }        
        public List<RequisicionProductoGridDTO> ListaProductos { get; set; }
    }
}