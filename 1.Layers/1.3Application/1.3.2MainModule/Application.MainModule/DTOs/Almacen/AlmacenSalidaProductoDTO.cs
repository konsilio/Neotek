using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class AlmacenSalidaProductoDTO
    {
        public int IdRequisicion { get; set; }
        public byte Orden { get; set; }
        public int IdAlmacen { get; set; }
        public int IdProducto { get; set; }
        public int IdUsuarioEntrega { get; set; }
        public int IdUsuarioRecibe { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CantidadAnterior { get; set; }
        public decimal CantidadFinal { get; set; }
        public string UrlDocSalida { get; set; }
        public string PathDocSalida { get; set; }
        public string Observaciones_ { get; set; }
        public System.DateTime FechaEntrada { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}
