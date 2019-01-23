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
    public class EqTransporteDataAccess
    {
        private SagasDataUow uow;

        public EqTransporteDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(EquipoTransporte _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<EquipoTransporte>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.IdEquipoTransporte;
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
        public RespuestaDto Actualizar(EquipoTransporte _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.EquipoTransporte>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdEquipoTransporte;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del centro de costo"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public List<EquipoTransporte> BuscarTodos(int IdEquipoTransporte)
        {
            return uow.Repository<EquipoTransporte>().Get(x => x.IdEquipoTransporte.Equals(IdEquipoTransporte)
                                                        )
                                                         .ToList();
        }
        public List<EquipoTransporte> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<EquipoTransporte>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                        )
                                                         .ToList();
        }
        public EquipoTransporte Buscar(int IdEquipoTransporte)
        {
            return uow.Repository<EquipoTransporte>().GetSingle(x => x.IdEquipoTransporte.Equals(IdEquipoTransporte));
        }
    }
}
