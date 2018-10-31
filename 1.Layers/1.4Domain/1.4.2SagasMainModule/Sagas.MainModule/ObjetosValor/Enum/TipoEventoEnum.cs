using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class TipoEventoEnum
    {
        // Inicial y Finala se utilizan para determinar si la operación es la inicial o la final
        public static byte Inicial = (byte)tipoEvento.Inicial;
        public static byte Final = (byte)tipoEvento.Final;

        // Se para indicar el tipo de proceso que se realizó
        public static byte Descarga = (byte)tipoEvento.Descarga;
        public static byte Recarga = (byte)tipoEvento.Recarga;
        public static byte AutoConsumo = (byte)tipoEvento.AutoConsumo;
        public static byte Traspaso = (byte)tipoEvento.Traspaso;
        public static byte Calibracion = (byte)tipoEvento.Calibracion;
        public static byte TomaLectura = (byte)tipoEvento.TomaLectura;
        public static byte EmpresaNueva = (byte)tipoEvento.EmpresaNueva;

        enum tipoEvento : byte
        {
            Inicial = 1,
            Final = 2,
            Descarga = 3,
            Recarga = 4,
            AutoConsumo = 5,
            Traspaso = 6,
            Calibracion = 7,
            TomaLectura = 8,
            EmpresaNueva = 9
        }
    }
}
