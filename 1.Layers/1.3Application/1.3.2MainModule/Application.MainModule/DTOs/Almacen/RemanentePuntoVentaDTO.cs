using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class RemanentePuntoVentaDTO
    {
        short IdEmpresa { get; set; }
        int IdPuntoVenta { get; set; }
        string NombrePuntoVenta { get; set; }
        decimal Remanente { get; set; }
        int Anio { get; set; }
        int Mes { get; set; }
        int dia { get; set; }
    }
}
