using Application.MainModule.com.admingest;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
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
        public static RespuestaDto Actuualizar(CFDI entidad)
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
        public static Receptor datosReceptor(CFDIDTO dto)
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
            ImpuestoConceptoTrasladado iva = new ImpuestoConceptoTrasladado();

            iva.Base = (float)((det.PrecioUnitarioKg ?? det.PrecioUnitarioProducto) * det.CantidadProducto);
            iva.Impuesto = "002";
            iva.TipoFactor = "Tasa";
            iva.TasaOCuota = 0.16F;
            iva.Importe = (float)((det.PrecioUnitarioKg ?? det.PrecioUnitarioProducto) * det.CantidadProducto) * iva.TasaOCuota;
            _impuesto.Add(iva);

            return new Concepto()
            {
                ClaveProdServ = "01010101",
                NoIdentificacion = det.IdProducto.ToString(),
                Cantidad = (float)det.CantidadProducto,
                ClaveUnidad = det.UnidadMedida,
                Unidad = "Pieza",
                ValorUnitario = (float)(det.PrecioUnitarioKg ?? det.PrecioUnitarioProducto),
                Descripcion = det.ProductoDescripcion,
                Importe = (float)((det.PrecioUnitarioKg ?? det.PrecioUnitarioProducto) * det.CantidadProducto),
                ImpuestoConceptoTrasladado = _impuesto.ToArray(),
            };
        }
    }
}
