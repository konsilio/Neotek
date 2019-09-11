using Application.MainModule.AdaptadoresDTO.Requisiciones;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.MainModule.Servicios.Requisiciones
{
    public static class RequisicionServicio
    {
        public static RespuestaDto GuardarRequisicionNueva(Requisicion _req)
        {
            return new RequisicionDataAccess().InsertarNueva(_req);
        }
        public static List<RequisicionDTO> BuscarRequisicionPorIdEmpresa(short _IdEmpresa)
        {
            return RequisicionAdapter.ToDTO(new RequisicionDataAccess().BuscarTodas(_IdEmpresa));
        }

        public static List<RequisicionDTO> BuscarRequisicionAlmacenPorIdEmpresa(short _IdEmpresa)
        {
            return RequisicionAdapter.ToDTO(new RequisicionDataAccess().BuscarTodasAlmacenPorIdEmpresa(_IdEmpresa));
        }
        

        public static List<RequisicionDTO> BuscarRequisicionPorPeriodo(short _IdEmpresa, DateTime periodo)
        {
            return RequisicionAdapter.ToDTO(new RequisicionDataAccess().BuscarTodas(_IdEmpresa, periodo));
        }
        public static List<Requisicion> BuscarRequisicionPorPeriodo(short _IdEmpresa, DateTime fi, DateTime ff)
        {
            return new RequisicionDataAccess().BuscarTodas(_IdEmpresa, fi, ff);
        }
        public static RequisicionRevisionDTO BuscarRequisicion(int _idrequi)
        {
            return RequisicionAdapter.ToRevDTO(new RequisicionDataAccess().BuscarPorIdRequisicion(_idrequi));
        }
        public static Requisicion BuscarRequisicionPorId(int _idrequi)
        {
            return new RequisicionDataAccess().BuscarPorIdRequisicion(_idrequi);
        }
        public static Requisicion Buscar(int _idrequi)
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
        public static RespuestaDto UpDateRequisicionAutoriza(Requisicion _req, List<RequisicionProducto> prods)
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
        public static RespuestaDto CancelarRequisicion(Requisicion _req)
        {
            return new RequisicionDataAccess().Actualizar(_req);
        }
        public static RequisicionProducto BuscarRequisiconProductoPorId(int idProd, int idReq)
        {
            return new RequisicionDataAccess().BuscarProducto(idProd, idReq);
        }
        public static List<Requisicion> IdentificarRequisicones(Requisicion _req)
        {
            List<Requisicion> lRequi = new List<Requisicion>();
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
        public static Requisicion DeterminaEstatusPorSalidas(Requisicion _requisicion, List<RequisicionProducto> _productos)
        {
            if (_productos.Where(x => x.CantidadEntregada.Equals(x.Cantidad)).Count().Equals(_productos.Count))
                _requisicion.IdRequisicionEstatus = RequisicionEstatusEnum.Cerrada;
            else
                _requisicion.IdRequisicionEstatus = RequisicionEstatusEnum.Solicitante_Notificado;
            return _requisicion;
        }
        public static Requisicion DeterminaEstatusPorAutorizacion(Requisicion _requisicion, List<RequisicionProducto> _productos)
        {
            _requisicion.IdRequisicionEstatus = RequisicionEstatusEnum.Autorizacion_parcial;
            if (_productos.Where(x => x.AutorizaEntrega.Equals(true)).Count().Equals(_productos.Count))
                _requisicion.IdRequisicionEstatus = RequisicionEstatusEnum.Autoriza_entrega;
            if (_productos.Where(x => x.AutorizaCompra.Equals(true)).Count().Equals(_productos.Count))
                _requisicion.IdRequisicionEstatus = RequisicionEstatusEnum.Autoriza_compra;
            return _requisicion;
        }
        public static string ListaProductos(List<RequisicionProducto> Lista)
        {
            string respuesta = string.Empty;
            foreach (var item in Lista)
            {
                if (respuesta.Equals(string.Empty))
                    respuesta = item.Producto.Descripcion;
                else
                    respuesta += string.Concat(", ", item.Producto.Descripcion);
            }
            return respuesta;
        }
        public static List<RepRequisicionDTO> ConvertirReporte(List<Requisicion> lista)
        {
            List<RepRequisicionDTO> respuesta = new List<RepRequisicionDTO>();
            foreach (var req in lista)
            {
                if (req.Productos.Count != 0)
                {
                    var porCentroCosto = req.Productos.Select(x => x.IdCentroCosto).Distinct().ToList();
                    foreach (var idcc in porCentroCosto)
                    {
                        respuesta.Add(RequisicionAdapter.ToRepDTO(req, req.Productos.Where(x => x.IdCentroCosto.Equals(idcc)).ToList()));
                    }   
                }
            }
            return respuesta;
        }
    }
}
