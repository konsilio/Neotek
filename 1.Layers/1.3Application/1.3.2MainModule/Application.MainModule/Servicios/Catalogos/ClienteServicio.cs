using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Respuesta;
using Sagas.MainModule.Entidades;
using Exceptions.MainModule.Validaciones;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.Servicios.Seguridad;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class ClienteServicio
    {
        public static List<ClientesDto> ListaClientes()
        {
            List<ClientesDto> lClientes = AdaptadoresDTO.Seguridad.ClientesAdapter.ToDTO(new ClientesDataAccess().Buscar());
            return lClientes;
        }

        public static RespuestaDto AltaCliente(Cliente cte)
        {
            return new ClientesDataAccess().Insertar(cte);
        }

        public static RespuestaDto AltaClienteL(ClienteLocacion cte)
        {
            return new ClientesDataAccess().Insertar(cte);
        }

        public static int BuscarRazon(ClienteDTO cliente)
        {
            var resultado = new ClientesDataAccess().BuscarRazonSocial(cliente,TokenServicio.ObtenerIdEmpresa());
            if (resultado != null)
                return resultado.IdCliente;
            else
                return 0;           
        }

        public static int BuscarCliente(ClienteDTO cliente)
        {
            var resultado = new ClientesDataAccess().Buscar(cliente);
            if (resultado != null)
                return cliente.IdCliente;
            else
                return 0;
        }

        public static Cliente Obtener(int IdCliente)
        {
            return new ClientesDataAccess().Buscar(IdCliente);
        }

        public static ClienteLocacion ObtenerCL(int IdCliente, short Orden)
        {
            return new ClientesDataAccess().BuscarLocacionId(IdCliente, Orden);
        }

        public static List<Cliente> BuscadorClientes(string criterio)
        {
            return new ClientesDataAccess().BuscadorClientes(criterio, TokenServicio.ObtenerIdEmpresa());
        }

        public static List<ClienteLocacionDTO> ObtenerLoc(int IdCliente)
        {
            List<ClienteLocacionDTO> lClientes = AdaptadoresDTO.Seguridad.ClientesAdapter.ToDTOLoc(new ClientesDataAccess().BuscarLocacion(IdCliente));
            return lClientes;
            // return new ClientesDataAccess().Buscar(IdCliente);
        }

        public static RespuestaDto Eliminar(ClienteLocacion cteLoc)
        {
            return new ClientesDataAccess().Eliminar(cteLoc);
        }

        public static RespuestaDto ModificarCL(ClienteLocacion cte)
        {
            return new ClientesDataAccess().Actualizar(cte);
        }

        public static RespuestaDto Modificar(Cliente cte)
        {
            return new ClientesDataAccess().Actualizar(cte);
        }
   
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El cliente");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }

        public static Cliente BuscarClientePorRFC(string rfc)
        {
            return new ClientesDataAccess().Buscar(rfc);
        }

        /// <summary>
        /// Permite realizar la actualización de los
        /// datos de credito del cliente
        /// </summary>
        /// <param name="cliente">Entidad de Cliente con los datos de creditoActualizados</param>
        /// <returns>Restorna la respuesta de la actualización del credito</returns>
        public static RespuestaDto ModificarCredito(Cliente cliente)
        {
           return new ClientesDataAccess().ActualizarCredito(cliente);
        }
    }
}
