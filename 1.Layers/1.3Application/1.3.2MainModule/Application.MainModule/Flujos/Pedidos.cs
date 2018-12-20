﻿using Application.MainModule.AdaptadoresDTO.Pedidos;
using Application.MainModule.DTOs.Pedidos;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Pedidos;
using Application.MainModule.Servicios.Seguridad;
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
        public List<PedidoModelDto> ListaPedidos()
        {
            var resp = PermisosServicio.PuedeConsultarPedido();
            if (!resp.Exito) return null;

            if (TokenServicio.EsSuperUsuario())
                return PedidosServicio.Obtener().ToList();

            else
                return PedidosServicio.Obtener().Where(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa())).ToList();
        }

        public PedidoModelDto PedidoId(int idPedido)
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

        public RespuestaDto Registra(PedidoModelDto pedidoDto)
        {
            var resp = PermisosServicio.PuedeRegistrarPedido();
            if (!resp.Exito) return resp;

            var pedido = PedidosAdapter.FromDto(pedidoDto);

            if (!TokenServicio.EsSuperUsuario() && !TokenServicio.ObtenerEsAdministracionCentral())
                pedido.IdEmpresa = TokenServicio.ObtenerIdEmpresa();

            return PedidosServicio.Alta(pedido);
        }
        public RespuestaDto Modifica(PedidoModelDto pedidoDto)
        {
            var resp = PermisosServicio.PuedeModificarPedido();
            if (!resp.Exito) return resp;

            var pedidos = new PedidosDataAccess().BuscarPedido(pedidoDto.IdPedido);
            if (pedidos == null) return PedidosServicio.NoExiste();

            var pedido = PedidosAdapter.FromDto(pedidoDto, pedidos);
            pedido.FechaRegistro = pedido.FechaRegistro;
            return PedidosServicio.Modificar(pedido);
        }
        public RespuestaDto Elimina(PedidoModelDto pedidoDto)
        {
            var resp = PermisosServicio.PuedeEliminarPedido();
            if (!resp.Exito) return resp;

            var pedidos = new PedidosDataAccess().BuscarPedido(pedidoDto.IdPedido);
            if (pedidos == null) return PedidosServicio.NoExiste();

            var pedido = PedidosAdapter.FromEntity(pedidos);
            pedido.IdEstatusPedido = EstatusPedidoEnum.Cancelado;
            return PedidosServicio.Modificar(pedido);
        }
    }
}