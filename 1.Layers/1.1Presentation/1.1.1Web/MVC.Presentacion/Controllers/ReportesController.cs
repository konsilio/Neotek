using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using DevExpress.Web.Mvc;

namespace MVC.Presentacion.Controllers
{
    public class ReportesController : Controller
    {
        string tkn = string.Empty;
        public ActionResult CuentasXPagar(CuentasPorPagarModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.Periodo.Equals(DateTime.MinValue))
            {
                ViewData["Periodo"] = model.Periodo;
                ViewData["Reporte"] = TiposReporteConst.CuentasXCobrar;
                TempData["DataSource"] = ReporteServicio.BuscarCuentasPorPagar(model, tkn);
            }
            return View(model);
        }
        public ActionResult InventarioXPuntoVenta(InventarioPorPuntoVentaModel model = null)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddSeconds(75);
            TimeSpan span = endTime.Subtract(startTime);

            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model == null)
                model = new InventarioPorPuntoVentaModel();
            if (model != null && !model.Fecha.Equals(DateTime.MinValue))
            {
                ViewData["Periodo"] = model.Fecha;
                ViewData["Reporte"] = TiposReporteConst.InventarioPorPuntoVenta;
                TempData["DataSource"] = ReporteServicio.BuscarInventarioPorPuntoVenta(model, tkn);
            }
            var pipas = PedidosServicio.ObtenerPipas(TokenServicio.ObtenerIdEmpresa(tkn), tkn);
            pipas.RemoveAt(0);
            model.Pipas = pipas.Select(x => { x.Activo = false; return x; }).ToList();
            model.Estaciones = CatalogoServicio.GetListaEstacionCarburacion(tkn).Select(x => { x.Activo = false; return x; }).ToList();
            var camionetas = PedidosServicio.ObtenerCamionetas(TokenServicio.ObtenerIdEmpresa(tkn), tkn).Select(x => { x.Activo = false; return x; }).ToList();
            camionetas.RemoveAt(0);
            model.Camionetas = camionetas;
            return View(model);
        }
        public ActionResult HistoricoPrecioVenta(HistoricoPrecioVentaModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model == null)
                model = new HistoricoPrecioVentaModel();
            ViewData["Reporte"] = TiposReporteConst.HistoricoPrecioVenta;
            if (model != null && !model.FechaFinal.Equals(DateTime.MinValue) && !model.FechaInicial.Equals(DateTime.MinValue))
                TempData["DataSource"] = ReporteServicio.BuscarHistoricoPrecioVenta(model, tkn);

            return View(model);
        }
        public ActionResult CallCenter(PeriodoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.CallCenter;
                TempData["DataSource"] = ReporteServicio.BuscarCallCenter(model, tkn);
            }
            return View(model);
        }
        public ActionResult Requisicion(RequisicionModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFinal.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.Requisicion;
                TempData["DataSource"] = ReporteServicio.BuscarRequisicion(model, tkn);
            }
            return View(model);
        }
        public ActionResult OrdenCompraRep(OrdenCompraModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFinal.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.OrdenCompra;
                TempData["DataSource"] = ReporteServicio.BuscarOrdenCompra(model, tkn);
            }
            return View(model);
        }

        public ActionResult Ventas(PeriodoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.Ventas;
                TempData["DataSource"] = ReporteServicio.BuscarVentas(model, tkn);
            }
            return View(model);
        }

        public ActionResult RendimientoVehicular(RendimientoVehicularModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFinal.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.RendimientoVehicular;
                TempData["DataSource"] = ReporteServicio.BuscarRendimientoVehicular(model, tkn);
            }
            return View(model);
        }

        public ActionResult RendimientoVehicularCamionetas(PeriodoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                if (model.Referencia == false)
                {
                    ViewData["Reporte"] = TiposReporteConst.RendimientoVehicularCamioneta;
                    TempData["DataSource"] = ReporteServicio.BuscarRendimientoVehicularCamionetas(model, tkn, model.Referencia);
                }
                else if(model.Referencia == true)
                {
                    ViewData["Reporte"] = TiposReporteConst.RendimientoVehicularPipas;
                    TempData["DataSource"] = ReporteServicio.BuscarRendimientoVehicularPipas(model, tkn, model.Referencia);

                }
                
                
              
            }
            return View(model);
        }

        public ActionResult AutoConsumos(PeriodoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.AutoConsumos;
                TempData["DataSource"] = ReporteServicio.BuscarAutoConsumo(model, tkn);
            }
            return View(model);
        }

        public ActionResult DescuentosXClientes(PeriodoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.DescuentosXClientes;
                TempData["DataSource"] = ReporteServicio.BuscarDescuentosXClientes(model, tkn);
            }
            return View(model);
        }

        public ActionResult VentasXPuntoVenta(VentasXPuntoVentaModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model.PeriodoDTO != null && !model.PeriodoDTO.FechaFin.Equals(DateTime.MinValue) && !model.PeriodoDTO.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.VentasXPuntoVenta;
                TempData["DataSource"] = JsonConvert.DeserializeObject(ReporteServicio.BuscarVentasXPuntoVenta(model, tkn).Mensaje);
            }
            return View(model);
        }

        public ActionResult EquipoDeTransporte(PeriodoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.EquipoDeTransporte;
                TempData["DataSource"] = JsonConvert.DeserializeObject(ReporteServicio.BuscarEquipoDeTransporte(model, tkn).Mensaje);
            }
            return View(model);
        }
        public ActionResult CreditoRecuperado(PeriodoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();            
            TempData.Remove("DataSourceDetallado");
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.CreditoRecuperado;
                TempData["DataSourceDetallado"] = TempData["DataSourceDetallado"] ?? ReporteServicio.BuscarCreditoRecuperadoClientes(model, tkn);
                TempData.Keep("DataSourceDetallado");
            }
        
            return View(model);
        }
        public ActionResult CreditoRecuperadoPartial()
        {
            var model = ((List<CreditoRecuperadoDTO>)TempData["DataSourceDetallado"]);
            TempData.Keep("DataSourceDetallado");  
            return PartialView("CreditoRecuperadoPartial", model);
        }

        public ActionResult DetalleCreditoRecuperado(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ViewData["CustomerID"] = id;
            var Detalles = ((List<CreditoRecuperadoDTO>)TempData["DataSourceDetallado"]).SingleOrDefault(x => x.Id.Equals(id.ToString())).Abonos;
            TempData.Keep("DataSourceDetallado");
            return PartialView("DetalleCreditoRecuperado", Detalles);
        }

        public ActionResult InventarioXConcepto(InventarioXConceptoModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFinal.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.InventarioXConcepto;
                TempData["DataSource"] = ReporteServicio.BuscarInventarioConcepto(model, tkn);
            }
            return View(model);
        }
        
        public ActionResult HistoricoVsVentas(HistoricoVentasConsulta model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (TempData["year"] != null) model.Years = (List<YearsDTO>)TempData["year"];
            if (model.Years == null) TempData["year"] = model.Years = HistoricoServicio.GetYears(tkn);
            if (model != null && model.Years.Exists(x => x.Seleccionar))
            {
                ViewData["Reporte"] = TiposReporteConst.HistoricoVsVentas;
                TempData["DataSource"] = ReporteServicio.BuscarHistoricoVSVentas(model, tkn);
                TempData["json"] = HistoricoServicio.GetJson(model, tkn);
            }
            return View(model);
        }
        public ActionResult CorteCaja(CorteCajaModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.Fecha.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.CorteCaja;
                TempData["DataSource"] = ReporteServicio.BuscarCorteCaja(model, tkn);
            }
            return View(model);
        }
        public ActionResult GastoVehicular(GastoVehiculoModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.GastoVehicular;
                TempData["DataSource"] = ReporteServicio.BuscarGastoVehicular(model, tkn);
            }
            return View(model);
        }
        public ActionResult Comisiones(PeriodoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.Comision;
                TempData["DataSource"] = ReporteServicio.CalcularComisiones(model, tkn);
            }
            return View(model);
        }
        public ActionResult CuentaConsolidada(CuentasPorPagarModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.Periodo.Equals(DateTime.MinValue))
            {
                ViewData["Periodo"] = model.Periodo;
                ViewData["Reporte"] = TiposReporteConst.CuentaConsolidada;
                TempData["DataSource"] = ReporteServicio.CuentasConsolidadas(model, tkn);
            }
            return View(model);
        }

        public static GridViewSettings CreateGeneralDetailGridSettings(string _id)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "CreditoRecuperadoDetalle_" + _id;
            settings.SettingsDetail.MasterGridName = "CreditoRecuperado";
            settings.CallbackRouteValues = new { Controller = "Reportes", Action = "DetalleCreditoRecuperado", id = _id };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "Id";
            settings.Columns.Add("FechaAbono");
            settings.Columns.Add("FechaCarga");
            settings.Columns.Add("FormaDePago");
            settings.Columns.Add("Importe");
            settings.Columns.Add("Nota");


            settings.Settings.ShowFooter = true;
            settings.Settings.ShowFilterRow = true;


            return settings;
        }

        public ActionResult CreditoOtorgado(PeriodoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();            
            TempData.Remove("DataSourceDetallado");
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.CreditoOtorgado;
                TempData["DataSourceDetallado"] = TempData["DataSourceDetallado"] ?? ReporteServicio.BuscarCreditoOtorgadoClientes(model, tkn);
                TempData.Keep("DataSourceDetallado");
            }
           
            return View(model);
        }
        public ActionResult CreditoOtorgadoPartial()
        {
            var model = ((List<CreditoOtorgadoModel>)TempData["DataSourceDetallado"]);
            TempData.Keep("DataSourceDetallado");
            return PartialView("CreditoOtorgadoPartial", model);
        }

        public ActionResult DetalleCreditoOtorgado(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ViewData["CustomerID"] = id;
            var Detalles = ((List<CreditoOtorgadoModel>)TempData["DataSourceDetallado"]).SingleOrDefault(x => x.Id.Equals(id.ToString())).Abonos;
            TempData.Keep("DataSourceDetallado");
            return PartialView("DetalleCreditoOtorgado", Detalles);
        }
        public static GridViewSettings CreateGeneralDetailGridSettingsCreditoOtorgado(string _id)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "CreditoOtorgadoDetalle_" + _id;
            settings.SettingsDetail.MasterGridName = "CreditoOtorgado";
            settings.CallbackRouteValues = new { Controller = "Reportes", Action = "DetalleCreditoOtorgado", id = _id };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "Id";      
            settings.Columns.Add("FechaCarga");
            settings.Columns.Add("Importe");
            settings.Columns.Add("Litros");
            settings.Columns.Add("Nota");
            settings.Columns.Add("Vendedor");


            settings.Settings.ShowFooter = true;
            settings.Settings.ShowFilterRow = true;


            return settings;
        }



        public ActionResult CreditoXCliente(PeriodoDTO model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            TempData.Remove("DataSourceDetallado");
            var Clte = ReporteServicio.BuscarCreditoXCliente(model, tkn);
            if (model != null && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.CreditoXCliente;
                TempData["DataSourceDetallado"] = TempData["DataSourceDetallado"] ?? ReporteServicio.BuscarCreditoXCliente(model, tkn);               
                TempData.Keep("DataSourceDetallado");
                
               
            }
         
         
            return View(model);
        }
        public ActionResult CreditoXClientePartial()
        {
            var model = ((List<CreditoXCliente>)TempData["DataSourceDetallado"]);
            TempData.Keep("DataSourceDetallado");
            return PartialView("CreditoXClientePartial", model);
        }

        public ActionResult DetalleCreditoXCliente(int id)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ViewData["CustomerID"] = id;
            var Detalles = ((List<CreditoXCliente>)TempData["DataSourceDetallado"]).SingleOrDefault(x => x.Id.Equals(id.ToString())).CargosDetallados;
            TempData.Keep("DataSourceDetallado");
            return PartialView("DetalleCreditoXCliente", Detalles);
        }
        public static GridViewSettings CreateGeneralDetailGridSettingsCreditoXCliente(string _id)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "CreditoXClienteDetalle_" + _id;
            settings.SettingsDetail.MasterGridName = "CreditoXCliente";
            settings.CallbackRouteValues = new { Controller = "Reportes", Action = "DetalleCreditoXCliente", id = _id };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.KeyFieldName = "Id";
            settings.Columns.Add("FechaRegistro");
            settings.Columns.Add("FechaVencimiento");
            settings.Columns.Add("Ticket");
            settings.Columns.Add("Serie");
            settings.Columns.Add("SaldoActual");
            settings.Columns.Add("SaldoCorriente");
            settings.Columns.Add("SaldoVencido");
            settings.Columns.Add("Dias1a7", "Dias 1 a 7");
            settings.Columns.Add("Dias8a16", "Dias 8 a 16");
            settings.Columns.Add("Dias17a31", "Dias 17 a 31");
            settings.Columns.Add("Dias32a61", "Dias 32 a 61");
            settings.Columns.Add("Dias62a91", "Dias 62 a 91");
            settings.Columns.Add("Mas91", "Mas 91");


            settings.Settings.ShowFooter = true;
            settings.Settings.ShowFilterRow = true;


            return settings;
        }

        public ActionResult CreditoXClienteMensual(PeriodoDTO model = null)
        {
            string FechaI = "01/01/219";
            string FechaF = "27/11/2019";
            model.FechaInicio = Convert.ToDateTime(FechaI);
            model.FechaFin = Convert.ToDateTime(FechaF);
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (TempData["DataSource"] != null)
                TempData["DataSource"] = null;
            if (model != null && !model.FechaFin.Equals(DateTime.MinValue) && !model.FechaInicio.Equals(DateTime.MinValue))
            {
                ViewData["Reporte"] = TiposReporteConst.CreditoXClienteMensual;
                TempData["DataSource"] = ReporteServicio.BuscarCreditoXClienteMensual(model, tkn);
            }
            return View(model);
        }


        //Cubo de inforamcion
        public ActionResult GetGridView(string Tipo)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            if (Tipo != null)
            {
                ViewData["Reporte"] = Tipo;
                if (Tipo.Equals(TiposReporteConst.CuentasXCobrar))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<CuentasPorPagarDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.InventarioPorPuntoVenta))
                    return View(TiposReporteConst.CuboInvXPunVen, (List<InventarioPorPuntoVentaDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.HistoricoPrecioVenta))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<HistoricoPrecioVentaDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.CallCenter))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<CallCenterDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.Requisicion))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<RequisicionRepDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.OrdenCompra))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<OrdenCompraRepDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.RendimientoVehicular))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<RendimientoVehicularDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.InventarioXConcepto))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<InventarioXConceptoDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.HistoricoVsVentas))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<YearsDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.CorteCaja))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<CorteCajaDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.GastoVehicular))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<GastoVehiculoDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.Comision))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<ComisionDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.CuentaConsolidada))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<CuentaConsolidadaDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.Ventas))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<VentasDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.RendimientoVehicularCamioneta))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<RendimientoCamionetaDTO>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.RendimientoVehicularPipas))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<RendimientoVehicularPipasModel>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.DescuentosXClientes))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<DescuentosXClientes>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.AutoConsumos))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<AutoConsumoModel>)TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.CreditoRecuperado))
                    return View(TiposReporteConst._CreditoRecuperadoPartial, (List<CreditoRecuperadoDTO>)TempData["DataSourceDetallado"]);
                if (Tipo.Equals(TiposReporteConst.CreditoOtorgado))
                    return View(TiposReporteConst._CreditoOtorgadoPartial, (List<CreditoOtorgadoModel>)TempData["DataSourceDetallado"]);
                if (Tipo.Equals(TiposReporteConst.CreditoXCliente))
                    return View(TiposReporteConst._CreditoXClientePartial, (List<CreditoXCliente>)TempData["DataSourceDetallado"]);
                if (Tipo.Equals(TiposReporteConst.VentasXPuntoVenta))
                    return View(TiposReporteConst.CuboInformacionGeneral, TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.EquipoDeTransporte))
                    return View(TiposReporteConst.CuboInformacionGeneral, TempData["DataSource"]);
                if (Tipo.Equals(TiposReporteConst.CreditoXClienteMensual))
                    return View(TiposReporteConst.CuboInformacionGeneral, (List<CreditoXClienteMensualModel>)TempData["DataSource"]);

                
                return View(TiposReporteConst.CuboInformacionGeneral);
            }
            return View(TiposReporteConst.CuboInformacionGeneral, new List<string>());
        }
    }
}