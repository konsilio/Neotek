using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class UsuarioAdapter
    {
        public static UsuarioDTO ToDTO(Usuario us)
        {
            UsuarioDTO usDTO = new UsuarioDTO();
            usDTO.IdUsuario = us.IdUsuario;
            usDTO.IdEmpresa = us.IdEmpresa;
            usDTO.IdRol = us.IdRol;
            usDTO.EsAdministracionCentral = us.EsAdministracionCentral;
            usDTO.EsSuperAdmin = us.EsSuperAdmin;
            usDTO.Nombre = us.Nombre;
            usDTO.Apellido1 = us.Apellido1;
            usDTO.Apellido2 = us.Apellido2;
            usDTO.Activo = us.Activo;
            usDTO.FechaRegistro = us.FechaRegistro;
            usDTO.NombreUsuario = us.NombreUsuario;
            usDTO.Password = us.Password;
            usDTO.Telefono1 = us.Telefono1;
            usDTO.Telefono2 = us.Telefono2;
            usDTO.Telefono3 = us.Telefono3;
            usDTO.Celular1 = us.Celular1;
            usDTO.Celular2 = us.Celular2;
            usDTO.Celular3 = us.Celular3;
            usDTO.Email1 = us.Email1;
            usDTO.Email2 = us.Email2;
            usDTO.Email3 = us.Email3;
            usDTO.SitioWeb1 = us.SitioWeb1;
            usDTO.SitioWeb2 = us.SitioWeb2;
            usDTO.SitioWeb3 = us.SitioWeb3;      
            usDTO.IdPais = us.IdPais;
            usDTO.IdEstadoRep = us.IdEstadoRep;
            usDTO.EstadoProvincia = us.EstadoProvincia;
            usDTO.Municipio = us.Municipio;
            usDTO.CodigoPostal = us.CodigoPostal;
            usDTO.Colonia = us.Colonia;
            usDTO.Calle = us.Calle;
            usDTO.NumExt = us.NumExt;
            usDTO.NumInt = us.NumInt;       
            return usDTO;
        }
        public static List<UsuarioDTO> ToDTO(List<Usuario> lu)
        {
            List<UsuarioDTO> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            return luDTO;
        }
        public static Usuario FromDTO(UsuarioDTO usDTO)
        {
            Usuario us = new Usuario();
            us.IdUsuario = usDTO.IdUsuario;
            us.IdEmpresa = usDTO.IdEmpresa;
            us.IdRol = usDTO.IdRol;
            us.EsAdministracionCentral = usDTO.EsAdministracionCentral;
            us.EsSuperAdmin = usDTO.EsSuperAdmin;
            us.Nombre = usDTO.Nombre;
            us.Apellido1 = usDTO.Apellido1;
            us.Apellido2 = usDTO.Apellido2;
            us.Activo = usDTO.Activo;
            us.FechaRegistro = usDTO.FechaRegistro;
            us.NombreUsuario = usDTO.NombreUsuario;
            us.Password = usDTO.Password;
            us.Telefono1 = usDTO.Telefono1;
            us.Telefono2 = usDTO.Telefono2;
            us.Telefono3 = usDTO.Telefono3;
            us.Celular1 = usDTO.Celular1;
            us.Celular2 = usDTO.Celular2;
            us.Celular3 = usDTO.Celular3;
            us.Email1 = usDTO.Email1;
            us.Email2 = usDTO.Email2;
            us.Email3 = usDTO.Email3;
            us.SitioWeb1 = usDTO.SitioWeb1;
            us.SitioWeb2 = usDTO.SitioWeb2;
            us.SitioWeb3 = usDTO.SitioWeb3;
            us.IdPais = usDTO.IdPais;
            us.IdEstadoRep = usDTO.IdEstadoRep;
            us.EstadoProvincia = usDTO.EstadoProvincia;
            us.Municipio = usDTO.Municipio;
            us.CodigoPostal = usDTO.CodigoPostal;
            us.Colonia = usDTO.Colonia;
            us.Calle = usDTO.Calle;
            us.NumExt = usDTO.NumExt;
            us.NumInt = usDTO.NumInt;
            return us;
        }
        public static List<Usuario> FromDTO(List<UsuarioDTO> luDTO)
        {
            List<Usuario> lu = luDTO.ToList().Select(x => FromDTO(x)).ToList();
            return lu;
        }
    }
}
