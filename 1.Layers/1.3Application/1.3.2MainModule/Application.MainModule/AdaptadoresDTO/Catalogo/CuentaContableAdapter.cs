using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class CuentaContableAdapter
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
        }        public static CuentaContable FromEntity(CuentaContable cuentaContable)
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
                Empresa = cuentaContable.Empresa.NombreComercial,
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

        public static CuentaContable ToDTO(CuentaContableDto dto)
        {
            CuentaContable cc = new CuentaContable()
            {
                IdCuentaContable = dto.IdCuentaContable,
                Numero = dto.Numero,
                Descripcion = dto.Descripcion,
                FechaRegistro = dto.FechaRegistro,
                IdEmpresa = dto.IdEmpresa,
                Activo = dto.Activo
            };
            return cc;
        }
        public static CuentaContableDto FromDto(CuentaContable cc)
        {
            CuentaContableDto ccDTO = new CuentaContableDto()
            {
                IdCuentaContable = cc.IdCuentaContable,
                Numero = cc.Numero,
                Descripcion = cc.Descripcion,
                FechaRegistro = cc.FechaRegistro,
                IdEmpresa = cc.IdEmpresa,
                Activo = cc.Activo
            };
            return ccDTO;
        }
        public static List<CuentaContableDto> FromDto(List<CuentaContable> ccs)
        {
            List<CuentaContableDto> lccDTO = ccs.ToList().Select(x => FromDto(x)).ToList();
            return lccDTO;
        }
        public static CuentaContable FromEmtyte(CuentaContable cc)
        {
            return new CuentaContable
            {
                IdCuentaContable = cc.IdCuentaContable,
                Numero = cc.Numero,
                FechaRegistro = cc.FechaRegistro,
                Descripcion = cc.Descripcion,
                IdEmpresa = cc.IdEmpresa,
                Activo = true
            };
        }
    }
}
