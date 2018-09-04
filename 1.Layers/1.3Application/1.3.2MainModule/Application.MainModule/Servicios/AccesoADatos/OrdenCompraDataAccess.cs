﻿using Application.MainModule.DTOs.Compras;
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
    public class OrdenCompraDataAccess
    {
        private SagasDataUow uow;

        public OrdenCompraDataAccess()
        {
            uow = new SagasDataUow();
        }
        public int BuscarUltimaOC()
        {
            if (uow.Repository<OrdenCompra>().GetAll().ToList().Count.Equals(0))
                return 0;
            else
                return uow.Repository<OrdenCompra>().GetAll().ToList().Last().IdOrdenCompra;
        }
        public List<OrdenCompra> Buscar(short idEmpresa, byte idOrdenComprEstatus, bool EsGas, bool EsActivoVenta, bool EsTransporteGas)
        {
            return uow.Repository<OrdenCompra>().Get(x => x.Activo
                                                                  && x.EsGas.Equals(EsGas)
                                                                  && x.EsActivoVenta.Equals(EsActivoVenta)
                                                                  && x.EsTransporteGas.Equals(EsTransporteGas)
                                                                  && x.IdEmpresa.Equals(idEmpresa)
                                                                  && x.IdOrdenCompraEstatus.Equals(idOrdenComprEstatus)).ToList();
            //return uow.Repository<OrdenCompra>().GetAll().ToList();
        }
        public OrdenCompra Buscar(int idOrdenCompr)
        {
            return uow.Repository<OrdenCompra>().GetSingle(x => x.IdOrdenCompra.Equals(idOrdenCompr));
            //return uow.Repository<OrdenCompra>().GetAll().ToList();
        }
        public List<OrdenCompra> BuscarTodos()
        {
            return uow.Repository<OrdenCompra>().GetAll().ToList();
        }
        public OrdenCompraRespuestaDTO InsertarNuevo(OrdenCompra oc)
        {
            OrdenCompraRespuestaDTO _respuesta = new OrdenCompraRespuestaDTO();
            using (uow)
            {
                try
                {
                    uow.Repository<OrdenCompra>().Insert(oc);
                    uow.SaveChanges();
                    _respuesta.IdOrdenCompra = oc.IdOrdenCompra;
                    _respuesta.NumOrdenCompra = oc.NumOrdenCompra;
                    _respuesta.Mensaje = Exito.OK;
                    _respuesta.Exito = true;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje= ex.Message;                           
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(OrdenCompra oc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<OrdenCompra>().Update(oc);
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
        public List<OrdenCompraEstatus> Estatus()
        {
            return uow.Repository<OrdenCompraEstatus>().Get(x => x.Activo).ToList();
        }
    }
}
