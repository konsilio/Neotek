using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class RemanentePtoVentaDTO
    {
        public short IdEmpresa { get; set; }
        public int IdPuntoVenta { get; set; }
        public string NombrePuntoVenta { get; set; }
        public string Remanente { get; set; }
        public decimal Porcentaje { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public string dia { get; set; }
    }
    public class RemanentePuntoVentaTodosDTO
    {
        public List<RemanentePtoVentaDTO> RemaentePuntoVenta { get; set; }
    }
}