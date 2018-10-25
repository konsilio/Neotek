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

        public AlmacenGasTomaLectura TomaLecturaLectura { get; set; }
        public List<AlmacenGasTomaLecturaFoto> TomaLecturaLecturaFotos { get; set; }
        public AlmacenGasTomaLectura TomaLecturaLecturaSinNavProp { get; set; }


        //public List<AlmacenGasTomaLectura> TomaLecturasFinales { get; set; }
        //public AlmacenGasTomaLectura TomaLecturaLecturaFinal { get; set; }
        //public List<AlmacenGasTomaLecturaFoto> TomaLecturaLecturaFinalFotos { get; set; }
        //public AlmacenGasTomaLectura TomaLecturaLecturaFinalSinNavProp { get; set; }




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


        public AlmacenGasMovimiento MovimientoUnidad { get; set; }
    }
}
