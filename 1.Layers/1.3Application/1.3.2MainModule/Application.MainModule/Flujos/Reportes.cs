using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.AdaptadoresDTO.IngresoEgreso;
using Application.MainModule.DTOs;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.IngresoGasto;
using Application.MainModule.Servicios.Requisiciones;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    //Estos reportes representaran el cubo de informacion 
    public class Reportes
    {
        public List<RepCuentaPorPagarDTO> RepCuentasPorPagar(DateTime periodo)
        {
            var requi = EgresoServicio.BuscarTodos(periodo);
            return EgresoAdapter.ToRepo(requi);
        }
        public List<RepInventarioPorPuntoVentaDTO> RepInventarioPorPuntoVenta(InventarioPorPuntoVentaDTO dto)
        {
            var pipas = PipaServicio.Obtener(dto.Pipas);
            var estaciones = EstacionCarburacionServicio.Obtener(dto.Estaciones);
            return new Almacenes().BuscarInvetarioPorPuntoDeVenta(pipas, estaciones);
        }
        public List<RepHistorioPrecioDTO> RepHistorioPrecios(HistoricoPrecioDTO dto)
        {
            var precios = PrecioVentaGasServicio.ObtenerListaPreciosVentaIdEmp(TokenServicio.ObtenerIdEmpresa(), dto.FechaInicial, dto.FechaFinal);
            return PrecioVentaGasAdapter.ToRepo(precios, dto);
        }
    }
}
