using System.Collections.Generic;
using System.Linq;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile;
using System;
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.Servicios.Catalogos;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class AnticiposCortesAdapter
    {
        public static DatosAnticiposCorteDto ToDTO(List<EstacionCarburacion> estaciones,List<UnidadAlmacenGas>unidades)
        {
            return new DatosAnticiposCorteDto()
            {
                estaciones = estaciones.Select(x=>ToDTO(x,unidades)).ToList(), 
            };
        }

        private static EstacionesDto ToDTO(EstacionCarburacion estacion, List<UnidadAlmacenGas> unidades)
        {
            var unidadEstacion = unidades.Single(x => x.IdEstacionCarburacion.Value.Equals(estacion.IdEstacionCarburacion));
            var lecturaInicial = unidadEstacion.TomasLectura.Where(
                x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)
                ).OrderBy(x=>x.FechaRegistro).Last();
            return new EstacionesDto()
            {
                Medidor = TipoMedidorAdapter.ToDto(TipoMedidorGasServicio.Obtener(unidadEstacion.IdTipoMedidor.Value)),
                IdTipoMedidor = unidadEstacion.IdTipoMedidor.Value,
                IdAlmacenGas = (short)estacion.IdEstacionCarburacion,
                NombreAlmacen = estacion.Nombre,
                P5000Inicial = lecturaInicial.P5000.Value,
                P5000Final =unidadEstacion.P5000Actual.Value,
                AnticiposEstacion = ToDTO(unidadEstacion)
            };
        }

        public static AnticiposEstacionDTO ToDTO(UnidadAlmacenGas unidad)
        {
            var anticiposEstacion = PuntoVentaServicio.ObtenerAnticipos(unidad).FindAll(x=>x.DatosProcesados.Equals(false));
            decimal suma = anticiposEstacion.Sum(x => x.TotalAnticipado);
            return new AnticiposEstacionDTO()
            {
                IdCAlmacenGas = unidad.IdCAlmacenGas,
                IdEstacion = unidad.IdEstacionCarburacion.Value,
                Anticipos = ToDTO(anticiposEstacion),
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
                    IdCAlmacenGas = (short)anticipoEstacion.CAlmacenGas.IdEstacionCarburacion.Value,
                    
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
                IdPuntoVenta= puntoventa.IdPuntoVenta  ,
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

        public static VentaCajaGeneral FromDTO(AnticipoDto dto, short idEmpresa, Usuario usuario, PuntoVenta puntoVenta,OperadorChofer operador, 
            Usuario entrega,List<VentaPuntoDeVenta> deContado,List<VentaPuntoDeVenta> creditos)
        {
            decimal deContadoTotal=0,creditoTotal=0;
            decimal descuentoContado = 0, descuentoCredito = 0,descuentoTotal = 0;
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
                TodoCorrecto =false,
                PuntoVenta  = puntoVenta.UnidadesAlmacen.Numero,
                OperadorChofer =  operador.Usuario.Nombre+" "+operador.Usuario.Apellido1+" "+operador.Usuario.Apellido2,
                UsuarioEntrega = entrega.Nombre +" "+ entrega.Apellido1+" "+entrega.Apellido2,
                UsuarioRecibe = usuario.Nombre + " " + usuario.Apellido1 + " " + usuario.Apellido2,
            };
        }

        public static DatosAnticiposCorteDto ToDTO(List<VentaPuntoDeVenta> ventas, List<VentaCorteAnticipoEC> anticipos, bool esAnticipos = false)
        {
            
            if (esAnticipos)
                return new DatosAnticiposCorteDto()
                {
                    anticipos = ToDTOAnticipo(ventas)
                };
            else
                return new DatosAnticiposCorteDto()
                {
                    cortes = ToDTOCortes(ventas),
                    fechasCorte = EstraerFechas(ventas),
                    TotalAnticiposCorte = anticipos.Where(x=>x.IdTipoOperacion.Equals(1)).Sum(x=>x.TotalAnticipado)
                };
        }

        public static List<DateTime> EstraerFechas(List<VentaPuntoDeVenta> ventas)
        {
            List<DateTime> list = new List<DateTime>();
            foreach (var venta in ventas)
            {
                if (!list.Contains(venta.FechaRegistro)) list.Add(venta.FechaRegistro);
            }
            return list;
        }

        public static List<CorteDto> ToDTOCortes(List<VentaPuntoDeVenta> ventas)
        {
            
            return ventas.Select(x=>ToDTO(x)).ToList();
        }

        public static CorteDto ToDTO(VentaPuntoDeVenta venta)
        {
            return new CorteDto()
            {
                ClaveOperacion = venta.FolioVenta,
                Tiket = venta.FolioVenta,
                Fecha = venta.FechaRegistro,
                IdCorte = (short) venta.IdPuntoVenta,
                Monto = venta.Total,
                Total = venta.Total
            };
        }

        public static List<AnticipoDto> ToDTOAnticipo(List<VentaPuntoDeVenta> ventas)
        {
            return ventas.Select(x=> ToDTOAn(x)).ToList();
        }

        public static AnticipoDto ToDTOAn(VentaPuntoDeVenta venta)
        {
            return new AnticipoDto()
            {
                Tiket = venta.FolioVenta,
                Fecha = venta.FechaRegistro,
                IdAnticipo = (short)venta.IdPuntoVenta,
                Total = venta.Total,
                Monto = venta.Total,
                ClaveOperacion = venta.FolioVenta,
            };
        }

        public static VentaCajaGeneral FromDTO(CorteDto dto, short idEmpresa, Usuario usuario, PuntoVenta puntoVenta, OperadorChofer operador, Usuario entrega, List<VentaPuntoDeVenta> deContado, List<VentaPuntoDeVenta> creditos)
        {
            decimal deContadoTotal = 0, creditoTotal = 0;
            decimal descuentoContado = 0, descuentoCredito = 0,descuentoTotal =0 ;
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
    }
}
