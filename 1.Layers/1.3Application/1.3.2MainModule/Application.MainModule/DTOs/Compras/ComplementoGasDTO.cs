using Application.MainModule.DTOs.Mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Compras
{
    public class ComplementoGasDTO //: PapeletaDTO
    {
        public int IdRequisicion { get; set; }
        public int IdUsuarioSolicitante { get; set; }
        public short IdEmpresa { get; set; }
        public string NombreComercial { get; set; }
        public string NumeroRequisicion { get; set; }
        public string UsuarioSolicitante { get; set; }
        public string MotivoRequisicion { get; set; }
        public string RequeridoEn { get; set; }
        public DateTime FechaRequerida { get; set; }

        public decimal FactorLitrosAKilos { get; set; }
        public DateTime FechaEntraGas { get; set; }
        public decimal PorcenMagnatelOcularTractorINI { get; set; }
        public decimal PorcenMagnatelOcularAlmacenINI { get; set; }
        public decimal PorcenMagnatelOcularTractorFIN { get; set; }
        public decimal PorcenMagnatelOcularAlmacenFIN { get; set; }
        public bool TanquePrestado { get; set; }
        public decimal KilosPapeleta { get; set; }
        public decimal KilosDescargados { get; set; }
        public decimal KilosDiferencia { get; set; }

        //Papepleta
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
        public List<String> Imagenes { get; set; }
        public decimal PorcentajeMedidor { get; set; }
        public String NombreTipoMedidorTractor { get; set; }
        public short IdTipoMedidorTractor { get; set; }
        public short CantidadFotosTractor { get; set; }
        //Papeleta
        public decimal PrecioConIVA { get; set; }
        public List<ProductoOCDTO> Productos { get; set; }
        public OrdenCompraDTO OrdenCompraExpedidor { get; set; }
        public OrdenCompraDTO OrdenCompraPorteador { get; set; }
        public List<ImagenDTO> Fotos { get; set; }
    }
}
