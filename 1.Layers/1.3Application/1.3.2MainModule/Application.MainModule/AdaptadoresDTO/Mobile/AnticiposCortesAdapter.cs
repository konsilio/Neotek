using System.Collections.Generic;
using System.Linq;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile;
using System;
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class AnticiposCortesAdapter
    {
        public static DatosAnticiposCorteDto ToDTO(List<EstacionCarburacion> estaciones, List<UnidadAlmacenGas> unidades)
        {
            return new DatosAnticiposCorteDto()
            {
                // estaciones = estaciones.Select(x => ToDTO(x, unidades)).ToList(),
            };
        }

        private static EstacionesDto ToDTO(EstacionCarburacion estacion, List<UnidadAlmacenGas> unidades)
        {
            var unidadEstacion = unidades.Single(x => x.IdEstacionCarburacion.Value.Equals(estacion.IdEstacionCarburacion));
            var lecturaInicial = unidadEstacion.TomasLectura.Where(
                x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)
                ).OrderBy(x => x.FechaRegistro).Last();
            return new EstacionesDto()
            {
                Medidor = TipoMedidorAdapter.ToDto(TipoMedidorGasServicio.Obtener(unidadEstacion.IdTipoMedidor.Value)),
                IdTipoMedidor = unidadEstacion.IdTipoMedidor.Value,
                IdAlmacenGas = (short)estacion.IdEstacionCarburacion,
                NombreAlmacen = estacion.Nombre,
                P5000Inicial = lecturaInicial.P5000.Value,
                P5000Final = unidadEstacion.P5000Actual.Value,
                AnticiposEstacion = ToDTO(unidadEstacion)
            };
        }
        public static EstacionesDto ToDTO(EstacionCarburacion estacion, UnidadAlmacenGas unidad)
        {

            var lecturaInicial = unidad.TomasLectura.Where(
                x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)
                ).OrderBy(x => x.FechaRegistro).Last();
            return new EstacionesDto()
            {
                Medidor = TipoMedidorAdapter.ToDto(TipoMedidorGasServicio.Obtener(unidad.IdTipoMedidor.Value)),
                IdTipoMedidor = unidad.IdTipoMedidor.Value,
                IdAlmacenGas = (short)estacion.IdEstacionCarburacion,
                NombreAlmacen = estacion.Nombre,
                P5000Inicial = lecturaInicial.P5000 != null ? lecturaInicial.P5000.Value : 0,
                P5000Final = unidad.P5000Actual != null ? unidad.P5000Actual.Value : 0,
                AnticiposEstacion = ToDTO(unidad)
            };
        }
        public static CamionetaDto ToDTO(Camioneta camioneta, UnidadAlmacenGas unidad)
        {

            var lecturaInicial = unidad.TomasLectura.Where(
                x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)
                ).OrderBy(x => x.FechaRegistro).Last();
            return new CamionetaDto()
            {
                Medidor = TipoMedidorAdapter.ToDto(TipoMedidorGasServicio.Obtener(unidad.IdTipoMedidor.Value)),
                IdTipoMedidor = unidad.IdTipoMedidor.Value,
                IdAlmacenGas = (short)camioneta.IdCamioneta,
                NombreAlmacen = camioneta.Nombre,
                //P5000Inicial = lecturaInicial.P5000.Value,
                //P5000Final = unidad.P5000Actual.Value,
                //AnticiposEstacion = ToDTO(unidad)
            };
        }
        public static PipaDto ToDTO(Pipa pipa, UnidadAlmacenGas unidad)
        {

            var lecturaInicial = unidad.TomasLectura.Where(
                x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)
                ).OrderBy(x => x.FechaRegistro).Last();
            return new PipaDto()
            {
                Medidor = TipoMedidorAdapter.ToDto(TipoMedidorGasServicio.Obtener(unidad.IdTipoMedidor ?? 0)),
                IdTipoMedidor = unidad.IdTipoMedidor ?? 0,
                IdAlmacenGas = (short)pipa.IdPipa,
                NombreAlmacen = pipa.Nombre,
                CantidadP5000 = lecturaInicial.P5000 ?? 0,
                P5000Final = unidad.P5000Actual ?? 0,
                AnticiposEstacion = ToDTO(unidad)
            };
        }
        public static AnticiposEstacionDTO ToDTO(UnidadAlmacenGas unidad)
        {
            var anticiposEstacion = PuntoVentaServicio.ObtenerAnticipos(unidad).FindAll(x => x.DatosProcesados.Equals(false));
            decimal suma = anticiposEstacion.Sum(x => x.TotalAnticipado);
            return new AnticiposEstacionDTO()
            {
                //IdCAlmacenGas = (unidad.IdCAlmacenGas != null) ? unidad.IdCAlmacenGas : 0,
                IdCAlmacenGas = unidad.IdCAlmacenGas,
                IdEstacion = unidad.IdEstacionCarburacion != null ? unidad.IdEstacionCarburacion.Value : 0,
                Anticipos = /*ToDTO(anticiposEstacion)*/null,
                Total = suma
            };
        }

        public static List<AnticipoDto> ToDTO(List<VentaCorteAnticipoEC> anticiposEstacion)
        {
            List<AnticipoDto> anticipos = new List<AnticipoDto>();
            foreach (var anticipoEstacion in anticiposEstacion)
            {
                anticipos.Add(new AnticipoDto()
                {
                    ClaveOperacion = anticipoEstacion.FolioOperacion,
                    Fecha = anticipoEstacion.FechaRegistro,
                    Monto = anticipoEstacion.TotalAnticipado,
                    Total = anticipoEstacion.TotalVenta,
                    IdCAlmacenGas = (anticipoEstacion.CAlmacenGas.IdEstacionCarburacion != null) ? (short)anticipoEstacion.CAlmacenGas.IdEstacionCarburacion.Value : (short)0,

                });
            }
            return anticipos;
        }

        public static VentaCorteAnticipoEC FromDto(AnticipoDto dto, short idEmpresa, int idUsario, PuntoVenta puntoventa)
        {
            return new VentaCorteAnticipoEC()
            {
                IdEmpresa = idEmpresa,
                TotalAnticipado = dto.Monto,
                MontoRecortadoAnticipado = dto.Monto,
                FolioOperacion = dto.ClaveOperacion,
                FolioOperacionDia = dto.ClaveOperacion,
                TotalVenta = dto.Total,
                IdUsuarioRecibe = idUsario,
                IdOperadorChofer = puntoventa.IdOperadorChofer,
                IdPuntoVenta = puntoventa.IdPuntoVenta,
                UsuarioRecibe = dto.Recibe
            };
        }

        public static VentaCorteAnticipoEC FromDto(CorteDto dto, short idEmpresa, int idUsuario, PuntoVenta puntoVenta)
        {
            return new VentaCorteAnticipoEC()
            {
                IdEmpresa = idEmpresa,
                TotalAnticipado = dto.Monto,
                MontoRecortadoAnticipado = dto.Monto,
                FolioOperacion = dto.ClaveOperacion,
                FolioOperacionDia = dto.ClaveOperacion,
                TotalVenta = dto.Total,
                IdUsuarioRecibe = idUsuario,
                IdOperadorChofer = puntoVenta.IdOperadorChofer,
                IdPuntoVenta = puntoVenta.IdPuntoVenta,
                UsuarioRecibe = dto.Recibe
            };
        }

        public static VentaCajaGeneral FromDTO(AnticipoDto dto, short idEmpresa, Usuario usuario, PuntoVenta puntoVenta, OperadorChofer operador,
            Usuario entrega, List<VentaPuntoDeVenta> deContado, List<VentaPuntoDeVenta> creditos)
        {
            decimal deContadoTotal = 0, creditoTotal = 0;
            decimal descuentoContado = 0, descuentoCredito = 0, descuentoTotal = 0;
            foreach (var contado in deContado)
            {
                deContadoTotal += contado.Total;
                descuentoContado += contado.Descuento;
                descuentoTotal += contado.Descuento;
            }
            foreach (var credito in creditos)
            {
                creditoTotal += credito.Total;
                descuentoCredito += credito.Descuento;
                descuentoTotal += credito.Descuento;
            }


            return new VentaCajaGeneral()
            {
                IdCAlmacenGas = puntoVenta.UnidadesAlmacen.IdCAlmacenGas,
                Year = (short)dto.Fecha.Year,
                Mes = (byte)dto.Fecha.Month,
                Dia = (byte)dto.Fecha.Day,
                IdEmpresa = idEmpresa,
                IdPuntoVenta = puntoVenta.IdPuntoVenta,
                IdOperadorChofer = puntoVenta.IdOperadorChofer,
                IdUsuarioRecibe = usuario.IdUsuario,
                IdUsuarioEntrega = entrega.IdUsuario,
                FolioOperacionDia = dto.ClaveOperacion,
                VentaTotal = dto.Total,
                VentaTotalContado = deContadoTotal,
                VentaTotalCredito = creditoTotal,
                OtrasVentas = 0,//Cambiar
                DescuentoOtrasVentas = 0,
                DescuentoCredito = descuentoCredito,
                DescuentoContado = descuentoContado,
                DescuentoTotal = 0,
                TodoCorrecto = false,
                PuntoVenta = puntoVenta.UnidadesAlmacen.Numero,
                OperadorChofer = operador.Usuario.Nombre + " " + operador.Usuario.Apellido1 + " " + operador.Usuario.Apellido2,
                UsuarioEntrega = entrega.Nombre + " " + entrega.Apellido1 + " " + entrega.Apellido2,
                UsuarioRecibe = usuario.Nombre + " " + usuario.Apellido1 + " " + usuario.Apellido2,
            };
        }

        public static DatosAnticiposCorteDto ToDTO(List<VentaPuntoDeVenta> ventas, List<VentaCorteAnticipoEC> anticipos, UnidadAlmacenGas almacen, bool esAnticipos = false)
        {

            if (esAnticipos)
                return new DatosAnticiposCorteDto()
                {
                    anticipos = ToDTOAnticipo(ventas, almacen)
                };
            else
                return new DatosAnticiposCorteDto()
                {
                    cortes = ToDTOCortes(ventas, almacen),
                    fechasCorte = EstraerFechas(ventas),
                    TotalAnticiposCorte = anticipos.Sum(x => x.TotalAnticipado)
                };
        }

        public static List<DateTime> EstraerFechas(List<VentaPuntoDeVenta> ventas)
        {
            List<DateTime> list = new List<DateTime>();
            foreach (var venta in ventas)
            {
                if (!list.Contains(venta.FechaAplicacion.Value)) list.Add(venta.FechaAplicacion.Value);
            }
            return list;
        }

        public static List<CorteDto> ToDTOCortes(List<VentaPuntoDeVenta> ventas, UnidadAlmacenGas almacen)
        {

            return ventas.Select(x => ToDTO(x, almacen)).ToList();
        }

        public static CorteDto ToDTO(VentaPuntoDeVenta venta, UnidadAlmacenGas almacen)
        {
            var recibe = TokenServicio.ObtenerUsuarioAplicacion();
            return new CorteDto()
            {
                ClaveOperacion = venta.FolioVenta,
                Tiket = venta.FolioVenta,
                Fecha = DateTime.Parse(venta.Year + "-" + venta.Mes + "-" + venta.Dia),
                IdCorte = (short)venta.IdPuntoVenta,
                IdCAlmacenGas = almacen.IdCAlmacenGas,
                Monto = venta.Total,
                Total = venta.Total,
                Recibe = recibe.Nombre + " " + recibe.Apellido1 + " " + recibe.Apellido2
            };
        }

        public static List<AnticipoDto> ToDTOAnticipo(List<VentaPuntoDeVenta> ventas, UnidadAlmacenGas almacen)
        {
            return ventas.Select(x => ToDTOAn(x, almacen)).ToList();
        }

        public static AnticipoDto ToDTOAn(VentaPuntoDeVenta venta, UnidadAlmacenGas almacen)
        {
            var recibe = TokenServicio.ObtenerUsuarioAplicacion();
            return new AnticipoDto()
            {
                Tiket = venta.FolioVenta,
                Fecha = DateTime.Parse(venta.Year + "-" + venta.Mes + "-" + venta.Dia),
                IdAnticipo = (short)venta.IdPuntoVenta,
                Recibe = recibe.Nombre + " " + recibe.Apellido1 + " " + recibe.Apellido2,
                Total = venta.Total,
                Monto = venta.Total,
                IdCAlmacenGas = almacen.IdCAlmacenGas,
                ClaveOperacion = venta.FolioVenta,
                FechaAnticipo = DateTime.Parse(venta.Year + "-" + venta.Mes + "-" + venta.Dia)
            };
        }

        public static VentaCajaGeneral FromDTO(CorteDto dto, short idEmpresa, Usuario usuario, PuntoVenta puntoVenta, OperadorChofer operador, Usuario entrega, List<VentaPuntoDeVenta> deContado, List<VentaPuntoDeVenta> creditos)
        {
            decimal deContadoTotal = 0, creditoTotal = 0;
            decimal descuentoContado = 0, descuentoCredito = 0, descuentoTotal = 0;
            foreach (var contado in deContado)
            {
                deContadoTotal += contado.Total;
                descuentoContado += contado.Descuento;
                descuentoTotal += contado.Descuento;
            }
            foreach (var credito in creditos)
            {
                creditoTotal += credito.Total;
                descuentoCredito += credito.Descuento;
                descuentoTotal += credito.Descuento;
            }
            return new VentaCajaGeneral()
            {
                IdCAlmacenGas = puntoVenta.UnidadesAlmacen.IdCAlmacenGas,
                Year = (short)dto.Fecha.Year,
                Mes = (byte)dto.Fecha.Month,
                Dia = (byte)dto.Fecha.Day,
                IdEmpresa = idEmpresa,
                IdPuntoVenta = puntoVenta.IdPuntoVenta,
                IdOperadorChofer = puntoVenta.IdOperadorChofer,
                IdUsuarioRecibe = usuario.IdUsuario,
                IdUsuarioEntrega = entrega.IdUsuario,
                FolioOperacionDia = dto.ClaveOperacion,
                VentaTotal = dto.Total,
                VentaTotalContado = creditoTotal,
                VentaTotalCredito = deContadoTotal,
                OtrasVentas = dto.Total,//Cambiar
                DescuentoOtrasVentas = 0,
                DescuentoCredito = descuentoCredito,
                DescuentoContado = descuentoContado,
                DescuentoTotal = descuentoTotal,
                TodoCorrecto = false,
                PuntoVenta = puntoVenta.UnidadesAlmacen.Numero,
                OperadorChofer = operador.Usuario.Nombre + " " + operador.Usuario.Apellido1 + " " + operador.Usuario.Apellido2,
                UsuarioEntrega = entrega.Nombre + " " + entrega.Apellido1 + " " + entrega.Apellido2,
                UsuarioRecibe = usuario.Nombre + " " + usuario.Apellido1 + " " + usuario.Apellido2,
            };
        }

        public static DatosAnticiposCorteDto ToDTOPipa(List<VentaPuntoDeVenta> ventas, List<VentaCorteAnticipoEC> anticipos, UnidadAlmacenGas unidadAlmacen, bool esAnticipos)
        {

            if (esAnticipos) { 
                decimal cantidadVentas = ventas.Count>0? ventas.Sum(x => x.Total):0;

                return new DatosAnticiposCorteDto()
                {
                    anticipos = ToDTOAnticipo(ventas, unidadAlmacen),
                    fechasCorte = EstraerFechas(ventas),
                    TotalAnticiposCorte = cantidadVentas
                };
            }else {
                decimal cantidadAnticipos = ventas.Count > 0 ? anticipos.Sum(x => x.TotalAnticipado):0;
                return new DatosAnticiposCorteDto()
                {
                   
                    cortes = ToDTOCortes(ventas, unidadAlmacen),
                    fechasCorte = EstraerFechas(ventas),
                    TotalAnticiposCorte = cantidadAnticipos
                };
            }
        }
    }
}
