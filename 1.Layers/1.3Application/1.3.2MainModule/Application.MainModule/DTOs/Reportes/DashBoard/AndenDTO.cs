using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class AndenDTO
    {
        public decimal TotalProduto { get; set; }
        public string OrdenCompra { get; set; }
        public int NivelAlmacen { get; set; }
        public List<VentaPuntoDeVenta> Ventas {get; set;}
    }
}
