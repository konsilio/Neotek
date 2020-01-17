using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public class FormaPagoAdapter
    {
        public static FormaPagoDTO ToDTO(FormaPago entidad)
        {
            return new FormaPagoDTO()
            {
                IdFormaPago = entidad.IdFormaPago,
                Descripcion = entidad.Descripcion,
                Activo = entidad.Activo,
                FechaRegistro = entidad.FechaRegistro
            };
        }
        public static List<FormaPagoDTO> ToDTO(List<FormaPago> entidades)
        {
            return entidades.Select(x => ToDTO(x)).ToList();
        }
        public static FormaPago FromDTO(FormaPagoDTO dto)
        {
            return new FormaPago()
            {
                IdFormaPago = dto.IdFormaPago,
                Descripcion = dto.Descripcion,
                Activo = dto.Activo,
                FechaRegistro = dto.FechaRegistro
            };
        }
        public static List<FormaPago> FromDTO(List<FormaPagoDTO> entidad)
        {
            return entidad.Select(x => FromDTO(x)).ToList();
        }
    }
}
