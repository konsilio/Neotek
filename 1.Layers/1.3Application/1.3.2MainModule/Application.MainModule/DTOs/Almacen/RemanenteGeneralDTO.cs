using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class RemanenteGeneralDTO
    {
        short IdEmpresa { get; set; }
        decimal InventarioInicial { get; set; }
        decimal Ventas { get; set; }
        decimal Carburacion { get; set; }
        decimal InventarioLibro { get; set; }
        decimal InventarioFisico { get; set; }
        decimal GasSobrante { get; set; }
        decimal RemanenteDecimal { get; set; }
        int Anio { get; set; }
        int Mes { get; set; }
        int dia { get; set; }

    }
}
