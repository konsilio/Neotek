using Application.MainModule.DTOs;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class CuentaContableAutorizadoAdapter
    {
        public static CuentaContableAutorizado FromDTO(CuentaContableAutorizadoDTO dto)
        {
            return new CuentaContableAutorizado()
            {
                IdCuentaContable = dto.IdCuentaContable,
                Autorizado = dto.Autorizado,
                Fecha = dto.Fecha,
            };
        }
        public static List<CuentaContableAutorizado> FromDTO(List<CuentaContableAutorizadoDTO> dto)
        {
            return dto.Select(x => FromDTO(x)).ToList();
        }
        public static CuentaContableAutorizadoDTO ToDTO(CuentaContableAutorizado Entidad)
        {
            return new CuentaContableAutorizadoDTO()
            {
                IdCuentaContable = Entidad.IdCuentaContable,
                Autorizado = Entidad.Autorizado,
                Fecha = Entidad.Fecha,
            };
        }
        public static List<CuentaContableAutorizadoDTO> ToDTO(List<CuentaContableAutorizado> entidad)
        {
            return entidad.Select(x => ToDTO(x)).ToList();
        }
        public static CuentaContableAutorizado FromEntity(CuentaContableAutorizado Entidad)
        {
            return new CuentaContableAutorizado()
            {
                IdCuentaContable = Entidad.IdCuentaContable,
                Autorizado = Entidad.Autorizado,
                Fecha = Entidad.Fecha,
            };
        }
    }
}
