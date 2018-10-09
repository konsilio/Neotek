using Application.MainModule.Servicios.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios
{
    public static class ActualizarSistemaServicio
    {
        public static void Actualizar()
        {
            AlmacenGasServicio.ProcesarInventario();



            ImagenServicio.LimpiarImagenes();
        }
    }
}
