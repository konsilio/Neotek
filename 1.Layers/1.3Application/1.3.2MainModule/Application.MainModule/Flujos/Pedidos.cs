﻿using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.AdaptadoresDTO.Pedidos;
using Application.MainModule.AdaptadoresDTO.Seguridad;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Pedidos;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Pedidos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Pedidos
    {
        public List<PedidoModelDto> ListaPedidos(short idempresa)
        {
            var resp = PermisosServicio.PuedeConsultarPedido();
            if (!resp.Exito) return null;

            //if (TokenServicio.EsSuperUsuario())
            //    return PedidosServicio.Obtener().ToList();

            //else
            //{
                PeriodoDTO periodo = new PeriodoDTO();
                periodo.FechaInicio = DateTime.Parse(string.Concat(DateTime.Now.AddDays(-2).ToShortDateString(), " 00:00:00"));
                periodo.FechaFin = DateTime.Parse(string.Concat(DateTime.Now.ToShortDateString(), " 23:59:59"));

                var pedidos = PedidosServicio.Obtener(idempresa, periodo);
                return PedidosAdapter.ToDTO(pedidos);
            //}
        }               
        public RegistraPedidoDto PedidoId(int idPedido)
        {
            var resp = PermisosServicio.PuedeConsultarPedido();
            if (!resp.Exito) return null;

            return PedidosServicio.Obtener(idPedido);
        }
        public List<EstatusPedidoDto> ListaEstatusPedidos()
        {
            var resp = PermisosServicio.PuedeConsultarPedido();
            if (!resp.Exito) return null;

            return PedidosServicio.ObtenerEstatus().ToList();
        }
        public List<CamionetaDTO> ListaCamionetas(short IdEmpresa)
        {
            var resp = PermisosServicio.PuedeConsultarPedido();
            if (!resp.Exito) return null;

            return PedidosServicio.ObtenerCamionetas(IdEmpresa).ToList();
        }
        public List<PipaDTO> ListaPipas(short IdEmpresa)
        {
            var resp = PermisosServicio.PuedeConsultarPedido();
            if (!resp.Exito) return null;

            return PedidosServicio.ObtenerPipas(IdEmpresa).ToList();
        }
        public RespuestaDto Registra(RegistraPedidoDto pedidoDto)
        {
            var resp = PermisosServicio.PuedeRegistrarPedido();
            if (!resp.Exito) return resp;

            var pedido = PedidosAdapter.FromDto(pedidoDto);

            if (!TokenServicio.EsSuperUsuario() && !TokenServicio.ObtenerEsAdministracionCentral())
                pedido.IdEmpresa = TokenServicio.ObtenerIdEmpresa();

            return PedidosServicio.Alta(pedido);
        }
        public RespuestaDto RegistraEncuesta(List<EncuestaDto> pedidoDto)
        {
            var resp = PermisosServicio.PuedeRegistrarPedido();
            if (!resp.Exito) return resp;            
                var pedido = PedidosAdapter.FromDto(pedidoDto);
                return PedidosServicio.Alta(pedido);
           
        }
        public RespuestaDto Modifica(RegistraPedidoDto pedidoDto)
        {
            var resp = PermisosServicio.PuedeModificarPedido();
            if (!resp.Exito) return resp;

            var pedidos = new PedidosDataAccess().BuscarPedido(pedidoDto.IdPedido);
            if (pedidos == null) return PedidosServicio.NoExiste();
            var pedido = PedidosAdapter.FromDto(pedidoDto, pedidos);
            if (pedido.IdCamioneta > 0)
            {
                var CrudDet = PedidosServicio.Alta(pedido.PedidoDetalle.ToList());
                if (!CrudDet.Exito) return resp;
            }
            var cliente = ClienteServicio.Obtener(pedido.IdCliente);
            if (cliente != null)
            {
                var clienteeditar = ClientesAdapter.FromEntity(cliente);
                clienteeditar.Telefono = pedidoDto.Telefono1;
                clienteeditar.Telefono1 = pedidoDto.Telefono1;
                ClienteServicio.Modificar(clienteeditar);
            }
            if (pedido.IdEstatusPedido.Equals(EstatusPedidoEnum.Surtido))
                pedido.FechaSurtido = DateTime.Now;
            return PedidosServicio.Modificar(pedido);
        }
        public RespuestaDto Elimina(RegistraPedidoDto pedidoDto)
        {
            var resp = PermisosServicio.PuedeEliminarPedido();
            if (!resp.Exito) return resp;

            var pedidos = new PedidosDataAccess().BuscarPedido(pedidoDto.IdPedido);
            if (pedidos == null) return PedidosServicio.NoExiste();

            var pedido = PedidosAdapter.FromEntity(pedidos);
            pedido.IdEstatusPedido = EstatusPedidoEnum.Cancelado;
            pedido.MotivoCancelacion = pedidoDto.MotivoCancelacion;
            return PedidosServicio.Modificar(pedido);
        }
       
    }
}
