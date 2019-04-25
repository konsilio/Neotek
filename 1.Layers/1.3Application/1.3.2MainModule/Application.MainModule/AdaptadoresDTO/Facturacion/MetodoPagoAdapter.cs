using Application.MainModule.DTOs;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO
{
    public class MetodoPagoAdapter
    {
        public static MetodoPagoDTO ToDTO(MetodoPago entidad)
        {
            return new MetodoPagoDTO()
            {
                Id_MetodoPago = entidad.Id_MetodoPago,
                MetodoPagoSAT = entidad.MetodoPagoSAT,
                Descripcion = entidad.Descripcion,
                FechaIniVigencia = entidad.FechaIniVigencia,
                FechaFinVigencia = entidad.FechaFinVigencia,
            };
        }
        public static List<MetodoPagoDTO> ToDTO(List<MetodoPago> entidad)
        {
            return entidad.Select(x => ToDTO(x)).ToList();
        }
        public static MetodoPago FromDTO(MetodoPagoDTO dto)
        {
            return new MetodoPago()
            {
                Id_MetodoPago = dto.Id_MetodoPago,
                MetodoPagoSAT = dto.MetodoPagoSAT,
                Descripcion = dto.Descripcion,
                FechaIniVigencia = dto.FechaIniVigencia,
                FechaFinVigencia = dto.FechaFinVigencia,
            };
        }
        public static List<MetodoPago> FromDTO(List<MetodoPagoDTO> entidad)
        {
            return entidad.Select(x => FromDTO(x)).ToList();
        }
        public static MetodoPago FormEmtity(MetodoPago entidad)
        {
            return new MetodoPago()
            {
                Id_MetodoPago = entidad.Id_MetodoPago,
                MetodoPagoSAT = entidad.MetodoPagoSAT,
                Descripcion = entidad.Descripcion,
                FechaIniVigencia = entidad.FechaIniVigencia,
                FechaFinVigencia = entidad.FechaFinVigencia,
            };
        }
    }
}
