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
        public static MantenimientoDTO ToDTO(CMantenimiento entidad, DetalleMantenimiento entidadDetalle)
        {
            return new MantenimientoDTO()
            {
                IdMantenimiento = entidad.Id_Mantenimiento,
                Mantenimiento = entidad.Mantenimiento,
                Descripcion = entidad.Descripcion,
                Activo = entidad.Activo,
                Id_Empresa = entidad.Id_Empresa,
                FechaMtto = entidadDetalle.FechaMtto,
                idVehiculo = entidadDetalle.id_vehiculo,
                EsCamioneta = entidadDetalle.EsCamioneta,
                EsPipa = entidadDetalle.EsPipa,
                EsUtilitario = entidadDetalle.EsUtilitario,
                IdTipomtto = entidadDetalle.Id_tipomtto,
                DescripcionMtto = entidadDetalle.DescripcionMtto,
                KilometrajeActual = entidadDetalle.Kilometraje_Actual,
                NumeroOC = entidadDetalle.NumeroOC,
            };
        }
        public static List<MantenimientoDTO> ToDTO(List<CMantenimiento> entidad, List<DetalleMantenimiento> entidadDetalle)
        {
            return new List<MantenimientoDTO>();
        }
        public static CMantenimiento FromDTO(CMantenimiento dto)
        {
            return new CMantenimiento()
            { };
        }
    }
}
