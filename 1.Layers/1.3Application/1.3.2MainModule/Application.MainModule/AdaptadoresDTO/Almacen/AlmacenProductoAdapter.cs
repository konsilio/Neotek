using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Application.MainModule.AdaptadoresDTO.Almacenes
{
    public static class AlmacenProductoAdapter
    {
        public static OrdenCompraEntradasDTO ToDTO(OrdenCompra oc, Requisicion req)
        {
            return new OrdenCompraEntradasDTO()
            {
                IdRequisicion = oc.IdRequisicion,
                IdOrdenCompra = oc.IdOrdenCompra,
                NumeroRequisicion = req.NumeroRequisicion,
                NumOrdenCompra = oc.NumOrdenCompra,
                IdEmpresa = oc.IdEmpresa,
                Empresa = oc.Empresa.NombreComercial,
                UsuarioSolicitante = req.Solicitante.Nombre,
                MotivoRequisicion = req.MotivoRequisicion,
                RequeridoEn = req.RequeridoEn,
                Proveedor = oc.Proveedor.NombreComercial,
                FechaRequerida = req.FechaRequerida,
                Productos = ToDTO(req.Productos.ToList())
            };
        }
        public static AlmacenEntradaDTO ToDTO(RequisicionProducto prod)
        {
            return new AlmacenEntradaDTO()
            {
                IdProducto = prod.IdProducto,
                TipoProducto = prod.Producto.TipoServicioOProducto.Nombre,
                Descripcion = prod.Producto.Descripcion,
                Requeridos = prod.Cantidad,
                UnidadMedida = prod.Producto.UnidadMedida.Descripcion,
                Aplicacion = prod.Aplicacion
            };
        }
        public static AlmacenEntradaDTO ToDTO(AlmacenEntradaProducto prod)
        {
            return new AlmacenEntradaDTO()
            {
                IdProducto = prod.IdProducto,
                TipoProducto = prod.Productos.TipoServicioOProducto.Nombre,
                Descripcion = prod.Productos.Descripcion,
                Requeridos = prod.Cantidad,
                UnidadMedida = prod.Productos.UnidadMedida.Descripcion,
                Observaciones = prod.Observaciones_
            };
        }
        public static List<AlmacenEntradaDTO> ToDTO(List<RequisicionProducto> prod)
        {
            return prod.Select(x => ToDTO(x)).ToList();
        }
        public static AlmacenEntradaProducto FromDTO(AlmacenEntradaDTO dto, int idOC, Almacen _alm)
        {
            var oc = new OrdenCompraDataAccess().Buscar(idOC);
            if (oc != null)
            {
                return new AlmacenEntradaProducto()
                {
                    IdOrdenCompra = idOC,
                    IdRequisicion = oc.IdRequisicion,
                    IdAlmacen = _alm.IdAlmacen,
                    IdProducto = dto.IdProducto,
                    IdUsuarioRecibe = TokenServicio.ObtenerIdUsuario(),
                    Cantidad = dto.Cantidad,
                    CantidadAnterior = dto.CantidadAnterior,
                    CantidadFinal = dto.CantidadFinal,
                    Observaciones_ = idOC.Equals(0) ? dto.Observaciones : oc.NumOrdenCompra,
                    FechaEntrada = DateTime.Now,
                    FechaRegistro = DateTime.Now
                };
            }
            else
            {
                return new AlmacenEntradaProducto()
                {                    
                    IdAlmacen = _alm.IdAlmacen,
                    IdProducto = dto.IdProducto,
                    IdUsuarioRecibe = TokenServicio.ObtenerIdUsuario(),
                    Cantidad = dto.Cantidad,
                    CantidadAnterior = dto.CantidadAnterior,
                    CantidadFinal = dto.CantidadFinal,
                    Observaciones_ = idOC.Equals(0) ? dto.Observaciones : oc.NumOrdenCompra,
                    FechaEntrada = DateTime.Now,
                    FechaRegistro = DateTime.Now
                };
            }           
        }
        public static AlmacenSalidaProducto FromDTO(AlmacenSalidaProductoDTO dto, int idReq, Almacen _alm)
        {            
            var req = new RequisicionDataAccess().BuscarPorIdRequisicion(idReq);
            if (req != null)
            {
                return new AlmacenSalidaProducto()
                {
                    IdRequisicion = req.IdRequisicion,
                    IdAlmacen = _alm.IdAlmacen,
                    IdProducto = dto.IdProducto,
                    IdUsuarioEntrega = dto.IdUsuarioEntrega,
                    IdUsuarioRecibe = TokenServicio.ObtenerIdUsuario(),
                    Cantidad = dto.Cantidad,
                    CantidadAnterior = dto.CantidadAnterior,
                    CantidadFinal = dto.CantidadFinal,
                    Observaciones_ = idReq.Equals(0) ? dto.Observaciones_ : req.NumeroRequisicion,
                    FechaEntrada = dto.FechaEntrada,
                    FechaRegistro = DateTime.Now
                };
            }
            else
            {
                return new AlmacenSalidaProducto()
                {                   
                    IdAlmacen = _alm.IdAlmacen,
                    IdProducto = dto.IdProducto,
                    IdUsuarioEntrega = TokenServicio.ObtenerIdUsuario(),
                    IdUsuarioRecibe = TokenServicio.ObtenerIdUsuario(),
                    Cantidad = dto.Cantidad,
                    CantidadAnterior = dto.CantidadAnterior,
                    CantidadFinal = dto.CantidadFinal,
                    Observaciones_ = idReq.Equals(0) ? dto.Observaciones_ : req.NumeroRequisicion,
                    FechaEntrada = DateTime.Now,
                    FechaRegistro = DateTime.Now
                };
            }
        }
        public static AlmacenDTO ToDTO(Almacen entidad)
        {
            //var prod = ProductoServicio.ObtenerProducto(entidad.IdProduto);
            return new AlmacenDTO()
            {
                IdAlmacen = entidad.IdAlmacen,
                IdEmpresa = entidad.IdEmpresa,
                IdProducto = entidad.IdProduto,
                Cantidad = entidad.Cantidad,
                Ubicacion = entidad.Ubicacion,
                FechaRegistro = entidad.FechaRegistro,
                FechaActualizacion = entidad.FechaActualizacion,
                Descripcion = entidad.Producto.Descripcion,
                IdCategoria = entidad.Producto.IdCategoria,
                Categoria = entidad.Producto.Categoria.Descripcion,
                IdProductoLinea = entidad.Producto.IdProductoLinea,
                ProductoLinea = entidad.Producto.LineaProducto.Descripcion
            };
        }
        public static List<AlmacenDTO> ToDTO(List<Almacen> entidad)
        {
            return entidad.Select(x => ToDTO(x)).ToList();
        }
        public static RepInventarioXConceptorDTO ToRepDTO(Almacen entidad)
        {         
            return new RepInventarioXConceptorDTO()
            {
                IdRegistro = entidad.IdAlmacen,
                Descripcion = entidad.Producto.Descripcion,
                Existencias = entidad.Cantidad,
                Categoria = entidad.Producto.Categoria.Descripcion,
                FechaActualizacion = entidad.FechaActualizacion,
            };
        }
        public static List<RepInventarioXConceptorDTO> ToRepDTO(List<Almacen> entidad)
        {
            return entidad.Select(x => ToRepDTO(x)).ToList();
        }
    }
}
