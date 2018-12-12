/**
 * UsuariosCorteAdapter
 * Adaptador para la lista de usuarios en el 
 * corte y anticipos
 * @Developer: Jorge Omar Tovar Martínez
 * @Date: 11/12/2018
 * @Update: 11/12/2018
 * @company: Neotecks
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile.Cortes;
using Exceptions.MainModule.Validaciones;
using Application.MainModule.Servicios.Catalogos;

namespace Application.MainModule.AdaptadoresDTO.Mobile.Cortes
{
    public class UsuariosCorteAdapter
    {
        public static UsuariosCorteDTO ToDTO(Empresa empresa, List<Usuario> usuariosEmpresa)
        {
            if (empresa != null && usuariosEmpresa != null && usuariosEmpresa.Count > 0)
                return new UsuariosCorteDTO()
                {
                    Mensaje = Exito.OK,
                    Exito = true,
                    IdEmpresa = empresa.IdEmpresa,
                    ModeloValido = true,
                    Usuarios = usuariosEmpresa.Select(x => ToDTO(x)).ToList()
                };
            else
                return new UsuariosCorteDTO()
                {
                    Exito = false,
                    Mensaje = "No hay usuarios para realizar el corte",
                    ModeloValido = false
                                     
                };
        }

        public static UsuarioDTO ToDTO(Usuario usuario)
        {
            
            return new UsuarioDTO()
            {
                  IdEmpresa = usuario.IdEmpresa,
                  IdUsuario = usuario.IdUsuario,
                  Nombre = usuario.Nombre+" "+" "+usuario.Apellido1+" "+usuario.Apellido2

            };
        }
    }
}
