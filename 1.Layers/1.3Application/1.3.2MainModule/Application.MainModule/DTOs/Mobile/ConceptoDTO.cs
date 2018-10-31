/**
 *  ConceptoDTO
 *  Concepto de la venta desde mobile, tine los campos
 *  utilizados dentro de este del concepto de la venta 
 *  Developer: Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 *  Company: Neoteck
 *  Date: 29/10/2018 09:56
 *  Update 29/10/2018 09:56
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class ConceptoDTO
    {
        public short IdTipoGas { get; set; }
        public int Cantidad { get; set; }
        public string Concepto { get; set; }
        public decimal PUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal { get; set; }
        public short IdCategoria { get; set; }
        public short IdLinea { get; set; }
        public short IdProducto { get; set; }

        public decimal LitrosDespachados { get; set; }
    }
}
