﻿using Application.MainModule.DTOs.Respuesta;
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
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.DatosProcesados.Equals(noProcesados)).ToList();
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

        public List<AlmacenGasMovimiento> Buscar(short empresa, short year, byte month, byte dia, short orden)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(empresa)
            && x.Year.Equals(year) && x.Mes.Equals(month) && x.Dia.Equals(dia) //&& x.Orden.Equals(orden)
                                                         ).ToList();
        }
        public List<VentaMovimiento> Buscar(int idPv)
        {
            return uow.Repository<VentaMovimiento>().Get(x => x.IdPuntoVenta.Equals(idPv)
                                                         ).ToList();
        }
      
        public VentaMovimiento BuscarUltimoMovSaldo(int idPv, DateTime fecha)
        {
            return BuscarUltimosSaldoPorPuntoVenta(idPv, fecha).LastOrDefault();
        }


        public List<VentaMovimiento> BuscarUltimosSaldoPorPuntoVenta(int idPv, DateTime fecha)
        {
            return uow.Repository<VentaMovimiento>().Get(x => x.IdPuntoVenta.Equals(idPv)
            && x.Year.Equals((short)fecha.Year) && x.Mes.Equals((byte)fecha.Month) && x.Dia.Equals((byte)fecha.Day)).ToList();
        }

        public VentaPuntoDeVenta BuscarUltimoMovimiento(int idPv, DateTime fecha)
        {
            return BuscarUltimosMovimientoEfectivoPorPuntoVenta(idPv, fecha).LastOrDefault();
        }


        public List<VentaPuntoDeVenta> BuscarUltimosMovimientoEfectivoPorPuntoVenta(int idPv, DateTime fecha)
        {
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
        public List<VentaPuntoDeVenta> BuscarTodosPV()
        {
            return uow.Repository<VentaPuntoDeVenta>().Get().ToList();
        }
        public List<VentaPuntoDeVenta> BuscarPorPV(int puntoDeVenta)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.IdPuntoVenta.Equals(puntoDeVenta)).ToList();
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
