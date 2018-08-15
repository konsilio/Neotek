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
        public static AlmacenGasDescarga FromDto(Dto papeletaDto)
        {
            return new AlmacenGasDescarga()
            {
                //IdAlmacenEntradaGasDescarga = papeletaDto,
                IdAlmacenGas = papeletaDto,
                //IdRequisicion = papeletaDto,
                IdOrdenCompraExpedidor = papeletaDto,
                IdOrdenCompraPorteador = papeletaDto,
                IdProveedorExpedidor = papeletaDto,
                IdProveedorPorteador = papeletaDto,
                //IdCAlmacenGas = papeletaDto,
                IdTipoMedidorTractor = papeletaDto,
                IdTipoMedidorAlmacen = papeletaDto,
                //PorcenMagnatelOcularTractorFIN = papeletaDto,
                //PorcenMagnatelOcularAlmacenFIN = papeletaDto,
                //FechaFinDescarga = papeletaDto,
                //TanquePrestado = papeletaDto,
                //PorcenMagnatelOcularTractorINI = papeletaDto,
                //PorcenMagnatelOcularAlmacenINI = papeletaDto,
                //FechaInicioDescarga = papeletaDto,
                FechaPapeleta = papeletaDto,
                FechaEmbarque = papeletaDto,
                NumeroEmbarque = papeletaDto,
                NombreOperador = papeletaDto,
                PlacasTractor = papeletaDto,
                NumTanquePG = papeletaDto,
                CapacidadTanqueLt = papeletaDto,
                CapacidadTanqueKg = papeletaDto,
                PorcenMagnatelPapeleta = papeletaDto,
                PresionTanque = papeletaDto,
                Sello = papeletaDto,
                ValorCarga = papeletaDto,
                NombreResponsable = papeletaDto,
                PorcenMagnatelOcular = papeletaDto,
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
