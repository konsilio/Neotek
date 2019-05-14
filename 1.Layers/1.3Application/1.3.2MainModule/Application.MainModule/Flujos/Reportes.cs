﻿using Application.MainModule.AdaptadoresDTO.Almacenes;
using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.AdaptadoresDTO.EquipoTrasnporteServicio;
using Application.MainModule.AdaptadoresDTO.IngresoEgreso;
using Application.MainModule.AdaptadoresDTO.Pedidos;
using Application.MainModule.AdaptadoresDTO.Requisiciones;
using Application.MainModule.AdaptadoresDTO.Seguridad;
using Application.MainModule.DTOs;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios.Equipo;
using Application.MainModule.Servicios.IngresoGasto;
using Application.MainModule.Servicios.Pedidos;
using Application.MainModule.Servicios.Requisiciones;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    //Estos reportes representaran el cubo de informacion 
    public class Reportes
    {
        public List<RepCuentaPorPagarDTO> RepCuentasPorPagar(DateTime periodo)
        {
            var requi = EgresoServicio.BuscarTodos(periodo);
            return EgresoAdapter.ToRepo(requi);
        }
        public List<RepInventarioPorPuntoVentaDTO> RepInventarioPorPuntoVenta(InventarioPorPuntoVentaDTO dto)
        {
            var pipas = PipaServicio.Obtener(dto.Pipas);
            var estaciones = EstacionCarburacionServicio.Obtener(dto.Estaciones);
            return new Almacenes().BuscarInvetarioPorPuntoDeVenta(pipas, estaciones);
        }
        public List<RepHistorioPrecioDTO> RepHistorioPrecios(HistoricoPrecioDTO dto)
        {
            var precios = PrecioVentaGasServicio.ObtenerListaPreciosVentaIdEmp(TokenServicio.ObtenerIdEmpresa(), dto.FechaInicial, dto.FechaFinal);
            return PrecioVentaGasAdapter.ToRepo(precios, dto);
        }
        public List<RepCallCenterDTO> RepCallCenter(CallCenterDTO dto)
        {
            var pedidos = PedidosServicio.Obtener(TokenServicio.ObtenerIdEmpresa(), dto.Periodo);
            return PedidosAdapter.FromDTO(pedidos);
        }
        public List<RepRequisicionDTO> RepRequisicion(RequisicionModelDTO dto)
        {
            var requisicones = RequisicionServicio.BuscarRequisicionPorPeriodo(TokenServicio.ObtenerIdEmpresa(), dto.FechaInicio, dto.FechaFinal);
            return RequisicionServicio.ConvertirReporte(requisicones);
        }
        public List<RepOrdenCompraDTO> RepOrdenCompra(OrdenCompraModelDTO dto)
        {
            var ordenes = OrdenCompraServicio.BuscarTodo(TokenServicio.ObtenerIdEmpresa(), dto.FechaInicio, dto.FechaFinal);
            return OrdenComprasAdapter.ToRepDTO(ordenes);
        }
        public List<RepRendimientoVehicularDTO> RepRendimientoVehicular(RendimientoVehicularDTO dto)
        {
            var recargas = RecargaCombustibleServicio.Buscar(dto.FechaInicio, dto.FechaFinal);
            return RecargaCombustibleAdapter.FormRepDTO(recargas);
        }
        public List<RepInventarioXConceptorDTO> RepInventarioPorConcepto(InventarioXConceptoDTO dto)
        {
            var alamacenes = ProductoAlmacenServicio.BuscarAlmacen(dto.FechaInicio, dto.FechaFinal);
            return AlmacenProductoAdapter.ToRepDTO(alamacenes);
        }
    }
}
