﻿using Application.MainModule.DTOs.Respuesta;
using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    public class PrecioVentaDTO
    {
        public short IdPrecioVenta { get; set; }
        public short IdEmpresa { get; set; }
        public byte IdPrecioVentaEstatus { get; set; }

      
        public short IdCategoria { get; set; }

       
        public short IdProductoLinea { get; set; }
        public int IdProducto { get; set; }
        public string Categoria { get; set; }        
        public string Linea { get; set; }
       

        public string Producto { get; set; }
        public Nullable<decimal> PrecioActual { get; set; }
        public Nullable<decimal> PrecioPemexKg { get; set; }
        public Nullable<decimal> PrecioPemexLt { get; set; }
        public Nullable<decimal> UtilidadEsperadaKg { get; set; }
        public Nullable<decimal> UtilidadEsperadaLt { get; set; }

       
        public Nullable<decimal> PrecioSalida { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        public Nullable<decimal> PrecioSalidaKg { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        public Nullable<decimal> PrecioSalidaLt { get; set; }
        public Nullable<decimal> PrecioFlete { get; set; }
        public bool EsGas { get; set; }

     
        public System.DateTime FechaProgramada { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
        public bool Activo { get; set; }
        public string PrecioVentaEstatus { get; set; }
        public string CategoriaProducto { get; set; }
        public string LineaProducto { get; set; }
        public string Empresa { get; set; }
        public Nullable<short> IdUnidadMedida { get; set; }
        public Nullable <int> IdEstacion { get; set; }
        public string Estacion { get; set; }
        public RespuestaDto respuesta { get; set; }
       
    }
}
