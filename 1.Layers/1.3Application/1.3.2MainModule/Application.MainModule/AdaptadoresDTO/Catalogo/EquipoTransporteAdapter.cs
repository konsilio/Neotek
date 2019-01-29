using Application.MainModule.DTOs;
using Application.MainModule.Servicios.AccesoADatos;
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
            CDetalleEquipoTransporte det = new EquipoTransporteDataAccess().BuscarDetalle(ec.IdEquipoTransporte);
            return new EquipoTransporteDTO()
            {
                IdEmpresa = ec.IdEmpresa,
                IdEquipoTransporte = ec.IdEquipoTransporte,
                IdCamioneta = ec.IdCamioneta,
                IdPipa = ec.IdPipa,
                IdVehiculoUtilitario = ec.IdVehiculoUtilitario,
                FechaRegistro = ec.FechaRegistro,
                Activo = ec.Activo,
                Descripcion = EquipoTransporteServicio.ObtenerNombre(ec)+" ",
                NumIdVehicular = det.NumIdVehicular,
                Placas = det.Placas,
                DescVehiculo = det.DescVehiculo,
                Marca = det.Marca,
                Modelo = det.Modelo,
                Color = det.Color,
                Cilindros = det.Cilindros,
                IdTipoCombustible = det.IdTipoCombustible,
                //IdTipoUnidad = det.
                AliasUnidad = det.Marca+" " + "Color" + " " + det.Color,
                Id_DetalleEtransporte = det.IdEquipoTransporteDetalle,
                EsCamioneta = det.EsCamioneta,
                EsPipa = det.EsPipa,
                EsUtilitario = det.EsUtilitario,
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
                Activo = ec.Activo,
                FechaRegistro = ec.FechaRegistro,
                DetalleEquipoTransporte = FromDTODet(ec),
            };
        }
        public static List<EquipoTransporte> FromDTO(List<EquipoTransporteDTO> ecs)
        {
            return ecs.Select(x => FromDTO(x)).ToList();
        }
        public static CDetalleEquipoTransporte FromDTOdet(EquipoTransporteDTO ec)
        {
            return new CDetalleEquipoTransporte()
            {

                NumIdVehicular = ec.NumIdVehicular,
                Placas = ec.Placas,
                NumMotor = ec.NumMotor,
                DescVehiculo = ec.DescVehiculo,
                Marca = ec.Marca,
                Modelo = ec.Modelo,
                Color = ec.Color,
                Cilindros = ec.Cilindros,
                IdTipoCombustible = ec.IdTipoCombustible,
                EsCamioneta = ec.IdTipoUnidad == 1 ? true : false,//dto.IdCamioneta
                EsPipa = ec.IdTipoUnidad == 2 ? true : false,//dto.IdPipa
                EsUtilitario = ec.IdTipoUnidad == 3 ? true : false,//dto.IdVehiculoUtilitario
            };
        }
        public static List<CDetalleEquipoTransporte> FromDTODet(EquipoTransporteDTO dto)
        {
            List<CDetalleEquipoTransporte> lst = new List<CDetalleEquipoTransporte>();
            lst.Add(FromDTOdet(dto));
            return lst;
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
