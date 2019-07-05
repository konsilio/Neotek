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
                    //foreach (var oc in ocs)                    
                    //    uow.Repository<OrdenCompra>().Update(oc);                    
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
        public RespuestaDto Actualizar(AlmacenGasDescarga _almDes, List<AlmacenGasDescargaFoto> _fotos, List<OrdenCompra> ocs)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    foreach (var oc in ocs)
                        uow.Repository<OrdenCompra>().Update(oc);

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
        public void Actualizar(AplicaDescargaDto aplicacionDto)
        {
            using (uow)
            {
                try
                {
                    if (aplicacionDto.unidadEntrada != null)
                        uow.Repository<UnidadAlmacenGas>().Update(aplicacionDto.unidadEntrada);

                    if (aplicacionDto.unidadSalida != null)
                        uow.Repository<UnidadAlmacenGas>().Update(aplicacionDto.unidadSalida);

                    if (aplicacionDto.AlmacenGas != null)
                        uow.Repository<AlmacenGas>().Update(aplicacionDto.AlmacenGas);

                    if (aplicacionDto.OCExpedidor != null)
                        uow.Repository<OrdenCompra>().Update(aplicacionDto.OCExpedidor);

                    if (aplicacionDto.OCPorteador != null)
                        uow.Repository<OrdenCompra>().Update(aplicacionDto.OCPorteador);

                    if (aplicacionDto.DescargaSinNavigationProperties != null)
                        uow.Repository<AlmacenGasDescarga>().Update(aplicacionDto.DescargaSinNavigationProperties);

                    if (aplicacionDto.DescargaFotos != null && aplicacionDto.DescargaFotos.Count > 0)
                        aplicacionDto.DescargaFotos.ToList().ForEach(x =>
                            uow.Repository<AlmacenGasDescargaFoto>().Update(x)
                        );

                    if (aplicacionDto.Movimiento != null)
                        uow.Repository<AlmacenGasMovimiento>().Insert(aplicacionDto.Movimiento);

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

        public RespuestaDto Insertar(AlmacenGasMovimiento Movimiento)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<AlmacenGasMovimiento>().Insert(Movimiento);

                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la insercion del gas movimiento"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
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
        public AlmacenGasDescarga BuscarAlmacenDescargaPorRequisicion(int idReq)
        {
            return uow.Repository<AlmacenGasDescarga>().GetSingle(x => x.IdRequisicion.Equals(idReq));
        }
        public AlmacenGasDescarga BuscarClaveOperacion(string claveOperacion)
        {
            return uow.Repository<AlmacenGasDescarga>().GetSingle(x => x.ClaveOperacion.Equals(claveOperacion));
        }
        public List<AlmacenGasDescargaFoto> BuscarImagenes(int idAlmacenEntradaGasDescarga)
        {
            return uow.Repository<AlmacenGasDescargaFoto>().Get(x => x.IdAlmacenEntradaGasDescarga.Equals(idAlmacenEntradaGasDescarga)).ToList();
        }
        public List<AlmacenGasDescargaFoto> BuscarImagenesSinVigencia(DateTime fechaVigencia)
        {
            //Agregar FechaRegistro en las imagenes
            //return uow.Repository<AlmacenGasDescargaFoto>().Get(x => x.FechaRegistro < fechaVigencia).ToList();
            return new List<AlmacenGasDescargaFoto>();
        }
    }
}