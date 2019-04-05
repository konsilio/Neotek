using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Facturacion
{
    public class FacturacionModel
    {    
        public int IdCliente { get; set; }
        public string RFC { get; set; }     
        public short IdEmpresa { get; set; }
        public string Ticket { get; set; }
        public short IdUsoCFDI { get; set; }
        public short IdFormaPago { get; set; }
        public DateTime FechaVenta { get; set; }     
        public List<VentaPuntoVentaDTO> Tickets { get; set; }
    }
}