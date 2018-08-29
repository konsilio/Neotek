using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Compras;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios;

namespace Application.MainModule.AdaptadoresDTO.Compras
{
    public class OrdenComprasAdapter
    {
        public static RequisicionOCDTO ToDTO(Sagas.MainModule.Entidades.Requisicion _req)
        {
            RequisicionOCDTO Req = new RequisicionOCDTO();
            Req.NumeroRequisicion = _req.NumeroRequisicion;
            Req.UsuarioSolicitante = _req.Solicitante.NombreUsuario;
            Req.NombreComercial = _req.Empresa.NombreComercial;
            Req.MotivoRequisicion = _req.MotivoRequisicion;
            Req.RequeridoEn = _req.RequeridoEn;
            Req.FechaRequerida = _req.FechaRequerida;
            Req.Productos = ProductosOCAdapter.ToDTO(_req.Productos.ToList());
            return Req;
        }

        public static OrdenCompraDTO ToDTO(OrdenCompra oc)
        {
            OrdenCompraDTO ocDTO = new OrdenCompraDTO
            {
                IdEmpresa = oc.IdEmpresa,
                IdOrdenCompraEstatus = oc.IdOrdenCompraEstatus,
                IdRequisicion = oc.IdRequisicion,
                IdProveedor = oc.IdProveedor,
                IdCentroCosto = oc.IdCentroCosto,
                IdCuentaContable = oc.IdCuentaContable,
                EsActivoVenta = oc.EsActivoVenta,
                EsGas = oc.EsGas,
                Activo = oc.Activo,
                SubtotalSinIva = oc.SubtotalSinIva != null ? oc.SubtotalSinIva.Value : 0,
                SubtotalSinIeps = oc.SubtotalSinIeps != null ? oc.SubtotalSinIeps.Value : 0,
                Iva = oc.Iva != null ? oc.Iva.Value :  0,
                Ieps = oc.Ieps != null ? oc.Ieps.Value : 0,
                Total = oc.Total != null ? oc.Total.Value : 0,
                EsTransporteGas = oc.EsTransporteGas
            };
            return ocDTO;
        }

        public static List<OrdenCompraDTO>ToDTO(List<OrdenCompra> ocDTO)
        {
            List<OrdenCompraDTO> oc = ocDTO.Select(x => ToDTO(x)).ToList();
            return oc;
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
          public static OrdenCompra FromEntity(OrdenCompra ocDTO)
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
    }
}
