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
using Application.MainModule.DTOs.Almacen;

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

        public void Actualizar(List<AplicaDescargaDto> aplicacionesDto)
        {
            if(aplicacionesDto.Count > 0)
                using (uow)
                {
                    try
                    {
                        foreach (var desDto in aplicacionesDto)
                        {
                            if(desDto.unidadEntrada != null)
                                uow.Repository<UnidadAlmacenGas>().Update(desDto.unidadEntrada);

                            if (desDto.unidadSalida != null)
                                uow.Repository<UnidadAlmacenGas>().Update(desDto.unidadSalida);

                            if (desDto.AlmacenGas != null)
                                uow.Repository<AlmacenGas>().Update(desDto.AlmacenGas);

                            if (desDto.OCExpedidor != null)
                                uow.Repository<OrdenCompra>().Update(desDto.OCExpedidor);

                            if (desDto.OCPorteador != null)
                                uow.RepositoryWithoutValidation<OrdenCompra>().Update(desDto.OCPorteador);

                            // Agregar al modelo de dominio AlmacenGasMovimiento
                            //if (desDto.AGMovimiento != null)
                            //    uow.Repository<AlmacenGasMovimiento>().Insert(desDto.MovInventario);

                            if (desDto.DescargaSinNavigationProperties != null)
                                uow.Repository<AlmacenGasDescarga>().Update(desDto.DescargaSinNavigationProperties);

                            if (desDto.DescargaFotos != null && desDto.DescargaFotos.Count > 0)
                                desDto.DescargaFotos.ToList().ForEach(x =>
                                    uow.Repository<AlmacenGasDescargaFoto>().Update(x)
                                );
                        }
                        uow.SaveChanges();
                        //_respuesta.Id = _almDes.IdAlmacenEntradaGasDescarga;
                        //_respuesta.Exito = true;
                        //_respuesta.EsActulizacion = true;
                        //_respuesta.ModeloValido = true;
                        //_respuesta.Mensaje = Exito.OK;
                    }
                    catch (Exception ex)
                    {
                        //_respuesta.Exito = false;
                        //_respuesta.Mensaje = string.Format(Error.C0003, "de la descargar de gas"); ;
                        //_respuesta.MensajesError = CatchInnerException.Obtener(ex);
                    }
                }
            //return _respuesta;
        }

        public List<AlmacenGasDescarga> BuscarTodas()
        {
            return uow.Repository<AlmacenGasDescarga>().GetAll().ToList();
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

        public List<AlmacenGasDescargaFoto> BuscarImagenes(int idAlmacenEntradaGasDescarga)
        {
            return uow.Repository<AlmacenGasDescargaFoto>().Get(x => x.IdAlmacenEntradaGasDescarga.Equals(idAlmacenEntradaGasDescarga)).ToList();
        }
    }
}