using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class AplicaTomaLecturaDto
    {
        public Empresa Empresa { get; set; }

        public AlmacenGasTomaLectura TomaLecturaLecturaInicial { get; set; }
        public List<AlmacenGasTomaLecturaFoto> TomaLecturaLecturaInicialFotos { get; set; }
        public AlmacenGasTomaLectura TomaLecturaLecturaInicialSinNavProp { get; set; }


        public List<AlmacenGasTomaLectura> TomaLecturasFinales { get; set; }
        public AlmacenGasTomaLectura TomaLecturaLecturaFinal { get; set; }
        public List<AlmacenGasTomaLecturaFoto> TomaLecturaLecturaFinalFotos { get; set; }
        public AlmacenGasTomaLectura TomaLecturaLecturaFinalSinNavProp { get; set; }




        public AlmacenGas AlmacenGas { get; set; }




        public UnidadAlmacenGas unidadAlmacenGasPrincipal { get; set; }
        public UnidadAlmacenGas unidadAlmacenGas { get; set; }
        public identidadUnidadAlmacenGas identidadUA { get; set; }
        public decimal P5000UA { get; set; }
        public decimal PorcentajeUA { get; set; }


        public List<CamionetaCilindro> CilindrosEnCamionetaInsertar { get; set; }
        public List<CamionetaCilindro> CilindrosEnCamionetaModificar { get; set; }
        public List<CamionetaCilindro> CilindrosEnCamionetaEliminar { get; set; }


        public string Concepto { get; set; }// Revisar si puede ser catálogo de apoyo


        public decimal CantidadSINRemanenteKg { get; set; }
        public decimal CantidadSINRemanenteLt { get; set; }
        public decimal RemanenteKg { get; set; }
        public decimal RemanenteLt { get; set; }
        public decimal CantidadCONRemanenteKg { get; set; }
        public decimal CantidadCONRemanenteLt { get; set; }
    }
}
