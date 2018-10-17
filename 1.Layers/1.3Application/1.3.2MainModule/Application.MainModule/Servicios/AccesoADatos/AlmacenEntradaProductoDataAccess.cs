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
    public class AlmacenEntradaProductoDataAccess
    {
        private SagasDataUow uow;
        public AlmacenEntradaProductoDataAccess()
        {
            uow = new SagasDataUow();
        }

        public RespuestaDto Insertar(AlmacenEntradaProducto _Entrada, Sagas.MainModule.Entidades.Almacen _alm)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<AlmacenEntradaProducto>().Insert(_Entrada);
                    uow.Repository<Sagas.MainModule.Entidades.Almacen>().Update(_alm);
                    uow.SaveChanges();

                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del AlmacenEntradaProducto");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(AlmacenEntradaProducto _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<AlmacenEntradaProducto>().Update(_pro);
                    uow.SaveChanges();

                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del AlmacenEntradaProducto"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public List<AlmacenEntradaProducto> BuscarTodos()
        {
            return uow.Repository<AlmacenEntradaProducto>().GetAll().ToList();
        }
        public List<AlmacenEntradaProducto> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<AlmacenEntradaProducto>().Get(x => x.Almacen.IdEmpresa.Equals(idEmpresa)).ToList();
        }
        public AlmacenEntradaProducto Buscar(int IdRequisicion, int idOrdeCompra)
        {
            return uow.Repository<AlmacenEntradaProducto>().GetSingle(x => x.IdRequisicion.Equals(IdRequisicion)
                                                         && x.IdOrdenCompra.Equals(idOrdeCompra));
        }

    }
}
