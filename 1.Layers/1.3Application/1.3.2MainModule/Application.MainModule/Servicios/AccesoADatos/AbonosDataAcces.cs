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
    public class AbonosDataAcces
    {

        private SagasDataUow uow;

        public AbonosDataAcces()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(Abono _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Abono>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.IdAbono;
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
        public RespuestaDto Actualizar(Abono _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Abono>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdAbono;
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
        public List<Abono> BuscarTodos()
        {
            return uow.Repository<Abono>().GetAll().ToList();
        }
        public List<Cargo> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<Cargo>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                        )
                                                         .ToList();
        }
        public Cargo Buscar(int idCargo)
        {
            return uow.Repository<Cargo>().GetSingle(x => x.IdCargo.Equals(idCargo));
        }
        //public Abono BuscarNumero(short idEmpresa, string numero)
        //{
        //    return uow.Repository<Abono>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
        //                                                     && x.Numero.Equals(numero)
        //                                                     && x.Activo);
        //}
        //public Abono BuscarDescripcion(short idEmpresa, string descripcion)
        //{
        //    return uow.Repository<Abono>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
        //                                                     && x.Descripcion.Equals(descripcion)
        //                                                     && x.Activo);
        //}
    }
}
