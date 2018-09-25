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
}
