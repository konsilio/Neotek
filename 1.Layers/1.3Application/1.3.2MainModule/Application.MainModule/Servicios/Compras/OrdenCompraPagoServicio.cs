using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Compras
{
    public static class OrdenCompraPagoServicio
    {
        public static RespuestaDto Guardar(OrdenCompraPago pago)
        {
            return new OrdenCompraPagoDataAccess().Insertar(pago);
        }
        public static List<OrdenCompraPago> BuscarPagos(int idOC)
        {
            return new OrdenCompraPagoDataAccess().Buscar(idOC);
        }
        public static OrdenCompraPago Buscar(int idOC, short orden)
        {
            return new OrdenCompraPagoDataAccess().Buscar(idOC, orden);
        }
        public static List<OrdenCompraPago> BuscarPagos()
        {
            return new OrdenCompraPagoDataAccess().BuscarTodo();
        }       
        public static RespuestaDto Actualiza(OrdenCompraPago pago, OrdenCompra oc )
        {
            return new OrdenCompraPagoDataAccess().Actualizar(pago, oc);
        }       
    }
}
