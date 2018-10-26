using System.Collections.Generic;
using System.Linq;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile;
using System;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class VentasEstacionesAdapter
    {
        public static DatosOtrosDto ToDTO(List<CategoriaProducto> categorias, List<LineaProducto> lineas, List<Producto> productos)
        {
            return new DatosOtrosDto()
            {
                Categorias = categorias.Select(x => ToDTO(x)).ToList(),
                Lineas = lineas.Select(x => ToDTO(x)).ToList(),
                Productos = productos.Select(x=>ToDTO(x)).ToList()
            };
        }

        private static ProductoDTO ToDTO(Producto producto)
        {
            return new ProductoDTO()
            {
                IdProducto = producto.IdProducto,
                IdLinea = producto.IdProductoLinea,
                Nombre = producto.Descripcion,
                IdCategoria = producto.IdCategoria
            };
        }

        public static LineaDto ToDTO(LineaProducto linea)
        {
            return new LineaDto()
            {
                Id = linea.IdProductoLinea,
                Nombre = linea.Descripcion
            };
        }

        public static CategoriaDto ToDTO(CategoriaProducto categoria)
        {            
            return new CategoriaDto()
            {
                Id = categoria.IdCategoria,
                Nombre = categoria.Nombre,
            };
        }

        public static VentaPuntoDeVenta FromDTO(VentaDTO venta, Cliente cliente, PuntoVenta punto_venta, AlmacenGas almacen,int idOrden,short idEmpresa)
        {
            return new VentaPuntoDeVenta()
            {
                Orden = (short)idOrden,
                EfectivoRecibido = venta.Efectivo,
                IdEmpresa = idEmpresa,
                ClienteConCredito = venta.Credito,
                IdOperadorChofer = punto_venta.IdOperadorChofer,
                Iva = venta.Iva,
                Subtotal = venta.Subtotal,
                Total = venta.Total,
                CambioRegresado = venta.Total - venta.Efectivo,
                IdPuntoVenta = punto_venta.IdPuntoVenta,
                PuntoVenta = punto_venta.UnidadesAlmacen.Numero,
                VentaPuntoDeVentaDetalle = ToDTO(venta.Concepto, venta, punto_venta, almacen, idOrden, idEmpresa)
            };
        }

        public static ICollection<VentaPuntoDeVentaDetalle> ToDTO(List<ConceptoDTO> conceptos, VentaDTO venta, PuntoVenta punto_venta, AlmacenGas almacen, int idOrden, short idEmpresa)
        {
            List<VentaPuntoDeVentaDetalle> list = new List<VentaPuntoDeVentaDetalle>();
            int idOrdenDetalle = 1;
            foreach (var concepto in conceptos)
            {
                list.Add(new VentaPuntoDeVentaDetalle()
                {
                    FechaRegistro= venta.Fecha,
                    Dia = (byte)venta.Fecha.Day,
                    Mes = (byte)venta.Fecha.Month,
                    Year =(short) venta.Fecha.Year,
                    ProductoDescripcion = concepto.Concepto,
                    IdEmpresa= idEmpresa,
                    Orden = (short) idOrden,
                    OrdenDetalle = (short) idOrdenDetalle,
                    CantidadProducto = concepto.Cantidad,
                    IdCategoria = concepto.IdCategoria,
                    IdProductoLinea = concepto.IdLinea,
                    PrecioUnitarioProducto = concepto.PUnitario,
                    Subtotal = concepto.Subtotal,
                    IdProducto = concepto.IdProducto,
                    DescuentoTotal = concepto.Descuento,
                });
                idOrdenDetalle++;
            }
            return list;
        }
    }
}
