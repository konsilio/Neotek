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
    public class PipaDataAccess
    {
        private SagasDataUow uow;

        public PipaDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(Pipa entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Pipa>().Insert(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.IdPipa;
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
        public RespuestaDto Actualizar(Pipa entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Pipa>().Update(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.IdPipa;
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
        public RespuestaDto Borrar(Pipa entidad)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Pipa>().Delete(entidad);
                    uow.SaveChanges();
                    _respuesta.Id = entidad.IdPipa;
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
        public Pipa ObtenerPipa(int IdP)
        {
            return uow.Repository<Pipa>().GetSingle(
                x => x.IdPipa.Equals(IdP) && x.Activo
                );
        }
        public Pipa ObtenerPipa(int IdP, bool Activo)
        {
            return uow.Repository<Pipa>().GetSingle(
                x => x.IdPipa.Equals(IdP));
        }
        public List<Pipa> ObtenerPipas()
        {
            return uow.Repository<Pipa>().Get(x => x.Activo).ToList();
        }
        public List<Pipa> ObtenerPipas(short idEmpresa)
        {
            return uow.Repository<Pipa>().Get(x => x.IdEmpresa.Equals(idEmpresa) && x.Activo).ToList();
        }
        public Pipa ObtenerPipa(short idEmpresa, string nom, string num)
        {
            return uow.Repository<Pipa>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa) && x.Activo && x.Nombre.Equals(nom.Trim()) && x.Numero.Equals(num.Trim()));
        }
    }
}
