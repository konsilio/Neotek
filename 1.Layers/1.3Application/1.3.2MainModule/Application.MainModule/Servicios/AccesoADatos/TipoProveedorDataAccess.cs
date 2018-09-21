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
    public class TipoProveedorDataAccess
    {
        private SagasDataUow uow;

        public TipoProveedorDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(TipoProveedor _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<TipoProveedor>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.IdTipoProveedor;
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
        public RespuestaDto Actualizar(TipoProveedor _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.TipoProveedor>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdTipoProveedor;
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
        public List<TipoProveedor> BuscarTodos()
        {
            return uow.Repository<TipoProveedor>().Get(x => x.Activo).ToList();
        }        
        public TipoProveedor Buscar(int idTipoProveedor)
        {
            return uow.Repository<TipoProveedor>().GetSingle(x => x.IdTipoProveedor.Equals(idTipoProveedor)
                                                         && x.Activo);
        }
        public TipoProveedor BuscarTipo(string Tipo)
        {
            return uow.Repository<TipoProveedor>().GetSingle(x =>  x.Tipo.Equals(Tipo)
                                                             && x.Activo);
        }        
    }
}
