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
using Sagas.MainModule.ObjetosValor.Enum;

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
        /// <summary>
        /// Permite realizar la busqueda de una entidad de tipo CamionetaCilindro
        /// para retornar la cantidad de cilindro que tiene la camioneta, se envian como parametros
        /// el id de la camioneta , el id del cilindro y el id de la empresa, tras finalizar retornara el 
        /// registro buscado 
        /// </summary>
        /// <param name="idCamioneta">Id de la camioneta </param>
        /// <param name="idCilindro">Id del cilindro </param>
        /// <param name="idEmpresa">Id de la empresa</param>
        /// <returns>Entidad de tipo CamionetaCilindro con el cilindro buscado </returns>
        public CamionetaCilindro BuscarCamionetaCilindro(int idCamioneta, int idCilindro, short idEmpresa)
        {
            return uow.Repository<CamionetaCilindro>().GetSingle(
                x => x.IdCamioneta.Equals(idCamioneta) &&
                x.IdCilindro.Equals(idCilindro) &&
                x.IdEmpresa.Equals(idEmpresa)
                );
        }
        /// <summary>
        /// Retorna una lista de lecturas de pipas ordenadas por el orden que sea mayor 
        /// </summary>
        /// <param name="idPipa">Id de la pipa</param>
        /// <param name="idCAlmacenGas"> Id de CAlmacenGas</param>
        /// <returns>Listado de lecturas en caso de no encontrar , retornara un listado vacio</returns>
        public List<AlmacenGasTomaLectura> ObtenerUltimaLecturasIniciales(short idCAlmacenGas)
        {
            List<AlmacenGasTomaLectura> inicial = new List<AlmacenGasTomaLectura>();
            inicial = uow.Repository<AlmacenGasTomaLectura>().Get(
                    x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                    && x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)
                    && x.FechaAplicacion.Equals(DateTime.Now)
                ).OrderByDescending(y => y.IdOrden).ToList();
            return inicial;
        }
        public List<AlmacenGasRecarga> ObtenerRecargaInicial(short IdCAlmacenGasEntrada)
        {
            return uow.Repository<AlmacenGasRecarga>().Get(
                x => x.IdCAlmacenGasEntrada.Equals(IdCAlmacenGasEntrada)
                ).ToList();
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
        public List<AlmacenGasTomaLectura> BuscarLecturas(short idCAlmacenGas, DateTime fecha)
        {
            return uow.Repository<AlmacenGasTomaLectura>().Get(x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                                                                    && x.FechaRegistro.Day.Equals(fecha.Day)
                                                                    && x.FechaRegistro.Month.Equals(fecha.Month)
                                                                    && x.FechaRegistro.Year.Equals(fecha.Year)).ToList();
        }
        public List<AlmacenGasTomaLecturaCilindro> BuscarLecturasCamioneta(short idCAlmacenGas)
        {
            return uow.Repository<AlmacenGasTomaLecturaCilindro>().Get(x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                                                    ).ToList();
        }
        /// <summary>
        /// Permite realizar la busqueda de un reporte del día en caso de que 
        /// exista alguno con los datos ya enviados se retornara como respuesta
        /// </summary>
        /// <param name="fecha">Fecha de busqueda</param>
        /// <param name="idCAlmacenGas">Id del CAlmacenGas buscado</param>
        /// <param name="idEmpresa">Id de la empresa que se busca</param>
        /// <returns>Entidad de típo reporte del día encontrada</returns>
        public ReporteDelDia BuscarReporte(DateTime fecha, short idCAlmacenGas, short idEmpresa)
        {
            return uow.Repository<ReporteDelDia>().GetSingle(x =>
            x.IdCAlmacenGas.Equals(idCAlmacenGas) &&
            x.FechaReporte.Equals(fecha) &&
            x.IdEmpresa.Equals(idEmpresa)
            );
        }
        public List<AlmacenGasTomaLectura> BuscarLecturas(short idCAlmacenGas, int mes, int anio)
        {
            return uow.Repository<AlmacenGasTomaLectura>().Get(x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
            && x.FechaAplicacion.Month.Equals(mes) && x.FechaAplicacion.Year.Equals(anio)).ToList();
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
                    _respuesta.Id = _autoconsumo.Orden;
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
        /// <summary>
        /// Permite buscar en el reporsitorio de AlmacenGasTomaLectura la última lectura inicial
        /// que se encuentre con respecto al id de almacen (idCalmacenGas) y la fecha (fecha)
        /// que se envie como parametro
        /// </summary>
        /// <param name="idCAlmacenGas">Id de CAlmacenGas a consultar </param>
        /// <param name="fecha">Fecha de busqueda de dicha operación</param>
        /// <returns>Entidad AlmacenGasTomaLectura con la última encontrada</returns>
        public AlmacenGasTomaLectura ObtenerUltimaLecturaInicial(short idCAlmacenGas, DateTime fecha)
        {
            return uow.Repository<AlmacenGasTomaLectura>().
                GetSingle(
                x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                && x.FechaAplicacion.Day.Equals(fecha.Day)
                && x.FechaAplicacion.Month.Equals(fecha.Month)
                && x.FechaAplicacion.Year.Equals(fecha.Year)
                && x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)
                && x.IdOrden > 0
                );
        }
        public AlmacenGasTomaLectura ObtenerUltimaLecturaFinal(short idCAlmacenGas, DateTime fecha)
        {
            return uow.Repository<AlmacenGasTomaLectura>().
                GetSingle(
                x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                && x.FechaAplicacion.Day.Equals(fecha.Day)
                && x.FechaAplicacion.Month.Equals(fecha.Month)
                && x.FechaAplicacion.Year.Equals(fecha.Year)
                && x.IdTipoEvento.Equals(TipoEventoEnum.Final)
                && x.IdOrden > 0
                );
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
                            uow.Repository<AlmacenGasRecargaFoto>().Update(x));

                    if (aplicaRecarga.RecargaLecturaFinalSinNavProp != null)
                        uow.Repository<AlmacenGasRecarga>().Update(aplicaRecarga.RecargaLecturaFinalSinNavProp);

                    if (aplicaRecarga.RecargaLecturaFinalFotos != null && aplicaRecarga.RecargaLecturaFinalFotos.Count > 0)
                        aplicaRecarga.RecargaLecturaFinalFotos.ToList().ForEach(x =>
                            uow.Repository<AlmacenGasRecargaFoto>().Update(x));

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
                catch (Exception)
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
                catch (Exception)
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
                            uow.Repository<AlmacenGasAutoConsumoFoto>().Update(x));
                    if (aplicaAutoConsumo.AutoConsumoLecturaFinalSinNavProp != null)
                        uow.Repository<AlmacenGasAutoConsumo>().Update(aplicaAutoConsumo.AutoConsumoLecturaFinalSinNavProp);
                    if (aplicaAutoConsumo.AutoConsumoLecturaFinalFotos != null && aplicaAutoConsumo.AutoConsumoLecturaFinalFotos.Count > 0)
                        aplicaAutoConsumo.AutoConsumoLecturaFinalFotos.ToList().ForEach(x =>
                            uow.Repository<AlmacenGasAutoConsumoFoto>().Update(x));

                    // Agregar al modelo de dominio AlmacenGasMovimiento 
                    if (aplicaAutoConsumo.MovimientoSalida != null)
                        uow.Repository<AlmacenGasMovimiento>().Insert(aplicaAutoConsumo.MovimientoSalida);
                    if (uow.repositories.Count > 0)
                        uow.SaveChanges();
                    //_respuesta.Id = aplicaAutoConsumo.IdCAlmacenGas;
                    //_respuesta.Exito = true;
                    //_respuesta.EsActulizacion = true;
                    //_respuesta.ModeloValido = true;
                    //_respuesta.Mensaje = Exito.OK;
                }
                catch (Exception)
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
                            uow.Repository<AlmacenGasCalibracionFoto>().Update(x));

                    if (aplicaCalibracion.CalibracionLecturaFinalSinNavProp != null)
                        uow.Repository<AlmacenGasCalibracion>().Update(aplicaCalibracion.CalibracionLecturaFinalSinNavProp);

                    if (aplicaCalibracion.CalibracionLecturaFinalFotos != null && aplicaCalibracion.CalibracionLecturaFinalFotos.Count > 0)
                        aplicaCalibracion.CalibracionLecturaFinalFotos.ToList().ForEach(x =>
                            uow.Repository<AlmacenGasCalibracionFoto>().Update(x));

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
                catch (Exception)
                {
                    //_respuesta.Exito = false;
                    //_respuesta.Mensaje = string.Format(Error.C0003, "de la unidad de almacén"); ;
                    //_respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
        }
        public List<Camioneta> ObtenerCamionetasEmpresa(short idEmpresa)
        {
            return uow.Repository<Camioneta>().Get(
                x => x.IdEmpresa.Equals(idEmpresa) && x.Activo
                ).ToList();
        }
        public Camioneta ObtenerCamioneta(int IdC)
        {
            return uow.Repository<Camioneta>().Get(
                x => x.IdCamioneta.Equals(IdC)).FirstOrDefault();
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
                            uow.Repository<AlmacenGasTomaLecturaFoto>().Update(x));

                    //if (aplicaTomaLectura.TomaLecturaLectura != null)
                    //    uow.Repository<AlmacenGasTomaLectura>().Update(aplicaTomaLectura.TomaLecturaLecturaFinalSinNavProp);

                    //if (aplicaTomaLectura.TomaLecturaLecturaFinalFotos != null && aplicaTomaLectura.TomaLecturaLecturaFinalFotos.Count > 0)
                    //    aplicaTomaLectura.TomaLecturaLecturaFinalFotos.ToList().ForEach(x =>
                    //        uow.Repository<AlmacenGasTomaLecturaFoto>().Update(x)
                    //    );

                    if (aplicaTomaLectura.MovimientoUnidad != null)
                        if (BuscarMovimientos(aplicaTomaLectura.MovimientoUnidad.IdEmpresa, aplicaTomaLectura.MovimientoUnidad.Year, aplicaTomaLectura.MovimientoUnidad.Mes, aplicaTomaLectura.MovimientoUnidad.Dia, aplicaTomaLectura.MovimientoUnidad.Orden) == null)
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
        public List<UnidadAlmacenGas> BuscarTodosPuntosVenta(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                            && (x.IdEstacionCarburacion != null || x.IdPipa != null || x.IdCamioneta != null)
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
        public AlmacenGasMovimiento BuscarMovimientos(short idempresa, short year, byte mes, byte dia, short orden)
        {
            return uow.Repository<AlmacenGasMovimiento>().GetSingle(x => x.Year.Equals(year)
                                                                && x.Mes.Equals(mes)
                                                                && x.Dia.Equals(dia)
                                                                && x.Orden.Equals(orden));
        }
        public List<AlmacenGasMovimiento> BuscarMovimientosEnInventario(short idEmpresa, short idAlmacenGas, short year, byte mes, byte dia)
        {
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                                && x.IdAlmacenGas.Equals(idAlmacenGas)
                                                                && x.Year.Equals(year)
                                                                && x.Mes.Equals(mes)
                                                                && x.Dia.Equals(dia)).ToList();
        }
        /// <summary>
        /// Permite verificar si hay una lectura final registrada 
        /// para poder continuar con el corte de caja , en caso de 
        ///  existir una lectura se retorna una entidad de tipo AlmacenGasTomaLectura
        /// </summary>
        /// <param name="fecha">Fecha actual de registro</param>
        /// <param name="idCAlmacenGas">Id del CAlmacenGas</param>
        /// <returns>Una entidad de tipo AlmacenGasTomaLectura en caso de existir una lectura </returns>
        public AlmacenGasTomaLectura ObtenerLectura(DateTime fecha, short idCAlmacenGas)
        {
            return uow.Repository<AlmacenGasTomaLectura>().GetSingle(x =>
            x.IdCAlmacenGas.Equals(idCAlmacenGas) &&
            x.FechaRegistro.Day.Equals(fecha.Day) &&
            x.FechaRegistro.Month.Equals(fecha.Month) &&
            x.FechaRegistro.Year.Equals(fecha.Year) &&
            x.IdTipoEvento.Equals(TipoEventoEnum.Final)
            ) ?? null;
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
        /// <summary>
        /// Permite retornar una lectura de un CAlmacenGas tomando como parametro
        /// el id de este en la base de datos, una fecha en especifico y una bandera 
        /// de que si este se buscara como inicial o final, se retornara un objeto de tipo
        /// entidad AlmacenGasTomaLectura con la busqueda
        /// </summary>
        /// <param name="idCAlmacenGas">Id del IdCAlmacenGas</param>
        /// <param name="fecha">Fecha que se requiere buscar</param>
        /// <param name="inicial">Determina si se retornara una lectura inicial o final</param>
        /// <returns>Entidad de tipo AlmacenGasTomaLectura con los datos encontrados</returns>
        public AlmacenGasTomaLectura BuscarLectura(short idCAlmacenGas, DateTime fecha, bool inicial)
        {
            return uow.Repository<AlmacenGasTomaLectura>().GetSingle(
                x => x.IdCAlmacenGas.Equals(idCAlmacenGas) &&
                x.FechaAplicacion.Day.Equals(fecha.Day) &&
                x.FechaAplicacion.Month.Equals(fecha.Month) &&
                x.FechaAplicacion.Day.Equals(fecha.Day) &&
                x.IdTipoEvento.Equals(inicial ? TipoEventoEnum.Inicial : TipoEventoEnum.Final)
                );
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
        public List<AlmacenGasTraspaso> Traspasos(short idCAlmacenGas)
        {
            return uow.Repository<AlmacenGasTraspaso>().Get(x => x.IdCAlmacenGasSalida.Equals(idCAlmacenGas)).ToList();
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
            return uow.Repository<AlmacenGasMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa) && x.IdAlmacenGas.Equals(idAlmacenGas)).LastOrDefault();
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
            return uow.Repository<UnidadAlmacenGas>().Get(
                x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                && x.Activo
            ).FirstOrDefault();
        }
        public UnidadAlmacenGas BuscarAlmacenPorPipa(int id)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdPipa == id).FirstOrDefault();
        }
        public UnidadAlmacenGas BuscarAlmacenPorCamioneta(int id)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdCamioneta == id).FirstOrDefault();
        }
        public UnidadAlmacenGas BuscarAlmacenPrincipal(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(
                x => x.IdEmpresa.Equals(idEmpresa)
                && x.EsGeneral
                && x.Activo
            ).FirstOrDefault();
        }
        public List<UnidadAlmacenGas> BuscarTodosCamionetas(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                       && x.Activo && x.IdCamioneta != null).ToList();
        }
        public UnidadAlmacenGasCilindro BuscarCilindro(int idCilindro)
        {
            return uow.Repository<UnidadAlmacenGasCilindro>().Get(x => x.IdCilindro.Equals(idCilindro)).FirstOrDefault();
        }
        public CamionetaCilindro BuscarCilindroEnCamioneta(int idCilindro)
        {
            return uow.Repository<CamionetaCilindro>().Get(x => x.IdCilindro.Equals(idCilindro)).FirstOrDefault();
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
            return uow.Repository<AlmacenGasTomaLectura>().Get(
                x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
                && x.IdTipoEvento.Equals(tipoEvento)
                && (x.FechaRegistro.Year.Equals(fecha.Year) && x.FechaRegistro.Month.Equals(fecha.Month) && x.FechaRegistro.Day.Equals(fecha.Day))
            ).OrderByDescending(x => x.IdOrden).FirstOrDefault();
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
        public List<AlmacenGasRecarga> BuscarTodasRecargas(int IdCAlmacenGas, DateTime date)
        {
            return uow.Repository<AlmacenGasRecarga>().Get(x => x.IdCAlmacenGasSalida.Equals(IdCAlmacenGas)
                                                                       && x.FechaAplicacion.Day.Equals(date.Day)
                                                                       && x.FechaAplicacion.Month.Equals(date.Month)
                                                                       && x.FechaAplicacion.Year.Equals(date.Year)).ToList(); ;
        }
        public List<AlmacenGasRecarga> BuscarTodasRecargasNoProcesadas(byte idTipoEvento)
        {
            return uow.Repository<AlmacenGasRecarga>().Get(x => !x.DatosProcesados && x.IdTipoEvento.Equals(idTipoEvento)).ToList();
        }
        public List<AlmacenGasTraspaso> BuscarTodosTraspasosNoProcesadas()
        {
            return uow.Repository<AlmacenGasTraspaso>().Get(x => !x.DatosProcesados).ToList();
        }
        public List<AlmacenGasTraspaso> BuscarTodosTraspasos(int IdCAlmacenGas, DateTime date)
        {
            return uow.Repository<AlmacenGasTraspaso>().Get(x => x.IdCAlmacenGasSalida.Equals(IdCAlmacenGas)
                                                                       && x.FechaAplicacion.Day.Equals(date.Day)
                                                                       && x.FechaAplicacion.Month.Equals(date.Month)
                                                                       && x.FechaAplicacion.Year.Equals(date.Year)).ToList(); ;
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
        public List<AlmacenGasAutoConsumo> BuscarTodosAutoConsumos(DateTime fi)
        {
            return uow.Repository<AlmacenGasAutoConsumo>().GetAll().Where(x => x.FechaRegistro > fi).ToList();
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
        public List<AlmacenGasCalibracion> BuscarTodasCalibraciones(int IdCAlmacenGas, DateTime fecha)
        {
            return uow.Repository<AlmacenGasCalibracion>().Get(x => x.IdCAlmacenGas.Equals(IdCAlmacenGas)
                                                                && x.FechaRegistro.Day.Equals(fecha.Day)
                                                                && x.FechaRegistro.Month.Equals(fecha.Month)
                                                                && x.FechaRegistro.Year.Equals(fecha.Year)).ToList();
        }
        public AlmacenGasAutoConsumo BuscarAutoconsumoClaveOperacion(string claveOperacion)
        {
            return uow.Repository<AlmacenGasAutoConsumo>().GetSingle(x => x.ClaveOperacion.Equals(claveOperacion));
        }
        public List<AlmacenGasAutoConsumo> BuscarAutoconsumo(int IdCAlmcenGas, DateTime date)
        {
            return uow.Repository<AlmacenGasAutoConsumo>().Get(x => x.IdCAlmacenGasSalida == IdCAlmcenGas
                                                                       && x.FechaAplicacion.Day == date.Day
                                                                       && x.FechaAplicacion.Month == date.Month
                                                                       && x.FechaAplicacion.Year == date.Year).ToList();
        }
        public List<AlmacenGasAutoConsumo> BuscarAutoconsumo(DateTime date)
        {
            return uow.Repository<AlmacenGasAutoConsumo>().Get(x => x.FechaAplicacion.Day == date.Day
                                                                       && x.FechaAplicacion.Month == date.Month
                                                                       && x.FechaAplicacion.Year == date.Year).ToList();
        }
        public AlmacenGasAutoConsumo BuscarAutoconsumoInicial(AlmacenGasAutoConsumo final)
        {
            return uow.Repository<AlmacenGasAutoConsumo>().Get(x =>
                x.Year.Equals(final.Year) &&
                x.Mes.Equals(final.Mes) &&
                x.Dia.Equals(final.Dia) &&
                x.IdCAlmacenGasEntrada.Equals(final.IdCAlmacenGasEntrada) &&
                x.IdCAlmacenGasSalida.Equals(final.IdCAlmacenGasSalida) &&
                x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)).OrderByDescending(o => o.Orden).FirstOrDefault();
        }
        public AlmacenGasAutoConsumo BuscarAutoconsumo(AlmacenGasAutoConsumo ac)
        {
            return uow.Repository<AlmacenGasAutoConsumo>().Get(x =>
                x.Year.Equals(ac.Year) &&
                x.Mes.Equals(ac.Mes) &&
                x.Dia.Equals(ac.Dia) &&
                x.IdCAlmacenGasEntrada.Equals(ac.IdCAlmacenGasEntrada) &&
                x.IdCAlmacenGasSalida.Equals(ac.IdCAlmacenGasSalida) &&
                x.Orden == ac.Orden).FirstOrDefault();
        }
        public AlmacenGasAutoConsumo BuscarAutoconsumo(int orden)
        {
            return uow.Repository<AlmacenGasAutoConsumo>().Get(x => x.Orden.Equals((short)orden)).FirstOrDefault();
        }
        public bool BuscarSiEstaRelacionado(AlmacenGasAutoConsumo orden)
        {
            var autoconusmo = uow.Repository<AlmacenGasAutoConsumo>().Get(x =>
                                            x.Year.Equals(orden.Year) &&
                                            x.Mes.Equals(orden.Mes) &&
                                            x.Dia.Equals(orden.Dia) &&
                                            x.IdCAlmacenGasEntrada.Equals(orden.IdCAlmacenGasEntrada) &&
                                            x.IdCAlmacenGasSalida.Equals(orden.IdCAlmacenGasSalida) &&
                                            x.OrdenRelacion == orden.Orden).FirstOrDefault();
            if (autoconusmo != null)
                return true;
            else
                return false;
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
        public List<ReporteDelDia> ObtenerReportesDelDia()
        {
            var fecha = DateTime.Now;
            return uow.Repository<ReporteDelDia>().Get(x => x.Year.Equals(fecha.Year)
                                                            && x.Mes.Equals(fecha.Month)
                                                            && x.Dia.Equals(fecha.Day)).ToList();

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





