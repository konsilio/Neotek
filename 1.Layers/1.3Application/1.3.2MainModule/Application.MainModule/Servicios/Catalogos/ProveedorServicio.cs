using Application.MainModule.AdaptadoresDTO.Catalogo;
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
        public static RespuestaDto RegistrarProveedor(ProveedorCrearDto proDto)
        {
            return new ProveedorDataAccess().Insertar(ProveedorAdapter.FromDto(proDto));
        }

        public static List<ProveedorDto> Obtener()
        {
            var empresa = new EmpresaDataAccess().Buscar(Seguridad.TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return ProveedorAdapter.ToDto(new ProveedorDataAccess().BuscarTodos());
            else
                return ProveedorAdapter.ToDto(new ProveedorDataAccess().BuscarTodos(empresa.IdEmpresa));
        }
        public static ProveedorDto Obtener(int IdProveedor)
        {
            var proveedor = new ProveedorDataAccess().Buscar(IdProveedor);
            if (proveedor == null)
                return new ProveedorDto();

            return ProveedorAdapter.ToDto(proveedor);
        }
    }
}
