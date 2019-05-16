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
    public class MantenimientoDataAccess
    {
        private SagasDataUow uow;

        public MantenimientoDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(CMantenimiento entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CMantenimiento>().Insert(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id_Mantenimiento;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del centro de costo");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;

        }
        public RespuestaDto Insertar(CMantenimiento entidad, DetalleMantenimiento entidadDetalle)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CMantenimiento>().Insert(entidad);
                    uow.Repository<DetalleMantenimiento>().Insert(entidadDetalle);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id_Mantenimiento;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del centro de costo");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(CMantenimiento entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CMantenimiento>().Update(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.Id_Mantenimiento;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del punto de venta"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public List<CMantenimiento> Obtener()
        {
            return uow.Repository<CMantenimiento>().Get(x => x.Activo).ToList();
        }
        public CMantenimiento Obtener(int id)
        {
            return uow.Repository<CMantenimiento>().GetSingle(x => x.Id_Mantenimiento.Equals(id));
        }
        public List<CMantenimiento> Obtener(short idEmpresa)
        {
            return uow.Repository<CMantenimiento>().Get(x => x.Id_Empresa.Equals(idEmpresa) && x.Activo).ToList();
        }
    }
}
