/**
 * Clase modelo para las recargas 
 * Programmer: Jorge Omar Tovar Martínez
 * Commpany: Neoteck
 * Date: 13/09/2018
 * Update: 13/09/2018
 */ 
using System;

namespace Application.MainModule.DTOs.Mobile
{
    public class RecargaDTO
    {
        /// <summary>
        /// Id del almacen de recarga
        /// </summary>
        public int IdAlmacenGasRecarga { get; set; }
        /// <summary>
        /// Id del almacen de Gas de salida 
        /// </summary>
        public short IdCAlmacenGasSalida { get; set; }
        /// <summary>
        /// Id del almacen de entrada de Gas
        /// </summary>
        public short IdCAlmacenGasEntrada { get; set; }
        /// <summary>
        /// Id del tipo de medior de salida 
        /// </summary>
        public short IdTipoMedidorSalida { get; set; }
        /// <summary>
        /// Id del tipo de medidor de entrada
        /// </summary>
        public short IdTipoMedidorEntrada { get; set; }
        /// <summary>
        /// Id del tipo de evento (Entrada/Salida)
        /// </summary>
        public byte IdTipoEvento { get; set; }
        /// <summary>
        /// Cantidad marcada por el P5000 de salida
        /// </summary>
        public decimal P5000Salida { get; set; }
        /// <summary>
        /// Cantidad marcada por el P5000 de entrada
        /// </summary>
        public decimal P5000Entrada { get; set; }
        /// <summary>
        /// Clave de la operación 
        /// </summary>
        public string ClaveOperacion { get; set; }
        /// <summary>
        /// Los datos fuero prosesados
        /// </summary>
        public bool DatosProcesados { get; set; }
        /// <summary>
        /// Fecha de registro 
        /// </summary>
        public DateTime FechaRegistro { get; set; }
    }
}
