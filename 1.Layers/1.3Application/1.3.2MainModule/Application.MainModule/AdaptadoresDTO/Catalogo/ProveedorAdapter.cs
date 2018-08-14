using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class ProveedorAdapter
    {
        public static ProveedorDto ToDto(Proveedor proveedor)
        {
            return new ProveedorDto()
            {
                IdProveedor = proveedor.IdProveedor,
                IdEmpresa = proveedor.IdEmpresa,
                IdTipoProveedor = proveedor.IdTipoProveedor,
                IdCuentaContable = proveedor.IdCuentaContable,
                IdFormaDePago = proveedor.IdFormaDePago,
                IdBanco = proveedor.IdBanco,
                IdPais = proveedor.IdPais,
                IdEstadoRep = proveedor.IdEstadoRep,
                IdTipoPersona = proveedor.IdTipoPersona,
                IdRegimenFiscal = proveedor.IdRegimenFiscal,
                NombreComercial = proveedor.NombreComercial,
                ProdutoPrinicpal = proveedor.ProdutoPrinicpal,
                TransportistaProdutoPrinicpal = proveedor.TransportistaProdutoPrinicpal,
                Vende = proveedor.Vende,
                FechaRegistro = proveedor.FechaRegistro,
                Cuenta = proveedor.Cuenta,
                DiasCredito = proveedor.DiasCredito ,
                Persona1 = proveedor.Persona1,
                Persona2 = proveedor.Persona2,
                Persona3 = proveedor.Persona3,
                Telefono1 = proveedor.Telefono1,
                Telefono2 = proveedor.Telefono2,
                Telefono3 = proveedor.Telefono3,
                Celular1 = proveedor.Celular1,
                Celular2 = proveedor.Celular2,
                Celular3 = proveedor.Celular3,
                Email1 = proveedor.Email1,
                Email2 = proveedor.Email2,
                Email3 = proveedor.Email3,
                SitioWeb1 = proveedor.SitioWeb1,
                SitioWeb2 = proveedor.SitioWeb2,
                SitioWeb3 = proveedor.SitioWeb3,
                EstadoProvincia = proveedor.EstadoProvincia,
                Municipio = proveedor.Municipio,
                CodigoPostal = proveedor.CodigoPostal,
                Colonia = proveedor.Colonia,
                Calle = proveedor.Calle,
                NumExt = proveedor.NumExt,
                NumInt = proveedor.NumInt,
                Rfc = proveedor.Rfc,
                RazonSocial = proveedor.RazonSocial,
            };
        }

        public static List<ProveedorDto> ToDto(List<Proveedor> proveedores)
        {
            return proveedores.ToList()
                              .Select(x => ToDto(x))
                              .ToList();
        }
    }
}
