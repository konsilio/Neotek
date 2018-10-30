using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.AdaptadoresDTO.Mobile;

namespace Application.MainModule.Servicios.Mobile
{
    public class VentaServicio
    {
        public static RespuestaDto BuscarFolioVenta(VentaDTO Venta)
        {
            return GasServicio.EvaluarClaveOperacion(Venta);
        }

        public static RespuestaDto EvaluarClaveOperacion(AnticipoDto dto)
        {
            return GasServicio.EvaluarClaveOperacion(dto);
        }

        public static RespuestaDto Anticipo(AnticipoDto dto,short idEmpresa,int idUsuario, List<VentaCorteAnticipoEC> anticipos,UnidadAlmacenGas estacion)
        {

            var idOrden = orden(anticipos);
            
            var puntos = PuntoVentaServicio.ObtenerIdEmp(idEmpresa);
            var PuntoVenta = puntos.Find(x => x.IdCAlmacenGas.Equals(dto.IdCAlmacenGas));

            var adapter = AnticiposCortesAdapter.FromDto(dto, idEmpresa, idUsuario, PuntoVenta);

            adapter.Orden = (short)idOrden;
            adapter.FechaAplicacion = dto.Fecha;
            adapter.Dia = (byte) dto.Fecha.Day;
            adapter.Mes = (byte)dto.Fecha.Month;
            adapter.Year = (short)dto.Fecha.Year;
            adapter.FechaRegistro = DateTime.Now;
            adapter.FechaCorteAnticipo = dto.Fecha;
            adapter.DatosProcesados = false;
            adapter.TipoOperacion = "Anticipo";
            adapter.IdTipoOperacion = 1;
            adapter.PuntoVenta = estacion.Numero;
                      
            return GasServicio.Anticipo(adapter); 
        }

        public static int orden(List<VentaCorteAnticipoEC> anticipos)
        {
            if (anticipos != null)
                if (anticipos.Count > 0)
                    return anticipos.Count + 1;
                else
                    return 1;
            else
                return 1;
        }

        public static List<VentaCorteAnticipoEC> ObtenerAnticipos(short idEmpresa)
        {
            return GasServicio.ObtenerAnticipos(idEmpresa);
        }

        public static RespuestaDto EvaluarClaveOperacion(CorteDto dto)
        {
            return GasServicio.EvaluarClaveOperacion(dto);
        }

        public static List<VentaCorteAnticipoEC> ObtenerCortes(short idEmpresa)
        {
            return GasServicio.ObtenerCortes(idEmpresa);
        }

        public static RespuestaDto Corte(CorteDto dto, short idEmpresa, int idUsuario,List<VentaCorteAnticipoEC> cortes,UnidadAlmacenGas estacion)
        {
            var idOrden = orden(cortes);
            var puntos = PuntoVentaServicio.ObtenerIdEmp(idEmpresa);
            var PuntoVenta = puntos.Find(x => x.IdCAlmacenGas.Equals(dto.IdCAlmacenGas));

            var adapter = AnticiposCortesAdapter.FromDto(dto, idEmpresa, idUsuario, PuntoVenta);

            adapter.Orden = (short)idOrden;
            adapter.FechaAplicacion = dto.Fecha;
            adapter.Dia = (byte)dto.Fecha.Day;
            adapter.Mes = (byte)dto.Fecha.Month;
            adapter.Year = (short)dto.Fecha.Year;
            adapter.FechaRegistro = DateTime.Now;
            adapter.FechaCorteAnticipo = dto.Fecha;
            adapter.DatosProcesados = false;
            adapter.TipoOperacion = "Corte caja";
            adapter.IdTipoOperacion = 2;
            adapter.PuntoVenta = estacion.Numero;
            return GasServicio.Corte(adapter);
        }
    }
}
