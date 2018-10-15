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
    
    public partial class AlmacenSalidaProducto
    {
        public int IdSalida { get; set; }
        public Nullable<int> IdRequisicion { get; set; }
        public byte Orden { get; set; }
        public int IdAlmacen { get; set; }
        public int IdProduto { get; set; }
        public int IdUsuarioEntrega { get; set; }
        public int IdUsuarioRecibe { get; set; }
        public decimal Cantidad { get; set; }
        public string UrlDocSalida { get; set; }
        public string PathDocSalida { get; set; }
        public string Observaciones_ { get; set; }
        public System.DateTime FechaEntrada { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    
        public virtual Almacen Almacen { get; set; }
        public virtual Producto Productos { get; set; }
        public virtual Requisicion Requisiciones { get; set; }
        public virtual Usuario UsuarioEntrega { get; set; }
        public virtual Usuario UsuarioRecibe { get; set; }
    }
}
