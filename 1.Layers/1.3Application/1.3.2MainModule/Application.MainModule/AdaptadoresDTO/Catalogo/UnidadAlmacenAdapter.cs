using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class UnidadAlmacenAdapter
    {
        public static UnidadAlmacenGasDTO ToDTO(UnidadAlmacenGas uag)
        {
            return new UnidadAlmacenGasDTO()
            {
                IdCAlmacenGas = uag.IdCAlmacenGas,
                IdAlmacenGas = uag.IdCAlmacenGas,
                IdEmpresa = uag.IdEmpresa,
                IdTipoAlmacen = uag.IdTipoAlmacen,
                EsGeneral = uag.EsGeneral,
                EsAlterno = uag.EsAlterno,
                Numero = uag.Numero,
                Activo = uag.Activo,
                FechaRegistro = uag.FechaRegistro
            };
        }
        public static List<UnidadAlmacenGasDTO> ToDTO(List<UnidadAlmacenGas> uags)
        {
            return uags.Select(x => ToDTO(x)).ToList();
        }
        public static UnidadAlmacenGas FromDTO(UnidadAlmacenGasDTO uag)
        {
            return new UnidadAlmacenGas()
            {
                IdCAlmacenGas = uag.IdCAlmacenGas,
                IdAlmacenGas = uag.IdCAlmacenGas,
                IdEmpresa = uag.IdEmpresa,
                IdTipoAlmacen = uag.IdTipoAlmacen,
                EsGeneral = uag.EsGeneral,
                EsAlterno = uag.EsAlterno,
                Numero = uag.Numero,
                Activo = uag.Activo,
                FechaRegistro = uag.FechaRegistro
            };
        }
        public static List<UnidadAlmacenGas> FromDTO(List<UnidadAlmacenGasDTO> uags)
        {
            return uags.Select(x => FromDTO(x)).ToList();
        }
    }
}
