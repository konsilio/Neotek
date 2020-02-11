using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;

namespace Application.MainModule.Servicios.Catalogos
{
    public class PrecioVentaGasServicio
    {
    
        public static RespuestaDto AltaPrecioVentaGas(List<PrecioVenta> cte)
        {           
            return new PrecioVentaDataAccess().Insertar(cte);
        }

        public static List<PrecioVentaDTO> Obtener()
        { 
            var lPventas = new PrecioVentaDataAccess().BuscarTodos();
            return AdaptadoresDTO.Catalogo.PrecioVentaGasAdapter.ToDTO(lPventas);
        }
        public static PrecioVenta Obtener(short idPrecioVenta)
        {
            return new PrecioVentaDataAccess().BuscarIdPV(idPrecioVenta);
        }

        public static PrecioVentaEstatus Obtener(byte IdPrecioVentaEstatus)
        {
            return new PrecioVentaDataAccess().Buscar(IdPrecioVentaEstatus);
        }

        public static List<PrecioVentaEstatusDTO> ObtenerListEstatus()
        {
            List<PrecioVentaEstatusDTO> lPventas = AdaptadoresDTO.Catalogo.PrecioVentaGasAdapter.ToDTOEstatus(new PrecioVentaDataAccess().Buscar());
            return lPventas;          
        }

        public static List<PrecioVentaDTO> ObtenerPreciosVentaIdEmp(short IdEmpresa)
        {
            var ventas = ObtenerListaPreciosVentaIdEmp(IdEmpresa);
            return AdaptadoresDTO.Catalogo.PrecioVentaGasAdapter.ToDTO(ventas);
        }
        public static List<PrecioVenta> ObtenerListaPreciosVentaIdEmp(short IdEmpresa)
        {
            return new PrecioVentaDataAccess().BuscarTodos(IdEmpresa);
        }
        public static List<PrecioVenta> ObtenerListaPreciosVentaIdEmp(short IdEmpresa, DateTime fi, DateTime ff)
        {
            return new PrecioVentaDataAccess().BuscarTodos(IdEmpresa, fi, ff);
        }
        public static PrecioVenta ObtenerPrecioVigente(short idEmpresa)
        {
            return new PrecioVentaDataAccess().BuscarPrecioVentaVigente(idEmpresa);
        }
        public static PrecioVenta ObtenerPrecioVigenteEstaciones(short idEmpresa)
        {
            return new PrecioVentaDataAccess().BuscarPrecioVentaVigenteEstaciones(idEmpresa);
        }
        public static PrecioVenta ObtenerPrecioVigenteEstacion(short idEmpresa, int idestacion)
        {
            return new PrecioVentaDataAccess().BuscarPrecioVentaVigenteEstaciones(idEmpresa, idestacion);
        }
        public static RespuestaDto Eliminar(PrecioVenta cteLoc)
        {
            return new PrecioVentaDataAccess().Eliminar(cteLoc);
        }
        public static RespuestaDto Modificar(PrecioVenta cte)
        {
            return new PrecioVentaDataAccess().Actualizar(cte);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El precio venta");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
        public static int ObtenerUltimoIdPrecioVenta()
        {
            return new PrecioVentaDataAccess().BuscarUltimoRegistro().IdPrecioVenta;
        }
    }
}
