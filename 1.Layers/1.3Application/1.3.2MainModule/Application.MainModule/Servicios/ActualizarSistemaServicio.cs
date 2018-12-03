using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Ventas;
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
            CajaGeneralServicio.ProcesarVentasPuntosDeVenta();
            AlmacenGasServicio.ProcesarInventario();
          
            //ImagenServicio.LimpiarImagenes();
        }
    }
}
