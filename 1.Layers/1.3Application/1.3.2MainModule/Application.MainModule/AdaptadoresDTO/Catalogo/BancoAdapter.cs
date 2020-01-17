using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    class BancoAdapter
    {
        public static BancoDTO ToDTO(Banco entidad)
        {
            return new BancoDTO()
            {
                IdBanco = entidad.IdBanco,
                Clave = entidad.Clave,
                NombreCorto = entidad.NombreCorto,
                NombreRazónSocial = entidad.NombreRazónSocial,
                FechaRegistro = entidad.FechaRegistro
            };
        }
        public static List<BancoDTO> ToDTO(List<Banco> entidades)
        {
            return entidades.Select(x => ToDTO(x)).ToList();
        }
        public static Banco FromDTO(BancoDTO dto)
        {
            return new Banco()
            {
                IdBanco = dto.IdBanco,
                Clave = dto.Clave,
                NombreCorto = dto.NombreCorto,
                NombreRazónSocial = dto.NombreRazónSocial,
                FechaRegistro = dto.FechaRegistro
            };
        }
        public static List<Banco> FromDTO(List<BancoDTO> entidad)
        {
            return entidad.Select(x => FromDTO(x)).ToList();
        }
    }
}
