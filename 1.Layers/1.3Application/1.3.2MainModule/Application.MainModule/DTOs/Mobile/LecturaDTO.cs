/***
 * Clase para la lectura inicial de Estación de carburación,Pipas y tanques 
 * Developer: Jorge Omar Tovar Martínez
 * Commpany: Neoteck
 * Date: 10/09/2018
 * Updated: 11/09/2018
 */
using System;
using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile
{
    public class LecturaDTO
    {        
        public short IdTipoMedidor { get; set; }
        public int CantidadFotografiasMedidor { get; set; }
        public List<String> Imagenes { get; set; }
        public short IdCAlmacenGas { get; set; }
        public int CantidadP5000 { get; set; }
        public decimal PorcentajeMedidor { get; set; }
        public String ClaveProceso { get; set; }
        public DateTime FechaAplicacion { get; set; }
    }
}
