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

        public static Cliente Obtener(int IdCliente)
        {
            return new ClientesDataAccess().Buscar(IdCliente);
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
    }
}
