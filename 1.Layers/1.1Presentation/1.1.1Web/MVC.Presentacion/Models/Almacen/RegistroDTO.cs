using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class RegistroDTO
    {
        public int IdProducto { get; set; }
        public short IdEmpresa { get; set; }
        public short IdCategoria { get; set; }
        public short IdProductoLinea { get; set; }
        public string NombreEmpresa { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CantidadAnterior { get; set; }
        public decimal CantidadFinal { get; set; }
        public string Referencia { get; set; }
        public bool EsEntrada { get; set; }
        public bool EsSalida { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}