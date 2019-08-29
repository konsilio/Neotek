using Application.MainModule.DTOs;
using Application.MainModule.DTOs.EquipoTransporte;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Equipo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.EquipoTrasnporteServicio
{
    public class RecargaCombustibleAdapter
    {
        public static RecargaCombustibleDTO ToDTO(DetalleRecargaCombustible entidad)
        {
            return new RecargaCombustibleDTO()
            {
                Id_DetalleRecargaComb = entidad.Id_DetalleRecargaComb,
                Id_Vehiculo = entidad.Id_Vehiculo,
                Vehiculo = EquipoTransporteServicio.ObtenerNombre(entidad),
                Chofer = OperadorChoferServicio.ObtenerNombreCompleto(entidad),
                EsCamioneta = entidad.EsCamioneta,
                EsPipa = entidad.EsPipa,
                EsUtilitario = entidad.EsUtilitario,
                KilometrajeActual = entidad.KilometrajeActual,
                KilometrajeRecorrido = entidad.KilometrajeRecorrido,
                LitrosRecargados = entidad.LitrosRecargados,
                IdTipoCombustible = entidad.IdTipoCombustible,
                FechaRecarga = entidad.FechaRecarga,
                Monto = entidad.Monto ?? 0,
                IdCuentaContable=entidad.IdCuentaContable,
            };
        }
        public static List<RecargaCombustibleDTO> ToDTO(List<DetalleRecargaCombustible> entidad)
        {
            return entidad.Select(x => ToDTO(x)).ToList();
        }
        public static DetalleRecargaCombustible FromDTO(RecargaCombustibleDTO dto)
        {
            return new DetalleRecargaCombustible()
            {
                Id_DetalleRecargaComb = dto.Id_DetalleRecargaComb,
                Id_Vehiculo = dto.Id_Vehiculo,
                EsCamioneta = dto.EsCamioneta,
                EsPipa = dto.EsPipa,
                EsUtilitario = dto.EsUtilitario,
                KilometrajeActual = dto.KilometrajeActual,
                KilometrajeRecorrido = dto.KilometrajeRecorrido,
                LitrosRecargados = dto.LitrosRecargados,
                IdTipoCombustible = dto.IdTipoCombustible,
                FechaRecarga = dto.FechaRecarga,
                Monto = dto.Monto,
                IdCuentaContable = dto.IdCuentaContable,
            };
        }
        public static List<DetalleRecargaCombustible> FromDTO(List<RecargaCombustibleDTO> entidad)
        {
            return entidad.Select(x => FromDTO(x)).ToList();
        }
        public static DetalleRecargaCombustible FormEmtity(DetalleRecargaCombustible entidad)
        {
            return new DetalleRecargaCombustible()
            {
                Id_DetalleRecargaComb = entidad.Id_DetalleRecargaComb,
                Id_Vehiculo = entidad.Id_Vehiculo,
                EsCamioneta = entidad.EsCamioneta,
                EsPipa = entidad.EsPipa,
                EsUtilitario = entidad.EsUtilitario,
                KilometrajeActual = entidad.KilometrajeActual,
                KilometrajeRecorrido = entidad.KilometrajeRecorrido,
                LitrosRecargados = entidad.LitrosRecargados,
                IdTipoCombustible = entidad.IdTipoCombustible,
                FechaRecarga = entidad.FechaRecarga,
                Monto = entidad.Monto ?? 0,
                IdCuentaContable=entidad.IdCuentaContable,
            };
        }
        public static RepRendimientoVehicularDTO FormRepDTO(DetalleRecargaCombustible entidad)
        {
            return new RepRendimientoVehicularDTO()
            {
                IdRegistro = entidad.Id_DetalleRecargaComb,
                Vehiculo = EquipoTransporteServicio.ObtenerNombre(entidad),
                KmInicial = CalculosEquipoTrasporte.ObtenerKilometrajeInicial(entidad),
                KmFinal = entidad.KilometrajeActual,
                KmRecorridos = entidad.KilometrajeRecorrido,
                LitrosCargados = entidad.LitrosRecargados,
                Rendimiento = EquipoTransporteServicio.ObtenerRendimento(entidad),
                Fecha = entidad.FechaRecarga,
            };
        }
        public static List<RepRendimientoVehicularDTO> FormRepDTO(List<DetalleRecargaCombustible> entidades)
        {
            return entidades.Select(x => FormRepDTO(x)).ToList();
        }
    }
}
