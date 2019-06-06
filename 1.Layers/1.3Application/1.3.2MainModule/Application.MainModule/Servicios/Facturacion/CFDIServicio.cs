using Application.MainModule.AdaptadoresDTO.Facturacion;
using Application.MainModule.com.admingest;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Cobranza;
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
        public static RespuestaDto Crear(CFDI entidad, List<VentaPuntoVentaDTO> Tickets)
        {
            List<RespuestaDto> resp = new List<RespuestaDto>();
            foreach (var t in Tickets)
            {

                var cfdi = Buscar(t.FolioVenta);
                entidad.Id_FolioVenta = t.FolioVenta;
                if (cfdi == null)
                    resp.Add(new CFDIDataAccess().Insertar(entidad));
                else
                    resp.Add(new RespuestaDto() { Exito = true, Id = cfdi.Id_RelTF });
            }

            if (resp.Exists(x => x.Exito.Equals(false)))
                return ErrorAlCrear();
            return Correcto();
        }
        public static RespuestaDto Actualizar(CFDI entidad)
        {
            return new CFDIDataAccess().Actualizar(entidad);
        }
        public static RespuestaDto Actualizar(List<CFDI> entidades)
        {
            return new CFDIDataAccess().Actualizar(entidades);
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

            List<RespuestaDto> resp = new List<RespuestaDto>();

            dto.RespuestaTimbrado = Actualizar(CFDIAdapter.FromDTO(dto));
            return dto;
        }
        public static CFDIDTO Timbrar(Comprobante _comp, CFDIDTO dto, List<VentaPuntoVentaDTO> Tickets)
        {
            _comp.Folio = dto.Folio.ToString();
            _comp.Serie = dto.Serie;
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
            //ActualizarFolioPuntoVenta(dto);
            List<CFDI> cfdis = new List<CFDI>();
            foreach (var t in Tickets)
            {
                var c = CFDIAdapter.FormEmtity(CFDIServicio.Buscar(t.FolioVenta));
                c.URLPdf = dto.URLPdf;
                c.URLXml = dto.URLXml;
                c.UUID = dto.UUID;
                cfdis.Add(c);
            }
            dto.RespuestaTimbrado = Actualizar(cfdis);
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
        public static Comprobante DatosComprobante(FacturacionDTO dto)
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());
            Comprobante _comp = new Comprobante();
            _comp.Fecha = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            _comp.FormaPago = "27";
            _comp.Moneda = MonedaEnum.PesoMexicano;
            _comp.TipoDeComprobante = TipoComprobanteEnum.Ingreso;
            _comp.MetodoPago = MetodoPagoConst.Pago_en_una_sola_exhibición;
            _comp.LugarExpedicion = empresa.CodigoPostal;

            return _comp;
        }
        public static int FolioFacturaGeneral()
        {
            var cfdis = new CFDIDataAccess().Obtener();
            return cfdis.Where(x => x.Serie.Equals("G")).Count() + 1;
        }
        internal static RespuestaDto ActualizarFolioPuntoVenta(CFDIDTO dto)
        {
            var venta = PuntoVentaServicio.Obtener(dto.Id_FolioVenta);
            return PuntoVentaServicio.ActualizarFolio(venta.IdPuntoVenta);
        }
        public static Receptor DatosReceptor(CFDIDTO dto)
        {
            var _cliente = PuntoVentaServicio.Obtener(dto.Id_FolioVenta).CCliente;
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
                UsoCFDI = UsoCFDIServicio.Buscar(2).UsoCFDISAT,
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
        public static List<Concepto> DatosConceptos(FacturacionDTO dto)
        {
            List<Concepto> _conceptos = new List<Concepto>();
            foreach (var item in dto.Tickets)
            {
                var venta = PuntoVentaServicio.Obtener(item.FolioVenta);
                if (venta.VentaPuntoDeVentaDetalle.Count != 0)
                    foreach (var detalle in venta.VentaPuntoDeVentaDetalle)
                    {
                        _conceptos.Add(DatosConceptos(detalle));
                    }
                else
                {
                    _conceptos.Add(DatosConceptos(venta));
                }
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
                NoIdentificacion = det.VentaPuntoDeVenta.FolioVenta,
                Cantidad = (float)det.CantidadProducto,
                ClaveUnidad = _p.UnidadMedida.ClaveSat,
                Unidad = _p.UnidadMedida.Descripcion,
                ValorUnitario = (float)(det.PrecioUnitarioKg ?? det.PrecioUnitarioProducto),
                Descripcion = det.ProductoDescripcion,
                Importe = (float)((det.PrecioUnitarioKg ?? det.PrecioUnitarioProducto) * det.CantidadProducto),
                ImpuestoConceptoTrasladado = _impuesto.ToArray(),
            };
        }
        public static Concepto DatosConceptos(VentaPuntoDeVenta det)
        {
            //En caso de que la factura global donde el total de la venta es el concepto
            List<ImpuestoConceptoTrasladado> _impuesto = new List<ImpuestoConceptoTrasladado>();

            var iva = GenerarImpiestoIVA(det);
            //var _p = ProductoServicio.ObtenerProducto(det.IdProducto);
            _impuesto.Add(iva);
            return new Concepto()
            {
                ClaveProdServ = "01010101",
                NoIdentificacion = "N/A",
                Cantidad = (float)1,
                ClaveUnidad = "EA",
                Unidad = "Elemento ",
                ValorUnitario = (float)det.Total,
                Descripcion = "Venta de Gas",
                Importe = (float)det.Total,
                ImpuestoConceptoTrasladado = _impuesto.ToArray(),
            };
        }
        public static Complemento DatosPago(Abono abono, CFDIDTO cfdi)
        {
            Complemento _complemento = new Complemento();
            List<Pago> pagos = new List<Pago>();
            Pago p = new Pago();

            p.FechaPago = abono.FechaAbono.ToString("yyyy - MM - dd hh: mm:ss");
            p.FormaDePagoP = abono.IdFormaPago < 10 ? string.Concat("0", abono.IdFormaPago.ToString()) : abono.IdFormaPago.ToString();
            p.MonedaP = MonedaEnum.PesoMexicano;
            p.Monto = float.Parse(abono.MontoAbono.ToString());
            p.DoctoRelacionado = DocumentoRelacionado(abono, cfdi).ToArray();
            pagos.Add(p);

            _complemento.Pago = pagos.ToArray();
            return _complemento;
        }
        public static List<DoctoRelacionado> DocumentoRelacionado(Abono abono, CFDIDTO cfdi)
        {
            List<DoctoRelacionado> respuesta = new List<DoctoRelacionado>();
            DoctoRelacionado dr = new DoctoRelacionado();

            dr.IdDocumento = cfdi.UUID;
            dr.Serie = cfdi.Serie;
            dr.Folio = cfdi.Folio.ToString();
            dr.MonedaDR = MonedaEnum.PesoMexicano;
            dr.MetodoDePagoDR = cfdi.Id_MetodoPago.Equals(0) ? MetodoPagoConst.Pago_en_una_sola_exhibición : MetodoPagoServicio.Buscar(cfdi.Id_MetodoPago).MetodoPagoSAT;
            dr.NumParcialidad = CobranzaServicio.CalcularNumAbono(abono);
            dr.ImpSaldoAnt = float.Parse(CobranzaServicio.CalcularNumSaldoAnteriorAbono(abono).ToString());
            dr.ImpPagado = float.Parse(abono.MontoAbono.ToString());
            dr.ImpSaldoInsoluto = float.Parse(CobranzaServicio.CalcularNumSaldoInsolutoAbono(abono).ToString());

            respuesta.Add(dr);
            return respuesta;
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
        public static ImpuestoConceptoTrasladado GenerarImpiestoIVA(VentaPuntoDeVenta det)
        {
            ImpuestoConceptoTrasladado iva = new ImpuestoConceptoTrasladado();
            iva.Base = (float)(det.Subtotal);
            iva.Impuesto = ImpuestosEnum.IVA;
            iva.TipoFactor = TipoFactorEnum.Tasa;
            iva.TasaOCuota = 0.16F;
            iva.Importe = (float)det.Iva;
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
            return Correcto();

        }
        private static RespuestaDto Correcto()
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
        private static RespuestaDto ErrorAlCrear()
        {
            return new RespuestaDto()
            {
                Exito = false,
                Mensaje = Error.F0003,
                MensajesError = new List<string>()
            };
        }
    }
}
