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
               // IdEmpresa = empresa.IdEmpresa,
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
                Persona1 = empresa.Persona1,
                Persona2 = empresa.Persona2,
                Persona3 = empresa.Persona3,
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
                //FactorLitrosAKilos = empresa.FactorLitrosAKilos,
                //CierreInventario = empresa.CierreInventario,
                //InventarioSano = empresa.InventarioSano,
                //InventarioCrítico = empresa.InventarioCrítico,
                //MaxRemaGaseraMensual = empresa.MaxRemaGaseraMensual,
                UrlLogotipoMenu = empresa.UrlLogotipoMenu,
                UrlLogotipoLogin = empresa.UrlLogotipoLogin,
                UrlLogotipo180px = empresa.UrlLogotipo180px,
                UrlLogotipo500px = empresa.UrlLogotipo500px,
                UrlLogotipo1000px = empresa.UrlLogotipo1000px

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
                //IdEmpresa = empresadto.IdEmpresa,
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
                //FactorLitrosAKilos = empresadto.FactorLitrosAKilos,
                //CierreInventario = empresadto.CierreInventario,
                //InventarioSano = empresadto.InventarioSano,
                //InventarioCrítico = empresadto.InventarioCrítico,
                //MaxRemaGaseraMensual = empresadto.MaxRemaGaseraMensual,
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

        public static Empresa FromDto(EmpresaCrearDTO empresa)
        {
            return new Empresa()
            {
                NombreComercial = empresa.NombreComercial,
                IdPais = empresa.IdPais,
                IdEstadoRep = empresa.IdEstadoRep,
                EstadoProvincia = empresa.EstadoProvincia,
                Municipio = empresa.Municipio,
                CodigoPostal = empresa.CodigoPostal,
                Colonia = empresa.Colonia,
                Calle = empresa.Calle,
                NumExt = empresa.NumExt,
                NumInt = empresa.NumInt,
                Persona1 = empresa.Persona1,
                Persona2 = empresa.Persona2,
                Persona3 = empresa.Persona3,
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
                UrlLogotipo180px = empresa.UrlLogotipo180px,
                UrlLogotipo500px = empresa.UrlLogotipo500px,
                UrlLogotipo1000px = empresa.UrlLogotipo1000px,
                FechaRegistro = DateTime.Now
                // UrlLogotipo1000px = empresa.Imagenes

            };
        }


        public static Empresa FromDtoConfig(EmpresaModificaConfig empresa, Empresa catEmp)
        {
            var catEmpresa = FromEntity(catEmp);
            catEmpresa.NombreComercial = empresa.NombreComercial;
            catEmpresa.IdPais = empresa.IdPais;
            catEmpresa.IdEstadoRep = empresa.IdEstadoRep;
            catEmpresa.EstadoProvincia = empresa.EstadoProvincia;
            catEmpresa.Municipio = empresa.Municipio;
            catEmpresa.CodigoPostal = empresa.CodigoPostal;
            catEmpresa.Colonia = empresa.Colonia;
            catEmpresa.Calle = empresa.Calle;
            catEmpresa.NumExt = empresa.NumExt;
            catEmpresa.NumInt = empresa.NumInt;
            catEmpresa.Persona1 = empresa.Persona1;
            catEmpresa.Persona2 = empresa.Persona2;
            catEmpresa.Persona3 = empresa.Persona3;
            catEmpresa.Telefono1 = empresa.Telefono1;
            catEmpresa.Telefono2 = empresa.Telefono2;
            catEmpresa.Telefono3 = empresa.Telefono3;
            catEmpresa.Celular1 = empresa.Celular1;
            catEmpresa.Celular2 = empresa.Celular2;
            catEmpresa.Celular3 = empresa.Celular3;
            catEmpresa.Email1 = empresa.Email1;
            catEmpresa.Email2 = empresa.Email2;
            catEmpresa.Email3 = empresa.Email3;
            catEmpresa.SitioWeb1 = empresa.SitioWeb1;
            catEmpresa.SitioWeb2 = empresa.SitioWeb2;
            catEmpresa.SitioWeb3 = empresa.SitioWeb3;
            catEmpresa.Rfc = empresa.Rfc;
            catEmpresa.RazonSocial = empresa.RazonSocial;
            catEmpresa.UrlLogotipo180px = empresa.UrlLogotipo180px;
            catEmpresa.UrlLogotipo500px = empresa.UrlLogotipo500px;
            catEmpresa.UrlLogotipo1000px = empresa.UrlLogotipo1000px;
            catEmpresa.FactorLitrosAKilos = empresa.FactorLitrosAKilos;
            catEmpresa.CierreInventario = empresa.CierreInventario;
            catEmpresa.InventarioSano = empresa.InventarioSano;
            catEmpresa.InventarioCrítico = empresa.InventarioCrítico;
            catEmpresa.MaxRemaGaseraMensual = empresa.MaxRemaGaseraMensual;
            catEmpresa.FactorGalonALitros = empresa.FactorGalonALitros;
            catEmpresa.FactorCompraLitroAKilos = empresa.FactorCompraLitroAKilos;
            catEmpresa.FactorFleteGas = empresa.FactorFleteGas;
            return catEmpresa;
            //return new Empresa()
            //{
            //    IdEmpresa = empresa.IdEmpresa,
            //    FactorLitrosAKilos = empresa.FactorLitrosAKilos,
            //    CierreInventario = empresa.CierreInventario,
            //    InventarioSano = empresa.InventarioSano,
            //    InventarioCrítico = empresa.InventarioCrítico,
            //    MaxRemaGaseraMensual = empresa.MaxRemaGaseraMensual,
            //    FactorGalonALitros = empresa.FactorGalonALitros,
            //    FactorCompraLitroAKilos = empresa.FactorCompraLitroAKilos,
            //    FactorFleteGas = empresa.FactorFleteGas
            //};
        }
        public static Empresa FromEntity(Empresa empresa)
        {
            return new Empresa()
            {
                NombreComercial = empresa.NombreComercial,
                IdPais = empresa.IdPais,
                IdEstadoRep = empresa.IdEstadoRep,
                EstadoProvincia = empresa.EstadoProvincia,
                Municipio = empresa.Municipio,
                CodigoPostal = empresa.CodigoPostal,
                Colonia = empresa.Colonia,
                Calle = empresa.Calle,
                NumExt = empresa.NumExt,
                NumInt = empresa.NumInt,
                Persona1 = empresa.Persona1,
                Persona2 = empresa.Persona2,
                Persona3 = empresa.Persona3,
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
                UrlLogotipo180px = empresa.UrlLogotipo180px,
                UrlLogotipo500px = empresa.UrlLogotipo500px,
                UrlLogotipo1000px = empresa.UrlLogotipo1000px,
                FactorLitrosAKilos = empresa.FactorLitrosAKilos,
                CierreInventario = empresa.CierreInventario,
                InventarioSano = empresa.InventarioSano,
                InventarioCrítico = empresa.InventarioCrítico,
                MaxRemaGaseraMensual = empresa.MaxRemaGaseraMensual,
                FactorGalonALitros = empresa.FactorGalonALitros,
                FactorCompraLitroAKilos = empresa.FactorCompraLitroAKilos,
                FactorFleteGas = empresa.FactorFleteGas
            };
        }

    }
}
