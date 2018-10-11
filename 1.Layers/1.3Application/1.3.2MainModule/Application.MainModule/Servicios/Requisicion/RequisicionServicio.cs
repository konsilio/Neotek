using System;
using System.Collections.Generic;
using System.Linq;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.AdaptadoresDTO.Requisicion;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.DTOs.Compras;

namespace Application.MainModule.Servicios.Requisicion
{
    public static class RequisicionServicio
    {
        public static RespuestaDto GuardarRequisicionNueva(Sagas.MainModule.Entidades.Requisicion _req)
        {
            return new RequisicionDataAccess().InsertarNueva(_req);
        }
        public static List<RequisicionDTO> BuscarRequisicionPorIdEmpresa(Int16 _IdEmpresa)
        {
            return RequisicionAdapter.ToDTO(new RequisicionDataAccess().BuscarTodas().Where(x => x.IdEmpresa.Equals(_IdEmpresa) && !x.Solicitante.EsAdministracionCentral).ToList());
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
            var productos = _req.Productos.ToList();
            var productosGas = _req.Productos.Where(x => x.EsGas || x.EsTransporteGas).ToList();
            if (productosGas != null)
            {
                if (!productosGas.Count.Equals(0))
                {
                    var reqGas = RequisicionAdapter.FromEntity(_req, productosGas);
                    reqGas.IdRequisicionEstatus = RequisicionEstatusEnum.Revision_exitosa;
                    lRequi.Add(reqGas);     
                }
            }
            productos = productos.Where(x => !x.EsGas && !x.EsTransporteGas).ToList();
            if (productos != null)
            {
                if (!productos.Count.Equals(0))
                {
                    var req = RequisicionAdapter.FromEntity(_req, productos);
                    req.IdRequisicionEstatus = RequisicionEstatusEnum.Creada;
                    lRequi.Add(req);
                }
            }            
            return lRequi;
        }
        public static List<RequisicionEstatus> RequisiconEstatus()
        {
            return new RequisicionDataAccess().Estatus();
        }
        public static RequisicionOCDTO DeterminarTipoRequisicion(RequisicionOCDTO req)
        {
            req.EsGasTransporte = req.ProductosOC.Where(x => x.EsGas).ToList().Count().Equals(0) ? false : true;
            return req;
        }
    }
}
