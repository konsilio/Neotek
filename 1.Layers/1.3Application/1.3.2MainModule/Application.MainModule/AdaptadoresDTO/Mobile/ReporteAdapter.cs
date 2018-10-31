using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Catalogo;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class ReporteAdapter
    {
        public ReporteDiaDTO ToDto(UnidadAlmacenGas almacen, TipoMedidorUnidadAlmacenGas tipoMedidor, AlmacenGasTomaLectura linicial, AlmacenGasTomaLectura lfinal)
        {
            return new ReporteDiaDTO()
            {
                NombreCAlmacen = almacen.Numero,
                Medidor = TipoMedidorAdapter.ToDto(tipoMedidor),
                LecturaInicial = ToDto(linicial),
                LecturaFinal = ToDto(lfinal),
                LitrosVenta = 0,
                Precio = 0,
                Importe = 0,
                ImporteCredito = 0,
                
            };
        }

        private LecturaAlmacenDto ToDto(AlmacenGasTomaLectura linicial)
        {
            return new LecturaAlmacenDto()
            {
                IdTipoMedidor = linicial.IdTipoMedidor.Value,
                ClaveOperacion = linicial.ClaveOperacion,
                PorcentajeP5000 = linicial.P5000.Value,
                PorcentajeMedidor = linicial.Porcentaje.Value,
                IdEstacionCarburacion = linicial.IdCAlmacenGas
            };
        }

        public ReporteDiaDTO ToDto(UnidadAlmacenGas almacen)
        {
            return new ReporteDiaDTO()
            {
                IdCAlmacenGas = almacen.IdCamioneta.Value,
                NombreCAlmacen = almacen.Numero,
                Carburacion = 0,
                KilosDeVenta = 0,
                Precio = 0,
                OtrasVentasTotal = 0,
                Importe = 0,
                ImporteCredito = 0,
                OtrasVentas = new List<OtrasVentasDto>(),
                Tanques = new List<TanquesDto>()
            };
        }

        public ReporteDelDia FormDto(ReporteDiaDTO reporte,OperadorChoferDTO operador,PuntoVenta puntoVenta)
        {
            
            return new ReporteDelDia()
            {
                IdCAlmacenGas = (short) reporte.IdCAlmacenGas,
                LitrosVenta = reporte.LitrosVenta,
                ImporteContado = reporte.Importe,
                PuntoVenta = reporte.NombreCAlmacen,
                IdOperadorChofer = operador.IdOperadorChofer,
                OperadorChofer = operador.Nombre+" "+operador.Apellido1+" "+operador.Apellido2,
                IdEmpresa =operador.IdEmpresa,
                IdPuntoVenta = puntoVenta.IdPuntoVenta,
                ImporteCredito = reporte.ImporteCredito,
                KilosVenta = reporte.KilosDeVenta,
            };
        }
    }
}
