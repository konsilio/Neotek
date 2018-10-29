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
    }
}
