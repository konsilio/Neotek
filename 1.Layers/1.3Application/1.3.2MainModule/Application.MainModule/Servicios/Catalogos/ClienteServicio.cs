using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class ClienteServicio
    {
        public static List<ClientesDto> ListaClientes()
        {
            List<ClientesDto> lClientes = AdaptadoresDTO.Catalogo.ClientesAdapter.ToDTO(new ClientesDataAccess().Buscar());
            return lClientes;
        }
    }
}
