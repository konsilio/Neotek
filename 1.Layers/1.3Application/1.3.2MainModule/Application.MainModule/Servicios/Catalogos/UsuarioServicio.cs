using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class UsuarioServicio
    {
        public static List<UsuarioDTO> ListaUsuarios(short idEmpresa)
        {//Se descartaran los usuarios de administracion central.
            List<UsuarioDTO> lUsuarios = AdaptadoresDTO.Catalogo.UsuarioAdapter.ToDTO(new UsuarioDataAccess().Buscar(idEmpresa));
            return lUsuarios;
        }
    }
}
