using Application.MainModule.DTOs.Pedidos;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Pedidos;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Pedidos
{
    public class PedidosAdapter
    {
        public static PedidoModelDto ToDTO(Pedido p)
        {
            var cant = "";
            var cant20 = "";
            var cant30 = "";
            var cant45 = "";
            List<PedidoDetalle> pd = new PedidosDataAccess().Buscar(p.IdPedido);
            foreach (var item in pd)
            {
                if (item.Cilindro20 == true)
                {
                    cant20 = item.Cantidad.ToString();
                }
                if (item.Cilindro30 == true)
                {
                    cant30 = item.Cantidad.ToString();
                }
                if (item.Cilindro45 == true)
                {
                    cant45 = item.Cantidad.ToString();
                }
                if (item.TotalKilos != 0)
                {
                    cant = item.TotalKilos.ToString();
                }
            }
            var estatus = PedidosServicio.GetEstatusPedido(p.IdEstatusPedido).ToString();
            var cliente = ClienteServicio.Obtener(p.IdCliente);

            var uni = p.IdCamioneta != null ? AlmacenGasServicio.ObtenerCamioneta(p.IdCamioneta ?? 0).Nombre : AlmacenGasServicio.ObtenerPipa(p.IdPipa ?? 0).Nombre;
            PedidoModelDto usDTO = new PedidoModelDto()
            {
                IdPedido = p.IdPedido,
                IdEmpresa = p.IdEmpresa,
                IdEstatusPedido = p.IdEstatusPedido,
                EstatusPedido = PedidosServicio.getString(PedidosServicio.GetEstatusPedido(p.IdEstatusPedido).ToString()),
                Cantidad = cant,
                Cantidad20 = cant20,
                Cantidad30 = cant30,
                Cantidad45 = cant45,
                MotivoCancelacion = "",
                Calle = cliente.Domicilio,
                Colonia = cliente.Domicilio,
                Unidad = uni,
                NombreRfc = cliente.Nombre + " " + cliente.Apellido1 + " " + cliente.Apellido2 + " - " + cliente.Rfc,
                IdPipa = p.IdPipa ?? 0,
                IdCamioneta = p.IdCamioneta ?? 0,
                ReferenciaUbicacion = "",
                FechaRegistroPedido = p.FechaRegistro,
                FechaEntregaPedido = p.FechaPedido.Value,
                Empresa = EmpresaServicio.Obtener(p.IdEmpresa).NombreComercial,
                TipoPersonaFiscal = TipoPersonaServicio.TipoPersona(cliente.IdTipoPersona ?? 0).Descripcion,
                RegimenFiscal = RegimenServicio.Regimen(cliente.IdRegimenFiscal ?? 0).Descripcion,
                //public List<PedidoModel> Pedidos 
                //public List<PedidoModel> Unidades 
            };
            return usDTO;
        }
        public static List<PedidoModelDto> ToDTO(List<Pedido> lu)
        {
            List<PedidoModelDto> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            return luDTO;
        }
        public static Pedido FromDto(PedidoModelDto pedidoDTO)
        {
            return new Pedido()
            {
                IdCliente = pedidoDTO.IdCliente,
                IdEmpresa = pedidoDTO.IdEmpresa,
                IdEstatusPedido = (short)pedidoDTO.IdEstatusPedido,
                FolioVenta = pedidoDTO.FolioVenta,
                FechaRegistro = DateTime.Now,
                FechaPedido = pedidoDTO.FechaEntregaPedido,
                IdPipa = pedidoDTO.IdPipa,
                IdCamioneta = pedidoDTO.IdCamioneta,
                Ruta = pedidoDTO.Ruta,
                PedidoDetalle = FromDtoDetalle(pedidoDTO),
            };
        }
        public static PedidoDetalle FromDtoDetalleP45(PedidoModelDto pedidoDTO)
        {
            return new PedidoDetalle()
            {
                IdPedido = pedidoDTO.IdPedido,
                Cantidad = Decimal.Parse(pedidoDTO.Cantidad),
                //Cilindro20 = pedidoDTO.Cantidad20 != "" ? true : false,
                //Cilindro30 = pedidoDTO.Cantidad30 != "" ? true : false,
                Cilindro45 = pedidoDTO.Cantidad45 != "" ? true : false,
                TotalKilos = pedidoDTO.TotalKilos,
                TotalLitros = pedidoDTO.TotalLitros,
            };
        }
        public static PedidoDetalle FromDtoDetalleP20(PedidoModelDto pedidoDTO)
        {
            return new PedidoDetalle()
            {
                IdPedido = pedidoDTO.IdPedido,
                Cantidad = Decimal.Parse(pedidoDTO.Cantidad),
                Cilindro20 = pedidoDTO.Cantidad20 != "" ? true : false,
                //Cilindro30 = pedidoDTO.Cantidad30 != "" ? true : false,
                //Cilindro45 = pedidoDTO.Cantidad45 != "" ? true : false,
                TotalKilos = pedidoDTO.TotalKilos,
                TotalLitros = pedidoDTO.TotalLitros,
            };
        }
        public static PedidoDetalle FromDtoDetalleP30(PedidoModelDto pedidoDTO)
        {
            return new PedidoDetalle()
            {
                IdPedido = pedidoDTO.IdPedido,
                Cantidad = Decimal.Parse(pedidoDTO.Cantidad),
                //Cilindro20 = pedidoDTO.Cantidad20 != "" ? true : false,
                Cilindro30 = pedidoDTO.Cantidad30 != "" ? true : false,
                //Cilindro45 = pedidoDTO.Cantidad45 != "" ? true : false,
                TotalKilos = pedidoDTO.TotalKilos,
                TotalLitros = pedidoDTO.TotalLitros,
            };
        }
        public static List<PedidoDetalle> FromDtoDetalle(PedidoModelDto pedidoDTO)
        {
            List<PedidoDetalle> _lst = new List<PedidoDetalle>();
            _lst.Add(FromDtoDetalleP20(pedidoDTO));
            _lst.Add(FromDtoDetalleP30(pedidoDTO));
            _lst.Add(FromDtoDetalleP45(pedidoDTO));
            return _lst;
        }
        public static Pedido FromDto(PedidoModelDto Pedidodto, Pedido catCte)
        {
            var _pedido = FromEntity(catCte);
            _pedido.IdPedido = Pedidodto.IdPedido;
            _pedido.IdCliente = Pedidodto.IdCliente;
            _pedido.IdEmpresa = Pedidodto.IdEmpresa;
            _pedido.IdEstatusPedido = (short)Pedidodto.IdEstatusPedido;
            _pedido.FolioVenta = Pedidodto.FolioVenta;
            _pedido.FechaRegistro = Pedidodto.FechaRegistroPedido;
            _pedido.FechaPedido = Pedidodto.FechaEntregaPedido;
            _pedido.IdPipa = Pedidodto.IdPipa;
            _pedido.IdCamioneta = Pedidodto.IdCamioneta;
            _pedido.Ruta = Pedidodto.Ruta;
            return _pedido;
        }
        public static Pedido FromEntity(Pedido ped)
        {
            return new Pedido()
            {
                IdPedido = ped.IdPedido,
                IdCliente = ped.IdCliente,
                IdEmpresa = ped.IdEmpresa,
                IdEstatusPedido = ped.IdEstatusPedido,
                FolioVenta = ped.FolioVenta,
                FechaRegistro = ped.FechaRegistro,
                FechaPedido = ped.FechaPedido,
                IdPipa = ped.IdPipa,
                IdCamioneta = ped.IdCamioneta,
                Ruta = ped.Ruta,
            };
        }

    }
}
