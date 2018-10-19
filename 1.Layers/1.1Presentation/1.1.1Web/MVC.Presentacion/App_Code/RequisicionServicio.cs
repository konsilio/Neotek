using MVC.Presentacion.Models.Requisicion;
using MVC.Presentacion.Agente;
using System;
using System.Collections.Generic;
using System.Linq;
using MVC.Presentacion.Models.Seguridad;
using Utilities.MainModule;
using Exceptions.MainModule.Validaciones;

namespace MVC.Presentacion.App_Code
{
    public static class RequisicionServicio
    {
        public static List<RequisicionDTO> BuscarRequisiciones(short idEmpresa, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarRequisiciones(idEmpresa, token);
            return respuestaReq._listaRequisicion;
        }
        public static List<RequisicionDTO> BuscarRequisicionesEntrega(short idEmpresa, string token)
        {
           return BuscarRequisiciones(idEmpresa, token)
                .Where(x => x.RequisicionEstatus.Equals(RequisicionEstatusEnum.Autoriza_entrega)
                    || x.IdRequisicionEstatus.Equals(RequisicionEstatusEnum.Autorizacion_finalizada)).ToList();
        }
        public static List<RequisicionEstatusDTO> BuscarRequisicionEstatus(string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarRequisicionEstatus(token);
            return respuestaReq._listaRequisicionEstatus;
        }
        public static RequisicionesModel InitRequisiciones(string _tok)
        {
            return new RequisicionesModel
            {
                Estatus = BuscarRequisicionEstatus(_tok),
                Requisiciones = BuscarRequisiciones(TokenServicio.ObtenerIdEmpresa(_tok), _tok).OrderByDescending(x => x.IdRequisicion).ToList(),
                Empresas = CatalogoServicio.Empresas(_tok)
            };
        }
        public static RequisicionesModel FiltrarRequisicones(RequisicionesModel model)
        {
            List<RequisicionDTO> newList = model.Requisiciones;
            #region Por estatus
            if (model.IdEstatus != 0)
                newList = newList.Where(x => x.IdRequisicionEstatus.ToString().Equals(model.IdEstatus)).ToList();
            #endregion

            #region Por Fecha de registro           
            if (model.FechaCreacionDe != null)
                newList = newList.Where(x => x.FechaRegistro >= Convert.ToDateTime(model.FechaCreacionDe)).ToList();
            if (model.FechaCreacionDe != null)
                newList = newList.Where(x => x.FechaRegistro <= Convert.ToDateTime(model.FechaCreacionA)).ToList();
            #endregion

            #region Por Fecha de sequisicion
            if (model.FechaRequeridaDe != null)
                newList = newList.Where(x => x.FechaRequerida >= Convert.ToDateTime(model.FechaRequeridaDe)).ToList();
            if (model.FechaRequeridaA != null)
                newList = newList.Where(x => x.FechaRequerida <= Convert.ToDateTime(model.FechaRequeridaA)).ToList();
            #endregion

            if (newList.Count.Equals(0))
                model.Requisiciones = newList;

            return model;
        }
        public static RequisicionDTO InitRequisicion(string _tkn)
        {
            return new RequisicionDTO()
            {
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(_tkn),
                IdUsuarioSolicitante = TokenServicio.ObtenerIdUsuario(_tkn),
                FechaRequerida = Convert.ToDateTime(DateTime.Today.ToShortDateString()),                               
                Productos = new List<RequisicionProductoDTO>(),
            };
        }
        public static RequisicionDTO AgregarProducto(RequisicionDTO model, string _tkn)
        {
            var lProductos = CatalogoServicio.ListaProductos(_tkn);            
            if (model.Productos == null)
                model.Productos = new List<RequisicionProductoDTO>();
            if (ValidarProductoRepetido(model))
                foreach (var item in model.Productos.Where((x => x.IdProducto.Equals(model.IdProducto)
                                       && x.IdCentroCosto.Equals(model.IdCentroCosto))))
                {                   
                    item.Cantidad = item.Cantidad + model.Cantidad;
                    item.Aplicacion = item.Aplicacion + "|" + model.Aplicacion;
                }
            else
                model.Productos.Add(CrearProductoNuevo(model, _tkn));
            model.IdProducto = 0;
            model.IdCentroCosto = 0;
            model.IdTipoProducto = 0;
            model.Cantidad = 0;
            model.Aplicacion = string.Empty;
            return model;
        }
        public static RequisicionDTO ActivarEditar(RequisicionDTO dto, int id, List<RequisicionProductoDTO> Prodcutos, string _tkn)
        {
            var newModel = dto;
            newModel.FechaRequerida = dto.FechaRequerida;
            newModel.Productos = Prodcutos;
            var prod = Prodcutos.SingleOrDefault(x => x.IdProducto.Equals(id));
            newModel.IdProducto = prod.IdProducto;
            newModel.IdTipoProducto = prod.IdTipoProducto;
            newModel.IdCentroCosto = prod.IdCentroCosto;
            newModel.Aplicacion = prod.Aplicacion;
            newModel.Unidad = prod.Unidad;
            newModel.Cantidad = prod.Cantidad;
            return newModel;
        }
        public static RequisicionDTO ActivarBorrar(RequisicionDTO model, int id, List<RequisicionProductoDTO> Prodcutos, string _tkn)
        {
            var newModel = model;
            newModel.FechaRequerida = model.FechaRequerida;
            newModel.Productos = Prodcutos.Where(x => !x.IdProducto.Equals(id)).ToList();
            //newModel.CentrosCostos = CatalogoServicio.BuscarCentrosCosto(_tkn);
            //newModel.Productos = CatalogoServicio.ListaProductos(_tkn);
            return newModel;
        }
        public static RequisicionProductoDTO CrearProductoNuevo(RequisicionDTO model, string _tkn)
        {
            var prod = CatalogoServicio.ListaProductos(_tkn).FirstOrDefault(x => x.IdProducto.Equals(model.IdProducto));
            var cc = CatalogoServicio.BuscarCentrosCosto(_tkn).FirstOrDefault(x => x.IdCentroCosto.Equals(model.IdCentroCosto));
            var orden = model.Productos.Where(x => x.IdProducto.Equals(model.IdProducto)).ToList().Count + 1;
            return new RequisicionProductoDTO()
            {
                IdTipoProducto = model.IdTipoProducto,
                TipoProducto = model.IdTipoProducto.Equals(2) ? "Producto" : "Servicio",
                Orden = short.Parse(orden.ToString()),
                IdProducto = model.IdProducto,
                Producto = prod.Descripcion,
                Cantidad = model.Cantidad,
                IdUnidad = prod.IdUnidadMedida,
                Unidad = prod.UnidadMedida,
                IdCentroCosto = model.IdCentroCosto,
                CentroCosto = cc.Descripcion,
                Aplicacion = model.Aplicacion
            };
        }
        private static bool ValidarProductoRepetido(RequisicionDTO model)
        {
            bool resp = false;
            if (!model.Productos.Equals(0))
                resp = (model.Productos.Exists(x => x.IdProducto.Equals(model.IdProducto) && x.IdCentroCosto.Equals(model.IdCentroCosto)));
            return resp;
        }
        public static RespuestaDTO GuardarRequisicion(RequisicionDTO dto, string tkn)
        {
            dto.FechaRegistro = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            var respuestaReq = new AgenteServicio();
            respuestaReq.GuardarRequisicon(dto, tkn);
            return respuestaReq._RespuestaDTO;
        }      
        #region Revicion Requisicion
        public static RespuestaDTO FinalizarRevision(RequisicionRevisionModel model, string _tok)
        {
            List<RequisicionProdReviPutDTO> lProd = new List<RequisicionProdReviPutDTO>();
            var valid = ValidarRevisionAlmacen(model, out lProd);
            if (valid.Exito)
            {
                RequisicionRevPutDTO dto = RequisicionRevisionDTO(model, _tok, lProd);
                RespuestaDTO resp = new RespuestaDTO();
                return ActualizarRequisicionRevision(dto, _tok);
            }
            else
            {
                return valid;
            }
        }
        private static RequisicionRevPutDTO RequisicionRevisionDTO(RequisicionRevisionModel model, string _tok, List<RequisicionProdReviPutDTO> lprods)
        {
            var requRevision = new RequisicionRevPutDTO()
            {
                IdRequisicion = model.IdRequisicion,
                NumeroRequisicion = model.NumeroRequisicion,
                OpinionAlmacen = model.OpinionAlmacen,
                FechaRevision = DateTime.Today,
                ListaProductos = lprods,
                IdRequisicionEstatus = RequisicionEstatusEnum.Revision_exitosa
            };
            if (TokenServicio.ObtenerEsAdministracionCentral(_tok))
                requRevision.IdUsuarioRevision = model.IdUsuarioSolicitante;
            else
                requRevision.IdUsuarioRevision = TokenServicio.ObtenerIdUsuario(_tok);
            return requRevision;
        }
        public static RespuestaDTO ActualizarRequisicionRevision(RequisicionRevPutDTO Req, string tkn)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.ActualizarRequisicionRevision(Req, tkn);
            return respuestaReq._RespuestaDTO;
        }
        private static RespuestaDTO ValidarRevisionAlmacen(RequisicionRevisionModel dto, out List<RequisicionProdReviPutDTO> lProd)
        {
            RespuestaDTO resp = new RespuestaDTO();
            resp.Exito = true;
            resp.MensajesError = new List<string>();
            List<RequisicionProdReviPutDTO> LProdPutDTO = new List<RequisicionProdReviPutDTO>();
            if (!string.IsNullOrEmpty(dto.OpinionAlmacen))
            {
                foreach (var _row in dto.Productos)
                {
                    if (!_row.RevisionFisica)
                    {
                        resp.MensajesError.Add(Error.R0013);
                        resp.Exito = false;
                        break;
                    }
                    else
                        LProdPutDTO.Add(new RequisicionProdReviPutDTO
                        {
                            IdProducto = _row.IdProducto,
                            Orden = _row.Orden,
                            RevisionFisica = true,
                        });
                }
            }
            else
            {
                resp.Exito = false;
                resp.MensajesError.Add(Error.R0014);
            }
            lProd = LProdPutDTO;
            return resp;
        }
        #endregion
        #region Autorizar Requisicion
        public static RequisicionAutorizacionModel RequisicionAutorizacion(int idReq, byte estatus, string tkn)
        {
            var _reqRev = BuscarRequisicionByIdRequiAuto(idReq, tkn);
            return new RequisicionAutorizacionModel()
            {
                IdEmpresa = _reqRev.IdEmpresa,
                IdRequisicion = _reqRev.IdRequisicion,
                NumeroRequisicion = _reqRev.NumeroRequisicion,
                MotivoRequisicion = _reqRev.MotivoRequisicion,
                RequeridoEn = _reqRev.RequeridoEn,
                FechaRequerida = _reqRev.FechaRequerida,
                IdUsuarioSolicitante = _reqRev.IdUsuarioSolicitante,
                RequisicionEstatus = _reqRev.IdRequisicionEstatus,
                Productos = _reqRev.ListaProductos,
                OpinionAlmacen = _reqRev.OpinionAlmacen
            };
        }
        public static RespuestaDTO FinalizarAutorizacion(RequisicionAutorizacionModel model, string _tok)
        {
            if (ValidarAutorizacion(model.Productos))
            {
                var _resp = ActualizarRequisicionAutorizacion(CrearAut(model), _tok);
                if (_resp.Exito)                
                    return new RespuestaDTO { Exito = true, Id = _resp.Id };                
                else                
                    return new RespuestaDTO { Exito = false, Mensaje = _resp.Mensaje };              
            }
            else            
                return new RespuestaDTO { Exito = false, Mensaje = Error.R0012 };
            
        }
        private static List<RequisicionProdAutPutDTO> GenerarAutorizados(RequisicionAutorizacionModel model)
        {            
            List<RequisicionProdAutPutDTO> lProd = new List<RequisicionProdAutPutDTO>();
            foreach (var _row in model.Productos)
            {
                lProd.Add(new RequisicionProdAutPutDTO
                {
                    IdProducto = _row.IdProducto,
                    AutorizaCompra = _row.AutorizaCompra,
                    AutorizaEntrega = _row.AutorizaEntrega,
                    CantidadAComprar = _row.CantidadAComprar,
                    CantidadRequerida = _row.Cantidad
                });
            }
            return lProd;
        }
        private static bool ValidarAutorizacion(List<RequisicionProductoAutorizacionDTO> list)
        {
            bool correcto = true;
            foreach (var _row in list)
            {
                if (_row.AutorizaCompra)
                    if (_row.CantidadAComprar > _row.CantidadAComprar)
                        correcto = false;
            }
            return correcto;
        }
        private static RequisicionAutPutDTO CrearAut(RequisicionAutorizacionModel model)
        {
            RequisicionAutPutDTO _aut = new RequisicionAutPutDTO();
            _aut.IdRequisicion = model.IdRequisicion;
            _aut.NumeroRequisicion = model.NumeroRequisicion;
            _aut.FechaAutorizacion = DateTime.Today;
            _aut.IdUsuarioAutorizacion = model.IdUsuarioSolicitante;
            _aut.IdRequisicionEstatus = RequisicionEstatusEnum.Autorizacion_finalizada;
            _aut.ListaProductos = GenerarAutorizados(model);
            return _aut;
        }
        private static RespuestaDTO ActualizarRequisicionAutorizacion(RequisicionAutPutDTO Req, string tkn)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.ActualizarRequisicionAutorizacion(Req, tkn);
            return respuestaReq._RespuestaDTO;
        }
        public static RequisicionAutorizacionDTO BuscarRequisicionByNumRequiAuto(int numreq, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarRequisicioAuto(numreq, token);
            return respuestaReq._requsicionAutorizacion;
        }
        #endregion
        public static RequisicionRevisionModel RequisicionRevision(int idReq, byte estatus, string tkn)
        {
            var _reqRev = BuscarRequisicionByIdRequiRevi(idReq, tkn);
            return new RequisicionRevisionModel()
            {
                IdEmpresa = _reqRev.IdEmpresa,
                IdRequisicion = _reqRev.IdRequisicion,
                NumeroRequisicion = _reqRev.NumeroRequisicion,
                MotivoRequisicion = _reqRev.MotivoRequisicion,
                RequeridoEn = _reqRev.RequeridoEn,
                FechaRequerida = _reqRev.FechaRequerida,
                IdUsuarioSolicitante = _reqRev.IdUsuarioSolicitante,
                RequisicionEstatus = _reqRev.IdRequisicionEstatus,
                Productos = _reqRev.ListaProductos,
                OpinionAlmacen = string.Empty
            };
        }
        public static RequisicionModel RquisicionAlternativa(int idReq, byte estatus, string tkn)
        {
            RequisicionModel newModel = new RequisicionModel();
            if (estatus.Equals(RequisicionEstatusEnum.En_revision))
            {
                var _reqAuto = BuscarRequisicionByIdRequiAuto(idReq, tkn);
                newModel = new RequisicionModel()
                {
                    RequisicionAutorizacion = _reqAuto,
                    NumeroRequisicion = _reqAuto.NumeroRequisicion,
                    MotivoRequisicion = _reqAuto.MotivoRequisicion,
                    RequeridoEn = _reqAuto.RequeridoEn,
                    FechaRequerida = _reqAuto.FechaRequerida,
                    IdUsuarioSolicitante = _reqAuto.IdUsuarioSolicitante,
                    RequisicionEstatus = _reqAuto.IdRequisicionEstatus
                };
            }
            return newModel;
        }
        private static RequisicionRevisionDTO BuscarRequisicionByIdRequiRevi(int IdRequisicion, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.RequisicionRevision(IdRequisicion, token);
            return respuestaReq._requisicionRevisionDTO;
        }
        private static RequisicionAutorizacionDTO BuscarRequisicionByIdRequiAuto(int IdRequisicion, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarRequisicioAuto(IdRequisicion, token);
            return respuestaReq._requsicionAutorizacion;
        }
        #region Adaptadores
        public static List<RequisicionProductoNuevoDTO> ToModel(List<RequisicionProductoRevisionDTO> modelPord)
        {
            return modelPord.Select(x => ToModel(x)).ToList();
        }
        public static RequisicionProductoNuevoDTO ToModel(RequisicionProductoRevisionDTO modelPord)
        {
            return new RequisicionProductoNuevoDTO()
            {
                IdProducto = modelPord.IdProducto,
                IdTipoProducto = modelPord.IdTipoProducto,
                IdCentroCosto = modelPord.IdCentroCosto,
                Cantidad = modelPord.Cantidad,
                Aplicacion = modelPord.Aplicacion
            };
        }
        public static List<RequisicionProductoEDTO> ToEDTO(List<RequisicionProductoNuevoDTO> modelPord)
        {
            return modelPord.Select(x => ToEDTO(x)).ToList();
        }
        public static RequisicionProductoEDTO ToEDTO(RequisicionProductoNuevoDTO modelPord)
        {
            return new RequisicionProductoEDTO()
            {
                IdProducto = modelPord.IdProducto,
                IdTipoProducto = modelPord.IdTipoProducto,
                IdCentroCosto = modelPord.IdCentroCosto,
                Cantidad = modelPord.Cantidad,
                Aplicacion = modelPord.Aplicacion
            };
        }
        #endregion
    }
}