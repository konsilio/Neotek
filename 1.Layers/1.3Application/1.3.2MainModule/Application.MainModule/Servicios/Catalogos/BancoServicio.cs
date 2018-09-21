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
    public class BancoServicio
    {
        public static List<Banco> Obtener()
        {
            //var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            //if (empresa.EsAdministracionCentral)
                return new BancoDataAccess().BuscarTodos();
            //else
            //    return new BancoDataAccess().BuscarTodos(empresa.IdEmpresa);
        }
    }
}
