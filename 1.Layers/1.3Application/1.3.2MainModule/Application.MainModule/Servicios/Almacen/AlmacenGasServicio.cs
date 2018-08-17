using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Almacen
{
    public static class AlmacenGasServicio
    {
        public static RespuestaDto InsertarDescargaGas(AlmacenGasDescarga alm)
        {
            return new AlmacenGasDescargaDataAccess().Insertar(alm);
        }

        public static RespuestaDto ActualizarDescargaGas(AlmacenGasDescarga alm, List<AlmacenGasDescargaFoto> fotos)
        {
            return new AlmacenGasDescargaDataAccess().Actualizar(alm, fotos);
        }

        public static List<AlmacenGas> ObtenerTodos(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarTodos(idEmpresa);
        }

        public static AlmacenGas Obtener(short idAlmacenGas)
        {
            return new AlmacenGasDataAccess().Buscar(idAlmacenGas);
        }

        public static AlmacenGasDescarga ObtenerDescargaPorOCompraExpedidor(int idOCompra)
        {
            return new AlmacenGasDescargaDataAccess().BuscarOCompraExpedidor(idOCompra);
        }

        public static AlmacenGasDescarga ObtenerDescargaPorClaveOperacion(string claveOperacion)
        {
            return new AlmacenGasDescargaDataAccess().BuscarClaveOperacion(claveOperacion);
        }
    }
}
