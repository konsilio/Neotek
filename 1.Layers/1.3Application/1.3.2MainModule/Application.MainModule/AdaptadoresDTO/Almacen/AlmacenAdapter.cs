using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.Servicios.Almacenes;

namespace Application.MainModule.AdaptadoresDTO.Almacenes
{
    public static class AlmacenAdapter
    {
        public static Almacen FromEmtity(Almacen _alm)
        {
            return new Almacen
            {
                IdAlmacen = _alm.IdAlmacen,
                IdEmpresa = _alm.IdEmpresa,
                IdProduto = _alm.IdProduto,
                Cantidad = _alm.Cantidad,
                Ubicacion = _alm.Ubicacion,
                FechaActualizacion = Convert.ToDateTime(_alm.FechaActualizacion.ToShortDateString()),
                FechaRegistro = Convert.ToDateTime(_alm.FechaRegistro.ToShortDateString())
            };
        }
        public static RequisicionSalidaDTO FromDTO(Requisicion r)
        {
            return new RequisicionSalidaDTO()
            {
                IdEmpresa = r.IdEmpresa,
                Empresa = r.Empresa.NombreComercial,
                IdRequisicion = r.IdRequisicion,
                IdRequisicionEstatus = r.IdRequisicionEstatus,
                NumeroRequisicion = r.NumeroRequisicion,
                UsuarioSolicitante = string.Concat(r.Solicitante.Nombre, " ", r.Solicitante.Apellido1),
                MotivoRequisicion = r.MotivoRequisicion,
                RequeridoEn = r.RequeridoEn,
                FechaRequerida = r.FechaRequerida,
                Productos = FromDTO(r.Productos.ToList()),
            };
        }
        public static AlmacenSalidaProductoDTO FromDTO(RequisicionProducto p)
        {
            var almacen = ProductoAlmacenServicio.ObtenerAlmacen(p.IdProducto, p.Requisicion.IdEmpresa);
            return new AlmacenSalidaProductoDTO()
            {
                IdRequisicion = p.IdRequisicion,
                Orden = p.Orden,
                IdProducto = p.IdProducto,
                Descripcion = p.Producto.Descripcion,
                Ubicacion = almacen.Ubicacion,
                Requeridos = p.Cantidad,
                CantidadEntregada = p.CantidadEntregada ?? 0,
                UnidadMedida = p.Producto.UnidadMedida.Descripcion,
                CantidadActual = almacen.Cantidad,
                Autorizado = p.AutorizaEntrega ?? false,
            };
        }
        public static List<AlmacenSalidaProductoDTO> FromDTO(List<RequisicionProducto> ps)
        {
            return ps.Select(x => FromDTO(x)).ToList();
        }
    }
}
