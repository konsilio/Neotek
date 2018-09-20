using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Respuesta;
using Sagas.MainModule.Entidades;

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
    }
}
