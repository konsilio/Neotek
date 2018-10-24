﻿using System;
using System.Collections.Generic;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Requisiciones;
using Application.MainModule.Servicios.Notificacion;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.AdaptadoresDTO.Requisiciones;
using Application.MainModule.Servicios;
using Application.MainModule.Servicios.Compras;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;

namespace Application.MainModule.Flujos
{
    public class Requisiciones
    {
        public RespuestaDto InsertRequisicionNueva(RequisicionDTO _req)
        {
            if (_req.Productos == null || _req.Productos.Count.Equals(0))
                return new RespuestaDto() { Exito= false, MensajesError = new List<string>() { string.Format(Error.R0006, "Productos") } }; 

            var _requisicion = RequisicionAdapter.FromDTO(_req);
            _requisicion = CalcularOrdenCompraServicio.CalcularAlmacenProcutos(_requisicion);
            var ListaRequisiciones = RequisicionServicio.IdentificarRequisicones(_requisicion);
            ListaRequisiciones = FolioServicio.GenerarNumeroRequisicion(ListaRequisiciones);
            RespuestaDto resp = new RespuestaDto();
            foreach (var item in ListaRequisiciones)
            {
                var respuesta = RequisicionServicio.GuardarRequisicionNueva(item);
                if (respuesta.Exito)
                {
                    resp.Id = respuesta.Id;
                    resp.Mensaje = string.IsNullOrEmpty(resp.Mensaje) ? respuesta.Mensaje : string.Concat(resp.Mensaje, " ,", respuesta.Mensaje);
                    resp.Exito = true;
                    resp.EsInsercion = true;          
                        NotificarServicio.RequisicionNueva(RequisicionServicio.Buscar(resp.Id));
                }
                else
                {
                    resp.MensajesError = new List<string>();
                    resp.MensajesError.AddRange(respuesta.MensajesError != null ? respuesta.MensajesError : new List<string>());
                }
            }
            return resp;     
        }
        public List<RequisicionDTO> BuscarRequisicionesPorEmpresa(short idEmpresa)
        {
            return RequisicionServicio.BuscarRequisicionPorIdEmpresa(idEmpresa);
        }
        public RequisicionRevisionDTO BuscarRequisicion(int idRequisicion)
        {
            return RequisicionServicio.BuscarRequisicion(idRequisicion);
        }
        public RequisicionAutorizacionDTO BuscarRequisicionAuto(int numRequisicon)
        {
            return RequisicionServicio.BuscarRequisicionAuto(numRequisicon);
        }
        public RespuestaDto ActualizarRequisicionRevision(RequisicionRevPutDTO _req)
        {
            return RequisicionServicio.UpdateRequisicionRevision(_req);
        }
        public RespuestaDto ActualizarRequisicionAutorizacion(RequisicionAutPutDTO _req)
        {
            var ReqAnterior = new RequisicionDataAccess().BuscarPorIdRequisicion(_req.IdRequisicion);
            var newReq = RequisicionAdapter.FromEntity(ReqAnterior);
            newReq.IdUsuarioAutorizacion = _req.IdUsuarioAutorizacion;
            newReq.FechaAutorizacion = _req.FechaAutorizacion;
            newReq.IdRequisicionEstatus = _req.IdRequisicionEstatus;

            var ReqProd = RequisicionProductoAdapter.FromDTO(_req.ListaProductos);
            var prodEdit = RequisicionProductoAdapter.FromEntity(new RequisicionDataAccess().BuscarProductoRequisicion(_req.IdRequisicion));
            foreach (var item in prodEdit)
            {
                foreach (var prod in ReqProd)
                {
                    if (item.IdProducto.Equals(prod.IdProducto))
                    {
                        item.AutorizaCompra = prod.AutorizaCompra;
                        item.AutorizaEntrega = prod.AutorizaEntrega;
                        if (prod.AutorizaEntrega.Value)
                        {
                            //Notificar
                        }
                        item.CantidadAComprar = prod.CantidadAComprar;
                    }
                }
            }
            return RequisicionServicio.UpDateRequisicionAutoriza(newReq, prodEdit);
        }        
        public RespuestaDto CancelarRequisicion(RequisicionCancelaDTO _req)
        {
            var entidad = new RequisicionDataAccess().BuscarPorIdRequisicion(_req.IdRequisicion);
            var entity = RequisicionAdapter.FromEntity(entidad);
            entity.IdRequisicionEstatus = RequisicionEstatusEnum.Cerrada;
            entity.MotivoCancelacion = _req.MotivoCancelacion;
            var respuesta = RequisicionServicio.CancelarRequisicion(entity);
            if (respuesta.Exito)
                respuesta.Mensaje = String.Format(Exito.OKCancelacion, "Requisicion", entity.NumeroRequisicion);
            return respuesta;
        }        
        public List<RequisicionEstatusDTO> ListaEstatus()
        {
            return RequisicionAdapter.ToDTO(RequisicionServicio.RequisiconEstatus());
        }
    }
}
