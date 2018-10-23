using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class AplicaAutoConsumoDto
    {
        public Empresa Empresa { get; set; }

        public AlmacenGasAutoConsumo AutoConsumoLecturaInicial { get; set; }
        public List<AlmacenGasAutoConsumoFoto> AutoConsumoLecturaInicialFotos { get; set; }
        public AlmacenGasAutoConsumo AutoConsumoLecturaInicialSinNavProp { get; set; }


        public List<AlmacenGasAutoConsumo> AutoConsumosFinales { get; set; }
        public AlmacenGasAutoConsumo AutoConsumoLecturaFinal { get; set; }
        public List<AlmacenGasAutoConsumoFoto> AutoConsumoLecturaFinalFotos { get; set; }
        public AlmacenGasAutoConsumo AutoConsumoLecturaFinalSinNavProp { get; set; }




        public AlmacenGas AlmacenGas { get; set; }
        public AlmacenGas AlmacenGasAnterior { get; set; }


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

                
        public AlmacenGasMovimiento MovimientoSalida { get; set; }
    }
}
