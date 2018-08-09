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
using Application.MainModule.Servicios.Mobile;

namespace Application.MainModule.Servicios.Seguridad
{
    public static class AutenticarServicio
    {
        public static RespuestaAutenticacionDto AutenticarUsuario(AutenticacionDto autDto)
        {
            UsuarioAplicacionDto usuario;
            // Validamos si es un usuario de la administración central
            // y buscamos la existencia del usuario, validando su contraseña
            usuario = AutenticarUsuarioDeEmpresa(autDto);

            if (usuario.autenticado)
            {
                var claims = new[]
                {
                    new Claim("Autenticado", usuario.autenticado.ToString()),
                    new Claim("NombreUsuario", autDto.Usuario),
                    new Claim("IdUsuario", usuario.IdUsuario.ToString()),
                    new Claim("IdEmpresa", usuario.IdEmpresa.ToString()),
                    new Claim("EsAdminCentral", usuario.AdminCentral ? "true": "false"),
                    new Claim("EsSuperUsuario", usuario.SuperUsuario ? "true": "false"),
                };

                var min = Math.Truncate(FechasFunciones.ObtenerMinutosEntreDosFechas(DateTime.Now, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59)));

                return new RespuestaAutenticacionDto()
                {
                    IdUsuario = usuario.IdUsuario,
                    Exito = true,
                    Mensaje = "OK",
                    token = TokenGenerator.GenerateTokenJwt(claims, autDto.Password, Convert.ToInt32(min).ToString())
                };
            }
            else
                return new RespuestaAutenticacionDto()
                {
                    IdUsuario = 0,
                    Exito = false,
                    Mensaje = Error.S0003,
                    token = string.Empty
                };
        } 
        
        public static RespuestaAutenticacionMobileDto AutenticarUsuarioMobile(AutenticacionDto autDto)
        {
            var aut = AutenticarUsuario(autDto);
            return new RespuestaAutenticacionMobileDto()
            {
                IdUsuario = aut.IdUsuario,
                Exito = aut.Exito,
                Mensaje = aut.Mensaje,
                token = aut.token,
                listMenu = aut.Exito ? MenuServicio.Crear(aut.IdUsuario) : null,
            };

        
        }       

        private static UsuarioAplicacionDto AutenticarUsuarioDeEmpresa(AutenticacionDto autDto)
        {
            var usuario = new UsuarioDataAccess().Buscar(autDto.IdEmpresa, autDto.Usuario, autDto.Password);
            if (usuario != null)
            {
                var autUsuario =  new UsuarioAplicacionDto()
                {
                    autenticado = true,
                    SuperUsuario = usuario.EsSuperAdmin,
                    IdEmpresa = usuario.IdEmpresa,
                    IdUsuario = usuario.IdUsuario,            
                };

                if (usuario.EsAdministracionCentral)
                    autUsuario.AdminCentral = true;
                else
                    autUsuario.AdminCentral = false;

                return autUsuario;
            }
            else
                return new UsuarioAplicacionDto()
                {
                    autenticado = false,
                };
        }
    }
}
