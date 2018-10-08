using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
   public class PapeletaDTO
    {   
        public string ClaveOperacion { get; set; }
        public int IdOrdenCompraExpedidor { get; set; }
        public int IdOrdenCompraPorteador { get; set; }
        public int IdProveedorPorteador { get; set; }
        public int IdProveedorExpedidor { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaEmbarque { get; set; }
        public String NumeroEmbarque { get; set; }
        public String PlacasTractor { get; set; }
        public String NombreOperador { get; set; }
        public String Producto { get; set; }
        public String NumeroTanque { get; set; }
        public decimal PresionTanque { get; set; }
        public decimal CapacidadTanque { get; set; }
        public decimal PorcentajeTanque { get; set; }
        public Double Masa { get; set; }
        public String Sello { get; set; }
        public decimal ValorCarga { get; set; }
        public String NombreResponsable { get; set; }
        public List<string> Imagenes { get; set; }
        public decimal PorcentajeMedidor { get; set; }
        public String NombreTipoMedidorTractor { get; set; }
        public short IdTipoMedidorTractor { get; set; }
        public short CantidadFotosTractor { get; set; }
    }
}
