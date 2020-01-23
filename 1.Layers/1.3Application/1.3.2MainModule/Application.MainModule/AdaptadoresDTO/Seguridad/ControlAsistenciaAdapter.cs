using Application.MainModule.DTOs;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO
{
    public static class ControlAsistenciaAdapter
    {
        public static List<ControlAsistencia> FromDTO(List<ControlAsistenciaDTO> dtos)
        {
            return dtos.Select(x => FromDTO(x)).ToList();
        }

        public static ControlAsistencia FromDTO(ControlAsistenciaDTO dto)
        {
            return new ControlAsistencia()
            {
                IdControlAsistencia = dto.IdControlAsistencia,
                IdUsuario = dto.IdUsuario,
                Estatus = dto.Estatus,
                FechaRegistro = dto.FechaRegistro,
                Coordenadas = dto.Coordenadas,
                IdEmpresa = dto.IdEmpresa,
            };
        }
        public static List<ControlAsistenciaDTO> toDTO(List<ControlAsistencia> entidades)
        {
            return entidades.Select(x => ToDTO(x)).ToList();
        }
        public static ControlAsistenciaDTO ToDTO(ControlAsistencia entidad)
        {
            return new ControlAsistenciaDTO()
            {
                IdControlAsistencia = entidad.IdControlAsistencia,
                IdUsuario = entidad.IdUsuario,
                Estatus = entidad.Estatus,
                FechaRegistro = entidad.FechaRegistro,
                Coordenadas = entidad.Coordenadas,
                IdEmpresa = entidad.IdEmpresa,
            };
        }
        public static ControlDeAsistenciaDTO ToDTOr(ControlAsistencia entidad)
        {
            return new ControlDeAsistenciaDTO()
            {
                IdRegistro = entidad.IdControlAsistencia,
                Nombre = UsuarioServicio.ObtenerNombreCompleto(entidad.Usuario),
                Estatus = entidad.Estatus ? "Exitoso" : "No exitoso",
                FechaRegistro = entidad.FechaRegistro,
                Coordenadas = entidad.Coordenadas,
                PtoVenta = PuntoVentaServicio.ObtenerPorUsuario(entidad.IdUsuario).Numero,
            };
        }
        public static List<ControlDeAsistenciaDTO> ToDTOr(List<ControlAsistencia> entidades)
        {
            return entidades.Select(x => ToDTOr(x)).ToList();
        }
    }
}
