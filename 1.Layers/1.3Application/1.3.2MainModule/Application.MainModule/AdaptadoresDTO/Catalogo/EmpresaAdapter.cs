using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class EmpresaAdapter
    {
        public static EmpresaDTO ToDTO(Empresa empresa)
        {
            EmpresaDTO empresaDto = new EmpresaDTO()
            {
                IdEmpresa = empresa.IdEmpresa,
                EsAdministracionCentral = empresa.EsAdministracionCentral,
                NombreComercial = empresa.NombreComercial,
                FechaRegistro = empresa.FechaRegistro,
                IdPais = empresa.IdPais,
                IdEstadoRep = empresa.IdEstadoRep,
                EstadoProvincia = empresa.EstadoProvincia,
                Municipio = empresa.Municipio,
                CodigoPostal = empresa.CodigoPostal,
                Colonia = empresa.Colonia,
                Calle = empresa.Calle,
                NumExt = empresa.NumExt,
                NumInt = empresa.NumInt,
                Telefono1 = empresa.Telefono1,
                Telefono2 = empresa.Telefono2,
                Telefono3 = empresa.Telefono3,
                Celular1 = empresa.Celular1,
                Celular2 = empresa.Celular2,
                Celular3 = empresa.Celular3,
                Email1 = empresa.Email1,
                Email2 = empresa.Email2,
                Email3 = empresa.Email3,
                SitioWeb1 = empresa.SitioWeb1,
                SitioWeb2 = empresa.SitioWeb2,
                SitioWeb3 = empresa.SitioWeb3,
                Rfc = empresa.Rfc,
                RazonSocial = empresa.RazonSocial,
                FactorLitrosAKilos = empresa.FactorLitrosAKilos,
                CierreInventario = empresa.CierreInventario,
                InventarioSano = empresa.InventarioSano,
                InventarioCrítico = empresa.InventarioCrítico,
                MaxRemaGaseraMensual = empresa.MaxRemaGaseraMensual,
                UrlLogotipoMenu = empresa.UrlLogotipoMenu,
                UrlLogotipoLogin = empresa.UrlLogotipoLogin
            };
            return empresaDto;
        }
        public static List<EmpresaDTO> ToDTO(List<Empresa> empresas)
        {
            List<EmpresaDTO> empreasDT = empresas.ToList().Select(x => ToDTO(x)).ToList();
            return empreasDT;
        }

        public static Empresa FromDTO(EmpresaDTO empresadto)
        {
            Empresa empresa = new Empresa()
            {
                IdEmpresa = empresadto.IdEmpresa,
                EsAdministracionCentral = empresadto.EsAdministracionCentral,
                NombreComercial = empresadto.NombreComercial,
                FechaRegistro = empresadto.FechaRegistro,
                IdPais = empresadto.IdPais,
                IdEstadoRep = empresadto.IdEstadoRep,
                EstadoProvincia = empresadto.EstadoProvincia,
                Municipio = empresadto.Municipio,
                CodigoPostal = empresadto.CodigoPostal,
                Colonia = empresadto.Colonia,
                Calle = empresadto.Calle,
                NumExt = empresadto.NumExt,
                NumInt = empresadto.NumInt,
                Telefono1 = empresadto.Telefono1,
                Telefono2 = empresadto.Telefono2,
                Telefono3 = empresadto.Telefono3,
                Celular1 = empresadto.Celular1,
                Celular2 = empresadto.Celular2,
                Celular3 = empresadto.Celular3,
                Email1 = empresadto.Email1,
                Email2 = empresadto.Email2,
                Email3 = empresadto.Email3,
                SitioWeb1 = empresadto.SitioWeb1,
                SitioWeb2 = empresadto.SitioWeb2,
                SitioWeb3 = empresadto.SitioWeb3,
                Rfc = empresadto.Rfc,
                RazonSocial = empresadto.RazonSocial,
                FactorLitrosAKilos = empresadto.FactorLitrosAKilos,
                CierreInventario = empresadto.CierreInventario,
                InventarioSano = empresadto.InventarioSano,
                InventarioCrítico = empresadto.InventarioCrítico,
                MaxRemaGaseraMensual = empresadto.MaxRemaGaseraMensual,
                UrlLogotipoMenu = empresadto.UrlLogotipoMenu,
                UrlLogotipoLogin = empresadto.UrlLogotipoLogin
            };
            return empresa;
        }
        public static List<Empresa> FromDTO(List<EmpresaDTO> empresasDTO)
        {
            List<Empresa> empreas = empresasDTO.ToList().Select(x => FromDTO(x)).ToList();
            return empreas;
        }        
    }
}
