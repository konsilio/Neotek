using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.AdaptadoresDTO.Seguridad;
using Application.MainModule.DTOs.Respuesta;
using Sagas.MainModule.Entidades;
using Exceptions.MainModule.Validaciones;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class EmpresaServicio
    {
        public static List<EmpresaDTO> BuscarEmpresas()
        {
            List<EmpresaDTO> lEmpresas = new List<EmpresaDTO>(EmpresaAdapter.ToDTO(new EmpresaDataAccess().BuscarTodos()));
            return lEmpresas;
        }

        public static List<EmpresaDTO> BuscarEmpresas(bool conAC)
        {
            List<EmpresaDTO> lEmpresas = new List<EmpresaDTO>(EmpresaAdapter.ToDTO(new EmpresaDataAccess().BuscarTodos(conAC)));
            return lEmpresas;
        }

        public static List<Empresa> BuscarEmpresasSinAdminCentral()
        {
            return new EmpresaDataAccess().BuscarTodos(false);
        }
        public static List<EmpresaDTO> BuscarEmpresasLogin()
        {            
            return BuscarEmpresas();
        }

        public static Empresa Obtener(short IdEmpresa)
        {
            return new EmpresaDataAccess().Buscar(IdEmpresa);
        }             
        
        public static Empresa Obtener(UnidadAlmacenGas unidad)
        {
            if (unidad != null)
                if (unidad.Empresa != null)
                    return unidad.Empresa;

            return Obtener(unidad.IdEmpresa);
        }

        public static RespuestaDto RegistrarEmpresa(Empresa emp)
        {
            return new EmpresaDataAccess().Insertar(emp);
        }

        public static RespuestaDto ModificarEmpresa(Empresa emp)
        {
            return new EmpresaDataAccess().Actualizar(emp);
        }

        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "La Empresa");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
