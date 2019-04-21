using Application.MainModule.AdaptadoresDTO.Facturacion;
using Application.MainModule.com.admingest;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Constantes;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.MainModule;

namespace Application.MainModule.Servicios.Facturacion
{
    public static class CFDIServicio
    {
        public static RespuestaDto Crear(CFDI entidad)
        {
            var cfdi = Buscar(entidad.Id_FolioVenta);
            if (cfdi == null)
                return new CFDIDataAccess().Insertar(entidad);
            else
                return new RespuestaDto() { Exito = true, Id = cfdi.Id_RelTF };
        }
        public static RespuestaDto Actualizar(CFDI entidad)
        {
            return new CFDIDataAccess().Actualizar(entidad);
        }
        public static CFDI Buscar(int id)
        {
            return new CFDIDataAccess().Obtener(id);
        }
        public static CFDI Buscar(string Id_FolioVenta)
        {
            return new CFDIDataAccess().Obtener(Id_FolioVenta);
        }
        public static List<CFDI> Buscar()
        {
            return new CFDIDataAccess().Obtener();
        }
        public static List<CFDI> BuscarPorCliente(int id)
        {
            var CFDIs = new CFDIDataAccess().Obtener();
            var ventas = PuntoVentaServicio.ObtenerVentasPorCliente(id);

            return CFDIs.Where(x => ventas.Any(v => v.FolioVenta.Equals(x.Id_FolioVenta))).ToList();
        }
        public static List<CFDI> BuscarPorRFC(string rfc)
        {
            var CFDIs = new CFDIDataAccess().Obtener();
            var ventas = PuntoVentaServicio.ObtenerVentasPorRFC(rfc);

            return CFDIs.Where(x => ventas.Any(v => v.FolioVenta.Equals(x.Id_FolioVenta))).ToList();
        }
        public static CFDIDTO Timbrar(Comprobante _comp, CFDIDTO dto)
        {
            var respTimbrado = new WsFactAdmingestControllerService().generarFacturaEstructuraAdmingest(ConfigurationManager.AppSettings["Usuario"], ConfigurationManager.AppSettings["Contrasena"], ConfigurationManager.AppSettings["RFC"], _comp);
            dto.RespuestaTimbrado = DatosRespuesta(respTimbrado);
            if (!dto.RespuestaTimbrado.Exito)
            {
                dto.Respuesta = dto.RespuestaTimbrado.Mensaje;
                return dto;
            }
            dto.URLPdf = respTimbrado.urlPdf;
            dto.URLXml = respTimbrado.urlXml;
            dto.UUID = respTimbrado.UUID;
            ActualizarFolioPuntoVenta(dto);
            dto.RespuestaTimbrado = Actualizar(CFDIAdapter.FromDTO(dto));
            return dto;
        }
        public static Comprobante DatosComprobante(CFDIDTO dto)
        {
            var venta = PuntoVentaServicio.Obtener(dto.Id_FolioVenta);
            var cfdi = Buscar(dto.Id_FolioVenta);
            var empresa = EmpresaServicio.Obtener(venta.IdEmpresa);
            Comprobante _comp = new Comprobante();
            _comp.Serie = cfdi != null ? cfdi.Serie : PuntoVentaServicio.ObtenerSerie(venta.IdPuntoVenta);
            _comp.Folio = cfdi != null ? cfdi.Folio.ToString() : PuntoVentaServicio.ObtenerFolio(venta.IdPuntoVenta);
            _comp.Fecha = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            _comp.FormaPago = dto.Id_FormaPago < 10 ? string.Concat("0", dto.Id_FormaPago.ToString()) : dto.Id_FormaPago.ToString();
            _comp.SubTotal = (float)venta.Subtotal;
            _comp.Moneda = MonedaEnum.PesoMexicano;
            _comp.Total = (float)venta.Total;
            _comp.TipoDeComprobante = TipoComprobanteEnum.Ingreso;
            _comp.MetodoPago = dto.Id_MetodoPago.Equals(0) ? MetodoPagoConst.Pago_en_una_sola_exhibición : MetodoPagoServicio.Buscar(dto.Id_MetodoPago).MetodoPagoSAT;
            _comp.LugarExpedicion = empresa.CodigoPostal;

            return _comp;
        }
        internal static RespuestaDto ActualizarFolioPuntoVenta(CFDIDTO dto)
        {
            var venta = PuntoVentaServicio.Obtener(dto.Id_FolioVenta);
            return PuntoVentaServicio.ActualizarFolio(venta.IdPuntoVenta);
        }
        public static Receptor DatosReceptor(CFDIDTO dto)
        {
            var _cliente = ClienteServicio.ObtenerPublicoEnGeneral();
            return new Receptor()
            {
                Nombre = _cliente.Nombre + " " + _cliente.Apellido1 + " " + _cliente.Apellido2,
                Rfc = _cliente.Rfc,
                UsoCFDI = UsoCFDIServicio.Buscar(dto.IdUsoCFDI).UsoCFDISAT,
                CorreoElectronico = _cliente.CorreoElectronico,
            };
        }
        public static Receptor DatosReceptor()
        {//Datos para factura global
            var _cliente = ClienteServicio.ObtenerPublicoEnGeneral();
            return new Receptor()
            {
                Nombre = _cliente.Nombre + " " + _cliente.Apellido1 + " " + _cliente.Apellido2,
                Rfc = _cliente.Rfc,
                UsoCFDI = UsoCFDIServicio.Buscar(1).UsoCFDISAT,
                CorreoElectronico = _cliente.CorreoElectronico,
            };
        }
        public static List<Concepto> DatosConceptos(CFDIDTO dto)
        {
            List<Concepto> _conceptos = new List<Concepto>();
            var venta = PuntoVentaServicio.Obtener(dto.Id_FolioVenta).VentaPuntoDeVentaDetalle;
            foreach (var detalle in venta)
            {
                _conceptos.Add(DatosConceptos(detalle));
            }
            return _conceptos;
        }
        public static Concepto DatosConceptos(VentaPuntoDeVentaDetalle det)
        {
            List<ImpuestoConceptoTrasladado> _impuesto = new List<ImpuestoConceptoTrasladado>();

            var iva = GenerarImpiestoIVA(det);
            var _p = ProductoServicio.ObtenerProducto(det.IdProducto);
            _impuesto.Add(iva);
            return new Concepto()
            {
                ClaveProdServ = _p.ClaveProdServ,
                NoIdentificacion = det.IdProducto.ToString(),
                Cantidad = (float)det.CantidadProducto,
                ClaveUnidad = _p.UnidadMedida.ClaveSat,
                Unidad = _p.UnidadMedida.Descripcion,
                ValorUnitario = (float)(det.PrecioUnitarioKg ?? det.PrecioUnitarioProducto),
                Descripcion = det.ProductoDescripcion,
                Importe = (float)((det.PrecioUnitarioKg ?? det.PrecioUnitarioProducto) * det.CantidadProducto),
                ImpuestoConceptoTrasladado = _impuesto.ToArray(),
            };
        }
        public static ImpuestoConceptoTrasladado GenerarImpiestoIVA(VentaPuntoDeVentaDetalle det)
        {
            ImpuestoConceptoTrasladado iva = new ImpuestoConceptoTrasladado();
            iva.Base = (float)((det.PrecioUnitarioKg ?? det.PrecioUnitarioProducto) * det.CantidadProducto);
            iva.Impuesto = ImpuestosEnum.IVA;
            iva.TipoFactor = TipoFactorEnum.Tasa;
            iva.TasaOCuota = 0.16F;
            iva.Importe = (float)((det.PrecioUnitarioKg ?? det.PrecioUnitarioProducto) * det.CantidadProducto) * iva.TasaOCuota;
            return iva;
        }
        public static RespuestaDto DatosRespuesta(WsRespFacturacion wsResp)
        {
            RespuestaDto _resp = new RespuestaDto();
            _resp.Exito = wsResp.success;
            _resp.Mensaje = wsResp.message;
            return _resp;
        }
        public static RespuestaDto DatosRespuesta(List<CFDIDTO> dtos)
        {
            RespuestaDto _resp = new RespuestaDto();
            _resp.Exito = true;
            _resp.Mensaje = FacturacionConst.M0001;
            foreach (var item in dtos)
            {
                _resp.Mensaje += item.UUID + " ,";
                if (!item.RespuestaTimbrado.Exito)
                {
                    _resp.Exito = false;
                    _resp.MensajesError = item.RespuestaTimbrado.MensajesError;
                    return _resp;
                }
            }
            _resp.Mensaje = _resp.Mensaje.TrimEnd(',');
            return _resp;
        }
        public static List<VentaPuntoDeVenta> DescartarFacturados(List<VentaPuntoDeVenta> ventas, List<CFDIDTO> Facturas)
        {
            List<VentaPuntoDeVenta> list = new List<VentaPuntoDeVenta>();
            foreach (var v in ventas)
                if (!Facturas.Exists(x => x.Id_FolioVenta.Equals(v.FolioVenta) && x.UUID.Trim() != string.Empty))
                    list.Add(v);
            return list;
        }
        public static RespuestaDto ValidarRangoBusqueda(FacturacionDTO dto)
        {
            if (!FechasFunciones.validaFechaInicialFinal(dto.FechaIni, dto.FechaFinal))
                return ErrorFechasRango();
            if (!FechasFunciones.validaFechaDentroDelMes(dto.FechaIni) || !FechasFunciones.validaFechaDentroDelMes(dto.FechaFinal))
                return ErrorFechasMesInvalido();
            return FechasCorrectas();

        }
        private static RespuestaDto FechasCorrectas()
        {
            return new RespuestaDto()
            {
                Exito = true
            };
        }
        private static RespuestaDto ErrorFechasMesInvalido()
        {
            return new RespuestaDto()
            {
                Exito = false,
                Mensaje = Error.F0002,
                MensajesError = new List<string>()
            };
        }
        private static RespuestaDto ErrorFechasRango()
        {
            return new RespuestaDto()
            {
                Exito = false,
                Mensaje = Error.F0002,
                MensajesError = new List<string>()
            };
        }
    }
}
