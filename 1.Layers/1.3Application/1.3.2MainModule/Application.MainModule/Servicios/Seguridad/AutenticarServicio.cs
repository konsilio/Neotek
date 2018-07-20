using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Utilities.MainModule;
using Security.MainModule.Token_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Seguridad
{
    public static class AutenticarServicio
    {
        public static RespuestaAutenticacionDto AutenticarUsuario(AutenticacionDto autDto)
        {
            UsuarioAplicacionDto usuario;
            // Validamos si es un usuario de la administración central
            // y buscamos la existencia del usuario, validando su contraseña
            if (autDto.IdEmpresa.Equals(-1))
                usuario = AutenticarUsuarioAdminCentral(autDto);
            else
                usuario = AutenticarUsuarioDeEmpresa(autDto);

            if (usuario.autenticado)
            {
                var claims = new[]
                {
                    new Claim("Autenticado", usuario.autenticado.ToString()),
                    new Claim("NombreUsuario", autDto.Usuario),
                    new Claim("Rol", usuario.IdRol.ToString()),
                    new Claim("IdUsuario", usuario.IdUsuario.ToString()),
                    new Claim("IdEmpresa", usuario.IdEmpresa.ToString()),
                    new Claim("EsAdminCentral", usuario.AdminCentral ? "true": "false"),
                    new Claim("EsSuperUsuario", usuario.SuperUsuario ? "true": "false"),
                };

                var min = Math.Truncate(FechasFunciones.ObtenerMinutosEntreDosFechas(DateTime.Now, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59)));
                
                return new RespuestaAutenticacionDto()
                {
                    token = TokenGenerator.GenerateTokenJwt(claims, autDto.Password, Convert.ToInt32(min).ToString())
                };
            }
            else
                return new RespuestaAutenticacionDto()
                {
                    Exito = false,
                    Mensaje = Error.S0003,
                    token = string.Empty
                };
        }

        private static UsuarioAplicacionDto AutenticarUsuarioAdminCentral(AutenticacionDto autDto)
        {
            var usuario = new UsuarioACDataAccess().Buscar(autDto.Usuario, autDto.Password);
            if (usuario != null)
                return new UsuarioAplicacionDto()
                {
                    autenticado = true,
                    AdminCentral = true,
                    SuperUsuario = usuario.SuperAdmin,
                    IdUsuario = usuario.IdUsuarioAC,
                    IdRol = usuario.IdRol,
                };
            else
                return new UsuarioAplicacionDto()
                {
                    autenticado = false,
                };
        }

        private static UsuarioAplicacionDto AutenticarUsuarioDeEmpresa(AutenticacionDto autDto)
        {
            var usuario = new UsuarioDataAccess().Buscar(autDto.IdEmpresa, autDto.Usuario, autDto.Password);
            if (usuario != null)
                return new UsuarioAplicacionDto()
                {
                    autenticado = true,
                    AdminCentral = false,
                    SuperUsuario = false,
                    IdEmpresa = usuario.IdEmpresa,
                    IdUsuario = usuario.IdUsuario,
                    IdRol = usuario.IdRol,
                };
            else
                return new UsuarioAplicacionDto()
                {
                    autenticado = false,
                };
        }
    }
}
