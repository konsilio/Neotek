using Application.MainModule.DTOs;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO
{
    public static class AsignacionUtilitarioAdapter
    {
        public static AsignacionUtilitarioDTO ToDTO(AsignacionUtilitarios entidad)
        {
            return new AsignacionUtilitarioDTO()
            {
                IdAsignacionUtilitario = entidad.IdAsignacionUtilitario,
                IdEmpresa = entidad.IdEmpresa,
                IdUtilitario = entidad.IdUtilitario,
                IdChoferOperador = entidad.IdChoferOperador,
                FechaMdidificacion = entidad.FechaMdidificacion,
                Activo = entidad.Activo,
                FechaCreacion = entidad.FechaCreacion,
            };
        }
        public static List<AsignacionUtilitarioDTO> ToDTO(List<AsignacionUtilitarios> entidad)
        {
            return entidad.Select(x => ToDTO(x)).ToList();
        }
        public static AsignacionUtilitarios FromDTO(AsignacionUtilitarioDTO dto)
        {
            return new AsignacionUtilitarios()
            {
                IdAsignacionUtilitario = dto.IdAsignacionUtilitario,
                IdEmpresa = dto.IdEmpresa,
                IdUtilitario = dto.IdUtilitario,
                IdChoferOperador = dto.IdChoferOperador,
                FechaMdidificacion = dto.FechaMdidificacion,
                Activo = dto.Activo,
                FechaCreacion = dto.FechaCreacion,
            };
        }
        public static List<AsignacionUtilitarios> FromDTO(List<AsignacionUtilitarioDTO> entidad)
        {
            return entidad.Select(x => FromDTO(x)).ToList();
        }
        public static AsignacionUtilitarios FormEmtity(AsignacionUtilitarios entidad)
        {
            return new AsignacionUtilitarios()
            {
                IdAsignacionUtilitario = entidad.IdAsignacionUtilitario,
                IdEmpresa = entidad.IdEmpresa,
                IdUtilitario = entidad.IdUtilitario,
                IdChoferOperador = entidad.IdChoferOperador,
                FechaMdidificacion = entidad.FechaMdidificacion,
                Activo = entidad.Activo,
                FechaCreacion = entidad.FechaCreacion,
            };
        }
    }
}
