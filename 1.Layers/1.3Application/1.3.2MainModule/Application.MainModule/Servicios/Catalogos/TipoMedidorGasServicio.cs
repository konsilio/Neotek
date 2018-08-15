using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class TipoMedidorGasServicio
    {
        public static List<TipoMedidorUnidadAlmacenGas> Obtener()
        {
            return new TipoMedidorGasDataAccess().BuscarTodos();
        }

        public static TipoMedidorUnidadAlmacenGas Obtener(short idTipoMedidor)
        {
            return new TipoMedidorGasDataAccess().Buscar(idTipoMedidor);
        }
    }
}
