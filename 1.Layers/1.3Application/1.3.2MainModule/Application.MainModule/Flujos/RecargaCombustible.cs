using Application.MainModule.AdaptadoresDTO.EquipoTrasnporteServicio;
using Application.MainModule.DTOs.EquipoTransporte;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Equipo;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class RecargaCombustible
    {
        public List<RecargaCombustibleDTO> Todo(short IdEmpresa)
        {
            var lista = RecargaCombustibleServicio.Buscar();
            return RecargaCombustibleAdapter.ToDTO(lista);
        }
        public RespuestaDto Crear(RecargaCombustibleDTO dto)
        {
            var resp = PermisosServicio.PuedeRegistrarRecargaCombustible();
            if (!resp.Exito) return resp;

            var newMan = RecargaCombustibleAdapter.FromDTO(dto);
            return RecargaCombustibleServicio.Crear(newMan);
        }
        public RespuestaDto Modificar(RecargaCombustibleDTO dto)
        {
            var resp = PermisosServicio.PuedeRegistrarRecargaCombustible();
            if (!resp.Exito) return resp;

            var entidad = RecargaCombustibleAdapter.FromDTO(dto);
            var emty = RecargaCombustibleAdapter.FormEmtity(entidad);
            return RecargaCombustibleServicio.Actualizar(emty);
        }
        public RespuestaDto Eliminar(int id)
        {
            var resp = PermisosServicio.PuedeRegistrarMantenimiento();
            if (!resp.Exito) return resp;

            var entidad = RecargaCombustibleServicio.Buscar(id);
            var emty = RecargaCombustibleAdapter.FormEmtity(entidad);
            //emty.Activo = false;
            return RecargaCombustibleServicio.Actualizar(emty);
        }
        public RespuestaDto Eliminar(RecargaCombustibleDTO dto)
        {
            var resp = PermisosServicio.PuedeRegistrarMantenimiento();
            if (!resp.Exito) return resp;

            var entidad = RecargaCombustibleServicio.Buscar(dto.Id_DetalleRecargaComb);
            var emty = RecargaCombustibleAdapter.FormEmtity(entidad);
            //emty.Activo = false;
            return RecargaCombustibleServicio.Borrar(emty);
        }
    }
}
