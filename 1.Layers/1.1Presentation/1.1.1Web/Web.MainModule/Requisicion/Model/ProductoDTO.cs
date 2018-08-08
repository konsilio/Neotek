using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.Requisicion.Model
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public short IdEmpresa { get; set; }
        public short IdProductoServicioTipo { get; set; }
        public short IdCategoria { get; set; }
        public short IdProductoLinea { get; set; }
        public short IdUnidadMedida { get; set; }
        public Nullable<short> IdUnidadMedida2 { get; set; }
        public string Descripcion { get; set; }
        public Nullable<decimal> Minimos { get; set; }
        public Nullable<decimal> Maximo { get; set; }
        public string UrlImagen { get; set; }
        public string PathImagen { get; set; }
        public bool Activo { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
    }
}