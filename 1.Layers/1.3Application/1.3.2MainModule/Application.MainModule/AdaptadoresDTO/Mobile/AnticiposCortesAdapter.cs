using System.Collections.Generic;
using System.Linq;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile;
using System;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class AnticiposCortesAdapter
    {
        public static DatosAnticiposCorteDto ToDTO(List<EstacionCarburacion> estaciones)
        {
            return new DatosAnticiposCorteDto()
            {
                estaciones = estaciones.Select(x=>ToDTO(x)).ToList()
            };
        }

        private static EstacionesDto ToDTO(EstacionCarburacion estacion)
        {
            return new EstacionesDto()
            {
                IdAlmacenGas = (short)estacion.IdEstacionCarburacion,
                NombreAlmacen = estacion.Nombre
            };
        }

        public static VentaCorteAnticipoEC FromDto(AnticipoDto dto, short idEmpresa, int idUsario, PuntoVenta puntoventa)
        {
            return new VentaCorteAnticipoEC()
            {
                IdCAlmacenGas =  dto.IdCAlmacenGas,
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
                IdCAlmacenGas = dto.IdCAlmacenGas,
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

        public static VentaCajaGeneral FromDTO(AnticipoDto dto, short idEmpresa, Usuario usuario, PuntoVenta puntoVenta,OperadorChofer operador, Usuario entrega)
        {

            return new VentaCajaGeneral()
            {
                IdCAlmacenGas = dto.IdCAlmacenGas,
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
                VentaTotalContado = dto.Total,//cambiar
                VentaTotalCredito = dto.Total,//cambiar
                OtrasVentas = dto.Total,//Cambiar
                DescuentoOtrasVentas = 0,
                DescuentoCredito = 0,
                DescuentoContado = 0,
                DescuentoTotal = 0,
                TodoCorrecto =false,
                PuntoVenta = "",
                OperadorChofer =  operador.Usuario.Nombre+" "+operador.Usuario.Apellido1+" "+operador.Usuario.Apellido2,
                UsuarioEntrega = entrega.Nombre +" "+ entrega.Apellido1+" "+entrega.Apellido2,
                UsuarioRecibe = usuario.Nombre + " " + usuario.Apellido1 + " " + usuario.Apellido2,
            };
        }

        public static DatosAnticiposCorteDto ToDTO(List<VentaPuntoDeVenta> ventas,bool esAnticipos = false)
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
                    fechasCorte = EstraerFechas(ventas)
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
                Id = (short) venta.IdPuntoVenta,
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
                Id = (short)venta.IdPuntoVenta,
                Total = venta.Total,
                Monto = venta.Total,
                ClaveOperacion = venta.FolioVenta,
            };
        }

        public static VentaCajaGeneral FromDTO(CorteDto dto, short idEmpresa, Usuario usuario, PuntoVenta puntoVenta, OperadorChofer operador, Usuario entrega)
        {
            return new VentaCajaGeneral()
            {
                IdCAlmacenGas = dto.IdCAlmacenGas,
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
                VentaTotalContado = dto.Total,//cambiar
                VentaTotalCredito = dto.Total,//cambiar
                OtrasVentas = dto.Total,//Cambiar
                DescuentoOtrasVentas = 0,
                DescuentoCredito = 0,
                DescuentoContado = 0,
                DescuentoTotal = 0,
                TodoCorrecto = false,
                PuntoVenta = "",
                OperadorChofer = operador.Usuario.Nombre + " " + operador.Usuario.Apellido1 + " " + operador.Usuario.Apellido2,
                UsuarioEntrega = entrega.Nombre + " " + entrega.Apellido1 + " " + entrega.Apellido2,
                UsuarioRecibe = usuario.Nombre + " " + usuario.Apellido1 + " " + usuario.Apellido2,
            };
        }
    }
}
