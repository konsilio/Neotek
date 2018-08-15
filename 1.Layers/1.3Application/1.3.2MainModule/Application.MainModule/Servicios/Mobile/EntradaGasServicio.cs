using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Mobile
{
    public static class EntradaGasServicio
    {
        public static RespuestaDto RegistrarPapeleta(AlmacenGasDescarga alm)
        {
            alm.FechaEntraGas = DateTime.Now;
            alm.DatosProcesados = false;
            return new AlmacenGasDescargaDataAccess().Insertar(alm);
        }

        public static RespuestaDto Descargar(AlmacenGasDescarga alm)
        {
            return new AlmacenGasDescargaDataAccess().Actualizar(alm);
        }
    }
}
