using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.UnitOfWork;
using Application.MainModule.DTOs.Respuesta;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class RequisicionDataAccess
    {
        private SagasDataUow uow;

        public RequisicionDataAccess()
        {
            uow = new SagasDataUow();
        }
        public Sagas.MainModule.Entidades.Requisicion Buscar(int IdRequisicion)
        {
            return uow.Repository<Sagas.MainModule.Entidades.Requisicion>().GetSingle(x => x.IdRequisicion.Equals(IdRequisicion) && x.IdRequisicionEstatus.Equals(0));
        }
        public int BuscarUltimaRequi()
        {
            if (uow.Repository<Sagas.MainModule.Entidades.Requisicion>().GetAll().ToList().Count.Equals(0))
                return 0;
            else
                return uow.Repository<Sagas.MainModule.Entidades.Requisicion>().GetAll().ToList().Last().IdRequisicion;
        }
        public Sagas.MainModule.Entidades.Requisicion BuscarPorNumeroRequisicion(string NumRequisicion)
        {
            return uow.Repository<Sagas.MainModule.Entidades.Requisicion>().GetSingle(x => x.NumeroRequisicion.Equals(NumRequisicion));
        }
        public List<Sagas.MainModule.Entidades.Requisicion> BuscarTodas()
        {
            return uow.Repository<Sagas.MainModule.Entidades.Requisicion>().GetAll().ToList();
        }
        public RespuestaRequisicionDto InsertarNueva(Sagas.MainModule.Entidades.Requisicion _req)
        {
            RespuestaRequisicionDto _respuesta = new RespuestaRequisicionDto();
            using (uow)
            {
                try
                {
                    _req.RequisicionEstatus = new Sagas.MainModule.Entidades.RequisicionEstatus()
                    {
                        IdRequisicionEstatus = 1,
                        Estatus = "Iniciada"
                    };
                    uow.Repository<Sagas.MainModule.Entidades.Requisicion>().Insert(_req);
                    uow.SaveChanges();
                    _respuesta.IdRequisicion = _req.IdRequisicion;
                    _respuesta.Exito = true;
                    _respuesta.Mensaje = _req.NumeroRequisicion;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = ex.Message;
                }
            }
            return _respuesta;
        }
        public RespuestaRequisicionDto Actualizar(Sagas.MainModule.Entidades.Requisicion _req)
        {
            RespuestaRequisicionDto _respuesta = new RespuestaRequisicionDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Requisicion>().Update(_req);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.Mensaje = string.Empty;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = ex.Message;
                }
            }
            return _respuesta;
        }
    }
}
