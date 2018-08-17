using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.DTOs.Respuesta;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class ProveedorServicio
    {
        public static RespuestaDto RegistrarProveedor(Proveedor provee)
        {
            return new ProveedorDataAccess().Insertar(provee);
        }

        public static RespuestaDto ModificarProveedor(Proveedor provee)
        {
            return new ProveedorDataAccess().Actualizar(provee);
        }

        public static List<Proveedor> Obtener(short IdEmpresa)
        {
            var empresa = new EmpresaDataAccess().Buscar(IdEmpresa);

            if (empresa.EsAdministracionCentral)
                return new ProveedorDataAccess().BuscarTodos();
            else
                return new ProveedorDataAccess().BuscarTodos(empresa.IdEmpresa);
        }
        public static Proveedor Obtener(int IdProveedor)
        {
            var proveedor = new ProveedorDataAccess().Buscar(IdProveedor);
            if (proveedor == null)
                return new Proveedor();

            return proveedor;
        }
    }
}
