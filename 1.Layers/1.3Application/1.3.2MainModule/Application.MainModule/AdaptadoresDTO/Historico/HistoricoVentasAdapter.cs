using Application.MainModule.DTOs;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Historico
{
    public static class HistoricoVentasAdapter
    {
        public static HistoricoVentaDTO ToDTO(HistoricoVentas entidad)
        {
            return new HistoricoVentaDTO()
            {
                Id = entidad.Id,
                Mes = entidad.Mes,
                Anio = entidad.Anio,
                MontoVenta = entidad.MontoVenta,
                EsPipa = entidad.EsPipa,
                EsCamioneta = entidad.EsCamioneta,
                EsLocal = entidad.EsLocal,
                FechaRegistro = entidad.FechaRegistro
                 
            };
        }
        public static List<HistoricoVentaDTO> ToDTO(List<HistoricoVentas> entidad)
        {
            return entidad.Select(x => ToDTO(x)).ToList();
        }
        public static HistoricoVentas FromDTO(HistoricoVentaDTO dto)
        {
            return new HistoricoVentas()
            {
                Id = dto.Id,
                Mes = dto.Mes,
                Anio = dto.Anio,
                MontoVenta = dto.MontoVenta,
                EsPipa = dto.EsPipa,
                EsCamioneta = dto.EsCamioneta,
                EsLocal = dto.EsLocal,
                FechaRegistro = dto.FechaRegistro ?? DateTime.Now // si el valor llega nulo asigana fecha actual
            };
        }
        public static List<HistoricoVentas> FromDTO(List<HistoricoVentaDTO> entidad)
        {
            return entidad.Select(x => FromDTO(x)).ToList();
        }
        public static HistoricoVentas FormEmtity(HistoricoVentas entidad)
        {
            return new HistoricoVentas()
            {
                Id = entidad.Id,
                Mes = entidad.Mes,
                Anio = entidad.Anio,
                MontoVenta = entidad.MontoVenta,
                EsPipa = entidad.EsPipa,
                EsCamioneta = entidad.EsCamioneta,
                EsLocal = entidad.EsLocal,
                FechaRegistro = entidad.FechaRegistro
            };
        }
        public static HistoricoVentas FromPV(VentaPuntoDeVenta entidad)
        {
            return new HistoricoVentas()
            {
                Mes = entidad.Mes,
                Anio = entidad.Year,
                MontoVenta = Convert.ToDouble(entidad.Total),
                EsPipa = entidad.CPuntoVenta.UnidadesAlmacen.IdPipa != null ? true : false,
                EsCamioneta = entidad.CPuntoVenta.UnidadesAlmacen.IdCamioneta != null ? true : false,
                EsLocal = true,
                FechaRegistro = entidad.FechaRegistro
            };
        }
        public static List<HistoricoVentas> FromPV(List<VentaPuntoDeVenta> entidad)
        {
            return entidad.Select(x => FromPV(x)).ToList();
        }

    }
}
