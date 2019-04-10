using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class EstacionCarburacionAdapter
    {
        public static EstacionCarburacionDTO toDTO(EstacionCarburacion ec)
        {
            return new EstacionCarburacionDTO()
            {
                IdEmpresa = ec.IdEmpresa,
                IdEstacionCarburacion = ec.IdEstacionCarburacion,
                Nombre = ec.Nombre,
                Numero = ec.Numero,
                FechaRegistro = ec.FechaRegistro,
                Activo = ec.Activo
            };
        }
        public static List<EstacionCarburacionDTO> toDTO(List<EstacionCarburacion> ecs)
        {
            return ecs.Select(x => toDTO(x)).ToList();
        }
        public static EstacionCarburacion FromDTO(EstacionCarburacionDTO ec)
        {
            return new EstacionCarburacion()
            {
                IdEmpresa = ec.IdEmpresa,
                IdEstacionCarburacion = ec.IdEstacionCarburacion,
                Nombre = ec.Nombre,
                Numero = ec.Numero,
                FechaRegistro = ec.FechaRegistro,
                Activo = ec.Activo,
            };
        }
        public static List<EstacionCarburacion> FromDTO(List<EstacionCarburacionDTO> ecs)
        {
            return ecs.Select(x => FromDTO(x)).ToList();
        }
        public static EstacionCarburacion FromEmtity(EstacionCarburacion ec)
        {
            return new EstacionCarburacion()
            {
                IdEmpresa = ec.IdEmpresa,
                IdEstacionCarburacion = ec.IdEstacionCarburacion,
                Nombre = ec.Nombre,
                Numero = ec.Numero,
                FechaRegistro = ec.FechaRegistro,
                Activo = ec.Activo,
                Serie = ec.Serie,
                Folio = ec.Folio,
            };
        }
    }
}
