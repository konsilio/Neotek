using Application.MainModule.DTOs.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class FacturacionDTO
    {
        public int IdCliente { get; set; }
        public string RFC { get; set; }
        public short IdEmpresa { get; set; }
        public string Ticket { get; set; }
        public DateTime FechaVenta { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFinal { get; set; }
        public List<VentaPuntoVentaDTO> Tickets { get; set; }
    }
}
