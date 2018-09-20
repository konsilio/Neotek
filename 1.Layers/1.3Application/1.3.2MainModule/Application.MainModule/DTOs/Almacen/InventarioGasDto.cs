using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class InventarioGasDto
    {
        public UnidadAlmacenGas unidadEntrada { get; set; }
        public identidadUnidadAlmacenGas identidadUE { get; set; }
        public decimal P5000UE { get; set; }
        public decimal PorcentajeUE { get; set; }



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
