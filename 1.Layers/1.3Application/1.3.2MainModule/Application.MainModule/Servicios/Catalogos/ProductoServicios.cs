using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class ProductoServicios
    {
        public static List<ProductoDTO> ListaProductos(short idEpresa)
        {
            return AdaptadoresDTO.Catalogo.ProductoAdapter.ToDTO(new AccesoADatos.ProductoDataAccess().ListaProductos(idEpresa));
        }
    }
}
