using MVC.Presentacion.Agente;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class ReporteServicio
    {
        public static List<CuentasPorPagarDTO> BuscarCuentasPorPagar(CuentasPorPagarModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscaCuentasPorPagar(model, token);
            return respuestaReq._ListaCuentasPorPagar;
        }
        public static List<InventarioPorPuntoVentaDTO> BuscarInventarioPorPuntoVenta(InventarioPorPuntoVentaModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscaInventarioPorPuntoVenta(model, token);
            return respuestaReq._ListaInventarioPuntoVenta;
        }
        public static List<HistoricoPrecioVentaDTO> BuscarHistoricoPrecioVenta(HistoricoPrecioVentaModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarHistoricoPrecioVenta(model, token);
            return respuestaReq._ListaHistoricoPrecioVenta;
        }
        public static List<CallCenterDTO> BuscarCallCenter(PeriodoDTO model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarCallCenter(model, token);
            return respuestaReq._ListaCallCenter;
        }
        public static List<RequisicionRepDTO> BuscarRequisicion(RequisicionModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarRequisicion(model, token);
            return respuestaReq._ListaRequisicion;
        }
        public static List<OrdenCompraRepDTO> BuscarOrdenCompra(OrdenCompraModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarOrdenCompra(model, token);
            return respuestaReq._ListaOrdenCompra;
        }
        public static List<VentasDTO> BuscarVentas(PeriodoDTO model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarVentas(model, token);
            return respuestaReq._ListaVentas;
        }
        public static List<OrdenCompraRepDTO> BuscarOrdenCompra(PeriodoDTO model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarVentas(model, token);
            return respuestaReq._ListaOrdenCompra;
        }
        public static List<RendimientoVehicularDTO> BuscarRendimientoVehicular(RendimientoVehicularModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarRendimientoVehicular(model, token);
            return respuestaReq._ListaRendimientoVehicular;
        }

        public static List<RendimientoCamionetaDTO> BuscarRendimientoVehicularCamionetas(PeriodoDTO model, string token, bool EsPipa = false)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarPuntoEqulibrio(model, token, EsPipa);       
                return respuestaReq._ListaRendimientoVehicularCamioneta;
       
        }
        public static List<RendimientoVehicularPipasModel> BuscarRendimientoVehicularPipas(PeriodoDTO model, string token, bool EsPipa = true)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarPuntoEqulibrio(model, token, EsPipa);
            return respuestaReq._ListaRendimientoVehicularPipas;
        }

        public static List<AutoConsumoModel> BuscarAutoConsumo(PeriodoDTO model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarAutoConsumos(model, token);
            return respuestaReq._ListaAutoConsumos;
        }

        public static List<DescuentosXClientes> BuscarDescuentosXClientes(PeriodoDTO model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarDescuentosXClientes(model, token);
            return respuestaReq._ListDescuentosXClientes;
        }
        public static List<CreditoRecuperadoDTO> BuscarCreditoRecuperadoClientes(PeriodoDTO model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarCreditoRecuperadoClientes(model, token);
            return respuestaReq._ListCreditoRecuperadoClientes;
        }
        public static List<CreditoOtorgadoModel> BuscarCreditoOtorgadoClientes(PeriodoDTO model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarCreditoOtorgadoClientes(model, token);
            return respuestaReq._ListCreditoOtorgadoClientes;
        }
        public static List<CreditoXCliente> BuscarCreditoXCliente(PeriodoDTO model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarCreditoXCliente(model, token);
            return respuestaReq._ListCreditoCreditoXCliente;
        }
        public static List<ControlAsistenciaModel> BuscarUsuarioAsistencia(PeriodoDTO model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarUsuarioAsistencia(model, token);
            return respuestaReq._ListControlAsistencia;
        }
        public static List<CreditoXClienteMensualModel> BuscarCreditoXClienteMensual(PeriodoDTO model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarCreditoXClienteMensual(model, token);
            return respuestaReq._ListCreditoCreditoXClienteMensual;
        }



        public static RespuestaDTO BuscarVentasXPuntoVenta(VentasXPuntoVentaModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.VentasXPuntoVenta(model, token);
            return respuestaReq._RespuestaDTO;
        }
        public static RespuestaDTO BuscarEquipoDeTransporte(PeriodoDTO model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.EquipoDeTransporte(model, token);
            return respuestaReq._RespuestaDTO;
        }
        

        public static List<InventarioXConceptoDTO> BuscarInventarioConcepto(InventarioXConceptoModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarInventarioPorConcepto(model, token);
            return respuestaReq._ListaInventarioConcepto;
        }
        public static List<YearsDTO> BuscarHistoricoVSVentas(HistoricoVentasConsulta model, string tkn)
        {
            AgenteServicio agenteServico = new AgenteServicio();
            agenteServico.GetVentasTotales(model, tkn);
            return agenteServico._ListYears;
        }
        public static List<CorteCajaDTO> BuscarCorteCaja(CorteCajaModel model, string tkn)
        {
            AgenteServicio agenteServico = new AgenteServicio();
            agenteServico.BuscarRepoCorteCaja(model, tkn);
            return agenteServico._ListaCorteCaja;
        }
        public static List<GastoVehiculoDTO> BuscarGastoVehicular(GastoVehiculoModel model, string tkn)
        {
            AgenteServicio agenteServico = new AgenteServicio();
            agenteServico.BuscarRepoGastoVehicular(model, tkn);
            return agenteServico._ListaGastoVehicular;
        }
        public static List<ComisionDTO> CalcularComisiones(PeriodoDTO model, string tkn)
        {
            AgenteServicio agenteServico = new AgenteServicio();
            agenteServico.CalcularComisiones(model, tkn);
            return agenteServico._ListaComisiones;
        }
        public static List<CuentaConsolidadaDTO> CuentasConsolidadas(CuentasPorPagarModel model, string tkn)
        {
            AgenteServicio agenteServico = new AgenteServicio();
            agenteServico.CuentasConsolidadas(model, tkn);
            return agenteServico._ListaCuentasConsolidadas;
        }
       
    }
}