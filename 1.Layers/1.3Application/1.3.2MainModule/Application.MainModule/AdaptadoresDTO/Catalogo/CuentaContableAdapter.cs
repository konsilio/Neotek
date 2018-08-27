using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public class CuentaContableAdapter
    {
        public static CuentaContable FromDto(CuentaContableCrearDto cuentaContableDto)
        {
            return new CuentaContable()
            {
                IdEmpresa = cuentaContableDto.IdEmpresa,
                Numero = cuentaContableDto.Numero,
                Descripcion = cuentaContableDto.Descripcion,
                FechaRegistro = DateTime.Now,
                Activo = true,
            };
        }

        public static CuentaContable FromDto(CuentaContableModificarDto cuentaContableDto)
        {
            return new CuentaContable()
            {
                IdCuentaContable = cuentaContableDto.IdCuenta,
                IdEmpresa = cuentaContableDto.IdEmpresa,
                Numero = cuentaContableDto.Numero,
                Descripcion = cuentaContableDto.Descripcion,
                Activo = true,
            };
        }

        public static CuentaContable FromEntity(CuentaContable cuentaContable)
        {
            return new CuentaContable()
            {
                IdCuentaContable = cuentaContable.IdCuentaContable,
                IdEmpresa = cuentaContable.IdEmpresa,
                Numero = cuentaContable.Numero,
                Descripcion = cuentaContable.Descripcion,
                FechaRegistro = cuentaContable.FechaRegistro,
                Activo = cuentaContable.Activo,
            };
        }

        public static CuentaContableDto ToDto(CuentaContable cuentaContable)
        {
            return new CuentaContableDto()
            {
                IdCuentaContable = cuentaContable.IdCuentaContable,
                IdEmpresa = cuentaContable.IdEmpresa,
                Numero = cuentaContable.Numero,
                Descripcion = cuentaContable.Descripcion,
                FechaRegistro = cuentaContable.FechaRegistro,
                Activo = cuentaContable.Activo,
            };
        }

        public static List<CuentaContableDto> ToDto(List<CuentaContable> CuentaContablees)
        {
            return CuentaContablees.ToList()
                              .Select(x => ToDto(x))
                              .ToList();
        }
    }
}
