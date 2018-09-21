using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public class TipoProveedorServicio
    {
        public static List<TipoProveedor> Obtener()
        {
            //var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());
            return new TipoProveedorDataAccess().BuscarTodos();
        }
    }
}
