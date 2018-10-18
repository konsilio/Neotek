using Application.MainModule.DTOs.Almacen;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Constantes;
using Sagas.MainModule.ObjetosValor.Enum;
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

                CantidadActualGeneralKg = almacenTotal.CantidadActualGeneralKg,
                CantidadActualGeneralLt = almacenTotal.CantidadActualGeneralLt,
                CapacidadGeneralKg = almacenTotal.CapacidadGeneralKg,
                CapacidadGeneralLt = almacenTotal.CapacidadGeneralLt,
                PorcentajeActualGeneral = almacenTotal.PorcentajeActualGeneral,
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

        public static AlmacenGasMovimiento FromInit()
        {
            return new AlmacenGasMovimiento
            {
                IdEmpresa = 0,
                Year = 0,
                Mes = 0,
                Dia = 0,
                Orden = 0,
                IdTipoMovimiento = 0,
                IdTipoEvento = null,
                IdOrdenVenta = null,
                IdAlmacenGas = 0,
                IdCAlmacenGasPrincipal = 0,
                IdCAlmacenGasReferencia = null,
                IdAlmacenEntradaGasDescarga = null,
                IdAlmacenGasRecarga = null,                
                RemanenteAcumuladoDiaKg = 0,
                RemanenteAcumuladoDiaLt = 0,
                RemanenteAcumuladoMesKg = 0,
                RemanenteAcumuladoMesLt = 0,
                RemanenteAcumuladoAnioKg = 0,
                RemanenteAcumuladoAnioLt = 0,
                EntradaKg = 0,
                EntradaLt = 0,
                SalidaKg = 0,
                SalidaLt = 0,
                CantidadAnteriorKg = 0,
                CantidadAnteriorLt = 0,
                CantidadActualKg = 0,
                CantidadActualLt = 0,                
                FechaAplicacion = 0,
                FechaRegistro = 0,
                CantidadAnteriorGeneralKg = 0,
                CantidadAnteriorGeneralLt = 0,
                CantidadActualGeneralKg = 0,
                CantidadActualGeneralLt = 0,
                PorcentajeAnteriorGeneral = 0,
                PorcentajeActualGeneral = 0,
                CantidadAnteriorTotalKg = 0,
                CantidadAnteriorTotalLt = 0,
                CantidadActualTotalKg = 0,
                CantidadActualTotalLt = 0,
                PorcentajeAnteriorTotal = 0,
                PorcentajeActualTotal = 0,                
                AutoconsumoAcumDiaKg = 0,
                AutoconsumoAcumDiaLt = 0,
                AutoconsumoAcumMesKg = 0,
                AutoconsumoAcumMesLt = 0,
                AutoconsumoAcumAnioKg = 0,
                AutoconsumoAcumAnioLt = 0,
                CalibracionAcumDiaKg = 0,
                CalibracionAcumDiaLt = 0,
                CalibracionAcumMesKg = 0,
                CalibracionAcumMesLt = 0,
                CalibracionAcumAnioKg = 0,
                CalibracionAcumAnioLt = 0,
                DescargaAcumDiaKg = 0,
                DescargaAcumDiaLt = 0,
                DescargaAcumMesKg = 0,
                DescargaAcumMesLt = 0,
                DescargaAcumAnioKg = 0,
                DescargaAcumAnioLt = 0,
                CAlmEntradaDiaKg = 0,
                CAlmEntradaDiaLt = 0,
                CAlmSalidaDiaKg = 0,
                CAlmSalidaDiaLt = 0,
                CAlmEntradaMesKg = 0,
                CAlmEntradaMesLt = 0,
                CAlmSalidaMesKg = 0,
                CAlmSalidaMesLt = 0,
                CAlmEntradaAnioKg = 0,
                CAlmEntradaAnioLt = 0,
                CAlmSalidaAnioKg = 0,
                CAlmSalidaAnioLt = 0,
                RecargaAcumDiaKg = 0,
                RecargaAcumDiaLt = 0,
                RecargaAcumMesKg = 0,
                RecargaAcumMesLt = 0,
                RecargaAcumAnioKg = 0,
                RecargaAcumAnioLt = 0,
                RemaAcumDiaKg = 0,
                RemaAcumDiaLt = 0,
                RemaAcumMesKg = 0,
                RemaAcumMesLt = 0,
                RemaAcumAnioKg = 0,
                RemaAcumAnioLt = 0,
                TraspasoAcumDiaKg = 0,
                TraspasoAcumDiaLt = 0,
                TraspasoAcumMesKg = 0,
                TraspasoAcumMesLt = 0,
                TraspasoAcumAnioKg = 0,
                TraspasoAcumAnioLt = 0,
                VentaAcumDiaKg = 0,
                VentaAcumDiaLt = 0,
                VentaAcumMesKg = 0,
                VentaAcumMesLt = 0,
                VentaAcumAnioKg = 0,
                VentaAcumAnioLt = 0,
                
                FolioOperacionDia = null,
                CAlmacenPrincipalNombre = null,
                CAlmacenReferenciaNombre = null,
                OperadorChoferNombre = null,
                TipoEvento = null,
                TipoMovimiento = null,
                RemanenteKg = null,
                RemanenteLt = null,
                CantidadAcumuladaDiaKg = null,
                CantidadAcumuladaDiaLt = null,
                CantidadAcumuladaMesKg = null,
                CantidadAcumuladaMesLt = null,
                CantidadAcumuladaAnioKg = null,
                CantidadAcumuladaAnioLt = null,
                PorcentajeAnterior = null,
                PorcentajeActual = null,
                P5000Anterior = null,
                P5000Actual = null,
                AutoconsumoKg = null,
                AutoconsumoLt = null,
                AutoconsumoDiaKg = null,
                AutoconsumoDiaLt = null,
                AutoconsumoMesKg = null,
                AutoconsumoMesLt = null,
                AutoconsumoAnioKg = null,
                AutoconsumoAnioLt = null,
                CalibracionKg = null,
                CalibracionLt = null,
                CalibracionDiaKg = null,
                CalibracionDiaLt = null,
                CalibracionMesKg = null,
                CalibracionMesLt = null,
                CalibracionAnioKg = null,
                CalibracionAnioLt = null,
                DescargaKg = null,
                DescargaLt = null,
                DescargaDiaKg = null,
                DescargaDiaLt = null,
                DescargaMesKg = null,
                DescargaMesLt = null,
                DescargaAnioKg = null,
                DescargaAnioLt = null,
                RecargaKg = null,
                RecargaLt = null,
                RecargaDiaKg = null,
                RecargaDiaLt = null,
                RecargaMesKg = null,
                RecargaMesLt = null,
                RecargaAnioKg = null,
                RecargaAnioLt = null,
                RemaKg = null,
                RemaLt = null,
                RemaDiaKg = null,
                RemaDiaLt = null,
                RemaMesKg = null,
                RemaMesLt = null,
                RemaAnioKg = null,
                RemaAnioLt = null,
                TraspasoKg = null,
                TraspasoLt = null,
                TraspasoDiaKg = null,
                TraspasoDiaLt = null,
                TraspasoMesKg = null,
                TraspasoMesLt = null,
                TraspasoAnioKg = null,
                TraspasoAnioLt = null,
                VentaKg = null,
                VentaLt = null,
                VentaDiaKg = null,
                VentaDiaLt = null,
                VentaMesKg = null,
                VentaMesLt = null,
                VentaAnioKg = null,
                VentaAnioLt = null,
            };
        }

        public static AlmacenGasMovimiento FromEntity(UnidadAlmacenGas unidadEntrada, AlmacenGasDescarga descarga, AlmacenGas almacenGasTotal, AlmacenGasMovimiento ultimoMovimiento, Empresa empresa, InventarioAnteriorDto invAnterior)
        {
            return new AlmacenGasMovimiento
            {
                IdEmpresa = empresa.IdEmpresa,
                Year = (short)descarga.FechaFinDescarga.Value.Year,
                Mes = (byte)descarga.FechaFinDescarga.Value.Month,
                Dia = (byte)descarga.FechaFinDescarga.Value.Day,
                Orden = ultimoMovimiento != null && ultimoMovimiento.Orden > 0 ? (short)(ultimoMovimiento.Orden + 1) : (short)1,
                IdTipoMovimiento = TipoMovimientoEnum.Entrada,
                IdTipoEvento = TipoEventoEnum.Descarga,
                IdAlmacenGas = almacenGasTotal.IdAlmacenGas,
                IdCAlmacenGasPrincipal = unidadEntrada.IdCAlmacenGas,
                IdAlmacenEntradaGasDescarga = descarga.IdAlmacenEntradaGasDescarga,
                CAlmacenPrincipalNombre = unidadEntrada.Numero,
                OperadorChoferNombre = descarga.NombreOperador,
                TipoEvento = AlmacenGasConst.Descarga,
                TipoMovimiento = AlmacenGasConst.Entrada,
                RemanenteKg = invAnterior.RemanenteKg,
                RemanenteLt = invAnterior.RemanenteLt,
                RemanenteAcumuladoDiaKg = invAnterior.RemanenteAcumuladoDiaKg,
                RemanenteAcumuladoDiaLt = invAnterior.RemanenteAcumuladoDiaLt,
                RemanenteAcumuladoMesKg = invAnterior.RemanenteAcumuladoMesKg,
                RemanenteAcumuladoMesLt = invAnterior.RemanenteAcumuladoMesLt,
                RemanenteAcumuladoAnioKg = invAnterior.RemanenteAcumuladoAnioKg,
                RemanenteAcumuladoAnioLt = invAnterior.RemanenteAcumuladoAnioLt,
                EntradaKg = invAnterior.EntradaKg,
                EntradaLt = invAnterior.EntradaLt,
                CantidadActualKg = unidadEntrada.CantidadActualKg,
                CantidadActualLt = unidadEntrada.CantidadActualLt,
                CantidadAnteriorKg = invAnterior.CantidadAnteriorKg,
                CantidadAnteriorLt = invAnterior.CantidadAnteriorLt,
                PorcentajeActual = unidadEntrada.PorcentajeActual,
                PorcentajeAnterior = invAnterior.PorcentajeAnterior,
                CantidadActualTotalKg = almacenGasTotal.CantidadActualKg,
                CantidadActualTotalLt = almacenGasTotal.CantidadActualLt,
                CantidadAnteriorTotalKg = invAnterior.CantidadAnteriorTotalKg,
                CantidadAnteriorTotalLt = invAnterior.CantidadAnteriorTotalLt,
                PorcentajeActualTotal = almacenGasTotal.PorcentajeActual,
                PorcentajeAnteriorTotal = invAnterior.PorcentajeAnteriorTotal,
                CantidadActualGeneralKg = almacenGasTotal.CantidadActualGeneralKg,
                CantidadActualGeneralLt = almacenGasTotal.CantidadActualGeneralLt,
                CantidadAnteriorGeneralKg = invAnterior.CantidadAnteriorGeneralKg,
                CantidadAnteriorGeneralLt = invAnterior.CantidadAnteriorGeneralLt,
                PorcentajeActualGeneral = almacenGasTotal.PorcentajeActualGeneral,
                PorcentajeAnteriorGeneral = invAnterior.PorcentajeAnteriorGeneral,
                CantidadAcumuladaDiaKg = invAnterior.CantidadAcumuladaDiaKg,
                CantidadAcumuladaDiaLt = invAnterior.CantidadAcumuladaDiaLt,
                CantidadAcumuladaMesKg = invAnterior.CantidadAcumuladaMesKg,
                CantidadAcumuladaMesLt = invAnterior.CantidadAcumuladaMesLt,
                CantidadAcumuladaAnioKg = invAnterior.CantidadAcumuladaAnioKg,
                CantidadAcumuladaAnioLt = invAnterior.CantidadAcumuladaAnioLt,
                FechaAplicacion = descarga.FechaFinDescarga.Value,
                FechaRegistro = DateTime.Now,

                                
                
                AutoconsumoAcumDiaKg = null,
                AutoconsumoAcumDiaLt = null,
                AutoconsumoAcumMesKg = null,
                AutoconsumoAcumMesLt = null,
                AutoconsumoAcumAnioKg = null,
                AutoconsumoAcumAnioLt = null,
                CalibracionAcumDiaKg = null,
                CalibracionAcumDiaLt = null,
                CalibracionAcumMesKg = null,
                CalibracionAcumMesLt = null,
                CalibracionAcumAnioKg = null,
                CalibracionAcumAnioLt = null,
                DescargaKg = null,
                DescargaLt = null,
                DescargaDiaKg = null,
                DescargaDiaLt = null,
                DescargaMesKg = null,
                DescargaMesLt = null,
                DescargaAnioKg = null,
                DescargaAnioLt = null,
                DescargaAcumDiaKg = null,
                DescargaAcumDiaLt = null,
                DescargaAcumMesKg = null,
                DescargaAcumMesLt = null,
                DescargaAcumAnioKg = null,
                DescargaAcumAnioLt = null,
                CAlmEntradaDiaKg = null,
                CAlmEntradaDiaLt = null,
                CAlmSalidaDiaKg = null,
                CAlmSalidaDiaLt = null,
                CAlmEntradaMesKg = null,
                CAlmEntradaMesLt = null,
                CAlmSalidaMesKg = null,
                CAlmSalidaMesLt = null,
                CAlmEntradaAnioKg = null,
                CAlmEntradaAnioLt = null,
                CAlmSalidaAnioKg = null,
                CAlmSalidaAnioLt = null,
                RecargaAcumDiaKg = null,
                RecargaAcumDiaLt = null,
                RecargaAcumMesKg = null,
                RecargaAcumMesLt = null,
                RecargaAcumAnioKg = null,
                RecargaAcumAnioLt = null,
                RemaKg = null,
                RemaLt = null,
                RemaDiaKg = null,
                RemaDiaLt = null,
                RemaMesKg = null,
                RemaMesLt = null,
                RemaAnioKg = null,
                RemaAnioLt = null,
                RemaAcumDiaKg = null,
                RemaAcumDiaLt = null,
                RemaAcumMesKg = null,
                RemaAcumMesLt = null,
                RemaAcumAnioKg = null,
                RemaAcumAnioLt = null,
                TraspasoAcumDiaKg = null,
                TraspasoAcumDiaLt = null,
                TraspasoAcumMesKg = null,
                TraspasoAcumMesLt = null,
                TraspasoAcumAnioKg = null,
                TraspasoAcumAnioLt = null,
                VentaAcumDiaKg = null,
                VentaAcumDiaLt = null,
                VentaAcumMesKg = null,
                VentaAcumMesLt = null,
                VentaAcumAnioKg = null,
                VentaAcumAnioLt = null,
            };
        }
    }
}
