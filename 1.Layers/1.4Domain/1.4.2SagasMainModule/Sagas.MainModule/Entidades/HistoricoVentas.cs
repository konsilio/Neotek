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
    
    public partial class HistoricoVentas
    {
        public int Id { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal MontoVenta { get; set; }
        public bool EsPipa { get; set; }
        public bool EsCamioneta { get; set; }
        public bool EsLocal { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
    }
}