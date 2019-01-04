/**
 *  ConceptoDTO
 *  Concepto de la venta desde mobile, tine los campos
 *  utilizados dentro de este del concepto de la venta 
 *  Developer: Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 *  Company: Neoteck
 *  Date: 29/10/2018 09:56
 *  Update 29/10/2018 09:56
 */

namespace Application.MainModule.DTOs.Mobile
{
    public class ConceptoDTO
    {
        public short IdTipoGas { get; set; }
        public decimal Cantidad { get; set; }
        public string Concepto { get; set; }
        public decimal PUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal { get; set; }
        public short IdCategoria { get; set; }
        public short IdLinea { get; set; }
        public short IdProducto { get; set; }
        public decimal LitrosDespachados { get; set; }
        public short IdUnidadMedida { get; set; }
        public decimal PrecioUnitarioProducto { get; set; }
        public decimal PrecioUnitarioLt { get; set; }
        public decimal PrecioUnitarioKg { get; set; }
        public decimal DescuentoUnitarioLt { get; set; }
        public decimal DescuentoUnitarioKg { get; set; }
        public decimal CantidadLt { get; set; }
        public decimal CantidadKg { get; set; }
        public decimal DescuentoTotal { get; set; }
        #region Permite identificar si en el detalle de la venta se debe descontar un cilindro
        public bool EsVentaCilindro { get; set; }
        public short IdCilindro { get; set; }
        #endregion
    }
}
