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
    
    public partial class OrdenCompraProducto
    {
        public int IdOrdenCompra { get; set; }
        public string ProductoServicioTipo { get; set; }
        public string Producto { get; set; }
        public string Categoria { get; set; }
        public string Linea { get; set; }
        public string UnidadMedida { get; set; }
        public string UnidadMedida2 { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal IVA { get; set; }
        public decimal IEPS { get; set; }
        public decimal Importe { get; set; }
    
        public virtual OrdenCompra OrdenCompra { get; set; }
    }
}
