using Application.MainModule.DTOs.Catalogo;
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
                if (p.IdCamioneta > 0)
                {
                    if (item.Cilindro20 == true)
                    {
                        cant += item.Cantidad.ToString().Split(',')[0] + " " + "Cilindro(s) 20Kg" + ", ";
                        cant20 = item.Cantidad.ToString().Split(',')[0];
                    }
                    if (item.Cilindro30 == true)
                    {
                        cant += item.Cantidad.ToString().Split(',')[0] + " " + "Cilindro(s) 30Kg" + ", ";
                        cant30 = item.Cantidad.ToString().Split(',')[0];
                    }
                    if (item.Cilindro45 == true)
                    {
                        cant += item.Cantidad.ToString().Split(',')[0] + " " + "Cilindro(s) 45Kg" + ", ";
                        cant45 = item.Cantidad.ToString().Split(',')[0];
                    }
                }
            }
            var cliente = ClienteServicio.Obtener(p.IdCliente);
            var clienteL = ClienteServicio.ObtenerCL(p.IdCliente, p.IdDireccion);
            var uni = p.IdCamioneta > 0 ? AlmacenGasServicio.ObtenerCamioneta(p.IdCamioneta.Value).Nombre : AlmacenGasServicio.ObtenerPipa(p.IdPipa ?? 0).Nombre;
            PedidoModelDto usDTO = new PedidoModelDto()
            {
                IdPedido = p.IdPedido,
                IdPedidoDetalle = pd[0].IdPedidoDetalle,
                IdEmpresa = p.IdEmpresa,
                IdEstatusPedido = p.IdEstatusPedido,
                EstatusPedido = PedidosServicio.getString(PedidosServicio.GetEstatusPedido(p.IdEstatusPedido).ToString()),
                Cantidad = p.IdCamioneta > 0 ? cant.TrimEnd(' ').TrimEnd(',') : pd[0].Cantidad.ToString().Split(',')[0] + " Kg",
                Cantidad20 = cant20,
                Cantidad30 = cant30,
                Cantidad45 = cant45,
                MotivoCancelacion = "",
                Calle = clienteL.Calle,
                Colonia = clienteL.Colonia,
                Telefono1 = cliente.Telefono1,
                IdCliente = cliente.IdCliente,
                Unidad = uni,
                NombreRfc = cliente.Nombre + " " + cliente.Apellido1 + " " + cliente.Apellido2,
                Rfc = cliente.Rfc,
                IdPipa = p.IdPipa ?? 0,
                IdCamioneta = p.IdCamioneta ?? 0,
                IdDireccion = p.IdDireccion,
                ReferenciaUbicacion = "",
                FechaRegistroPedido = p.FechaRegistro,
                FechaEntregaPedido = p.FechaPedido.Value,
                Empresa = EmpresaServicio.Obtener(p.IdEmpresa).NombreComercial,
                TipoPersonaFiscal = TipoPersonaServicio.TipoPersona(cliente.IdTipoPersona ?? 0).Descripcion,
                RegimenFiscal = RegimenServicio.Regimen(cliente.IdRegimenFiscal ?? 0).Descripcion,
                Pedidos = FromDtoDetalle(pd),
                //public List<PedidoModel> Unidades 
            };
            return usDTO;
        }
        public static List<PedidoModelDto> ToDTO(List<Pedido> lu)
        {
            List<PedidoModelDto> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            return luDTO;
        }

        public static CamionetaDTO ToDTO(Camioneta p)
        {
            CamionetaDTO usDTO = new CamionetaDTO()
            {
                IdCamioneta = p.IdCamioneta,
                IdEmpresa = p.IdEmpresa,
                Numero = p.Numero,
                Nombre = p.Nombre,
                Activo = p.Activo,
                FechaRegistro = p.FechaRegistro,

            };
            return usDTO;
        }
        public static List<CamionetaDTO> ToDTO(List<Camioneta> lu)
        {
            List<CamionetaDTO> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            return luDTO;
        }

        public static PipaDTO ToDTO(Pipa p)
        {
            PipaDTO usDTO = new PipaDTO()
            {
                IdPipa = p.IdPipa,
                IdEmpresa = p.IdEmpresa,
                Numero = p.Numero,
                Nombre = p.Nombre,
                Activo = p.Activo,
                FechaRegistro = p.FechaRegistro,

            };
            return usDTO;
        }
        public static List<PipaDTO> ToDTO(List<Pipa> lu)
        {
            List<PipaDTO> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
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
                IdDireccion = pedidoDTO.Orden,
                PedidoDetalle = FromDtoDetalle(pedidoDTO),
            };
        }
        public static PedidoDetalle FromDtoDetalleP45(PedidoModelDto pedidoDTO, decimal factor, int idD)
        {
            var kg = Decimal.Parse(pedidoDTO.Cantidad45) * 45;
            return new PedidoDetalle()
            {
                IdPedido = pedidoDTO.IdPedido,
                IdPedidoDetalle = idD,
                Cantidad = Decimal.Parse(pedidoDTO.Cantidad45),
                Cilindro45 = true,
                TotalKilos = kg,
                TotalLitros = kg * factor,
            };
        }
        public static PedidoDetalle FromDtoDetalleP20(PedidoModelDto pedidoDTO, decimal factor, int idD)
        {
            var kg = Decimal.Parse(pedidoDTO.Cantidad20) * 20;
            return new PedidoDetalle()
            {
                IdPedido = pedidoDTO.IdPedido,
                IdPedidoDetalle = idD,
                Cantidad = Decimal.Parse(pedidoDTO.Cantidad20),
                Cilindro20 = true,
                TotalKilos = kg,
                TotalLitros = kg * factor,
            };
        }
        public static PedidoDetalle FromDtoDetalleP30(PedidoModelDto pedidoDTO, decimal factor, int idD)
        {
            var kg = Decimal.Parse(pedidoDTO.Cantidad30) * 30;

            return new PedidoDetalle()
            {
                IdPedido = pedidoDTO.IdPedido,
                IdPedidoDetalle = idD,
                Cantidad = Decimal.Parse(pedidoDTO.Cantidad30),
                Cilindro30 = true,
                TotalKilos = kg,
                TotalLitros = kg * factor,
            };
        }
        public static PedidoDetalle FromDtoDetallePipa(PedidoModelDto pedidoDTO, decimal factor)
        {
            return new PedidoDetalle()
            {
                IdPedido = pedidoDTO.IdPedido,
                IdPedidoDetalle = pedidoDTO.IdPedidoDetalle,
                Cantidad = Decimal.Parse(pedidoDTO.Cantidad),
                TotalKilos = Decimal.Parse(pedidoDTO.Cantidad),
                TotalLitros = Decimal.Parse(pedidoDTO.Cantidad) * factor,
            };
        }
        public static List<PedidoDetalle> FromDtoDetalle(PedidoModelDto pedidoDTO)
        {
            var factorLt = EmpresaServicio.Obtener(pedidoDTO.IdEmpresa).FactorLitrosAKilos;
            List<PedidoDetalle> _lp = new PedidosDataAccess().Buscar(pedidoDTO.IdPedido);
            List<PedidoDetalle> _lst = new List<PedidoDetalle>();
            if (pedidoDTO.IdCamioneta > 0)
            {
                var idDet = 0;
                if (pedidoDTO.Cantidad20 != "" && pedidoDTO.Cantidad20 != null)
                {
                    idDet = _lp.Where(x => x.Cilindro20.Value.Equals(true)).FirstOrDefault().IdPedidoDetalle;
                    _lst.Add(FromDtoDetalleP20(pedidoDTO, factorLt, idDet));
                }
                if (pedidoDTO.Cantidad30 != "" && pedidoDTO.Cantidad30 != null)
                {
                    //idDet = _lp.Where(x => x.Cilindro30.Value.Equals(true)).FirstOrDefault().IdPedidoDetalle;
                    idDet = _lp.Where(x => x.Cilindro30 != null).FirstOrDefault().IdPedidoDetalle;
                    _lst.Add(FromDtoDetalleP30(pedidoDTO, factorLt, idDet));
                }
                if (pedidoDTO.Cantidad45 != "" && pedidoDTO.Cantidad45 != null)
                {
                    idDet = _lp.Where(x => x.Cilindro45 != null).FirstOrDefault().IdPedidoDetalle;
                    _lst.Add(FromDtoDetalleP45(pedidoDTO, factorLt, idDet));
                }
            }
            else
                _lst.Add(FromDtoDetallePipa(pedidoDTO, factorLt));

            return _lst;
        }
        public static Pedido FromDto(PedidoModelDto Pedidodto, Pedido catCte)
        {
            var _pedido = FromEntity(catCte);
            _pedido.IdPedido = Pedidodto.IdPedido;
            _pedido.IdCliente = catCte.IdCliente;
            _pedido.IdEmpresa = Pedidodto.IdEmpresa;
            _pedido.IdEstatusPedido = (short)Pedidodto.IdEstatusPedido;
            _pedido.FolioVenta = Pedidodto.FolioVenta;
            _pedido.FechaRegistro = DateTime.Now;
            _pedido.FechaPedido = Pedidodto.FechaEntregaPedido;
            _pedido.IdPipa = Pedidodto.IdPipa;
            _pedido.IdCamioneta = Pedidodto.IdCamioneta;
            _pedido.IdDireccion = Pedidodto.IdDireccion;
            _pedido.Ruta = Pedidodto.Ruta;
            _pedido.PedidoDetalle = FromDtoDetalle(Pedidodto);
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
                IdDireccion = ped.IdDireccion,
                
            };
        }
        public static List<PedidoModelDto> FromDtoDetalle(List<PedidoDetalle> lu)
        {
            List<PedidoModelDto> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            return luDTO;
        }

        public static PedidoModelDto ToDTO(PedidoDetalle lu)
        {
            PedidoModelDto usDTO = new PedidoModelDto()
            {
                IdPedido = lu.IdPedido,
                IdPedidoDetalle = lu.IdPedidoDetalle,
                Cantidad = lu.Cantidad.ToString(),
                Cantidad20 = lu.Cilindro20 != null ? "Si" : "No",
                Cantidad30 = lu.Cilindro30 != null ? "Si" : "No",
                Cantidad45 = lu.Cilindro45 != null ? "Si" : "No",
            };
            return usDTO;
        }
    }
}
