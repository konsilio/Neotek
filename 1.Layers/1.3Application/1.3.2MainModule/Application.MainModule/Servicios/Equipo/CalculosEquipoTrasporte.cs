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
        public static decimal CalcularCostoMantenimientosPipa(List<DetalleMantenimiento> mantenimientos, GastoVehicularDTO dto, Pipa p)
        {
            return mantenimientos.Where(x => x.EsPipa 
                                        && x.id_vehiculo.Equals(p.IdPipa) 
                                        && x.FechaMtto > dto.FechaInicio
                                        && x.FechaMtto < dto.FechaFin)
                                    .Sum(recarga => recarga.Monto) ?? 0;
        }
        public static decimal CalcularCostoMantenimientosCamioneta(List<DetalleMantenimiento> mantenimientos, GastoVehicularDTO dto, Camioneta c)
        {
            return mantenimientos.Where(x => x.EsCamioneta
                                        && x.id_vehiculo.Equals(c.IdCamioneta)
                                        && x.FechaMtto > dto.FechaInicio
                                        && x.FechaMtto < dto.FechaFin)
                                    .Sum(recarga => recarga.Monto) ?? 0;
        }
        public static decimal CalcularCostoMantenimientosUtilitario(List<DetalleMantenimiento> mantenimientos, GastoVehicularDTO dto, CUtilitario c)
        {
            return mantenimientos.Where(x => x.EsUtilitario
                                        && x.id_vehiculo.Equals(c.IdUtilitario)
                                        && x.FechaMtto > dto.FechaInicio
                                        && x.FechaMtto < dto.FechaFin)
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
                                    && x.FechaRecarga < dto.FechaFin)
                                .Sum(recarga => recarga.Monto) ?? 0;
        }
       
        public static decimal CalacuarCostoRecargasCombustibleCamioneta(List<DetalleRecargaCombustible> recargas, GastoVehicularDTO dto, Camioneta p)
        {
            return recargas.Where(x => x.EsCamioneta
                                    && x.Id_Vehiculo.Equals(p.IdCamioneta)
                                    && x.FechaRecarga > dto.FechaInicio
                                    && x.FechaRecarga < dto.FechaFin)
                                .Sum(recarga => recarga.Monto) ?? 0;
        }        
        public static decimal CalacuarCostoRecargasCombustibleUtilitario(List<DetalleRecargaCombustible> recargas, GastoVehicularDTO dto, CUtilitario p)
        {
            return recargas.Where(x => x.EsUtilitario
                                    && x.Id_Vehiculo.Equals(p.IdUtilitario)
                                    && x.FechaRecarga > dto.FechaInicio
                                    && x.FechaRecarga < dto.FechaFin)
                                .Sum(recarga => recarga.Monto) ?? 0;
        }       
    }
}
