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
    public class PedidosDataAccess
    {
        private SagasDataUow uow;
        public PedidosDataAccess()
        {
            uow = new SagasDataUow();
        }
        public List<Pedido> BuscarTodos()
        {
            return uow.Repository<Pedido>().GetAll().ToList();
        }

        public List<PedidoDetalle> Buscar(int idPedido)
        {
            return uow.Repository<PedidoDetalle>().Get(x=> x.IdPedido.Equals(idPedido)).ToList();
        }
        public Pedido BuscarPedido(int idPedido)
        {
            return uow.Repository<Pedido>().GetSingle(x => x.IdPedido.Equals(idPedido));
        }
        public RespuestaDto Actualizar(Pedido _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Pedido>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdPedido;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del pedido"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Insertar(Pedido cte)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Pedido>().Insert(cte);
                    uow.SaveChanges();
                    _respuesta.Id = cte.IdPedido;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del pedido");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
    }
}
