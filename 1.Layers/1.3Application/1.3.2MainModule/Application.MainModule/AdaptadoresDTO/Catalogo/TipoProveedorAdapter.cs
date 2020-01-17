using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class TipoProveedorAdapter
    {
        public static TipoProveedorDTO ToDTO(TipoProveedor entidad)
        {
            return new TipoProveedorDTO()
            {
                IdTipoProveedor = entidad.IdTipoProveedor,
                Tipo = entidad.Tipo,
                Activo = entidad.Activo,
                FechaRegistro = entidad.FechaRegistro
            };
        }
        public static List<TipoProveedorDTO> ToDTO(List<TipoProveedor> entidades)
        {
            return entidades.Select(x => ToDTO(x)).ToList();
        }
        public static TipoProveedor FromDTO(TipoProveedorDTO dto)
        {
            return new TipoProveedor()
            {
                IdTipoProveedor = dto.IdTipoProveedor,
                Tipo = dto.Tipo,
                Activo = dto.Activo,
                FechaRegistro = dto.FechaRegistro
            };
        }
        public static List<TipoProveedor> FromDTO(List<TipoProveedorDTO> entidad)
        {
            return entidad.Select(x => FromDTO(x)).ToList();
        }
    }
}
