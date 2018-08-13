using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Almacen
{
    public static class ProductoAlmacenServicio
    {
        public static Sagas.MainModule.Entidades.Requisicion CalcularAlmacenProcutos(Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            int x = 0;
            foreach (Sagas.MainModule.Entidades.RequisicionProducto _prod in _requisicion.Productos)
            {
                var almacen = new AccesoADatos.AlmacenDataAccess().ProductoAlmacen(_prod.IdProducto);
                _requisicion.Productos.ElementAt(x).CantidadAlmacenActual = almacen != null ? almacen.Cantidad : 0;
                if (_prod.CantidadAlmacenActual.Value > _prod.Cantidad)
                    _requisicion.Productos.ElementAt(x).CantidadAComprar = 0;
                else
                    _requisicion.Productos.ElementAt(x).CantidadAComprar = Math.Abs(_prod.CantidadAlmacenActual.Value - _prod.Cantidad);
            }
            return _requisicion;
        }
    }
}
