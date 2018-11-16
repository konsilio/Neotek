using Application.MainModule.DTOs.Almacen;
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
using Application.MainModule.DTOs.Mobile;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class AlmacenGasDataAccess
    {
        private SagasDataUow uow;

        public AlmacenGasDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(AlmacenGas _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<AlmacenGas>().Insert(_alm);
                    uow.SaveChanges();
                    _respuesta.Id = _alm.IdAlmacenGas;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del Almacen");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Insertar(AlmacenGasMovimiento _almMov)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<AlmacenGasMovimiento>().Insert(_almMov);
                    uow.SaveChanges();
                    _respuesta.Id = _almMov.IdAlmacenGas;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del movimiento de almacén");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Insertar(UnidadAlmacenGas _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<UnidadAlmacenGas>().Insert(_alm);
                    uow.SaveChanges();
                    _respuesta.Id = _alm.IdCAlmacenGas;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "de la unidad de almacén");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Eliminar(AlmacenGas _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<AlmacenGas>().Delete(_alm.IdAlmacenGas);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0009, "del Almacén total y general");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public AlmacenGasTomaLectura BuscarLectura(short idCAlmacenGas, int idOrden)
        {
            return uow.Repository<AlmacenGasTomaLectura>().GetSingle(x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                                                                        && x.IdOrden.Equals(idOrden));
        }
        public AlmacenGasTomaLectura BuscarUltimaLectura(short idCAlmacenGas, byte idTipoEvento)
        {
            var lecturas = uow.Repository<AlmacenGasTomaLectura>().Get(x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                                                                         && x.IdTipoEvento.Equals(idTipoEvento));

            if (lecturas != null && lecturas.ToList().Count > 0)
                return lecturas.Last();

            return null;
        }
        public List<AlmacenGasTomaLectura> BuscarLecturas(short idCAlmacenGas)
        {
            return uow.Repository<AlmacenGasTomaLectura>().Get(x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                                                    ).ToList();
        }
        public List<AlmacenGasTomaLectura> BuscarLecturas(short idCAlmacenGas, bool noProcesados)
        {
            return uow.Repository<AlmacenGasTomaLectura>().Get(x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                                                                && x.DatosProcesados.Equals(noProcesados)
                                                    ).ToList();
        }

        public RespuestaDto Insertar(AlmacenGasAutoConsumo _autoconsumo)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<AlmacenGasAutoConsumo>().Insert(_autoconsumo);
                    uow.SaveChanges();
                    _respuesta.Id = _autoconsumo.IdCAlmacenGasEntrada;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, " del autoconsumo");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Insertar(AlmacenGasTomaLectura _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<AlmacenGasTomaLectura>().Insert(_alm);
                    uow.SaveChanges();
                    _respuesta.Id = _alm.IdCAlmacenGas;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "de la Lectura");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        internal RespuestaDto Insertar(AlmacenGasRecarga _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            try
            {
                uow.Repository<AlmacenGasRecarga>().Insert(_alm);
                uow.SaveChanges();
                _respuesta.Id = _alm.IdAlmacenGasRecarga;
                _respuesta.EsInsercion = true;
                _respuesta.Exito = true;
                _respuesta.ModeloValido = true;
                _respuesta.Mensaje = Exito.OK;
            }
            catch (Exception ex)
            {
                _respuesta.Exito = false;
                _respuesta.Mensaje = string.Format(Error.C0002, "de la recarga del Almacén");
                _respuesta.MensajesError = CatchInnerException.Obtener(ex);
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(AlmacenGas _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<AlmacenGas>().Update(_alm);
                    uow.SaveChanges();
                    _respuesta.Id = _alm.IdAlmacenGas;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del Almacén"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(UnidadAlmacenGas _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<UnidadAlmacenGas>().Update(_alm);
                    uow.SaveChanges();
                    _respuesta.Id = _alm.IdCAlmacenGas;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la unidad de almacén"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public void Actualizar(AplicaRecargaDto aplicaRecarga)
        {
            using (uow)
            {
                try
                {
                    if (aplicaRecarga.unidadEntrada != null)
                        uow.Repository<UnidadAlmacenGas>().Update(aplicaRecarga.unidadEntrada);

                    if (aplicaRecarga.unidadSalida != null)
                        uow.Repository<UnidadAlmacenGas>().Update(aplicaRecarga.unidadSalida);

                    if (aplicaRecarga.RecargaLecturaInicialSinNavProp != null)
                        uow.Repository<AlmacenGasRecarga>().Update(aplicaRecarga.RecargaLecturaInicialSinNavProp);

                    if (aplicaRecarga.RecargaLecturaInicialFotos != null && aplicaRecarga.RecargaLecturaInicialFotos.Count > 0)
                        aplicaRecarga.RecargaLecturaInicialFotos.ToList().ForEach(x =>
                            uow.Repository<AlmacenGasRecargaFoto>().Update(x)
                        );

                    if (aplicaRecarga.RecargaLecturaFinalSinNavProp != null)
                        uow.Repository<AlmacenGasRecarga>().Update(aplicaRecarga.RecargaLecturaFinalSinNavProp);

                    if (aplicaRecarga.RecargaLecturaFinalFotos != null && aplicaRecarga.RecargaLecturaFinalFotos.Count > 0)
                        aplicaRecarga.RecargaLecturaFinalFotos.ToList().ForEach(x =>
                            uow.Repository<AlmacenGasRecargaFoto>().Update(x)
                        );

                    if (aplicaRecarga.MovimientoEntrada != null)
                        uow.Repository<AlmacenGasMovimiento>().Insert(aplicaRecarga.MovimientoEntrada);

                    if (aplicaRecarga.MovimientoSalida != null)
                        uow.Repository<AlmacenGasMovimiento>().Insert(aplicaRecarga.MovimientoSalida);

                    if (uow.repositories.Count > 0)
                        uow.SaveChanges();
                    //_respuesta.Id = aplicaRecarga.IdCAlmacenGas;
                    //_respuesta.Exito = true;
                    //_respuesta.EsActulizacion = true;
                    //_respuesta.ModeloValido = true;
                    //_respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    //_respuesta.Exito = false;
                    //_respuesta.Mensaje = string.Format(Error.C0003, "de la unidad de almacén"); ;
                    //_respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
        }

        public void Actualizar(AplicaTraspasoDto aplicaTraspaso)
        {
            using (uow)
            {
                try
                {
                    if (aplicaTraspaso.unidadEntrada != null)
                        uow.Repository<UnidadAlmacenGas>().Update(aplicaTraspaso.unidadEntrada);

                    if (aplicaTraspaso.unidadSalida != null)
                        uow.Repository<UnidadAlmacenGas>().Update(aplicaTraspaso.unidadSalida);

                    if (aplicaTraspaso.TraspasoLecturaInicialSinNavProp != null)
                        uow.Repository<AlmacenGasTraspaso>().Update(aplicaTraspaso.TraspasoLecturaInicialSinNavProp);

                    if (aplicaTraspaso.TraspasoLecturaInicialFotos != null && aplicaTraspaso.TraspasoLecturaInicialFotos.Count > 0)
                        aplicaTraspaso.TraspasoLecturaInicialFotos.ToList().ForEach(x =>
                            uow.Repository<AlmacenGasTraspasoFoto>().Update(x)
                        );

                    if (aplicaTraspaso.TraspasoLecturaFinalSinNavProp != null)
                        uow.Repository<AlmacenGasTraspaso>().Update(aplicaTraspaso.TraspasoLecturaFinalSinNavProp);

                    if (aplicaTraspaso.TraspasoLecturaFinalFotos != null && aplicaTraspaso.TraspasoLecturaFinalFotos.Count > 0)
                        aplicaTraspaso.TraspasoLecturaFinalFotos.ToList().ForEach(x =>
                            uow.Repository<AlmacenGasTraspasoFoto>().Update(x)
                        );

                    if (aplicaTraspaso.MovimientoEntrada != null)
                        uow.Repository<AlmacenGasMovimiento>().Insert(aplicaTraspaso.MovimientoEntrada);

                    if (aplicaTraspaso.MovimientoSalida != null)
                        uow.Repository<AlmacenGasMovimiento>().Insert(aplicaTraspaso.MovimientoSalida);

                    if (uow.repositories.Count > 0)
                        uow.SaveChanges();
                    //_respuesta.Id = aplicaTraspaso.IdCAlmacenGas;
                    //_respuesta.Exito = true;
                    //_respuesta.EsActulizacion = true;
                    //_respuesta.ModeloValido = true;
                    //_respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    //_respuesta.Exito = false;
                    //_respuesta.Mensaje = string.Format(Error.C0003, "de la unidad de almacén"); ;
                    //_respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
        }

        public void Actualizar(AplicaAutoConsumoDto aplicaAutoConsumo)
        {
            using (uow)
            {
                try
                {
                    if (aplicaAutoConsumo.unidadEntrada != null)
                        uow.Repository<UnidadAlmacenGas>().Update(aplicaAutoConsumo.unidadEntrada);

                    if (aplicaAutoConsumo.unidadSalida != null)
                        uow.Repository<UnidadAlmacenGas>().Update(aplicaAutoConsumo.unidadSalida);

                    if (aplicaAutoConsumo.AutoConsumoLecturaInicialSinNavProp != null)
                        uow.Repository<AlmacenGasAutoConsumo>().Update(aplicaAutoConsumo.AutoConsumoLecturaInicialSinNavProp);

                    if (aplicaAutoConsumo.AutoConsumoLecturaInicialFotos != null && aplicaAutoConsumo.AutoConsumoLecturaInicialFotos.Count > 0)
                        aplicaAutoConsumo.AutoConsumoLecturaInicialFotos.ToList().ForEach(x =>
                            uow.Repository<AlmacenGasAutoConsumoFoto>().Update(x)
                        );

                    if (aplicaAutoConsumo.AutoConsumoLecturaFinalSinNavProp != null)
                        uow.Repository<AlmacenGasAutoConsumo>().Update(aplicaAutoConsumo.AutoConsumoLecturaFinalSinNavProp);

                    if (aplicaAutoConsumo.AutoConsumoLecturaFinalFotos != null && aplicaAutoConsumo.AutoConsumoLecturaFinalFotos.Count > 0)
                        aplicaAutoConsumo.AutoConsumoLecturaFinalFotos.ToList().ForEach(x =>
                            uow.Repository<AlmacenGasAutoConsumoFoto>().Update(x)
                        );

                    // Agregar al modelo de dominio AlmacenGasMovimiento
                    //if (aplicaAutoConsumo.AGMovimiento != null)
                    //    uow.Repository<AlmacenGasMovimiento>().Insert(aplicaAutoConsumo.MovInventario);
                    if (uow.repositories.Count > 0)
                        uow.SaveChanges();
                    //_respuesta.Id = aplicaAutoConsumo.IdCAlmacenGas;
                    //_respuesta.Exito = true;
                    //_respuesta.EsActulizacion = true;
                    //_respuesta.ModeloValido = true;
                    //_respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    //_respuesta.Exito = false;
                    //_respuesta.Mensaje = string.Format(Error.C0003, "de la unidad de almacén"); ;
                    //_respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
        }

        public void Actualizar(AplicaCalibracionDto aplicaCalibracion)
        {
            using (uow)
            {
                try
                {
                    if (aplicaCalibracion.unidadAlmacenGas != null)
                        uow.Repository<UnidadAlmacenGas>().Update(aplicaCalibracion.unidadAlmacenGas);

                    if (aplicaCalibracion.CalibracionLecturaInicialSinNavProp != null)
                        uow.Repository<AlmacenGasCalibracion>().Update(aplicaCalibracion.CalibracionLecturaInicialSinNavProp);

                    if (aplicaCalibracion.CalibracionLecturaInicialFotos != null && aplicaCalibracion.CalibracionLecturaInicialFotos.Count > 0)
                        aplicaCalibracion.CalibracionLecturaInicialFotos.ToList().ForEach(x =>
                            uow.Repository<AlmacenGasCalibracionFoto>().Update(x)
                        );

                    if (aplicaCalibracion.CalibracionLecturaFinalSinNavProp != null)
                        uow.Repository<AlmacenGasCalibracion>().Update(aplicaCalibracion.CalibracionLecturaFinalSinNavProp);

                    if (aplicaCalibracion.CalibracionLecturaFinalFotos != null && aplicaCalibracion.CalibracionLecturaFinalFotos.Count > 0)
                        aplicaCalibracion.CalibracionLecturaFinalFotos.ToList().ForEach(x =>
                            uow.Repository<AlmacenGasCalibracionFoto>().Update(x)
                        );

                    // Agregar al modelo de dominio AlmacenGasMovimiento
                    //if (aplicaCalibracion.AGMovimiento != null)
                    //    uow.Repository<AlmacenGasMovimiento>().Insert(aplicaCalibracion.MovInventario);
                    if (uow.repositories.Count > 0)
                        uow.SaveChanges();
                    //_respuesta.Id = aplicaCalibracion.IdCAlmacenGas;
                    //_respuesta.Exito = true;
                    //_respuesta.EsActulizacion = true;
                    //_respuesta.ModeloValido = true;
                    //_respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    //_respuesta.Exito = false;
                    //_respuesta.Mensaje = string.Format(Error.C0003, "de la unidad de almacén"); ;
                    //_respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
        }

        public void Actualizar(AplicaTomaLecturaDto aplicaTomaLectura)
        {
            using (uow)
            {
                try
                {
                    if (aplicaTomaLectura.AlmacenGas != null)
                        uow.Repository<AlmacenGas>().Update(aplicaTomaLectura.AlmacenGas);

                    if (aplicaTomaLectura.unidadAlmacenGas != null)
                        uow.Repository<UnidadAlmacenGas>().Update(aplicaTomaLectura.unidadAlmacenGas);

                    if (aplicaTomaLectura.TomaLecturaLectura != null)
                        uow.Repository<AlmacenGasTomaLectura>().Update(aplicaTomaLectura.TomaLecturaLectura);

                    if (aplicaTomaLectura.TomaLecturaLecturaFotos != null && aplicaTomaLectura.TomaLecturaLecturaFotos.Count > 0)
                        aplicaTomaLectura.TomaLecturaLecturaFotos.ToList().ForEach(x =>
                            uow.Repository<AlmacenGasTomaLecturaFoto>().Update(x)
                        );

                    //if (aplicaTomaLectura.TomaLecturaLectura != null)
                    //    uow.Repository<AlmacenGasTomaLectura>().Update(aplicaTomaLectura.TomaLecturaLecturaFinalSinNavProp);

                    //if (aplicaTomaLectura.TomaLecturaLecturaFinalFotos != null && aplicaTomaLectura.TomaLecturaLecturaFinalFotos.Count > 0)
                    //    aplicaTomaLectura.TomaLecturaLecturaFinalFotos.ToList().ForEach(x =>
                    //        uow.Repository<AlmacenGasTomaLecturaFoto>().Update(x)
                    //    );

                    if (aplicaTomaLectura.MovimientoUnidad != null)
                        uow.Repository<AlmacenGasMovimiento>().Insert(aplicaTomaLectura.MovimientoUnidad);

                    if (uow.repositories.Count > 0)
                        uow.SaveChanges();
                    //_respuesta.Id = aplicaTomaLectura.IdCAlmacenGas;
                    //_respuesta.Exito = true;
                    //_respuesta.EsActulizacion = true;
                    //_respuesta.ModeloValido = true;
                    //_respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    //_respuesta.Exito = false;
                    //_respuesta.Mensaje = string.Format(Error.C0003, "de la unidad de almacén"); ;
                    //_respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
        }

        public List<UnidadAlmacenGas> BuscarTodosEstacionCarburacion(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                            && x.IdEstacionCarburacion != null
                                                            && x.Activo).ToList();
        }
        public List<AlmacenGasMovimiento> BuscarMovimientos(string folio, short year, byte mes, byte dia)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.FolioOperacionDia.Equals(folio)
                                                                //&& x.IdAlmacenGas.Equals(idAlmacenGas)
                                                                && x.Year.Equals(year)
                                                                && x.Mes.Equals(mes)
                                                                && x.Dia.Equals(dia)).ToList();
        }

        public List<AlmacenGasMovimiento> BuscarMovimientosEnInventario(short idEmpresa, short idAlmacenGas, short year, byte mes, byte dia)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdAlmacenGas.Equals(idAlmacenGas)
                                                                && x.Year.Equals(year)
                                                                && x.Mes.Equals(mes)
                                                                && x.Dia.Equals(dia)).ToList();
        }

        public List<AlmacenGasMovimiento> BuscarMovimientosEnInventario(short idAlmacenGas, short idCAlmacenGas, short idEmpresa, short year, byte mes, byte dia)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdAlmacenGas.Equals(idAlmacenGas)
                                                                && x.IdCAlmacenGasPrincipal.Equals(idCAlmacenGas)
                                                                && x.Year.Equals(year)
                                                                && x.Mes.Equals(mes)
                                                                && x.Dia.Equals(dia)).ToList();
        }

        public List<AlmacenGasMovimiento> BuscarMovimientosConTipoEvento(short idEmpresa, byte idTipoEvento, short year, byte mes, byte dia)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdTipoEvento != null
                                                                && x.IdTipoEvento.Value.Equals(idTipoEvento)
                                                                && x.Year.Equals(year)
                                                                && x.Mes.Equals(mes)
                                                                && x.Dia.Equals(dia)).ToList();
        }

        public List<AlmacenGasMovimiento> BuscarMovimientosConTipoEvento(short idEmpresa, byte idTipoEvento, short year, byte mes)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdTipoEvento != null
                                                                && x.IdTipoEvento.Value.Equals(idTipoEvento)
                                                                && x.Year.Equals(year)
                                                                && x.Mes.Equals(mes)).ToList();
        }

        public List<AlmacenGasMovimiento> BuscarMovimientosConTipoEvento(short idEmpresa, byte idTipoEvento, short year)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdTipoEvento != null
                                                                && x.IdTipoEvento.Value.Equals(idTipoEvento)
                                                                && x.Year.Equals(year)).ToList();
        }

        public List<AlmacenGasMovimiento> BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, byte idTipoMovimiento, short year, byte mes, byte dia)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdCAlmacenGasPrincipal.Equals(idCAlmacenGas)
                                                                && x.IdTipoEvento != null
                                                                && x.IdTipoEvento.Value.Equals(idTipoEvento)
                                                                && x.IdTipoMovimiento.Equals(idTipoMovimiento)
                                                                && x.Year.Equals(year)
                                                                && x.Mes.Equals(mes)
                                                                && x.Dia.Equals(dia)).ToList();
        }
        public List<AlmacenGasMovimiento> BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, byte idTipoMovimiento, short year, byte mes)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdCAlmacenGasPrincipal.Equals(idCAlmacenGas)
                                                                && x.IdTipoEvento != null
                                                                && x.IdTipoEvento.Value.Equals(idTipoEvento)
                                                                && x.IdTipoMovimiento.Equals(idTipoMovimiento)
                                                                && x.Year.Equals(year)
                                                                && x.Mes.Equals(mes)).ToList();
        }
        public List<AlmacenGasMovimiento> BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, byte idTipoMovimiento, short year)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdCAlmacenGasPrincipal.Equals(idCAlmacenGas)
                                                                && x.IdTipoEvento != null
                                                                && x.IdTipoEvento.Value.Equals(idTipoEvento)
                                                                && x.IdTipoMovimiento.Equals(idTipoMovimiento)
                                                                && x.Year.Equals(year)).ToList();
        }

        public List<AlmacenGasMovimiento> BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, short year, byte mes, byte dia)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdCAlmacenGasPrincipal.Equals(idCAlmacenGas)
                                                                && x.IdTipoEvento != null
                                                                && x.IdTipoEvento.Value.Equals(idTipoEvento)
                                                                && x.Year.Equals(year)
                                                                && x.Mes.Equals(mes)
                                                                && x.Dia.Equals(dia)).ToList();
        }
        public List<AlmacenGasMovimiento> BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, short year, byte mes)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdCAlmacenGasPrincipal.Equals(idCAlmacenGas)
                                                                && x.IdTipoEvento != null
                                                                && x.IdTipoEvento.Value.Equals(idTipoEvento)
                                                                && x.Year.Equals(year)
                                                                && x.Mes.Equals(mes)).ToList();
        }
        public List<AlmacenGasMovimiento> BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, short year)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdCAlmacenGasPrincipal.Equals(idCAlmacenGas)
                                                                && x.IdTipoEvento != null
                                                                && x.IdTipoEvento.Value.Equals(idTipoEvento)
                                                                && x.Year.Equals(year)).ToList();
        }

        public List<AlmacenGasMovimiento> BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, byte idTipoMovimiento)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdCAlmacenGasPrincipal.Equals(idCAlmacenGas)
                                                                && x.IdTipoEvento != null
                                                                && x.IdTipoEvento.Value.Equals(idTipoEvento)
                                                                && x.IdTipoMovimiento.Equals(idTipoMovimiento)).ToList();
        }

        public List<AlmacenGasMovimiento> BuscarUltimosMovimientosPorUnidadAlamcenGas(short idEmpresa, short idCAlmacenGas)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdCAlmacenGasPrincipal.Equals(idCAlmacenGas)).ToList();
        }

        public AlmacenGasMovimiento BuscarUltimoMovimientoPorUnidadAlamcenGas(short idEmpresa, short idCAlmacenGas)
        {
            return BuscarUltimosMovimientosPorUnidadAlamcenGas(idEmpresa, idCAlmacenGas).LastOrDefault();
        }


        public AlmacenGasMovimiento BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, byte idTipoMovimiento, short year, byte mes, byte dia)
        {
            return BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, idTipoEvento, idTipoMovimiento, year, mes, dia).LastOrDefault();
        }
        public AlmacenGasMovimiento BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, byte idTipoMovimiento, short year, byte mes)
        {
            return BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, idTipoEvento, idTipoMovimiento, year, mes).LastOrDefault();
        }
        public AlmacenGasMovimiento BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, byte idTipoMovimiento, short year)
        {
            return BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, idTipoEvento, idTipoMovimiento, year).LastOrDefault();
        }
        public AlmacenGasMovimiento BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, short year, byte mes, byte dia)
        {
            return BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, idTipoEvento, year, mes, dia).LastOrDefault();
        }
        public AlmacenGasMovimiento BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, short year, byte mes)
        {
            return BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, idTipoEvento, year, mes).LastOrDefault();
        }
        public AlmacenGasMovimiento BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, short year)
        {
            return BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, idTipoEvento, year).LastOrDefault();
        }

        public AlmacenGasMovimiento BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, byte idTipoMovimiento)
        {
            return BuscarUltimosMovimientosPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, idTipoEvento, idTipoMovimiento).LastOrDefault();
        }

        public AlmacenGasMovimiento BuscarUltimoMovimientoEnInventario(short idEmpresa, short idAlmacenGas)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                    && x.IdAlmacenGas.Equals(idAlmacenGas)).LastOrDefault();
        }

        public AlmacenGasMovimiento BuscarUltimoMovimientoEnInventario(short idEmpresa, short idAlmacenGas, byte idTipoEvento, byte idTipoMovimiento)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdAlmacenGas.Equals(idAlmacenGas)
                                                                && x.IdTipoEvento.Equals(idTipoEvento)
                                                                && x.IdTipoMovimiento.Equals(idTipoMovimiento)).LastOrDefault();
        }

        public AlmacenGasMovimiento BuscarUltimoMovimientoEnInventario(short idEmpresa, short idAlmacenGas, byte idTipoEvento, byte idTipoMovimiento, short year, byte mes, byte dia)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdAlmacenGas.Equals(idAlmacenGas)
                                                                && x.IdTipoEvento.Equals(idTipoEvento)
                                                                && x.IdTipoMovimiento.Equals(idTipoMovimiento)
                                                                && x.Year.Equals(year)
                                                                && x.Mes.Equals(mes)
                                                                && x.Dia.Equals(dia)).LastOrDefault();
        }

        public AlmacenGasMovimiento BuscarUltimoMovimientoEnInventario(short idEmpresa, short idAlmacenGas, short year, byte mes, byte dia)
        {
            return BuscarMovimientosEnInventario(idEmpresa, idAlmacenGas, year, mes, dia).LastOrDefault();
        }

        public AlmacenGasMovimiento BuscarUltimoMovimientoEnInventario(short idAlmacenGas, short idCAlmacenGas, short idEmpresa, short year, byte mes, byte dia)
        {
            return BuscarMovimientosEnInventario(idEmpresa, idAlmacenGas, idCAlmacenGas, year, mes, dia).LastOrDefault();
        }

        public AlmacenGasMovimiento BuscarUltimoMovimientoConTipoEvento(short idEmpresa, byte idTipoEvento, short year, byte mes, byte dia)
        {
            return BuscarMovimientosConTipoEvento(idEmpresa, idTipoEvento, year, mes, dia).LastOrDefault();
        }

        public AlmacenGasMovimiento BuscarUltimoMovimientoConTipoEvento(short idEmpresa, byte idTipoEvento, short year, byte mes)
        {
            return BuscarMovimientosConTipoEvento(idEmpresa, idTipoEvento, year, mes).LastOrDefault();
        }

        public AlmacenGasMovimiento BuscarUltimoMovimientoConTipoEvento(short idEmpresa, byte idTipoEvento, short year)
        {
            return BuscarMovimientosConTipoEvento(idEmpresa, idTipoEvento, year).LastOrDefault();
        }

        public UnidadAlmacenGas ObtenerUnidadAlmacenGasAlterno(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                  && x.Activo
                                                                  && x.EsAlterno).FirstOrDefault();
        }
        public List<UnidadAlmacenGas> ObtenerUnidadesAlmacenGasAlterno(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                  && x.Activo
                                                                  && x.EsAlterno).ToList();
        }
        public List<UnidadAlmacenGas> ObtenerUnidadesAlmacenGasAlternoNoActivos(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                  && !x.Activo
                                                                  && x.EsAlterno).ToList();
        }

        public List<UnidadAlmacenGas> BuscarTodosPipas(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                       && x.Activo && x.IdPipa != null).ToList();
        }

        public UnidadAlmacenGas BuscarAlmacen(short idCAlmacenGas)
        {
            return uow.Repository<UnidadAlmacenGas>().GetSingle(
                x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                && x.Activo
            );
        }

        public List<UnidadAlmacenGas> BuscarTodosCamionetas(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                       && x.Activo && x.IdCamioneta != null).ToList();
        }
        public UnidadAlmacenGasCilindro BuscarCilindro(int idCilindro)
        {
            return uow.Repository<UnidadAlmacenGasCilindro>().GetSingle(x => x.IdCilindro.Equals(idCilindro));
        }
        public CamionetaCilindro BuscarCilindroEnCamioneta(int idCilindro)
        {
            return uow.Repository<CamionetaCilindro>().GetSingle(x => x.IdCilindro.Equals(idCilindro));
        }
        public List<UnidadAlmacenGasCilindro> BuscarTodosCilindros(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGasCilindro>().Get(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
        }
        public List<CamionetaCilindro> BuscarTodosCilindros(int idCamioneta)
        {
            return uow.Repository<CamionetaCilindro>().Get(x => x.IdCamioneta.Equals(idCamioneta)).ToList();
        }
        public List<AlmacenGas> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<AlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                      && x.Activo).ToList();
        }
        public UnidadAlmacenGas BuscarUnidadAlamcenGas(short idCAlmacenGas)
        {
            return uow.Repository<UnidadAlmacenGas>().GetSingle(x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                                                         && x.Activo);
        }

        internal AlmacenGasTomaLectura BuscarLectura(short idCAlmacenGas, byte tipoEvento, DateTime fecha)

        {

            return uow.Repository<AlmacenGasTomaLectura>().GetSingle(

                x => x.IdCAlmacenGas.Equals(idCAlmacenGas)

                && x.IdTipoEvento.Equals(tipoEvento)

                && x.FechaRegistro.ToShortDateString().Equals(fecha.ToShortDateString())

            );

        }

        public List<UnidadAlmacenGas> BuscarTodas(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                      && x.Activo).ToList();
        }
        public List<UnidadAlmacenGas> BuscarTodos(short idEmpresa, bool almacenGral, bool almacenAlterno)
        {
            if (almacenGral && almacenAlterno)
                return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                            && x.EsGeneral.Equals(almacenGral)
                                                            && x.Activo).ToList();

            if (almacenGral && !almacenAlterno)
                return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                            && x.EsGeneral.Equals(almacenGral)
                                                            && x.EsAlterno.Equals(almacenAlterno)
                                                            && x.Activo).ToList();

            if (!almacenGral && almacenAlterno)
                return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                           && x.EsAlterno.Equals(almacenAlterno)
                                                           && x.Activo).ToList();

            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                            && x.EsGeneral.Equals(almacenGral)
                                                            && x.Activo).ToList();
        }
        public AlmacenGas Buscar(short idAlmacenGas)
        {
            return uow.Repository<AlmacenGas>().GetSingle(x => x.IdAlmacenGas.Equals(idAlmacenGas)
                                                         && x.Activo);
        }
        public AlmacenGas BuscarPorEmpresa(short idEmpresa)
        {
            var alms = uow.Repository<AlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.Activo).ToList();

            return alms.FirstOrDefault();
        }
        public AlmacenGas ProductoAlmacenGas(short idEmpresa)
        {
            return uow.Repository<AlmacenGas>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa));
        }
        public AlmacenGasTomaLectura BuscarClaveOperacion(string claveOperacion)
        {
            return uow.Repository<AlmacenGasTomaLectura>().GetSingle(x => x.ClaveOperacion.Equals(claveOperacion));
        }
        public List<AlmacenGasRecargaFoto> BuscarImagenes(int idAlmacenGasRecarga)
        {
            return uow.Repository<AlmacenGasRecargaFoto>().Get(x => x.IdAlmacenGasRecarga.Equals(idAlmacenGasRecarga)).ToList();
        }

        public AlmacenGasRecarga BuscarRecargaClaveOperacion(string claveOperacion)
        {
            return uow.Repository<AlmacenGasRecarga>().GetSingle(x => x.ClaveOperacion.Equals(claveOperacion));
        }

        public List<AlmacenGasDescarga> BuscarTodasDescargasNoProcesadas()
        {
            return uow.Repository<AlmacenGasDescarga>().Get(x => !x.DatosProcesados.Value).ToList();
        }
        public List<AlmacenGasRecarga> BuscarTodasRecargasNoProcesadas()
        {
            return uow.Repository<AlmacenGasRecarga>().Get(x => !x.DatosProcesados).ToList();
        }
        public List<AlmacenGasRecarga> BuscarTodasRecargasNoProcesadas(byte idTipoEvento)
        {
            return uow.Repository<AlmacenGasRecarga>().Get(x => !x.DatosProcesados && x.IdTipoEvento.Equals(idTipoEvento)).ToList();
        }
        public List<AlmacenGasTraspaso> BuscarTodosTraspasosNoProcesadas()
        {
            return uow.Repository<AlmacenGasTraspaso>().Get(x => !x.DatosProcesados).ToList();
        }
        public List<AlmacenGasTraspaso> BuscarTodosTraspasosNoProcesadas(byte idTipoEvento)
        {
            return uow.Repository<AlmacenGasTraspaso>().Get(x => !x.DatosProcesados && x.IdTipoEvento.Equals(idTipoEvento)).ToList();
        }
        public List<AlmacenGasTraspasoFoto> BuscarImagenesTraspaso(short idEmpresa, short anio, byte mes, byte dia, short orden)
        {
            return uow.Repository<AlmacenGasTraspasoFoto>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                  && x.Year.Equals(anio)
                                                                  && x.Mes.Equals(mes)
                                                                  && x.Dia.Equals(dia)
                                                                  && x.Orden.Equals(orden)).ToList();
        }
        public List<AlmacenGasAutoConsumo> BuscarTodosAutoConsumosNoProcesadas()
        {
            return uow.Repository<AlmacenGasAutoConsumo>().Get(x => !x.DatosProcesados).ToList();
        }
        public List<AlmacenGasAutoConsumoFoto> BuscarImagenesAutoConsumo(short idEmpresa, short anio, byte mes, byte dia, short orden)
        {
            return uow.Repository<AlmacenGasAutoConsumoFoto>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                  && x.Year.Equals(anio)
                                                                  && x.Mes.Equals(mes)
                                                                  && x.Dia.Equals(dia)
                                                                  && x.Orden.Equals(orden)).ToList();
        }
        public List<AlmacenGasCalibracionFoto> BuscarImagenesCalibracion(int idCAlmacenGas)
        {
            return uow.Repository<AlmacenGasCalibracionFoto>().Get(x => x.IdCAlmacenGas.Equals(idCAlmacenGas)).ToList();
        }
        public List<AlmacenGasTomaLecturaFoto> BuscarImagenesTomaLectura(int idCAlmacenGas)
        {
            return uow.Repository<AlmacenGasTomaLecturaFoto>().Get(x => x.IdCAlmacenGas.Equals(idCAlmacenGas)).ToList();
        }
        public List<AlmacenGasTomaLectura> BuscarTodasLecturasNoProcesadas()
        {
            return uow.Repository<AlmacenGasTomaLectura>().Get(x => !x.DatosProcesados).ToList();
        }
        public List<AlmacenGasTomaLectura> BuscarTodasLecturasNoProcesadas(byte idTipoEvento)
        {
            return uow.Repository<AlmacenGasTomaLectura>().Get(x => x.IdTipoEvento.Equals(idTipoEvento)
                                                                && !x.DatosProcesados).ToList();
        }
        public List<AlmacenGasCalibracion> BuscarTodasCalibracionesNoProcesadas()
        {
            return uow.Repository<AlmacenGasCalibracion>().Get(x => !x.DatosProcesados).ToList();
        }

        public AlmacenGasAutoConsumo BuscarAutoconsumoClaveOperacion(string claveOperacion)
        {
            return uow.Repository<AlmacenGasAutoConsumo>().GetSingle(x => x.ClaveOperacion.Equals(claveOperacion));
        }

        public AlmacenGasCalibracion BuscarCalibracion(string claveOperacion)
        {
            return uow.Repository<AlmacenGasCalibracion>().GetSingle(x => x.ClaveOperacion.Equals(claveOperacion));
        }

        public RespuestaDto Insertar(AlmacenGasCalibracion carburacion)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            try
            {
                uow.Repository<AlmacenGasCalibracion>().Insert(carburacion);
                uow.SaveChanges();
                _respuesta.Id = carburacion.IdCAlmacenGas;
                _respuesta.EsInsercion = true;
                _respuesta.Exito = true;
                _respuesta.ModeloValido = true;
                _respuesta.Mensaje = Exito.OK;
            }
            catch (Exception ex)
            {
                _respuesta.Exito = false;
                _respuesta.Mensaje = string.Format(Error.C0002, "del la recarga.");
                _respuesta.MensajesError = CatchInnerException.Obtener(ex);
            }
            return _respuesta;
        }

        public AlmacenGasTraspaso BuscarTraspaso(string claveOperacion)
        {
            return uow.Repository<AlmacenGasTraspaso>().GetSingle(x => x.ClaveOperacion.Equals(claveOperacion));
        }

        public RespuestaDto Insertar(AlmacenGasTraspaso traspaso)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            try
            {
                uow.Repository<AlmacenGasTraspaso>().Insert(traspaso);
                uow.SaveChanges();
                _respuesta.Id = traspaso.IdCAlmacenGasEntrada;
                _respuesta.EsInsercion = true;
                _respuesta.Exito = true;
                _respuesta.ModeloValido = true;
                _respuesta.Mensaje = Exito.OK;
            }
            catch (Exception ex)
            {
                _respuesta.Exito = false;
                _respuesta.Mensaje = string.Format(Error.C0002, "del la recarga.");
                _respuesta.MensajesError = CatchInnerException.Obtener(ex);
            }
            return _respuesta;
        }


        public RespuestaDto Insertar(List<AlmacenGasMovimiento> ventamov)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            try
            {
                foreach (var iventa in ventamov)
                {
                    uow.Repository<AlmacenGasMovimiento>().Insert(iventa);
                }
                uow.SaveChanges();
                // _respuesta.Id = traspaso.IdCAlmacenGasEntrada;
                _respuesta.EsInsercion = true;
                _respuesta.Exito = true;
                _respuesta.ModeloValido = true;
                _respuesta.Mensaje = Exito.OK;
            }
            catch (Exception ex)
            {
                _respuesta.Exito = false;
                _respuesta.Mensaje = string.Format(Error.C0002, "del gas movimiento");
                _respuesta.MensajesError = CatchInnerException.Obtener(ex);
            }
            return _respuesta;
        }
        public List<ReporteDelDia> ObtenerReportes()
        {
            return uow.Repository<ReporteDelDia>().GetAll().ToList();

        }

        public RespuestaDto Insertar(ReporteDelDia reporte)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            try
            {
                uow.Repository<ReporteDelDia>().Insert(reporte);
                uow.SaveChanges();
                _respuesta.Id = (int)reporte.IdCAlmacenGas;
                _respuesta.EsInsercion = true;
                _respuesta.Exito = true;
                _respuesta.ModeloValido = true;
                _respuesta.Mensaje = Exito.OK;

            }
            catch (Exception ex)
            {
                _respuesta.Exito = false;
                _respuesta.Mensaje = string.Format(Error.C0002, "del la recarga.");
                _respuesta.MensajesError = CatchInnerException.Obtener(ex);
            }
            return _respuesta;
        }
    }
}





