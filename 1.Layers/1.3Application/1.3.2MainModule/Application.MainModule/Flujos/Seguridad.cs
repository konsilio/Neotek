using Application.MainModule.AdaptadoresDTO.Seguridad;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Respuesta;
using System.Data;
using Application.MainModule.Servicios.AccesoADatos;

namespace Application.MainModule.Flujos
{
    public class Seguridad
    {
        public DataSet Test()
        {
            List<System.Data.SqlClient.SqlParameter> lp = new List<System.Data.SqlClient.SqlParameter>();
            lp.Add(new System.Data.SqlClient.SqlParameter("IdCliente", 1));
            lp.Add(new System.Data.SqlClient.SqlParameter("Fecha", DBNull.Value));
            lp.Add(new System.Data.SqlClient.SqlParameter("IdEmpresa", 2));
            return new DataAccess().StoredProcedure_DataSet("SpSel_CarteraVencida", lp );
        }
        public RespuestaAutenticacionDto Autenticacion(AutenticacionDto autenticacionDto)
        {
            return AutenticarServicio.AutenticarUsuario(autenticacionDto);
        }
        public RespuestaAutenticacionMobileDto AutenticacionMobile(LoginFbDTO autenticacionDto)
        {
            var responce = AutenticarServicio.AutenticarUsuarioMobile(autenticacionDto);
            if (!responce.Exito)
                return responce;
            var usuario = UsuarioServicio.Obtener(responce.IdUsuario);
            usuario = UsuarioAdapter.FromEntity(usuario);
            usuario.MovileKey = autenticacionDto.FbToken;
            var respuesta = UsuarioServicio.Actualizar(usuario);
            if (!respuesta.Exito)
            {
                responce.Exito = respuesta.Exito;
                responce.EsActulizacion = respuesta.EsActulizacion;
                responce.Codigo = respuesta.Codigo;
                responce.Mensaje = respuesta.Mensaje;
                responce.MensajesError = respuesta.MensajesError;
            }

            return responce;
        }

        #region Usuarios
        public List<UsuariosModel> AllUsers()
        {
            return UsuarioServicio.ListaAllUsuarios().ToList();
        }
        public List<UsuarioDTO> ListaUsuarios(short idEmpresa)
        {
            if (TokenServicio.EsSuperUsuario())
                return UsuarioServicio.ListaUsuarios().Where(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
            else
                return UsuarioServicio.ListaUsuarios().Where(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa())).ToList();
        }
        public RespuestaDto AltaUsuarios(UsuarioDTO userDto)
        {
            var resp = PermisosServicio.PuedeRegistrarUsuario();
            if (!resp.Exito) return resp;

            var usuario = UsuarioAdapter.FromDto(userDto);

            if (!TokenServicio.EsSuperUsuario() && !TokenServicio.ObtenerEsAdministracionCentral())
                usuario.IdEmpresa = TokenServicio.ObtenerIdEmpresa();

            return UsuarioServicio.AltaUsuario(usuario);
        }
        public RespuestaDto ModificaCredencial(UsuarioDTO userDto)
        {
            var resp = PermisosServicio.PuedeModificarUsuario();
            if (!resp.Exito) return resp;

            var user = UsuarioServicio.Obtener(userDto.IdUsuario);
            if (user == null) return UsuarioServicio.NoExiste();

            var emp = UsuarioAdapter.FromDtoCredencial(userDto, user);
            emp.FechaRegistro = emp.FechaRegistro;
            return UsuarioServicio.Actualizar(emp);
        }
        public RespuestaDto ModificaUsuario(UsuarioDTO userDto)
        {
            var resp = PermisosServicio.PuedeModificarUsuario();
            if (!resp.Exito) return resp;

            var user = UsuarioServicio.Obtener(userDto.IdUsuario);
            if (user == null) return UsuarioServicio.NoExiste();

            var emp = UsuarioAdapter.FromDTOEditar(userDto, user);
            emp.FechaRegistro = emp.FechaRegistro;
            return UsuarioServicio.Actualizar(emp);
        }
        public RespuestaDto EliminaUsuario(short id)
        {
            var resp = PermisosServicio.PuedeEliminarUsuario();
            if (!resp.Exito) return resp;

            var user = UsuarioServicio.Obtener(id);
            if (user == null) return UsuarioServicio.NoExiste();

            if (user.EsSuperAdmin)
                return UsuarioServicio.BorrarSuperAdmin();
            

            user = UsuarioAdapter.FromEntity(user);
            user.Activo = false;
            return UsuarioServicio.Actualizar(user);
        }
        public RespuestaDto AsignarRol(UsuarioRolDto uRDto)
        {
            var resp = PermisosServicio.PuedeModificarUsuario();
            if (!resp.Exito) return resp;

            var user = UsuarioServicio.Obtener(uRDto.IdUsuario);
            if (user == null) return UsuarioServicio.NoExiste();

            var rol = RolServicio.Obtener(uRDto.IdRol);
            if (rol == null) return RolServicio.NoExiste();

         //   var userrol = RolServicio.Obtener(uRDto.IdRol, uRDto.IdUsuario);            

            resp = RolServicio.ExisteRol(user, rol,"alta");
            if (!resp.Exito) return resp;

            var usuarioRol = UsuarioAdapter.FromDtoRol(uRDto);
            return UsuarioServicio.Insertar(usuarioRol);
        }
        public RespuestaDto EliminaRolAsignado(UsuarioRolDto usrol)
        {
            var resp = PermisosServicio.PuedeEliminarRol();
            if (!resp.Exito) return resp;

            var user = UsuarioServicio.Obtener(usrol.IdUsuario);
            if (user == null) return UsuarioServicio.NoExiste();

            var rol = RolServicio.Obtener(usrol.IdRol);
            if (rol == null) return RolServicio.NoExiste();

            //var userrol = RolServicio.Obtener(usrol.IdRol, usrol.IdUsuario);
          
            resp = RolServicio.ExisteRol(user, rol,"eliminar");
            if (!resp.Exito) return resp;

            var userol = UsuarioAdapter.FromDtoRol(usrol);
            return UsuarioServicio.Eliminar(userol);
        }
        #endregion

        #region Roles
        public List<RolDto> AllRoles(short id)
        {
            if (TokenServicio.EsSuperUsuario() || TokenServicio.ObtenerEsAdministracionCentral())            
                return RolServicio.ListaAllRoles(id).ToList();
            else            
                return RolServicio.ListaAllRoles(TokenServicio.ObtenerIdEmpresa()).ToList();
        }
        public RespuestaDto AltaRoles(RolDto rolDto)
        {
            var resp = PermisosServicio.PuedeRegistrarRol();
            if (!resp.Exito) return resp;

            var rol = RolAdapter.FromDto(rolDto);

            if (!TokenServicio.EsSuperUsuario() && !TokenServicio.ObtenerEsAdministracionCentral())
                rol.IdEmpresa = TokenServicio.ObtenerIdEmpresa();

            return RolServicio.AltaRol(rol);
        }
        public RespuestaDto ModificaRolName(RolDto rolDto)
        {
            var resp = PermisosServicio.PuedeModificarRol();
            if (!resp.Exito) return resp;

            var rol = RolServicio.Obtener(rolDto.IdRol);
            if (rol == null) return RolServicio.NoExiste();

            var emp = RolAdapter.FromDtoNomRol(rolDto, rol);
            emp.FechaRegistro = emp.FechaRegistro;
            return RolServicio.Actualizar(emp);
            //insertar Rol in data access
        }
        public RespuestaDto ModificaPermisos(List<RolDto> rolDto)
        {
            var resp = PermisosServicio.PuedeModificarRol();
            if (!resp.Exito) return resp;

                var emp = RolAdapter.FromDtoPer(rolDto);
                return RolServicio.Actualizar(emp);           

            //insertar Rol in data access
        }
        public RespuestaDto EliminaRol(short id)
        {
            var resp = PermisosServicio.PuedeEliminarRol();
            if (!resp.Exito) return resp;

            var rol = RolServicio.Obtener(id);
            if (rol == null) return RolServicio.NoExiste();

            rol = RolAdapter.FromEntity(rol);
            rol.Activo = false;
            return RolServicio.Actualizar(rol);
        }
        #endregion

    }
}
