using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.UnitOfWork;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class OrdenCompraProductoDataAccess
    {
        private SagasDataUow uow;

        public OrdenCompraProductoDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Actualizar(List<OrdenCompraProducto> prods, OrdenCompra oc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<OrdenCompra>().Update(oc);
                    foreach (var p in prods)                    
                        uow.Repository<OrdenCompraProducto>().Update(p);                    
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.MensajesError = new List<string>();
                    _respuesta.Exito = false;
                    _respuesta.MensajesError.Add(string.Concat(Error.OC0001, " | ", ex.Message));
                    if (ex.InnerException != null)
                        _respuesta.MensajesError.Add(ex.InnerException.Message);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(List<OrdenCompraProducto> prods, List<OrdenCompra> ocs)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var oc in ocs)
                        uow.Repository<OrdenCompra>().Update(oc);
                    foreach (var p in prods)                    
                        uow.Repository<OrdenCompraProducto>().Update(p);                    
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.MensajesError = new List<string>();
                    _respuesta.Exito = false;
                    _respuesta.MensajesError.Add(string.Concat(Error.OC0001, " | ", ex.Message));
                    if (ex.InnerException != null)
                        _respuesta.MensajesError.Add(ex.InnerException.Message);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(OrdenCompraProducto prod)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<OrdenCompraProducto>().Update(prod);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.MensajesError = new List<string>();
                    _respuesta.Exito = false;
                    _respuesta.MensajesError.Add(string.Concat(Error.OC0001, " | ", ex.Message));
                    if (ex.InnerException != null)
                        _respuesta.MensajesError.Add(ex.InnerException.Message);
                }
            }
            return _respuesta;
        }
        public List<OrdenCompraProducto> Buscar(int idOrdenCompra, int idProducto)
        {
           return  uow.Repository<OrdenCompraProducto>().Get(x => x.IdOrdenCompra.Equals(idOrdenCompra) && x.IdProducto.Equals(idProducto)).ToList();
        }
        public List<OrdenCompraProducto> Buscar(int idOrdenCompra)
        {
            return uow.Repository<OrdenCompraProducto>().Get(x => x.IdOrdenCompra.Equals(idOrdenCompra)).ToList();
        }
    }
}
