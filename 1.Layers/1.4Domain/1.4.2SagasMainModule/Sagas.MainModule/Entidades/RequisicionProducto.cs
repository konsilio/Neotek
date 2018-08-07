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
    
    public partial class RequisicionProducto
    {
        public int IdRequisicion { get; set; }
        public int IdProducto { get; set; }
        public int IdTipoProducto { get; set; }
        public int IdCentroCosto { get; set; }
        public decimal Cantidad { get; set; }
        public string Aplicacion { get; set; }
        public Nullable<bool> RevisionFisica { get; set; }
        public Nullable<decimal> CantidadAlmacenActual { get; set; }
        public Nullable<decimal> CantidadAComprar { get; set; }
        public Nullable<bool> AutorizaEntrega { get; set; }
        public Nullable<bool> AutorizaCompra { get; set; }
    
        public virtual Requisicion Requisicion { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
