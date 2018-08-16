using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using Application.MainModule.Servicios.Catalogos;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
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
        public static List<Usuario> FromDTO(List<UsuarioDTO> luDTO)
        {
            List<Usuario> lu = luDTO.ToList().Select(x => FromDTO(x)).ToList();
            return lu;
        }
    }
}
