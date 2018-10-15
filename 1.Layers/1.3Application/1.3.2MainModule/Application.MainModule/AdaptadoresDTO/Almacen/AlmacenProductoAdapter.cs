using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Almacen
{
    public static class AlmacenProductoAdapter
    {
        public static OrdenCompraEntradasDTO ToDTO(OrdenCompra oc, Sagas.MainModule.Entidades.Requisicion req)
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
        public static List<AlmacenEntradaDTO> ToDTO(List<RequisicionProducto> prod)
        {
            return prod.Select(x => ToDTO(x)).ToList();
        }
        public static AlmacenEntradaProducto FromDTO(AlmacenEntradaDTO dto, int idOC, Sagas.MainModule.Entidades.Almacen _alm)
        {
            return new AlmacenEntradaProducto()
            {
                IdOrdenCompra = idOC,
                IdRequisicion = new OrdenCompraDataAccess().Buscar(idOC).IdRequisicion,
                IdAlmacen = _alm.IdAlmacen,
                IdProduto = dto.IdProducto,
                IdUsuarioRecibe = TokenServicio.ObtenerIdUsuario(),
                Cantidad = dto.Cantidad,
                Observaciones_ = dto.Aplicacion,
                FechaEntrada = Convert.ToDateTime(DateTime.Today.ToShortDateString()),
                FechaRegistro = Convert.ToDateTime(DateTime.Today.ToShortDateString())
            };
        }

        public static AlmacenDTO ToDTO(Sagas.MainModule.Entidades.Almacen entidad)
        {
            var prod = ProductoServicio.ObtenerProducto(entidad.IdProduto);
            return new AlmacenDTO()
            {
                IdAlmacen = entidad.IdAlmacen,
                IdEmpresa = entidad.IdEmpresa,
                IdProduto = entidad.IdProduto,
                Cantidad = entidad.Cantidad,
                Ubicacion = entidad.Ubicacion,
                FechaRegistro = entidad.FechaRegistro,
                FechaActualizacion = entidad.FechaActualizacion,
                Descripcion = prod.Descripcion,
                IdCategoria = prod.IdCategoria,
                Categoria = prod.Categoria.Descripcion,
                IdProductoLinea = prod.IdProductoLinea,
                ProductoLinea = prod.LineaProducto.Descripcion
            };
        }
        public static List<AlmacenDTO> ToDTO(List<Sagas.MainModule.Entidades.Almacen> entidad)
        {
            return entidad.Select(x => ToDTO(x)).ToList();
        }
    }
}
