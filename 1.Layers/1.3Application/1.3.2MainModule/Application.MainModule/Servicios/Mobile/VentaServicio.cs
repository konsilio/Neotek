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
        public static RespuestaDto Anticipo(AnticipoDto dto,short idEmpresa,Usuario usuario, List<VentaCorteAnticipoEC> anticipos,UnidadAlmacenGas estacion)
        {
            var idOrden = orden(anticipos);
            
            var puntos = PuntoVentaServicio.ObtenerIdEmp(idEmpresa);
            var estaciones = EstacionCarburacionServicio.ObtenerTodas(idEmpresa);
            var estacionVenta = estaciones.Find(x => x.IdEstacionCarburacion.Equals(dto.IdCAlmacenGas));
            var almacenGas = AlmacenGasServicio.ObtenerAlmacenes(idEmpresa);
            var almacen = almacenGas.Find(x => x.IdEstacionCarburacion.Equals(estacionVenta.IdEstacionCarburacion));
            var PuntoVenta = puntos.Find(x =>x.IdCAlmacenGas.Equals(almacen.IdCAlmacenGas));
            //var almacen = AlmacenGasServicio.Obtener(PuntoVenta.IdCAlmacenGas);

            var adapter = AnticiposCortesAdapter.FromDto(dto, idEmpresa, usuario.IdUsuario, PuntoVenta);

            var entrega = PuntoVenta.OperadorChofer.Usuario;
            var operador = PuntoVenta.OperadorChofer;
            adapter.IdCAlmacenGas = almacen.IdCAlmacenGas;
            adapter.Orden = (short)idOrden;
            adapter.FechaAplicacion = dto.Fecha;
            adapter.Dia = (byte) dto.Fecha.Day;
            adapter.Mes = (byte)dto.Fecha.Month;
            adapter.Year = (short)dto.Fecha.Year;
            adapter.FechaRegistro = DateTime.Now;
            adapter.FechaAplicacion = dto.Fecha;
            adapter.FechaCorteAnticipo = dto.Fecha;
            adapter.DatosProcesados = false;
            adapter.TipoOperacion = "Anticipo";
            adapter.IdTipoOperacion = 1;
            adapter.PuntoVenta = PuntoVenta.UnidadesAlmacen.Numero;
            adapter.OperadorChofer = operador.Usuario.Nombre + " " + operador.Usuario.Apellido1 + " " + operador.Usuario.Apellido2;
            var anticipo = GasServicio.Anticipo(adapter);

            /*if (anticipo.Exito)
            {
                var deContado = PuntoVentaServicio.ObtenerVentasContado(PuntoVenta.IdPuntoVenta, dto.Fecha);
                var credito = PuntoVentaServicio.ObtenerVentasCredito(PuntoVenta.IdPuntoVenta, dto.Fecha);
                var corteCajaGeneral = AnticiposCortesAdapter.FromDTO(dto, idEmpresa, usuario, PuntoVenta, operador, entrega,deContado,credito);
                return PuntoVentaServicio.InsertMobil(corteCajaGeneral);
            }
                
            */
            return anticipo;
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
        public static RespuestaDto Corte(CorteDto dto, short idEmpresa, int idUsuario,List<VentaCorteAnticipoEC> cortes,PuntoVenta puntoVenta,UnidadAlmacenGas almacen)
        {
            var idOrden = orden(cortes);
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
            adapter.FechaCorteAnticipo = dto.Fecha;
            adapter.DatosProcesados = false;
            adapter.TipoOperacion = "Corte caja";
            adapter.IdTipoOperacion = 2;
            adapter.PuntoVenta = almacen.Numero;
            adapter.FechaAplicacion = dto.Fecha;
            adapter.OperadorChofer = puntoVenta.OperadorChofer.Usuario.Nombre + "" + puntoVenta.OperadorChofer.Usuario.Apellido1 + puntoVenta.OperadorChofer.Usuario.Apellido2;
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
