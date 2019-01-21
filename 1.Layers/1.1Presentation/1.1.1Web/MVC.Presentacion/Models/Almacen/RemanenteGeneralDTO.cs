using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Almacen
{
    public class RemanenteGeneralDTO
    {
        public short IdEmpresa { get; set; }
        public decimal InventarioInicial { get; set; }
        public decimal AcumuladoCompras { get; set; }
        public decimal Ventas { get; set; }
        public decimal Carburacion { get; set; }
        public decimal InventarioLibro { get; set; }
        public decimal InventarioFisico { get; set; }
        public decimal GasSobrante { get; set; }
        public decimal RemanenteDecimal { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int dia { get; set; }
    }
}