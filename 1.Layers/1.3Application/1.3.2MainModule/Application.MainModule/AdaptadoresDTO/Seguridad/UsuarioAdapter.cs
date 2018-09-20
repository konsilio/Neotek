using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Seguridad;
using Sagas.MainModule.Entidades;
using Application.MainModule.Servicios.Catalogos;
using Security.MainModule.Criptografia;
using Application.MainModule.Servicios.Seguridad;

namespace Application.MainModule.AdaptadoresDTO.Seguridad
{
    public static class UsuarioAdapter
    {
        public static UsuarioDTO ToDTO(Usuario us)
        {
            UsuarioDTO usDTO = new UsuarioDTO()
            {
                IdUsuario = us.IdUsuario,
                IdEmpresa = us.IdEmpresa,
                EsAdministracionCentral = us.EsAdministracionCentral,
                EsSuperAdmin = us.EsSuperAdmin,
                Nombre = us.Nombre,
                Apellido1 = us.Apellido1,
                Apellido2 = us.Apellido2,
                NombreCompleto = UsuarioServicio.ObtenerNombreCompleto(us),
                Activo = us.Activo,
                FechaRegistro = us.FechaRegistro,
                NombreUsuario = us.NombreUsuario,
                Password = us.Password,
                Telefono1 = us.Telefono1,
                Telefono2 = us.Telefono2,
                Telefono3 = us.Telefono3,
                Celular1 = us.Celular1,
                Celular2 = us.Celular2,
                Celular3 = us.Celular3,
                Email1 = us.Email1,
                Email2 = us.Email2,
                Email3 = us.Email3,
                SitioWeb1 = us.SitioWeb1,
                SitioWeb2 = us.SitioWeb2,
                SitioWeb3 = us.SitioWeb3,
                IdPais = us.IdPais,
                IdEstadoRep = us.IdEstadoRep,
                EstadoProvincia = us.EstadoProvincia,
                Municipio = us.Municipio,
                CodigoPostal = us.CodigoPostal,
                Colonia = us.Colonia,
                Calle = us.Calle,
                NumExt = us.NumExt,
                NumInt = us.NumInt,
            };

            return usDTO;
        }
        public static List<UsuarioDTO> ToDTO(List<Usuario> lu)
        {
            List<UsuarioDTO> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            return luDTO;
        }
        public static Usuario FromDTO(UsuarioDTO usDTO)
        {
            Usuario us = new Usuario()
            {
                IdUsuario = usDTO.IdUsuario,
                IdEmpresa = usDTO.IdEmpresa,
                EsAdministracionCentral = usDTO.EsAdministracionCentral,
                EsSuperAdmin = usDTO.EsSuperAdmin,
                Nombre = usDTO.Nombre,
                Apellido1 = usDTO.Apellido1,
                Apellido2 = usDTO.Apellido2,
                Activo = usDTO.Activo,
                FechaRegistro = usDTO.FechaRegistro,
                NombreUsuario = usDTO.NombreUsuario,
                Password = usDTO.Password,
                Telefono1 = usDTO.Telefono1,
                Telefono2 = usDTO.Telefono2,
                Telefono3 = usDTO.Telefono3,
                Celular1 = usDTO.Celular1,
                Celular2 = usDTO.Celular2,
                Celular3 = usDTO.Celular3,
                Email1 = usDTO.Email1,
                Email2 = usDTO.Email2,
                Email3 = usDTO.Email3,
                SitioWeb1 = usDTO.SitioWeb1,
                SitioWeb2 = usDTO.SitioWeb2,
                SitioWeb3 = usDTO.SitioWeb3,
                IdPais = usDTO.IdPais,
                IdEstadoRep = usDTO.IdEstadoRep,
                EstadoProvincia = usDTO.EstadoProvincia,
                Municipio = usDTO.Municipio,
                CodigoPostal = usDTO.CodigoPostal,
                Colonia = usDTO.Colonia,
                Calle = usDTO.Calle,
                NumExt = usDTO.NumExt,
                NumInt = usDTO.NumInt,
            };
            return us;
        }
        public static Usuario FromEntity(Usuario user)
        {
            return new Usuario()
            {
                IdUsuario = user.IdUsuario,
                IdEmpresa = user.IdEmpresa,
                EsAdministracionCentral = user.EsAdministracionCentral,
                EsSuperAdmin = user.EsSuperAdmin,
                Nombre = user.Nombre,
                Apellido1 = user.Apellido1,
                Apellido2 = user.Apellido2,
                Activo = user.Activo,
                FechaRegistro = user.FechaRegistro,
                NombreUsuario = user.NombreUsuario,
                Password = user.Password,
                MovileKey = user.MovileKey,
                Telefono1 = user.Telefono1,
                Telefono2 = user.Telefono2,
                Telefono3 = user.Telefono3,
                Celular1 = user.Celular1,
                Celular2 = user.Celular2,
                Celular3 = user.Celular3,
                Email1 = user.Email1,
                Email2 = user.Email2,
                Email3 = user.Email3,
                SitioWeb1 = user.SitioWeb1,
                SitioWeb2 = user.SitioWeb2,
                SitioWeb3 = user.SitioWeb3,
                IdPais = user.IdPais,
                IdEstadoRep = user.IdEstadoRep,
                EstadoProvincia = user.EstadoProvincia,
                Municipio = user.Municipio,
                CodigoPostal = user.CodigoPostal,
                Colonia = user.Colonia,
                Calle = user.Calle,
                NumExt = user.NumExt,
                NumInt = user.NumInt,

            };
        }
        public static List<Usuario> FromDTO(List<UsuarioDTO> luDTO)
        {
            List<Usuario> lu = luDTO.ToList().Select(x => FromDTO(x)).ToList();
            return lu;
        }

        public static Usuario FromDto(UsuarioDTO usDTO)
        {
            return new Usuario()
            {
                IdEmpresa = usDTO.IdEmpresa,
                Nombre = usDTO.Nombre,
                Apellido1 = usDTO.Apellido1,
                Apellido2 = usDTO.Apellido2,
                NombreUsuario = usDTO.Email1,
                Password = usDTO.Password,
                Telefono1 = usDTO.Telefono1,
                Telefono2 = usDTO.Telefono2,
                Telefono3 = usDTO.Telefono3,
                Celular1 = usDTO.Celular1,
                Celular2 = usDTO.Celular2,
                Celular3 = usDTO.Celular3,
                Email1 = usDTO.Email1,
                Email2 = usDTO.Email2,
                Email3 = usDTO.Email3,
                SitioWeb1 = usDTO.SitioWeb1,
                SitioWeb2 = usDTO.SitioWeb2,
                SitioWeb3 = usDTO.SitioWeb3,
                IdPais = usDTO.IdPais,
                IdEstadoRep = usDTO.IdEstadoRep,
                EstadoProvincia = usDTO.EstadoProvincia,
                Municipio = usDTO.Municipio,
                CodigoPostal = usDTO.CodigoPostal,
                Colonia = usDTO.Colonia,
                Calle = usDTO.Calle,
                NumExt = usDTO.NumExt,
                NumInt = usDTO.NumInt,
                Activo = true,
                FechaRegistro = DateTime.Now
            };
        }

        public static UsuariosModel ToDTOEmp(Usuario us)
        {
            UsuariosModel usDTO = new UsuariosModel()
            {
                IdUsuario = us.IdUsuario,
                IdEmpresa = us.IdEmpresa,
                EsAdministracionCentral = us.EsAdministracionCentral,
                EsSuperAdmin = us.EsSuperAdmin,
                Nombre = us.Nombre,
                Apellido1 = us.Apellido1,
                Apellido2 = us.Apellido2,
                NombreCompleto = UsuarioServicio.ObtenerNombreCompleto(us),
                Activo = us.Activo,
                FechaRegistro = us.FechaRegistro,
                NombreUsuario = us.NombreUsuario,
                Password = us.Password,
                Telefono1 = us.Telefono1,
                Telefono2 = us.Telefono2,
                Telefono3 = us.Telefono3,
                Celular1 = us.Celular1,
                Celular2 = us.Celular2,
                Celular3 = us.Celular3,
                Email1 = us.Email1,
                Email2 = us.Email2,
                Email3 = us.Email3,
                SitioWeb1 = us.SitioWeb1,
                SitioWeb2 = us.SitioWeb2,
                SitioWeb3 = us.SitioWeb3,
                IdPais = us.IdPais,
                IdEstadoRep = us.IdEstadoRep,
                EstadoProvincia = us.EstadoProvincia,
                Municipio = us.Municipio,
                CodigoPostal = us.CodigoPostal,
                Colonia = us.Colonia,
                Calle = us.Calle,
                NumExt = us.NumExt,
                NumInt = us.NumInt,
                Empresa = us.Empresa.NombreComercial,
                Roles = getRol(us.UsuarioRoles.ToList()),
                //Roles = RolAdapter.ToDTO(us.Roles.ToList()),
            //    UsuarioRoles = RolAdapter.ToDTO(us.UsuarioRoles.ToList())
               // catUsuario.Roles.Add(getRol(usDTO, catUsuario.IdEmpresa))
        };

            return usDTO;
        }

        public static List<RolDto> getRol(List<UsuarioRol> inf)
        {
            List<RolDto> rl = new List<RolDto>();
          
            foreach (UsuarioRol v in inf)
            {
                RolDto rol = new RolDto();

                rol.IdRol = v.IdRol;
                rol.NombreRol = v.Role.NombreRol;

                rl.Add(rol);
            }

            return rl;
        }

        public static List<UsuariosModel> ToDTOEmpresa(List<Usuario> lu)
        {
            List<UsuariosModel> luDTO = lu.ToList().Select(x => ToDTOEmp(x)).ToList();
            return luDTO;
        }

        public static Usuario FromDtoCredencial(UsuarioDTO usDTO, Usuario usr)
        {
            var catUsuario = FromEntity(usr);
            if (usDTO.Email1 != null) { catUsuario.Email1 = usDTO.Email1; } else { catUsuario.Email1 = catUsuario.Email1; }
            if (usDTO.Password != null) { catUsuario.Password = usDTO.Password; } else { catUsuario.Password = catUsuario.Password; }

            return catUsuario;

            #region seccionAnterior
            //return new Usuario()
            //{
            //    IdEmpresa = usDTO.IdEmpresa,
            //    Nombre = usDTO.Nombre,
            //    Apellido1 = usDTO.Apellido1,
            //    Apellido2 = usDTO.Apellido2,
            //    NombreUsuario = usDTO.Email1,
            //    Password = usDTO.Password,
            //    Telefono1 = usDTO.Telefono1,
            //    Telefono2 = usDTO.Telefono2,
            //    Telefono3 = usDTO.Telefono3,
            //    Celular1 = usDTO.Celular1,
            //    Celular2 = usDTO.Celular2,
            //    Celular3 = usDTO.Celular3,
            //    Email1 = usDTO.Email1,
            //    Email2 = usDTO.Email2,
            //    Email3 = usDTO.Email3,
            //    SitioWeb1 = usDTO.SitioWeb1,
            //    SitioWeb2 = usDTO.SitioWeb2,
            //    SitioWeb3 = usDTO.SitioWeb3,
            //    IdPais = usDTO.IdPais,
            //    IdEstadoRep = usDTO.IdEstadoRep,
            //    EstadoProvincia = usDTO.EstadoProvincia,
            //    Municipio = usDTO.Municipio,
            //    CodigoPostal = usDTO.CodigoPostal,
            //    Colonia = usDTO.Colonia,
            //    Calle = usDTO.Calle,
            //    NumExt = usDTO.NumExt,
            //    NumInt = usDTO.NumInt,
            //    Activo = true,
            //    FechaRegistro = DateTime.Now
            //};
            #endregion
        }

        //public static Usuario FromDtoRol(UsuariosModel usDTO, Usuario usr)
        //{
        //    var catUsuario = FromEntity(usr);
        //    //catUsuario.Roles.Add(getRol(usDTO, catUsuario.IdEmpresa));
        //    catUsuario.Roles.Add(RolAdapter.FromDto(usDTO.Roles.FirstOrDefault()));
        //   // catUsuario.Roles.
        //    return catUsuario;
        //}

        //public static Usuario FromEntity(Usuario user)
        //{
        //    return new Usuario()
        //    {
        //        IdUsuario = user.IdUsuario,            

        //    };
        //}

        public static UsuarioRol FromDtoRol(UsuarioRolDto usDTO)
        {
            //var catUsuario = FromEntity(usr);
            ////catUsuario.Roles.Add(getRol(usDTO, catUsuario.IdEmpresa));
            //catUsuario.Roles.Add(RolAdapter.FromDto(usDTO.Roles.FirstOrDefault()));
            //// catUsuario.Roles.
            //return catUsuario;

            return new UsuarioRol()
            {
                IdRol = usDTO.IdRol,
                IdUsuario = usDTO.IdUsuario,
                Descripcion = usDTO.Descripcion
            };
        }

        //public static Rol getRol(UsuariosModel usrM,short idEmp)
        //{
        //    Rol rl = new Rol();

        //    rl.IdRol = usrM.IdRol;
        //    rl.NombreRol = usrM.Roles[0].NombreRol;
        //    rl.Rol1 = usrM.Roles[0].NombreRol;
        //    rl.FechaRegistro =DateTime.Now;
        //    rl.IdEmpresa = idEmp;

        //    return rl;
        //}

        //public static Usuario getUsr(UsuariosModel usrM)
        //{
        //    Usuario usr = new Usuario();
        //    usr.IdUsuario = usrM.IdUsuario;
        //    usr.FechaRegistro = DateTime.Now;
        //    return usr;
        //}

        public static Usuario FromDTOEditar(UsuarioDTO usuariodto, Usuario catUser)
        {
            var catUsuario = FromEntity(catUser);


            if (usuariodto.IdEmpresa != 0) { catUsuario.IdEmpresa = usuariodto.IdEmpresa; } else { catUsuario.IdEmpresa = catUsuario.IdEmpresa; }
            if (usuariodto.Nombre != null) { catUsuario.Nombre = usuariodto.Nombre; } else catUsuario.Nombre = catUsuario.Nombre;
            if (usuariodto.Apellido1 != null) { catUsuario.Apellido1 = usuariodto.Apellido1; } else catUsuario.Apellido1 = catUsuario.Apellido1;
            if (usuariodto.Apellido2 != null) { catUsuario.Apellido2 = usuariodto.Apellido2; } else catUsuario.Apellido2 = catUsuario.Apellido2;
            if (usuariodto.NombreUsuario != null) { catUsuario.NombreUsuario = usuariodto.NombreUsuario; } else catUsuario.NombreUsuario = catUsuario.NombreUsuario;
            if (usuariodto.Password != null) { catUsuario.Password = usuariodto.Password; } else catUsuario.Password = catUsuario.Password;
            if (usuariodto.Telefono1 != null) { catUsuario.Telefono1 = usuariodto.Telefono1; } else catUsuario.Telefono1 = catUsuario.Telefono1;
            if (usuariodto.Telefono2 != null) { catUsuario.Telefono2 = usuariodto.Telefono2; } else catUsuario.Telefono2 = catUsuario.Telefono2;
            if (usuariodto.Telefono3 != null) catUsuario.Telefono3 = usuariodto.Telefono3; else catUsuario.Telefono3 = catUsuario.Telefono3;
            if (usuariodto.Celular1 != null) { catUsuario.Celular1 = usuariodto.Celular1; } else catUsuario.Celular1 = catUsuario.Celular1;
            if (usuariodto.Celular2 != null) { catUsuario.Celular2 = usuariodto.Celular2; } else catUsuario.Celular2 = catUsuario.Celular2;
            if (usuariodto.Celular3 != null) catUsuario.Celular3 = usuariodto.Celular3; else catUsuario.Celular3 = catUsuario.Celular3;
            if (usuariodto.Email1 != null) { catUsuario.Email1 = usuariodto.Email1; } else catUsuario.Email1 = catUsuario.Email1;
            if (usuariodto.Email2 != null) { catUsuario.Email2 = usuariodto.Email2; } else catUsuario.Email2 = catUsuario.Email2;
            if (usuariodto.Email3 != null) catUsuario.Email3 = usuariodto.Email3; else catUsuario.Email3 = catUsuario.Email3;
            if (usuariodto.SitioWeb1 != null) { catUsuario.SitioWeb1 = usuariodto.SitioWeb1; } else catUsuario.SitioWeb1 = catUsuario.SitioWeb1;
            if (usuariodto.SitioWeb2 != null) { catUsuario.SitioWeb2 = usuariodto.SitioWeb2; } else catUsuario.SitioWeb2 = catUsuario.SitioWeb2;
            if (usuariodto.SitioWeb3 != null) { catUsuario.SitioWeb3 = usuariodto.SitioWeb3; } else catUsuario.SitioWeb3 = catUsuario.SitioWeb3;
            if (usuariodto.IdPais != 0) { catUsuario.IdPais = usuariodto.IdPais; } else { catUsuario.IdPais = catUsuario.IdPais; }
            if (usuariodto.IdEstadoRep != 0) { catUsuario.IdEstadoRep = usuariodto.IdEstadoRep; } else { catUsuario.IdEstadoRep = catUsuario.IdEstadoRep; }
            if (usuariodto.EstadoProvincia != null) catUsuario.EstadoProvincia = usuariodto.EstadoProvincia; else catUsuario.EstadoProvincia = catUsuario.EstadoProvincia;
            if (usuariodto.Municipio != null) catUsuario.Municipio = usuariodto.Municipio; else catUsuario.Municipio = catUsuario.Municipio;
            if (usuariodto.CodigoPostal != null) catUsuario.CodigoPostal = usuariodto.CodigoPostal; else catUsuario.CodigoPostal = catUsuario.CodigoPostal;
            if (usuariodto.Colonia != null) catUsuario.Colonia = usuariodto.Colonia; else catUsuario.Colonia = catUsuario.Colonia;
            if (usuariodto.Calle != null) catUsuario.Calle = usuariodto.Calle; else catUsuario.Calle = catUsuario.Calle;
            if (usuariodto.NumExt != null) catUsuario.NumExt = usuariodto.NumExt; else catUsuario.NumExt = catUsuario.NumExt;
            if (usuariodto.NumInt != null) catUsuario.NumInt = usuariodto.NumInt; else catUsuario.NumInt = catUsuario.NumInt;


            return catUsuario;
        }


    }
}
