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
            bool noProcesados = false;

            var ventas = ObtenerVentasPuntosVenta().Where(x=> x.DatosProcesados.Equals(noProcesados));
            if (ventas != null && ventas.Count() > 0)
            {

            }
        }

        public static List<VentaPuntoDeVenta> ObtenerVentasPuntosVenta()
        {
            return new CajaGeneralDataAccess().Buscar();
        }
    }
}
