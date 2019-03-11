using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Seguridad;

namespace Application.MainModule.Servicios.Mobile
{
    public class VentaServicio
    {
        public static RespuestaDto BuscarFolioVenta(VentaDTO Venta)
        {
            return GasServicio.EvaluarClaveOperacion(Venta);
        }
        public static RespuestaDto EvaluarClaveOperacion(AnticipoDto dto)
        {
            return GasServicio.EvaluarClaveOperacion(dto);
        }
        public static RespuestaDto Anticipo(AnticipoDto dto,short idEmpresa,Usuario usuario, List<VentaCorteAnticipoEC> anticipos,UnidadAlmacenGas estacion,List<VentaCorteAnticipoEC> cortesYanticiposOrden)
        {
            var idOrden = orden(cortesYanticiposOrden);
            
            var puntos = PuntoVentaServicio.ObtenerIdEmp(idEmpresa);
            var estaciones = EstacionCarburacionServicio.ObtenerTodas(idEmpresa);
            var estacionVenta = estaciones.Find(x => x.IdEstacionCarburacion.Equals(dto.IdCAlmacenGas));
            var almacenGas = AlmacenGasServicio.ObtenerAlmacenes(idEmpresa);
            var almacen = almacenGas.Find(x => x.IdEstacionCarburacion.Equals(estacionVenta.IdEstacionCarburacion));
            var lpipas = AlmacenGasServicio.ObtenerPipasEmpresa(idEmpresa);
            var lestaciones = AlmacenGasServicio.ObtenerEstacionesEmpresa(idEmpresa);
            var lcamionetas = AlmacenGasServicio.ObtenerCamionetasEmpresa(idEmpresa);
            var pipa = lpipas.Find(x => x.IdPipa.Equals(dto.IdCAlmacenGas));
            var camioneta = lcamionetas.Find(x => x.IdCamioneta.Equals(dto.IdCAlmacenGas));
            var _estacion = lestaciones.Find(x => x.IdEstacionCarburacion.Equals(dto.IdCAlmacenGas));
            PuntoVenta punto = null;
            if (pipa != null)
            {
                punto = pipa.UnidadAlmacenGas.First().PuntosVenta.First();
            }else if (camioneta != null)
            {
                punto = camioneta.UnidadAlmacenGas.First().PuntosVenta.First();
            }else if (_estacion!=null)
            {
                punto = _estacion.UnidadAlmacenGas.First().PuntosVenta.First();
            }

            //var PuntoVenta = puntos.Find(x =>x.IdCAlmacenGas.Equals(almacen.IdCAlmacenGas));
            //var almacen = AlmacenGasServicio.Obtener(PuntoVenta.IdCAlmacenGas);

            var adapter = AnticiposCortesAdapter.FromDto(dto, idEmpresa, usuario.IdUsuario, punto);

            
            var entrega = usuario.Nombre+" "+usuario.Apellido1+""+usuario.Apellido2;
            var operador = punto.OperadorChofer.Usuario.Nombre+" "+ punto.OperadorChofer.Usuario.Apellido1+ punto.OperadorChofer.Usuario.Apellido2;
            adapter.IdCAlmacenGas = almacen.IdCAlmacenGas;
            adapter.Orden = (short)idOrden;
            adapter.FechaAplicacion = dto.Fecha;
            adapter.Dia = (byte) dto.Fecha.Day;
            adapter.Mes = (byte)dto.Fecha.Month;
            adapter.Year = (short)dto.Fecha.Year;
            adapter.FechaRegistro = DateTime.Now;
            adapter.FechaAplicacion = dto.Fecha;
            adapter.FechaCorteAnticipo = dto.FechaAnticipo;
            adapter.DatosProcesados = false;
            adapter.TipoOperacion = "Anticipo";
            adapter.IdTipoOperacion = 1;
            adapter.PuntoVenta = punto.UnidadesAlmacen.Numero;
            adapter.OperadorChofer = entrega;

            var anticipo = GasServicio.Anticipo(adapter);
            ////Insert en la tabla de VentaCajaGeneral
            if (anticipo.Exito)
            {
                var deContado = PuntoVentaServicio.ObtenerVentasContado(punto.IdPuntoVenta, dto.Fecha);
                var credito = PuntoVentaServicio.ObtenerVentasCredito(punto.IdPuntoVenta, dto.Fecha);
                var ventasCajasGral = PuntoVentaServicio.ObtenerVentasCajaGral();
                var corteCajaGeneral = AnticiposCortesAdapter.FromDTO(dto, idEmpresa, usuario, punto, punto.OperadorChofer, usuario, deContado, credito);

                corteCajaGeneral.Orden = (short)orden(ventasCajasGral);
                corteCajaGeneral.OtrasVentas = CalculoOtrasVentas(deContado, credito);
                return PuntoVentaServicio.InsertMobil(corteCajaGeneral);
            }
            ////Fin del Insert en la tabla de VentaCajaGeneral
            //return anticipo;
            return anticipo;
        }
        public static decimal CalculoOtrasVentas(List<VentaPuntoDeVenta> deContado, List<VentaPuntoDeVenta> credito)
        {
            decimal total = 0;
            if(deContado!=null && deContado.Count > 0) { 
                foreach (var item in deContado)
                {
                
                    var detalles = item.VentaPuntoDeVentaDetalle.Where(x => x.ProductoDescripcion.Equals("CILINDRO"));
                    if (detalles != null) { 
                        foreach (var detalle in detalles)
                        {
                            total += detalle.Subtotal;
                        }
                    }
                }
            }
            if(credito!=null && credito.Count > 0) { 
                foreach (var item in credito)
                {
                    var detalles = item.VentaPuntoDeVentaDetalle.Where(x => x.ProductoCategoria.Equals("CILINDRO"));
                    if (detalles != null) { 
                        foreach (var detalle in detalles)
                        {
                            total += detalle.Subtotal;
                        }
                    }
                }
            }
            return total;
        }
        public static int orden(List<VentaCajaGeneral> ventasCajasGral)
        {
            if (ventasCajasGral != null)
                if (ventasCajasGral.Count == 0)
                    return 1;
                else
                    return  ventasCajasGral.Last(x => x.Orden > 0).Orden + 1;
            else
                return 1;
        }
        public static int orden(List<VentaCorteAnticipoEC> anticipos)
        {
            if (anticipos != null)
                if (anticipos.Count > 0)
                    return anticipos.Count + 1;
                else
                    return 1;
            else
                return 1;
        }
        public static List<VentaCorteAnticipoEC> ObtenerAnticipos(short idEmpresa)
        {
            return GasServicio.ObtenerAnticipos(idEmpresa);
        }
        public static RespuestaDto EvaluarClaveOperacion(CorteDto dto)
        {
            return GasServicio.EvaluarClaveOperacion(dto);
        }
        public static List<VentaCorteAnticipoEC> ObtenerCortes(short idEmpresa)
        {
            return GasServicio.ObtenerCortes(idEmpresa);
        }
        public static RespuestaDto Corte(CorteDto dto, short idEmpresa, int idUsuario,List<VentaCorteAnticipoEC> cortes,PuntoVenta puntoVenta,UnidadAlmacenGas almacen,List<VentaCorteAnticipoEC> cortesYanticiposOrden)
        {
            var idOrden = orden(cortesYanticiposOrden);
            //var puntos = PuntoVentaServicio.ObtenerIdEmp(idEmpresa);
            //var PuntoVenta = puntos.Find(x => x.IdCAlmacenGas.Equals(dto.IdCAlmacenGas));

            var adapter = AnticiposCortesAdapter.FromDto(dto, idEmpresa, idUsuario, puntoVenta);

            adapter.IdCAlmacenGas = almacen.IdCAlmacenGas;
            adapter.Orden = (short)idOrden;
            adapter.FechaAplicacion = dto.Fecha;
            adapter.Dia = (byte)dto.Fecha.Day;
            adapter.Mes = (byte)dto.Fecha.Month;
            adapter.Year = (short)dto.Fecha.Year;
            adapter.FechaRegistro = DateTime.Now;
            adapter.FechaCorteAnticipo = dto.FechaCorte;
            adapter.DatosProcesados = false;
            adapter.TipoOperacion = "Corte caja";
            adapter.IdTipoOperacion = 2;
            adapter.PuntoVenta = almacen.Numero;
            adapter.FechaAplicacion = dto.Fecha;
            adapter.OperadorChofer = puntoVenta.OperadorChofer.Usuario.Nombre + " " + puntoVenta.OperadorChofer.Usuario.Apellido1 +" "+ puntoVenta.OperadorChofer.Usuario.Apellido2;
            return GasServicio.Corte(adapter);
        }
        public static int ObtenerIdCamioneta(int idUsuario)
        {
            var chofis = OperadorChoferServicio.ObtenerPorUsuario(idUsuario);
            var puntoVenta = PuntoVentaServicio.BuscarPorOperadorChofer(chofis.IdOperadorChofer);

            return puntoVenta.ToList().FirstOrDefault(x => x.IdOperadorChofer.Equals(chofis.IdOperadorChofer)).UnidadesAlmacen.IdCamioneta ?? 0;
        }
    }
}
