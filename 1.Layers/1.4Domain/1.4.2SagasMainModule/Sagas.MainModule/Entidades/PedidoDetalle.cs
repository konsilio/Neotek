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
    
    public partial class PedidoDetalle
    {
        public int IdPedidoDetalle { get; set; }
        public int IdPedido { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public Nullable<bool> Cilindro20 { get; set; }
        public Nullable<bool> Cilindro30 { get; set; }
        public Nullable<bool> Cilindro45 { get; set; }
        public Nullable<decimal> TotalKilos { get; set; }
        public Nullable<decimal> TotalLitros { get; set; }
    
        public virtual Pedido Pedido { get; set; }
    }
}
