using Application.MainModule.DTOs;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class EquipoTransporteAdapter
    {
        public static EquipoTransporteDTO toDTO(CDetalleEquipoTransporte ec)
        {
            return new EquipoTransporteDTO()
            {
                IdEmpresa = ec.IdEmpresa,
                IdEquipoTransporte = EquipoTransporteServicio.ObtenerId(ec),
                //IdEquipoTransporte = ec.IdEquipoTransporteDetalle,//Se asigna este id ya que es el id primary de la tabla de detalle transporte
                IdCamioneta = ec.IdCamioneta,
                IdPipa = ec.IdPipa,
                IdVehiculoUtilitario = ec.IdUtilitario,
                FechaRegistro = EquipoTransporteServicio.ObtenerFechaRegistro(ec),
                Activo = EquipoTransporteServicio.ObtenerActivo(ec),
                EsForaneo = EquipoTransporteServicio.ObtenerForaneo(ec),
                Descripcion = EquipoTransporteServicio.ObtenerNombre(ec),
                NumIdVehicular = ec.NumIdVehicular,
                Placas = ec.Placas,
                NumMotor = ec.NumMotor,
                DescVehiculo = ec.DescVehiculo,
                Marca = ec.Marca,
                Modelo = ec.Modelo,
                Color = ec.Color,
                Cilindros = ec.Cilindros,
                IdTipoCombustible = ec.IdTipoCombustible,
                IdTipoUnidad = EquipoTransporteServicio.ObtenerTipo(ec),
                AliasUnidad = EquipoTransporteServicio.ObtenerAlias(ec),
                Id_DetalleEtransporte = ec.IdEquipoTransporteDetalle,
                CapacidadKg = EquipoTransporteServicio.ObtenerCapacidadKg(ec),
                CapacidadLts = EquipoTransporteServicio.ObtenerCapacidadLt(ec),
                EsCamioneta = ec.IdCamioneta != null ? true : false ,
                EsPipa = ec.IdPipa != null ? true : false,
                EsUtilitario = ec.IdUtilitario != null ? true : false,
            };
        }
        public static List<EquipoTransporteDTO> toDTO(List<CDetalleEquipoTransporte> ecs)
        {
            return ecs.Select(x => toDTO(x)).ToList();
        }
 
        public static CDetalleEquipoTransporte FromDTO(EquipoTransporteDTO ec)
        {
            return new CDetalleEquipoTransporte()
            {
                //IdEquipoTransporteDetalle = ec.Id_DetalleEtransporte,
                IdEquipoTransporteDetalle = ec.IdEquipoTransporte,//Se asigna este id ya que es el id primary de la tabla de detalle transporte
                IdEmpresa = ec.IdEmpresa,
                NumIdVehicular = ec.NumIdVehicular,
                Placas = ec.Placas,
                NumMotor = ec.NumMotor,
                DescVehiculo = ec.DescVehiculo,
                Marca = ec.Marca,
                Modelo = ec.Modelo,
                Color = ec.Color,
                Cilindros = ec.Cilindros,
                IdTipoCombustible = ec.IdTipoCombustible,
                IdCamioneta = ec.IdCamioneta,
                IdPipa = ec.IdPipa,
                IdUtilitario = ec.IdVehiculoUtilitario,
            };
        }
        public static List<CDetalleEquipoTransporte> FromDTO(List<EquipoTransporteDTO> dto)
        {
            return dto.Select(x => FromDTO(x)).ToList();
        }
      
        public static CDetalleEquipoTransporte FromEntity(CDetalleEquipoTransporte ec)
        {
            return new CDetalleEquipoTransporte()
            {
                IdEquipoTransporteDetalle = ec.IdEquipoTransporteDetalle,
                IdEmpresa = ec.IdEmpresa,
                IdCamioneta = ec.IdCamioneta,
                IdPipa = ec.IdPipa,
                IdUtilitario = ec.IdUtilitario,           
                NumIdVehicular = ec.NumIdVehicular,
                Placas = ec.Placas,
                NumMotor = ec.NumMotor,
                DescVehiculo = ec.DescVehiculo,
                Marca = ec.Marca,
                Modelo = ec.Modelo,
                Color = ec.Color,
                Cilindros = ec.Cilindros,
                IdTipoCombustible = ec.IdTipoCombustible,             
            };
        }
    }
}
