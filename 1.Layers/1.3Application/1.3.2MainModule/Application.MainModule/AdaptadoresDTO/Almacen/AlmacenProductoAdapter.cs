using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.Servicios.AccesoADatos;
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
                Producto = ToDTO(req.Productos.ToList())
            };
        }
        public static AlmacenEntradaDTO ToDTO(RequisicionProducto prod)
        {
            return new AlmacenEntradaDTO()
            {
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
        public static AlmacenEntradaProducto FromDTO(AlmacenCrearEntradaDTO dto, Sagas.MainModule.Entidades.Almacen _alm)
        {
            return new AlmacenEntradaProducto()
            {
                IdOrdenCompra = dto.IdOrdenCompra,
                IdRequisicion = new OrdenCompraDataAccess().Buscar(dto.IdOrdenCompra).IdRequisicion,
                IdAlmacen = _alm.IdAlmacen,
                IdProduto = dto.IdProduto,
                IdUsuarioRecibe = TokenServicio.ObtenerIdUsuario(),
                Cantidad = dto.Cantidad,
                Observaciones_ = dto.Observaciones,
                FechaEntrada = Convert.ToDateTime(DateTime.Today.ToShortDateString()),
                FechaRegistro = Convert.ToDateTime(DateTime.Today.ToShortDateString())
            };
        }
    }
}
