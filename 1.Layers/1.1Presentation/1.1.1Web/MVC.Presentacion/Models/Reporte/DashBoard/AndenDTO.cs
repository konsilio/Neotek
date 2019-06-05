using MVC.Presentacion.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class AndenDTO
    {
        public decimal TotalProduto { get; set; }
        public string OrdenCompra { get; set; }
        public int NivelAlmacen { get; set; }
        public decimal KilosAlmacen { get; set; }
        public List<VentaPuntoVentaDTO> Ventas { get; set; }
    }
}