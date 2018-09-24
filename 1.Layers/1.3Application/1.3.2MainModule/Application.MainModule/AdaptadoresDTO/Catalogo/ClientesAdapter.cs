using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Seguridad
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
                DescuentoXKilo = us.DescuentoXKilo,
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
                Empresa = us.Empresa.NombreComercial,
                TipoPersonaFiscal = us.TipoPersonaFiscal.Descripcion,
                RegimenFiscal = us.RegimenFiscal.Descripcion

            };
            return usDTO;
        }
        public static List<ClientesDto> ToDTO(List<Cliente> lu)
        {
            List<ClientesDto> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            return luDTO;
        }

        public static ClienteLocacionDTO ToDTOL(ClienteLocacion _loc)
        {
            var p = PaisServicio.Obtener(_loc.IdPais);
            var e = EstadosrepServicio.Obtener(_loc.IdEstadoRep.Value);

            ClienteLocacionDTO usDTO = new ClienteLocacionDTO()
            {
                IdCliente = _loc.IdCliente,
                Orden = _loc.Orden,
                IdPais = _loc.IdPais,
                IdEstadoRep = _loc.IdEstadoRep,
                EstadoProvincia = _loc.EstadoProvincia,
                Municipio = _loc.Municipio,
                CodigoPostal = _loc.CodigoPostal,
                Colonia = _loc.Colonia,
                Calle = _loc.Calle,
                NumExt = _loc.NumExt,
                NumInt = _loc.NumInt,
                formatted_address = _loc.formatted_address,
                location_lat = _loc.location_lat,
                location_lng = _loc.location_lng,
                place_id = _loc.place_id,
                TipoLocacion = _loc.TipoLocacion,
                Pais = p.PaisNombre,
                Estado = e.Estado,

            };
            return usDTO;
        }
        public static List<ClienteLocacionDTO> ToDTOLoc(List<ClienteLocacion> _loc)
        {          
            List<ClienteLocacionDTO> locDto = _loc.ToList().Select(x => ToDTOL(x)).ToList();

            return locDto;
        }
      
        public static ClienteLocacion FromDtox(ClienteLocacionDTO cteDTO)
        {
            return new ClienteLocacion()
            {
                IdCliente = cteDTO.IdCliente,
                Orden = cteDTO.Orden,
                IdPais = cteDTO.IdPais,
                IdEstadoRep = cteDTO.IdEstadoRep,
                EstadoProvincia = cteDTO.EstadoProvincia,
                Municipio = cteDTO.Municipio,
                CodigoPostal = cteDTO.CodigoPostal,
                Colonia = cteDTO.Colonia,
                Calle = cteDTO.Calle,
                NumExt = cteDTO.NumExt,
                NumInt = cteDTO.NumInt,
                formatted_address = cteDTO.Calle + cteDTO.Colonia + cteDTO.NumExt,//cteDTO.formatted_address,
                location_lat = "1",//cteDTO.location_lat,
                location_lng = "1",//cteDTO.location_lng,
                place_id = "1",//cteDTO.place_id,
                TipoLocacion = "1"//cteDTO.TipoLocacion,
            };
        }
        public static ClienteLocacion FromDtocteLoc(ClienteLocacionDTO cteDTO)
        {
            return new ClienteLocacion()
            {
                IdCliente = cteDTO.IdCliente,
                Orden = cteDTO.Orden,
                IdPais = cteDTO.IdPais,
                IdEstadoRep = cteDTO.IdEstadoRep,
                EstadoProvincia = cteDTO.EstadoProvincia,
                Municipio = cteDTO.Municipio,
                CodigoPostal = cteDTO.CodigoPostal,
                Colonia = cteDTO.Colonia,
                Calle = cteDTO.Calle,
                NumExt = cteDTO.NumExt,
                NumInt = cteDTO.NumInt,
                formatted_address = cteDTO.formatted_address,
                location_lat = cteDTO.location_lat,
                location_lng = cteDTO.location_lng,
                place_id = cteDTO.place_id,
                TipoLocacion = cteDTO.TipoLocacion,
            };
        }
        public static Cliente FromDtoMod(ClienteCrearDto cteDTO)
        {
            return new Cliente()
            {
                IdEmpresa = cteDTO.IdEmpresa,
                IdTipoPersona = cteDTO.IdTipoPersona,
                IdRegimenFiscal = cteDTO.IdRegimenFiscal,
                IdCuentaContable = cteDTO.IdCuentaContable,
                Nombre = cteDTO.Nombre,
                Apellido1 = cteDTO.Apellido1,
                Apellido2 = cteDTO.Apellido2,
                DescuentoXKilo = cteDTO.DescuentoXKilo,
                limiteCreditoMonto = cteDTO.limiteCreditoMonto,
                limiteCreditoDias = cteDTO.limiteCreditoDias,
                Telefono1 = cteDTO.Telefono1,
                Telefono2 = cteDTO.Telefono2,
                Telefono3 = cteDTO.Telefono3,
                Celular1 = cteDTO.Celular1,
                Celular2 = cteDTO.Celular2,
                Celular3 = cteDTO.Celular3,
                Email1 = cteDTO.Email1,
                Email2 = cteDTO.Email2,
                Email3 = cteDTO.Email3,
                SitioWeb1 = cteDTO.SitioWeb1,
                SitioWeb2 = cteDTO.SitioWeb2,
                SitioWeb3 = cteDTO.SitioWeb3,
                Usuario = cteDTO.Usuario,
                Password = cteDTO.Password,
                AccesoPortal = cteDTO.AccesoPortal,
                Rfc = cteDTO.Rfc,
                RazonSocial = cteDTO.RazonSocial,
                RepresentanteLegal = cteDTO.RepresentanteLegal,
                Telefono = cteDTO.Telefono,
                Celular = cteDTO.Celular,
                CorreoElectronico = cteDTO.CorreoElectronico,
                Domicilio = cteDTO.Domicilio,
                Activo = true,
                FechaRegistro = DateTime.Now
            };
        }

        public static Cliente FromDtoEditar(ClienteCrearDto Ctedto, Cliente catCte)
        {
            var catCliente = FromEntity(catCte);
            if (Ctedto.IdEmpresa != 0) { catCliente.IdEmpresa = Ctedto.IdEmpresa; } else { catCliente.IdEmpresa = catCliente.IdEmpresa; }
            if (Ctedto.IdTipoPersona != 0) { catCliente.IdTipoPersona = Ctedto.IdTipoPersona; } else catCliente.IdTipoPersona = catCliente.IdTipoPersona;
            if (Ctedto.IdRegimenFiscal != 0) catCliente.IdRegimenFiscal = Ctedto.IdRegimenFiscal; else catCliente.IdRegimenFiscal = catCliente.IdRegimenFiscal;
            if (Ctedto.IdCuentaContable != 0) catCliente.IdCuentaContable = Ctedto.IdCuentaContable; else catCliente.IdCuentaContable = catCliente.IdCuentaContable;
            if (Ctedto.Nombre != null) catCliente.Nombre = Ctedto.Nombre; else catCliente.Nombre = catCliente.Nombre;
            if (Ctedto.Apellido1 != null) catCliente.Apellido1 = Ctedto.Apellido1; else catCliente.Apellido1 = catCliente.Apellido1;
            if (Ctedto.Apellido2 != null) catCliente.Apellido2 = Ctedto.Apellido2; else catCliente.Apellido2 = catCliente.Apellido2;
            if (Ctedto.DescuentoXKilo != 0) catCliente.DescuentoXKilo = Ctedto.DescuentoXKilo; else catCliente.DescuentoXKilo = catCliente.DescuentoXKilo;
            if (Ctedto.limiteCreditoMonto != 0) catCliente.limiteCreditoMonto = Ctedto.limiteCreditoMonto; else catCliente.limiteCreditoMonto = catCliente.limiteCreditoMonto;
            if (Ctedto.limiteCreditoDias != 0) catCliente.limiteCreditoDias = Ctedto.limiteCreditoDias; else catCliente.limiteCreditoDias = catCliente.limiteCreditoDias;
            if (Ctedto.Telefono1 != null) catCliente.Telefono1 = Ctedto.Telefono1; else catCliente.Telefono1 = catCliente.Telefono1;
            if (Ctedto.Telefono2 != null) catCliente.Telefono2 = Ctedto.Telefono2; else catCliente.Telefono2 = catCliente.Telefono2;
            if (Ctedto.Telefono3 != null) catCliente.Telefono3 = Ctedto.Telefono3; else catCliente.Telefono3 = catCliente.Telefono3;
            if (Ctedto.Celular1 != null) catCliente.Celular1 = Ctedto.Celular1; else catCliente.Celular1 = catCliente.Celular1;
            if (Ctedto.Celular2 != null) catCliente.Celular2 = Ctedto.Celular2; else catCliente.Celular2 = catCliente.Celular2;
            if (Ctedto.Celular3 != null) catCliente.Celular3 = Ctedto.Celular3; else catCliente.Celular3 = catCliente.Celular3;
            if (Ctedto.Email1 != null) catCliente.Email1 = Ctedto.Email1; else catCliente.Email1 = catCliente.Email1;
            if (Ctedto.Email2 != null) catCliente.Email2 = Ctedto.Email2; else catCliente.Email2 = catCliente.Email2;
            if (Ctedto.Email3 != null) catCliente.Email3 = Ctedto.Email3; else catCliente.Email3 = catCliente.Email3;
            if (Ctedto.SitioWeb1 != null) catCliente.SitioWeb1 = Ctedto.SitioWeb1; else catCliente.SitioWeb1 = catCliente.SitioWeb1;
            if (Ctedto.SitioWeb2 != null) catCliente.SitioWeb2 = Ctedto.SitioWeb2; else catCliente.SitioWeb2 = catCliente.SitioWeb2;
            if (Ctedto.SitioWeb3 != null) catCliente.SitioWeb3 = Ctedto.SitioWeb3; else catCliente.SitioWeb3 = catCliente.SitioWeb3;
            if (Ctedto.Rfc != null) catCliente.Rfc = Ctedto.Rfc; else catCliente.Rfc = catCliente.Rfc;
            if (Ctedto.RazonSocial != null) catCliente.RazonSocial = Ctedto.RazonSocial; else catCliente.RazonSocial = catCliente.RazonSocial;
            if (Ctedto.Usuario != null) catCliente.Usuario = Ctedto.Usuario; else catCliente.Usuario = catCliente.Usuario;
            if (Ctedto.Password != null) catCliente.Password = Ctedto.Password; else catCliente.Password = catCliente.Password;
            catCliente.AccesoPortal = Ctedto.AccesoPortal;
            if (Ctedto.RepresentanteLegal != null) catCliente.RepresentanteLegal = Ctedto.RepresentanteLegal; else catCliente.RepresentanteLegal = catCliente.RepresentanteLegal;
            if (Ctedto.Telefono != null) catCliente.Telefono = Ctedto.Telefono; else catCliente.Telefono = catCliente.Telefono;
            if (Ctedto.Celular != null) catCliente.Celular = Ctedto.Celular; else catCliente.Celular = catCliente.Celular;
            if (Ctedto.CorreoElectronico != null) catCliente.CorreoElectronico = Ctedto.CorreoElectronico; else catCliente.CorreoElectronico = catCliente.CorreoElectronico;
            if (Ctedto.Domicilio != null) catCliente.Domicilio = Ctedto.Domicilio; else catCliente.Domicilio = catCliente.Domicilio;
            // if (Ctedto.Loca != null) catCliente.Domicilio = Ctedto.Domicilio; else catCliente.Domicilio = catCliente.Domicilio;

            return catCliente;
        }

        public static ClienteLocacion FromDto(ClienteLocacionDTO Ctedto, ClienteLocacion catCte)
        {
            var catCliente = FromEntityLoc(catCte);
            //if (Ctedto.Orden != 0) { catCliente.Orden = Ctedto.Orden; } else { catCliente.Orden = catCliente.Orden; }
            if (Ctedto.IdPais != 0) { catCliente.IdPais = Ctedto.IdPais; } else catCliente.IdPais = catCliente.IdPais;
            if (Ctedto.IdEstadoRep != 0) catCliente.IdEstadoRep = Ctedto.IdEstadoRep; else catCliente.IdEstadoRep = catCliente.IdEstadoRep;
            if (Ctedto.EstadoProvincia != null) catCliente.EstadoProvincia = Ctedto.EstadoProvincia; else catCliente.EstadoProvincia = catCliente.EstadoProvincia;
            if (Ctedto.Municipio != null) catCliente.Municipio = Ctedto.Municipio; else catCliente.Municipio = catCliente.Municipio;
            if (Ctedto.CodigoPostal != null) catCliente.CodigoPostal = Ctedto.CodigoPostal; else catCliente.CodigoPostal = catCliente.CodigoPostal;
            if (Ctedto.Colonia != null) catCliente.Colonia = Ctedto.Colonia; else catCliente.Colonia = catCliente.Colonia;
            if (Ctedto.Calle != null) catCliente.Calle = Ctedto.Calle; else catCliente.Calle = catCliente.Calle;
            if (Ctedto.NumExt != null) catCliente.NumExt = Ctedto.NumExt; else catCliente.NumExt = catCliente.NumExt;
            if (Ctedto.NumInt != null) catCliente.NumInt = Ctedto.NumInt; else catCliente.NumInt = catCliente.NumInt;
            if (Ctedto.formatted_address != null) catCliente.formatted_address = Ctedto.formatted_address; else catCliente.formatted_address = catCliente.formatted_address;
            if (Ctedto.location_lat != null) catCliente.location_lat = Ctedto.location_lat; else catCliente.location_lat = catCliente.location_lat;
            if (Ctedto.location_lng != null) catCliente.location_lng = Ctedto.location_lng; else catCliente.location_lng = catCliente.location_lng;
            if (Ctedto.place_id != null) catCliente.place_id = Ctedto.place_id; else catCliente.place_id = catCliente.place_id;
            if (Ctedto.TipoLocacion != null) catCliente.TipoLocacion = Ctedto.TipoLocacion; else catCliente.TipoLocacion = catCliente.TipoLocacion;

            return catCliente;
        }

        public static List<ClienteLocacionDTO> getLoc(List<ClienteLocacion> inf)
        {
            List<ClienteLocacionDTO> lst = new List<ClienteLocacionDTO>();

            foreach (ClienteLocacion v in inf)
            {
                ClienteLocacionDTO _loc = new ClienteLocacionDTO();
                _loc.Orden = v.Orden;
                _loc.IdPais = v.IdPais;
                _loc.IdEstadoRep = v.IdEstadoRep;
                _loc.EstadoProvincia = v.EstadoProvincia;
                _loc.Municipio = v.Municipio;
                _loc.CodigoPostal = v.CodigoPostal;
                _loc.Colonia = v.Colonia;
                _loc.Calle = v.Calle;
                _loc.NumExt = v.NumExt;
                _loc.NumInt = v.NumInt;
                _loc.formatted_address = v.formatted_address;
                _loc.location_lat = v.location_lat;
                _loc.location_lng = v.location_lng;
                _loc.place_id = v.place_id;
                _loc.TipoLocacion = v.TipoLocacion;
                lst.Add(_loc);
            }

            return lst;
        }
        public static Cliente FromEntity(Cliente cte)
        {
            return new Cliente()
            {
                IdCliente = cte.IdCliente,
                IdEmpresa = cte.IdEmpresa,
                IdTipoPersona = cte.IdTipoPersona,
                IdRegimenFiscal = cte.IdRegimenFiscal,
                IdCuentaContable = cte.IdCuentaContable,
                Nombre = cte.Nombre,
                Apellido1 = cte.Apellido1,
                Apellido2 = cte.Apellido2,
                Activo = cte.Activo,
                FechaRegistro = cte.FechaRegistro,
                DescuentoXKilo = cte.DescuentoXKilo,
                limiteCreditoMonto = cte.limiteCreditoMonto,
                limiteCreditoDias = cte.limiteCreditoDias,
                Telefono1 = cte.Telefono1,
                Telefono2 = cte.Telefono2,
                Telefono3 = cte.Telefono3,
                Celular1 = cte.Celular1,
                Celular2 = cte.Celular2,
                Celular3 = cte.Celular3,
                Email1 = cte.Email1,
                Email2 = cte.Email2,
                Email3 = cte.Email3,
                SitioWeb1 = cte.SitioWeb1,
                SitioWeb2 = cte.SitioWeb2,
                SitioWeb3 = cte.SitioWeb3,
                Usuario = cte.Usuario,
                Password = cte.Password,
                AccesoPortal = cte.AccesoPortal,
                Rfc = cte.Rfc,
                RazonSocial = cte.RazonSocial,
                RepresentanteLegal = cte.RepresentanteLegal,
                Telefono = cte.Telefono,
                Celular = cte.Celular,
                CorreoElectronico = cte.CorreoElectronico,
                Domicilio = cte.Domicilio,
                /**************************/
                Locaciones = cte.Locaciones//getLoc(cte.Locaciones.ToList()),

                //Orden = cte
                //IdPais =
                //IdEstadoRep =
                //EstadoProvincia =
                //Municipio =
                //CodigoPostal =
                //Colonia =
                //Calle =
                //NumExt =
                //NumInt =
                //formatted_address =
                //location_lat =
                //location_lng =
                //place_id =
                //TipoLocacion =

            };
        }
        public static ClienteLocacion FromEntityLoc(ClienteLocacion cte)
        {
            return new ClienteLocacion()
            {
                IdCliente = cte.IdCliente,
                Orden = cte.Orden,
                IdPais =cte.IdPais,
                IdEstadoRep = cte.IdEstadoRep,
                EstadoProvincia = cte.EstadoProvincia,
                Municipio = cte.Municipio,
                CodigoPostal =cte.CodigoPostal,
                Colonia = cte.Colonia,
                Calle = cte.Calle,
                NumExt = cte.NumExt,
                NumInt = cte.NumInt,
                formatted_address = cte.formatted_address,
                location_lat = cte.location_lat,
                location_lng = cte.location_lng,
                place_id = cte.place_id,
                TipoLocacion = cte.TipoLocacion

            };
        }
    }
}
