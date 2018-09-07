﻿using System;
using System.Collections.Generic;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Requisicion;
using Application.MainModule.Servicios.Notificacion;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.AdaptadoresDTO.Requisicion;
using Application.MainModule.Servicios;

namespace Application.MainModule.Flujos
{
    public class Requisicion
    {
        public RespuestaDto InsertRequisicionNueva(RequisicionEDTO _req)
        {
            var _requisicion = RequisicionAdapter.FromEDTO(_req);
            _requisicion = Servicios.Almacen.ProductoAlmacenServicio.CalcularAlmacenProcutos(_requisicion);
            var ListaRequisiciones = RequisicionServicio.IdentificarRequisicones(_requisicion);
            ListaRequisiciones = FolioServicio.GenerarNumeroRequisicion(ListaRequisiciones);
            RespuestaDto resp = new RespuestaDto();
            foreach (var item in ListaRequisiciones)
            {
                var respuesta = RequisicionServicio.GuardarRequisicionNueva(item);
                if (respuesta.Exito)
                {
                    resp.Id = respuesta.Id;
                    resp.Mensaje = string.Concat(resp.Mensaje, " ,", respuesta.Mensaje);
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
            return RequisicionServicio.CancelarRequisicion(_req);
        }
        public List<RequisicionEstatusDTO> ListaEstatus()
        {
            return RequisicionAdapter.ToDTO(RequisicionServicio.RequisiconEstatus());
        }
    }
}
