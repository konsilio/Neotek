using System;
using System.Collections.Generic;
using System.Linq;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.AdaptadoresDTO.Requisicion;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.Servicios.Requisicion
{
    public static class RequisicionServicio
    {
        public static RespuestaRequisicionDto GuardarRequisicionNueva(Sagas.MainModule.Entidades.Requisicion _req)
        {
            var respuesta = new RequisicionDataAccess().InsertarNueva(_req);
            return respuesta;
        }
        public static List<RequisicionDTO> BuscarRequisicionPorIdEmpresa(Int16 _IdEmpresa)
        {
            return RequisicionAdapter.ToDTO(new RequisicionDataAccess().BuscarTodas().Where(x => x.IdEmpresa.Equals(_IdEmpresa)).ToList());
        }
        public static RequisicionRevisionDTO BuscarRequisicion(int _idrequi)
        {
            return RequisicionAdapter.ToRevDTO(new RequisicionDataAccess().BuscarPorIdRequisicion(_idrequi));
        }
        public static Sagas.MainModule.Entidades.Requisicion BuscarRequisicionPorId(int _idrequi)
        {
            return new RequisicionDataAccess().BuscarPorIdRequisicion(_idrequi);
        }
        public static Sagas.MainModule.Entidades.Requisicion Buscar(int _idrequi)
        {
            return new RequisicionDataAccess().BuscarPorIdRequisicion(_idrequi);
        }
        public static RequisicionAutorizacionDTO BuscarRequisicionAuto(int _idequi)
        {
            return RequisicionAdapter.ToAutDTO(new RequisicionDataAccess().BuscarPorIdRequisicion(_idequi));
        }
        public static RespuestaDto UpdateRequisicionRevision(RequisicionRevPutDTO _req)
        {
            var entidad = new RequisicionDataAccess().BuscarPorIdRequisicion(_req.IdRequisicion);
            return new RequisicionDataAccess().Actualizar(RequisicionAdapter.FromDTO(_req, entidad), RequisicionProductoAdapter.FromDTO(_req.ListaProductos, entidad.Productos.ToList()));
        }
        public static RespuestaDto UpDateRequisicionAutoriza(Sagas.MainModule.Entidades.Requisicion _req, List<Sagas.MainModule.Entidades.RequisicionProducto> prods)
        { 
            return new RequisicionDataAccess().Actualizar(_req, prods);
        }
        public static RespuestaDto UpDateRequisicionEstaus(int _req, byte status)
        {
            var req = new RequisicionDataAccess().BuscarPorIdRequisicion(_req);
            var entity = RequisicionAdapter.FromEntity(req);
            entity.IdRequisicionEstatus = status;
            return new RequisicionDataAccess().Actualizar(entity);
        }
        public static RespuestaDto CancelarRequisicion(RequisicionCancelaDTO _req)
        {
            var entidad = new RequisicionDataAccess().BuscarPorIdRequisicion(_req.IdRequisicion);
            return new RequisicionDataAccess().Actualizar(RequisicionAdapter.FromDTO(_req, entidad));
        }
        public static RequisicionProducto BuscarRequisiconProductoPorId(int idProd, int idReq)
        {
            return new RequisicionDataAccess().BuscarProducto(idProd, idReq);
        }
        public static List<Sagas.MainModule.Entidades.Requisicion> IdentificarRequisicones(Sagas.MainModule.Entidades.Requisicion _req)
        {
            List<Sagas.MainModule.Entidades.Requisicion> lRequi = new List<Sagas.MainModule.Entidades.Requisicion>();
            foreach (var prod in _req.Productos)
            {
                if (lRequi.Count.Equals(0))
                {
                    Sagas.MainModule.Entidades.Requisicion newReq = _req;
                    newReq.Productos = new List<RequisicionProducto>();
                    newReq.Productos.Add(prod);
                    lRequi.Add(newReq);
                }
                else 
                {
                    if (prod.Producto.EsGas || prod.Producto.EsTransporteGas.Value)
                    {
                        foreach (var lr in lRequi)
                        {
                            
                        }
                        if (lRequi.Exists(x => x.Productos.ToList().Exists(p => p.EsGas != null ? p.EsGas.Value : false && p.EsTransporteGas != null ? p.EsTransporteGas.Value : false)))
                        {

                        }
                    }
                }
            }
            return lRequi;
        }
    }
}
