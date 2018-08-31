using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class AlmacenEntradaDTO
    {
        public int IdRequisicion { get; set; }
        public int IdOrdenCompra { get; set; }
        public int IdAlmacen { get; set; }
        public int IdProduto { get; set; }
        public int IdUsuarioRecibe { get; set; }
        public decimal Cantidad { get; set; }
        public string UrlDocEntrada { get; set; }
        public string PathDocEntrada { get; set; }
        public string Observaciones_ { get; set; }
        public System.DateTime FechaEntrada { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}
