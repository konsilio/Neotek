using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
   public class OperadorChoferAdapter
    {
        public static OperadorChoferDTO ToOperador(OperadorChofer lu)
        {
            OperadorChoferDTO _opeDTO = new OperadorChoferDTO()
            {
                IdOperadorChofer = lu.IdOperadorChofer,
                IdTipoOperadorChofer = lu.IdTipoOperadorChofer,
                IdEmpresa = lu.IdEmpresa,
                IdUsuario = lu.IdUsuario,
                Activo = lu.Activo,
                FechaRegistro = lu.FechaRegistro,
                Nombre = UsuarioServicio.Obtener(lu.IdUsuario).Nombre,
                Apellido1 = UsuarioServicio.Obtener(lu.IdUsuario).Apellido1,
                Apellido2 = UsuarioServicio.Obtener(lu.IdUsuario).Apellido2,
            };
            return _opeDTO;
        }
        public static List<OperadorChoferDTO> ToUsuariosOpe(List<OperadorChofer> lu)
        {
            List<OperadorChoferDTO> luDTO = lu.ToList().Select(x => ToOperador(x)).ToList();
            return luDTO;
        }
    }
}
