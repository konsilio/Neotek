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
    public class OperadorChoferDataAccess
    {
        private SagasDataUow uow;

        public OperadorChoferDataAccess()
        {
            uow = new SagasDataUow();
        }

        public RespuestaDto Insertar(OperadorChofer _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try 
                {
                    uow.Repository<OperadorChofer>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.IdOperadorChofer;
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
        public RespuestaDto Actualizar(OperadorChofer _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.OperadorChofer>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdOperadorChofer;
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
        public List<OperadorChofer> BuscarTodos()
        {
            return uow.Repository<OperadorChofer>().Get(x => x.Activo).ToList();
        }
        public List<OperadorChofer> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<OperadorChofer>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.Activo)
                                                         .ToList();
        }
        public OperadorChofer Buscar(int idOperadorChofer)
        {
            return uow.Repository<OperadorChofer>().GetSingle(x => x.IdOperadorChofer.Equals(idOperadorChofer)
                                                         && x.Activo);
        }
        public OperadorChofer BuscarPorUsuario(int idUsuario)
        {
            return uow.Repository<OperadorChofer>().GetSingle(x => x.IdUsuario.Equals(idUsuario)
                                                         && x.Activo);
        }
    }
}
