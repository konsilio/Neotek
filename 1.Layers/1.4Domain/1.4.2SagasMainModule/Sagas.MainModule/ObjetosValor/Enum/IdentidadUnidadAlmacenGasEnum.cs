using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class IdentidadUnidadAlmacenGasEnum
    {
        public static byte AlmacenPrincipal = (byte)identidadUnidadAlmacenGas.AlmacenPrincipal;
        public static byte AlmacenAlterno = (byte)identidadUnidadAlmacenGas.AlmacenAlterno;
        public static byte EstacionCarburacion = (byte)identidadUnidadAlmacenGas.EstacionCarburacion;
        public static byte Pipa = (byte)identidadUnidadAlmacenGas.Pipa;
        public static byte Camioneta = (byte)identidadUnidadAlmacenGas.Camioneta;      

    }

    public enum identidadUnidadAlmacenGas : byte
    {
        AlmacenPrincipal = 1,
        AlmacenAlterno = 2,
        EstacionCarburacion = 3,
        Pipa = 4,
        Camioneta = 5,
    }

   public enum stringUnidad { AlmacenPrincipal, AlmacenAlterno, EstacionCarburacion, Pipa, Camioneta };

    public static class SUnidad
    {
        public static string AP = stringUnidad.AlmacenPrincipal.ToString();
        public static string AA = stringUnidad.AlmacenAlterno.ToString();
        public static string EC = stringUnidad.EstacionCarburacion.ToString();
        public static string P = stringUnidad.Pipa.ToString();
        public static string C = stringUnidad.Camioneta.ToString();


    }
}
