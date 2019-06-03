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
    public class BancoDataAccess
    {
        private SagasDataUow uow;

        public BancoDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(Banco _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Banco>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.IdBanco;
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
        public RespuestaDto Actualizar(Banco _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Banco>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdBanco;
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
        public List<Banco> BuscarTodos()
        {
            return uow.Repository<Banco>().GetAll().ToList();
        }
        public Banco Buscar(int idBanco)
        {
            return uow.Repository<Banco>().GetSingle(x => x.IdBanco.Equals(idBanco));
        }
        public Banco BuscarNombreCorto(string NombreCorto)
        {
            return uow.Repository<Banco>().GetSingle(x => x.NombreCorto.Equals(NombreCorto));
        }
    }
}
