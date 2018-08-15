using Application.MainModule.DTOs.Mobile;
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
        public static AlmacenGasDescarga FromDto(PapeletaDTO papeletaDto)
        {
            return new AlmacenGasDescarga()
            {
                //IdAlmacenEntradaGasDescarga = papeletaDto,
                //IdAlmacenGas = papeletaDto,
                //IdRequisicion = papeletaDto,
                IdOrdenCompraExpedidor = papeletaDto.IdOrdenCompraExpedidor,
                IdOrdenCompraPorteador = papeletaDto.IdOrdenCompraPorteador,
                IdProveedorExpedidor = papeletaDto.IdProveedorExpedidor,
                IdProveedorPorteador = papeletaDto.IdProveedorPorteador,
                //IdCAlmacenGas = papeletaDto,
                IdTipoMedidorTractor = papeletaDto.IdTipoMedidorTractor,
                //IdTipoMedidorAlmacen = papeletaDto,
                //PorcenMagnatelOcularTractorFIN = papeletaDto,
                //PorcenMagnatelOcularAlmacenFIN = papeletaDto,
                //FechaFinDescarga = papeletaDto,
                //TanquePrestado = papeletaDto,
                //PorcenMagnatelOcularTractorINI = papeletaDto,
                //PorcenMagnatelOcularAlmacenINI = papeletaDto,
                //FechaInicioDescarga = papeletaDto,
                FechaPapeleta = papeletaDto.Fecha,
                FechaEmbarque = papeletaDto.FechaEmbarque,
                NumeroEmbarque = papeletaDto.NumeroEmbarque,
                NombreOperador = papeletaDto.NombreOperador,
                PlacasTractor = papeletaDto.PlacasTractor,
                NumTanquePG = papeletaDto.NumeroTanque,
                CapacidadTanqueLt = papeletaDto.CapacidadTanque,
                //CapacidadTanqueKg = papeletaDto,
                PorcenMagnatelPapeleta = papeletaDto.PorcentajeTanque,
                PresionTanque = papeletaDto.PresionTanque,
                Sello = papeletaDto.Sello,
                ValorCarga = papeletaDto.ValorCarga,
                NombreResponsable = papeletaDto.NombreResponsable,
                PorcenMagnatelOcular = papeletaDto.PorcentajeMedidor,
                //FechaEntraGas = papeletaDto,
                //DatosProcesados = false,
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
