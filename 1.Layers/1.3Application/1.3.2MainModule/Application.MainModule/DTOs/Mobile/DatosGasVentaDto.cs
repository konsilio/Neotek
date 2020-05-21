
using System;
/**
*  DatosGasVentaDto
*  Permite que se implemente en el listado de 
*  venta de gas o cilindros en el apartado de 
*  punto de venta de la aplicación mobile 
*  Developer: Jorge Omar Tovar Martrínez
*  Company: Neoteck
*  Date: 30/10/2018
*  Update 30/10/2018
*/
namespace Application.MainModule.DTOs.Mobile
{
    public class DatosGasVentaDto
    {
        public int Id { get; set; }
        public decimal Existencia { get; set; }
        public decimal Descuento { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioVigente { get; set; }
        public Nullable<decimal> CapacidadLt{ get; set; }
        public Nullable<decimal> CapacidadKg { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
    }
}
