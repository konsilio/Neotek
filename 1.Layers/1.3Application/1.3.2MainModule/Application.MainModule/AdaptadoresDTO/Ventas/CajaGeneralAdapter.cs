﻿using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Ventas;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Ventas
{
    public class CajaGeneralAdapter
    {
        public static CajaGeneralDTO ToDTO(VentaMovimiento pv)
        {
            CajaGeneralDTO usDTO = new CajaGeneralDTO()
            {
                IdEmpresa = pv.IdEmpresa,
                Year = pv.Year,
                Mes = pv.Mes,
                Dia = pv.Dia,
                Orden = pv.Orden,
                IdTipoMovimiento = pv.IdTipoMovimiento,
                IdPuntoVenta = pv.IdPuntoVenta,
                IdCliente = pv.IdCliente,
                IdOperadorChofer = pv.IdOperadorChofer,
                IdCAlmacenGas = pv.IdCAlmacenGas,
                FolioOperacionDia = pv.FolioOperacionDia,
                FolioVenta = pv.FolioVenta,
                FolioAnticipo = pv.FolioAnticipo,
                FolioCorteCaja = pv.FolioCorteCaja,
                TipoMovimiento = pv.TipoMovimiento,
                Descripcion = pv.Descripcion,
                Ingreso = pv.Ingreso,
                Egreso = pv.Egreso,
                Saldo = pv.Saldo,
                PuntoVenta = pv.PuntoVenta,
                OperadorChoferNombre = pv.OperadorChoferNombre,
                FechaAplicacion = pv.FechaAplicacion,
                FechaRegistro = pv.FechaRegistro
                //CAlmacenGas  = pv.CAlmacenGas,
                //COperadorChofer
                //CPuntoVenta 
            };
            return usDTO;
        }
        public static List<CajaGeneralDTO> ToDTO(List<VentaMovimiento> lu)
        {
            List<CajaGeneralDTO> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            return luDTO;
        }

        //public static VPuntoVentaDetalleDTO ToDTO(VentaPuntoDeVentaDetalle pv, decimal totalCilindros, decimal CantKg20, decimal CantKg30, decimal CantKg45)
        //{
        //    string concepto = "";
        //    decimal c = 0;
        //    if (CantKg20 != 0)
        //        concepto = "20kg";
        //    if (CantKg30 != 0)
        //        concepto = "30kg";
        //    if (CantKg45 != 0)
        //        concepto = "45kg";

        //    VPuntoVentaDetalleDTO usDTO = new VPuntoVentaDetalleDTO()
        //    {
        //        IdEmpresa = pv.IdEmpresa,
        //        Year = pv.Year,
        //        Mes = pv.Mes,
        //        Dia = pv.Dia,
        //        Orden = pv.Orden,
        //        OrdenDetalle = pv.OrdenDetalle,
        //        IdProducto = pv.IdProducto,
        //        IdProductoLinea = pv.IdProductoLinea,
        //        IdCategoria = pv.IdCategoria,
        //        PrecioUnitarioLt = pv.PrecioUnitarioLt,
        //        PrecioUnitarioKg = pv.PrecioUnitarioKg,
        //        DescuentoUnitarioLt = pv.DescuentoUnitarioLt,
        //        DescuentoUnitarioKg = pv.DescuentoUnitarioKg,
        //        CantidadLt = pv.CantidadLt,
        //        CantidadKg = pv.CantidadKg,
        //        DescuentoTotal = pv.DescuentoTotal,
        //        Subtotal = pv.Subtotal,
        //        ProductoDescripcion = pv.ProductoDescripcion,
        //        ProductoLinea = pv.ProductoLinea,
        //        ProductoCategoria = pv.ProductoCategoria,
        //        FechaRegistro = pv.FechaRegistro,
        //        IdUnidadMedida = pv.IdUnidadMedida,
        //        PrecioUnitarioProducto = pv.PrecioUnitarioProducto,
        //        DescuentoUnitarioProducto = pv.DescuentoUnitarioProducto,
        //        CantidadProducto = pv.CantidadProducto,
        //        //Camioneta Reporte del dia Camioneta
        //        CantidadTotalProd = totalCilindros,//ventas
        //        Concepto = concepto,
        //        Total = (totalCilindros * pv.DescuentoUnitarioLt),
        //        Salida = "", //LecturaInicial Clindro
        //        Recepcion = "",//LecturaFinal Clindro
        //    };


        //    return usDTO;
        //}

        //public static List<VPuntoVentaDetalleDTO> ToDTO(List<VentaPuntoDeVentaDetalle> lu)
        //{
        //    decimal TotalProductos = 0;
        //    decimal CantKg20 = 0;
        //    decimal CantKg30 = 0;
        //    decimal CantKg45 = 0;
        //    List<VPuntoVentaDetalleDTO> luDTO = new List<VPuntoVentaDetalleDTO>();
        //    var capacidadCilindros = lu.GroupBy(x => x.CantidadKg.Value).ToList();
        //    foreach (var l in lu)

        //    {
        //        TotalProductos += l.CantidadProducto.Value;
        //        if (l.CantidadKg.Value == 20)
        //            CantKg20 = l.CantidadKg.Value;
        //        if (l.CantidadKg.Value == 30)
        //            CantKg30 = l.CantidadKg.Value;
        //        if (l.CantidadKg.Value == 45)
        //            CantKg45 = l.CantidadKg.Value;

        //        luDTO = lu.ToList().Select(x => ToDTO(x, TotalProductos, CantKg20, CantKg30, CantKg45)).ToList();

        //    }
        //    //cuantos de 20, cuantos de 30, cuantos de 45???????????

        //    return luDTO;
        //}
        public static VPuntoVentaDetalleDTO ToDTO(VentaPuntoDeVentaDetalle pv)
        {

            VPuntoVentaDetalleDTO usDTO = new VPuntoVentaDetalleDTO()
            {
                IdEmpresa = pv.IdEmpresa,
                Year = pv.Year,
                Mes = pv.Mes,
                Dia = pv.Dia,
                Orden = pv.Orden,
                OrdenDetalle = pv.OrdenDetalle,
                IdProducto = pv.IdProducto,
                IdProductoLinea = pv.IdProductoLinea,
                IdCategoria = pv.IdCategoria,
                PrecioUnitarioLt = pv.PrecioUnitarioLt,
                PrecioUnitarioKg = pv.PrecioUnitarioKg,
                DescuentoUnitarioLt = pv.DescuentoUnitarioLt,
                DescuentoUnitarioKg = pv.DescuentoUnitarioKg,
                CantidadLt = pv.CantidadLt,
                CantidadKg = pv.CantidadKg,
                DescuentoTotal = pv.DescuentoTotal,
                Subtotal = pv.Subtotal,
                ProductoDescripcion = pv.ProductoDescripcion,
                ProductoLinea = pv.ProductoLinea,
                ProductoCategoria = pv.ProductoCategoria,
                FechaRegistro = pv.FechaRegistro,
                IdUnidadMedida = pv.IdUnidadMedida,
                PrecioUnitarioProducto = pv.PrecioUnitarioProducto,
                DescuentoUnitarioProducto = pv.DescuentoUnitarioProducto,
                CantidadProducto = pv.CantidadProducto,
                //Camioneta Reporte del dia Camioneta
                //CantidadTotalProd = totalCilindros,//ventas
                //Concepto = concepto,
                //Total = (totalCilindros * pv.DescuentoUnitarioLt),
                //Salida = "", //LecturaInicial Clindro
                //Recepcion = "",//LecturaFinal Clindro
            };


            return usDTO;
        }

        public static List<VPuntoVentaDetalleDTO> ToDTO(List<VentaPuntoDeVentaDetalle> lu)
        {
            List<VPuntoVentaDetalleDTO> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();

            return luDTO;
        }

        public static VPuntoVentaDetalleDTO ToDTOVC(VentaPuntoDeVentaDetalle pv, decimal cilindrosVendidos)
        {
            VPuntoVentaDetalleDTO usDTO = new VPuntoVentaDetalleDTO()
            {
                IdEmpresa = pv.IdEmpresa,
                Year = pv.Year,
                Mes = pv.Mes,
                Dia = pv.Dia,
                Orden = pv.Orden,
                OrdenDetalle = pv.OrdenDetalle,
                IdProducto = pv.IdProducto,
                IdProductoLinea = pv.IdProductoLinea,
                IdCategoria = pv.IdCategoria,
                PrecioUnitarioLt = pv.PrecioUnitarioLt,
                PrecioUnitarioKg = pv.PrecioUnitarioKg,
                DescuentoUnitarioLt = pv.DescuentoUnitarioLt,
                DescuentoUnitarioKg = pv.DescuentoUnitarioKg,
                CantidadLt = pv.CantidadLt,
                CantidadKg = pv.CantidadKg,
                DescuentoTotal = pv.DescuentoTotal,
                Subtotal = pv.Subtotal,
                ProductoDescripcion = pv.ProductoDescripcion,//Concepto
                ProductoLinea = pv.ProductoLinea,
                ProductoCategoria = pv.ProductoCategoria,
                FechaRegistro = pv.FechaRegistro,
                IdUnidadMedida = pv.IdUnidadMedida,
                PrecioUnitarioProducto = pv.PrecioUnitarioProducto,
                DescuentoUnitarioProducto = pv.DescuentoUnitarioProducto,
                CantidadProducto = cilindrosVendidos,//Ventas                                                    
                Total = (cilindrosVendidos * pv.PrecioUnitarioKg),
                Salida = "", //LecturaInicial Clindro
                Recepcion = "",//LecturaFinal Clindro
            };


            return usDTO;
        }

        public static List<VPuntoVentaDetalleDTO> ToDTOVC(List<VentaPuntoDeVentaDetalle> lu)
        {
            decimal totalVentas = 0;
            foreach (var x in lu)
            {
                totalVentas += x.CantidadProducto.Value;
            }
            List<VPuntoVentaDetalleDTO> luDTO = lu.ToList().Select(x => ToDTOVC(x, totalVentas)).ToList();

            return luDTO;
        }

        public static TanquesDto ToDTO(VentaPuntoDeVentaDetalle pv, decimal cilindrosVendidos)
        {
            var Nombre = "";
            if (pv.CantidadKg == 20)
            {
                Nombre = "20 kg";
            }
            if (pv.CantidadKg == 30)
            {
                Nombre = "30 kg";
            }
            else
            {
                Nombre = "45 kg";
            }
            TanquesDto usDTO = new TanquesDto()
            {
                NombreTanque = Nombre,
                Normal = (int)cilindrosVendidos,
                Venta = 0,
            };


            return usDTO;
        }

        public static List<TanquesDto> ToDTOT(List<VentaPuntoDeVentaDetalle> lu)
        {
            decimal totalVentas = 0;
            foreach (var x in lu)
            {
                totalVentas += x.CantidadProducto.Value;
            }
            List<TanquesDto> luDTO = lu.ToList().Select(x => ToDTO(x, totalVentas)).ToList();

            return luDTO;
        }

        public static AlmacenGasMovimientoDto ToDTO(AlmacenGasMovimiento pv)
        {
            AlmacenGasMovimientoDto usDTO = new AlmacenGasMovimientoDto()
            {
                IdEmpresa = pv.IdEmpresa,
                Year = pv.Year,
                Mes = pv.Mes,
                Dia = pv.Dia,
                Orden = pv.Orden,
                IdTipoMovimiento = pv.IdTipoMovimiento,
                IdTipoEvento = pv.IdTipoMovimiento,
                IdOrdenVenta = pv.IdTipoMovimiento,
                IdAlmacenGas = pv.IdTipoMovimiento,
                IdCAlmacenGasPrincipal = pv.IdTipoMovimiento,
                IdCAlmacenGasReferencia = pv.IdTipoMovimiento,
                IdAlmacenEntradaGasDescarga = pv.IdTipoMovimiento,
                IdAlmacenGasRecarga = pv.IdTipoMovimiento,
                FolioOperacionDia = pv.FolioOperacionDia,
                CAlmacenPrincipalNombre = pv.CAlmacenPrincipalNombre,
                CAlmacenReferenciaNombre = pv.CAlmacenReferenciaNombre,
                OperadorChoferNombre = pv.OperadorChoferNombre,
                TipoEvento = pv.TipoEvento,
                TipoMovimiento = pv.TipoMovimiento,
                EntradaKg = pv.EntradaKg,
                EntradaLt = pv.EntradaLt,
                SalidaKg = pv.SalidaKg,
                SalidaLt = pv.SalidaLt,
                CantidadAnteriorKg = pv.CantidadAnteriorKg,
                CantidadAnteriorLt = pv.CantidadAnteriorLt,
                CantidadActualKg = pv.CantidadActualKg,
                CantidadActualLt = pv.CantidadActualLt,
                CantidadAcumuladaDiaKg = pv.CantidadAcumuladaDiaKg,
                CantidadAcumuladaDiaLt = pv.CantidadAcumuladaDiaLt,
                CantidadAcumuladaMesKg = pv.CantidadAcumuladaMesKg,
                CantidadAcumuladaMesLt = pv.CantidadAcumuladaMesLt,
                CantidadAcumuladaAnioKg = pv.CantidadAcumuladaAnioKg,
                CantidadAcumuladaAnioLt = pv.CantidadAcumuladaAnioLt,
                PorcentajeAnterior = pv.PorcentajeAnterior,
                PorcentajeActual = pv.PorcentajeActual,
                P5000Anterior = pv.P5000Anterior,
                P5000Actual = pv.P5000Actual,
                FechaAplicacion = pv.FechaAplicacion,
                FechaRegistro = pv.FechaRegistro,
                /********************/
                CantidadAnteriorGeneralKg = pv.CantidadAnteriorGeneralKg,
                CantidadAnteriorGeneralLt = pv.CantidadAnteriorGeneralLt,
                CantidadActualGeneralKg = pv.CantidadActualGeneralKg,
                CantidadActualGeneralLt = pv.CantidadActualGeneralLt,
                PorcentajeAnteriorGeneral = pv.PorcentajeAnteriorGeneral,
                PorcentajeActualGeneral = pv.PorcentajeActualGeneral,
                CantidadAnteriorTotalKg = pv.CantidadAnteriorTotalKg,
                CantidadAnteriorTotalLt = pv.CantidadAnteriorTotalLt,
                CantidadActualTotalKg = pv.CantidadActualTotalKg,
                CantidadActualTotalLt = pv.CantidadActualTotalLt,
                PorcentajeAnteriorTotal = pv.PorcentajeAnteriorTotal,
                PorcentajeActualTotal = pv.PorcentajeActualTotal,
                AutoconsumoKg = pv.AutoconsumoKg,
                AutoconsumoLt = pv.AutoconsumoLt,
                AutoconsumoDiaKg = pv.AutoconsumoDiaKg,
                AutoconsumoDiaLt = pv.AutoconsumoDiaLt,
                AutoconsumoMesKg = pv.AutoconsumoMesKg,
                AutoconsumoMesLt = pv.AutoconsumoMesLt,
                AutoconsumoAnioKg = pv.AutoconsumoAnioKg,
                AutoconsumoAnioLt = pv.AutoconsumoAnioLt,
                AutoconsumoAcumDiaKg = pv.AutoconsumoAcumDiaKg,
                AutoconsumoAcumDiaLt = pv.AutoconsumoAcumDiaLt,
                AutoconsumoAcumMesKg = pv.AutoconsumoAcumMesKg,
                AutoconsumoAcumMesLt = pv.AutoconsumoAcumMesLt,
                AutoconsumoAcumAnioKg = pv.AutoconsumoAcumAnioKg,
                AutoconsumoAcumAnioLt = pv.AutoconsumoAcumAnioLt,
                CalibracionKg = pv.CalibracionKg,
                CalibracionLt = pv.CalibracionLt,
                CalibracionDiaKg = pv.CalibracionDiaKg,
                CalibracionDiaLt = pv.CalibracionDiaLt,
                CalibracionMesKg = pv.CalibracionMesKg,
                CalibracionMesLt = pv.CalibracionMesLt,
                CalibracionAnioKg = pv.CalibracionAnioKg,
                CalibracionAnioLt = pv.CalibracionAnioLt,
                CalibracionAcumDiaKg = pv.CalibracionAcumDiaKg,
                CalibracionAcumDiaLt = pv.CalibracionAcumDiaLt,
                CalibracionAcumMesKg = pv.CalibracionAcumMesKg,
                CalibracionAcumMesLt = pv.CalibracionAcumMesLt,
                CalibracionAcumAnioKg = pv.CalibracionAcumAnioKg,
                CalibracionAcumAnioLt = pv.CalibracionAcumAnioLt,
                DescargaKg = pv.DescargaKg,
                DescargaLt = pv.DescargaLt,
                DescargaDiaKg = pv.DescargaDiaKg,
                DescargaDiaLt = pv.DescargaDiaLt,
                DescargaMesKg = pv.DescargaMesKg,
                DescargaMesLt = pv.DescargaMesLt,
                DescargaAnioKg = pv.DescargaAnioKg,
                DescargaAnioLt = pv.DescargaAnioLt,
                DescargaAcumDiaKg = pv.DescargaAcumDiaKg,
                DescargaAcumDiaLt = pv.DescargaAcumDiaLt,
                DescargaAcumMesKg = pv.DescargaAcumMesKg,
                DescargaAcumMesLt = pv.DescargaAcumMesLt,
                DescargaAcumAnioKg = pv.DescargaAcumAnioKg,
                DescargaAcumAnioLt = pv.DescargaAcumAnioLt,
                CAlmEntradaDiaKg = pv.CAlmEntradaDiaKg,
                CAlmEntradaDiaLt = pv.CAlmEntradaDiaLt,
                CAlmSalidaDiaKg = pv.CAlmSalidaDiaKg,
                CAlmSalidaDiaLt = pv.CAlmSalidaDiaLt,
                CAlmEntradaMesKg = pv.CAlmEntradaMesKg,
                CAlmEntradaMesLt = pv.CAlmEntradaMesLt,
                CAlmSalidaMesKg = pv.CAlmSalidaMesKg,
                CAlmSalidaMesLt = pv.CAlmSalidaMesLt,
                CAlmEntradaAnioKg = pv.CAlmEntradaAnioKg,
                CAlmEntradaAnioLt = pv.CAlmEntradaAnioLt,
                CAlmSalidaAnioKg = pv.CAlmSalidaAnioKg,
                CAlmSalidaAnioLt = pv.CAlmSalidaAnioLt,
                RecargaKg = pv.RecargaKg,
                RecargaLt = pv.RecargaLt,
                RecargaDiaKg = pv.RecargaDiaKg,
                RecargaDiaLt = pv.RecargaDiaLt,
                RecargaMesKg = pv.RecargaMesKg,
                RecargaMesLt = pv.RecargaMesLt,
                RecargaAnioKg = pv.RecargaAnioKg,
                RecargaAnioLt = pv.RecargaAnioLt,
                RecargaAcumDiaKg = pv.RecargaAcumDiaKg,
                RecargaAcumDiaLt = pv.RecargaAcumDiaLt,
                RecargaAcumMesKg = pv.RecargaAcumMesKg,
                RecargaAcumMesLt = pv.RecargaAcumMesLt,
                RecargaAcumAnioKg = pv.RecargaAcumAnioKg,
                RecargaAcumAnioLt = pv.RecargaAcumAnioLt,
                RemaKg = pv.RemaKg,
                RemaLt = pv.RemaLt,
                RemaDiaKg = pv.RemaDiaKg,
                RemaDiaLt = pv.RemaDiaLt,
                RemaMesKg = pv.RemaMesKg,
                RemaMesLt = pv.RemaMesLt,
                RemaAnioKg = pv.RemaAnioKg,
                RemaAnioLt = pv.RemaAnioLt,
                RemaAcumDiaKg = pv.RemaAcumDiaKg,
                RemaAcumDiaLt = pv.RemaAcumDiaLt,
                RemaAcumMesKg = pv.RemaAcumMesKg,
                RemaAcumMesLt = pv.RemaAcumMesLt,
                RemaAcumAnioKg = pv.RemaAcumAnioKg,
                RemaAcumAnioLt = pv.RemaAcumAnioLt,
                TraspasoKg = pv.TraspasoKg,
                TraspasoLt = pv.TraspasoLt,
                TraspasoDiaKg = pv.TraspasoDiaKg,
                TraspasoDiaLt = pv.TraspasoDiaLt,
                TraspasoMesKg = pv.TraspasoMesKg,
                TraspasoMesLt = pv.TraspasoMesLt,
                TraspasoAnioKg = pv.TraspasoAnioKg,
                TraspasoAnioLt = pv.TraspasoAnioLt,
                TraspasoAcumDiaKg = pv.TraspasoAcumDiaKg,
                VentaKg = pv.VentaKg,
                VentaLt = pv.VentaLt,
                VentaDiaKg = pv.VentaDiaKg,
                VentaDiaLt = pv.VentaDiaLt,
                VentaMesKg = pv.VentaMesKg,
                VentaMesLt = pv.VentaMesLt,
                VentaAnioKg = pv.VentaAnioKg,
                VentaAnioLt = pv.VentaAnioLt,
                VentaAcumDiaKg = pv.VentaAcumDiaKg,
                VentaAcumDiaLt = pv.VentaAcumDiaLt,
                VentaAcumMesKg = pv.VentaAcumMesKg,
                VentaAcumMesLt = pv.VentaAcumMesLt,
                VentaAcumAnioKg = pv.VentaAcumAnioKg,
                VentaAcumAnioLt = pv.VentaAcumAnioLt,
                VentaLecturasP5000Kg = pv.VentaLecturasP5000Kg,
                VentaLecturasP5000Lt = pv.VentaLecturasP5000Lt,
                VentaLecturasMagnatelKg = pv.VentaLecturasMagnatelKg,
                VentaLecturasMagnatelLt = pv.VentaLecturasMagnatelLt,
                VentaLecturasP5000MesKg = pv.VentaLecturasP5000MesKg,
                VentaLecturasP5000MesLt = pv.VentaLecturasP5000MesLt,
                VentaLecturasMagnatelMesKg = pv.VentaLecturasMagnatelMesKg,
                VentaLecturasMagnatelMesLt = pv.VentaLecturasMagnatelMesLt,
                VentaLecturasP5000AnioKg = pv.VentaLecturasP5000AnioKg,
                VentaLecturasP5000AnioLt = pv.VentaLecturasP5000AnioLt,
                VentaLecturasMagnatelAnioKg = pv.VentaLecturasMagnatelAnioKg,
                VentaLecturasMagnatelAnioLt = pv.VentaLecturasMagnatelAnioLt,
            };
            return usDTO;
        }
        public static List<AlmacenGasMovimientoDto> ToDTO(List<AlmacenGasMovimiento> lu)
        {
            List<AlmacenGasMovimientoDto> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            return luDTO;
        }
        public static VentaPuntoVentaDTO ToDTOC(VentaPuntoDeVenta pv)
        {
            var vt = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotal;

            var vtc = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotalCredito;
            var vtco = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotalContado;
            var ov = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).OtrasVentas;
            var almacen = PuntoVentaServicio.Obtener(pv.IdPuntoVenta).IdCAlmacenGas;
            var EsPipa = AlmacenGasServicio.ObtenerAlmacen(almacen);//(pv.FolioOperacionDia).OtrasVentas;
            var EsCamioneta = AlmacenGasServicio.ObtenerAlmacen(almacen);


            VentaPuntoVentaDTO usDTO = new VentaPuntoVentaDTO()
            {
                IdEmpresa = pv.IdEmpresa,
                Year = pv.Year,
                Mes = pv.Mes,
                Dia = pv.Dia,
                Orden = pv.Orden,
                IdPuntoVenta = pv.IdPuntoVenta,
                IdCliente = pv.IdCliente,
                IdOperadorChofer = pv.IdOperadorChofer,
                IdTipoVenta = pv.IdTipoVenta,
                IdFactura = pv.IdFactura,
                FolioOperacionDia = pv.FolioOperacionDia,
                FolioVenta = pv.FolioVenta,
                RequiereFactura = pv.RequiereFactura,
                VentaACredito = pv.VentaACredito,
                Subtotal = pv.Subtotal,
                Descuento = pv.Descuento,
                Iva = pv.Iva,
                Total = pv.Total,
                PorcentajeIva = pv.PorcentajeIva,
                EfectivoRecibido = pv.EfectivoRecibido,
                CambioRegresado = pv.CambioRegresado,
                PuntoVenta = pv.PuntoVenta,
                RazonSocial = pv.RazonSocial,
                RFC = pv.RFC,
                ClienteConCredito = pv.ClienteConCredito,
                OperadorChofer = pv.OperadorChofer,
                DatosProcesados = pv.DatosProcesados,
                FechaRegistro = pv.FechaRegistro,
                VentaTotal = vt,//CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotal,
                VentaTotalCredito = vtc,//CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotalCredito,
                VentaTotalContado = vtco,//CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotalContado,
                OtrasVentas = ov,//CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).OtrasVentas,
            };
            return usDTO;
        }
        public static List<VentaPuntoVentaDTO> ToDTOC(List<VentaPuntoDeVenta> lu)
        {
            List<VentaPuntoVentaDTO> luDTO = lu.ToList().Select(x => ToDTOC(x)).ToList();
            return luDTO;
        }

        public static VentaCorteAnticipoDTO ToDTOCE(VentaCorteAnticipoEC pv)
        {
            VentaCorteAnticipoDTO usDTO = new VentaCorteAnticipoDTO()
            {
                IdEmpresa = pv.IdEmpresa,
                Year = pv.Year,
                Mes = pv.Mes,
                Dia = pv.Dia,
                Orden = pv.Orden,
                IdTipoOperacion = pv.IdTipoOperacion,
                IdPuntoVenta = pv.IdPuntoVenta,
                IdCAlmacenGas = pv.IdCAlmacenGas,
                IdOperadorChofer = pv.IdOperadorChofer,
                IdUsuarioRecibe = pv.IdUsuarioRecibe,
                FolioOperacionDia = pv.FolioOperacionDia,
                FolioOperacion = pv.FolioOperacion,
                TotalVenta = pv.TotalVenta,
                TotalAnticipado = pv.TotalAnticipado,
                MontoRecortadoAnticipado = pv.MontoRecortadoAnticipado,
                TipoOperacion = pv.TipoOperacion,
                PuntoVenta = pv.PuntoVenta,
                OperadorChofer = pv.OperadorChofer,
                UsuarioRecibe = pv.UsuarioRecibe,
                DatosProcesados = pv.DatosProcesados,
                FechaCorteAnticipo = pv.FechaCorteAnticipo,
                FechaAplicacion = pv.FechaAplicacion,
                FechaRegistro = pv.FechaRegistro
            };
            return usDTO;
        }
        public static List<VentaCorteAnticipoDTO> ToDTOCE(List<VentaCorteAnticipoEC> lu)
        {
            List<VentaCorteAnticipoDTO> luDTO = lu.ToList().Select(x => ToDTOCE(x)).ToList();
            return luDTO;
        }

        public static VentaPuntoVentaDTO ToDTOP(VentaPuntoDeVenta pv)
        {
            VentaPuntoVentaDTO usDTO = new VentaPuntoVentaDTO()
            {
                IdEmpresa = pv.IdEmpresa,
                Year = pv.Year,
                Mes = pv.Mes,
                Dia = pv.Dia,
                Orden = pv.Orden,
                IdPuntoVenta = pv.IdPuntoVenta,
                IdCliente = pv.IdCliente,
                IdOperadorChofer = pv.IdOperadorChofer,
                IdTipoVenta = pv.IdTipoVenta,
                IdFactura = pv.IdFactura,
                FolioOperacionDia = pv.FolioOperacionDia,
                FolioVenta = pv.FolioVenta,
                RequiereFactura = pv.RequiereFactura,
                VentaACredito = pv.VentaACredito,
                Subtotal = pv.Subtotal,
                Descuento = pv.Descuento,
                Iva = pv.Iva,
                Total = pv.Total,
                PorcentajeIva = pv.PorcentajeIva,
                EfectivoRecibido = pv.EfectivoRecibido,
                CambioRegresado = pv.CambioRegresado,
                PuntoVenta = pv.PuntoVenta,
                RazonSocial = pv.RazonSocial,
                RFC = pv.RFC,
                ClienteConCredito = pv.ClienteConCredito,
                OperadorChofer = pv.OperadorChofer,
                DatosProcesados = pv.DatosProcesados,
                FechaRegistro = pv.FechaRegistro,
                VentaTotal = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotal,
                VentaTotalCredito = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotalCredito,
                VentaTotalContado = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotalContado,
                OtrasVentas = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).OtrasVentas,
            };
            return usDTO;
        }
        public static RegistrarVentasMovimientosDTO ToDTO(VentaCorteAnticipoEC v)
        {
            RegistrarVentasMovimientosDTO lstFinal = new RegistrarVentasMovimientosDTO();

            lstFinal.IdEmpresa = v.IdEmpresa;
            lstFinal.Year = v.Year;
            lstFinal.Mes = v.Mes;
            lstFinal.Dia = v.Dia;
            lstFinal.Orden = (short)(CalcularPreciosVentaServicio.ObtenerConsecutivoOrden() + 1);//v.Orden,
            lstFinal.IdPuntoVenta = v.IdPuntoVenta;
            lstFinal.IdCliente = 0;//v.IdCliente,
            lstFinal.IdOperadorChofer = v.IdOperadorChofer;
            lstFinal.FolioOperacionDia = v.FolioOperacionDia;
            lstFinal.FolioVenta = v.FolioOperacion;//v.FolioVenta,
            lstFinal.Egreso = v.TotalAnticipado;
            lstFinal.PuntoVenta = v.PuntoVenta;
            lstFinal.OperadorChoferNombre = v.OperadorChofer;
            lstFinal.FechaRegistro = DateTime.Now;
            lstFinal.FechaAplicacion = v.FechaAplicacion;
            lstFinal.Descripcion = v.TipoOperacion;
            lstFinal.IdCAlmacenGas = new PuntoVentaDataAccess().Buscar(v.IdPuntoVenta).IdCAlmacenGas;

            return lstFinal;
        }

        public static VentaCajaGeneral FromDto(VentaCajaGeneral pvDTO)
        {
            return new VentaCajaGeneral()
            {
                IdEmpresa = pvDTO.IdEmpresa,
                Year = pvDTO.Year,
                Mes = pvDTO.Mes,
                Dia = pvDTO.Dia,
                Orden = pvDTO.Orden,
                IdPuntoVenta = pvDTO.IdPuntoVenta,
                IdCAlmacenGas = pvDTO.IdCAlmacenGas,
                IdOperadorChofer = pvDTO.IdOperadorChofer,
                IdUsuarioEntrega = pvDTO.IdUsuarioEntrega,
                IdUsuarioRecibe = pvDTO.IdUsuarioRecibe,
                FolioOperacionDia = pvDTO.FolioOperacionDia,
                VentaTotal = pvDTO.VentaTotal,
                VentaTotalCredito = pvDTO.VentaTotalCredito,
                VentaTotalContado = pvDTO.VentaTotalContado,
                OtrasVentas = pvDTO.OtrasVentas,
                DescuentoTotal = pvDTO.DescuentoTotal,
                DescuentoCredito = pvDTO.DescuentoCredito,
                DescuentoContado = pvDTO.DescuentoContado,
                DescuentoOtrasVentas = pvDTO.DescuentoOtrasVentas,
                TodoCorrecto = true,
                PuntoVenta = pvDTO.PuntoVenta,
                OperadorChofer = pvDTO.OperadorChofer,
                UsuarioEntrega = pvDTO.UsuarioEntrega,
                UsuarioRecibe = pvDTO.UsuarioRecibe,

            };
        }

        public static List<VentaCajaGeneral> FromDto(List<VentaCajaGeneral> DTO)
        {
            return DTO.ToList().Select(x => FromDto(x)).ToList();
        }

        public static VentaCorteAnticipoEC FromDtoCE(VentaCorteAnticipoDTO pvDTO)
        {
            return new VentaCorteAnticipoEC()
            {
                IdEmpresa = pvDTO.IdEmpresa,
                Year = pvDTO.Year,
                Mes = pvDTO.Mes,
                Dia = pvDTO.Dia,
                Orden = pvDTO.Orden,
                IdTipoOperacion = pvDTO.IdTipoOperacion,
                IdPuntoVenta = pvDTO.IdPuntoVenta,
                IdCAlmacenGas = pvDTO.IdCAlmacenGas,
                IdOperadorChofer = pvDTO.IdOperadorChofer,
                IdUsuarioRecibe = pvDTO.IdUsuarioRecibe,
                FolioOperacionDia = pvDTO.FolioOperacionDia,
                FolioOperacion = pvDTO.FolioOperacion,
                TotalVenta = pvDTO.TotalVenta,
                TotalAnticipado = pvDTO.TotalAnticipado,
                MontoRecortadoAnticipado = pvDTO.MontoRecortadoAnticipado,
                TipoOperacion = pvDTO.TipoOperacion,
                PuntoVenta = pvDTO.PuntoVenta,
                OperadorChofer = pvDTO.OperadorChofer,
                UsuarioRecibe = pvDTO.UsuarioRecibe,
                DatosProcesados = true,
                FechaCorteAnticipo = pvDTO.FechaCorteAnticipo,
                FechaAplicacion = pvDTO.FechaAplicacion,
                FechaRegistro = DateTime.Now
            };
        }
        public static List<VentaCorteAnticipoEC> FromDtoCE(List<VentaCorteAnticipoDTO> DTO)
        {
            return DTO.ToList().Select(x => FromDtoCE(x)).ToList();
        }
        public static VentaMovimiento FromDTO(RegistrarVentasMovimientosDTO pvDTO)
        {
            return new VentaMovimiento()
            {
                IdEmpresa = pvDTO.IdEmpresa,
                Year = pvDTO.Year,
                Mes = pvDTO.Mes,
                Dia = pvDTO.Dia,
                Orden = pvDTO.Orden,
                IdTipoMovimiento = pvDTO.IdTipoMovimiento,
                IdPuntoVenta = pvDTO.IdPuntoVenta,
                IdCliente = pvDTO.IdCliente,
                IdOperadorChofer = pvDTO.IdOperadorChofer,
                IdCAlmacenGas = pvDTO.IdCAlmacenGas,
                FolioOperacionDia = pvDTO.FolioOperacionDia,
                FolioVenta = pvDTO.FolioVenta,
                FolioAnticipo = pvDTO.FolioAnticipo,
                FolioCorteCaja = pvDTO.FolioCorteCaja,
                TipoMovimiento = pvDTO.TipoMovimiento,
                Descripcion = pvDTO.Descripcion,
                Ingreso = pvDTO.Ingreso,
                Egreso = pvDTO.Egreso,
                Saldo = pvDTO.Saldo,
                PuntoVenta = pvDTO.PuntoVenta,
                OperadorChoferNombre = pvDTO.OperadorChoferNombre,
                FechaAplicacion = pvDTO.FechaAplicacion,
                FechaRegistro = DateTime.Now
            };
        }
        public static VentaMovimiento FromDtoVtaMov(RegistrarVentasMovimientosDTO pvDTO)
        {
            return new VentaMovimiento()
            {
                IdEmpresa = pvDTO.IdEmpresa,
                Year = pvDTO.Year,
                Mes = pvDTO.Mes,
                Dia = pvDTO.Dia,
                Orden = pvDTO.Orden,
                IdTipoMovimiento = pvDTO.IdTipoMovimiento,
                IdPuntoVenta = pvDTO.IdPuntoVenta,
                IdCliente = pvDTO.IdCliente,
                IdOperadorChofer = pvDTO.IdOperadorChofer,
                IdCAlmacenGas = pvDTO.IdCAlmacenGas,
                FolioOperacionDia = pvDTO.FolioOperacionDia,
                FolioVenta = pvDTO.FolioVenta,
                FolioAnticipo = pvDTO.FolioAnticipo,
                FolioCorteCaja = pvDTO.FolioCorteCaja,
                TipoMovimiento = pvDTO.TipoMovimiento,
                Descripcion = pvDTO.Descripcion,
                Ingreso = pvDTO.Ingreso,
                Egreso = pvDTO.Egreso,
                Saldo = pvDTO.Saldo,
                PuntoVenta = pvDTO.PuntoVenta,
                OperadorChoferNombre = pvDTO.OperadorChoferNombre,
                FechaAplicacion = pvDTO.FechaAplicacion,
                FechaRegistro = DateTime.Now
            };
        }
        public static List<VentaMovimiento> FromDtoVtaM(List<RegistrarVentasMovimientosDTO> DTO)
        {
            return DTO.ToList().Select(x => FromDtoVtaMov(x)).ToList();
        }

        public static VentaPuntoDeVenta FromEntity(VentaPuntoDeVenta venta)
        {
            return new VentaPuntoDeVenta()
            {
                IdEmpresa = venta.IdEmpresa,
                Year = venta.Year,
                Mes = venta.Mes,
                Dia = venta.Dia,
                Orden = venta.Orden,
                IdPuntoVenta = venta.IdPuntoVenta,
                IdCliente = venta.IdCliente,
                IdOperadorChofer = venta.IdOperadorChofer,
                IdTipoVenta = venta.IdTipoVenta,
                IdFactura = venta.IdFactura,
                FolioOperacionDia = venta.FolioOperacionDia,
                FolioVenta = venta.FolioVenta,
                RequiereFactura = venta.RequiereFactura,
                VentaACredito = venta.VentaACredito,
                //Subtotal = venta.Subtotal,
                //Descuento = venta.Descuento,
                //Iva = venta.Iva,
                //Total = venta.Total,
                PorcentajeIva = venta.PorcentajeIva,
                //EfectivoRecibido = venta.EfectivoRecibido,
                CambioRegresado = venta.CambioRegresado,
                PuntoVenta = venta.PuntoVenta,
                RazonSocial = venta.RazonSocial,
                RFC = venta.RFC,
                ClienteConCredito = venta.ClienteConCredito,
                OperadorChofer = venta.OperadorChofer,
                DatosProcesados = venta.DatosProcesados,
                FechaRegistro = venta.FechaRegistro,
                FechaAplicacion = venta.FechaAplicacion,
                Subtotal = venta.Subtotal,
                SubtotalDia = venta.SubtotalDia,
                SubtotalMes = venta.SubtotalMes,
                SubtotalAnio = venta.SubtotalAnio,
                SubtotalAcumDia = venta.SubtotalAcumDia,
                SubtotalAcumMes = venta.SubtotalAcumMes,
                SubtotalAcumAnio = venta.SubtotalAcumAnio,
                Descuento = venta.Descuento,
                DescuentoDia = venta.DescuentoDia,
                DescuentoMes = venta.DescuentoMes,
                DescuentoAnio = venta.DescuentoAnio,
                DescuentoAcumDia = venta.DescuentoAcumDia,
                DescuentoAcumMes = venta.DescuentoAcumMes,
                DescuentoAcumAnio = venta.DescuentoAcumAnio,
                Iva = venta.Iva,
                IvaDia = venta.IvaDia,
                IvaMes = venta.IvaMes,
                IvaAnio = venta.IvaAnio,
                IvaAcumDia = venta.IvaAcumDia,
                IvaAcumMes = venta.IvaAcumMes,
                IvaAcumAnio = venta.IvaAcumAnio,
                Total = venta.Total,
                TotalDia = venta.TotalDia,
                TotalMes = venta.TotalMes,
                TotalAnio = venta.TotalAnio,
                TotalAcumDia = venta.TotalAcumDia,
                TotalAcumMes = venta.TotalAcumMes,
                TotalAcumAnio = venta.TotalAcumAnio,
                EfectivoRecibido = venta.EfectivoRecibido,
                EfectivoRecibidoDia = venta.EfectivoRecibidoDia,
                EfectivoRecibidoMes = venta.EfectivoRecibidoMes,
                EfectivoRecibidoAnio = venta.EfectivoRecibidoAnio,
                EfectivoRecibidoAcumDia = venta.EfectivoRecibidoAcumDia,
                EfectivoRecibidoAcumMes = venta.EfectivoRecibidoAcumMes,
                EfectivoRecibidoAcumAnio = venta.EfectivoRecibidoAcumAnio,
            };
        }

        public static VentaMovimiento FromEntity(VentaMovimiento venta)
        {
            return new VentaMovimiento()
            {
                IdEmpresa = venta.IdEmpresa,
                Year = venta.Year,
                Mes = venta.Mes,
                Dia = venta.Dia,
                Orden = venta.Orden,
                IdTipoMovimiento = venta.IdTipoMovimiento,
                IdPuntoVenta = venta.IdPuntoVenta,
                IdCliente = venta.IdCliente,
                IdOperadorChofer = venta.IdOperadorChofer,
                IdCAlmacenGas = venta.IdCAlmacenGas,
                FolioOperacionDia = venta.FolioOperacionDia,
                FolioVenta = venta.FolioVenta,
                FolioAnticipo = venta.FolioAnticipo,
                FolioCorteCaja = venta.FolioCorteCaja,
                TipoMovimiento = venta.TipoMovimiento,
                Descripcion = venta.Descripcion,
                Ingreso = venta.Ingreso,
                Egreso = venta.Egreso,
                Saldo = venta.Saldo,
                PuntoVenta = venta.PuntoVenta,
                OperadorChoferNombre = venta.OperadorChoferNombre,
                FechaAplicacion = venta.FechaAplicacion,
                FechaRegistro = venta.FechaRegistro,
            };
        }

        public static VentaCorteAnticipoEC FromEntity(VentaCorteAnticipoEC venta)
        {
            return new VentaCorteAnticipoEC()
            {
                IdEmpresa = venta.IdEmpresa,
                Year = venta.Year,
                Mes = venta.Mes,
                Dia = venta.Dia,
                Orden = venta.Orden,
                IdTipoOperacion = venta.IdTipoOperacion,
                IdPuntoVenta = venta.IdPuntoVenta,
                IdCAlmacenGas = venta.IdCAlmacenGas,
                IdOperadorChofer = venta.IdOperadorChofer,
                IdUsuarioRecibe = venta.IdUsuarioRecibe,
                FolioOperacionDia = venta.FolioOperacionDia,
                FolioOperacion = venta.FolioOperacion,
                TotalVenta = venta.TotalVenta,
                TotalAnticipado = venta.TotalAnticipado,
                MontoRecortadoAnticipado = venta.MontoRecortadoAnticipado,
                TipoOperacion = venta.TipoOperacion,
                PuntoVenta = venta.PuntoVenta,
                OperadorChofer = venta.OperadorChofer,
                UsuarioRecibe = venta.UsuarioRecibe,
                DatosProcesados = venta.DatosProcesados,
                FechaCorteAnticipo = venta.FechaCorteAnticipo,
                FechaAplicacion = venta.FechaAplicacion,
                FechaRegistro = venta.FechaRegistro,
            };
        }

        public static AlmacenGasMovimiento FromEntity(UnidadAlmacenGas unidadSalida, Empresa empresa, AlmacenGasMovimientoDto Dto, Decimal Linicial, Decimal Lfinal)
        {
            decimal unidadSalidaCantidadKg = unidadSalida.CantidadActualKg;
            decimal unidadSalidaCantidadLt = unidadSalida.CantidadActualLt;
            decimal unidadSalidaPorcentaje = unidadSalida.PorcentajeActual;

            AlmacenGasMovimiento ulMov = AlmacenGasServicio.ObtenerUltimoMovimientoEnInventario(Dto.IdEmpresa, Dto.IdAlmacenGas, Dto.FechaAplicacion);
            AlmacenGasMovimiento ulMovSalida = AlmacenGasServicio.ObtenerUltimoMovimientoPorUnidadAlmacenGas(Dto.IdEmpresa, Dto.IdCAlmacenGasPrincipal, Dto.IdTipoEvento.Value, Dto.IdTipoMovimiento, Dto.FechaAplicacion);//ObtenerUltimoMovimientoEnInventario(Dto.IdEmpresa, Dto.IdAlmacenGas, Dto.FechaAplicacion);
            //AlmacenGasMovimiento ulMovSalida = AlmacenGasServicio.ObtenerUltimoMovimientoEnInventario(Dto.IdEmpresa, Dto.IdAlmacenGas, Dto.FechaAplicacion);//AlmacenGasServicio.ObtenerUltimoMovimientoEnInventario(Dto.IdEmpresa, Dto.IdAlmacenGas, Dto.IdTipoEvento ?? 0, TipoMovimientoEnum.Salida, Dto.FechaAplicacion);
            //AlmacenGasServicio.ObtenerUltimoMovimientoDeVenta(Dto.IdEmpresa, unidadSalida.IdCAlmacenGas, Dto.FechaAplicacion);

            decimal MaganatelLtIni = 0;
            decimal MaganatelLtFin = 0;
            decimal kilogramosRemanentes = 0;//CalcularPreciosVentaServicio.ObtenerKilogramosRemanentes(ulMovSalida.P5000Actual.Value, Dto.P5000Actual.Value, MaganatelLtIni, MaganatelLtFin);
            decimal litrosRemanentes = CalcularGasServicio.ObtenerLitrosDesdeKilos(kilogramosRemanentes, empresa.FactorLitrosAKilos);
            decimal UltimaVentaDiaKg = ulMovSalida.VentaDiaKg ?? 0; //UltimaVentaDiaKg
            decimal UltimaVentaMesKg = ulMovSalida.VentaMesKg ?? 0;
            decimal UltimaVentaAnioKg = ulMovSalida.VentaAnioKg ?? 0;
            decimal UltimaVentaDiaLt = ulMovSalida.VentaDiaLt ?? 0;
            decimal UltimaVentaMesLt = ulMovSalida.VentaMesLt ?? 0;
            decimal UltimaVentaAnioLt = ulMovSalida.VentaAnioLt ?? 0;

            AlmacenGasMovimiento x = new AlmacenGasMovimiento();

            x.IdEmpresa = Dto.IdEmpresa;
            x.Year = Dto.Year;
            x.Mes = Dto.Mes;
            x.Dia = Dto.Dia;
            x.Orden = AlmacenGasServicio.ObtenerOrdenMovimientoEnInventario(Dto.IdEmpresa, Dto.IdAlmacenGas, Dto.FechaAplicacion);
            x.IdTipoMovimiento = Dto.IdTipoMovimiento;
            x.IdTipoEvento = Dto.IdTipoEvento;
            x.IdOrdenVenta = Dto.IdOrdenVenta;
            x.IdAlmacenGas = Dto.IdAlmacenGas;
            x.IdCAlmacenGasPrincipal = unidadSalida.IdCAlmacenGas;
            x.IdCAlmacenGasReferencia = Dto.IdCAlmacenGasReferencia;
            x.IdAlmacenEntradaGasDescarga = Dto.IdAlmacenEntradaGasDescarga;
            x.IdAlmacenGasRecarga = Dto.IdAlmacenGasRecarga;
            x.FolioOperacionDia = Dto.FolioOperacionDia;
            x.CAlmacenPrincipalNombre = unidadSalida.Numero;
            x.CAlmacenReferenciaNombre = Dto.CAlmacenReferenciaNombre;
            x.OperadorChoferNombre = Dto.OperadorChoferNombre;
            x.TipoEvento = Dto.TipoEvento;
            x.TipoMovimiento = Dto.TipoMovimiento;
            x.EntradaKg = 0;
            x.EntradaLt = 0;
            x.SalidaKg = CalcularPreciosVentaServicio.ObtenerLtVenta(Dto.IdEmpresa, Dto.Year, Dto.Mes, Dto.Dia, Dto.Orden, "Kg");
            x.SalidaLt = CalcularPreciosVentaServicio.ObtenerLtVenta(Dto.IdEmpresa, Dto.Year, Dto.Mes, Dto.Dia, Dto.Orden, "Lt");
            x.CantidadAnteriorKg = unidadSalidaCantidadKg;
            x.CantidadAnteriorLt = unidadSalidaCantidadLt;
            x.CantidadActualKg = CalcularGasServicio.RestarKilogramos(unidadSalidaCantidadKg, x.SalidaKg);
            x.CantidadActualLt = CalcularGasServicio.RestarKilogramos(unidadSalida.CantidadActualKg, empresa.FactorLitrosAKilos);
            x.PorcentajeAnterior = unidadSalidaPorcentaje;
            if (unidadSalida.CapacidadTanqueLt != null && unidadSalida.CapacidadTanqueLt.Value != 0)
            {
                x.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(unidadSalida.CapacidadTanqueLt.Value, unidadSalida.CantidadActualLt);
            }
            else
            {
                x.PorcentajeActual = 0;
            }
            x.P5000Anterior = Linicial;//ulMovSalida.P5000Actual;
            x.P5000Actual = Lfinal;//ulMovSalida.P5000Actual;
            x.CantidadAcumuladaDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.VentaAcumDiaKg, x.SalidaKg);
            x.CantidadAcumuladaDiaLt = CalcularGasServicio.SumarLitros(ulMov.VentaAcumDiaLt, x.SalidaLt);
            x.CantidadAcumuladaMesKg = CalcularGasServicio.SumarKilogramos(ulMov.VentaAcumMesKg, x.SalidaKg);
            x.CantidadAcumuladaMesLt = CalcularGasServicio.SumarLitros(ulMov.VentaAcumMesLt, x.SalidaLt);
            x.CantidadAcumuladaAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.VentaAcumAnioKg, x.SalidaKg);
            x.CantidadAcumuladaAnioLt = CalcularGasServicio.SumarLitros(ulMov.VentaAcumAnioLt, x.SalidaLt);

            x.RemaKg = kilogramosRemanentes;
            x.RemaLt = litrosRemanentes;
            x.RemaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.RemaDiaKg != null ? ulMovSalida.RemaDiaKg.Value : 0, kilogramosRemanentes);
            x.RemaDiaLt = CalcularGasServicio.SumarLitros(ulMovSalida.RemaDiaLt != null ? ulMovSalida.RemaDiaLt.Value : 0, litrosRemanentes);
            x.RemaMesKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.RemaMesKg != null ? ulMovSalida.RemaMesKg.Value : 0, kilogramosRemanentes);
            x.RemaMesLt = CalcularGasServicio.SumarLitros(ulMovSalida.RemaMesLt != null ? ulMovSalida.RemaMesLt.Value : 0, litrosRemanentes);
            x.RemaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.RemaAnioKg != null ? ulMovSalida.RemaAnioKg.Value : 0, kilogramosRemanentes);
            x.RemaAnioLt = CalcularGasServicio.SumarLitros(ulMovSalida.RemaAnioLt != null ? ulMovSalida.RemaAnioLt.Value : 0, litrosRemanentes);
            x.RemaAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.RemaAcumDiaKg, kilogramosRemanentes);
            x.RemaAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov != null ? ulMov.RemaAcumDiaLt : 0, litrosRemanentes);
            x.RemaAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov != null ? ulMov.RemaAcumMesKg : 0, kilogramosRemanentes);
            x.RemaAcumMesLt = CalcularGasServicio.SumarLitros(ulMov != null ? ulMov.RemaAcumMesLt : 0, litrosRemanentes);
            x.RemaAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov != null ? ulMov.RemaAcumAnioKg : 0, kilogramosRemanentes);
            x.RemaAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov != null ? ulMov.RemaAcumAnioLt : 0, litrosRemanentes);
            x.FechaAplicacion = Dto.FechaAplicacion;
            x.FechaRegistro = DateTime.Now;
            /***************/
            x.VentaKg = CalcularPreciosVentaServicio.ObtenerLtVenta(Dto.IdEmpresa, Dto.Year, Dto.Mes, Dto.Dia, Dto.Orden, "Kg");
            x.VentaLt = CalcularPreciosVentaServicio.ObtenerLtVenta(Dto.IdEmpresa, Dto.Year, Dto.Mes, Dto.Dia, Dto.Orden, "Lt");
            x.VentaDiaKg = CalcularGasServicio.SumarKilogramos(x.VentaKg ?? 0, UltimaVentaDiaKg);
            x.VentaDiaLt = CalcularGasServicio.SumarLitros(x.VentaLt ?? 0, UltimaVentaDiaLt);
            x.VentaMesKg = CalcularGasServicio.SumarKilogramos(x.VentaKg ?? 0, UltimaVentaMesKg);
            x.VentaMesLt = CalcularGasServicio.SumarLitros(x.VentaLt ?? 0, UltimaVentaMesLt);
            x.VentaAnioKg = CalcularGasServicio.SumarKilogramos(x.VentaKg ?? 0, UltimaVentaAnioKg);
            x.VentaAnioLt = CalcularGasServicio.SumarLitros(x.VentaLt ?? 0, UltimaVentaDiaLt);
            x.VentaAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.VentaAcumDiaKg, x.VentaKg ?? 0);
            x.VentaAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov.VentaAcumDiaLt, x.VentaLt ?? 0);
            x.VentaAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov.VentaAcumMesKg, x.VentaKg ?? 0);
            x.VentaAcumMesLt = CalcularGasServicio.SumarLitros(ulMov.VentaAcumMesLt, x.VentaLt ?? 0);
            x.VentaAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.VentaAcumAnioKg, x.VentaKg ?? 0);
            x.VentaAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov.VentaAcumAnioLt, x.VentaLt ?? 0);
            x.VentaLecturasP5000Kg = 0;
            x.VentaLecturasP5000Lt = 0;
            x.VentaLecturasMagnatelKg = 0;
            x.VentaLecturasMagnatelLt = 0;
            x.VentaLecturasP5000MesKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.VentaLecturasP5000MesKg ?? 0, Dto.VentaLecturasP5000MesKg ?? 0);
            x.VentaLecturasP5000MesLt = CalcularGasServicio.SumarLitros(ulMovSalida.VentaLecturasP5000MesLt ?? 0, Dto.VentaLecturasP5000MesLt ?? 0);
            x.VentaLecturasMagnatelMesKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.VentaLecturasMagnatelMesKg ?? 0, Dto.VentaLecturasMagnatelMesKg ?? 0);
            x.VentaLecturasMagnatelMesLt = CalcularGasServicio.SumarLitros(ulMovSalida.VentaLecturasMagnatelMesLt ?? 0, Dto.VentaLecturasMagnatelMesLt ?? 0);
            x.VentaLecturasP5000AnioKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.VentaLecturasP5000AnioKg ?? 0, Dto.VentaLecturasP5000AnioKg ?? 0);
            x.VentaLecturasP5000AnioLt = CalcularGasServicio.SumarLitros(ulMovSalida.VentaLecturasP5000AnioLt ?? 0, Dto.VentaLecturasP5000AnioLt ?? 0);
            x.VentaLecturasMagnatelAnioKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.VentaLecturasMagnatelAnioKg ?? 0, Dto.VentaLecturasMagnatelAnioKg ?? 0);
            x.VentaLecturasMagnatelAnioLt = CalcularGasServicio.SumarLitros(ulMovSalida.VentaLecturasMagnatelAnioLt ?? 0, Dto.VentaLecturasMagnatelAnioLt ?? 0);

            return x;

        }

        public static VPuntoVentaDetalleDTO ToDto(VentaPuntoDeVentaDetalle pv, decimal totalCilindros, decimal P5000Inicial, decimal P5000Final, decimal PorcentajeInicial, decimal PorcentajeFinal)
        {
            VPuntoVentaDetalleDTO usDTO = new VPuntoVentaDetalleDTO()
            {
                IdEmpresa = pv.IdEmpresa,
                Year = pv.Year,
                Mes = pv.Mes,
                Dia = pv.Dia,
                Orden = pv.Orden,
                OrdenDetalle = pv.OrdenDetalle,
                IdProducto = pv.IdProducto,
                IdProductoLinea = pv.IdProductoLinea,
                IdCategoria = pv.IdCategoria,
                PrecioUnitarioLt = pv.PrecioUnitarioLt,
                PrecioUnitarioKg = pv.PrecioUnitarioKg,
                DescuentoUnitarioLt = pv.DescuentoUnitarioLt,
                DescuentoUnitarioKg = pv.DescuentoUnitarioKg,
                CantidadLt = pv.CantidadLt,
                CantidadKg = pv.CantidadKg,
                DescuentoTotal = pv.DescuentoTotal,
                Subtotal = pv.Subtotal,
                ProductoDescripcion = pv.ProductoDescripcion,
                ProductoLinea = pv.ProductoLinea,
                ProductoCategoria = pv.ProductoCategoria,
                FechaRegistro = pv.FechaRegistro,
                IdUnidadMedida = pv.IdUnidadMedida,
                PrecioUnitarioProducto = pv.PrecioUnitarioProducto,
                DescuentoUnitarioProducto = pv.DescuentoUnitarioProducto,
                CantidadProducto = pv.CantidadProducto,
                //Camioneta Reporte del dia
                CantidadTotalProd = totalCilindros,
                P5000Inicial = P5000Inicial,
                P5000Final = P5000Final,
                PorcentajeInicial = PorcentajeInicial,
                PorcentajeFinal = PorcentajeFinal,
            };
            return usDTO;

        }

        public static VPuntoVentaDetalleDTO ToDto(VentaCajaGeneral entidad, string Folio, decimal LtVendidos, decimal PrecioLt, DateTime fecha, decimal P5000Inicial, decimal P5000Final, decimal PorcentajeInicial, decimal PorcentajeFinal)
        {
            VPuntoVentaDetalleDTO usDTO = new VPuntoVentaDetalleDTO()
            {
                //Camioneta Reporte del dia Pipa

                P5000Inicial = P5000Inicial,
                P5000Final = P5000Final,
                PorcentajeInicial = PorcentajeInicial,
                PorcentajeFinal = PorcentajeFinal,
                FolioOperacion = Folio,
                LitrosVendidos = LtVendidos,
                PrecioLitro = PrecioLt,
                totalCredito = entidad.VentaTotalCredito,
                totalContado = entidad.VentaTotalContado,
                FechaRegistro = fecha,
            };
            return usDTO;

        }

        public static ReporteDiaDTO ToDTOC(VPuntoVentaDetalleDTO pv, VentaCajaGeneral entidad, UnidadAlmacenGas almacen, List<TanquesDto> tanques, decimal k)
        {

            ReporteDiaDTO usDTO = new ReporteDiaDTO()
            {
                IdCAlmacenGas = almacen.IdCamioneta.Value,
                NombreCAlmacen = almacen.Numero,
                ClaveReporte = entidad.FolioOperacionDia,
                Fecha = pv.FechaRegistro,
                Carburacion = 0,
                KilosDeVenta = k,
                Precio = pv.PrecioUnitarioKg.Value,
                OtrasVentasTotal = 0,
                Importe = entidad.VentaTotalContado,
                ImporteCredito = entidad.VentaTotalCredito,
                OtrasVentas = new List<OtrasVentasDto>(),
                Tanques = tanques

            };


            return usDTO;
        }

        public static List<ReporteDiaDTO> ToDtoC(List<VPuntoVentaDetalleDTO> lu, VentaCajaGeneral entidad, UnidadAlmacenGas almacen, List<TanquesDto> tanques)
        {
            var capacidad = 0;

            //Obtener Kilos venta
            decimal kgVentas = 0;
            foreach (var x in tanques)
            {
                if (x.NombreTanque == "20 kg")
                {
                    capacidad = 20;
                }
                if (x.NombreTanque == "30 kg")
                {
                    capacidad = 30;
                }
                else
                {
                    capacidad = 45;
                }
                kgVentas += CalcularPreciosVentaServicio.ObtenerKilosVenta(capacidad, x.Normal);
            }
            List<ReporteDiaDTO> luDTO = lu.ToList().Select(x => ToDTOC(x, entidad, almacen, tanques, kgVentas)).ToList();
            return luDTO;
        }
    }
}
