using Application.MainModule.DTOs;
using Application.MainModule.DTOs.EquipoTransporte;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Equipo;
using Application.MainModule.Servicios.Ventas;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.MainModule;

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
                IdCuentaContable = entidad.IdCuentaContable,
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
                IdCuentaContable = entidad.IdCuentaContable,
            };
        }
        public static RepRendimientoVehicularDTO FormRepDTO(DetalleRecargaCombustible entidad, RendimientoVehicularDTO dto = null)
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
        public static List<RepRendimientoVehicularDTO> FormRepDTO(List<DetalleRecargaCombustible> entidades, RendimientoVehicularDTO dto = null)
        {
            return entidades.Select(x => FormRepDTO(x, dto)).ToList();
        }

        public static RendimientoVehicularCamionetaDTO FormRepDTO(PuntoVenta pv, PeriodoDTO dtop)
        {
            //var pv = PuntoVentaServicio.Obtener(DTORecarga);
            var recargas = RecargaCombustibleServicio.Buscar(dtop.FechaInicio, dtop.FechaFin, pv);
            RendimientoVehicularCamionetaDTO dto = new RendimientoVehicularCamionetaDTO();
            dto.Unidad = pv.UnidadesAlmacen.Numero;
            dto.DiasTrabajados = pv.VentaPuntoDeVenta.Where(x => x.FechaRegistro >= dtop.FechaInicio && x.FechaRegistro <= dtop.FechaFin).Select(y => y.FechaRegistro).Distinct().Count();
            dto.MantenimientoMensual = MantenimientoDetalleServicio.Buscar(dtop.FechaInicio, dtop.FechaFin, pv).Sum(x => x.Monto ?? 0);
            dto.CarburacionMensualKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(recargas.Sum(x => x.LitrosRecargados), (decimal)0.54);
            dto.CombustibleDiarioKg = dto.CarburacionMensualKg != 0 ? (dto.CarburacionMensualKg / dto.DiasTrabajados) : 0;
            dto.MantenimientoDiario = dto.MantenimientoMensual != 0 ? (dto.MantenimientoMensual / dto.DiasTrabajados) : 0;
            dto.CombustibleDiario = recargas.Sum(x => x.Monto ?? 0) != 0 ? (recargas.Sum(x => x.Monto ?? 0) / dto.DiasTrabajados) : 0;
            dto.GastosDiarios = (dto.MantenimientoDiario + dto.CombustibleDiario + dto.Comisiones);

            dto.Comisiones = CalculosGenerales.Truncar(CalcularPreciosVentaServicio.CalcularComision(pv.VentaPuntoDeVenta.Where(x => x.FechaRegistro >= dtop.FechaInicio && x.FechaRegistro <= dtop.FechaFin).ToList(), dtop),2);
            dto.KgVendidos = pv.VentaPuntoDeVenta.Where(x => x.FechaRegistro >= dtop.FechaInicio && x.FechaRegistro <= dtop.FechaFin).Sum(x => x.VentaPuntoDeVentaDetalle.Sum(e => e.CantidadKg ?? 0));
            dto.VentaDiariaKg = CalculosGenerales.Truncar(dto.KgVendidos != 0 ? (dto.KgVendidos / dto.DiasTrabajados) : 0,2) ;
            //dto.PtoEquilibrioDiario = Definir
            //dto.UtilidadDiaria = Definir
            //dto.Sueldo Definir
            //dto.Celular = Definir
            return dto;
        }
        public static List<RendimientoVehicularCamionetaDTO> FormRepDTOCamioneta(List<PuntoVenta> entidades, PeriodoDTO dto = null)
        {
            return entidades.Select(x => FormRepDTO(x, dto)).ToList();
        }

        public static RendimientoVehicularPipasDTO FormRepDTOP(PuntoVenta pv, PeriodoDTO dtop)
        {
            //var pv = PuntoVentaServicio.Obtener(DTORecarga);
            var recargas = RecargaCombustibleServicio.Buscar(dtop.FechaInicio, dtop.FechaFin, pv);
            RendimientoVehicularPipasDTO dto = new RendimientoVehicularPipasDTO();
            dto.Unidad = pv.UnidadesAlmacen.Numero; 
            dto.DiasTrabajados = pv.VentaPuntoDeVenta.Where(x => x.FechaRegistro >= dtop.FechaInicio && x.FechaRegistro <= dtop.FechaFin).Select(y => y.FechaRegistro).Distinct().Count();
            dto.MantenimientoMensual = MantenimientoDetalleServicio.Buscar(dtop.FechaInicio, dtop.FechaFin, pv).Sum(x => x.Monto ?? 0);           
            dto.CarburacionMensualLt = CalcularGasServicio.ObtenerKilogramosDesdeLitros(recargas.Sum(x => x.LitrosRecargados), (decimal)0.54);            
            dto.CombustibleDiarioLt = dto.CarburacionMensualLt != 0 ? (dto.CarburacionMensualLt / dto.DiasTrabajados) : 0;
            dto.MantenimientoDiario = dto.MantenimientoMensual != 0 ? (dto.MantenimientoMensual / dto.DiasTrabajados) : 0;
            dto.CombustibleDiario = recargas.Sum(x => x.Monto ?? 0) != 0 ? (recargas.Sum(x => x.Monto ?? 0) / dto.DiasTrabajados) : 0;
            dto.GastosDiarios = (dto.MantenimientoDiario + dto.CombustibleDiario + dto.Comisiones);

            dto.Comisiones = CalculosGenerales.Truncar(CalcularPreciosVentaServicio.CalcularComision(pv.VentaPuntoDeVenta.Where(x => x.FechaRegistro >= dtop.FechaInicio && x.FechaRegistro <= dtop.FechaFin).ToList(), dtop), 2);
            dto.KgVendidos = pv.VentaPuntoDeVenta.Where(x => x.FechaRegistro >= dtop.FechaInicio && x.FechaRegistro <= dtop.FechaFin).Sum(x => x.VentaPuntoDeVentaDetalle.Sum(e => e.CantidadKg ?? 0));
            dto.VentaDiariaKg = CalculosGenerales.Truncar(dto.KgVendidos != 0 ? (dto.KgVendidos / dto.DiasTrabajados) : 0, 2);
            //dto.PtoEquilibrioDiario = Definir
            //dto.UtilidadDiaria = Definir
            //dto.Sueldo Definir
            //dto.Celular = Definir
            return dto;
        }
        public static List<RendimientoVehicularPipasDTO> FormRepDTOPipas(List<PuntoVenta> entidades, PeriodoDTO dto = null)
        {
            return entidades.Select(x => FormRepDTOP(x, dto)).ToList();
        }


    }
}
