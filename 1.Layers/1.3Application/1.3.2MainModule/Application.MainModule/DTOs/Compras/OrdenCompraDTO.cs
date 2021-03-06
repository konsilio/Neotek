﻿using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    [Serializable]
    public class OrdenCompraDTO
    {
        public int IdOrdenCompra { get; set; }
        public short IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public byte IdOrdenCompraEstatus { get; set; }
        public string OrdenCompraEstatus { get; set; }
        public int IdRequisicion { get; set; }
        public string NumeroRequisicion { get; set; }
        public string Solicitante { get; set; }
        public string MotivoRequisicion { get; set; }
        public string RequeridoEn { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public Nullable<int> IdUsuarioGenerador { get; set; }
        public Nullable<int> IdUsuarioAutorizador { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdCuentaContable { get; set; }
        public string NumOrdenCompra { get; set; }
        public bool EsActivoVenta { get; set; }
        public bool EsGas { get; set; }
        public bool EsTransporteGas { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        public DateTime FechaRequerida { get; set; }
        public Nullable<decimal> SubtotalSinIva { get; set; }
        public Nullable<decimal> SubtotalSinIeps { get; set; }
        public Nullable<decimal> Iva { get; set; }
        public Nullable<decimal> Ieps { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> MontBelvieuDlls { get; set; }
        public Nullable<decimal> TarifaServicioPorGalonDlls { get; set; }
        public Nullable<decimal> TipoDeCambioDOF { get; set; }
        public Nullable<decimal> PrecioPorGalon { get; set; }
        public Nullable<decimal> FactorGalonALitros { get; set; }
        public Nullable<decimal> ImporteEnLitros { get; set; }
        public Nullable<decimal> FactorCompraLitrosAKilos { get; set; }
        public Nullable<decimal> PVPM { get; set; }
        public string FolioFiscalUUID { get; set; }
        public string FolioFactura { get; set; }
        public DateTime? FechaResgistroFactura { get; set; }
        public Nullable<decimal> FactorConvTransporte { get; set; }
        public Nullable<decimal> PrecioTransporte { get; set; }
        public Nullable<decimal> Casetas { get; set; }
        public List<OrdenCompraProductoDTO> Productos { get; set; }
    }
}
