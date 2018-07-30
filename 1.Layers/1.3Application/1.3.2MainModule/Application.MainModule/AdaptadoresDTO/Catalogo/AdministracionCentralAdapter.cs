using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class AdministracionCentralAdapter
    {
        public static AdministracionCentralDto ToDTO(AdministracionCentral ac)
        {
            AdministracionCentralDto acDTO = new AdministracionCentralDto()
            {
                IdAdministracionCentral = ac.IdAdministracionCentral,
                NombreComercial = ac.NombreComercial,
                FechaRegistro = ac.FechaRegistro,
                IdPais = ac.IdPais,
                IdEstadoRep = ac.IdEstadoRep,
                EstadoProvincia = ac.EstadoProvincia,
                Municipio = ac.Municipio,
                CodigoPostal = ac.CodigoPostal,
                Colonia = ac.Colonia,
                Calle = ac.Calle,
                NumExt = ac.NumExt,
                NumInt = ac.NumInt,
                Rfc = ac.Rfc,
                RazonSocial = ac.RazonSocial,
                UrlLogotipoMenu = ac.UrlLogotipoMenu,
                UrlLogotipoLogin = ac.UrlLogotipoLogin,
                Telefono1 = ac.Telefono1,
                Telefono2 = ac.Telefono2,
                Telefono3 = ac.Telefono3,
                Celular1 = ac.Celular1,
                Celular2 = ac.Celular2,
                Celular3 = ac.Celular3,
                Email1 = ac.Email1,
                Email2 = ac.Email2,
                Email3 = ac.Email3,
                SitioWeb1 = ac.SitioWeb1,
                SitioWeb2 = ac.SitioWeb2,
                SitioWeb3 = ac.SitioWeb3
            };
            return acDTO;
        }
        public static List<AdministracionCentralDto> ToDTO(List<AdministracionCentral> lac)
        {
            List<AdministracionCentralDto> lacDTO = lac.ToList().Select(x => ToDTO(x)).ToList();
            return lacDTO;
        }

        public static AdministracionCentral FromDTO(AdministracionCentralDto acDTO)
        {
            AdministracionCentral ac = new AdministracionCentral()
            {
                IdAdministracionCentral = acDTO.IdAdministracionCentral,
                NombreComercial = acDTO.NombreComercial,
                FechaRegistro = acDTO.FechaRegistro,
                IdPais = acDTO.IdPais,
                IdEstadoRep = acDTO.IdEstadoRep,
                EstadoProvincia = acDTO.EstadoProvincia,
                Municipio = acDTO.Municipio,
                CodigoPostal = acDTO.CodigoPostal,
                Colonia = acDTO.Colonia,
                Calle = acDTO.Calle,
                NumExt = acDTO.NumExt,
                NumInt = acDTO.NumInt,
                Rfc = acDTO.Rfc,
                RazonSocial = acDTO.RazonSocial,
                UrlLogotipoMenu = acDTO.UrlLogotipoMenu,
                UrlLogotipoLogin = acDTO.UrlLogotipoLogin,
                Telefono1 = acDTO.Telefono1,
                Telefono2 = acDTO.Telefono2,
                Telefono3 = acDTO.Telefono3,
                Celular1 = acDTO.Celular1,
                Celular2 = acDTO.Celular2,
                Celular3 = acDTO.Celular3,
                Email1 = acDTO.Email1,
                Email2 = acDTO.Email2,
                Email3 = acDTO.Email3,
                SitioWeb1 = acDTO.SitioWeb1,
                SitioWeb2 = acDTO.SitioWeb2,
                SitioWeb3 = acDTO.SitioWeb3
            };
            return ac;
        }
        public static List<AdministracionCentral> FromDTO(List<AdministracionCentralDto> lacDTO)
        {
            List<AdministracionCentral> lac = lacDTO.ToList().Select(x => FromDTO(x)).ToList();
            return lac;
        }
    }
}
