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
                ClaveProceso = linicial.ClaveOperacion,
                CantidadP5000 = linicial.P5000.Value,
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

        /// <summary>
        /// permite retornar un objeto ReporteDiaDTO de un registro existente 
        /// en la base de datos de una Camioneta, toma como parametros una 
        /// entidad de tipo Reporte DelDia y una unidad almacen gas en este caso la camioenta
        /// </summary>
        /// <param name="resp">Entidad con el reporte encontrado</param>
        /// <param name="almacen">Unidad Almnacen gas en este caso la camioneta </param>
        /// <returns>Objeto de tipo ReporteDiaDTo con los datos encontrados </returns>
        public static ReporteDiaDTO ToDtoCamioneta(ReporteDelDia resp, UnidadAlmacenGas almacen,ICollection<AlmacenGasTomaLecturaCilindro> cilindrosInicial, ICollection<AlmacenGasTomaLecturaCilindro> cilindrosFinal, AlmacenGasTomaLectura inicial, AlmacenGasTomaLectura final, List<OtrasVentasDto>  otrasVentas, List<VentaPuntoDeVenta> ventasContado, List<VentaPuntoDeVenta> ventasCredito)
        {
            return new ReporteDiaDTO()
            {
                ImporteCredito = resp.ImporteCredito ?? 0,
                Importe = resp.ImporteContado ?? 0,
                IdCAlmacenGas = resp.IdCAlmacenGas ?? 0,
                NombreCAlmacen = almacen.Camioneta.Nombre,
                ClaveReporte = resp.FolioOperacionDia,
                Precio = resp.PrecioLt ?? 0,
                LitrosVenta = resp.LitrosVenta ?? 0,
                Fecha = resp.FechaReporte,
                LecturaInicial = ToDTO(inicial),
                LecturaFinal = ToDTO(final),
                Error = false,
                Mensaje = "Exito",
                EsCamioneta = true,
                Tanques = ToDTO(cilindrosInicial,cilindrosFinal),
                OtrasVentas = otrasVentas

            };
        }
        /// <summary>
        /// permite retornar un objeto ReporteDiaDTO de un registro existente 
        /// en la base de datos de una Camioneta, toma como parametros una 
        /// entidad de tipo Reporte DelDia y una unidad almacen gas en este caso la 
        /// Estación de carburación
        /// </summary>
        /// <param name="resp">Entidad con el reporte encontrado</param>
        /// <param name="almacen">Unidad Almnacen gas en este caso la Estación </param>
        /// <returns>Objeto de tipo ReporteDiaDTo con los datos encontrados </returns>
        public static ReporteDiaDTO ToDtoEstacion(ReporteDelDia resp, UnidadAlmacenGas almacen, AlmacenGasTomaLectura inicial, AlmacenGasTomaLectura final)
        {
            return new ReporteDiaDTO()
            {
                ImporteCredito = resp.ImporteCredito ?? 0,
                Importe = resp.ImporteContado ?? 0,
                IdCAlmacenGas = resp.IdCAlmacenGas ?? 0,
                NombreCAlmacen = almacen.EstacionCarburacion.Nombre,
                ClaveReporte = resp.FolioOperacionDia,
                Precio = resp.PrecioLt ?? 0,
                LitrosVenta = resp.LitrosVenta ?? 0,
                Fecha = resp.FechaReporte,
                LecturaInicial = ToDTO(inicial),
                LecturaFinal = ToDTO(final),
                Error = false,
                Mensaje = "Exito",
                EsCamioneta = false
            };
        }
        /// <summary>
        /// permite retornar un objeto ReporteDiaDTO de un registro existente 
        /// en la base de datos de una Camioneta, toma como parametros una 
        /// entidad de tipo Reporte DelDia y una unidad almacen gas en este caso la 
        /// Pipa
        /// </summary>
        /// <param name="resp">Entidad con el reporte encontrado</param>
        /// <param name="almacen">Unidad Almnacen gas en este caso la Pipa </param>
        /// <returns>Objeto de tipo ReporteDiaDTo con los datos encontrados </returns>
        public static ReporteDiaDTO ToDtoPipa(ReporteDelDia resp, UnidadAlmacenGas almacen, AlmacenGasTomaLectura inicial, AlmacenGasTomaLectura final)
        {
            return new ReporteDiaDTO()
            {
                ImporteCredito = resp.ImporteCredito??0,
                Importe = resp.ImporteContado??0,
                IdCAlmacenGas = resp.IdCAlmacenGas??0,
                NombreCAlmacen = almacen.Pipa.Nombre,
                ClaveReporte = resp.FolioOperacionDia,
                Precio = resp.PrecioLt??0,
                LitrosVenta = resp.LitrosVenta??0,
                Fecha = resp.FechaReporte,
                LecturaInicial = ToDTO(inicial),
                LecturaFinal = ToDTO(final),
                Error = false,
                Mensaje = "Exito",
                EsCamioneta = false
            };
        }
        /// <summary>
        /// Permite generar un objeto de tipo ReporteDiaDTO para el reporte 
        /// del día de mobile para el caso de una pipa ,toma como parametros
        /// una entidad de tipo UnidadAlamcen con el almacen , las lecturas iniciales
        /// y finales de la misma de igual manera, las ventas realizada 
        /// </summary>
        /// <param name="almacen">Uniad almacen del que se hara el reporte de pipa</param>
        /// <param name="lectInicial">Lectura incial actual </param>
        /// <param name="lectFinal">Lectura final del almacen actual</param>
        /// <param name="ventasContado">Ventas de contado del almacen </param>
        /// <param name="ventasCredito">Ventas de credito del almacen </param>
        /// <returns>Entidad de tipo ReporteDiaDTO</returns>
        public static ReporteDiaDTO ToDtoPipa(UnidadAlmacenGas almacen, AlmacenGasTomaLectura lectInicial, AlmacenGasTomaLectura lectFinal, List<VentaPuntoDeVenta> ventasContado, List<VentaPuntoDeVenta> ventasCredito)
        {
            return new ReporteDiaDTO()
            {
                LecturaInicial = ToDTO(lectInicial),
                LecturaFinal = ToDTO(lectFinal),
                IdCAlmacenGas = almacen.IdCAlmacenGas,
                Importe = ventasContado.Sum(x=>x.Total),
                ImporteCredito = ventasCredito.Sum(x=>x.Total),
                NombreCAlmacen = almacen.Pipa.Nombre,
                Medidor = TipoMedidorAdapter.ToDto(almacen.Medidor)

            };
        }
        /// <summary>
        /// Permite genera un objeto DTO con las lecturas 
        /// para el repote del día en el caso de pipa y estación de carburación
        /// </summary>
        /// <param name="lectura">Entidad de tipo AlmacenGasTomaLectura con los datos de la lectura inicial/final a pasar a DTO</param>
        /// <returns>objeto de tipo LecturaAlmacenDto con los datos adaptados</returns>
        public static LecturaAlmacenDto ToDTO(AlmacenGasTomaLectura lectura)
        {
            return new LecturaAlmacenDto()
            {
                ClaveProceso = lectura.ClaveOperacion,
                IdTipoMedidor = lectura.IdTipoMedidor??0,
                IdEstacionCarburacion = lectura.IdCAlmacenGas,
                PorcentajeMedidor = lectura.Porcentaje??0,
                CantidadP5000 = lectura.P5000??0,
               
            };
        }
        /// <summary>
        /// Adaopta los datos del reporte para una estación de carburación 
        /// a un objeto de tipo ReporteDiaDTO con los datos adaptados,
        /// se envian como parametros la unidad almacen , las lecturas iniciales y finales de esta
        /// y las ventas generadas , para retornar un objeto de tipo ReporteDiaDTO adaptado
        /// </summary>
        /// <param name="almacen">Entidad de tipo UnidadAlmacenGas de la estación de carburación</param>
        /// <param name="lectInicial">Entidad de tipo AlmacenGasTomaLectura con la lectura inicial de la estación</param>
        /// <param name="lectFinal">Entidad de tipo AlmacenGasTomaLectura con la lectura inicial de la estación</param>
        /// <param name="ventasContado">Lista de entidades de tipo VentaPuntoDeVenta con las ventas a credito</param>
        /// <param name="ventasCredito">Lista de entidades de tipo VentaPuntoDeVenta con las ventas a contado</param>
        /// <returns>Objeto de tipo ReporteDiaDTO con los valores adaptados </returns>
        public static ReporteDiaDTO ToDtoEstacion(UnidadAlmacenGas almacen, AlmacenGasTomaLectura lectInicial, AlmacenGasTomaLectura lectFinal, List<VentaPuntoDeVenta> ventasContado, List<VentaPuntoDeVenta> ventasCredito)
        {
            return new ReporteDiaDTO()
            {
                LecturaInicial = ToDTO(lectInicial),
                LecturaFinal = ToDTO(lectFinal),
                IdCAlmacenGas = almacen.IdCAlmacenGas,
                Importe = ventasContado.Sum(x => x.Total),
                ImporteCredito = ventasCredito.Sum(x => x.Total),
                NombreCAlmacen = almacen.EstacionCarburacion.Nombre,
                Medidor = TipoMedidorAdapter.ToDto(almacen.Medidor)

            };
        }

        public static ReporteDiaDTO ToDtoCamioneta(UnidadAlmacenGas almacen, ICollection<AlmacenGasTomaLecturaCilindro> cilindrosInicial, ICollection<AlmacenGasTomaLecturaCilindro> cilindrosFinal, List<VentaPuntoDeVenta> ventasContado, List<VentaPuntoDeVenta> ventasCredito,
            AlmacenGasTomaLectura inicial, AlmacenGasTomaLectura final)
        {
            return new ReporteDiaDTO()
            {
                Medidor = TipoMedidorAdapter.ToDto(almacen.Medidor),
                Importe = ventasContado.Sum(x => x.Total),
                ImporteCredito = ventasCredito.Sum(x=>x.Total),
                 LecturaInicial = ToDTO(inicial),
                 LecturaFinal = ToDTO(final),
                 Tanques = ToDTO(cilindrosInicial,cilindrosFinal)

            };
        }

        public static List<TanquesDto> ToDTO(ICollection<AlmacenGasTomaLecturaCilindro> cilindrosInicial, ICollection<AlmacenGasTomaLecturaCilindro> cilindrosFinal)
        {
            return cilindrosInicial.Select(x => ToDTO(x, cilindrosFinal)).ToList();
        }

        public static TanquesDto ToDTO(AlmacenGasTomaLecturaCilindro cilindro, ICollection<AlmacenGasTomaLecturaCilindro> cilindrosFinal)
        {
            var cilindroFinal = cilindrosFinal.Where(x => x.IdCilindro.Equals(cilindro.IdCilindro)).First();
            decimal FINAL = 0;

            if (cilindroFinal != null) { 
                FINAL = cilindro.Cantidad - cilindroFinal.Cantidad;
            }else
            {
                FINAL = cilindroFinal.Cantidad;
            }
            return new TanquesDto()
            {
                NombreTanque = cilindro.Cilindro.CapacidadKg.ToString() + "KG",
                Normal = cilindro.Cantidad,
                Venta =  FINAL
            };
        }

        public static ReporteDelDia FromDTO(ReporteDiaDTO reporteDTO)
        {
            return new ReporteDelDia()
            {
                 FolioOperacionDia = reporteDTO.ClaveReporte,
                 Dia = (byte)reporteDTO.Fecha.Day,
                 Mes = (byte)reporteDTO.Fecha.Month,
                 Year = (short) reporteDTO.Fecha.Year,
                 FechaRegistro = reporteDTO.Fecha,
                 FechaReporte = reporteDTO.Fecha,
                 IdCAlmacenGas = (short)reporteDTO.IdCAlmacenGas,
                 LitrosVenta = reporteDTO.LitrosVenta,
                 KilosVenta = reporteDTO.KilosDeVenta,
                 ImporteContado = reporteDTO.Importe,
                 ImporteCredito = reporteDTO.ImporteCredito,
                 Total = reporteDTO.Importe + reporteDTO.ImporteCredito,

            };
        }
    }
}
