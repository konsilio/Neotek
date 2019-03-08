using Application.MainModule.com.admingest;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Facturacion
{
    public static class CFDIServicio
    {
        public static RespuestaDto Crear(CFDI entidad)
        {
            return new CFDIDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Actualizar(CFDI entidad)
        {
            return new CFDIDataAccess().Actualizar(entidad);
        }
        public static List<CFDI> Buscar()
        {
            return new CFDIDataAccess().Obtener();
        }
        public static CFDI Buscar(int id)
        {
            return new CFDIDataAccess().Obtener(id);
        }
        public static WsRespFacturacion Timbrar(Comprobante _comp)
        {
            return new WsFactAdmingestControllerService().generarFacturaEstructuraAdmingest(ConfigurationManager.AppSettings["Usuario"], ConfigurationManager.AppSettings["Contrasena"], ConfigurationManager.AppSettings["RFC"], _comp);
        }
        public static Comprobante DatosComprobante(CFDIDTO dto)
        {
            var venta = PuntoVentaServicio.Obtener(dto.Id_FolioVenta);
           
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());
            Comprobante _comp = new Comprobante();
            _comp.Serie = PuntoVentaServicio.ObtenerSerie(venta.IdPuntoVenta);
            _comp.Folio = PuntoVentaServicio.ObtenerFolio(venta.IdPuntoVenta);
            _comp.Fecha = DateTime.Now.ToString();
            _comp.FormaPago = dto.Id_FormaPago.ToString();
            _comp.SubTotal = (float)venta.Subtotal;
            _comp.Moneda = MonedaEnum.PesoMexicano;
            _comp.Total = (float)venta.Total;
            _comp.TipoDeComprobante = TipoComprobanteEnum.Ingreso;
            _comp.MetodoPago = MetodoPagoServicio.Buscar(dto.Id_MetodoPago).MetodoPagoSAT;
            _comp.LugarExpedicion = empresa.CodigoPostal;

            return _comp;
        }
        public static Receptor DatosReceptor(CFDIDTO dto)
        {
            var _cliente = PuntoVentaServicio.Obtener(dto.Id_FolioVenta).CCliente;
            return new Receptor()
            {
                Nombre = _cliente.Nombre + " " + _cliente.Apellido1 + " " + _cliente.Apellido2,
                Rfc = _cliente.Rfc,
                UsoCFDI = dto.UsoCFDI,
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
                ClaveUnidad = _p.UnidadMedida.Acronimo,
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
       
    }
}
