﻿using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class AlmacenAdapter
    {
        public static AlmacenGasDescarga FromDto(Dto papeletaDto)
        {
            return new AlmacenGasDescarga()
            {
            };
        }

        public static AlmacenGasDescarga FromDto(DescargaDto DescargaDto, bool finDescarga)
        {
            if(!finDescarga)
                return new AlmacenGasDescarga()
                {
                    //IdAlmacenEntradaGasDescarga = inicioDescargaDto,
                    IdAlmacenGas = DescargaDto.IdAlmacen,
                    //IdRequisicion = papeletaDto,
                    //IdOrdenCompraExpedidor = papeletaDto,
                    //IdOrdenCompraPorteador = papeletaDto,
                    //IdProveedorExpedidor = papeletaDto,
                    //IdProveedorPorteador = papeletaDto,
                    //IdCAlmacenGas = papeletaDto,
                    //IdTipoMedidorTractor = papeletaDto,
                    IdTipoMedidorAlmacen = DescargaDto.IdTipoMedidorAlmacen,
                    //PorcenMagnatelOcularTractorFIN = papeletaDto,
                    //PorcenMagnatelOcularAlmacenFIN = papeletaDto,
                    //FechaFinDescarga = papeletaDto,
                    TanquePrestado = DescargaDto.TanquePrestado,
                    PorcenMagnatelOcularTractorINI = DescargaDto.PorcentajeMedidorTractor,
                    PorcenMagnatelOcularAlmacenINI = DescargaDto.PorcentajeMedidorAlmacen,
                    FechaInicioDescarga = DescargaDto.FechaDescarga,
                    //FechaPapeleta = papeletaDto,
                    //FechaEmbarque = papeletaDto,
                    //NumeroEmbarque = papeletaDto,
                    //NombreOperador = papeletaDto,
                    //PlacasTractor = papeletaDto,
                    //NumTanquePG = papeletaDto,
                    //CapacidadTanqueLt = papeletaDto,
                    //CapacidadTanqueKg = papeletaDto,
                    //PorcenMagnatelPapeleta = papeletaDto,
                    //PresionTanque = papeletaDto,
                    //Sello = papeletaDto,
                    //ValorCarga = papeletaDto,
                    //NombreResponsable = papeletaDto,
                    //PorcenMagnatelOcular = papeletaDto,
                    //FechaEntraGas = papeletaDto,
                    //DatosProcesados = false,
                };
            else
                return new AlmacenGasDescarga()
                {
                    //IdAlmacenEntradaGasDescarga = DescargaDto,
                    //IdAlmacenGas = inicioDescargaDto,
                    //IdRequisicion = papeletaDto,
                    //IdOrdenCompraExpedidor = papeletaDto,
                    //IdOrdenCompraPorteador = papeletaDto,
                    //IdProveedorExpedidor = papeletaDto,
                    //IdProveedorPorteador = papeletaDto,
                    //IdCAlmacenGas = papeletaDto,
                    //IdTipoMedidorTractor = papeletaDto,
                    //IdTipoMedidorAlmacen = papeletaDto,
                    PorcenMagnatelOcularTractorFIN = DescargaDto.PorcentajeMedidorTractor,
                    PorcenMagnatelOcularAlmacenFIN = DescargaDto.PorcentajeMedidorAlmacen,
                    FechaFinDescarga = DescargaDto.FechaDescarga,
                    //TanquePrestado = inicioDescargaDto,
                    //PorcenMagnatelOcularTractorINI = inicioDescargaDto,
                    //PorcenMagnatelOcularAlmacenINI = inicioDescargaDto,
                    //FechaInicioDescarga = inicioDescargaDto,
                    //FechaPapeleta = papeletaDto,
                    //FechaEmbarque = papeletaDto,
                    //NumeroEmbarque = papeletaDto,
                    //NombreOperador = papeletaDto,
                    //PlacasTractor = papeletaDto,
                    //NumTanquePG = papeletaDto,
                    //CapacidadTanqueLt = papeletaDto,
                    //CapacidadTanqueKg = papeletaDto,
                    //PorcenMagnatelPapeleta = papeletaDto,
                    //PresionTanque = papeletaDto,
                    //Sello = papeletaDto,
                    //ValorCarga = papeletaDto,
                    //NombreResponsable = papeletaDto,
                    //PorcenMagnatelOcular = papeletaDto,
                    //FechaEntraGas = papeletaDto,
                    //DatosProcesados = false,
                };
        }

        public static AlmacenGasDescarga FromEntity(AlmacenGasDescarga descarga)
        {
            return new AlmacenGasDescarga()
            {
                IdAlmacenEntradaGasDescarga = descarga.IdAlmacenEntradaGasDescarga,
                IdAlmacenGas = descarga.IdAlmacenGas,
                IdRequisicion = descarga.IdRequisicion,
                IdOrdenCompraExpedidor = descarga.IdOrdenCompraExpedidor,
                IdOrdenCompraPorteador = descarga.IdOrdenCompraPorteador,
                IdProveedorExpedidor = descarga.IdProveedorExpedidor,
                IdProveedorPorteador = descarga.IdProveedorPorteador,
                IdCAlmacenGas = descarga.IdCAlmacenGas,
                IdTipoMedidorTractor = descarga.IdTipoMedidorTractor,
                IdTipoMedidorAlmacen = descarga.IdTipoMedidorAlmacen,
                PorcenMagnatelOcularTractorFIN = descarga.PorcenMagnatelOcularTractorFIN,
                PorcenMagnatelOcularAlmacenFIN = descarga.PorcenMagnatelOcularAlmacenFIN,
                FechaFinDescarga = descarga.FechaFinDescarga,
                TanquePrestado = descarga.TanquePrestado,
                PorcenMagnatelOcularTractorINI = descarga.PorcenMagnatelOcularTractorINI,
                PorcenMagnatelOcularAlmacenINI = descarga.PorcenMagnatelOcularAlmacenINI,
                FechaInicioDescarga = descarga.FechaInicioDescarga,
                FechaPapeleta = descarga.FechaPapeleta,
                FechaEmbarque = descarga.FechaEmbarque,
                NumeroEmbarque = descarga.NumeroEmbarque,
                NombreOperador = descarga.NombreOperador,
                PlacasTractor = descarga.PlacasTractor,
                NumTanquePG = descarga.NumTanquePG,
                CapacidadTanqueLt = descarga.CapacidadTanqueLt,
                CapacidadTanqueKg = descarga.CapacidadTanqueKg,
                PorcenMagnatelPapeleta = descarga.PorcenMagnatelPapeleta,
                PresionTanque = descarga.PresionTanque,
                Sello = descarga.Sello,
                ValorCarga = descarga.ValorCarga,
                NombreResponsable = descarga.NombreResponsable,
                PorcenMagnatelOcular = descarga.PorcenMagnatelOcular,
                FechaEntraGas = descarga.FechaEntraGas,
                DatosProcesados = descarga.DatosProcesados,
            };
        }

        public static AlmacenDto ToDto(AlmacenGas alm)
        {
            return new AlmacenDto()
            {
                IdAlmacenGas = alm.IdAlmacenGas,
                NombreAlmacen= "Núm. 1",
            };
        }

        public static List<AlmacenDto> ToDto(List<AlmacenGas> alm)
        {
            return alm.ToList().Select(x => ToDto(x)).ToList();
        }
    }
}
