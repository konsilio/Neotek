//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sagas.MainModule.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class AlmacenGasTraspaso
    {
        public short IdCAlmacenGasSalida { get; set; }
        public short IdCAlmacenGasEntrada { get; set; }
        public short IdTipoMedidorSalida { get; set; }
        public byte IdTipoEvento { get; set; }
        public decimal P5000Salida { get; set; }
        public decimal P5000Entrada { get; set; }
        public Nullable<decimal> PorcentajeSalida { get; set; }
        public string ClaveOperacion { get; set; }
        public bool DatosProcesados { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    
        public virtual UnidadAlmacenGas UnidadSalida { get; set; }
        public virtual UnidadAlmacenGas UnidadEntrada { get; set; }
        public virtual AlmacenGasTraspasoFoto Fotografias { get; set; }
        public virtual TipoEvento TipoEvento { get; set; }
    }
}
