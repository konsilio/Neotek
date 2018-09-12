using Application.MainModule.DTOs.Mobile;
using Application.MainModule.Servicios.Almacen;
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
                ClaveOperacion = papeletaDto.ClaveOperacion,
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
                Fotos = FromDto(papeletaDto.Imagenes, 0, 1)
            };
        }
        public static AlmacenGasDescarga FromDto(DescargaDto DescargaDto, AlmacenGasDescarga descarga, bool finDescarga)
        {
            if(!finDescarga)
            {
                descarga.ClaveOperacion = DescargaDto.ClaveOperacion;
                //descarga.IdAlmacenEntradaGasDescarga = inicioDescargaDto;
                //descarga.IdAlmacenGas = DescargaDto.IdAlmacen;
                //descarga.IdRequisicion = papeletaDto;
                //descarga.IdOrdenCompraExpedidor = papeletaDto;
                //descarga.IdOrdenCompraPorteador = papeletaDto;
                //descarga.IdProveedorExpedidor = papeletaDto;
                //descarga.IdProveedorPorteador = papeletaDto;
                descarga.IdCAlmacenGas = DescargaDto.IdAlmacen;
                //descarga.IdTipoMedidorTractor = papeletaDto;
                descarga.IdTipoMedidorAlmacen = DescargaDto.IdTipoMedidorAlmacen;
                //descarga.PorcenMagnatelOcularTractorFIN = papeletaDto;
                //descarga.PorcenMagnatelOcularAlmacenFIN = papeletaDto;
                //descarga.FechaFinDescarga = papeletaDto;
                descarga.TanquePrestado = DescargaDto.TanquePrestado;
                descarga.PorcenMagnatelOcularTractorINI = DescargaDto.PorcentajeMedidorTractor;
                descarga.PorcenMagnatelOcularAlmacenINI = DescargaDto.PorcentajeMedidorAlmacen;
                descarga.FechaInicioDescarga = DescargaDto.FechaDescarga;
                //descarga.FechaPapeleta = papeletaDto;
                //descarga.FechaEmbarque = papeletaDto;
                //descarga.NumeroEmbarque = papeletaDto;
                //descarga.NombreOperador = papeletaDto;
                //descarga.PlacasTractor = papeletaDto;
                //descarga.NumTanquePG = papeletaDto;
                //descarga.CapacidadTanqueLt = papeletaDto;
                //descarga.CapacidadTanqueKg = papeletaDto;
                //descarga.PorcenMagnatelPapeleta = papeletaDto;
                //descarga.PresionTanque = papeletaDto;
                //descarga.Sello = papeletaDto;
                //descarga.ValorCarga = papeletaDto;
                //descarga.NombreResponsable = papeletaDto;
                //descarga.PorcenMagnatelOcular = papeletaDto;
                //descarga.FechaEntraGas = papeletaDto;
                //descarga.DatosProcesados = false;
            }
            else
            {
                descarga.ClaveOperacion = DescargaDto.ClaveOperacion;
                //descarga.IdAlmacenEntradaGasDescarga = DescargaDto;
                //descarga.IdAlmacenGas = inicioDescargaDto;
                //descarga.IdRequisicion = papeletaDto;
                //descarga.IdOrdenCompraExpedidor = papeletaDto;
                //descarga.IdOrdenCompraPorteador = papeletaDto;
                //descarga.IdProveedorExpedidor = papeletaDto;
                //descarga.IdProveedorPorteador = papeletaDto;
                //descarga.IdCAlmacenGas = papeletaDto;
                //descarga.IdTipoMedidorTractor = papeletaDto;
                //descarga.IdTipoMedidorAlmacen = papeletaDto;
                descarga.PorcenMagnatelOcularTractorFIN = DescargaDto.PorcentajeMedidorTractor;
                descarga.PorcenMagnatelOcularAlmacenFIN = DescargaDto.PorcentajeMedidorAlmacen;
                descarga.FechaFinDescarga = DescargaDto.FechaDescarga;
                //descarga.TanquePrestado = inicioDescargaDto;
                //descarga.PorcenMagnatelOcularTractorINI = inicioDescargaDto;
                //descarga.PorcenMagnatelOcularAlmacenINI = inicioDescargaDto;
                //descarga.FechaInicioDescarga = inicioDescargaDto;
                //descarga.FechaPapeleta = papeletaDto;
                //descarga.FechaEmbarque = papeletaDto;
                //descarga.NumeroEmbarque = papeletaDto;
                //descarga.NombreOperador = papeletaDto;
                //descarga.PlacasTractor = papeletaDto;
                //descarga.NumTanquePG = papeletaDto;
                //descarga.CapacidadTanqueLt = papeletaDto;
                //descarga.CapacidadTanqueKg = papeletaDto;
                //descarga.PorcenMagnatelPapeleta = papeletaDto;
                //descarga.PresionTanque = papeletaDto;
                //descarga.Sello = papeletaDto;
                //descarga.ValorCarga = papeletaDto;
                //descarga.NombreResponsable = papeletaDto;
                //descarga.PorcenMagnatelOcular = papeletaDto;
                //descarga.FechaEntraGas = papeletaDto;
                //descarga.DatosProcesados = false;
            }

            return descarga;
        }
        public static AlmacenGasDescarga FromEntity(AlmacenGasDescarga descarga)
        {
            return new AlmacenGasDescarga()
            {
                ClaveOperacion = descarga.ClaveOperacion,
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
        public static AlmacenGasDescargaFoto FromDto(string cadenaBase64, int idAlmEntrGasDes, short numOrden)
        {
            var resp = new AlmacenGasDescargaFoto()
            {                
                CadenaBase64 = cadenaBase64,
                Orden = numOrden,
                IdImagenDe = 1
            };

            if (idAlmEntrGasDes > 0)
                resp.IdAlmacenEntradaGasDescarga = idAlmEntrGasDes;

            return resp;
        }
        public static List<AlmacenGasDescargaFoto> FromDto(List<string> lista, int idAlmEntrGasDes, short numOrden)
        {
            var fotos = new List<AlmacenGasDescargaFoto>();
            foreach (var cadena in lista)
            {
                fotos.Add(FromDto(cadena, idAlmEntrGasDes, numOrden));
                numOrden++;
            }

            return fotos;
        }
        public static AlmacenDto ToDto(UnidadAlmacenGas alm)
        {
            return new AlmacenDto()
            {
                IdAlmacenGas = alm.IdCAlmacenGas,
                IdTipoMedidor = alm.IdTipoMedidor,
                NombreAlmacen= AlmacenGasServicio.ObtenerNombreUnidadAlmacenGas(alm),
                CantidadP5000 = alm.P5000Actual,
                PorcentajeMedidor = alm.PorcentajeActual,
                Cilindros = alm.Camioneta != null ?  : null,
            };
        }
        public static List<AlmacenDto> ToDto(List<UnidadAlmacenGas> alm)
        {
            return alm.ToList().Select(x => ToDto(x)).ToList();
        }

        public static CilindroDto ToDto(UnidadAlmacenGasCilindro alm)
        {
            return new CilindroDto()
            {
                IdCilindro = alm.IdCilindro,
                CapacidadKg = alm.CapacidadKg.ToString(),
                Cantidad = alm.Cantidad,
            };
        }
        public static List<CilindroDto> ToDto(List<UnidadAlmacenGasCilindro> alms)
        {
            return alms.ToList().Select(x => ToDto(x)).ToList();
        }
    }
}
