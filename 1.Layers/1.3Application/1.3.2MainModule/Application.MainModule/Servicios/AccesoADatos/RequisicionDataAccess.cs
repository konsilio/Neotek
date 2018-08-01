using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagas.MainModule.Entidades;
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
        public void Acualizar(Requisicion req)
        {
            uow.Repository<Requisicion>().Update(req);
        }
        public Requisicion Buscar(int IdRequisicion)
        {
            return uow.Repository<Requisicion>().GetSingle(x => x.IdRequisicion.Equals(IdRequisicion) && x.IdRequisicionEstatus.Equals(0));
        }
        public List<Requisicion> BuscarTodas()
        {
            return uow.Repository<Requisicion>().GetAll().ToList();
        }
        public RespuestaRequisicionDto Insertar(Requisicion _req)
        {
            RespuestaRequisicionDto _respuesta = new RespuestaRequisicionDto();
            using (uow)
            {
                try
                {                    
                    uow.Repository<Requisicion>().Insert(_req);
                    uow.SaveChanges();
                    _respuesta.IdRequisicion = _req.IdRequisicion;
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


        public RespuestaOperacionDto Actualizar(ZonaMenosEconomica zona)
        {
            using (uow)
            {
                try
                {
                    uow.Repository<ZonaMenosEconomica>().Update(zona);
                    uow.SaveChanges();
                    _respuesta.Guardado = true;
                    _respuesta.ModeloValido = true;
                }
                catch (Exception ex)
                {
                    _respuesta.Guardado = false;
                    _respuesta.Mensaje = ex.Message;
                }
            }

            return _respuesta;
        }
    }
}
