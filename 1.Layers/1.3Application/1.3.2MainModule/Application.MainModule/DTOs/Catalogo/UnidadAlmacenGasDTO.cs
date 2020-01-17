using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    public class UnidadAlmacenGasDTO
    {
        public short IdCAlmacenGas { get; set; }
        public Nullable<short> IdAlmacenGas { get; set; }
        public short IdEmpresa { get; set; }
        public byte IdTipoAlmacen { get; set; }
        public Nullable<short> IdTipoMedidor { get; set; }
        public Nullable<int> IdEstacionCarburacion { get; set; }
        public Nullable<int> IdCamioneta { get; set; }
        public Nullable<int> IdPipa { get; set; }
        public bool EsGeneral { get; set; }
        public bool EsAlterno { get; set; }
        public string Numero { get; set; }
        public Nullable<decimal> CapacidadTanqueLt { get; set; }
        public Nullable<decimal> CapacidadTanqueKg { get; set; }
        public decimal CantidadActualLt { get; set; }
        public decimal CantidadActualKg { get; set; }
        public decimal PorcentajeActual { get; set; }
        public Nullable<decimal> P5000Actual { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public decimal PorcentajeCalibracionPlaneada { get; set; }
    }
}
