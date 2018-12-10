/*
 *PuntoVentaAsignadoDTO
 * Dto para la extración de los datos de la session para
 * el punto de venta asignado 
 * @developer Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 * @date 10/12/2018 
 * @update 10/12/2018
 * @company Neoteck
 */

namespace Application.MainModule.DTOs.Mobile.PuntoVenta
{
    public class PuntoVentaAsignadoDTO
    {
        /// <summary>
        /// Id de la pipa , camioneta o estacion de carburacion 
        /// </summary>
        public short IdEstacion { get; set; }
        /// <summary>
        /// Id de su respectivo IdCAlmacenGas
        /// </summary>
        public short IdCAlmacen { get; set; }
        /// <summary>
        /// Id del punto de venta 
        /// </summary>
        public short IdPuntoVenta { get; set; }
        /// <summary>
        /// Nombre del punto de venta asignado Ej. Pipa No. 1, Isla, Camioneta No. 1
        /// </summary>
        public string  NombrePuntoVenta { get; set; }
        /// <summary>
        /// Nombre del operador asignado 
        /// </summary>
        public string NombreOperador { get; set; }
        /// <summary>
        /// Id de Operador o Chofer del usuario
        /// </summary>
        public short IdOperadorChofer { get; set; }
        /// <summary>
        /// Id de usuario 
        /// </summary>
        public short IdUusario { get; set; }
    }
}
