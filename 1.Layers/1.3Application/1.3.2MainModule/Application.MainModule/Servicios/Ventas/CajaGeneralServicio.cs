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

        public static void ProcesarVentasPuntosDeVenta()
        {
            ProcesarMovimientoVentas();
        }

        public static void ProcesarMovimientoVentas()
        {
            List<VentaPuntoDeVenta> ventaspv = ObtenerVentasPuntosVentaNoProc();//obtenerVentasPuntoVentaNoProc no procesadas

            if (ventaspv != null && ventaspv.Count > 0)
            {
                ActualizarTotalesVentas(ventaspv); //se actualizan totales de VentasPuntoVenta
                CargarAVentasMovimientos(ventaspv);//guardar Ventas (de VentaPuntoDeVenta) a Tabla VentasMovimiento
            }

            List<VentaCorteAnticipoEC> CortesAnticipos = ObtenerVentasCorteAnticipoNoProc();//Obtener existencia de anticipos no procesados
            if (CortesAnticipos != null && CortesAnticipos.Count() > 0)
            {
                CargarAnticiposAMovimientos(CortesAnticipos);//Guardar Anticipos en Tabla VentasMovimiento
            }

        }
        public static VentaMovimiento ObtenerVentaMovimiento(int puntoventa, short orden)
        {
            return new CajaGeneralDataAccess().Buscar(puntoventa).Where(x => x.Orden.Equals(orden)).FirstOrDefault();
        }

        public static List<VentaMovimiento> ObtenerListaMovimientos(int puntoventa)
        {
            return new CajaGeneralDataAccess().Buscar(puntoventa);
        }
        public static VentaMovimiento ObtenerVentasMovimientos(int puntoventa, short orden)
        {
            return new CajaGeneralDataAccess().Buscar(puntoventa).Where(x => x.Orden.Equals(orden)).FirstOrDefault();
        }
        public static VentaPuntoDeVenta ObtenerPuntoVenta(int puntoventa, short orden)
        {
            return new CajaGeneralDataAccess().BuscarPorPV(puntoventa).Where(x => x.Orden.Equals(orden)).FirstOrDefault();
        }
        public static List<VentaPuntoDeVenta> ObtenerPuntosVenta()
        {
            return new CajaGeneralDataAccess().Buscar();
        }
        
        public static void ActualizarTotalesVentas(List<VentaPuntoDeVenta> vm)
        {
            VentaPuntoDeVenta Updt = new VentaPuntoDeVenta();
            // List<VentaPuntoDeVenta> lst = vm.GroupBy(x => x.IdPuntoVenta).SelectMany(gr => gr).ToList(); //VentaspuntosDeVentaAgrupados- por punto de venta
            var lst = vm.GroupBy(x => x.IdPuntoVenta).ToList(); //VentaspuntosDeVentaAgrupados- por punto de venta
            //Actualizar Total Dia      
            foreach (var _lst in lst)
            {
                int position = 0;
                foreach (var item in _lst)
                {
                    position++;
                    Updt = ObtenerPuntoVenta(item.IdPuntoVenta, item.Orden);
                    if (item.Total > 0)
                    {
                        item.TotalDia = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, item.Orden, position, "TotalDia"); //Obtener Saldo actual por punto de venta - TotalDia
                        item.TotalMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, item.Orden, position, "TotalMes"); //Obtener Saldo actual por punto de venta - TotalMes
                        item.TotalAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, item.Orden, position, "TotalAnio"); //Obtener Saldo actual por punto de venta - TotalAnio

                        Updt.TotalDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, item.TotalDia); //se agrega el Total de venta al TotalDia por punto de venta
                        Updt.TotalMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, item.TotalMes); //se agrega el Total de venta al TotalMes por punto de venta
                        Updt.TotalAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Total, item.TotalAnio); //se agrega el Total de venta al TotalAnio por punto de venta
                    }
                    if (item.Iva > 0)
                    {
                        item.IvaDia = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, item.Orden, position, "IvaDia"); //Obtener Saldo actual por punto de venta - IvaDia
                        item.IvaMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, item.Orden, position, "IvaMes"); //Obtener Saldo actual por punto de venta - IvaMes
                        item.IvaAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, item.Orden, position, "IvaAnio"); //Obtener Saldo actual por punto de venta - IvaAnio

                        Updt.IvaDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Iva, item.IvaDia); //se agrega el Iva de venta al IvaDia por punto de venta
                        Updt.IvaMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Iva, item.IvaMes); //se agrega el Iva de venta al IvaMes por punto de venta
                        Updt.IvaAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Iva, item.IvaAnio); //se agrega el Iva de venta al IvaAnio por punto de venta
                    }
                    if (item.Subtotal > 0)
                    {
                        item.SubtotalDia = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, item.Orden, position, "SubtotalDia"); //Obtener Saldo actual por punto de venta - SubtotalDia
                        item.SubtotalMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, item.Orden, position, "SubtotalMes"); //Obtener Saldo actual por punto de venta - SubtotalMes
                        item.SubtotalAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, item.Orden, position, "SubtotalAnio"); //Obtener Saldo actual por punto de venta - SubtotalAnio

                        Updt.SubtotalDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Subtotal, item.SubtotalDia); //se agrega el Subtotal de venta al SubtotalDia por punto de venta
                        Updt.SubtotalMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Subtotal, item.SubtotalMes); //se agrega el Subtotal de venta al SubtotalMes por punto de venta
                        Updt.SubtotalAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Subtotal, item.SubtotalAnio); //se agrega el Subtotal de venta al SubtotalAnio por punto de venta
                    }

                    if (item.Descuento > 0)
                    {
                        item.DescuentoDia = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, item.Orden, position, "DescuentoDia"); //Obtener Saldo actual por punto de venta - DescuentoDia
                        item.DescuentoMes = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, item.Orden, position, "DescuentoMes"); //Obtener Saldo actual por punto de venta - DescuentoMes
                        item.DescuentoAnio = CalcularPreciosVentaServicio.ObtenerSaldoActual(item.IdPuntoVenta, item.Orden, position, "DescuentoAnio"); //Obtener Saldo actual por punto de venta - DescuentoAnio

                        Updt.DescuentoDia = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Descuento, item.DescuentoDia); //se agrega el Descuento de venta al DescuentoDia por punto de venta
                        Updt.DescuentoMes = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Descuento, item.DescuentoMes); //se agrega el Descuento de venta al DescuentoMes por punto de venta
                        Updt.DescuentoAnio = CalcularPreciosVentaServicio.ObtenerSumaTotalVenta(item.Descuento, item.DescuentoAnio); //se agrega el Descuento de venta al DescuentoAnio por punto de venta

                    }
                    Updt.DatosProcesados = true;
                    var rep = CajaGeneralAdapter.FromEntity(Updt);
                    new CajaGeneralDataAccess().Actualizar(rep);
                }
            }
        }

        public static void ActualizarSaldos(List<VentaMovimiento> vm, string from)
        {
            VentaMovimiento Updt = new VentaMovimiento();
            int position = 0;
            foreach (var _lst in vm)
            {
                position++;
                Updt = ObtenerVentaMovimiento(_lst.IdPuntoVenta, _lst.Orden);
                _lst.Saldo = CalcularPreciosVentaServicio.ObtenerSaldoActual(_lst.IdPuntoVenta, _lst.Orden, position);
                if (from == "PuntosVenta")
                {
                    if (_lst.Ingreso > 0 )//Actualiza saldo proveniente de Puntos venta
                    {
                        Updt.Saldo = CalcularPreciosVentaServicio.ObtenerSumaSaldoVenta(_lst.Ingreso, _lst.Saldo);
                    }
                    else if (_lst.Egreso > 0 )//Actualiza saldo proveniente de Puntos venta
                    {
                        Updt.Saldo = CalcularPreciosVentaServicio.ObtenerSaldoVentaEgreso(_lst.Egreso, _lst.Saldo);
                    }
                }
                else
                {
                    if (_lst.Egreso > 0)
                    {
                        Updt.Saldo = CalcularPreciosVentaServicio.ObtenerSaldoVentaEgreso(_lst.Egreso, _lst.Saldo);
                    }
                }
                var rep = CajaGeneralAdapter.FromEntity(Updt);
                new CajaGeneralDataAccess().Actualizar(rep);


            }

        }

        public static void CargarAVentasMovimientos(List<VentaPuntoDeVenta> lst)
        {
            List<RegistrarVentasMovimientosDTO> _lst = MergedLst(lst, null);
            List<VentaMovimiento> VtasMov = AdaptadoresDTO.Ventas.CajaGeneralAdapter.FromDtoVtaM(_lst);
            new CajaGeneralDataAccess().Insertar(VtasMov);

            var listaMov = VtasMov.GroupBy(x => x.IdPuntoVenta).ToList(); //VentaspuntosDeVentaAgrupados - por punto de venta

            foreach (var _lMov in listaMov)
            {
                ActualizarSaldos(_lMov.ToList(),"PuntosVenta");
            }
        }
        public static void CargarAnticiposAMovimientos(List<VentaCorteAnticipoEC> lst)
        {
            //insertar movimientos de anticipos en tabla movimientos
            bool Procesados = true;
            foreach (var li in lst)
            {
                RegistrarVentasMovimientosDTO _lstmov = MergeLstAnticipos(li);

                VentaMovimiento Vtasanticipos = AdaptadoresDTO.Ventas.CajaGeneralAdapter.FromDTO(_lstmov);

                new CajaGeneralDataAccess().Insertar(Vtasanticipos);
                li.DatosProcesados = Procesados;
                var rep = CajaGeneralAdapter.FromEntity(li);
                new CajaGeneralDataAccess().Actualizar(rep);

            }
            //actualizar totales de VentasMovimientos
            var puntosventa = lst.GroupBy(x => x.IdPuntoVenta);

            foreach (var x in puntosventa)
            {/*Seleccionar ultimo registro del movimiento, perteneciente a los insertados de los anticipos*/
                ActualizarSaldos(ObtenerListaMovimientos(x.Select(z => z.IdPuntoVenta).FirstOrDefault()),"");
            }

        }
        public static List<VentaPuntoDeVenta> ObtenerVentasPuntosVentaNoProc()
        {
            return new CajaGeneralDataAccess().Buscar();
        }
        public static List<VentaCorteAnticipoEC> ObtenerVentasCorteAnticipoNoProc()
        {
            bool noProcesados = false;
            return new CajaGeneralDataAccess().BuscarAnticiposC().Where(x => x.DatosProcesados.Equals(noProcesados)).OrderByDescending(x => x.FechaAplicacion).ToList();
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
                FechaAplicacion = v.FechaAplicacion ?? DateTime.Now,
                Descripcion = "",
                IdCAlmacenGas = new PuntoVentaDataAccess().Buscar(v.IdPuntoVenta).IdCAlmacenGas,
            }).ToList();

            return lstFinal;
        }

        public static RegistrarVentasMovimientosDTO MergeLstAnticipos(VentaCorteAnticipoEC v)
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
            lstFinal.FechaRegistro = v.FechaRegistro;
            lstFinal.FechaAplicacion = v.FechaAplicacion;
            lstFinal.Descripcion = v.TipoOperacion;
            lstFinal.IdCAlmacenGas = new PuntoVentaDataAccess().Buscar(v.IdPuntoVenta).IdCAlmacenGas;

            return lstFinal;
        }
        public static List<VentaPuntoDeVenta>  ObtenerVentas()
        {
            return new CajaGeneralDataAccess().BuscarVentas(); 
        }
    }
}
