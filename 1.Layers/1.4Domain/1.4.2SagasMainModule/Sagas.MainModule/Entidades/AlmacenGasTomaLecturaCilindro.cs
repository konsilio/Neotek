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
    
    public partial class AlmacenGasTomaLecturaCilindro
    {
        public short IdCAlmacenGas { get; set; }
        public int IdOrden { get; set; }
        public short IdOrdenCilindro { get; set; }
        public int IdCilindro { get; set; }
        public decimal Cantidad { get; set; }
    
        public virtual AlmacenGasTomaLectura TomaLectura { get; set; }
        public virtual UnidadAlmacenGasCilindro Cilindro { get; set; }
    }
}
