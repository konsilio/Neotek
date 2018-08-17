using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Catalogos
    {
        #region Empresas
        public List<EmpresaDTO> ListaEmpresasLogin()
        {
            return EmpresaServicio.BuscarEmpresasLogin();
        }
        public List<EmpresaDTO> ListaEmpresas()
        {
            return EmpresaServicio.BuscarEmpresas();
        }
        public List<EmpresaDTO> ListaEmpresas(bool conAC)
        {
            return EmpresaServicio.BuscarEmpresas(conAC);
        }
        #endregion

        #region Usuarios
        public List<UsuarioDTO> ListaUsuarios(short idEmpresa)
        {
            return UsuarioServicio.ListaUsuarios(idEmpresa);
        }
        #endregion

        #region Productos
        public List<ProductoDTO> ListaProductos(short idEmpresa)
        {
            return ProductoServicios.ListaProductos(idEmpresa);
        }
        #endregion

        #region Proveedor
        public RespuestaDto RegistraProveedor(ProveedorCrearDto provDto)
        {            
            return ProveedorServicio.RegistrarProveedor(ProveedorAdapter.FromDto(provDto));
        }
        
        public RespuestaDto ModificaProveedor(ProveedorModificarDto provDto)
        {
            var provee = ProveedorServicio.Obtener(provDto.IdProveedor);
            if (provee == null) return ProveedorServicio.NoExiste();

            var proveedor = ProveedorAdapter.FromDto(provDto);
            proveedor.FechaRegistro = provee.FechaRegistro;
            return ProveedorServicio.ModificarProveedor(proveedor);
        }

        public RespuestaDto EliminaProveedor(ProveedorEliminarDto provDto)
        {
            var provee = ProveedorServicio.Obtener(provDto.IdProveedor);
            if (provee == null) return ProveedorServicio.NoExiste();

            provee = ProveedorAdapter.FromEntity(provee);
            provee.Activo = false;
            return ProveedorServicio.ModificarProveedor(provee);
        }

        public List<ProveedorDto> ConsultaProveedores(short idEmpresa)
        {
            return ProveedorAdapter.ToDto(ProveedorServicio.Obtener(idEmpresa));
        }

        public ProveedorDto ConsultaProveedor(int idProveedor)
        {
            return ProveedorAdapter.ToDto(ProveedorServicio.Obtener(idProveedor));
        }
        #endregion
    }
}
