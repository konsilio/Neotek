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
        public RespuestaDto Insertar(Pipa _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Pipa>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.IdPipa;
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
        public RespuestaDto Actualizar(Pipa _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Pipa>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdPipa;
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
        public List<Pipa> ObtenerPipas(short idEmpresa)
        {
            return uow.Repository<Pipa>().Get(x => x.IdEmpresa.Equals(idEmpresa) && x.Activo).ToList();
        }
    }
}
