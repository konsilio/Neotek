using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.OrdenCompra
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
        [Range(minimum:1, maximum:100, ErrorMessage =Error.R0003)]
        public decimal Cantidad { get; set; }
        public string UrlDocEntrada { get; set; }
        public string PathDocEntrada { get; set; }
        public string Observaciones_ { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}