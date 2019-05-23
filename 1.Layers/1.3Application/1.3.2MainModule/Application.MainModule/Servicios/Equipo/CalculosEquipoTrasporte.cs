using Application.MainModule.DTOs;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Equipo
{
    public static class CalculosEquipoTrasporte
    {
        public static int ObtenerKilometrajeInicial(DetalleRecargaCombustible entidad)
        {
            return entidad.KilometrajeActual - (int)entidad.KilometrajeRecorrido;
        }
        public static decimal CalcularRendimientoVehicular(DetalleRecargaCombustible entidad)
        {
            return entidad.KilometrajeRecorrido / entidad.LitrosRecargados;
        }
        public static decimal CalcularCostoMantenimientos(List<DetalleMantenimiento> mantenimientos, GastoVehicularDTO dto)
        {
            return mantenimientos.Where(x => x.FechaMtto > dto.FechaInicio
                                    && x.FechaMtto < dto.FechaInicio)
                                    .Sum(recarga => recarga.Monto) ?? 0;
        }
        public static decimal CalcularCostoMantenimientosUtilitario(CUtilitario entidad, GastoVehicularDTO dto)
        {
            return MantenimientoDetalleServicio.Buscar(dto.FechaInicio, dto.FechaFin).ToList()
                    .Where(x => x.id_vehiculo.Equals(entidad.IdUtilitario)  
                            && x.EsUtilitario)
                            .Sum(recarga => recarga.Monto) ?? 0;
        }
        public static decimal CalacuarCostoRecargasCombustiblePipa(List<DetalleRecargaCombustible> recargas, GastoVehicularDTO dto, Pipa p)
        {
            return recargas.Where(x => x.EsPipa
                                    && x.Id_Vehiculo.Equals(p.IdPipa)
                                    && x.FechaRecarga > dto.FechaInicio
                                    && x.FechaRecarga < dto.FechaInicio)
                                .Sum(recarga => recarga.Monto) ?? 0;
        }
       
        public static decimal CalacuarCostoRecargasCombustibleCamioneta(List<DetalleRecargaCombustible> recargas, GastoVehicularDTO dto, Camioneta p)
        {
            return recargas.Where(x => x.EsCamioneta
                                    && x.Id_Vehiculo.Equals(p.IdCamioneta)
                                    && x.FechaRecarga > dto.FechaInicio
                                    && x.FechaRecarga < dto.FechaInicio)
                                .Sum(recarga => recarga.Monto) ?? 0;
        }        
        public static decimal CalacuarCostoRecargasCombustibleUtilitario(List<DetalleRecargaCombustible> recargas, GastoVehicularDTO dto, CUtilitario p)
        {
            return recargas.Where(x => x.EsUtilitario
                                    && x.Id_Vehiculo.Equals(p.IdUtilitario)
                                    && x.FechaRecarga > dto.FechaInicio
                                    && x.FechaRecarga < dto.FechaInicio)
                                .Sum(recarga => recarga.Monto) ?? 0;
        }       
    }
}
