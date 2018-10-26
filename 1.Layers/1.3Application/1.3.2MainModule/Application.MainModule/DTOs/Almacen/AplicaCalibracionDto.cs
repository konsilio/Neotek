using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class AplicaCalibracionDto
    {
        public Empresa Empresa { get; set; }

        public AlmacenGasCalibracion CalibracionLecturaInicial { get; set; }
        public List<AlmacenGasCalibracionFoto> CalibracionLecturaInicialFotos { get; set; }
        public AlmacenGasCalibracion CalibracionLecturaInicialSinNavProp { get; set; }


        public List<AlmacenGasCalibracion> CalibracionsFinales { get; set; }
        public AlmacenGasCalibracion CalibracionLecturaFinal { get; set; }
        public List<AlmacenGasCalibracionFoto> CalibracionLecturaFinalFotos { get; set; }
        public AlmacenGasCalibracion CalibracionLecturaFinalSinNavProp { get; set; }




        public AlmacenGas AlmacenGas { get; set; }
        public AlmacenGas AlmacenGasAnterior { get; set; }




        public UnidadAlmacenGas unidadAlmacenGasPrincipal { get; set; }
        public UnidadAlmacenGas unidadAlmacenGas { get; set; }
        public identidadUnidadAlmacenGas identidadUA { get; set; }
        public decimal P5000UA { get; set; }
        public decimal PorcentajeUA { get; set; }


        public List<CamionetaCilindro> CilindrosEnCamionetaInsertar { get; set; }
        public List<CamionetaCilindro> CilindrosEnCamionetaModificar { get; set; }
        public List<CamionetaCilindro> CilindrosEnCamionetaEliminar { get; set; }

        
        public string Concepto { get; set; }// Revisar si puede ser catálogo de apoyo


        public AlmacenGasMovimiento MovimientoEntrada { get; set; }
        public AlmacenGasMovimiento MovimientoSalida { get; set; }
    }
}
