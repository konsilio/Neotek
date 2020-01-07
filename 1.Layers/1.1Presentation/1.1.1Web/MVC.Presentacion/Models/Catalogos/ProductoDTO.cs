using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    [Serializable]
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public short IdEmpresa { get; set; }
        public short IdProductoServicioTipo { get; set; }
        public string TipoProducto { get; set; }
        public short IdCategoria { get; set; }
        public string Categoria { get; set; }
        public int IdCuentaContable { get; set; }
        public short IdProductoLinea { get; set; }
        public string ProductoLinea { get; set; }
        public short IdUnidadMedida { get; set; }
        public string UnidadMedida { get; set; }
        public short? IdUnidadMedida2 { get; set; }
        public string Descripcion { get; set; }
        public decimal Minimos { get; set; }
        public decimal Maximo { get; set; }
        public string UrlImagen { get; set; }
        public string PathImagen { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public bool EsActivoVenta { get; set; }
        public bool EsGas { get; set; }
        public bool EsTransporteGas { get; set; }
    }
}