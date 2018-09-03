using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class CentroCostoAdapter
    {
        public static CentroCosto FromDto(CentroCostoCrearDto cCostoDto)
        {
            return new CentroCosto()
            {
                IdEmpresa = cCostoDto.IdEmpresa,
                IdTipoCentroCosto = cCostoDto.IdTipoCentroCosto,
                IdCAlmacenGas = cCostoDto.IdCAlmacenGas != null && cCostoDto.IdCAlmacenGas > 0 ? cCostoDto.IdCAlmacenGas : null,
                IdCamioneta = cCostoDto.IdCamioneta != null && cCostoDto.IdCamioneta > 0 ? cCostoDto.IdCamioneta : null,
                IdCilindro = cCostoDto.IdCilindro != null && cCostoDto.IdCilindro > 0 ? cCostoDto.IdCilindro : null,
                IdEquipoTransporte = cCostoDto.IdEquipoTransporte != null && cCostoDto.IdCAlmacenGas > 0 ? cCostoDto.IdCAlmacenGas : null,
                IdEstacionCarburacion = cCostoDto.IdEstacionCarburacion != null && cCostoDto.IdEstacionCarburacion > 0 ? cCostoDto.IdEstacionCarburacion : null,
                IdPipa = cCostoDto.IdPipa != null && cCostoDto.IdPipa > 0 ? cCostoDto.IdPipa : null,
                IdVehiculoUtilitario = cCostoDto.IdVehiculoUtilitario != null && cCostoDto.IdVehiculoUtilitario > 0 ? cCostoDto.IdVehiculoUtilitario : null,
                Numero = cCostoDto.Numero,
                Descripcion = cCostoDto.Descripcion,
                FechaRegistro = DateTime.Now,
                Activo = true,
            };
        }
        public static CentroCosto FromDto(CentroCostoModificarDto cCostoDto)
        {
            return new CentroCosto()
            {
                IdCentroCosto = cCostoDto.IdCentroCosto,
                IdEmpresa = cCostoDto.IdEmpresa,
                IdTipoCentroCosto = cCostoDto.IdTipoCentroCosto,
                IdCAlmacenGas = cCostoDto.IdCAlmacenGas != null && cCostoDto.IdCAlmacenGas > 0 ? cCostoDto.IdCAlmacenGas : null,
                IdCamioneta = cCostoDto.IdCamioneta != null && cCostoDto.IdCamioneta > 0 ? cCostoDto.IdCamioneta : null,
                IdCilindro = cCostoDto.IdCilindro != null && cCostoDto.IdCilindro > 0 ? cCostoDto.IdCilindro : null,
                IdEquipoTransporte = cCostoDto.IdEquipoTransporte != null && cCostoDto.IdCAlmacenGas > 0 ? cCostoDto.IdCAlmacenGas : null,
                IdEstacionCarburacion = cCostoDto.IdEstacionCarburacion != null && cCostoDto.IdEstacionCarburacion > 0 ? cCostoDto.IdEstacionCarburacion : null,
                IdPipa = cCostoDto.IdPipa != null && cCostoDto.IdPipa > 0 ? cCostoDto.IdPipa : null,
                IdVehiculoUtilitario = cCostoDto.IdVehiculoUtilitario != null && cCostoDto.IdVehiculoUtilitario > 0 ? cCostoDto.IdVehiculoUtilitario : null,
                Numero = cCostoDto.Numero,
                Descripcion = cCostoDto.Descripcion,
                Activo = true,
            };
        }
        public static CentroCosto FromEntity(CentroCosto centro)
        {
            return new CentroCosto()
            {
                IdCentroCosto = centro.IdCentroCosto,
                IdEmpresa = centro.IdEmpresa,
                IdTipoCentroCosto = centro.IdTipoCentroCosto,
                IdCAlmacenGas = centro.IdCAlmacenGas,
                IdCamioneta = centro.IdCamioneta,
                IdCilindro = centro.IdCilindro,
                IdEquipoTransporte = centro.IdEquipoTransporte,
                IdEstacionCarburacion = centro.IdEstacionCarburacion,
                IdPipa = centro.IdPipa,
                IdVehiculoUtilitario = centro.IdVehiculoUtilitario,
                Numero = centro.Numero,
                Descripcion = centro.Descripcion,
                FechaRegistro = centro.FechaRegistro,
                Activo = centro.Activo,
            };
        }
        public static CentroCostoDTO ToDTO(CentroCosto cc)
        {
            CentroCostoDTO ccDTO = new CentroCostoDTO();
            ccDTO.IdCentroCosto = cc.IdCentroCosto;
            ccDTO.IdEmpresa = cc.IdEmpresa;
            ccDTO.Empresa = cc.Empresa.NombreComercial;
            ccDTO.IdTipoCentroCosto = cc.IdTipoCentroCosto;
            ccDTO.TipoCentroCosto = cc.TipoCentroCosto.Descripcion;
            ccDTO.IdEquipoTransporte = cc.IdEquipoTransporte != null ? cc.IdEquipoTransporte.Value : 0;
            ccDTO.IdVehiculoUtilitario = cc.IdVehiculoUtilitario != null ? cc.IdVehiculoUtilitario.Value : 0;
            ccDTO.IdCAlmacenGas = cc.IdCAlmacenGas != null ? cc.IdCAlmacenGas.Value : (short)0;
            ccDTO.IdEstacionCarburacion = cc.IdEstacionCarburacion != null ? cc.IdEstacionCarburacion.Value : 0;
            ccDTO.IdCamioneta = cc.IdCamioneta != null ? cc.IdCamioneta.Value : 0;
            ccDTO.IdPipa = cc.IdPipa != null ? cc.IdPipa.Value : 0;
            ccDTO.IdCilindro = cc.IdCilindro != null ? cc.IdCilindro.Value : 0;
            ccDTO.Numero = cc.Numero;
            ccDTO.Descripcion = cc.Descripcion;
            ccDTO.Activo = cc.Activo;
            ccDTO.FechaRegistro = cc.FechaRegistro;
            return ccDTO;
        }
        public static List<CentroCostoDTO> ToDTO(List<CentroCosto> ccs)
        {
            List<CentroCostoDTO> ccsDTO = ccs.Select(x => ToDTO(x)).ToList();
            return ccsDTO;
        }
    }
}
