/***
 * Clase para la lectura inicial de la camioneta
 * Developer: Jorge Omar Tovar Martínez
 * Commpany: Neoteck
 * Date: 11/09/2018
 * Updated: 11/09/2018
 */
using System;
using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile
{
    public class LecturaCamionetaDTO
    {
        /// <summary>
        /// Clave de operación
        /// </summary>
        public String ClaveProceso { get; set; }

        /// <summary>
        /// Id de IdCAlmacenGas (Id de la camioneta)
        /// </summary>
        public short IdCAlmacenGas { get; set; }

        /// <summary>
        /// Determina si es el encargado de la puerta o
        /// Encargado del Andén
        /// </summary>
        public bool EsEncargadoPuerta { get; set; }
        /// <summary>
        /// Contiene las cantidades de los 
        /// cilindros 
        /// </summary>
        public List<decimal> CilindroCantidad { get;set;}
        /// <summary>
        /// Contiene los ids de los cilindros
        /// </summary>
        public List<short> IdCilindro { get; set; }
    }
}
