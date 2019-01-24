using Application.MainModule.DTOs;
using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class EquipoTransporteAdapter
    {
        public static EquipoTransporteDTO toDTO(EquipoTransporte ec)
        {
            return new EquipoTransporteDTO()
            {
                IdEmpresa = ec.IdEmpresa,
                IdEquipoTransporte = ec.IdEquipoTransporte,
                IdCamioneta = ec.IdCamioneta,
                IdPipa = ec.IdPipa,
                IdVehiculoUtilitario = ec.IdVehiculoUtilitario,
                FechaRegistro = ec.FechaRegistro,
                Activo = ec.Activo,
                Descripcion = EquipoTransporteServicio.ObtenerNombre(ec),
            };
        }
        public static List<EquipoTransporteDTO> toDTO(List<EquipoTransporte> ecs)
        {
            return ecs.Select(x => toDTO(x)).ToList();
        }
        public static EquipoTransporte FromDTO(EquipoTransporteDTO ec)
        {
            return new EquipoTransporte()
            {
                IdEmpresa = ec.IdEmpresa,
                IdEquipoTransporte = ec.IdEquipoTransporte,
                IdCamioneta = ec.IdCamioneta,
                IdPipa = ec.IdPipa,
                IdVehiculoUtilitario = ec.IdVehiculoUtilitario,
                FechaRegistro = ec.FechaRegistro
            };
        }
        public static List<EquipoTransporte> FromDTO(List<EquipoTransporteDTO> ecs)
        {
            return ecs.Select(x => FromDTO(x)).ToList();
        }

        public static EquipoTransporte FromDto(EquipoTransporteDTO Vehiculodto, EquipoTransporte catCte)
        {
            var _unidad = FromEntity(catCte);
            _unidad.IdEmpresa = Vehiculodto.IdEmpresa;
            _unidad.IdEquipoTransporte = Vehiculodto.IdEquipoTransporte;
            _unidad.IdCamioneta = Vehiculodto.IdCamioneta;
            _unidad.IdPipa = Vehiculodto.IdPipa;
            _unidad.IdVehiculoUtilitario = Vehiculodto.IdVehiculoUtilitario;
            _unidad.FechaRegistro = Vehiculodto.FechaRegistro;
            return _unidad;
        }
        public static EquipoTransporte FromEntity(EquipoTransporte ec)
        {
            return new EquipoTransporte()
            {
                IdEmpresa = ec.IdEmpresa,
                IdEquipoTransporte = ec.IdEquipoTransporte,
                IdCamioneta = ec.IdCamioneta,
                IdPipa = ec.IdPipa,
                IdVehiculoUtilitario = ec.IdVehiculoUtilitario,
                FechaRegistro = ec.FechaRegistro
            };
        }
    }
}
