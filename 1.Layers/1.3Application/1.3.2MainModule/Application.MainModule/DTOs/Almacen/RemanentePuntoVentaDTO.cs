using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class RemanentePuntoVentaDTO
    {
        public short IdEmpresa { get; set; }
        public int IdPuntoVenta { get; set; }
        public string NombrePuntoVenta { get; set; }
        public decimal Remanente { get; set; }
        public decimal Porcentaje { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int dia { get; set; }
    }
    public class RemanentePuntoVentaTodosDTO
    {
        public List<RemanentePuntoVentaDTO> RemaentePuntoVenta { get; set; }
    }
}
