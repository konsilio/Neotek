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
        public static ComplementoGasDTO ToDTO(AlmacenGasDescarga descarga)
        {
            return new ComplementoGasDTO()
            {
                FechaEmbarque = descarga.FechaEmbarque.Value,
                NumeroEmbarque = descarga.NumeroEmbarque,
                ValorCarga = descarga.ValorCarga.Value,
                Sello = descarga.Sello,
                NombreResponsable = descarga.NombreResponsable,
                PorcentajeTanque = descarga.PorcenMagnatelPapeleta.Value,
                //Tractor
                PlacasTractor = descarga.PlacasTractor,
                NombreOperador = descarga.NombreOperador,
                PresionTanque = descarga.PresionTanque.Value,
                NumeroTanque = descarga.NumTanquePG,
                CapacidadTanque = descarga.CapacidadTanqueLt.Value,
                PorcentajeMedidor = descarga.PorcenMagnatelOcular.Value,
                //Descarga
                FechaEntraGas = descarga.FechaPapeleta.Value,
                PorcenMagnatelOcularTractorINI = descarga.PorcenMagnatelOcularTractorINI ?? 0,
                PorcenMagnatelOcularTractorFIN = descarga.PorcenMagnatelOcularTractorFIN??0,
                PorcenMagnatelOcularAlmacenINI = descarga.PorcenMagnatelOcularAlmacenINI??0,
                PorcenMagnatelOcularAlmacenFIN = descarga.PorcenMagnatelOcularAlmacenFIN??0,
                TanquePrestado = descarga.TanquePrestado??false,
                KilosPapeleta = descarga.MasaKg??0,
                Productos = new List<ProductoOCDTO>()
            };
        }
        public static ComplementoGasDTO ToRequisicion(ComplementoGasDTO dto, Sagas.MainModule.Entidades.Requisicion requ)
        {
            dto.IdRequisicion = requ.IdRequisicion;
            dto.NumeroRequisicion = requ.NumeroRequisicion;
            dto.FechaRequerida = requ.FechaRequerida;
            dto.IdUsuarioSolicitante = requ.IdUsuarioSolicitante;
            dto.UsuarioSolicitante = requ.Solicitante.Nombre;
            dto.MotivoRequisicion = requ.MotivoRequisicion;
            dto.RequeridoEn = requ.RequeridoEn;
            dto.NombreComercial = requ.Empresa.NombreComercial;
            dto.FactorLitrosAKilos = requ.Empresa.FactorLitrosAKilos;
            return dto;
        }
    }
}
