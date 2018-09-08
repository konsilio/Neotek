using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
   public static class PaisAdapter
    {

        public static PaisDTO ToDTO(Pais _pais)
        {
            PaisDTO paisDTO = new PaisDTO();
            paisDTO.IdPais = _pais.IdPais;
            paisDTO.Pais = _pais.PaisNombre;

            return paisDTO;
        }
        public static List<PaisDTO> ToDTO(List<Pais> lPais)
        {
            List<PaisDTO> lprodDTO = lPais.ToList().Select(x => ToDTO(x)).ToList();
            return lprodDTO;
        }
        public static Pais FromDTO(PaisDTO pDTO)
        {
            Pais _p = new Pais();
            _p.IdPais = pDTO.IdPais;
            
            return _p;
        }
        public static List<Pais> FromDTO(List<PaisDTO> lPDTO)
        {
            List<Pais> lpais = lPDTO.ToList().Select(x => FromDTO(x)).ToList();
            return lpais;
        }
        public static Pais FromEntity(Pais pAnterior)
        {
            return new Pais()
            {
                IdPais = pAnterior.IdPais,
                 PaisNombre = pAnterior.PaisNombre
            };
        }

        public static List<Pais> FromEntity(List<Pais> lPDTO)
        {
            return lPDTO.ToList().Select(x => FromEntity(x)).ToList();
        }
    }
}
