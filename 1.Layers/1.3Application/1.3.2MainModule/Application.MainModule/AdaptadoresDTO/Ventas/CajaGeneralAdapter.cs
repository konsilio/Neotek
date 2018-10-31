using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacenes;
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
        public static VentaPuntoVentaDTO ToDTOC(VentaPuntoDeVenta pv)
        {
            var vt = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotal;

            var vtc = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotalCredito;
            var vtco = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotalContado;
            var ov = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).OtrasVentas;

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

        public static VentaPuntoDeVenta FromDto(VentaPuntoVentaDTO pvDTO)
        {
            return new VentaPuntoDeVenta()
            {
                IdEmpresa = pvDTO.IdEmpresa,
                Year = pvDTO.Year,
                Mes = pvDTO.Mes,
                Dia = pvDTO.Dia,
                Orden = pvDTO.Orden,
                IdPuntoVenta = pvDTO.IdPuntoVenta,
                IdCliente = pvDTO.IdCliente,
                IdOperadorChofer = pvDTO.IdOperadorChofer,
                IdTipoVenta = pvDTO.IdTipoVenta,
                IdFactura = pvDTO.IdFactura,
                FolioOperacionDia = pvDTO.FolioOperacionDia,
                FolioVenta = pvDTO.FolioVenta,
                RequiereFactura = pvDTO.RequiereFactura,
                VentaACredito = pvDTO.VentaACredito,
                Subtotal = pvDTO.Subtotal,
                Descuento = pvDTO.Descuento,
                Iva = pvDTO.Iva,
                Total = pvDTO.Total,
                PorcentajeIva = pvDTO.PorcentajeIva,
                EfectivoRecibido = pvDTO.EfectivoRecibido,
                CambioRegresado = pvDTO.CambioRegresado,
                PuntoVenta = pvDTO.PuntoVenta,
                RazonSocial = pvDTO.RazonSocial,
                RFC = pvDTO.RFC,
                ClienteConCredito = pvDTO.ClienteConCredito,
                OperadorChofer = pvDTO.OperadorChofer,
                DatosProcesados = true,
                FechaRegistro = DateTime.Now
            };
        }

        public static List<VentaPuntoDeVenta> FromDto(List<VentaPuntoVentaDTO> DTO)
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

        public static AlmacenGasMovimiento FromEntity(UnidadAlmacenGas unidadSalida, Empresa empresa, AlmacenGasMovimientoDto Dto)
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
            decimal kilogramosRemanentes = CalcularPreciosVentaServicio.ObtenerKilogramosRemanentes(Dto.P5000Anterior ?? 0, Dto.P5000Actual ?? 0, MaganatelLtIni, MaganatelLtFin);
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
            x.P5000Anterior = ulMovSalida.P5000Actual;//ulMovSalida.P5000Actual;
            x.P5000Actual = ulMovSalida.P5000Actual;//ulMovSalida.P5000Actual;
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


    }
}
