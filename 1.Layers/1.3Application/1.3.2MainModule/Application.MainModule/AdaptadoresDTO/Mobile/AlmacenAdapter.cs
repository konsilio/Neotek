using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
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
                ClaveOperacion = "Pendiente",
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
                FechaRegistro = DateTime.Now,
                Fotos = FromDto(papeletaDto.Imagenes, 1)
            };
        }

        public static AlmacenGasDescarga FromDto(DescargaDto DescargaDto, bool finDescarga)
        {
            if(!finDescarga)
                return new AlmacenGasDescarga()
                {
                    ClaveOperacion = "Pendiente",
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
                    ClaveOperacion = "Pendiente",
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
                FechaRegistro = descarga.FechaRegistro,
            };
        }

        public static AlmacenGasDescargaFoto FromDto(string cadenaBase64, short numOrden)
        {
            return new AlmacenGasDescargaFoto()
            {
                CadenaBase64 = cadenaBase64,
                Orden = numOrden,
                IdImagenDe = 1
            };
        }

        public static List<AlmacenGasDescargaFoto> FromDto(List<string> lista, short numOrden)
        {
            var fotos = new List<AlmacenGasDescargaFoto>();
            foreach (var cadena in lista)
            {
                fotos.Add(FromDto(cadena, numOrden));
                numOrden++;
            }

            return fotos;
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
