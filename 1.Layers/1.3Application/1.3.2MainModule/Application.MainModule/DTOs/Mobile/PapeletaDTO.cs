using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
   public class PapeletaDTO
    {
        private int IdOrdenCompraExpedidor { get; set; }

        private int IdOrdenCompraPorteador { get; set; }

        private int IdProveedorPorteador { get; set; }

        private int IdProveedorExpedidor { get; set; }

        private DateTime Fecha { get; set; }

        private DateTime FechaEmbarque { get; set; }

        private String NumeroEmbarque { get; set; }

        private String PlacasTractor { get; set; }

        private String NombreOperador { get; set; }

        private String Producto { get; set; }

        private String NumeroTanque { get; set; }

        private Double PresionTanque { get; set; }

        private Double CapacidadTanque { get; set; }

        private Double PorcentajeTanque { get; set; }

        private Double Masa { get; set; }

        private String Sello { get; set; }

        private Double ValorCarga { get; set; }

        private String NombreResponsable { get; set; }

        private List<byte[]> Imagenes { get; set; }

        private Double PorcentajeMedidor { get; set; }

        private String NombreTipoMedidorTractor { get; set; }

        private int IdTipoMedidorTractor { get; set; }

        private int CantidadFotosTractor { get; set; }

    }
}
