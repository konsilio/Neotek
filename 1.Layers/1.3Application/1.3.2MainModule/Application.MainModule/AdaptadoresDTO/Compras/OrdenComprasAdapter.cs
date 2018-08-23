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
                NumOrdenCompra = FolioServicio.GeneraNumerOrdenCompra(ocDTO),
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
