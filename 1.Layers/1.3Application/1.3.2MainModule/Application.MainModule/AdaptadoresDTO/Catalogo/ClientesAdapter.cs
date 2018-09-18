using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class ClientesAdapter
    {
        public static ClientesDto ToDTO(Cliente us)
        {
            ClientesDto usDTO = new ClientesDto()
            {
                IdCliente = us.IdCliente,
                IdEmpresa = us.IdEmpresa,
                IdTipoPersona = us.IdTipoPersona,
                IdRegimenFiscal = us.IdRegimenFiscal,
                IdCuentaContable = us.IdCuentaContable,
                Nombre = us.Nombre,
                Apellido1 = us.Apellido1,
                Apellido2 = us.Apellido2,
                //Activo = us.Activo,
                //FechaRegistro = us.FechaRegistro,
                PorcentDescuento = us.PorcentDescuento,
                limiteCreditoMonto = us.limiteCreditoMonto,
                limiteCreditoDias = us.limiteCreditoDias,
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
                Usuario = us.Usuario,
                Password = us.Password,
                AccesoPortal = us.AccesoPortal,
                Rfc = us.Rfc,
                RazonSocial = us.RazonSocial,
                RepresentanteLegal = us.RepresentanteLegal,
                Telefono = us.Telefono,
                Celular = us.Celular,
                CorreoElectronico = us.CorreoElectronico,
                Domicilio = us.Domicilio,

            };

            return usDTO;
        }
        public static List<ClientesDto> ToDTO(List<Cliente> lu)
        {
            List<ClientesDto> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            return luDTO;
        }
    }
}
