using System;
using System.Collections.Generic;
using System.Linq;
using Application.MainModule.DTOs.Compras;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs;

namespace Application.MainModule.AdaptadoresDTO.Compras
{
    public class OrdenComprasAdapter
    {
        public static RequisicionOCDTO ToDTO(Sagas.MainModule.Entidades.Requisicion _req)
        {
            RequisicionOCDTO Req = new RequisicionOCDTO();
            Req.IdRequisicion = _req.IdRequisicion;
            Req.NumeroRequisicion = _req.NumeroRequisicion;
            Req.IdUsuarioSolicitante = _req.IdUsuarioSolicitante;
            Req.UsuarioSolicitante = _req.Solicitante.NombreUsuario;
            Req.IdEmpresa = _req.IdEmpresa;
            Req.NombreComercial = _req.Empresa.NombreComercial;
            Req.MotivoRequisicion = _req.MotivoRequisicion;
            Req.RequeridoEn = _req.RequeridoEn;
            Req.FechaRequerida = _req.FechaRequerida;
            Req.ProductosOC = ProductosOCAdapter.ToDTO(_req.Productos.ToList());
            return Req;
        }      
        public static List<OrdenCompraDTO> ToDTO(List<OrdenCompra> ocDTO)
        {
            List<OrdenCompraDTO> oc = ocDTO.Select(x => ToDTO(x)).ToList();
            return oc;
        }
        public static OrdenCompraDTO ToDTO(OrdenCompra oc)
        {
            OrdenCompraDTO ocDTO = new OrdenCompraDTO
            {
                IdOrdenCompra = oc.IdOrdenCompra,
                IdEmpresa = oc.IdEmpresa,
                Empresa = oc.Empresa.NombreComercial,
                IdOrdenCompraEstatus = oc.IdOrdenCompraEstatus,
                NumOrdenCompra = oc.NumOrdenCompra,
                OrdenCompraEstatus = oc.OrdenCompraEstatus.Descripcion,
                IdRequisicion = oc.IdRequisicion,
                NumeroRequisicion = oc.Requisicion.NumeroRequisicion,
                IdProveedor = oc.IdProveedor,
                Proveedor = oc.Proveedor.NombreComercial,
                IdCentroCosto = oc.IdCentroCosto,
                IdCuentaContable = oc.IdCuentaContable,
                IdUsuarioGenerador = oc.IdUsuarioGenerador,
                Solicitante = string.Concat(oc.UsuarioGenerador.Nombre, " ", oc.UsuarioGenerador.Apellido1),
                IdUsuarioAutorizador = oc.IdUsuarioAutorizador,
                EsActivoVenta = oc.EsActivoVenta,
                EsGas = oc.EsGas,
                Activo = oc.Activo,
                FechaRegistro = oc.FechaRegistro,
                FechaAutorizacion = oc.FechaAutorizacion != null ? oc.FechaAutorizacion.Value : DateTime.MinValue,
                FechaRequerida = oc.Requisicion.FechaRequerida,
                SubtotalSinIva = oc.SubtotalSinIva != null ? oc.SubtotalSinIva.Value : 0,
                SubtotalSinIeps = oc.SubtotalSinIeps != null ? oc.SubtotalSinIeps.Value : 0,
                Iva = oc.Iva != null ? oc.Iva.Value : 0,
                Ieps = oc.Ieps != null ? oc.Ieps.Value : 0,
                Total = oc.Total != null ? oc.Total.Value : 0,
                EsTransporteGas = oc.EsTransporteGas,
                Productos = ProductosOCAdapter.ToDTOx(oc.Productos.ToList()),
                FolioFiscalUUID = oc.FolioFiscalUUID,
                FolioFactura = oc.FolioFactura,
                //Expedidor
                MontBelvieuDlls = oc.MontBelvieuDlls,
                TarifaServicioPorGalonDlls = oc.TarifaServicioPorGalonDlls,
                TipoDeCambioDOF = oc.TipoDeCambioDOF,
                PrecioPorGalon = oc.PrecioPorGalon,
                FactorGalonALitros = oc.FactorGalonALitros,
                ImporteEnLitros = oc.ImporteEnLitros,
                FactorCompraLitrosAKilos = oc.FactorCompraLitrosAKilos,
                PVPM = oc.PVPM,
                //Porteador
                FactorConvTransporte = oc.FactorConvTransporte,
                PrecioTransporte = oc.PrecioTransporte,
                Casetas = oc.Casetas,

            };
            return ocDTO;
        }
        public static OrdenCompra FromDTO(OrdenCompraDTO ocDTO)
        {
            OrdenCompra oc = new OrdenCompra
            {
                IdEmpresa = ocDTO.IdEmpresa,
                IdOrdenCompraEstatus = ocDTO.IdOrdenCompraEstatus,
                IdRequisicion = ocDTO.IdRequisicion,
                IdProveedor = ocDTO.IdProveedor,
                IdCentroCosto = ocDTO.IdCentroCosto,
                IdCuentaContable = ocDTO.IdCuentaContable,
                EsActivoVenta = ocDTO.EsActivoVenta,
                EsGas = ocDTO.EsGas,
                Activo = ocDTO.Activo,
                SubtotalSinIva = ocDTO.SubtotalSinIva,
                SubtotalSinIeps = ocDTO.SubtotalSinIeps,
                Iva = ocDTO.Iva,
                Ieps = ocDTO.Ieps,
                Total = ocDTO.Total,
                EsTransporteGas = ocDTO.EsTransporteGas
            };
            return oc;
        }
        public static List<OrdenCompra> FromDTO(List<OrdenCompraDTO> ocDTO)
        {
            List<OrdenCompra> oc = ocDTO.Select(x => FromDTO(x)).ToList();
            return oc;
        }
        public static OrdenCompra FromEntity(OrdenCompra oc)
        {
            return new OrdenCompra
            {
                IdEmpresa = oc.IdEmpresa,
                IdOrdenCompraEstatus = oc.IdOrdenCompraEstatus,
                IdRequisicion = oc.IdRequisicion,
                IdOrdenCompra = oc.IdOrdenCompra,                
                IdProveedor = oc.IdProveedor,
                IdUsuarioGenerador = oc.IdUsuarioGenerador,
                IdUsuarioAutorizador = oc.IdUsuarioAutorizador,
                IdCentroCosto = oc.IdCentroCosto,
                IdCuentaContable = oc.IdCuentaContable,
                FechaRegistro = oc.FechaRegistro,
                NumOrdenCompra = oc.NumOrdenCompra,                
                EsActivoVenta = oc.EsActivoVenta,
                EsGas = oc.EsGas,
                Activo = oc.Activo,
                SubtotalSinIva = oc.SubtotalSinIva,
                SubtotalSinIeps = oc.SubtotalSinIeps,
                Iva = oc.Iva,
                Ieps = oc.Ieps,
                Total = oc.Total,
                EsTransporteGas = oc.EsTransporteGas,
                FechaAutorizacion = oc.FechaAutorizacion,                
                Casetas = oc.Casetas,
                FactorCompraLitrosAKilos = oc.FactorCompraLitrosAKilos,
                FactorConvTransporte = oc.FactorConvTransporte,
                FactorGalonALitros = oc.FactorGalonALitros,
                FechaResgistroFactura = oc.FechaResgistroFactura,
                FolioFactura = oc.FolioFactura,
                FolioFiscalUUID = oc.FolioFiscalUUID,
                FechaComplemento = oc.FechaComplemento,
                ImporteEnLitros = oc.ImporteEnLitros,
                MontBelvieuDlls = oc.MontBelvieuDlls, 
                PrecioPorGalon = oc.PrecioPorGalon,
                PrecioTransporte = oc.PrecioTransporte,
                PVPM = oc.PVPM,
                TarifaServicioPorGalonDlls = oc.TarifaServicioPorGalonDlls,
                TipoDeCambioDOF = oc.TipoDeCambioDOF,
            };            
        }
        public static OrdenCompraEstatusDTO ToDTO(OrdenCompraEstatus est)
        {
            return new OrdenCompraEstatusDTO()
            {
                IdOrdenCompraEstatus = est.IdOrdenCompraEstatus,
                Descripcion = est.Descripcion
            };
        }
        public static List<OrdenCompraEstatusDTO> ToDTO(List<OrdenCompraEstatus> est)
        {
            return est.Select(x => ToDTO(x)).ToList();
        }
    }
}
