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
        public List<UnidadAlmacenGas> BuscarTodosEstacionCarburacion(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                            && x.IdEstacionCarburacion != null
                                                            && x.Activo).ToList();
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
        public List<UnidadAlmacenGas> BuscarTodosCamionetas(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                       && x.Activo && x.IdCamioneta != null).ToList();
        }
        public UnidadAlmacenGasCilindro BuscarCilindro(int idCilindro)
        {
            return uow.Repository<UnidadAlmacenGasCilindro>().GetSingle(x => x.IdCilindro.Equals(idCilindro));
        }
        public List<UnidadAlmacenGasCilindro> BuscarTodosCilindros(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGasCilindro>().Get(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
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
        public List<AlmacenGasTomaLectura> BuscarTodasLecturasNoProcesadas()
        {
            return uow.Repository<AlmacenGasTomaLectura>().Get(x => !x.DatosProcesados).ToList();
        }
        public List<AlmacenGasAutoConsumo> BuscarTodasAutoConsumosNoProcesadas()
        {
            return uow.Repository<AlmacenGasAutoConsumo>().Get(x => !x.DatosProcesados).ToList();
        }
        public List<AlmacenGasTraspaso> BuscarTodasTraspasosNoProcesadas()
        {
            return uow.Repository<AlmacenGasTraspaso>().Get(x => !x.DatosProcesados).ToList();
        }
        public List<AlmacenGasCalibracion> BuscarTodasCalibracionesNoProcesadas()
        {
            return uow.Repository<AlmacenGasCalibracion>().Get(x => !x.DatosProcesados).ToList();
        }
    }
}
      
