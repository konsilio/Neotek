using Application.MainModule.DTOs.Compras;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Compras
{
    public static class ComplementoGasAdapter
    {
        public static ComplementoGasDTO ToDTO(AlmacenGasDescarga almacen)
        {
            return new ComplementoGasDTO()
            {
                FechaEmbarque = almacen.FechaEmbarque.Value,
                NumeroEmbarque = almacen.NumeroEmbarque,
                ValorCarga = almacen.ValorCarga.Value,
                Sello = almacen.Sello,
                NombreResponsable = almacen.NombreResponsable,
                PorcentajeTanque = almacen.PorcenMagnatelPapeleta.Value,
                //Tractor
                PlacasTractor = almacen.PlacasTractor,
                NombreOperador = almacen.NombreOperador,
                PresionTanque = almacen.PresionTanque.Value,
                NumeroTanque = almacen.NumTanquePG,
                CapacidadTanque = almacen.CapacidadTanqueLt.Value,
                PorcentajeMedidor = almacen.PorcenMagnatelOcular.Value,
                //Descarga
                FechaEntraGas = almacen.FechaPapeleta.Value,
                PorcenMagnatelOcularTractorINI = almacen.PorcenMagnatelOcularTractorINI.Value,
                PorcenMagnatelOcularTractorFIN = almacen.PorcenMagnatelOcularTractorFIN.Value,
                PorcenMagnatelOcularAlmacenINI = almacen.PorcenMagnatelOcularAlmacenINI.Value,
                PorcenMagnatelOcularAlmacenFIN = almacen.PorcenMagnatelOcularAlmacenFIN.Value,
                TanquePrestado = almacen.TanquePrestado.Value,
            };
        }
    }
}
