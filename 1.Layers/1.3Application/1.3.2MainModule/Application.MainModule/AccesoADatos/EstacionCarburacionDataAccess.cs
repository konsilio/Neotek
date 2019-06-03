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
    public class EstacionCarburacionDataAccess
    {
        private SagasDataUow uow;

        public EstacionCarburacionDataAccess()
        {
            uow = new SagasDataUow();
        }        
        public EstacionCarburacion Buscar(int idEstacion)
        {
            return uow.Repository<EstacionCarburacion>().GetSingle(x => x.IdEstacionCarburacion.Equals(idEstacion)
                                                            && x.Activo);
        }
        public EstacionCarburacion BuscarEstacionCarburacion(UnidadAlmacenGas uAG)
        {
            if (uAG.EstacionCarburacion != null)
                return uAG.EstacionCarburacion;
            else
            {
                if (uAG.IdPipa != null && uAG.IdPipa > 0)
                    return Buscar(uAG.IdEstacionCarburacion.Value);
                else
                    return null;
            }
        }
        public RespuestaDto Insertar(EstacionCarburacion _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<EstacionCarburacion>().Insert(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdEstacionCarburacion;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del EstacionCarburacion");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(EstacionCarburacion _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.EstacionCarburacion>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdEstacionCarburacion;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del EstacionCarburacion"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public List<EstacionCarburacion> BuscarTodos()
        {
            return uow.Repository<EstacionCarburacion>().Get(x => x.Activo).ToList();
        }
        public List<EstacionCarburacion> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<EstacionCarburacion>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.Activo)
                                                         .ToList();
        }
    }
}
