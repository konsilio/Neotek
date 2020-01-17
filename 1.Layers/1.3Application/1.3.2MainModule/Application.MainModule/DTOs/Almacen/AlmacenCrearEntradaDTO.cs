using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class AlmacenCrearEntradaDTO
    {
        public short IdEmpresa { get; set; }
        public int IdProduto { get; set; }
        public int IdOrdenCompra { get; set; }
        public string Observaciones { get; set; }
        public decimal Cantidad { get; set; }
    }
}
