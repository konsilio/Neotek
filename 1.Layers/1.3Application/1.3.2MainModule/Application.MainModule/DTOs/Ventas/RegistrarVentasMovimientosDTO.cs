using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Ventas
{
    public class RegistrarVentasMovimientosDTO
    {
        public List<VentaPuntoDeVenta> Tickets { get; set; }
        public List<VentaCorteAnticipoEC> Lecturas { get; set; }             

        public AlmacenGasMovimiento Movimiento { get; set; }

        public short IdEmpresa { get; set; }
        public short Year { get; set; }
        public byte Mes { get; set; }
        public byte Dia { get; set; }
        public short Orden { get; set; }
        public byte IdTipoMovimiento { get; set; }
        public int IdPuntoVenta { get; set; }
        public int IdCliente { get; set; }
        public int IdOperadorChofer { get; set; }
        public short IdCAlmacenGas { get; set; }
        public string FolioOperacionDia { get; set; }
        public string FolioVenta { get; set; }
        public string FolioAnticipo { get; set; }
        public string FolioCorteCaja { get; set; }
        public string TipoMovimiento { get; set; }
        public string Descripcion { get; set; }
        public decimal Ingreso { get; set; }
        public decimal Egreso { get; set; }
        public decimal Saldo { get; set; }
        public string PuntoVenta { get; set; }
        public string OperadorChoferNombre { get; set; }
        public System.DateTime FechaAplicacion { get; set; }
        public System.DateTime FechaRegistro { get; set; }

    }


}
