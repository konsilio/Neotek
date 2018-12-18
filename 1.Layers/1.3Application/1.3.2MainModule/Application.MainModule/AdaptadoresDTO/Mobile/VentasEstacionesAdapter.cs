using System.Collections.Generic;
using System.Linq;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile;
using System;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Mobile;
using Application.MainModule.Servicios.Seguridad;
using Application.MainModule.DTOs.Mobile.Cortes;

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
                //IdCliente = venta.IdCliente,
                //RazonSocial = cliente.RazonSocial,
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
                    CantidadLt = concepto.CantidadLt,   
                    CantidadKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(concepto.CantidadLt, EmpresaServicio.Obtener(idEmpresa).FactorLitrosAKilos),
                    //Agregados recientemente
                    IdUnidadMedida = concepto.IdUnidadMedida,
                    PrecioUnitarioKg = concepto.PrecioUnitarioKg,
                    PrecioUnitarioLt = concepto.PrecioUnitarioLt,
                    DescuentoUnitarioKg = concepto.DescuentoUnitarioKg,
                    DescuentoUnitarioLt = concepto.DescuentoUnitarioLt,
                    

            });
                idOrdenDetalle++;
            }
            return list;
        }

        public static List<DatosGasVentaDto> ToDTO(List<Producto> productosGas, List<PrecioVenta> precios, decimal CatidadKilosGas)
        {
            return productosGas.Select(x => ToDTO(x, precios, CatidadKilosGas)).ToList();
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
        public static DatosGasVentaDto ToDTO(Producto productoGas, List<PrecioVenta> precios, decimal CantidadKgGas)
        {
            var precio = precios.Find(x => x.IdProducto.Equals(productoGas.IdProducto));
            if (productoGas.EsGas)
            {
                return new DatosGasVentaDto()
                {
                    Nombre = productoGas.Descripcion,
                    Id = productoGas.IdProducto,
                    PrecioUnitario = precio.PrecioSalidaKg.Value,
                    Existencia = CantidadKgGas,
                };
            }
            else
            {
                var existencias = ProductoAlmacenServicio.ObtenerAlmacen(productoGas.IdProducto, TokenServicio.ObtenerIdEmpresa());
                return new DatosGasVentaDto()
                {
                    Nombre = productoGas.Descripcion,
                    Id = productoGas.IdProducto,
                    PrecioUnitario = precio.PrecioSalidaKg.Value,
                    Existencia = existencias != null ? existencias.Cantidad : 0,
                };
            }
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
                var IdCamioneta = VentaServicio.ObtenerIdCamioneta(TokenServicio.ObtenerIdUsuario());
                if (IdCamioneta != 0)
                {
                    var cilindrosCamionetas = cilindro.CilindrosCamionetas;
                    var cilindroExistencia = cilindro.Cantidad;
                    
                    //if (cilindrosCamionetas != null)
                    //    cilindroExistencia = 0;
                    //else
                    //    cilindroExistencia =  cilindrosCamionetas.FirstOrDefault(x => x.IdCilindro.Equals(cilindro.IdCilindro) && x.IdCamioneta.Equals(IdCamioneta)).Cantidad;
                    list.Add(new DatosGasVentaDto()
                    {
                        Nombre = "Cilindro " + cilindro.CapacidadKg,
                        PrecioUnitario = cilindro.Precio,
                        Id = cilindro.IdCilindro,                        
                        Existencia = cilindroExistencia,//,
                        CapacidadKg = cilindro.CapacidadKg,
                        CapacidadLt = cilindro.CapacidadLt
                    });
                }
                else
                {

                    list.Add(new DatosGasVentaDto()
                    {
                        Nombre = "Cilindro " + cilindro.CapacidadKg,
                        PrecioUnitario = cilindro.Precio,
                        Id = cilindro.IdCilindro,
                        CapacidadKg = cilindro.CapacidadKg,
                        CapacidadLt = cilindro.CapacidadLt
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// VentaPuntoDeVenta
        /// Permite transformar los datos de la venta a actualizar para la sección de 
        /// cortes de estación, se enviara de parametros un objeto de tipo VentasCorteDTO
        /// con las ventas incluidas dentro del corte y VentaPuntoDeVenta que se la venta
        /// registrada
        /// Developer: Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
        /// Date: 4/12/2018
        /// Update: 4/12/2018
        /// Company: Neoteck
        /// </summary>
        /// <param name="item">Ventas incluidas en el corte</param>
        /// <param name="venta">Venta a actualizar</param>
        /// <returns></returns>
        public static VentaPuntoDeVenta ToDTO(VentasCorteDTO item, VentaPuntoDeVenta venta)
        {
            return new VentaPuntoDeVenta()
            {
                IdEmpresa = venta.IdEmpresa,
                Year = venta.Year,
                Mes = venta.Mes,
                Dia = venta.Dia,
                Orden = venta.Orden,
                IdPuntoVenta = venta.IdPuntoVenta,
                IdCliente =venta.IdCliente,
                IdOperadorChofer= venta.IdOperadorChofer,
                IdTipoVenta = venta.IdTipoVenta,//No se de que es no tiene relación
                IdFactura = venta.IdFactura,
                FolioOperacionDia = item.Corte,
                FolioVenta = venta.FolioVenta,
                RequiereFactura= venta.RequiereFactura,
                VentaACredito = venta.VentaACredito,
                Subtotal = venta.Subtotal,
                Descuento =venta.Descuento,
                Iva =venta.Iva,
                Total = venta.Total,
                PorcentajeIva = venta.PorcentajeIva,
                EfectivoRecibido = venta.EfectivoRecibido,
                CambioRegresado= venta.CambioRegresado,
                PuntoVenta = venta.PuntoVenta,
                RazonSocial =venta.RazonSocial,
                RFC =venta.RFC,
                ClienteConCredito = venta.ClienteConCredito,
                OperadorChofer =venta.OperadorChofer,
                DatosProcesados = venta.DatosProcesados,
                FechaAplicacion = venta.FechaAplicacion,
                FechaRegistro = venta.FechaRegistro
            };
        }

       
    }
}
