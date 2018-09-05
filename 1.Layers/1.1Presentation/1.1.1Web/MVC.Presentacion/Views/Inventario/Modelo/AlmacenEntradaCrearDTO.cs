using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.Inventario.Model
{
    public class AlmacenEntradaCrearDTO
    {
        public short IdEmpresa { get; set; }
        public int IdProduto { get; set; }
        public int IdOrdenCompra { get; set; }
        public string Observaciones { get; set; }
        public decimal Cantidad { get; set; }
    }
}