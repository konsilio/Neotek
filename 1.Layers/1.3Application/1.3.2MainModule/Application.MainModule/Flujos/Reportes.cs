using Application.MainModule.AdaptadoresDTO;
using Application.MainModule.AdaptadoresDTO.Almacenes;
using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.AdaptadoresDTO.EquipoTrasnporteServicio;
using Application.MainModule.AdaptadoresDTO.IngresoEgreso;
using Application.MainModule.AdaptadoresDTO.Pedidos;
using Application.MainModule.AdaptadoresDTO.Ventas;
using Application.MainModule.DTOs;
using Application.MainModule.Servicios;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Cobranza;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios.Equipo;
using Application.MainModule.Servicios.IngresoGasto;
using Application.MainModule.Servicios.Pedidos;
using Application.MainModule.Servicios.Requisiciones;
using Application.MainModule.Servicios.Seguridad;
using Application.MainModule.Servicios.Ventas;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Constantes;
using Sagas.MainModule.ObjetosValor.Enum;
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
            var resp = PermisosServicio.PuedeConsultarCuentaContable();
            if (!resp.Exito) return null;
            var requi = EgresoServicio.BuscarTodos(periodo);
            return EgresoAdapter.ToRepo(requi);
        }
        public List<RepInventarioPorPuntoVentaDTO> RepInventarioPorPuntoVenta(InventarioPorPuntoVentaDTO dto)
        {
            var resp = PermisosServicio.PuedeConsultarPuntoVenta();
            if (!resp.Exito) return null;
            var pipas = PipaServicio.Obtener(dto.Pipas);
            var estaciones = EstacionCarburacionServicio.Obtener(dto.Estaciones);
            var camionetas = CamionetaServicio.Obtener(dto.Camionetas);
            return new Almacenes().BuscarInvetarioPorPuntoDeVenta(camionetas, pipas, estaciones, dto.Fecha);
        }
        public List<RepHistorioPrecioDTO> RepHistorioPrecios(HistoricoPrecioDTO dto)
        {
            var resp = PermisosServicio.PuedeConsultarPrecioVentaGas();
            if (!resp.Exito) return null;
            var precios = PrecioVentaGasServicio.ObtenerListaPreciosVentaIdEmp(TokenServicio.ObtenerIdEmpresa(), dto.FechaInicial, dto.FechaFinal);
            return PrecioVentaGasAdapter.ToRepo(precios, dto);
        }
        public List<RepCallCenterDTO> RepCallCenter(PeriodoDTO dto)
        {
            var resp = PermisosServicio.PuedeConsultarPedido();
            if (!resp.Exito) return null;
            dto.FechaInicio = DateTime.Parse(string.Concat(dto.FechaInicio.ToShortDateString(), " 00:00:00"));
            dto.FechaFin = DateTime.Parse(string.Concat(dto.FechaFin.ToShortDateString(), " 23:59:59"));
            var pedidos = PedidosServicio.Obtener(TokenServicio.ObtenerIdEmpresa(), dto);
            return PedidosAdapter.FromDTO(pedidos);
        }
        public List<RepRequisicionDTO> RepRequisicion(RequisicionModelDTO dto)
        {
            var resp = PermisosServicio.PuedeGenerarRequisicion();
            if (!resp.Exito) return null;
            var requisicones = RequisicionServicio.BuscarRequisicionPorPeriodo(TokenServicio.ObtenerIdEmpresa(), dto.FechaInicio, dto.FechaFinal);
            return RequisicionServicio.ConvertirReporte(requisicones);
        }
        public List<RepOrdenCompraDTO> RepOrdenCompra(OrdenCompraModelDTO dto)
        {
            var resp = PermisosServicio.PuedeRegistrarOrdenCompra();
            if (!resp.Exito) return null;
            var ordenes = OrdenCompraServicio.BuscarTodo(TokenServicio.ObtenerIdEmpresa(), dto.FechaInicio, dto.FechaFinal);
            return OrdenComprasAdapter.ToRepDTO(ordenes);
        }
        public List<RepRendimientoVehicularDTO> RepRendimientoVehicular(RendimientoVehicularDTO dto)
        {
            var resp = PermisosServicio.PuedeRegistrarParqueVehicular();
            if (!resp.Exito) return null;
            var recargas = RecargaCombustibleServicio.Buscar(dto.FechaInicio, dto.FechaFinal);
            return RecargaCombustibleAdapter.FormRepDTO(recargas);
        }
        public List<RepInventarioXConceptorDTO> RepInventarioPorConcepto(InventarioXConceptoDTO dto)
        {
            var resp = PermisosServicio.PuedeRegistrarParqueVehicular();
            if (!resp.Exito) return null;
            var alamacenes = ProductoAlmacenServicio.BuscarAlmacen(dto.FechaInicio, dto.FechaFinal);
            return AlmacenProductoAdapter.ToRepDTO(alamacenes);
        }
        public List<RepCorteCajaDTO> RepCorteCaja(CajaGralDTO dto)
        {
            var resp = PermisosServicio.PuedeModificarCajaGeneral();
            if (!resp.Exito) return null;
            var Estaciones = EstacionCarburacionServicio.ObtenerTodas();
            var VEstaciones = CajaGeneralServicio.ObtenerTotalVentasEstaciones(dto.Fecha) ?? new List<VentaPuntoDeVenta>();
            var VPipas = CajaGeneralServicio.ObtenerTotalVentasPipas(dto.Fecha) ?? new List<VentaPuntoDeVenta>();
            var VCilindros = CajaGeneralServicio.ObtenerTotalVentasCamioneta(dto.Fecha) ?? new List<VentaPuntoDeVenta>();
            var VFacturasCredito = CobranzaServicio.Obtener(dto.Fecha) ?? new List<Abono>();
            var VBonificaciones = CajaGeneralServicio.ObtenerTotalBonificaciones(dto.Fecha) ?? new List<VentaPuntoDeVenta>();

            List<RepCorteCajaDTO> respuesta = new List<RepCorteCajaDTO>();
            respuesta.AddRange(CajaGeneralAdapter.ToRepoCorteCajaEstaciones(VEstaciones, Estaciones));
            respuesta.Add(CajaGeneralAdapter.ToRepoCorteCajaCamionetas(VCilindros));
            respuesta.Add(CajaGeneralAdapter.ToRepoCorteCajaPipas(VPipas));
            respuesta.Add(CajaGeneralAdapter.ToRepoCorteCajaCredito(VFacturasCredito));
            respuesta.Add(CajaGeneralAdapter.ToRepoCorteCajaBonificaciones(VBonificaciones));
            return respuesta;
        }
        public List<RepGastoVehicularDTO> RepGastoXVehiculo(GastoVehicularDTO dto)
        {
            var resp = PermisosServicio.PuedeConsultarParqueVehicular();
            if (!resp.Exito) return null;
            var Pipas = PipaServicio.Obtener();
            var Camionetas = CamionetaServicio.Obtener();
            var Utilitarios = VehiculoUtilitarioServicio.Obtener();
            var RecCombustible = RecargaCombustibleServicio.Buscar(dto.FechaInicio, dto.FechaFin);
            List<RepGastoVehicularDTO> respuesta = new List<RepGastoVehicularDTO>();
            respuesta.AddRange(TransporteAdapter.ToRepoPipas(Pipas, RecCombustible, dto));
            respuesta.AddRange(TransporteAdapter.ToRepoCamionetas(Camionetas, RecCombustible, dto));
            respuesta.AddRange(TransporteAdapter.ToRepoUtilitario(Utilitarios, RecCombustible, dto));

            return respuesta.Where(x => !x.Total.Equals(0)).ToList();
        }
        public List<ComisionDTO> RepComisiones(PeriodoDTO periodo)
        {
            List<ComisionDTO> respesta = new List<ComisionDTO>();
            var choferes = OperadorChoferServicio.ObtenerPorEmpresa(TokenServicio.ObtenerIdEmpresa());
            periodo.FechaInicio = DateTime.Parse(string.Concat(periodo.FechaInicio.ToShortDateString(), " 00:00:00"));
            periodo.FechaFin = DateTime.Parse(string.Concat(periodo.FechaFin.ToShortDateString(), " 23:59:59"));
            var ventas = PuntoVentaServicio.ObtenerVentas(periodo.FechaInicio, periodo.FechaFin);
            foreach (var chofer in choferes)
            {
                ComisionDTO dto = new ComisionDTO();
                dto.FechaFin = periodo.FechaFin;
                dto.FechaInicio = periodo.FechaInicio;
                dto.Empleado = string.Concat(chofer.Usuario.Nombre, " ", chofer.Usuario.Apellido1);
                dto.Puesto = chofer.TipoOperadorChofer.Descripcion;
                dto.Venta = 0;
                dto.Comision = 0;
                dto.Total = 0;

                if (chofer.PuntosVenta.Count > 0)
                {
                    dto.PuntoVenta = chofer.PuntosVenta.FirstOrDefault().UnidadesAlmacen.Numero;
                    if (chofer.PuntosVenta.FirstOrDefault().UnidadesAlmacen.IdCamioneta != null)
                    {
                        dto.Venta = ventas.Where(x => x.IdOperadorChofer.Equals(chofer.IdOperadorChofer)).Sum(y => y.VentaPuntoDeVentaDetalle.Sum(v => v.CantidadKg.Value));
                        dto.Comision = (decimal)0.4;
                        dto.Unidad = "Kg";
                        dto.Total = CalcularPreciosVentaServicio.CalcularComisionCamioneta(ventas.Where(x => x.IdOperadorChofer.Equals(chofer.IdOperadorChofer)).ToList(), periodo);
                    }
                    if (chofer.PuntosVenta.FirstOrDefault().UnidadesAlmacen.IdPipa != null)
                    {
                        dto.Venta = ventas.Where(x => x.IdOperadorChofer.Equals(chofer.IdOperadorChofer)).Sum(y => y.VentaPuntoDeVentaDetalle.Sum(v => v.CantidadLt.Value));
                        dto.Comision = (decimal)0.15;
                        dto.Unidad = "Lts";
                        dto.Total = CalcularPreciosVentaServicio.CalcularComisionPipas(ventas.Where(x => x.IdOperadorChofer.Equals(chofer.IdOperadorChofer)).ToList(), periodo);
                    }
                    if (!chofer.PuntosVenta.FirstOrDefault().UnidadesAlmacen.EsGeneral && chofer.PuntosVenta.FirstOrDefault().UnidadesAlmacen.IdEstacionCarburacion == null)
                        respesta.Add(dto);

                }

            }
            return respesta;
        }
        public List<CuentasConsolidadasDTO> RepCuentasConsolidadas(DateTime periodo)
        {
            List<CuentasConsolidadasDTO> respuesta = new List<CuentasConsolidadasDTO>();
            var gastos = EgresoServicio.BuscarTodos(periodo);
            var cuentasContables = CuentaContableServicio.Obtener();

            foreach (var cc in cuentasContables)
            {
                var cca = CuentaContableAutorizadoServicio.Obtener(cc.IdCuentaContable, periodo);
                if (cca == null)
                    cca = CuentaContableAutorizadoServicio.RegistrarCuentaContableAutorizado(cc.IdCuentaContable);
                CuentasConsolidadasDTO dto = new CuentasConsolidadasDTO();
                dto.Concepto = cc.Descripcion;
                dto.CantidadAutorizada = cca == null ? 0 : cca.Autorizado;
                dto.CantidadGastada = gastos == null ? 0 : gastos.Where(x => x.IdCuentaContable.Equals(cc.IdCuentaContable)).Sum(y => y.Monto);
                dto.Diferencia = dto.CantidadAutorizada - dto.CantidadGastada;
                respuesta.Add(dto);
            }
            return respuesta;
        }

        #region Dash Board (Pruebas)
        //public AdministracionDTO DashAdministracionVentaVSRema()
        //{
        //    AdministracionDTO dto = new AdministracionDTO();

        //    var remaDTO = AlmacenGasServicio.BusquedaGeneralPeriodoActual();
        //    var remanente = new Almacenes().ConsultarRemanenteGeneral(remaDTO);

        //    var Estaciones = EstacionCarburacionServicio.ObtenerTodas();
        //    var VEstaciones = CajaGeneralServicio.ObtenerTotalVentasEstaciones(new DateTime(2018, 12, 31)) ?? new List<VentaPuntoDeVenta>();
        //    var VPipas = CajaGeneralServicio.ObtenerTotalVentasPipas(new DateTime(2018, 12, 31)) ?? new List<VentaPuntoDeVenta>();
        //    var VCilindros = CajaGeneralServicio.ObtenerTotalVentasCamioneta(new DateTime(2018, 12, 31)) ?? new List<VentaPuntoDeVenta>();

        //    dto.TotalEstaciones = (decimal)CajaGeneralAdapter.ToRepoCorteCajaEstaciones(VEstaciones, Estaciones).Sum(x => x.Cantidad);
        //    dto.TotalCamionetas = (decimal)CajaGeneralAdapter.ToRepoCorteCajaCamionetas(VCilindros).Cantidad;
        //    dto.TotalPipas = (decimal)CajaGeneralAdapter.ToRepoCorteCajaPipas(VPipas).Cantidad;
        //    dto.TotalVetna = dto.TotalEstaciones + dto.TotalCamionetas + dto.TotalPipas;
        //    dto.Json = JsonServicio.JsonGeneralRemanente(remanente);

        //    return dto;
        //}
        //public string DashCallCenter()
        //{
        //    var Pedidos = PedidosServicio.Obtener(TokenServicio.ObtenerIdEmpresa(), new DateTime(2019, 2, 28));
        //    var Dash = PedidosServicio.GenerarListDash(Pedidos, new DateTime(2019, 2, 28));
        //    return JsonServicio.JsonCallCenter(Dash);
        //}
        //public AndenDTO DashAnden()
        //{
        //    var oc = OrdenCompraServicio.BuscarUltimaOCGas(TokenServicio.ObtenerIdEmpresa());
        //    var alm = AlmacenGasServicio.ObtenerAlmacenPrincipal(TokenServicio.ObtenerIdEmpresa());
        //    var respuesta = new AndenDTO()
        //    {
        //        TotalProduto = AlmacenGasServicio.ObtenerAlmacenGeneral(TokenServicio.ObtenerIdEmpresa()).Sum(x => x.CantidadActualKg),
        //        NivelAlmacen = Convert.ToInt32(alm.PorcentajeActual),
        //        KilosAlmacen = alm.CantidadActualKg,
        //        OrdenCompra = oc == null ? OrdenCompraConst.SinOCProxima : oc.NumOrdenCompra,
        //        Ventas = PuntoVentaServicio.ObtenerVentasTOPDTO(5, new DateTime(2018, 12, 11))
        //    };
        //    return respuesta;
        //}
        //public CarteraDTO DashCartera()
        //{
        //    var CarterRec = CobranzaServicio.CRecuperada(TokenServicio.ObtenerIdEmpresa());
        //    var CarterVencida = CobranzaServicio.CarteraVencida(TokenServicio.ObtenerIdEmpresa());
        //    var DeudoresLongevos = CobranzaServicio.TopDeudores(TokenServicio.ObtenerIdEmpresa(), 5);
        //    var DeudoresValioso = CobranzaServicio.TopDeudoresValiosos(TokenServicio.ObtenerIdEmpresa(), 5);
        //    var respuesta = new CarteraDTO()
        //    {
        //        CarteraRecuperada = CarterRec,
        //        CarteraVencida = CarterVencida,
        //        TopDeudoresL = DeudoresLongevos,
        //        TopDeudoresV = DeudoresValioso,
        //    };
        //    return respuesta;
        //}
        //public AdministracionDTO DashCaja()
        //{
        //    AdministracionDTO dto = new AdministracionDTO();

        //    var Estaciones = EstacionCarburacionServicio.ObtenerTodas();
        //    var VEstaciones = CajaGeneralServicio.ObtenerTotalVentasEstaciones(new DateTime(2018, 12, 31)) ?? new List<VentaPuntoDeVenta>();
        //    var VPipas = CajaGeneralServicio.ObtenerTotalVentasPipas(new DateTime(2018, 12, 31)) ?? new List<VentaPuntoDeVenta>();
        //    var VCilindros = CajaGeneralServicio.ObtenerTotalVentasCamioneta(new DateTime(2018, 12, 31)) ?? new List<VentaPuntoDeVenta>();

        //    dto.TotalEstaciones = (decimal)CajaGeneralAdapter.ToRepoCorteCajaEstaciones(VEstaciones, Estaciones).Sum(x => x.TotalVenta);
        //    dto.TotalCamionetas = (decimal)CajaGeneralAdapter.ToRepoCorteCajaCamionetas(VCilindros).TotalVenta;
        //    dto.TotalPipas = (decimal)CajaGeneralAdapter.ToRepoCorteCajaPipas(VPipas).TotalVenta;
        //    dto.TotalVetna = dto.TotalEstaciones + dto.TotalCamionetas + dto.TotalPipas;

        //    return dto;
        //}
        #endregion
        #region Dash Board (Produccion)
        public AdministracionDTO DashAdministracionVentaVSRema()
        {
            AdministracionDTO dto = new AdministracionDTO();
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());
            var remaDTO = AlmacenGasServicio.BusquedaGeneralPeriodoActual();
            var remanente = new Almacenes().ConsultarRemanenteGeneral(remaDTO);

            var Estaciones = EstacionCarburacionServicio.ObtenerTodas();
            var VEstaciones = CajaGeneralServicio.ObtenerTotalVentasEstaciones(DateTime.Now) ?? new List<VentaPuntoDeVenta>();
            var VPipas = CajaGeneralServicio.ObtenerTotalVentasPipas(DateTime.Now) ?? new List<VentaPuntoDeVenta>();
            var VCilindros = CajaGeneralServicio.ObtenerTotalVentasCamioneta(DateTime.Now) ?? new List<VentaPuntoDeVenta>();

            dto.TotalEstaciones = (decimal)CajaGeneralAdapter.ToRepoCorteCajaEstaciones(VEstaciones, Estaciones).Sum(x => x.Cantidad);
            dto.TotalCamionetas = (decimal)CajaGeneralAdapter.ToRepoCorteCajaCamionetas(VCilindros).Cantidad;
            dto.TotalPipas = (decimal)CajaGeneralAdapter.ToRepoCorteCajaPipas(VPipas).Cantidad * empresa.FactorLitrosAKilos;
            dto.TotalVetna = dto.TotalEstaciones + dto.TotalCamionetas + dto.TotalPipas;
            dto.Json = JsonServicio.JsonGeneralRemanente(remanente);

            return dto;
        }
        public string DashCallCenter()
        {
            var Pedidos = PedidosServicio.Obtener(TokenServicio.ObtenerIdEmpresa(), DateTime.Now);
            var Dash = PedidosServicio.GenerarListDash(Pedidos, DateTime.Now);
            return JsonServicio.JsonCallCenter(Dash);
        }
        public AndenDTO DashAnden()
        {
            var oc = OrdenCompraServicio.BuscarUltimaOCGas(TokenServicio.ObtenerIdEmpresa());
            var alm = AlmacenGasServicio.ObtenerAlmacenPrincipal(TokenServicio.ObtenerIdEmpresa());
            var respuesta = new AndenDTO()
            {
                TotalProduto = AlmacenGasServicio.ObtenerAlmacenGeneral(TokenServicio.ObtenerIdEmpresa()).Sum(x => x.CantidadActualKg),
                NivelAlmacen = Convert.ToInt32(alm.PorcentajeActual),
                KilosAlmacen = alm.CantidadActualKg,
                OrdenCompra = oc == null ? OrdenCompraConst.SinOCProxima : oc.NumOrdenCompra,
                Ventas = PuntoVentaServicio.ObtenerVentasTOPDTO(5, DateTime.Now)
            };
            return respuesta;
        }
        public CarteraDTO DashCartera()
        {
            var CarterRec = CobranzaServicio.CRecuperada(TokenServicio.ObtenerIdEmpresa());
            var CarterVencida = CobranzaServicio.CarteraVencida(TokenServicio.ObtenerIdEmpresa());
            var DeudoresLongevos = CobranzaServicio.TopDeudores(TokenServicio.ObtenerIdEmpresa(), 5);
            var DeudoresValioso = CobranzaServicio.TopDeudoresValiosos(TokenServicio.ObtenerIdEmpresa(), 5);
            var respuesta = new CarteraDTO()
            {
                CarteraRecuperada = CarterRec,
                CarteraVencida = CarterVencida,
                TopDeudoresL = DeudoresLongevos,
                TopDeudoresV = DeudoresValioso,
            };
            return respuesta;
        }
        public AdministracionDTO DashCaja()
        {
            AdministracionDTO dto = new AdministracionDTO();

            var Estaciones = EstacionCarburacionServicio.ObtenerTodas();
            var VEstaciones = CajaGeneralServicio.ObtenerTotalVentasEstaciones(DateTime.Now) ?? new List<VentaPuntoDeVenta>();
            var VPipas = CajaGeneralServicio.ObtenerTotalVentasPipas(DateTime.Now) ?? new List<VentaPuntoDeVenta>();
            var VCilindros = CajaGeneralServicio.ObtenerTotalVentasCamioneta(DateTime.Now) ?? new List<VentaPuntoDeVenta>();

            dto.TotalEstaciones = (decimal)CajaGeneralAdapter.ToRepoCorteCajaEstaciones(VEstaciones, Estaciones).Sum(x => x.TotalVenta);
            dto.TotalCamionetas = (decimal)CajaGeneralAdapter.ToRepoCorteCajaCamionetas(VCilindros).TotalVenta;
            dto.TotalPipas = (decimal)CajaGeneralAdapter.ToRepoCorteCajaPipas(VPipas).TotalVenta;
            dto.TotalVetna = dto.TotalEstaciones + dto.TotalCamionetas + dto.TotalPipas;

            return dto;
        }
        #endregion
    }
}
