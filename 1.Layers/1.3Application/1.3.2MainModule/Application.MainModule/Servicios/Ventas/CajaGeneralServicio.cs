using Application.MainModule.AdaptadoresDTO.Ventas;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Ventas
{
    public class CajaGeneralServicio
    {
        public static List<CajaGeneralDTO> Obtener()
        {
            List<CajaGeneralDTO> lPventas = AdaptadoresDTO.Ventas.CajaGeneralAdapter.ToDTO(new CajaGeneralDataAccess().BuscarTodos());
            return lPventas;
        }

        public static List<CajaGeneralDTO> ObtenerIdEmp(short IdEmpresa)
        {
            List<CajaGeneralDTO> lPventas = AdaptadoresDTO.Ventas.CajaGeneralAdapter.ToDTO(new CajaGeneralDataAccess().Buscar(IdEmpresa));
            return lPventas;
        }
        public static VentaCajaGeneral ObtenerCG(string cve)
        {
            return new CajaGeneralDataAccess().BuscarGralPorCve(cve);
        }
        public static List<VentaPuntoVentaDTO> ObtenerPV(string cve)
        {
            List<VentaPuntoVentaDTO> lPventas = AdaptadoresDTO.Ventas.CajaGeneralAdapter.ToDTOC(new CajaGeneralDataAccess().BuscarPorCve(cve));
            return lPventas;
        }

        public static List<VentaCorteAnticipoDTO> ObtenerCE(string cve)
        {
            List<VentaCorteAnticipoDTO> lPventas = AdaptadoresDTO.Ventas.CajaGeneralAdapter.ToDTOCE(new CajaGeneralDataAccess().BuscarPorCveEC(cve));
            return lPventas;
        }

        public static RespuestaDto Actualizar(List<VentaPuntoDeVenta> pv)
        {
            return new CajaGeneralDataAccess().Actualizar(pv);
        }

        public static RespuestaDto Actualizar(List<VentaCorteAnticipoEC> pv)
        {
            return new CajaGeneralDataAccess().Actualizar(pv);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El Reporte del dia");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }

        public static void ProcesarSaldos()
        {
            CalcularSaldoMovimientoVentas();
        }

        public static List<VentaMovimiento> CalcularSaldoMovimientoVentas()
        {
            List<VentaMovimiento> movimientos = new List<VentaMovimiento>();
            List<PuntoVenta> punto = ObtenerPuntosVenta();
            foreach (var pvx in punto)
            {
                movimientos = ObtenerVentaMovimiento(pvx.IdPuntoVenta);
                if (movimientos != null && movimientos.Count > 0)
                {
                    movimientos.ForEach(x => movimientos.Add(ActualizarSaldos(x)));
                }
            }
            return movimientos;
        }
        public static List<VentaMovimiento> ObtenerVentaMovimiento(int puntoventa)
        {
            return new CajaGeneralDataAccess().Buscar(puntoventa);
        }

        public static List<PuntoVenta> ObtenerPuntosVenta()
        {
            return new PuntoVentaDataAccess().BuscarTodos();
        }

        public static VentaMovimiento ActualizarSaldos(VentaMovimiento vm)
        {
            VentaMovimiento Updt = new VentaMovimiento();
            if (vm.Saldo != 0)
            {
                vm.Saldo = CalcularPreciosVentaServicio.ObtenerSaldoActual(vm.IdPuntoVenta);
                if (vm.Ingreso > 0)
                {
                    Updt.Saldo = CalcularPreciosVentaServicio.ObtenerSumaSaldoVenta(vm.Ingreso, vm.Saldo);
                }
                else if (vm.Egreso > 0)
                {
                    Updt.Saldo = CalcularPreciosVentaServicio.ObtenerSaldoVentaEgreso(vm.Egreso, vm.Saldo);
                }
                new CajaGeneralDataAccess().Actualizar(Updt);
            }

            return Updt;
        }

        public static void CargarVentasMovimientos()
        {
            //bool noProcesados = false;

            //var ventas = ObtenerVentasPuntosVenta().Where(x=> x.DatosProcesados.Equals(noProcesados));
            //_lus.OrderByDescending(x => x.FechaAplicacion).ToList();
            var ventas = ObtenerVentasCortesAnticipos();
            if (ventas != null && ventas.Count() > 0)
            {
                var rep = CajaGeneralAdapter.FromDtoVtaM(ventas);
               new CajaGeneralDataAccess().Insertar(rep);

            }
        }

        public static List<VentaPuntoDeVenta> ObtenerVentasPuntosVenta()
        {
            return new CajaGeneralDataAccess().Buscar();
        }
        public static List<VentaCorteAnticipoEC> ObtenerVentasCorteAnticipo()
        {
            return new CajaGeneralDataAccess().BuscarAnticiposC();
        }

        public static List<RegistrarVentasMovimientosDTO> ObtenerVentasCortesAnticipos()
        {
            List<RegistrarVentasMovimientosDTO> _lst = new List<RegistrarVentasMovimientosDTO>();
            bool noProcesados = false;
            bool Procesados = false;
            List<VentaPuntoDeVenta> VentasPV = ObtenerVentasPuntosVenta().Where(x => x.DatosProcesados.Equals(noProcesados)).OrderByDescending(x => x.FechaRegistro).ToList();
            List<VentaCorteAnticipoEC> VentasCortes = ObtenerVentasCorteAnticipo().Where(x => x.DatosProcesados.Equals(noProcesados)).OrderByDescending(x => x.FechaRegistro).ToList();
            if ((VentasPV != null && VentasPV.Count() > 0)|| (VentasCortes != null && VentasCortes.Count() > 0))
            {
                _lst = MergedLst(VentasPV, VentasCortes);               
                VentasPV.ForEach(x => x.DatosProcesados = Procesados);
               new CajaGeneralDataAccess().Actualizar(VentasPV);
            }
            return _lst;
        }

        public static List<RegistrarVentasMovimientosDTO> MergedLst(List<VentaPuntoDeVenta> pv, List<VentaCorteAnticipoEC> vca)
        {
            List<VentaPuntoDeVenta> Ventas = pv.AsEnumerable()
                                     .Select(o => new VentaPuntoDeVenta
                                     {
                                         IdEmpresa = o.IdEmpresa,
                                         Year = o.Year,
                                         Mes = o.Mes,
                                         Dia = o.Dia,
                                         Orden = o.Orden,
                                         IdPuntoVenta = o.IdPuntoVenta,
                                         IdCliente = o.IdCliente,
                                         IdOperadorChofer = o.IdOperadorChofer,
                                         FolioOperacionDia = o.FolioOperacionDia,
                                         FolioVenta = o.FolioVenta,
                                         Total = o.Total,
                                         PuntoVenta = o.PuntoVenta,
                                         OperadorChofer = o.OperadorChofer,
                                         FechaRegistro = o.FechaRegistro,
                                     }).ToList();

            List<RegistrarVentasMovimientosDTO> lstFinal = pv.Select(v => new RegistrarVentasMovimientosDTO()
            {
                IdEmpresa = v.IdEmpresa,
                Year = v.Year,
                Mes = v.Mes,
                Dia = v.Dia,
                Orden = v.Orden,
                IdPuntoVenta = v.IdPuntoVenta,
                IdCliente = v.IdCliente,
                IdOperadorChofer = v.IdOperadorChofer,
                FolioOperacionDia = v.FolioOperacionDia,
                FolioVenta = v.FolioVenta,
                Ingreso = v.Total,
                PuntoVenta = v.PuntoVenta,
                OperadorChoferNombre = v.OperadorChofer,
                FechaRegistro = v.FechaRegistro,
            }).ToList();

            return lstFinal;
        }
               
    }
}
