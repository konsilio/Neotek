using Application.MainModule.DTOs.EquipoTransporte;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.EquipoTrasnporte
{
    public static class MantenimientoAdapter
    {
        public static MantenimientoDTO ToDTO(CMantenimiento entidad)
        {
            return new MantenimientoDTO()
            {
                Id_Mantenimiento = entidad.Id_Mantenimiento,
                Mantenimiento = entidad.Mantenimiento,
                Descripcion = entidad.Descripcion,
                Activo = entidad.Activo,
                Id_Empresa = entidad.Id_Empresa,
            };
        }
        public static List<MantenimientoDTO> ToDTO(List<CMantenimiento> entidad)
        {
            return entidad.Select(x => ToDTO(x)).ToList();
        }
        public static CMantenimiento FromDTO(MantenimientoDTO dto)
        {
            return new CMantenimiento()
            {
                Id_Mantenimiento = dto.Id_Mantenimiento,
                Mantenimiento = dto.Mantenimiento,
                Descripcion = dto.Descripcion,
                Activo = dto.Activo,
                Id_Empresa = dto.Id_Empresa,
            };
        }
        public static List<CMantenimiento> FromDTO(List<MantenimientoDTO> entidad)
        {
            return entidad.Select(x => FromDTO(x)).ToList();
        }
        public static CMantenimiento FormEmtity(CMantenimiento entidad)
        {
            return new CMantenimiento()
            {
                Id_Mantenimiento = entidad.Id_Mantenimiento,
                Mantenimiento = entidad.Mantenimiento,
                Descripcion = entidad.Descripcion,
                Activo = entidad.Activo,
                Id_Empresa = entidad.Id_Empresa,
            };
        }
    }
}
