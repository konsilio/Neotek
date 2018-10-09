/***
 * ReporteDiaDto
 * Clase dto para generar el reporte del 
 * día para las lecturas
 * Developer: Jorge Omar Tovar Martínez
 * Company: Neoteck
 * Date: 03/10/2018
 * Update: 03/10/2018
 */
using System;
using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile
{
    public class ReporteDiaDTO
    {
        public int IdCamioneta { get; set; }
        public string NombreCAlmacen { get; set; }
        public MedidorDto Medidor { get; set; }
        public string ClaveReporte { get; set; }
        public DateTime Fecha { get; set; }
        public LecturaAlmacenDto LecturaInicial { get; set; }
        public LecturaAlmacenDto LecturaFinal { get; set; }
        public decimal LitrosVenta { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteCredito { get; set; }
        public List<TanquesDto> Tanques { get; set; }
        public List<OtrasVentasDto> OtrasVentas { get; set; }
        public decimal Carburacion { get; set; }
        public decimal KilosDeVenta { get; set; }
        public decimal OtrasVentasTotal { get; set; }

    }
}
