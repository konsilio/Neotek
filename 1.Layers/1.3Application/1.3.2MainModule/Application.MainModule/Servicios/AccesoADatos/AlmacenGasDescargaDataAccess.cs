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
    public class AlmacenGasDescargaDataAccess
    {
        private SagasDataUow uow;

        public AlmacenGasDescargaDataAccess()
        {
            uow = new SagasDataUow();
        }

        public RespuestaDto Insertar(AlmacenGasDescarga _AlmDes)
        {            
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<AlmacenGasDescarga>().Insert(_AlmDes);
                    uow.SaveChanges();
                    _respuesta.Id = _AlmDes.IdAlmacenEntradaGasDescarga;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "de la entrada de gas");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(AlmacenGasDescarga _almDes, List<AlmacenGasDescargaFoto> _fotos)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {                    
                    uow.Repository<AlmacenGasDescarga>().Update(_almDes);
                    foreach (var foto in _fotos)
                        uow.Repository<AlmacenGasDescargaFoto>().Insert(foto);
                    uow.SaveChanges();
                    _respuesta.Id = _almDes.IdAlmacenEntradaGasDescarga;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la descargar de gas"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public AlmacenGasDescarga Buscar(int idAlmacenEntradaGasDescarga)
        {
            return uow.Repository<AlmacenGasDescarga>().GetSingle(x => x.IdAlmacenEntradaGasDescarga.Equals(idAlmacenEntradaGasDescarga));
        }

        public AlmacenGasDescarga BuscarOCompraExpedidor(int idOCompra)
        {
            return uow.Repository<AlmacenGasDescarga>().GetSingle(x => x.IdOrdenCompraExpedidor.Equals(idOCompra));
        }

        public AlmacenGasDescarga BuscarClaveOperacion(string claveOperacion)
        {
            return uow.Repository<AlmacenGasDescarga>().GetSingle(x => x.ClaveOperacion.Equals(claveOperacion));
        }
    }
}