using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Mobile
{
    public class VentaServicio
    {
        public static PuntoVenta BuscarFolioVenta(string folioVenta,int idUsuario)
        {
            var Operador = PuntoVentaServicio.ObtenerOperador(idUsuario);

            return null; /*PuntoVentaServicio.Obtener(Operador); */
        }
    }
}
