using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.AdaptadoresDTO.Mobile;
using System;
using Application.MainModule.DTOs.Respuesta;
using System.Collections.Generic;

namespace Application.MainModule.Servicios.Mobile
{
    class ClientesServicio
    {
        public static DatosTipoPersonaDto ConsultarTipoPersonas()
        {
            var tpersona = TipoPersonaServicio.ListaTipoPersona();
            var tregimen = RegimenServicio.ListaRegimen();
            //var puntoventa = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
            return TipoPersonaAdapter.ToDto(tpersona,tregimen);
            
        }

        public static ClienteDTO EvaluarCliente(ClienteDTO cliente)
        {
            int id = 0;
            if (!String.IsNullOrEmpty(cliente.RazonSocial))
            {
                id = ClienteServicio.BuscarRazon(cliente);
            }
            else
            {
                id= ClienteServicio.BuscarCliente(cliente);
                
            }
            cliente.IdCliente = (id != 0) ? id : 0;
            return cliente;
        }

        public static RespuestaDto Registar(ClienteDTO cliente,short idEmpresa)
        {
            var adapter = ClienteAdapter.FromDTO(cliente);
            adapter.FechaRegistro = DateTime.Now;
            adapter.Activo = true;
            adapter.IdEmpresa = idEmpresa;
            adapter.RazonSocial = (cliente.RazonSocial!=null) ? cliente.RazonSocial : null;
            return ClienteServicio.AltaCliente(adapter);
        }

        internal static RespuestaDto Modificar(ClienteDTO cliente,short idEmpresa)
        {
            var adapter = ClienteAdapter.FromDTO(cliente);
            adapter.FechaRegistro = DateTime.Now;
            adapter.Activo = true;
            adapter.IdEmpresa = idEmpresa;
            adapter.RazonSocial = (cliente.RazonSocial != null) ? cliente.RazonSocial : null;
            return ClienteServicio.Modificar(adapter);
        }

        public static DatosClientesDto BuscadorClientes(string criterio)
        {
            var clientes =  ClienteServicio.BuscadorClientes(criterio);

            return ClienteAdapter.FromDTO(clientes); 
        }
    }
}
