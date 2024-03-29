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
    public class EquipoTransporteDataAccess
    {
        private SagasDataUow uow;

        public EquipoTransporteDataAccess()
        {
            uow = new SagasDataUow();
        }

        //public RespuestaDto Insertar(UnidadAlmacenGas _uag)
        //{
        //    RespuestaDto _respuesta = new RespuestaDto();
        //    using (uow)
        //    {
        //        try
        //        {
        //            uow.Repository<UnidadAlmacenGas>().Insert(_uag);
        //            uow.SaveChanges();
        //            _respuesta.Id = _uag.IdCAlmacenGas;
        //            _respuesta.EsInsercion = true;
        //            _respuesta.Exito = true;
        //            _respuesta.ModeloValido = true;
        //            _respuesta.Mensaje = Exito.OK;
        //        }
        //        catch (Exception ex)
        //        {
        //            _respuesta.Exito = false;
        //            _respuesta.Mensaje = string.Format(Error.C0002, "del UnidadAlmacenGas");
        //            _respuesta.MensajesError = CatchInnerException.Obtener(ex);
        //        }
        //    }
        //    return _respuesta;
        //}

        //public RespuestaDto Actualizar(UnidadAlmacenGas _uag)
        //{
        //    RespuestaDto _respuesta = new RespuestaDto();
        //    using (uow)
        //    {
        //        try
        //        {
        //            uow.Repository<Sagas.MainModule.Entidades.UnidadAlmacenGas>().Update(_uag);
        //            uow.SaveChanges();
        //            _respuesta.Id = _uag.IdCAlmacenGas;
        //            _respuesta.Exito = true;
        //            _respuesta.EsActulizacion = true;
        //            _respuesta.ModeloValido = true;
        //            _respuesta.Mensaje = Exito.OK;
        //        }
        //        catch (Exception ex)
        //        {
        //            _respuesta.Exito = false;
        //            _respuesta.Mensaje = string.Format(Error.C0003, "del UnidadAlmacenGas"); ;
        //            _respuesta.MensajesError = CatchInnerException.Obtener(ex);
        //        }
        //    }
        //    return _respuesta;
        //}

        public List<UnidadAlmacenGas> BuscarTodos()
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.Activo).ToList();
        }
        public List<UnidadAlmacenGas> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.Activo)
                                                         .ToList();
        }
        public UnidadAlmacenGas BuscarUnidades(short idEmpresa, short idCAlmacenGas)
        {
            return uow.Repository<UnidadAlmacenGas>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                               && x.IdCAlmacenGas.Equals(idCAlmacenGas) && x.Activo);
        }
        public Camioneta BuscarCamioneta(int idCamioneta)
        {
            return uow.Repository<Camioneta>().GetSingle(x => x.IdCamioneta.Equals(idCamioneta)
                                                            && x.Activo);
        }
        public Pipa BuscarPipa(int idPipa)
        {
            return uow.Repository<Pipa>().GetSingle(x => x.IdPipa.Equals(idPipa)
                                                     && x.Activo);
        }
        public EstacionCarburacion BuscarEstacion(int idEstacion)
        {
            return uow.Repository<EstacionCarburacion>().GetSingle(x => x.IdEstacionCarburacion.Equals(idEstacion)
                                                 && x.Activo);
        }
        public CUtilitario BuscarUtilitario(int id)
        {
            return uow.Repository<CUtilitario>().GetSingle(x => x.IdUtilitario.Equals(id));
        }
        public Camioneta BuscarCamioneta(UnidadAlmacenGas uAG)
        {
            if (uAG.Camioneta != null)
                return uAG.Camioneta;
            else
            {
                if (uAG.IdCamioneta != null && uAG.IdCamioneta > 0)
                    return BuscarCamioneta(uAG.IdCamioneta.Value);
                else
                    return null;
            }
        }
        public Pipa BuscarPipa(UnidadAlmacenGas uAG)
        {
            if (uAG.Pipa != null)
                return uAG.Pipa;
            else
            {
                if (uAG.IdPipa != null && uAG.IdPipa > 0)
                    return BuscarPipa(uAG.IdPipa.Value);
                else
                    return null;
            }
        }
        public EstacionCarburacion BuscarEstacion(UnidadAlmacenGas uAG)
        {
            if (uAG.EstacionCarburacion != null)
                return uAG.EstacionCarburacion;
            else
            {
                if (uAG.IdEstacionCarburacion != null && uAG.IdEstacionCarburacion > 0)
                    return BuscarEstacion(uAG.IdEstacionCarburacion.Value);
                else
                    return null;
            }
        }
        public List<CDetalleEquipoTransporte> BuscarEquipoTransporte()
        {
            return uow.Repository<CDetalleEquipoTransporte>().Get().ToList();
        }
        public List<CDetalleEquipoTransporte> BuscarEquipoTransporte(short idEmpresa)
        {
            return uow.Repository<CDetalleEquipoTransporte>().Get(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
        }
        public RespuestaDto Insertar(CDetalleEquipoTransporte _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CDetalleEquipoTransporte>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.IdEquipoTransporteDetalle;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del vehiculo");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(CDetalleEquipoTransporte _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CDetalleEquipoTransporte>().Update(_pro);                 
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdEquipoTransporteDetalle;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del equipo de transporte");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        
        public RespuestaDto Actualizar(CDetalleEquipoTransporte _pro, CDetalleEquipoTransporte Acturalizar)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    if (Acturalizar.CUtilitario != null)
                        uow.Repository<CUtilitario>().Update(Acturalizar.CUtilitario);
                    if (Acturalizar.CCamioneta != null)
                        uow.Repository<Camioneta>().Update(Acturalizar.CCamioneta);
                    if (Acturalizar.CPipa != null)
                        uow.Repository<Pipa>().Update(Acturalizar.CPipa);

                    uow.Repository<CDetalleEquipoTransporte>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdEquipoTransporteDetalle;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del equipo de transporte");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }       
        public List<CDetalleEquipoTransporte> BuscarTodos(int IdEquipoTransporte)
        {
            return uow.Repository<CDetalleEquipoTransporte>().Get(x => x.IdEquipoTransporteDetalle.Equals(IdEquipoTransporte)).ToList();
        }
        public List<CDetalleEquipoTransporte> Buscar(short idEmpresa)
        {
            return uow.Repository<CDetalleEquipoTransporte>().Get(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
        }
        public CDetalleEquipoTransporte Buscar(int IdEquipoTransporte)
        {
            return uow.Repository<CDetalleEquipoTransporte>().GetSingle(x => x.IdEquipoTransporteDetalle.Equals(IdEquipoTransporte));
        }
        public CDetalleEquipoTransporte BuscarDetalle(int IdEquipoTransporte)
        {
            return uow.Repository<CDetalleEquipoTransporte>().GetSingle(x => x.IdEquipoTransporteDetalle.Equals(IdEquipoTransporte));
        }
    }
}
