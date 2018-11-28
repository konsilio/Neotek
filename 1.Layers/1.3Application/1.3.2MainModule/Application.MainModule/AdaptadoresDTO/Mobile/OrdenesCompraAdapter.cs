
using Application.MainModule.DTOs.Compras;
using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
   public static class OrdenesCompraAdapter
    {
        public static OrdenCompraDTO ToDTO(OrdenCompra ordenCompra)
        {
            return new OrdenCompraDTO()
            {
                IdOrdenCompra = ordenCompra.IdOrdenCompra,
                NumOrdenCompra = ordenCompra.NumOrdenCompra,
                Ieps = ordenCompra.Ieps,
                IdProveedor = ordenCompra.IdProveedor,
                FechaRequisicion = ordenCompra.Requisicion.FechaRequerida,
                ProveedorCalle = ordenCompra.Proveedor.Calle,
                ProveedorCelular1 = ordenCompra.Proveedor.Celular1,
                ProveedorCelular2 = ordenCompra.Proveedor.Celular2,
                ProveedorCelular3 = ordenCompra.Proveedor.Celular3,
                ProveedorCodigoPostal = ordenCompra.Proveedor.CodigoPostal,
                ProveedorColonia = ordenCompra.Proveedor.Colonia,
                ProveedorEstadoProvincia = ordenCompra.Proveedor.EstadoProvincia,
                ProveedorMunicipio = ordenCompra.Proveedor.Municipio,
                ProveedorNombreComercial = ordenCompra.Proveedor.NombreComercial,
                ProveedorNumExt = ordenCompra.Proveedor.NumExt,
                ProveedorNumInt = ordenCompra.Proveedor.NumInt,
                ProveedorRfc = ordenCompra.Proveedor.Rfc,
                ProveedorTelefono1 = ordenCompra.Proveedor.Telefono1,
                ProveedorTelefono2 = ordenCompra.Proveedor.Telefono2,
                ProveedorTelefono3 = ordenCompra.Proveedor.Telefono3,
                Iva = ordenCompra.Iva,
                SubtotalSinIva = ordenCompra.SubtotalSinIva,
                Total = ordenCompra.Total,
                Productos = ToDTO(ordenCompra.Productos.ToList()),
            };
        }

        public static ProductoDTO ToDTO (OrdenCompraProducto ordenCompraProducto)
        {
            return new ProductoDTO()
            {
                Cantidad = ordenCompraProducto.Cantidad,
                Descuento = ordenCompraProducto.Descuento,
                IdOrdenCompra = ordenCompraProducto.IdOrdenCompra,
                IEPS = ordenCompraProducto.IEPS,
                Importe = ordenCompraProducto.Importe,
                IVA = ordenCompraProducto.IVA,
                Precio = ordenCompraProducto.Precio,
                Producto = ordenCompraProducto.Producto,
                UnidadMedida = ordenCompraProducto.UnidadMedida,
            };
        }
        public static List<ProductoDTO> ToDTO(List<OrdenCompraProducto> ordenCompraProducto)
        {
            return ordenCompraProducto.ToList().Select(x => ToDTO(x)).ToList();
        }

        public static List<OrdenCompraDTO> ToDTO(List<OrdenCompra> listOrdenCompra)
        {
            return listOrdenCompra.ToList().Select(x => ToDTO(x)).ToList();
        }
    //    public static OrdenCompra FormEntity(OrdenCompra oc)
    //    {
    //        return new OrdenCompra()
    //        {
    //      IdOrdenCompra { get; set; }
    //      IdEmpresa { get; set; }
    //     byte IdOrdenCompraEstatus { get; set; }
    //      IdRequisicion { get; set; }
    //      IdProveedor { get; set; }
    //     <> IdUsuarioGenerador { get; set; }
    //     <> IdUsuarioAutorizador { get; set; }
    //      IdCentroCosto { get; set; }
    //      IdCuentaContable { get; set; }
    //      NumOrdenCompra { get; set; }
    //     bool EsActivoVenta { get; set; }
    //     bool EsGas { get; set; }
    //     bool EsTransporteGas { get; set; }
    //     bool Activo { get; set; }
    //     System.DateTime FechaRegistro { get; set; }
    //     <System.DateTime> FechaAutorizacion { get; set; }
    //     <System.DateTime> FechaComplemento { get; set; }
    //     <> SubtotalSinIva { get; set; }
    //     <> SubtotalSinIeps { get; set; }
    //     <> Iva { get; set; }
    //     <> Ieps { get; set; }
    //     <> Total { get; set; }
    //     <> MontBelvieuDlls { get; set; }
    //     <> TarifaServicioPorGalonDlls { get; set; }
    //     <> TipoDeCambioDOF { get; set; }
    //     <> PrecioPorGalon { get; set; }
    //     <> FactorGalonALitros { get; set; }
    //     <> ImporteEnLitros { get; set; }
    //     <> FactorCompraLitrosAKilos { get; set; }
    //     <> PVPM { get; set; }
    //      FolioFiscalUUID { get; set; }
    //      FolioFactura { get; set; }
    //     <System.DateTime> FechaResgistroFactura { get; set; }
    //     <> FactorConvTransporte { get; set; }
    //     <> PrecioTransporte { get; set; }
    //     <> Casetas { get; set; }

    //};
    //    }
    }
}
