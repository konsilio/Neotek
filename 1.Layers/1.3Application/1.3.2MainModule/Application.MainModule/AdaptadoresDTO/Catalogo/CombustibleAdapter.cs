using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class CombustibleAdapter
    {
        public static CombustibleDTO toDTO(CCombustible ec)
        {
            return new CombustibleDTO()
            {
                Id_Combustible = ec.Id_Combustible,
                TipoCombustible = ec.TipoCombustible,
                Descripcion = ec.Descripcion,
                Activo = ec.Activo,
                Id_Empresa = ec.Id_Empresa,
            };
        }
        public static List<CombustibleDTO> toDTO(List<CCombustible> ecs)
        {
            return ecs.Select(x => toDTO(x)).ToList();
        }
        public static CCombustible FromDTO(CombustibleDTO ec)
        {
            return new CCombustible()
            {
                Id_Combustible = ec.Id_Combustible,
                TipoCombustible = ec.TipoCombustible,
                Descripcion = ec.Descripcion,
                Activo = ec.Activo,
                Id_Empresa = ec.Id_Empresa,
            };
        }
        public static List<CCombustible> FromDTO(List<CombustibleDTO> ecs)
        {
            return ecs.Select(x => FromDTO(x)).ToList();
        }

        public static CCombustible FromDto(CombustibleDTO EntidadDto, CCombustible catCte)
        {
            var _ent = FromEntity(catCte);
            _ent.Id_Combustible = EntidadDto.Id_Combustible;
            _ent.TipoCombustible = EntidadDto.TipoCombustible;
            _ent.Descripcion = EntidadDto.Descripcion;
            _ent.Activo = EntidadDto.Activo;
            _ent.Id_Empresa = EntidadDto.Id_Empresa;
            return _ent;
        }
        public static CCombustible FromEntity(CCombustible ec)
        {
            return new CCombustible()
            {
                Id_Combustible = ec.Id_Combustible,
                TipoCombustible = ec.TipoCombustible,
                Descripcion = ec.Descripcion,
                Activo = ec.Activo,
                Id_Empresa = ec.Id_Empresa,
            };
        }
    }
}
