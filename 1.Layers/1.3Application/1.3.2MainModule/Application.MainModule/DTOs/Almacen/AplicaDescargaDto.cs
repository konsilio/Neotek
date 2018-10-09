using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class AplicaDescargaDto
    {
        public AlmacenGasDescarga Descarga { get; set; }
        public AlmacenGasDescarga DescargaSinNavigationProperties { get; set; }
        public List<AlmacenGasDescargaFoto> DescargaFotos { get; set; }


        public OrdenCompra OCExpedidor { get; set; }
        public OrdenCompra OCPorteador { get; set; }



        public AlmacenGas AlmacenGas { get; set; }



        public UnidadAlmacenGas unidadEntrada { get; set; }
        public identidadUnidadAlmacenGas identidadUE { get; set; }
        public decimal P5000UE { get; set; }
        public decimal PorcentajeUE { get; set; }


        public string Concepto { get; set; }// Revisar si puede ser catálogo de apoyo


        public AlmacenGasMovimiento Movimiento { get; set; }

        public decimal CantidadSINRemanenteKg { get; set; }
        public decimal CantidadSINRemanenteLt { get; set; }
        public decimal RemanenteKg { get; set; }
        public decimal RemanenteLt { get; set; }
        public decimal CantidadCONRemanenteKg { get; set; }
        public decimal CantidadCONRemanenteLt { get; set; }



        public UnidadAlmacenGas unidadSalida { get; set; }
        public identidadUnidadAlmacenGas identidadUS { get; set; }        
        public decimal P5000US { get; set; }
        public decimal PorcentajeUS { get; set; }
    }
}
