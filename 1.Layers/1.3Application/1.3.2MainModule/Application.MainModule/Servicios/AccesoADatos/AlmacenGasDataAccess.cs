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

        public List<AlmacenGasTomaLectura> BuscarLecturas(short idCAlmacenGas)
        {
            return uow.Repository<AlmacenGasTomaLectura>().Get(x => x.IdCAlmacenGas.Equals(idCAlmacenGas)
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

        public RespuestaDto Actualizar(AlmacenGas _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.AlmacenGas>().Update(_alm);
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
                    _respuesta.Mensaje = string.Format(Error.C0003, "del Almacen"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public List<UnidadAlmacenGas> BuscarTodosEstacionCarburacion(short idEmpresa)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                       && x.Activo && x.IdEstacionCarburacion!=null).ToList();
        }

        public List<AlmacenGas> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<AlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                      && x.Activo).ToList();
        }
        public List<UnidadAlmacenGas> BuscarTodos(short idEmpresa,bool almacenGral)
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                      && x.Activo && x.EsGeneral.Equals(almacenGral)).ToList();
        }
        public AlmacenGas Buscar(short idAlmacenGas)
        {
            return uow.Repository<AlmacenGas>().GetSingle(x => x.IdAlmacenGas.Equals(idAlmacenGas)
                                                         && x.Activo);
        }
        public AlmacenGas ProductoAlmacenGas(short idEmpresa)
        {
            return uow.Repository<AlmacenGas>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa));
        }
        public AlmacenGasTomaLectura BuscarClaveOperacion(string claveOperacion)
        {
            return uow.Repository<AlmacenGasTomaLectura>().GetSingle(x => x.ClaveOperacion.Equals(claveOperacion));
        }
        
    }
}
