using Application.MainModule.AdaptadoresDTO.Ventas;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using Application.MainModule.Servicios.Ventas;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.MainModule;

namespace Application.MainModule.Flujos
{
    public class Ventas
    {
        public List<CajaGeneralDTO> CajaGeneral()
        {
            //  CajaGeneralServicio.ProcesarMovimientoVentas();
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            if (TokenServicio.EsSuperUsuario())
                return CajaGeneralServicio.Obtener();

            else
                return CajaGeneralServicio.Obtener();
        }
        public List<AlmacenGasMovimientoDto> MovimientosGas(short unidad, short empresa, short year, byte month, byte dia, short? orden, string Folio)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            return CajaGeneralServicio.ObtenerPVDetalle(unidad, empresa, year, month, dia, orden.Value, Folio).ToList();
        }
        public List<VentasPipaDto> MovimientosGas(short unidad, short empresa, short year, byte month, byte dia, short? orden, DateTime fecha, string Folio)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            return CajaGeneralServicio.ObtenerVentasPipas(unidad, empresa, year, month, dia, orden.Value, fecha, Folio).ToList();
        }
        public List<VPuntoVentaDetalleDTO> MovimientosGasCilindro(short? empresa, short year, byte month, byte dia, short? orden)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            return CajaGeneralServicio.ObtenerVentas(empresa.Value, year, month, dia, orden.Value).ToList();
        }
        public List<CajaGeneralDTO> CajaGeneralIdEmpresa(short IdEmpresa)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;
            return CajaGeneralServicio.ObtenerIdEmp(IdEmpresa).ToList();
        }
        public CorteCajaDTO CajaGeneral(string cveReporte)
        {
            CorteCajaDTO corte = new CorteCajaDTO();
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            var precio = PrecioVentaGasServicio.ObtenerPrecioVigente(TokenServicio.ObtenerIdEmpresa());
            if (!resp.Exito) return null;
            var reporteDia = CajaGeneralServicio.ObtenerReporteDia(cveReporte);
            if (reporteDia == null)
                return corte;
            corte.Tickets = CajaGeneralServicio.ObtenerVPV(reporteDia).ToList();
            var lecturas = AlmacenGasServicio.ObtenerLecturas(reporteDia.IdCAlmacenGas.Value, reporteDia.FechaReporte);
            corte.Fecha = reporteDia.FechaReporte;
            corte.NombreUnidad = reporteDia.CAlmacenGas.Numero;
            corte.IdPuntoVenta = reporteDia.IdPuntoVenta ?? 0;
            corte.OperadorChofer = reporteDia.OperadorChofer;
            corte.TipoUnidad = 1;
            if (reporteDia.CAlmacenGas.IdCamioneta != null)
            {
                corte.TipoUnidad = 2;
                var li = lecturas.FirstOrDefault(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial));
                var lf = lecturas.FirstOrDefault(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final));

                foreach (var cil in li.Cilindros)
                {
                    VentasPipaDto lects = new VentasPipaDto();
                    lects.Concepto = Math.Truncate(cil.Cilindro.CapacidadKg).ToString() + "Kg";
                    lects.P5000Inicial = cil.Cantidad;
                    lects.P5000Final = lf.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(cil.IdCilindro)).Cantidad;
                    lects.CantidadLt = CalculosGenerales.DiferenciaEntreDosNumero(lects.P5000Inicial, lects.P5000Final);
                    lects.Venta = (lects.CantidadLt * cil.Cilindro.CapacidadKg) * precio.PrecioSalidaKg.Value;
                    corte.Lecturas.Add(lects);
                }                
            }
            if (reporteDia.CAlmacenGas.IdPipa != null || reporteDia.CAlmacenGas.IdEstacionCarburacion != null)
            {
                if (reporteDia.CAlmacenGas.IdPipa != null)
                    corte.TipoUnidad = 3;
                var li = lecturas.FirstOrDefault(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial));
                var lf = lecturas.FirstOrDefault(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final));

                VentasPipaDto lects = new VentasPipaDto();
                lects.Concepto = "Litros";
                lects.P5000Inicial = li.P5000 ?? 0;
                lects.P5000Final = lf.P5000 ?? 0;
                lects.CantidadLt = CalculosGenerales.DiferenciaEntreDosNumero(lects.P5000Inicial, lects.P5000Final);
                lects.Venta = lects.CantidadLt  * precio.PrecioSalidaKg.Value;
                corte.Lecturas.Add(lects);
            }
            corte.TotalVenta = corte.Tickets.Sum(x => x.Total);
            corte.TotalContado = corte.Tickets.Where(x => x.VentaACredito.Equals(false)).Sum(v => v.Total);
            corte.TotalCredito = corte.Tickets.Where(x => x.VentaACredito.Equals(true)).Sum(v => v.Total);
            corte.TotalOtros = 0;
            corte.Descuentos = 0;

            return corte;
        }
        public List<VentaPuntoDeVenta> CajaGeneralCamioneta(DateTime fecha)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;
            return CajaGeneralServicio.ObtenerTotalVentasCamioneta(fecha).ToList();
        }
        public List<VentaPuntoDeVenta> CajaGeneralEstacion(DateTime fecha)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;
            return CajaGeneralServicio.ObtenerTotalVentasEstaciones(fecha).ToList();
        }
        public List<VentaCorteAnticipoDTO> CajaGeneralEstacion(string cveReporte)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            return CajaGeneralServicio.ObtenerCE(cveReporte).ToList();
        }
        public RespuestaDto GuardarReporteLiquidado(VentaPuntoVentaDTO Dto)
        {
            var resp = PermisosServicio.PuedeModificarCajaGeneral();
            if (!resp.Exito) return resp;

            var reporte = CajaGeneralServicio.ObtenerPV(Dto.FolioOperacionDia).ToList();
            if (reporte == null) return CajaGeneralServicio.NoExiste();

            var rcg = CajaGeneralServicio.ObtenerCG(Dto.FolioOperacionDia);

            var rep = CajaGeneralAdapter.FromDto(rcg);

            return CajaGeneralServicio.Actualizar(rep);
        }
        public RespuestaDto GuardarReporteLiquidadoEst(VentaCorteAnticipoDTO Dto)
        {
            var resp = PermisosServicio.PuedeModificarCajaGeneral();
            if (!resp.Exito) return resp;

            var reporte = CajaGeneralServicio.ObtenerCE(Dto.FolioOperacion).ToList();
            if (reporte == null) return CajaGeneralServicio.NoExiste();

            var rep = CajaGeneralAdapter.FromDtoCE(reporte);
            return CajaGeneralServicio.Actualizar(rep);
        }
    }
}
