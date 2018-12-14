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
                IdOrdenCompraExpedidor = descarga.IdOrdenCompraExpedidor ?? 0,
                IdOrdenCompraPorteador = descarga.IdOrdenCompraPorteador ?? 0,
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
                PorcenMagnatelOcularTractorFIN = descarga.PorcenMagnatelOcularTractorFIN ?? 0,
                PorcenMagnatelOcularAlmacenINI = descarga.PorcenMagnatelOcularAlmacenINI ?? 0,
                PorcenMagnatelOcularAlmacenFIN = descarga.PorcenMagnatelOcularAlmacenFIN ?? 0,
                TanquePrestado = descarga.TanquePrestado ?? false,
                KilosPapeleta = descarga.MasaKg ?? 0,
                Productos = new List<ProductoOCDTO>()
            };
        }
        public static ComplementoGasDTO ToPapeleta(ComplementoGasDTO descarga)
        {
            return new ComplementoGasDTO()
            {
                //Papeleta
                Fecha = descarga.Fecha,
                FechaEmbarque = descarga.FechaEmbarque,
                NumeroEmbarque = descarga.NumeroEmbarque,
                ValorCarga = descarga.ValorCarga,
                Sello = descarga.Sello,
                NombreResponsable = descarga.NombreResponsable,
                PorcentajeTanque = descarga.PorcentajeTanque,
                //Tractor
                PlacasTractor = descarga.PlacasTractor,
                NombreOperador = descarga.NombreOperador,
                PresionTanque = descarga.PresionTanque,
                NumeroTanque = descarga.NumeroTanque,
                CapacidadTanque = descarga.CapacidadTanque,
                PorcentajeMedidor = descarga.PorcentajeMedidor,
                //Descarga
                FechaEntraGas = descarga.FechaEntraGas,
                PorcenMagnatelOcularTractorINI = descarga.PorcenMagnatelOcularTractorINI,
                PorcenMagnatelOcularTractorFIN = descarga.PorcenMagnatelOcularTractorFIN,
                PorcenMagnatelOcularAlmacenINI = descarga.PorcenMagnatelOcularAlmacenINI,
                PorcenMagnatelOcularAlmacenFIN = descarga.PorcenMagnatelOcularAlmacenFIN,
                //Kilos
                KilosPapeleta = descarga.KilosPapeleta,
                KilosDescargados = descarga.KilosDescargados,
                KilosDiferencia = descarga.KilosDiferencia,
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
        public static AlmacenGasDescarga FromEntity(ComplementoGasDTO descarga)
        {
            return new AlmacenGasDescarga
            {
                FechaPapeleta = descarga.Fecha,
                FechaEmbarque = descarga.FechaEmbarque,
                NumeroEmbarque = descarga.NumeroEmbarque,
                ValorCarga = descarga.ValorCarga,
                Sello = descarga.Sello,
                NombreResponsable = descarga.NombreResponsable,
                PorcenMagnatelPapeleta = descarga.PorcentajeTanque,
                ClaveOperacion = descarga.ClaveOperacion,
                //Tractor
                PlacasTractor = descarga.PlacasTractor,
                NombreOperador = descarga.NombreOperador,
                PresionTanque = descarga.PresionTanque,
                NumTanquePG = descarga.NumeroTanque,
                CapacidadTanqueLt = descarga.CapacidadTanque,
                PorcenMagnatelOcular = descarga.PorcentajeMedidor,
                //Descarga
                FechaEntraGas = descarga.FechaEntraGas,
                PorcenMagnatelOcularTractorINI = descarga.PorcenMagnatelOcularTractorINI,
                PorcenMagnatelOcularTractorFIN = descarga.PorcenMagnatelOcularTractorFIN,
                PorcenMagnatelOcularAlmacenINI = descarga.PorcenMagnatelOcularAlmacenINI,
                PorcenMagnatelOcularAlmacenFIN = descarga.PorcenMagnatelOcularAlmacenFIN,
                TanquePrestado = descarga.TanquePrestado,
                ////Kilos
                MasaKg = descarga.KilosPapeleta,
                //kilosdescargados = descarga.kilosdescargados,
                //kilosdiferencia = descarga.kilosdiferencia,
            };
        }
        public static AlmacenGasDescarga FromEntity(AlmacenGasDescarga descarga)
        {
            return new AlmacenGasDescarga
            {
                IdAlmacenEntradaGasDescarga = descarga.IdAlmacenEntradaGasDescarga,
                FechaRegistro = descarga.FechaRegistro,
                FechaPapeleta = descarga.FechaPapeleta,
                FechaEmbarque = descarga.FechaEmbarque,
                NumeroEmbarque = descarga.NumeroEmbarque,
                ValorCarga = descarga.ValorCarga,
                Sello = descarga.Sello,
                NombreResponsable = descarga.NombreResponsable,
                PorcenMagnatelPapeleta = descarga.PorcenMagnatelPapeleta,
                ClaveOperacion = descarga.ClaveOperacion,
                //Tractor
                PlacasTractor = descarga.PlacasTractor,
                NombreOperador = descarga.NombreOperador,
                PresionTanque = descarga.PresionTanque,
                NumTanquePG = descarga.NumTanquePG,
                CapacidadTanqueLt = descarga.CapacidadTanqueLt,
                PorcenMagnatelOcular = descarga.PorcenMagnatelOcular,
                //Descarga
                FechaEntraGas = descarga.FechaEntraGas,
                PorcenMagnatelOcularTractorINI = descarga.PorcenMagnatelOcularTractorINI,
                PorcenMagnatelOcularTractorFIN = descarga.PorcenMagnatelOcularTractorFIN,
                PorcenMagnatelOcularAlmacenINI = descarga.PorcenMagnatelOcularAlmacenINI,
                PorcenMagnatelOcularAlmacenFIN = descarga.PorcenMagnatelOcularAlmacenFIN,
                TanquePrestado = descarga.TanquePrestado,
                ////Kilos
                MasaKg = descarga.MasaKg,
                //Fotos = descarga.Fotos
                //kilosdescargados = descarga.kilosdescargados,
                //kilosdiferencia = descarga.kilosdiferencia,
            };
        }
    }

}
