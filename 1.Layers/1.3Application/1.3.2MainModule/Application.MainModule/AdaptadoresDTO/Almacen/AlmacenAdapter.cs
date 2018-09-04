using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Almacen
{
    public static class AlmacenAdapter
    {
        public static Sagas.MainModule.Entidades.Almacen FromEmtity(Sagas.MainModule.Entidades.Almacen _alm)
        {
            return new Sagas.MainModule.Entidades.Almacen
            {
                IdAlmacen = _alm.IdAlmacen,
                IdEmpresa = _alm.IdEmpresa,
                IdProduto = _alm.IdProduto,
                Cantidad = _alm.Cantidad,
                Ubicacion = _alm.Ubicacion,
                FechaActualizacion = _alm.FechaActualizacion,
                FechaRegistro = _alm.FechaRegistro
            };
        }
    }
}
