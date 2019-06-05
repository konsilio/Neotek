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
    public class ProveedorDataAccess
    {
        private SagasDataUow uow;

        public ProveedorDataAccess()
        {
            uow = new SagasDataUow();
        }

        public RespuestaDto Insertar(Proveedor _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Proveedor>().Insert(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdProveedor;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del Proveedor");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(Proveedor _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Proveedor>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdProveedor;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del Proveedor"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public List<Proveedor> BuscarTodos()
        {
            return uow.Repository<Proveedor>().Get(x => x.Activo).ToList();
        }

        public List<Proveedor> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<Proveedor>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.Activo)
                                                         .ToList();
        }

        public Proveedor Buscar(int idProveedor)
        {
            return uow.Repository<Proveedor>().GetSingle(x => x.IdProveedor.Equals(idProveedor)
                                                         && x.Activo);
        }
    }
}
