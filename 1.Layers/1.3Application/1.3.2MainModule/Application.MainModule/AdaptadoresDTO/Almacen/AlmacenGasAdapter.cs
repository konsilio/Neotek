﻿using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Almacen
{
    public class AlmacenGasAdapter
    {
        public static AlmacenGas FromEntity(AlmacenGas almacenTotal)
        {
            return new AlmacenGas
            {
                IdAlmacenGas = almacenTotal.IdAlmacenGas,
                IdEmpresa = almacenTotal.IdEmpresa,
                CapacidadTotalLt = almacenTotal.CapacidadTotalLt,
                CapacidadTotalKg = almacenTotal.CapacidadTotalKg,
                CantidadActualLt = almacenTotal.CantidadActualLt,
                CantidadActualKg = almacenTotal.CantidadActualKg,
                PorcentajeActual = almacenTotal.PorcentajeActual,
                Activo = almacenTotal.Activo,
                FechaRegistro = almacenTotal.FechaRegistro,
            };
        }

        public static UnidadAlmacenGas FromEntity(UnidadAlmacenGas unidad)
        {
            return new UnidadAlmacenGas()
            {
                IdCAlmacenGas = unidad.IdCAlmacenGas,
                IdAlmacenGas = unidad.IdAlmacenGas,
                IdEmpresa = unidad.IdEmpresa,
                IdTipoAlmacen = unidad.IdTipoAlmacen,
                IdTipoMedidor = unidad.IdTipoMedidor,
                IdEstacionCarburacion = unidad.IdEstacionCarburacion,
                IdCamioneta = unidad.IdCamioneta,
                IdPipa = unidad.IdPipa,
                EsGeneral = unidad.EsGeneral,
                EsAlterno = unidad.EsAlterno,
                Numero = unidad.Numero,
                CapacidadTanqueLt = unidad.CapacidadTanqueLt,
                CapacidadTanqueKg = unidad.CapacidadTanqueKg,
                CantidadActualLt = unidad.CantidadActualLt,
                CantidadActualKg = unidad.CantidadActualKg,
                PorcentajeActual = unidad.PorcentajeActual,
                P5000Actual = unidad.P5000Actual,
                Activo = unidad.Activo,
                FechaRegistro = unidad.FechaRegistro,
                PorcentajeCalibracionPlaneada = unidad.PorcentajeCalibracionPlaneada,
            };
        }

        public static CamionetaCilindro FromEntity(CamionetaCilindro cilindro)
        {
            return new CamionetaCilindro
            {
                IdEmpresa = cilindro.IdEmpresa,
                IdCamioneta = cilindro.IdCamioneta,
                IdCilindro = cilindro.IdCilindro,
                Cantidad = cilindro.Cantidad,
            };
        }

        public static CamionetaCilindro FromEntity(AlmacenGasRecargaCilindro cilindro, UnidadAlmacenGas unidad, UnidadAlmacenGasCilindro cilindroUA)
        {
            return new CamionetaCilindro
            {
                IdEmpresa = cilindroUA.IdEmpresa,
                IdCamioneta = unidad.IdCamioneta.Value,
                IdCilindro = cilindroUA.IdCilindro,
                Cantidad = cilindro.Cantidad,
            };
        }

        public static AlmacenGasDescarga FromEntity(AlmacenGasDescarga descarga)
        {
            return new AlmacenGasDescarga
            {
                IdAlmacenEntradaGasDescarga = descarga.IdAlmacenEntradaGasDescarga,
                IdAlmacenGas = descarga.IdAlmacenGas,
                IdCAlmacenGas = descarga.IdCAlmacenGas,
                IdOrdenCompraExpedidor = descarga.IdOrdenCompraExpedidor,
                IdOrdenCompraPorteador = descarga.IdOrdenCompraPorteador,
                IdProveedorExpedidor = descarga.IdProveedorExpedidor,
                IdProveedorPorteador = descarga.IdProveedorPorteador,
                IdRequisicion = descarga.IdRequisicion,
                IdTipoMedidorAlmacen = descarga.IdTipoMedidorAlmacen,
                IdTipoMedidorTractor = descarga.IdTipoMedidorTractor,
                CapacidadTanqueKg = descarga.CapacidadTanqueKg,
                CapacidadTanqueLt = descarga.CapacidadTanqueLt,
                ClaveOperacion = descarga.ClaveOperacion,
                DatosProcesados = descarga.DatosProcesados,
                FechaEmbarque = descarga.FechaEmbarque,
                FechaEntraGas = descarga.FechaEntraGas,
                FechaFinDescarga = descarga.FechaFinDescarga,
                FechaInicioDescarga = descarga.FechaInicioDescarga,
                FechaPapeleta = descarga.FechaPapeleta,
                FechaRegistro = descarga.FechaRegistro,
                MasaKg = descarga.MasaKg,
                NombreOperador = descarga.NombreOperador,
                NombreResponsable = descarga.NombreResponsable,
                NumeroEmbarque = descarga.NumeroEmbarque,
                NumTanquePG = descarga.NumTanquePG,
                PlacasTractor = descarga.PlacasTractor,
                PorcenMagnatelOcular = descarga.PorcenMagnatelOcular,
                PorcenMagnatelOcularAlmacenFIN = descarga.PorcenMagnatelOcularAlmacenFIN,
                PorcenMagnatelOcularAlmacenINI = descarga.PorcenMagnatelOcularAlmacenINI,
                PorcenMagnatelOcularTractorFIN = descarga.PorcenMagnatelOcularTractorFIN,
                PorcenMagnatelOcularTractorINI = descarga.PorcenMagnatelOcularTractorINI,
                PorcenMagnatelPapeleta = descarga.PorcenMagnatelPapeleta,
                PresionTanque = descarga.PresionTanque,
                Sello = descarga.Sello,
                TanquePrestado = descarga.TanquePrestado,
                ValorCarga = descarga.ValorCarga,
            };
        }

        public static AlmacenGasDescargaFoto FromEntity(AlmacenGasDescargaFoto img)
        {
            return new AlmacenGasDescargaFoto
            {
                IdAlmacenEntradaGasDescarga = img.IdAlmacenEntradaGasDescarga,
                Orden = img.Orden,
                IdImagenDe = img.IdImagenDe,
                CadenaBase64 = img.CadenaBase64,
                PathImagen = img.PathImagen,
                UrlImagen = img.UrlImagen
            };
        }

        public static AlmacenGasRecarga FromEntity(AlmacenGasRecarga recarga)
        {
            return new AlmacenGasRecarga
            {
                IdAlmacenGasRecarga = recarga.IdAlmacenGasRecarga,
                IdCAlmacenGasSalida = recarga.IdCAlmacenGasSalida,
                IdCAlmacenGasEntrada = recarga.IdCAlmacenGasEntrada,
                IdTipoMedidorSalida = recarga.IdTipoMedidorSalida,
                IdTipoMedidorEntrada = recarga.IdTipoMedidorEntrada,
                IdTipoEvento = recarga.IdTipoEvento,
                P5000Salida = recarga.P5000Salida,
                P5000Entrada = recarga.P5000Entrada,
                PorcentajeSalida = recarga.PorcentajeSalida,
                ProcentajeEntrada = recarga.ProcentajeEntrada,
                ClaveOperacion = recarga.ClaveOperacion,
                DatosProcesados = recarga.DatosProcesados,
                FechaRegistro = recarga.FechaRegistro,
            };
        }

        public static AlmacenGasRecargaFoto FromEntity(AlmacenGasRecargaFoto img)
        {
            return new AlmacenGasRecargaFoto
            {
                IdAlmacenGasRecarga = img.IdAlmacenGasRecarga,
                IdOrden = img.IdOrden,
                IdImagenDe = img.IdImagenDe,
                CadenaBase64 = img.CadenaBase64,
                PathImagen = img.PathImagen,
                UrlImagen = img.UrlImagen
            };
        }

        public static AlmacenGasTraspaso FromEntity(AlmacenGasTraspaso traspaso)
        {
            return new AlmacenGasTraspaso
            {
                IdEmpresa = traspaso.IdEmpresa,
                Year = traspaso.Year,
                Mes = traspaso.Mes,
                Dia = traspaso.Dia,
                Orden = traspaso.Orden,
                IdCAlmacenGasSalida = traspaso.IdCAlmacenGasSalida,
                IdCAlmacenGasEntrada = traspaso.IdCAlmacenGasEntrada,
                IdTipoMedidorSalida = traspaso.IdTipoMedidorSalida,
                IdTipoEvento = traspaso.IdTipoEvento,
                P5000Salida = traspaso.P5000Salida,
                P5000Entrada = traspaso.P5000Entrada,
                PorcentajeSalida = traspaso.PorcentajeSalida,
                ClaveOperacion = traspaso.ClaveOperacion,
                DatosProcesados = traspaso.DatosProcesados,
                FechaRegistro = traspaso.FechaRegistro,
            };
        }

        public static AlmacenGasTraspasoFoto FromEntity(AlmacenGasTraspasoFoto img)
        {
            return new AlmacenGasTraspasoFoto
            {
                IdEmpresa = img.IdEmpresa,
                Year = img.Year,
                Mes = img.Mes,
                Dia = img.Dia,
                Orden = img.Orden,
                OrdenImagen = img.OrdenImagen,
                IdImagenDe = img.IdImagenDe,
                CadenaBase64 = img.CadenaBase64,
                PathImagen = img.PathImagen,
                UrlImagen = img.UrlImagen
            };
        }

        public static AlmacenGasAutoConsumo FromEntity(AlmacenGasAutoConsumo AutoConsumo)
        {
            return new AlmacenGasAutoConsumo
            {
                IdEmpresa = AutoConsumo.IdEmpresa,
                Year = AutoConsumo.Year,
                Mes = AutoConsumo.Mes,
                Dia = AutoConsumo.Dia,
                Orden = AutoConsumo.Orden,
                IdCAlmacenGasSalida = AutoConsumo.IdCAlmacenGasSalida,
                IdCAlmacenGasEntrada = AutoConsumo.IdCAlmacenGasEntrada,
                IdTipoEvento = AutoConsumo.IdTipoEvento,
                P5000Salida = AutoConsumo.P5000Salida,
                ClaveOperacion = AutoConsumo.ClaveOperacion,
                DatosProcesados = AutoConsumo.DatosProcesados,
                FechaRegistro = AutoConsumo.FechaRegistro,                
            };
        }

        public static AlmacenGasAutoConsumoFoto FromEntity(AlmacenGasAutoConsumoFoto img)
        {
            return new AlmacenGasAutoConsumoFoto
            {
                IdEmpresa = img.IdEmpresa,
                Year = img.Year,
                Mes = img.Mes,
                Dia = img.Dia,
                Orden = img.Orden,
                OrdenImagen = img.OrdenImagen,
                IdImagenDe = img.IdImagenDe,
                CadenaBase64 = img.CadenaBase64,
                PathImagen = img.PathImagen,
                UrlImagen = img.UrlImagen,
            };
        }

        public static AlmacenGasCalibracion FromEntity(AlmacenGasCalibracion Calibracion)
        {
            return new AlmacenGasCalibracion
            {
                IdCAlmacenGas = Calibracion.IdCAlmacenGas,
                IdTipoEvento = Calibracion.IdTipoEvento,                
                IdDestinoCalibracion = Calibracion.IdDestinoCalibracion,
                IdOrden = Calibracion.IdOrden,
                IdTipoMedidor = Calibracion.IdTipoMedidor,
                PorcentajeCalibracion = Calibracion.PorcentajeCalibracion,
                Porcentaje = Calibracion.Porcentaje,
                P5000 = Calibracion.P5000,
                ClaveOperacion = Calibracion.ClaveOperacion,
                DatosProcesados = Calibracion.DatosProcesados,
                FechaRegistro = Calibracion.FechaRegistro,
            };
        }

        public static AlmacenGasCalibracionFoto FromEntity(AlmacenGasCalibracionFoto img)
        {
            return new AlmacenGasCalibracionFoto
            {
                IdCAlmacenGas = img.IdCAlmacenGas,
                IdOrden = img.IdOrden,
                IdOrdenFoto = img.IdOrdenFoto,
                IdImagenDe = img.IdImagenDe,
                CadenaBase64 = img.CadenaBase64,
                PathImagen = img.PathImagen,
                UrlImagen = img.UrlImagen,
            };
        }
    }
}
