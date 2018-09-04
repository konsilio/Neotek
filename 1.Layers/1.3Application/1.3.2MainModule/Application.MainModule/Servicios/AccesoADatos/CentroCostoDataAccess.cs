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
    public class CentroCostoDataAccess
    {
        private SagasDataUow uow;

        public CentroCostoDataAccess()
        {
            uow = new SagasDataUow();
        }

        public RespuestaDto Insertar(CentroCosto _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CentroCosto>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.IdCentroCosto;
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
        public RespuestaDto Actualizar(CentroCosto _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.CentroCosto>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdCentroCosto;
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
        public List<CentroCosto> BuscarTodos()
        {
            return uow.Repository<CentroCosto>().Get(x => x.Activo).ToList();
        }
        public List<CentroCosto> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<CentroCosto>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.Activo)
                                                         .ToList();
        }
        public CentroCosto Buscar(int idCentroCosto)
        {
            return uow.Repository<CentroCosto>().GetSingle(x => x.IdCentroCosto.Equals(idCentroCosto)
                                                         && x.Activo);
        }
        public CentroCosto BuscarNumero(short idEmpresa, string numero)
        {
            return uow.Repository<CentroCosto>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                             && x.Numero.Equals(numero)
                                                             && x.Activo);
        }
        public CentroCosto BuscarDescripcion(short idEmpresa, string descripcion)
        {
            return uow.Repository<CentroCosto>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                             && x.Descripcion.Equals(descripcion)
                                                             && x.Activo);
        }
        /// <summary>
        /// Enlista todos los tipos de centro de costos
        /// </summary>
        /// <returns></returns>
        public List<TipoCentroCosto> ListaTiposCentrosCostos()
        {
            return uow.Repository<TipoCentroCosto>().GetAll().ToList();
        }
        /// <summary>
        /// Enlista todas las unidades de almacen de gas
        /// </summary>
        /// <returns></returns>
        public List<UnidadAlmacenGas> ListaUnidadesAlmacenGas()
        {
            return uow.Repository<UnidadAlmacenGas>().Get(x => x.Activo).ToList();
        }
        /// <summary>
        /// Enlista todas las estaciones de Carburacion 
        /// </summary>
        /// <returns></returns>
        public List<EstacionCarburacion> ListaEstacionesCarburacion()
        {
            return uow.Repository<EstacionCarburacion>().Get(x => x.Activo ).ToList();
        }
        /// <summary>
        /// Enlista todas las camionetas
        /// </summary>
        /// <returns></returns>
        public List<Camioneta> ListaCamionetas()
        {
            return uow.Repository<Camioneta>().Get(x => x.Activo).ToList();
        }
        /// <summary>
        /// Enlista todas las pipas
        /// </summary>
        /// <returns></returns>
        public List<Pipa> ListaPipas()
        {
            return uow.Repository<Pipa>().Get(x => x.Activo).ToList();
        }
        /// <summary>
        /// Enlista todos las camionestas Cilindro
        /// </summary>
        /// <returns></returns>
        public List<CamionetaCilindro> ListaCilindro()
        {
            return uow.Repository<CamionetaCilindro>().GetAll().ToList();
        }
        /// <summary>
        /// Enlista todos los Almacenes de gas cilindro
        /// </summary>
        /// <returns></returns>
        public List<CAlmacenGasCilindro> ListaAlmacenCilindro()
        {
            return uow.Repository<CAlmacenGasCilindro>().GetAll().ToList();
        }
    }
}
