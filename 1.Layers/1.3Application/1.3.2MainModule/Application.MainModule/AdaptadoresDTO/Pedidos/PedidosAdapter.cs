using Application.MainModule.AdaptadoresDTO.Seguridad;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Pedidos;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Pedidos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Constantes;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.MainModule;

namespace Application.MainModule.AdaptadoresDTO.Pedidos
{
    public class PedidosAdapter
    {
        public static PedidoModelDto ToDTO(Pedido p)
        {
            var cant = "";          
            if (p.PedidoDetalle != null && !p.PedidoDetalle.Count.Equals(0))
            {
                if (p.IdCamioneta > 0)
                {
                    cant += CalculosGenerales.Truncar(p.PedidoDetalle.Where(x => x.Cilindro20 ?? false).Sum(y => y.Cantidad.Value), 2).ToString() + " " + "Cilindro(s) 20Kg" + ", ";
                    cant += CalculosGenerales.Truncar(p.PedidoDetalle.Where(x => x.Cilindro30 ?? false).Sum(y => y.Cantidad.Value), 2).ToString() + " " + "Cilindro(s) 30Kg" + ", ";
                    cant += CalculosGenerales.Truncar(p.PedidoDetalle.Where(x => x.Cilindro45 ?? false).Sum(y => y.Cantidad.Value), 2).ToString() + " " + "Cilindro(s) 45Kg" + ", ";
                }
                else
                    cant = string.Concat(CalculosGenerales.Truncar(PedidosServicio.ObtenerCantidadVentaPipaEstacion(p.PedidoDetalle.ToList()), 2).ToString(), " Lts.");
            }
            PedidoModelDto usDTO = new PedidoModelDto()
            {
                IdPedido = p.IdPedido,
                IdPedidoDetalle = p.PedidoDetalle.Count > 0 ? p.PedidoDetalle.FirstOrDefault().IdPedidoDetalle : 0,
                IdEstatusPedido = p.IdEstatusPedido,
                EstatusPedido = p.PedidoEstatus.Descripcion,
                Cantidad = cant,
                MotivoCancelacion = p.MotivoCancelacion ?? string.Empty,
                Unidad = AlmacenGasServicio.ObtenerNombreUnidadAlmacenGas(p),
                NombreRfc = ClienteServicio.ObtenerNomreCliente(p.CCliente),
                IdPipa = p.IdPipa ?? 0,
                IdCamioneta = p.IdCamioneta ?? 0,
                IdDireccion = p.IdDireccion,
                ReferenciaUbicacion = "",
                FechaRegistroPedido = p.FechaRegistro,
                FechaEntregaPedido = p.FechaPedido.Value,
                FechaSurtido = p.FechaSurtido,
                Telefono = ClienteServicio.ObtenerTelefono(p.CCliente),

            };
            return usDTO;
        }
        public static List<PedidoModelDto> ToDTO(List<Pedido> lu)
        {
            return lu.ToList().Select(x => ToDTO(x)).ToList();
        }
        public static RegistraPedidoDto ToDTOEdit(Pedido p)
        {
            var cant = "";
            var cant20 = "";
            var cant30 = "";
            var cant45 = "";
            //List<PedidoDetalle> pd = new PedidosDataAccess().Buscar(p.IdPedido);
            foreach (var item in p.PedidoDetalle)
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
          
            var clienteL = p.CCliente.Locaciones.FirstOrDefault();
            List<RespuestaSatisfaccionPedido> pe = p.RespuestaSatisfaccionPedido.ToList();
            RegistraPedidoDto usDTO = new RegistraPedidoDto()
            {
                IdPedido = p.IdPedido,
                IdEmpresa = p.IdEmpresa,
                IdCliente = p.CCliente.IdCliente,
                IdPedidoDetalle = p.PedidoDetalle != null ? p.PedidoDetalle.FirstOrDefault().IdPedidoDetalle : 0,
                IdEstatusPedido = p.IdEstatusPedido,
                EstatusPedido = EstatusPedidoConst.ObtenerString(p.IdEstatusPedido),
                FolioVenta = p.FolioVenta,
                FechaRegistroPedido = p.FechaRegistro,
                FechaPedido = p.FechaPedido.Value,
                IdPipa = p.IdPipa ?? 0,
                IdCamioneta = p.IdCamioneta ?? 0,
                Unidad = p.IdCamioneta > 0 ? AlmacenGasServicio.ObtenerCamioneta(p.IdCamioneta.Value).Nombre : AlmacenGasServicio.ObtenerPipa(p.IdPipa ?? 0).Nombre,
                Ruta = p.Ruta.Value,
                Orden = clienteL.Orden,
                TotalKilos = p.PedidoDetalle.Sum(x => x.TotalKilos) ?? 0,
                TotalLitros = p.PedidoDetalle.Sum(x => x.TotalLitros) ?? 0,
                Cantidad = p.IdCamioneta > 0 ? cant.TrimEnd(' ').TrimEnd(',') : p.PedidoDetalle.Sum(x => x.Cantidad).ToString().Split(',')[0] + " Kg",
                Cantidad20 = cant20,
                Cantidad30 = cant30,
                Cantidad45 = cant45,
                MotivoCancelacion = p.MotivoCancelacion,
                Telefono1 = ClienteServicio.ObtenerTelefono(p.CCliente),
                Rfc = p.CCliente.Rfc,
                Calle = clienteL != null ? string.Concat(clienteL.Calle, " Num. Ext: ", clienteL.NumExt, " Nun. Int: ", clienteL.NumInt) : Error.NoEncontrado,
                Colonia = clienteL != null ? clienteL.Colonia : Error.NoEncontrado,
                NombreRfc = ClienteServicio.ObtenerNomreCliente(p.CCliente),
                ReferenciaUbicacion = clienteL.formatted_address,
                encuesta = pe.Count > 0 ? FromDto(pe) : FromInit(p.IdPedido),
            };
            return usDTO;
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
        public static Pedido FromDto(RegistraPedidoDto pedidoDTO)
        {
            return new Pedido()
            {
                IdCliente = pedidoDTO.IdCliente,
                IdEmpresa = pedidoDTO.IdEmpresa,
                IdEstatusPedido = (short)pedidoDTO.IdEstatusPedido,
                FolioVenta = pedidoDTO.FolioVenta,
                FechaRegistro = DateTime.Now,
                FechaPedido = pedidoDTO.FechaPedido,
                IdPipa = pedidoDTO.IdPipa,
                IdCamioneta = pedidoDTO.IdCamioneta,
                Ruta = pedidoDTO.Ruta,
                IdDireccion = pedidoDTO.Orden,
                PedidoDetalle = FromDtoDetalle(pedidoDTO),
            };
        }
        public static RespuestaSatisfaccionPedido FromDto(EncuestaDto p)
        {
            var resp = p.Respuesta;
            string res = resp.Substring(resp.Length - 1, 1);
            RespuestaSatisfaccionPedido usDTO = new RespuestaSatisfaccionPedido()
            {
                //IdRespuesta = p.IdPregunta,
                IdPedido = p.IdPedido,
                IdPregunta = p.IdPregunta,
                Respuesta = Convert.ToByte(res),
                Activo = true,
            };
            return usDTO;
        }
        public static List<RespuestaSatisfaccionPedido> FromDto(List<EncuestaDto> lu)
        {
            List<RespuestaSatisfaccionPedido> luDTO = lu.ToList().Select(x => FromDto(x)).ToList();
            return luDTO;
        }
        public static PedidoDetalle FromDtoDetalleP45(RegistraPedidoDto pedidoDTO, decimal factor, int idD)
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
        public static PedidoDetalle FromDtoDetalleP20(RegistraPedidoDto pedidoDTO, decimal factor, int idD)
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
        public static PedidoDetalle FromDtoDetalleP30(RegistraPedidoDto pedidoDTO, decimal factor, int idD)
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
        public static PedidoDetalle FromDtoDetallePipa(RegistraPedidoDto pedidoDTO, decimal factor)
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
        public static List<PedidoDetalle> FromDtoDetalle(RegistraPedidoDto pedidoDTO)
        {
            var factorLt = EmpresaServicio.Obtener(pedidoDTO.IdEmpresa).FactorLitrosAKilos;
            List<PedidoDetalle> _lp = new PedidosDataAccess().Buscar(pedidoDTO.IdPedido);
            List<PedidoDetalle> _lst = new List<PedidoDetalle>();
            if (pedidoDTO.IdCamioneta > 0)
            {
                var idDet = 0;
                if (pedidoDTO.Cantidad20 != "" && pedidoDTO.Cantidad20 != "0" && pedidoDTO.Cantidad20 != null)
                {
                    if (_lp != null && _lp.Count > 0)
                        idDet = _lp.Where(x => x.Cilindro20 != null).Count() > 0 ? _lp.Where(x => x.Cilindro20 != null).FirstOrDefault().IdPedidoDetalle : idDet;
                    _lst.Add(FromDtoDetalleP20(pedidoDTO, factorLt, idDet));
                }
                if (pedidoDTO.Cantidad30 != "" && pedidoDTO.Cantidad30 != "0" && pedidoDTO.Cantidad30 != null)
                {
                    if (_lp != null && _lp.Count > 0)
                        idDet = _lp.Where(x => x.Cilindro30 != null).Count() > 0 ? _lp.Where(x => x.Cilindro30 != null).FirstOrDefault().IdPedidoDetalle : idDet;
                    _lst.Add(FromDtoDetalleP30(pedidoDTO, factorLt, idDet));
                }
                if (pedidoDTO.Cantidad45 != "" && pedidoDTO.Cantidad45 != "0" && pedidoDTO.Cantidad45 != null)
                {
                    if (_lp != null && _lp.Count > 0)
                        idDet = _lp.Where(x => x.Cilindro45 != null).Count() > 0 ? _lp.Where(x => x.Cilindro45 != null).FirstOrDefault().IdPedidoDetalle : idDet;
                    _lst.Add(FromDtoDetalleP45(pedidoDTO, factorLt, idDet));
                }
            }
            else
                _lst.Add(FromDtoDetallePipa(pedidoDTO, factorLt));

            return _lst;
        }
        public static Pedido FromDto(RegistraPedidoDto Pedidodto, Pedido catCte)
        {
            var _pedido = FromEntity(catCte);
            _pedido.IdPedido = Pedidodto.IdPedido;
            _pedido.IdCliente = catCte.IdCliente;
            _pedido.IdEmpresa = Pedidodto.IdEmpresa;
            _pedido.IdEstatusPedido = (short)Pedidodto.IdEstatusPedido;
            _pedido.FolioVenta = Pedidodto.FolioVenta;
            _pedido.FechaRegistro = catCte.FechaRegistro;
            _pedido.FechaPedido = Pedidodto.FechaPedido;
            _pedido.IdPipa = Pedidodto.IdPipa;
            _pedido.IdCamioneta = Pedidodto.IdCamioneta;
            _pedido.IdDireccion = Pedidodto.Orden;
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
                MotivoCancelacion = ped.MotivoCancelacion,
            };
        }
        public static PedidoDetalle FromEntity(PedidoDetalle ped)
        {
            return new PedidoDetalle()
            {
                IdPedido = ped.IdPedido,
                IdPedidoDetalle = ped.IdPedidoDetalle,
                Cantidad = ped.Cantidad,
                Cilindro20 = ped.Cilindro20,
                Cilindro30 = ped.Cilindro30,
                Cilindro45 = ped.Cilindro45,
                TotalKilos = ped.TotalKilos,
                TotalLitros = ped.TotalLitros,

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
        public static List<EncuestaDto> FromDto(List<RespuestaSatisfaccionPedido> lu)
        {
            List<EncuestaDto> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            if (luDTO.Count < 5)
            {
                for (int i = luDTO.Count + 1; i < 6; i++)
                {
                    //luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
                    luDTO.Add(ToDTOInit(lu[0].IdPedido, i, i));
                }
            }
            return luDTO;
        }
        public static EncuestaDto ToDTO(RespuestaSatisfaccionPedido lu)
        {
            EncuestaDto usDTO = new EncuestaDto()
            {
                IdRespuesta = lu.IdRespuesta,
                IdPedido = lu.IdPedido,
                IdPregunta = lu.IdPregunta,
                //  Respuesta = lu.Respuesta,
                Pregunta1Val1 = lu.Respuesta >= 1 ? true : false,
                Pregunta1Val2 = lu.Respuesta >= 2 ? true : false,
                Pregunta1Val3 = lu.Respuesta >= 3 ? true : false,
                Pregunta1Val4 = lu.Respuesta >= 4 ? true : false,
                Pregunta1Val5 = lu.Respuesta >= 5 ? true : false,
            };

            return usDTO;
        }
        public static List<EncuestaDto> FromInit(int pedido)
        {
            List<EncuestaDto> luDTO = new List<EncuestaDto>();
            for (int i = 1; i < 6; i++)
            {
                luDTO.Add(ToDTOInit(pedido, i, i));
            }

            return luDTO;
        }
        public static EncuestaDto ToDTOInit(int pedido, int resp, int preg)
        {
            EncuestaDto usDTO = new EncuestaDto()
            {
                IdRespuesta = resp,
                IdPedido = pedido,
                IdPregunta = preg,
                Pregunta1Val1 = false,
                Pregunta1Val2 = false,
                Pregunta1Val3 = false,
                Pregunta1Val4 = false,
                Pregunta1Val5 = false,
            };

            return usDTO;
        }
        public static EstatusPedidoDto ToDTO(PedidoEstatus entidad)
        {
            return new EstatusPedidoDto()
            {
                IdEstatusPedido = entidad.IdPedidoEstatus,
                Descripcion = entidad.Descripcion,
            };
        }
        public static List<EstatusPedidoDto> ToDTO(List<PedidoEstatus> lu)
        {
            return lu.ToList().Select(x => ToDTO(x)).ToList();
        }


        public static RepCallCenterDTO FromDTO(Pedido entidad)
        {
            var cant20 = "0";
            var cant30 = "0";
            var cant45 = "0";
            var lts = "0";
            if (entidad.PedidoDetalle != null && !entidad.PedidoDetalle.Count.Equals(0))
            {
                if (entidad.IdCamioneta > 0)
                {
                    cant20 = decimal.ToInt32(entidad.PedidoDetalle.Where(x => x.Cilindro20 ?? false).Sum(y => y.Cantidad.Value)).ToString();
                    cant30 = decimal.ToInt32(entidad.PedidoDetalle.Where(x => x.Cilindro30 ?? false).Sum(y => y.Cantidad.Value)).ToString();
                    cant45 = decimal.ToInt32(entidad.PedidoDetalle.Where(x => x.Cilindro45 ?? false).Sum(y => y.Cantidad.Value)).ToString();                    
                }
                else                               
                    lts = decimal.ToInt32(PedidosServicio.ObtenerCantidadVentaPipaEstacion(entidad.PedidoDetalle.ToList())).ToString();
                
            }
            return new RepCallCenterDTO()
            {
                IdPedido = entidad.IdPedido,
                RFC = entidad.CCliente.Rfc,
                Estatus = EstatusPedidoConst.ObtenerString(entidad.IdEstatusPedido),
                Observaciones = string.IsNullOrEmpty(entidad.MotivoCancelacion) ? "N/A" : entidad.MotivoCancelacion,
                Fecha = entidad.FechaRegistro,
                //Cantidad = entidad.PedidoDetalle.Count() > 0 ? entidad.PedidoDetalle.Sum(x => x.Cantidad ?? 0) : 0,
                Litros = lts,
                kg20 = cant20,
                kg30 = cant30,
                kg45 = cant45,
                AtendidoPor = EquipoTransporteServicio.ObtenerNombre(entidad) ?? "N/A",
                AtendidoEn = PedidosServicio.ObtenerTimpoAtencion(entidad),
                Pedido = entidad.IdCamioneta != null && !entidad.IdCamioneta.Value.Equals(0) ? TipoVehiculoConst.Camioneta : TipoVehiculoConst.Pipa,
            };
        }
        public static List<RepCallCenterDTO> FromDTO(List<Pedido> entidades)
        {
            return entidades.Select(x => FromDTO(x)).ToList();
        }
    }
}
