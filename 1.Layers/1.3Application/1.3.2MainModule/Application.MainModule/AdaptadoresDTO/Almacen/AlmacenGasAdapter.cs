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
            var fechaRegistro = DateTime.Now;

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
                EntradaKg = 0,
                EntradaLt = 0,
                SalidaKg = 0,
                SalidaLt = 0,
                CantidadAnteriorKg = 0,
                CantidadAnteriorLt = 0,
                CantidadActualKg = 0,
                CantidadActualLt = 0,
                CantidadAcumuladaDiaKg = 0,
                CantidadAcumuladaDiaLt = 0,
                CantidadAcumuladaMesKg = 0,
                CantidadAcumuladaMesLt = 0,
                CantidadAcumuladaAnioKg = 0,
                CantidadAcumuladaAnioLt = 0,
                FechaAplicacion = fechaRegistro,
                FechaRegistro = fechaRegistro,
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
            var almGMovimiento = FromInit();

            //------Ids y nombres-----------------
            almGMovimiento.IdEmpresa = empresa.IdEmpresa;
            almGMovimiento.Year = (short)descarga.FechaFinDescarga.Value.Year;
            almGMovimiento.Mes = (byte)descarga.FechaFinDescarga.Value.Month;
            almGMovimiento.Dia = (byte)descarga.FechaFinDescarga.Value.Day;
            almGMovimiento.Orden = ultimoMovimiento != null && ultimoMovimiento.Orden > 0 ? (short)(ultimoMovimiento.Orden + 1) : (short)1;
            almGMovimiento.IdTipoMovimiento = TipoMovimientoEnum.Entrada;
            almGMovimiento.IdTipoEvento = TipoEventoEnum.Descarga;
            almGMovimiento.IdAlmacenGas = almacenGasTotal.IdAlmacenGas;
            almGMovimiento.IdCAlmacenGasPrincipal = unidadEntrada.IdCAlmacenGas;
            almGMovimiento.IdAlmacenEntradaGasDescarga = descarga.IdAlmacenEntradaGasDescarga;
            almGMovimiento.CAlmacenPrincipalNombre = unidadEntrada.Numero;
            almGMovimiento.OperadorChoferNombre = descarga.NombreOperador;
            almGMovimiento.TipoEvento = AlmacenGasConst.Descarga;
            almGMovimiento.TipoMovimiento = AlmacenGasConst.Entrada;
            //------Ids y nombres-----------------

            //------Entrada, Salida y Saldo-----------------            
            almGMovimiento.EntradaKg = invAnterior.EntradaKg;
            almGMovimiento.EntradaLt = invAnterior.EntradaLt;
            almGMovimiento.CantidadActualKg = unidadEntrada.CantidadActualKg;
            almGMovimiento.CantidadActualLt = unidadEntrada.CantidadActualLt;
            almGMovimiento.CantidadAnteriorKg = invAnterior.CantidadAnteriorKg;
            almGMovimiento.CantidadAnteriorLt = invAnterior.CantidadAnteriorLt;
            almGMovimiento.PorcentajeActual = unidadEntrada.PorcentajeActual;
            almGMovimiento.PorcentajeAnterior = invAnterior.PorcentajeAnterior;
            //------Entrada, Salida y Saldo-----------------

            //------Entrada, Salida y Saldo Acumulados-----------------
            almGMovimiento.CAlmEntradaDiaKg = invAnterior.CAlmEntradaDiaKg;
            almGMovimiento.CAlmEntradaDiaLt = invAnterior.CAlmEntradaDiaLt;
            almGMovimiento.CAlmSalidaDiaKg = invAnterior.CAlmSalidaDiaKg;
            almGMovimiento.CAlmSalidaDiaLt = invAnterior.CAlmSalidaDiaLt;
            almGMovimiento.CAlmEntradaMesKg = invAnterior.CAlmEntradaMesKg;
            almGMovimiento.CAlmEntradaMesLt = invAnterior.CAlmEntradaMesLt;
            almGMovimiento.CAlmSalidaMesKg = invAnterior.CAlmSalidaMesKg;
            almGMovimiento.CAlmSalidaMesLt = invAnterior.CAlmSalidaMesLt;
            almGMovimiento.CAlmEntradaAnioKg = invAnterior.CAlmEntradaAnioKg;
            almGMovimiento.CAlmEntradaAnioLt = invAnterior.CAlmEntradaAnioLt;
            almGMovimiento.CAlmSalidaAnioKg = invAnterior.CAlmSalidaAnioKg;
            almGMovimiento.CAlmSalidaAnioLt = invAnterior.CAlmSalidaAnioLt;
            almGMovimiento.CantidadAcumuladaDiaKg = invAnterior.CantidadAcumuladaDiaKg;
            almGMovimiento.CantidadAcumuladaDiaLt = invAnterior.CantidadAcumuladaDiaLt;
            almGMovimiento.CantidadAcumuladaMesKg = invAnterior.CantidadAcumuladaMesKg;
            almGMovimiento.CantidadAcumuladaMesLt = invAnterior.CantidadAcumuladaMesLt;
            almGMovimiento.CantidadAcumuladaAnioKg = invAnterior.CantidadAcumuladaAnioKg;
            almGMovimiento.CantidadAcumuladaAnioLt = invAnterior.CantidadAcumuladaAnioLt;
            //------Entrada, Salida y Saldo Acumulados-----------------

            //------Almacen Gas Total y General--------------
            almGMovimiento.CantidadActualTotalKg = almacenGasTotal.CantidadActualKg;
            almGMovimiento.CantidadActualTotalLt = almacenGasTotal.CantidadActualLt;
            almGMovimiento.CantidadAnteriorTotalKg = invAnterior.CantidadAnteriorTotalKg;
            almGMovimiento.CantidadAnteriorTotalLt = invAnterior.CantidadAnteriorTotalLt;
            almGMovimiento.PorcentajeActualTotal = almacenGasTotal.PorcentajeActual;
            almGMovimiento.PorcentajeAnteriorTotal = invAnterior.PorcentajeAnteriorTotal;
            almGMovimiento.CantidadActualGeneralKg = almacenGasTotal.CantidadActualGeneralKg;
            almGMovimiento.CantidadActualGeneralLt = almacenGasTotal.CantidadActualGeneralLt;
            almGMovimiento.CantidadAnteriorGeneralKg = invAnterior.CantidadAnteriorGeneralKg;
            almGMovimiento.CantidadAnteriorGeneralLt = invAnterior.CantidadAnteriorGeneralLt;
            almGMovimiento.PorcentajeActualGeneral = almacenGasTotal.PorcentajeActualGeneral;
            almGMovimiento.PorcentajeAnteriorGeneral = invAnterior.PorcentajeAnteriorGeneral;
            //------Almacen Gas Total y General--------------

            //----Descarga--------------------
            almGMovimiento.DescargaKg = invAnterior.DescargaKg;
            almGMovimiento.DescargaLt = invAnterior.DescargaLt;
            almGMovimiento.DescargaDiaKg = invAnterior.DescargaDiaKg;
            almGMovimiento.DescargaDiaLt = invAnterior.DescargaDiaLt;
            almGMovimiento.DescargaMesKg = invAnterior.DescargaMesKg;
            almGMovimiento.DescargaMesLt = invAnterior.DescargaMesLt;
            almGMovimiento.DescargaAnioKg = invAnterior.DescargaAnioKg;
            almGMovimiento.DescargaAnioLt = invAnterior.DescargaAnioLt;
            almGMovimiento.DescargaAcumDiaKg = invAnterior.DescargaAcumDiaKg;
            almGMovimiento.DescargaAcumDiaLt = invAnterior.DescargaAcumDiaLt;
            almGMovimiento.DescargaAcumMesKg = invAnterior.DescargaAcumMesKg;
            almGMovimiento.DescargaAcumMesLt = invAnterior.DescargaAcumMesLt;
            almGMovimiento.DescargaAcumAnioKg = invAnterior.DescargaAcumAnioKg;
            almGMovimiento.DescargaAcumAnioLt = invAnterior.DescargaAcumAnioLt;
            almGMovimiento.FechaAplicacion = descarga.FechaFinDescarga.Value;
            //----Descarga--------------------

            //----Remanente--------------------
            almGMovimiento.RemaKg = invAnterior.RemaKg;
            almGMovimiento.RemaLt = invAnterior.RemaLt;
            almGMovimiento.RemaDiaKg = invAnterior.RemaDiaKg;
            almGMovimiento.RemaDiaLt = invAnterior.RemaDiaLt;
            almGMovimiento.RemaMesKg = invAnterior.RemaMesKg;
            almGMovimiento.RemaMesLt = invAnterior.RemaMesLt;
            almGMovimiento.RemaAnioKg = invAnterior.RemaAnioKg;
            almGMovimiento.RemaAnioLt = invAnterior.RemaAnioLt;
            almGMovimiento.RemaAcumDiaKg = invAnterior.RemaAcumDiaKg;
            almGMovimiento.RemaAcumDiaLt = invAnterior.RemaAcumDiaLt;
            almGMovimiento.RemaAcumMesKg = invAnterior.RemaAcumMesKg;
            almGMovimiento.RemaAcumMesLt = invAnterior.RemaAcumMesLt;
            almGMovimiento.RemaAcumAnioKg = invAnterior.RemaAcumAnioKg;
            almGMovimiento.RemaAcumAnioLt = invAnterior.RemaAcumAnioLt;
            //----Remanente--------------------

            almGMovimiento.AutoconsumoAcumDiaKg = ultimoMovimiento.AutoconsumoAcumDiaKg;
            almGMovimiento.AutoconsumoAcumDiaLt = ultimoMovimiento.AutoconsumoAcumDiaLt;
            almGMovimiento.AutoconsumoAcumMesKg = ultimoMovimiento.AutoconsumoAcumMesKg;
            almGMovimiento.AutoconsumoAcumMesLt = ultimoMovimiento.AutoconsumoAcumMesLt;
            almGMovimiento.AutoconsumoAcumAnioKg = ultimoMovimiento.AutoconsumoAcumAnioKg;
            almGMovimiento.AutoconsumoAcumAnioLt = ultimoMovimiento.AutoconsumoAcumAnioLt;
            almGMovimiento.CalibracionAcumDiaKg = ultimoMovimiento.CalibracionAcumDiaKg;
            almGMovimiento.CalibracionAcumDiaLt = ultimoMovimiento.CalibracionAcumDiaLt;
            almGMovimiento.CalibracionAcumMesKg = ultimoMovimiento.CalibracionAcumMesKg;
            almGMovimiento.CalibracionAcumMesLt = ultimoMovimiento.CalibracionAcumMesLt;
            almGMovimiento.CalibracionAcumAnioKg = ultimoMovimiento.CalibracionAcumAnioKg;
            almGMovimiento.CalibracionAcumAnioLt = ultimoMovimiento.CalibracionAcumAnioLt;            
            almGMovimiento.RecargaAcumDiaKg = ultimoMovimiento.RecargaAcumDiaKg;
            almGMovimiento.RecargaAcumDiaLt = ultimoMovimiento.RecargaAcumDiaLt;
            almGMovimiento.RecargaAcumMesKg = ultimoMovimiento.RecargaAcumMesKg;
            almGMovimiento.RecargaAcumMesLt = ultimoMovimiento.RecargaAcumMesLt;
            almGMovimiento.RecargaAcumAnioKg = ultimoMovimiento.RecargaAcumAnioKg;
            almGMovimiento.RecargaAcumAnioLt = ultimoMovimiento.RecargaAcumAnioLt;            
            almGMovimiento.TraspasoAcumDiaKg = ultimoMovimiento.TraspasoAcumDiaKg;
            almGMovimiento.TraspasoAcumDiaLt = ultimoMovimiento.TraspasoAcumDiaLt;
            almGMovimiento.TraspasoAcumMesKg = ultimoMovimiento.TraspasoAcumMesKg;
            almGMovimiento.TraspasoAcumMesLt = ultimoMovimiento.TraspasoAcumMesLt;
            almGMovimiento.TraspasoAcumAnioKg = ultimoMovimiento.TraspasoAcumAnioKg;
            almGMovimiento.TraspasoAcumAnioLt = ultimoMovimiento.TraspasoAcumAnioLt;
            almGMovimiento.VentaAcumDiaKg = ultimoMovimiento.VentaAcumDiaKg;
            almGMovimiento.VentaAcumDiaLt = ultimoMovimiento.VentaAcumDiaLt;
            almGMovimiento.VentaAcumMesKg = ultimoMovimiento.VentaAcumMesKg;
            almGMovimiento.VentaAcumMesLt = ultimoMovimiento.VentaAcumMesLt;
            almGMovimiento.VentaAcumAnioKg = ultimoMovimiento.VentaAcumAnioKg;
            almGMovimiento.VentaAcumAnioLt = ultimoMovimiento.VentaAcumAnioLt;

            return almGMovimiento;
        }
    }
}
