using Application.MainModule.DTOs.Ventas;
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
    }
}
