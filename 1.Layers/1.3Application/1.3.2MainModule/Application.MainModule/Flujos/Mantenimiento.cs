﻿using Application.MainModule.AdaptadoresDTO.EquipoTrasnporteServicio;
using Application.MainModule.AdaptadoresDTO.MantenimientoDetalleAdapter;
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
    public class Mantenimiento
    {
        public List<MantenimientoDTO> TodoCatalogo(short IdEmpresa)
        {
            var lista = MantenimientoServicio.Buscar(IdEmpresa);
            return MantenimientoAdapter.ToDTO(lista);
        }
        public List<MantenimientoDTO> TodoCatalogo()
        {
            var lista = MantenimientoServicio.Buscar(TokenServicio.ObtenerIdEmpresa());
            return MantenimientoAdapter.ToDTO(lista);
        }
        public RespuestaDto CrearCatalogo(MantenimientoDTO dto)
        {
            var resp = PermisosServicio.PuedeRegistrarMantenimiento();
            if (!resp.Exito) return resp;

            var newMan = MantenimientoAdapter.FromDTO(dto);
            return MantenimientoServicio.Crear(newMan);
        }
        public RespuestaDto ModificarCatalogo(MantenimientoDTO dto)
        {
            var resp = PermisosServicio.PuedeRegistrarMantenimiento();
            if (!resp.Exito) return resp;

            var entidad = MantenimientoServicio.Buscar(dto.Id_Mantenimiento);
            var emty = MantenimientoAdapter.FormEmtity(entidad);
            return MantenimientoServicio.Actualizar(emty);
        }
        public RespuestaDto EliminarCatalogo(MantenimientoDTO dto)
        {
            var resp = PermisosServicio.PuedeRegistrarMantenimiento();
            if (!resp.Exito) return resp;

            var entidad = MantenimientoServicio.Buscar(dto.Id_Mantenimiento);
            var emty = MantenimientoAdapter.FormEmtity(entidad);
            emty.Activo = false;
            return MantenimientoServicio.Actualizar(emty);
        }
        public List<MantenimientoDetalleDTO> Todo()
        { 
            var lista = MantenimientoDetalleServicio.Buscar();
            return MantenimientoDetalleAdapter.ToDTO(lista);
        }
        public RespuestaDto Crear(MantenimientoDetalleDTO dto)
        {
            var resp = PermisosServicio.PuedeRegistrarMantenimiento();
            if (!resp.Exito) return resp;

            var newMan = MantenimientoDetalleAdapter.FromDTO(dto);
            return MantenimientoDetalleServicio.Crear(newMan);
        }
        public RespuestaDto Modificar(MantenimientoDetalleDTO dto)
        {
            var resp = PermisosServicio.PuedeRegistrarMantenimiento();
            if (!resp.Exito) return resp;

            //var entidad = MantenimientoDetalleServicio.Buscar(dto.Id_DetalleMtto);
            var entidad = MantenimientoDetalleAdapter.FromDTO(dto);
            var emty = MantenimientoDetalleAdapter.FormEmtity(entidad);
            return MantenimientoDetalleServicio.Actualizar(emty);
        }
        public RespuestaDto Borrar(int Id_DetalleMtto)
        {
            var resp = PermisosServicio.PuedeRegistrarMantenimiento();
            if (!resp.Exito) return resp;

            var entidad = MantenimientoDetalleServicio.Buscar(Id_DetalleMtto);
            var emty = MantenimientoDetalleAdapter.FormEmtity(entidad);
            return MantenimientoDetalleServicio.Borrar(emty);
        }
    }
}
