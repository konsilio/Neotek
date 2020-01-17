using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class AlmacenDTO
    {
        public int IdAlmacen { get; set; }
        public short IdEmpresa { get; set; }
        public int IdProducto { get; set; }
        public short IdCategoria { get; set; }
        public short IdProductoLinea { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public string ProductoLinea { get; set; }
        public decimal Cantidad { get; set; }
        public string Ubicacion { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}