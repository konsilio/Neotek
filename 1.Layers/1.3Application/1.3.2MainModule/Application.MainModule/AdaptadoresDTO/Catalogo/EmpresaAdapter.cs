using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.AdaptadoresDTO.Seguridad
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
                FactorLitrosAKilos = empresa.FactorLitrosAKilos,
                CierreInventario = empresa.CierreInventario,
                InventarioSano = empresa.InventarioSano,
                InventarioCrítico = empresa.InventarioCrítico,
                MaxRemaGaseraMensual = empresa.MaxRemaGaseraMensual,
                FactorGalonALitros = empresa.FactorGalonALitros,
                FactorCompraLitroAKilos = empresa.FactorCompraLitroAKilos,
                FactorFleteGas = empresa.FactorFleteGas,
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
        public static Empresa FromDTOEditar(EmpresaDTO empresadto, Empresa catEmp)
        {
            var catEmpresa = FromEntity(catEmp);
            if (empresadto.NombreComercial != null) { catEmpresa.NombreComercial = empresadto.NombreComercial; } else { catEmpresa.NombreComercial = catEmpresa.NombreComercial; }
            if (empresadto.IdPais != 0) { catEmpresa.IdPais = empresadto.IdPais; } else catEmpresa.IdPais = catEmpresa.IdPais;
            if (empresadto.IdEstadoRep != 0) catEmpresa.IdEstadoRep = empresadto.IdEstadoRep; else catEmpresa.IdEstadoRep = catEmpresa.IdEstadoRep;
            if (empresadto.EstadoProvincia != null) catEmpresa.EstadoProvincia = empresadto.EstadoProvincia; else catEmpresa.EstadoProvincia = catEmpresa.EstadoProvincia;
            if (empresadto.Municipio != null) catEmpresa.Municipio = empresadto.Municipio; else catEmpresa.Municipio = catEmpresa.Municipio;
            if (empresadto.CodigoPostal != null) catEmpresa.CodigoPostal = empresadto.CodigoPostal; else catEmpresa.CodigoPostal = catEmpresa.CodigoPostal;
            if (empresadto.Colonia != null) catEmpresa.Colonia = empresadto.Colonia; else catEmpresa.Colonia = catEmpresa.Colonia;
            if (empresadto.Calle != null) catEmpresa.Calle = empresadto.Calle; else catEmpresa.Calle = catEmpresa.Calle;
            if (empresadto.NumExt != null) catEmpresa.NumExt = empresadto.NumExt; else catEmpresa.NumExt = catEmpresa.NumExt;
            if (empresadto.NumInt != null) catEmpresa.NumInt = empresadto.NumInt; else catEmpresa.NumInt = catEmpresa.NumInt;
            if (empresadto.Persona1 != null) catEmpresa.Persona1 = empresadto.Persona1; else catEmpresa.Persona1 = catEmpresa.Persona1;
            if (empresadto.Persona2 != null) catEmpresa.Persona2 = empresadto.Persona2; else catEmpresa.Persona2 = catEmpresa.Persona2;
            if (empresadto.Persona3 != null) catEmpresa.Persona3 = empresadto.Persona3; else catEmpresa.Persona3 = catEmpresa.Persona3;
            if (empresadto.Telefono1 != null) catEmpresa.Telefono1 = empresadto.Telefono1; else catEmpresa.Telefono1 = catEmpresa.Telefono1;
            if (empresadto.Telefono2 != null) catEmpresa.Telefono2 = empresadto.Telefono2; else catEmpresa.Telefono2 = catEmpresa.Telefono2;
            if (empresadto.Telefono3 != null) catEmpresa.Telefono3 = empresadto.Telefono3; else catEmpresa.Telefono3 = catEmpresa.Telefono3;
            if (empresadto.Celular1 != null) catEmpresa.Celular1 = empresadto.Celular1; else catEmpresa.Celular1 = catEmpresa.Celular1;
            if (empresadto.Celular2 != null) catEmpresa.Celular2 = empresadto.Celular2; else catEmpresa.Celular2 = catEmpresa.Celular2;
            if (empresadto.Celular3 != null) catEmpresa.Celular3 = empresadto.Celular3; else catEmpresa.Celular3 = catEmpresa.Celular3;
            if (empresadto.Email1 != null) catEmpresa.Email1 = empresadto.Email1; else catEmpresa.Email1 = catEmpresa.Email1;
            if (empresadto.Email2 != null) catEmpresa.Email2 = empresadto.Email2; else catEmpresa.Email2 = catEmpresa.Email2;
            if (empresadto.Email3 != null) catEmpresa.Email3 = empresadto.Email3; else catEmpresa.Email3 = catEmpresa.Email3;
            if (empresadto.SitioWeb1 != null) catEmpresa.SitioWeb1 = empresadto.SitioWeb1; else catEmpresa.SitioWeb1 = catEmpresa.SitioWeb1;
            if (empresadto.SitioWeb2 != null) catEmpresa.SitioWeb2 = empresadto.SitioWeb2; else catEmpresa.SitioWeb2 = catEmpresa.SitioWeb2;
            if (empresadto.SitioWeb3 != null) catEmpresa.SitioWeb3 = empresadto.SitioWeb3; else catEmpresa.SitioWeb3 = catEmpresa.SitioWeb3;
            if (empresadto.Rfc != null) catEmpresa.Rfc = empresadto.Rfc; else catEmpresa.Rfc = catEmpresa.Rfc;
            if (empresadto.RazonSocial != null) catEmpresa.RazonSocial = empresadto.RazonSocial; else catEmpresa.RazonSocial = catEmpresa.RazonSocial;
            if (empresadto.UrlLogotipo180px != null) catEmpresa.UrlLogotipo180px = empresadto.UrlLogotipo180px; else catEmpresa.UrlLogotipo180px = catEmpresa.UrlLogotipo180px;
            if (empresadto.UrlLogotipo500px != null) catEmpresa.UrlLogotipo500px = empresadto.UrlLogotipo500px; else catEmpresa.UrlLogotipo500px = catEmpresa.UrlLogotipo500px;
            if (empresadto.UrlLogotipo1000px != null) catEmpresa.UrlLogotipo1000px = empresadto.UrlLogotipo1000px; else catEmpresa.UrlLogotipo1000px = catEmpresa.UrlLogotipo1000px;

            empresadto.FactorLitrosAKilos = catEmpresa.FactorLitrosAKilos;
            empresadto.CierreInventario = catEmpresa.CierreInventario;
            empresadto.InventarioSano = catEmpresa.InventarioSano;
            empresadto.InventarioCrítico = catEmpresa.InventarioCrítico;
            empresadto.MaxRemaGaseraMensual = catEmpresa.MaxRemaGaseraMensual;


            return catEmpresa;
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
               // if (empresa.EstadoProvincia == "") { IdEstadoRep = null} else { IdEstadoRep = empresa.IdEstadoRep },//,
              //  empresa.EstadoProvincia != "" ? IdEstadoRep = null : IdEstadoRep = empresa.IdEstadoRep,
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
                CierreInventario = DateTime.Now,//empresa.CierreInventario,
                //FactorCompraLitroAKilos = empresa.FactorCompraLitroAKilos,
                //FactorFleteGas = empresa.FactorFleteGas,
                //FactorGalonALitros = empresa.FactorGalonALitros,
                //FactorLitrosAKilos = empresa.FactorLitrosAKilos,
                //InventarioCrítico = empresa.InventarioCrítico,
                //InventarioSano = empresa.InventarioSano,
                //MaxRemaGaseraMensual = empresa.MaxRemaGaseraMensual,
                UrlLogotipo180px = empresa.UrlLogotipo180px,
                UrlLogotipo500px = empresa.UrlLogotipo500px,
                UrlLogotipo1000px = empresa.UrlLogotipo1000px,
                FechaRegistro = DateTime.Now,
                Activo = true,

            };
        }

        public static Empresa FromDtoConfig(EmpresaModificaConfig empresa, Empresa catEmp)
        {
            var catEmpresa = FromEntity(catEmp);

            catEmpresa.FactorLitrosAKilos = empresa.FactorLitrosAKilos;
            catEmpresa.CierreInventario = empresa.CierreInventario;
            catEmpresa.InventarioSano = empresa.InventarioSano;
            catEmpresa.InventarioCrítico = empresa.InventarioCrítico;
            catEmpresa.MaxRemaGaseraMensual = empresa.MaxRemaGaseraMensual;
            catEmpresa.FactorGalonALitros = empresa.FactorGalonALitros;
            catEmpresa.FactorCompraLitroAKilos = empresa.FactorCompraLitroAKilos;
            catEmpresa.FactorFleteGas = empresa.FactorFleteGas;
            return catEmpresa;
        }
        public static Empresa FromEntity(Empresa empresa)
        {
            return new Empresa()
            {
                IdEmpresa = empresa.IdEmpresa,
                EsAdministracionCentral = empresa.EsAdministracionCentral,
                NombreComercial = empresa.NombreComercial,
                FechaRegistro = empresa.FechaRegistro,
                Activo = empresa.Activo,
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
                FactorLitrosAKilos = empresa.FactorLitrosAKilos,
                CierreInventario = empresa.CierreInventario,
                InventarioSano = empresa.InventarioSano,
                InventarioCrítico = empresa.InventarioCrítico,
                MaxRemaGaseraMensual = empresa.MaxRemaGaseraMensual,
                FactorGalonALitros = empresa.FactorGalonALitros,
                FactorCompraLitroAKilos = empresa.FactorCompraLitroAKilos,
                FactorFleteGas = empresa.FactorFleteGas,
                UrlLogotipoMenu = empresa.UrlLogotipoMenu,
                UrlLogotipoLogin = empresa.UrlLogotipoLogin,
                UrlLogotipo180px = empresa.UrlLogotipo180px,
                UrlLogotipo500px = empresa.UrlLogotipo500px,
                UrlLogotipo1000px = empresa.UrlLogotipo1000px,

            };
        }

    }
}
