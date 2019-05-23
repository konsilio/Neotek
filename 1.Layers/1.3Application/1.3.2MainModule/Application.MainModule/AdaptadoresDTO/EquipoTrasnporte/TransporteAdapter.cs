using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Transporte;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Equipo;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO
{
    public static class TransporteAdapter
    {
        public static List<TransporteDTO> ToDTO(List<PuntoVenta> pvs, List<AsignacionUtilitarios> utili)
        {
            List<TransporteDTO> list = new List<TransporteDTO>();
            list.AddRange(ToDTO(pvs));
            list.AddRange(ToDTO(utili));
            return list;
        }
        public static TransporteDTO ToDTO(PuntoVenta pv)
        {
            TransporteDTO t = new TransporteDTO();
            t.IdChofer = pv.IdOperadorChofer;
            t.IdEmpresa = pv.IdEmpresa;
            t.Chofer = pv.OperadorChofer.Usuario.Nombre;
            t.IdVehiculo = pv.IdCAlmacenGas;
            t.Vehiculo = AlmacenGasServicio.ObtenerNombreUnidadAlmacenGas(pv.UnidadesAlmacen);
            if (pv.UnidadesAlmacen.IdPipa != null)
                t.TipoVehiculo = (short)TipoUnidadEqTransporteEnum.Pipa;
            if (pv.UnidadesAlmacen.IdCamioneta != null)
                t.TipoVehiculo = (short)TipoUnidadEqTransporteEnum.Camioneta;
            return t;
        }
        public static List<TransporteDTO> ToDTO(List<PuntoVenta> pvs)
        {
            return pvs.Select(x => ToDTO(x)).ToList();
        }
        public static TransporteDTO ToDTO(AsignacionUtilitarios ut)
        {
            TransporteDTO t = new TransporteDTO();
            t.IdChofer = ut.IdChoferOperador;
            t.IdEmpresa = ut.IdEmpresa;
            t.Chofer = ut.Usuario.Nombre;
            t.IdVehiculo = Convert.ToInt16(ut.IdUtilitario);
            t.Vehiculo = VehiculoUtilitarioServicio.ObtenerNombre(ut.IdUtilitario);
            t.TipoVehiculo = (short)TipoUnidadEqTransporteEnum.Utilitario;
            return t;
        }
        public static List<TransporteDTO> ToDTO(List<AsignacionUtilitarios> utili)
        {
            return utili.Select(x => ToDTO(x)).ToList();
        }
        public static List<RepGastoVehicularDTO> ToRepoPipas(List<Pipa> entidades, List<DetalleRecargaCombustible> recargas, GastoVehicularDTO dto)
        {
            return entidades.Select(x => ToRepoPipas(x, recargas, dto)).ToList();
        }
        public static RepGastoVehicularDTO ToRepoPipas(Pipa p, List<DetalleRecargaCombustible> recargas, GastoVehicularDTO dto)
        {
            RepGastoVehicularDTO respDTO = new RepGastoVehicularDTO();

            respDTO.Vehiculo = p.Nombre;
            respDTO.Combustible = Convert.ToDouble(CalculosEquipoTrasporte.CalacuarCostoRecargasCombustiblePipa(recargas, dto, p));
            respDTO.Mantenimiento = Convert.ToDouble(CalculosEquipoTrasporte.CalcularCostoMantenimientosPipa(p.DetalleMantenimiento.ToList(), dto, p));
            respDTO.Otros = 0; //Falta buscar en "Egresos" por "Centro de Costo"  y por "Rango de Fecha" 
            respDTO.Total = respDTO.Mantenimiento + respDTO.Combustible;
            return respDTO;
        }
        public static List<RepGastoVehicularDTO> ToRepoCamionetas(List<Camioneta> entidades, List<DetalleRecargaCombustible> recargas, GastoVehicularDTO dto)
        {
            return entidades.Select(x => ToRepoCamionetas(x, recargas, dto)).ToList();
        }
        public static RepGastoVehicularDTO ToRepoCamionetas(Camioneta c, List<DetalleRecargaCombustible> recargas, GastoVehicularDTO dto)
        {
            RepGastoVehicularDTO respDTO = new RepGastoVehicularDTO();

            respDTO.Vehiculo = c.Nombre;
            respDTO.Combustible = Convert.ToDouble(CalculosEquipoTrasporte.CalacuarCostoRecargasCombustibleCamioneta(recargas, dto, c));
            respDTO.Mantenimiento = Convert.ToDouble(CalculosEquipoTrasporte.CalcularCostoMantenimientosCamioneta(c.DetalleMantenimiento.ToList(), dto, c));
            respDTO.Otros = 0; //Falta buscar en "Egresos" por "Centro de Costo"  y por "Rango de Fecha" 
            respDTO.Total = respDTO.Mantenimiento + respDTO.Combustible;
            return respDTO;
        }
        public static List<RepGastoVehicularDTO> ToRepoUtilitario(List<CUtilitario> entidades, List<DetalleRecargaCombustible> recargas, GastoVehicularDTO dto)
        {
            return entidades.Select(x => ToRepoUtilitario(x, recargas, dto)).ToList();
        }
        public static RepGastoVehicularDTO ToRepoUtilitario(CUtilitario u, List<DetalleRecargaCombustible> recargas, GastoVehicularDTO dto)
        {
            RepGastoVehicularDTO respDTO = new RepGastoVehicularDTO();
            respDTO.Vehiculo = u.Nombre;
            respDTO.Combustible = Convert.ToDouble(CalculosEquipoTrasporte.CalacuarCostoRecargasCombustibleUtilitario(recargas, dto, u));
            respDTO.Mantenimiento = Convert.ToDouble(CalculosEquipoTrasporte.CalcularCostoMantenimientosUtilitario(u, dto));
            respDTO.Otros = 0; //Falta buscar en "Egresos" por "Centro de Costo"  y por "Rango de Fecha" 
            respDTO.Total = respDTO.Mantenimiento + respDTO.Combustible + respDTO.Otros;
            return respDTO;
        }
    }
}
