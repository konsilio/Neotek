using Application.MainModule.DTOs;
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

namespace Application.MainModule.AccesoADatos
{
    public class ControlAsistenciaDataAccess
    {
        private SagasDataUow uow;
        public ControlAsistenciaDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(ControlAsistencia entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    entidad.FechaRegistro = DateTime.Now;
                    uow.Repository<ControlAsistencia>().Insert(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.IdControlAsistencia;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del control de asistencia");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(ControlAsistencia entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<ControlAsistencia>().Update(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.IdControlAsistencia;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del control de asistencia"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public List<ControlAsistencia> Buscar(short idEmpresa)
        {
            return uow.Repository<ControlAsistencia>().Get(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
        }
        public List<ControlAsistencia> Buscar(short idEmpresa, PeriodoDTO periodo)
        {
            return uow.Repository<ControlAsistencia>().Get(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
        }
        public List<ControlAsistencia> Buscar(int idUsuario)
        {
            return uow.Repository<ControlAsistencia>().Get(x => x.IdUsuario.Equals(idUsuario)).ToList();
        }
        public List<ControlAsistencia> Buscar(int idUsuario, PeriodoDTO periodo)
        {
            return uow.Repository<ControlAsistencia>().Get(x => x.IdUsuario.Equals(idUsuario)).ToList();
        }
        public ControlAsistencia Buscar(int idUsuario, DateTime fecha)
        {
            return uow.Repository<ControlAsistencia>().GetSingle(x => x.IdUsuario.Equals(idUsuario)
            && x.FechaRegistro.Year.Equals(fecha.Year)
            && x.FechaRegistro.Month.Equals(fecha.Month)
            && x.FechaRegistro.Day.Equals(fecha.Day)
            && x.Estatus);
        }

    }
}
