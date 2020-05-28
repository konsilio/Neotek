using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Cobranza;
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
            //string nom = ClienteServicio.ObtenerNomreCliente(us);
            try
            {
                ClientesDto usDTO = new ClientesDto()
                {
                    IdCliente = us.IdCliente,
                    IdEmpresa = us.IdEmpresa,
                    IdTipoPersona = us.IdTipoPersona,
                    IdRegimenFiscal = us.IdRegimenFiscal,
                    //IdCuentaContable = us.IdCuentaContable,
                    Nombre = string.IsNullOrEmpty(us.RazonSocial?.Trim()) ? us.Nombre : us.RazonSocial,
                    Apellido1 = string.IsNullOrEmpty(us.RazonSocial?.Trim()) ? us.Apellido1 ?? string.Empty : string.Empty,
                    Apellido2 = string.IsNullOrEmpty(us.RazonSocial?.Trim()) ? us.Apellido2 ?? string.Empty : string.Empty,
                    DescuentoXKilo = us.DescuentoXKilo,
                    limiteCreditoMonto = us.limiteCreditoMonto,
                    limiteCreditoDias = us.limiteCreditoDias,
                    CreditoDisponibleMonto = us.CreditoDisponibleMonto,
                    Telefono1 = ClienteServicio.ObtenerTelefono(us),
                    Telefono2 = us.Telefono2 ?? string.Empty,
                    Telefono3 = us.Telefono3 ?? string.Empty,
                    Celular1 = us.Celular1 ?? string.Empty,
                    Celular2 = us.Celular2 ?? string.Empty,
                    Celular3 = us.Celular3 ?? string.Empty,
                    Email1 = us.Email1 ?? string.Empty,
                    Email2 = us.Email2 ?? string.Empty,
                    Email3 = us.Email3 ?? string.Empty,
                    SitioWeb1 = us.SitioWeb1 ?? string.Empty,
                    SitioWeb2 = us.SitioWeb2 ?? string.Empty,
                    SitioWeb3 = us.SitioWeb3 ?? string.Empty,
                    //Usuario = us.Usuario,
                    //Password = us.Password,
                    AccesoPortal = us.AccesoPortal,
                    Rfc = us.Rfc,
                    RazonSocial = string.IsNullOrEmpty(us.RazonSocial?.Trim()) ? us.Nombre + " "+ 
                                                       us.Apellido1 ?? string.Empty + " " +
                                                       us.Apellido2 ?? string.Empty : us.RazonSocial,
                    RepresentanteLegal = us.RepresentanteLegal ?? string.Empty,
                    Telefono = us.Telefono ?? string.Empty,
                    Celular = us.Celular ?? string.Empty,
                    CorreoElectronico = us.CorreoElectronico ?? string.Empty,
                    Domicilio = us.Domicilio ?? string.Empty,
                    Empresa = us.Empresa.NombreComercial,
                    TipoPersonaFiscal = us.TipoPersonaFiscal.Descripcion,
                    RegimenFiscal = us.RegimenFiscal.Descripcion,
                    Cliente = us.Rfc,
                    EsFijo = us.EsFijo,
                };
                return usDTO;
            }
            catch (Exception ex)
            {

                throw;
            }
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
                IdEstadoRep = _loc.IdEstadoRep ?? 0,
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
                //Estado = e.Estado,
            };
            if (e != null)
                usDTO.Estado = e.Estado;
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
                Orden = (short)(ClienteServicio.ObtenerUltimoOrdenLocacion(cteDTO.IdCliente) + 1),
                IdPais = cteDTO.IdPais,
                IdEstadoRep = cteDTO.IdEstadoRep,
                EstadoProvincia = cteDTO.EstadoProvincia,
                Municipio = cteDTO.Municipio,
                CodigoPostal = cteDTO.CodigoPostal,
                Colonia = cteDTO.Colonia,
                Calle = cteDTO.Calle,
                NumExt = cteDTO.NumExt,
                NumInt = cteDTO.NumInt,
                formatted_address = cteDTO.formatted_address ?? "",//cteDTO.Calle + cteDTO.Colonia + cteDTO.NumExt,
                location_lat = "1",//cteDTO.location_lat,
                location_lng = "1",//cteDTO.location_lng,
                place_id = "1",//cteDTO.place_id,
                TipoLocacion = cteDTO.TipoLocacion,
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
                CreditoDisponibleMonto = cteDTO.CreditoDisponibleMonto,
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
                FechaRegistro = DateTime.Now,
                VentaExtraordinaria = cteDTO.VentaExtraordinaria,
                EsFijo = cteDTO.EsFijo,
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
            //if (Ctedto.DescuentoXKilo != 0)
            catCliente.DescuentoXKilo = Ctedto.DescuentoXKilo;
            //else catCliente.DescuentoXKilo = catCliente.DescuentoXKilo;
            //if (Ctedto.limiteCreditoMonto != 0)
            catCliente.limiteCreditoMonto = Ctedto.limiteCreditoMonto;
            //else catCliente.limiteCreditoMonto = catCliente.limiteCreditoMonto;
            //if (Ctedto.CreditoDisponibleMonto != 0)
            catCliente.CreditoDisponibleMonto = Ctedto.CreditoDisponibleMonto;
            //else catCliente.CreditoDisponibleMonto= catCliente.CreditoDisponibleMonto;
            //if (Ctedto.limiteCreditoDias != 0)
            catCliente.limiteCreditoDias = Ctedto.limiteCreditoDias;
            //else catCliente.limiteCreditoDias = catCliente.limiteCreditoDias;
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
            catCliente.EsFijo = Ctedto.EsFijo;
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
                _loc.IdEstadoRep = v.IdEstadoRep ?? 0;
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
                //Locaciones = cte.Locaciones//getLoc(cte.Locaciones.ToList()),

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
                IdPais = cte.IdPais,
                IdEstadoRep = cte.IdEstadoRep,
                EstadoProvincia = cte.EstadoProvincia,
                Municipio = cte.Municipio,
                CodigoPostal = cte.CodigoPostal,
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
        public static List<DescuentosXClientesDTO> ToDTOC(List<Cliente> ListaClientes, PeriodoDTO dtop)
        {
            List<DescuentosXClientesDTO> respuesta = new List<DescuentosXClientesDTO>();
            foreach (var item in ListaClientes)
            {
                decimal num = 0.00M;
                if (item.VentaPuntoDeVenta.Count > 0 && item.VentaPuntoDeVenta.Sum(x => x.Descuento) > num)
                {
                    foreach (var ticket in item.VentaPuntoDeVenta.Where(x => x.Descuento > 0))
                    {
                        if (ticket.FechaRegistro >= dtop.FechaInicio && ticket.FechaRegistro <= dtop.FechaFin)
                        {
                            DescuentosXClientesDTO newDesc = new DescuentosXClientesDTO();
                            newDesc.Id = item.IdCliente;
                            newDesc.Cliente = item.Nombre;
                            newDesc.PrecioDeVenta = ticket.VentaPuntoDeVentaDetalle.FirstOrDefault().PrecioUnitarioProducto ?? 0;
                            newDesc.DescuentoTotal = ticket.Descuento;
                            newDesc.DescuentoLt = item.DescuentoXKilo;
                            newDesc.Diferencia = ticket.Total;
                            newDesc.Total = ticket.Total + ticket.Descuento;
                            respuesta.Add(newDesc);
                        }
                    }
                }
            }
            return respuesta;
        }
        public static List<CreditoRecuperadoDTO> ToDTOCR(List<Cliente> ListaClientes, PeriodoDTO dto)
        {
            return ListaClientes.Select(x => ToDTOCR(x, dto)).ToList();
        }

        public static CreditoRecuperadoDTO ToDTOCR(Cliente Clte, PeriodoDTO p)
        {
            List<Abono> abonos = Clte.Cargo.SelectMany(x => x.Abono.Where(a => a.FechaAbono > p.FechaInicio && a.FechaAbono < p.FechaFin)).ToList();
            //var tiket = Clte.VentaPuntoDeVenta.SingleOrDefault(x => x.FolioVenta.Equals(new Abono().Cargo.Ticket));

            return new CreditoRecuperadoDTO()
            {
                Nombre = ClienteServicio.ObtenerNomreCliente(Clte),
                Id = Clte.IdCliente.ToString(),
                Total = abonos.Sum(x => x.MontoAbono),
                Abonos = ToDTOCA(abonos),
            };
        }
        public static List<CreditoRecuperadoAbonoDTO> ToDTOCA(List<Abono> Abono)
        {
            return Abono.Select(x => ToDTOCA(x)).ToList();
        }
        public static CreditoRecuperadoAbonoDTO ToDTOCA(Abono Abono)
        {
            var tiket = Abono.Cargo.CCliente.VentaPuntoDeVenta.SingleOrDefault(x => x.FolioVenta.Equals(Abono.Cargo.Ticket));
            return new CreditoRecuperadoAbonoDTO()
            {
                Nota = Abono.Cargo.Ticket,
                FechaAbono = Abono.FechaAbono,
                Importe = Abono.MontoAbono,
                FormaDePago = Abono.CFormaPago.Descripcion,
                FechaCarga = tiket != null ? tiket.FechaRegistro.ToShortDateString() : "Ticket borrado",
            };
        }
        public static List<CreditoOtorgadoDTO> ToDTOCO(List<Cliente> ListaClientes, PeriodoDTO dto)
        {
            return ListaClientes.Select(x => ToDTOCO(x, dto)).ToList();
        }
        public static CreditoOtorgadoDTO ToDTOCO(Cliente Clte, PeriodoDTO dto)
        {
            //List<Abono> Cargos = Clte.Cargo.SelectMany(x=> x.Abono).ToList();
            List<Cargo> Cargos = Clte.Cargo.Where(x => x.FechaRegistro > dto.FechaInicio && x.FechaRegistro < dto.FechaFin).ToList();
            var Litros = Clte.VentaPuntoDeVenta.Sum(x => x.VentaPuntoDeVentaDetalle.Sum(y => y.CantidadLt ?? 0));
            return new CreditoOtorgadoDTO()
            {
                Nombre = ClienteServicio.ObtenerNomreCliente(Clte),
                Id = Clte.IdCliente.ToString(),
                Total = Cargos.Where(c => c.Activo).Sum(x => x.TotalCargo),
                Litros = Clte.VentaPuntoDeVenta.Sum(x => x.VentaPuntoDeVentaDetalle.Sum(y => y.CantidadLt ?? 0)),
                Abonos = ToDTOCC(Cargos),
            };
        }
        public static List<CreditoOtorgadoCargosDTO> ToDTOCC(List<Cargo> Cargo)
        {
            return Cargo.Select(x => ToDTOCC(x)).ToList();
        }
        public static CreditoOtorgadoCargosDTO ToDTOCC(Cargo Cargo)
        {
            var tiket = Cargo.CCliente.VentaPuntoDeVenta.Where(x => x.FolioVenta.Equals(Cargo.Ticket)).FirstOrDefault();
            var Unidad = string.Concat(tiket != null ? tiket.CPuntoVenta.UnidadesAlmacen.Pipa != null ? tiket.CPuntoVenta.UnidadesAlmacen.Pipa.Serie + " " : "P-- " : "0", Cargo.Ticket);
            return new CreditoOtorgadoCargosDTO()
            {
                Nota = Unidad,
                FechaCarga = tiket != null ? tiket.FechaRegistro.ToShortDateString() : "Ticket borrado",
                Importe = Cargo.TotalCargo,
                Vendedor = tiket != null ? tiket.OperadorChofer : "Ticket borrado",
                Litros = tiket != null ? Convert.ToString(tiket.VentaPuntoDeVentaDetalle.Sum(x => x.CantidadLt)) : "0",
            };
        }
        public static List<CreditoXClienteDTO> ToDTOCXC(List<Cliente> ListaClientes,PeriodoDTO dto)
        {
            return ListaClientes.Select(x => ToDTOCXC(x, dto)).ToList();
        }
        public static CreditoXClienteDTO ToDTOCXC(Cliente Clte, PeriodoDTO dto)
        {
            var cargo = Clte.Cargo.Where(z => z.FechaRegistro <= dto.FechaInicio
                                                                            && !z.Saldada
                                                                            && z.Activo).ToList();
            return new CreditoXClienteDTO()
            {
                Id = Clte.IdCliente.ToString(),
                Nombre = ClienteServicio.ObtenerNomreCliente(Clte),
                SaldoActual = cargo.Sum(x => x.TotalCargo),
                SaldoCorriente = cargo.Sum(x => DateTime.Now.Date < x.FechaVencimiento ? 0 : x.TotalCargo),
                Vencido = cargo.Sum(x => DateTime.Now.Date > x.FechaVencimiento ? 0 : x.TotalCargo),
                Dias1a7 = cargo.Sum(x => (DateTime.Now.Date - x.FechaVencimiento).Days >= 1 && (DateTime.Now.Date - x.FechaVencimiento).Days <= 7 ? x.TotalCargo : 0),
                Dias8a16 = cargo.Sum(x => (DateTime.Now.Date - x.FechaVencimiento).Days >= 8 && (DateTime.Now.Date - x.FechaVencimiento).Days <= 16 ? x.TotalCargo : 0),
                Dias17a31 = cargo.Sum(x => (DateTime.Now.Date - x.FechaVencimiento).Days >= 17 && (DateTime.Now.Date - x.FechaVencimiento).Days <= 31 ? x.TotalCargo : 0),
                Dias32a61 = cargo.Sum(x => (DateTime.Now.Date - x.FechaVencimiento).Days >= 32 && (DateTime.Now.Date - x.FechaVencimiento).Days <= 61 ? x.TotalCargo : 0),
                Dias62a91 = cargo.Sum(x => (DateTime.Now.Date - x.FechaVencimiento).Days >= 62 && (DateTime.Now.Date - x.FechaVencimiento).Days <= 91 ? x.TotalCargo : 0),
                Mas91 = cargo.Sum(x => (DateTime.Now.Date - x.FechaVencimiento).Days > 91 ? x.TotalCargo : 0),
                CargosDetallados = ToDTOCX(cargo),
            };
        }


        public static List<ControlDeAsistenciaDTO> ToDTOCA(List<Usuario> ListaClientes)
        {
            return ListaClientes.Select(x => ToDTOCA(x)).ToList();
        }

        public static ControlDeAsistenciaDTO ToDTOCA(Usuario Us)
        {
            var NombreUsuario = Us.Nombre.FirstOrDefault();

            DateTime fechaPasada = Us.FechaRegistro;
            return new ControlDeAsistenciaDTO()
            {
                //IdUsuario = Us.IdUsuario.ToString(),
                Nombre = Us.Nombre + " " + Us.Apellido1,
                PtoVenta = Us.Nombre == "Alejandro" ? "ISLA" : "LIBRAMIENTO",
                FechaRegistro = Us.Nombre == "Alejandro" ? DateTime.Now : fechaPasada,
                Estatus = Us.Nombre == "Alejandro" ? "Exitoso" : "No exitoso",
            };
        }
        public static List<CargosDTO> ToDTOCX(List<Cargo> Cargo)
        {
            return Cargo.Select(x => ToDTOCX(x)).ToList();
        }
        public static CargosDTO ToDTOCX(Cargo cargo)
        {
            var tikett = cargo.CCliente.VentaPuntoDeVenta.Where(x => x.FolioVenta.Equals(cargo.Ticket)).FirstOrDefault();
            return new CargosDTO()
            {
                FechaRegistro = cargo.FechaRegistro,
                FechaVencimiento = cargo.FechaVencimiento,
                Ticket = cargo.Ticket,
                Serie = string.Concat(tikett != null ? tikett.CPuntoVenta.UnidadesAlmacen.Pipa != null ? tikett.CPuntoVenta.UnidadesAlmacen.Pipa.Serie + " " : "P-" : "0", cargo.Ticket),
                SaldoActual = cargo.TotalCargo,
                SaldoCorriente = (DateTime.Now.Date > cargo.FechaVencimiento ? 0 : cargo.TotalCargo),
                SaldoVencido = (DateTime.Now.Date < cargo.FechaVencimiento ? 0 : cargo.TotalCargo),
                Dias1a7 = (DateTime.Now.Date - cargo.FechaVencimiento).Days >= 1 && (DateTime.Now.Date - cargo.FechaVencimiento).Days <= 7 ? cargo.TotalCargo : 0,
                Dias8a16 = (DateTime.Now.Date - cargo.FechaVencimiento).Days >= 8 && (DateTime.Now.Date - cargo.FechaVencimiento).Days <= 16 ? cargo.TotalCargo : 0,
                Dias17a31 = (DateTime.Now.Date - cargo.FechaVencimiento).Days >= 17 && (DateTime.Now.Date - cargo.FechaVencimiento).Days <= 31 ? cargo.TotalCargo : 0,
                Dias32a61 = (DateTime.Now.Date - cargo.FechaVencimiento).Days >= 32 && (DateTime.Now.Date - cargo.FechaVencimiento).Days <= 61 ? cargo.TotalCargo : 0,
                Dias62a91 = (DateTime.Now.Date - cargo.FechaVencimiento).Days >= 62 && (DateTime.Now.Date - cargo.FechaVencimiento).Days <= 91 ? cargo.TotalCargo : 0,
                Mas91 = (DateTime.Now.Date - cargo.FechaVencimiento).Days > 91 ? cargo.TotalCargo : 0,
            };
        }
        public static List<CreditoXClienteMensualDTO> ToDTOCXCM(List<Cliente> ListaClientes, PeriodoDTO dto)
        {
            return ListaClientes.Select(x => ToDTOCXCM(x, dto)).ToList();
        }
        public static CreditoXClienteMensualDTO ToDTOCXCM(Cliente Clte, PeriodoDTO dto)
        {
            var cargo = Clte.Cargo.Where(z => z.FechaRegistro.Month.Equals(dto.FechaInicio.Month)
                                                   && z.FechaRegistro.Year.Equals(dto.FechaInicio.Year)
                                                   && z.Activo
                                                   && !z.Saldada).ToList();
            return new CreditoXClienteMensualDTO()
            {
                Nombre = ClienteServicio.ObtenerNomreCliente(Clte),
                SaldoActual = cargo.Sum(x => x.TotalCargo),
                SaldoCorriente = cargo.Sum(x => DateTime.Now.Date > x.FechaVencimiento ? 0 : x.TotalCargo),
                Vencido = cargo.Sum(x => DateTime.Now.Date < x.FechaVencimiento ? 0 : x.TotalCargo),
                Dias1a7 = cargo.Sum(x => (DateTime.Now.Date - x.FechaVencimiento).Days >= 1 && (DateTime.Now.Date - x.FechaVencimiento).Days <= 7 ? x.TotalCargo : 0),
                Dias8a16 = cargo.Sum(x => (DateTime.Now.Date - x.FechaVencimiento).Days >= 8 && (DateTime.Now.Date - x.FechaVencimiento).Days <= 16 ? x.TotalCargo : 0),
                Dias17a31 = cargo.Sum(x => (DateTime.Now.Date - x.FechaVencimiento).Days >= 17 && (DateTime.Now.Date - x.FechaVencimiento).Days <= 31 ? x.TotalCargo : 0),
                Dias32a61 = cargo.Sum(x => (DateTime.Now.Date - x.FechaVencimiento).Days >= 32 && (DateTime.Now.Date - x.FechaVencimiento).Days <= 61 ? x.TotalCargo : 0),
                Dias62a91 = cargo.Sum(x => (DateTime.Now.Date - x.FechaVencimiento).Days >= 62 && (DateTime.Now.Date - x.FechaVencimiento).Days <= 91 ? x.TotalCargo : 0),
                Mas91 = cargo.Sum(x => (DateTime.Now.Date - x.FechaVencimiento).Days > 91 ? x.TotalCargo : 0),
            };
        }

        public static CreditoXClienteMensualDTO SumaCreditoMensual(List<CreditoXClienteMensualDTO> lista)
        {
            return new CreditoXClienteMensualDTO()
            {
                Nombre = "Total",
                SaldoActual = lista.Sum(x => x.SaldoActual),
                SaldoCorriente = lista.Sum(x => x.SaldoCorriente),
                Vencido = lista.Sum(x => x.Vencido),
                Dias1a7 = lista.Sum(x => x.Dias1a7),
                Dias8a16 = lista.Sum(x => x.Dias8a16),
                Dias17a31 = lista.Sum(x => x.Dias17a31),
                Dias32a61 = lista.Sum(x => x.Dias32a61),
                Dias62a91 = lista.Sum(x => x.Dias62a91),
                Mas91 = lista.Sum(x => x.Mas91),
            };
        }

    }
}
