using Application.MainModule.DTOs.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class CorteCajaDTO
    {
        public List<VentaPuntoVentaDTO> Tickets { get; set; }
        public List<VentasPipaDto> Lecturas { get; set; }
        public DateTime Fecha { get; set; }
        public string FolioOperacionDia { get; set; }
        public string OperadorChofer { get; set; }
        public short TipoUnidad { get; set; }
        public int IdPuntoVenta { get; set; }
        public string NombreUnidad { get; set; }
        public decimal TotalCantidad { get; set; }

        public decimal TotalVenta { get; set; }
        public decimal TotalOtros { get; set; }
        public decimal PrecioLitro { get; set; }
        public decimal TotalCredito { get; set; }
        public decimal TotalContado { get; set; }
        public decimal TotalEfectio { get; set; }
        public decimal Descuentos { get; set; }
        public decimal Bonidificaciones { get; set; }
        public decimal TotalCheques { get; set; }
        public decimal TotalTransferencias { get; set; }
        public string Mensaje { get; set; }
        //public decimal Bonidificaciones { get; set; }

        public CorteCajaDTO()
        {
            Tickets = new List<VentaPuntoVentaDTO>();
            Lecturas = new List<VentasPipaDto>();
        }
    }
}
