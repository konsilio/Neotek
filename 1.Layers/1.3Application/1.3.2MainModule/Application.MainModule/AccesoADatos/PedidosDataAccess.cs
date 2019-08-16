using Application.MainModule.AdaptadoresDTO.Pedidos;
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
        public List<Pedido> Buscar()
        {
            return uow.Repository<Pedido>().Get(x => x.PedidoDetalle.Count > 0).ToList();
        }
        public List<Pedido> Buscar(short idempresa)
        {

            return uow.Repository<Pedido>().Get(x =>
            x.IdEmpresa.Equals(idempresa)
            && x.PedidoDetalle.Count > 0).ToList();
        }
        public List<Pedido> Buscar(short idempresa, DateTime periodo)
        {
            return uow.Repository<Pedido>().Get(x => x.IdEmpresa.Equals(idempresa)
                                                && x.FechaRegistro.Year.Equals(periodo.Year)
                                                && x.FechaRegistro.Month.Equals(periodo.Month)
                                                && x.PedidoDetalle.Count > 0).ToList();
        }
        public List<Pedido> Buscar(short idempresa, DateTime fi, DateTime ff)
        {
            return uow.Repository<Pedido>().Get(x => x.IdEmpresa.Equals(idempresa)
            && x.FechaRegistro >= fi && x.FechaRegistro <= ff
                                                && x.PedidoDetalle.Count > 0).ToList();
        }
        public List<PedidoDetalle> Buscar(int idPedido)
        {
            return uow.Repository<PedidoDetalle>().Get(x => x.IdPedido.Equals(idPedido)).ToList();
        }
        public List<RespuestaSatisfaccionPedido> BuscarEnc(int idPedido)
        {
            return uow.Repository<RespuestaSatisfaccionPedido>().Get(x => x.IdPedido.Equals(idPedido)).ToList();
        }
        public List<PedidoEstatus> BuscarEstatus()
        {
            return uow.Repository<PedidoEstatus>().GetAll().ToList();
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
                    if (_pro.IdPipa > 0)
                        foreach (var det in _pro.PedidoDetalle)
                        {
                            uow.Repository<Sagas.MainModule.Entidades.PedidoDetalle>().Update(det);
                        }
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
        public RespuestaDto Insertar(List<PedidoDetalle> cte)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    var query = new PedidosDataAccess().Buscar(cte[0].IdPedido);
                    if (query.Count() != cte.Count())
                    {
                        foreach (var it in query)
                        {
                            if (it.Cilindro45 == true && query.Where(x => x.Cilindro45 != null).Count() > 0 && cte.Where(x => x.Cilindro45 != null).Count() == 0)
                            {
                                var item = PedidosAdapter.FromEntity(it);
                                uow.Repository<Sagas.MainModule.Entidades.PedidoDetalle>().Delete(item);
                            }
                            if (it.Cilindro30 == true && query.Where(x => x.Cilindro30 != null).Count() > 0 && cte.Where(x => x.Cilindro30 != null).Count() == 0)
                            {
                                var item = PedidosAdapter.FromEntity(it);
                                uow.Repository<Sagas.MainModule.Entidades.PedidoDetalle>().Delete(item);
                            }
                            if (it.Cilindro20 == true && query.Where(x => x.Cilindro20 != null).Count() > 0 && cte.Where(x => x.Cilindro20 != null).Count() == 0)
                            {
                                var item = PedidosAdapter.FromEntity(it);
                                uow.Repository<Sagas.MainModule.Entidades.PedidoDetalle>().Delete(item);
                            }
                        }
                    }
                    foreach (var item in cte)
                    {
                        if (item.Cilindro45 == true)
                        {
                            if (query.Where(x => x.Cilindro45 != null).Count() > 0)
                            {
                                uow.Repository<Sagas.MainModule.Entidades.PedidoDetalle>().Update(item);
                            }
                            else
                            {
                                uow.Repository<PedidoDetalle>().Insert(item);
                            }
                        }
                        if (item.Cilindro30 == true)
                        {
                            if (query.Where(x => x.Cilindro30 != null).Count() > 0)
                            {
                                uow.Repository<Sagas.MainModule.Entidades.PedidoDetalle>().Update(item);
                            }
                            else
                            {
                                uow.Repository<PedidoDetalle>().Insert(item);
                            }
                        }
                        if (item.Cilindro20 == true)
                        {
                            if (query.Where(x => x.Cilindro20 != null).Count() > 0)
                            {
                                uow.Repository<Sagas.MainModule.Entidades.PedidoDetalle>().Update(item);
                            }
                            else
                            {
                                uow.Repository<PedidoDetalle>().Insert(item);
                            }
                        }
                    }
                    uow.SaveChanges();
                    _respuesta.Id = cte[0].IdPedido;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del pedidoDetalle");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Insertar(List<RespuestaSatisfaccionPedido> cte)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var item in cte)
                    {
                        uow.Repository<RespuestaSatisfaccionPedido>().Insert(item);
                    }

                    uow.SaveChanges();
                    _respuesta.Id = cte[0].IdPedido;
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
