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
            //var vt = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotal;
            //var vtc = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotalCredito;
            //var vtco = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).VentaTotalContado;
            //var ov = CajaGeneralServicio.ObtenerCG(pv.FolioOperacionDia).OtrasVentas;

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
        public static List<VentaPuntoVentaDTO> ToDTOP(List<VentaPuntoDeVenta> lu)
        {
            List<VentaPuntoVentaDTO> luDTO = lu.ToList().Select(x => ToDTOC(x)).ToList();
            return luDTO;
        }
    }
}
