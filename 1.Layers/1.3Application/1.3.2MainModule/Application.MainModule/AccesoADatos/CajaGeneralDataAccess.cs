using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.UnitOfWork;
using Exceptions.MainModule;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class CajaGeneralDataAccess
    {
        private SagasDataUow uow;

        public CajaGeneralDataAccess()
        {
            uow = new SagasDataUow();
        }
        public List<VentaPuntoDeVenta> Buscar()
        {
            bool noProcesados = false;
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.DatosProcesados.Equals(noProcesados) & x.FolioOperacionDia != null).ToList();
        }
        public List<VentaPuntoDeVentaDetalle> BuscarDetalleVenta(short? empresa, short anio, byte mes, byte dia, short? orden)
        {
            if (empresa != null)
            {
                return uow.Repository<VentaPuntoDeVentaDetalle>().Get(x => x.IdEmpresa.Equals(empresa.Value)
                && x.Year.Equals(anio)
                && x.Mes.Equals(mes)
                && x.Dia.Equals(dia)).ToList();
            }
            else
            {
                return uow.Repository<VentaPuntoDeVentaDetalle>().Get(x =>
                x.Year.Equals(anio)
                && x.Mes.Equals(mes)
                && x.Dia.Equals(dia)
               ).ToList();
            }
        }
        public List<VentaMovimiento> BuscarTodos()
        {
            return uow.Repository<VentaMovimiento>().Get().ToList();
        }
        public List<VentaMovimiento> Buscar(short idEmpresa)
        {
            return uow.Repository<VentaMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         ).ToList();
        }
        public List<AlmacenGasMovimiento> Buscar(short empresa, short year, byte month, byte dia, short orden, string Folio)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(empresa)
            && x.Year.Equals(year) && x.Mes.Equals(month) && x.Dia.Equals(dia) && x.FolioOperacionDia.Equals(Folio)
                                                         ).ToList();
        }
        public List<VentaMovimiento> Buscar(int idPv)
        {
            return uow.Repository<VentaMovimiento>().Get(x => x.IdPuntoVenta.Equals(idPv)
                                                         ).ToList();
        }
        public List<VentaMovimiento> BuscarUltimoOrden(int idPv, DateTime fecha)
        {
            return uow.Repository<VentaMovimiento>().Get(x => x.IdPuntoVenta.Equals(idPv)
               && x.Year.Equals((short)fecha.Year) && x.Mes.Equals((byte)fecha.Month) && x.Dia.Equals((byte)fecha.Day)).ToList();
        }
        public VentaMovimiento BuscarUltimoMovSaldo(int idPv, DateTime fecha)
        {
            return BuscarUltimosSaldoPorPuntoVenta(idPv, fecha).LastOrDefault();
        }
        public List<VentaMovimiento> BuscarUltimosSaldoPorPuntoVenta(int idPv, DateTime fecha)
        {
            return uow.Repository<VentaMovimiento>().Get(x => x.IdPuntoVenta.Equals(idPv)).ToList();
            //&& x.Year.Equals((short)fecha.Year) && x.Mes.Equals((byte)fecha.Month) && x.Dia.Equals((byte)fecha.Day)).ToList();
        }
        public VentaPuntoDeVenta BuscarUltimoMovimiento(short empresa, int idPv, string Tipo, DateTime fecha)
        {
            return BuscarUltimosMovimientoEfectivoPorPuntoVenta(empresa, idPv, Tipo, fecha).LastOrDefault();
        }
        public List<VentaPuntoDeVenta> BuscarUltimosMovimientoEfectivoPorPuntoVenta(short empresa, int idPv, string Tipo, DateTime fecha)
        {
            if (Tipo == "TotalMes" || Tipo == "IvaMes" || Tipo == "SubtotalMes" || Tipo == "DescuentoMes")
            {
                return uow.Repository<VentaPuntoDeVenta>().Get(x => x.IdPuntoVenta.Equals(idPv)
                && x.Year.Equals((short)fecha.Year) && x.Mes.Equals((byte)fecha.Month) && x.DatosProcesados.Equals(true)).ToList();
            }
            if (Tipo == "TotalAnio" || Tipo == "IvaAnio" || Tipo == "SubtotalAnio" || Tipo == "DescuentoAnio")
            {
                return uow.Repository<VentaPuntoDeVenta>().Get(x => x.IdPuntoVenta.Equals(idPv)
                && x.Year.Equals((short)fecha.Year) && x.DatosProcesados.Equals(true)).ToList();
            }
            if (Tipo == "TotalAcumDia")
            {
                return uow.Repository<VentaPuntoDeVenta>().Get(x => x.IdEmpresa.Equals(empresa)
                && x.Year.Equals((short)fecha.Year) && x.Mes.Equals((byte)fecha.Month) && x.Dia.Equals((byte)fecha.Day) && x.DatosProcesados.Equals(true)).ToList();
            }
            if (Tipo == "TotalAcumMes")
            {
                return uow.Repository<VentaPuntoDeVenta>().Get(x => x.IdEmpresa.Equals(empresa)
                && x.Year.Equals((short)fecha.Year) && x.Mes.Equals((byte)fecha.Month) && x.DatosProcesados.Equals(true)).ToList();
            }
            if (Tipo == "TotalAcumAnio")
            {
                return uow.Repository<VentaPuntoDeVenta>().Get(x => x.IdEmpresa.Equals(empresa)
                 && x.Year.Equals((short)fecha.Year) && x.DatosProcesados.Equals(true)).ToList();
            }

            else
                return uow.Repository<VentaPuntoDeVenta>().Get(x => x.IdPuntoVenta.Equals(idPv)
                && x.Year.Equals((short)fecha.Year) && x.Mes.Equals((byte)fecha.Month) && x.Dia.Equals((byte)fecha.Day)).ToList();
        }
        public VentaCajaGeneral BuscarGralPorCve(string cve)
        {
            return uow.Repository<VentaCajaGeneral>().GetSingle(x => x.FolioOperacionDia.Equals(cve));
        }
        public List<VentaPuntoDeVenta> BuscarPorCve(string cve)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.FolioOperacionDia.Equals(cve)).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarTotalVentasCamionetas(DateTime fecha)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.CPuntoVenta.UnidadesAlmacen.IdCamioneta != null
                                                             && x.FechaRegistro.Month.Equals(fecha.Month)
                                                             && x.FechaRegistro.Year.Equals(fecha.Year)
                                                             && x.FechaRegistro <= fecha).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarTotalVentasPipas(DateTime fecha)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.CPuntoVenta.UnidadesAlmacen.IdPipa != null
                                                             && x.FechaRegistro.Month.Equals(fecha.Month)
                                                             && x.FechaRegistro.Year.Equals(fecha.Year)
                                                             && x.FechaRegistro <= fecha).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarTotalBonificaciones(DateTime fecha)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.Descuento > 0
                                                             && x.CPuntoVenta.UnidadesAlmacen.IdCamioneta != null
                                                             && x.CPuntoVenta.UnidadesAlmacen.IdEstacionCarburacion != null
                                                             && x.FechaRegistro.Month.Equals(fecha.Month)
                                                             && x.FechaRegistro.Year.Equals(fecha.Year)
                                                             && x.FechaRegistro <= fecha).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarTotalVentasEstaciones(DateTime fecha)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.CPuntoVenta.UnidadesAlmacen.IdEstacionCarburacion != null
                                                             && x.FechaRegistro.Month.Equals(fecha.Month)
                                                              && x.FechaRegistro.Year.Equals(fecha.Year)
                                                             && x.FechaRegistro <= fecha).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarTotalVentasEstaciones(EstacionCarburacion entidad, DateTime fecha)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.CPuntoVenta.UnidadesAlmacen.IdEstacionCarburacion.Equals(entidad.IdEstacionCarburacion)
                                                                && x.FechaRegistro.Month.Equals(fecha.Month)
                                                                && x.FechaRegistro.Year.Equals(fecha.Year)
                                                                && x.FechaRegistro <= fecha).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarTotalVentasACredito(DateTime fecha)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.VentaACredito.Equals(true)
                                                             && x.FechaRegistro.Month.Equals(fecha.Month)
                                                              && x.FechaRegistro.Year.Equals(fecha.Year)
                                                             && x.FechaRegistro <= fecha).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarTodosPV()
        {
            return uow.Repository<VentaPuntoDeVenta>().Get().ToList();
        }
        public List<VentaPuntoDeVenta> BuscarPorPV(int puntoDeVenta)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.IdPuntoVenta.Equals(puntoDeVenta)).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarPorPV(int puntoDeVenta, byte dia, byte mes, short anio)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x =>
            x.Dia.Equals(dia) 
            && x.Mes.Equals(mes) 
            && x.Year.Equals(anio) 
            && x.IdPuntoVenta.Equals(puntoDeVenta)).ToList();
        }
        public List<VentaPuntoDeVenta> BuscarPorPuntoVenta(int idPv, DateTime fecha)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.IdPuntoVenta.Equals(idPv)
            && x.Year.Equals((short)fecha.Year) && x.Mes.Equals((byte)fecha.Month) && x.Dia.Equals((byte)fecha.Day)).ToList();
        }
        public List<VentaCorteAnticipoEC> BuscarPorCveEC(string cve)
        {
            return uow.Repository<VentaCorteAnticipoEC>().Get(x => x.FolioOperacionDia.Equals(cve)).ToList();
        }
        public List<VentaCorteAnticipoEC> BuscarPorIdPv(int idPv)
        {
            return uow.Repository<VentaCorteAnticipoEC>().Get(x => x.IdPuntoVenta.Equals(idPv)).ToList();
        }
        public List<VentaCorteAnticipoEC> BuscarAnticiposC()
        {
            return uow.Repository<VentaCorteAnticipoEC>().Get().ToList();
        }
        public RespuestaDto Actualizar(VentaCajaGeneral pv)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {

                    uow.Repository<Sagas.MainModule.Entidades.VentaCajaGeneral>().Update(pv);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la liquidación"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(VentaPuntoDeVenta pv)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.VentaPuntoDeVenta>().Update(pv);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.Id = pv.IdPuntoVenta;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de Venta Punto De Venta");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(List<VentaPuntoDeVenta> pv)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var _pv in pv)
                    {
                        uow.Repository<Sagas.MainModule.Entidades.VentaPuntoDeVenta>().Update(_pv);
                    }
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la liquidación"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(List<VentaCorteAnticipoEC> pv)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var _pv in pv)
                    {
                        uow.Repository<Sagas.MainModule.Entidades.VentaCorteAnticipoEC>().Update(_pv);
                    }
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la liquidación"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(VentaCorteAnticipoEC pv)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.VentaCorteAnticipoEC>().Update(pv);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de Cortes Anticipos"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(VentaMovimiento pv)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.VentaMovimiento>().Update(pv);
                    uow.SaveChanges();
                    _respuesta.Id = pv.IdPuntoVenta;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del movimiento de venta"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Insertar(List<VentaMovimiento> pv)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var _pv in pv)
                    {
                        uow.Repository<VentaMovimiento>().Insert(_pv);
                    }
                    uow.SaveChanges();
                    // _respuesta.Id = pv.IdPuntoVenta;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del movimiento de venta");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Insertar(VentaMovimiento pv)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<VentaMovimiento>().Insert(pv);
                    uow.SaveChanges();
                    _respuesta.Id = pv.IdPuntoVenta;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del movimiento de venta");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
    }
}
