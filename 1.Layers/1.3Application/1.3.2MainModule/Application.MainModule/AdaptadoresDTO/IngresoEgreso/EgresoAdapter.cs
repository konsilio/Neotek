using Application.MainModule.DTOs;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.IngresoEgreso
{
    public static class EgresoAdapter
    {
        public static EgresoDTO ToDTO(Egreso entidad)
        {
            return new EgresoDTO()
            {
                IdEgreso = entidad.IdEgreso,
                IdEmpresa = entidad.IdEmpresa,
                FechaRegistro = entidad.FechaRegistro,
                IdCentroCosto = entidad.IdCentroCosto,
                IdCuentaContable = entidad.IdCuentaContable,
                Monto = entidad.Monto,
                Descripcion = entidad.Descripcion,
                EsExterno = entidad.EsExterno,
                GastoMensual = entidad.GastoMensual,
                EsFiscal = entidad.EsFiscal,
                Activo = entidad.Activo,                
            };
        }
        public static List<EgresoDTO> ToDTO(List<Egreso> entidad)
        {
            return entidad.Select(x => ToDTO(x)).ToList();
        }
        public static Egreso FromDTO(EgresoDTO dto)
        {
            return new Egreso()
            {
                IdEgreso = dto.IdEgreso,
                IdEmpresa = dto.IdEmpresa,
                FechaRegistro = dto.FechaRegistro,
                IdCentroCosto = dto.IdCentroCosto,
                IdCuentaContable = dto.IdCuentaContable,
                Monto = dto.Monto,
                Descripcion = dto.Descripcion,
                EsExterno = dto.EsExterno,
                GastoMensual = dto.GastoMensual,
                EsFiscal = dto.EsFiscal,
                Activo = dto.Activo,
            };
        }
        public static List<Egreso> FromDTO(List<EgresoDTO> entidad)
        {
            return entidad.Select(x => FromDTO(x)).ToList();
        }
        public static Egreso FormEmtity(Egreso entidad)
        {
            return new Egreso()
            {
                IdEgreso = entidad.IdEgreso,
                IdEmpresa = entidad.IdEmpresa,
                FechaRegistro = entidad.FechaRegistro,
                IdCentroCosto = entidad.IdCentroCosto,
                IdCuentaContable = entidad.IdCuentaContable,
                Monto = entidad.Monto,
                Descripcion = entidad.Descripcion,
                EsExterno = entidad.EsExterno,
                GastoMensual = entidad.GastoMensual,
                EsFiscal = entidad.EsFiscal,
                Activo = entidad.Activo,
            };
        }
        public static Egreso FormEmtity(Egreso entidad, EgresoDTO dto)
        {
            return new Egreso()
            {
                IdEgreso = entidad.IdEgreso,
                IdEmpresa = entidad.IdEmpresa,
                FechaRegistro = entidad.FechaRegistro,
                IdCentroCosto = dto.IdCentroCosto,
                IdCuentaContable = dto.IdCuentaContable,
                Monto = dto.Monto,
                Descripcion = dto.Descripcion,
                EsExterno = dto.EsExterno,
                GastoMensual = dto.GastoMensual,
                EsFiscal = dto.EsFiscal,
                Activo = dto.Activo,
            };
        }
        public static RepCuentaPorPagarDTO ToRepo(Egreso entidad)
        {
            return new RepCuentaPorPagarDTO()
            {
                IdCuenta = entidad.IdEgreso,
                Descripcion = entidad.Descripcion,
                CunentaContable = entidad.CCuentaContable.Descripcion,
                SaldoPagado = Convert.ToDouble(entidad.Monto),
                SaldoPasivo = Convert.ToDouble(entidad.Monto),
                SaldoInsoluto = 0,                
            };
        }
        public static List<RepCuentaPorPagarDTO> ToRepo(List<Egreso> entidad)
        {
            return entidad.Select(x => ToRepo(x)).ToList();
        }
    }
}
