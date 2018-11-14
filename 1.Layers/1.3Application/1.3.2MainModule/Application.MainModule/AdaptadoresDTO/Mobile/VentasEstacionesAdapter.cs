using System.Collections.Generic;
using System.Linq;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile;
using System;
using Application.MainModule.DTOs.Respuesta;

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

        public static VentaPuntoDeVenta FromDTO(VentaDTO venta, Cliente cliente, PuntoVenta punto_venta,int idOrden,short idEmpresa)
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
                IdCliente = venta.IdCliente,
                RFC = cliente.Rfc,
                RazonSocial = cliente.RazonSocial,
                VentaPuntoDeVentaDetalle = ToDTO(venta.Concepto, venta, punto_venta, idOrden, idEmpresa),
            };
        }

        public static ICollection<VentaPuntoDeVentaDetalle> ToDTO(List<ConceptoDTO> conceptos, VentaDTO venta, PuntoVenta punto_venta, int idOrden, short idEmpresa)
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
                    CantidadLt = concepto.LitrosDespachados,
                    
                });
                idOrdenDetalle++;
            }
            return list;
        }

        public static List<DatosGasVentaDto> ToDTO(List<Producto> productosGas, List<PrecioVenta> precios)
        {
            return productosGas.Select(x => ToDTO(x, precios)).ToList();
        }

        public static DatosGasVentaDto ToDTO(Producto productoGas, List<PrecioVenta> precios)
        {
            var precio = precios.Find(x => x.IdProducto.Equals(productoGas.IdProducto));
            return new DatosGasVentaDto()
            {
                Nombre = productoGas.Descripcion,
                Id= productoGas.IdProducto,
                PrecioUnitario = precio.PrecioSalidaKg.Value,
                
            };
        }

        public static List<DatosGasVentaDto> ToDTO(UnidadAlmacenGas camioneta)
        {
            return camioneta.Camioneta.Cilindros.Select(x => ToDTO(x)).ToList();
        }

        public static DatosGasVentaDto ToDTO(CamionetaCilindro cilindro)
        {
            return new DatosGasVentaDto()
            {
                Id = cilindro.IdCilindro,
                Existencia = cilindro.Cantidad,
                PrecioUnitario = cilindro.UnidadAlmacenGasCilindro.Precio,
                Nombre = "Cilindro " + cilindro.UnidadAlmacenGasCilindro.CapacidadKg
            };
        }

        public static List<DatosGasVentaDto> ToDTOC(List<UnidadAlmacenGasCilindro> cilindros)
        {
            List<DatosGasVentaDto> list = new List<DatosGasVentaDto>();
            foreach (var cilindro in cilindros)
            {
                list.Add(new DatosGasVentaDto()
                {
                    Nombre = "Cilindro "+ cilindro.CapacidadKg,
                    PrecioUnitario =cilindro.Precio,
                    Id = cilindro.IdCilindro
                });
            }
            return list;
        }

    }
}
