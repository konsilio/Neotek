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
        public static CuentaContable ToDTO(CuentaContableDTO dto)
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
        public static CuentaContableDTO FromDTO(CuentaContable cc)
        {
            CuentaContableDTO ccDTO = new CuentaContableDTO()
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
        public static List<CuentaContableDTO> FromDTO(List<CuentaContable> ccs)
        {
            List<CuentaContableDTO> lccDTO = ccs.ToList().Select(x => FromDTO(x)).ToList();
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
