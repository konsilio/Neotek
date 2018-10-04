using Application.MainModule.DTOs.Mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Compras
{
     public class ComplementoGasDTO : PapeletaDTO
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
        public DateTime FechaEntraGas { get; set; }
        public decimal PorcenMagnatelOcularTractorINI { get; set; }
        public decimal PorcenMagnatelOcularAlmacenINI { get; set; }
        public decimal PorcenMagnatelOcularTractorFIN { get; set; }
        public decimal PorcenMagnatelOcularAlmacenFIN { get; set; }
        public bool TanquePrestado { get; set; }
        public decimal KilosPapeleta { get; set; }
        public decimal KilosDescargados { get; set; }
        public decimal KilosDiferencia { get; set; }
        public OrdenCompraDTO OrdenCompraExpedidor { get; set; }
        public OrdenCompraDTO OrdenCompraPorteador { get; set; }
        public List<ImagenDTO> Fotos { get; set; }
    }
}
