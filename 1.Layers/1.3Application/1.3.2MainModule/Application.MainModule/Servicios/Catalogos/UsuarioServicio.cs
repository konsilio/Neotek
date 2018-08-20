﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class UsuarioServicio
    {
        public static List<UsuarioDTO> ListaUsuarios(short idEmpresa)
        {
            List<UsuarioDTO> lUsuarios = AdaptadoresDTO.Catalogo.UsuarioAdapter.ToDTO(new UsuarioDataAccess().Buscar(idEmpresa));
            return lUsuarios;
        }
        public static List<UsuarioDTO> ListaUsuarios()
        {
            List<UsuarioDTO> lUsuarios = AdaptadoresDTO.Catalogo.UsuarioAdapter.ToDTO(new UsuarioDataAccess().BuscarTodos());
            return lUsuarios;
        }

        public static Usuario Obtener(int idUsuario)
        {
            return new UsuarioDataAccess().Buscar(idUsuario);
        }

        public static string ObtenerNombreCompleto(Usuario usuario)
        {
            return string.Concat(usuario.Nombre, " ", usuario.Apellido1, " ", usuario.Apellido2);
        }
    }
}
