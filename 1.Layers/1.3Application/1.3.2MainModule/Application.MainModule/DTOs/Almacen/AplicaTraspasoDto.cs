using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class AplicaTraspasoDto
    {
        public Empresa Empresa { get; set; }


        public AlmacenGasTraspaso TraspasoLecturaInicial { get; set; }        
        public List<AlmacenGasTraspasoFoto> TraspasoLecturaInicialFotos { get; set; }
        public AlmacenGasTraspaso TraspasoLecturaInicialSinNavProp { get; set; }


        public List<AlmacenGasTraspaso> TraspasosFinales { get; set; }
        public AlmacenGasTraspaso TraspasoLecturaFinal { get; set; }        
        public List<AlmacenGasTraspasoFoto> TraspasoLecturaFinalFotos { get; set; }
        public AlmacenGasTraspaso TraspasoLecturaFinalSinNavProp { get; set; }




        public AlmacenGas AlmacenGas { get; set; }


        public UnidadAlmacenGas unidadSalida { get; set; }
        public identidadUnidadAlmacenGas identidadUS { get; set; }
        public decimal P5000US { get; set; }
        public decimal PorcentajeUS { get; set; }


        public UnidadAlmacenGas unidadEntrada { get; set; }
        public List<CamionetaCilindro> CilindrosEnCamionetaInsertar { get; set; }
        public List<CamionetaCilindro> CilindrosEnCamionetaModificar { get; set; }
        public List<CamionetaCilindro> CilindrosEnCamionetaEliminar { get; set; }

        public identidadUnidadAlmacenGas identidadUE { get; set; }
        public decimal P5000UE { get; set; }
        public decimal PorcentajeUE { get; set; }


        public string Concepto { get; set; }// Revisar si puede ser catálogo de apoyo


        public decimal CantidadSINRemanenteKg { get; set; }
        public decimal CantidadSINRemanenteLt { get; set; }
        public decimal RemanenteKg { get; set; }
        public decimal RemanenteLt { get; set; }
        public decimal CantidadCONRemanenteKg { get; set; }
        public decimal CantidadCONRemanenteLt { get; set; }
    }
}
