using Application.MainModule.DTOs.EquipoTransporte;
using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.MantenimientoDetalleAdapter
{
    public class MantenimientoDetalleAdapter
    {
        public static MantenimientoDetalleDTO ToDTO(DetalleMantenimiento entidad)
        {
            return new MantenimientoDetalleDTO()
            {
                Id_DetalleMtto = entidad.Id_DetalleMtto,
                FechaMtto = entidad.FechaMtto,
                id_vehiculo = entidad.id_vehiculo,
                Vehiculo = EquipoTransporteServicio.ObtenerNombre(entidad),
                EsCamioneta = entidad.EsCamioneta,
                EsPipa = entidad.EsPipa,
                EsUtilitario = entidad.EsUtilitario,
                Id_tipomtto = entidad.Id_tipomtto,
                DescripcionMtto = entidad.DescripcionMtto,
                Kilometraje_Actual = entidad.Kilometraje_Actual,
                NumeroOC = entidad.NumeroOC,
            };
        }
        public static List<MantenimientoDetalleDTO> ToDTO(List<DetalleMantenimiento> entidad)
        {
            return entidad.Select(x => ToDTO(x)).ToList();
        }
        public static DetalleMantenimiento FromDTO(MantenimientoDetalleDTO dto)
        {
            return new DetalleMantenimiento()
            {
                Id_DetalleMtto = dto.Id_DetalleMtto,
                FechaMtto = dto.FechaMtto,
                id_vehiculo = dto.id_vehiculo,
                EsCamioneta = dto.EsCamioneta,
                EsPipa = dto.EsPipa,
                EsUtilitario = dto.EsUtilitario,
                Id_tipomtto = dto.Id_tipomtto,
                DescripcionMtto = dto.DescripcionMtto,
                Kilometraje_Actual = dto.Kilometraje_Actual,
                NumeroOC = dto.NumeroOC,
            };
        }
        public static List<DetalleMantenimiento> FromDTO(List<MantenimientoDetalleDTO> entidad)
        {
            return entidad.Select(x => FromDTO(x)).ToList();
        }
        public static DetalleMantenimiento FormEmtity(DetalleMantenimiento entidad)
        {
            return new DetalleMantenimiento()
            {
                Id_DetalleMtto = entidad.Id_DetalleMtto,
                FechaMtto = entidad.FechaMtto,
                id_vehiculo = entidad.id_vehiculo,
                EsCamioneta = entidad.EsCamioneta,
                EsPipa = entidad.EsPipa,
                EsUtilitario = entidad.EsUtilitario,
                Id_tipomtto = entidad.Id_tipomtto,
                DescripcionMtto = entidad.DescripcionMtto,
                Kilometraje_Actual = entidad.Kilometraje_Actual,
                NumeroOC = entidad.NumeroOC,
            };
        }
    }
}
