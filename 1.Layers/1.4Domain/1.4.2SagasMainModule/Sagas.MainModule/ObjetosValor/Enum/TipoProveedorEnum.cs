using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class TipoProveedorEnum
    {
        public static byte Proveedor = (byte)tipoProveedor.Proveedor;
        public static byte Acreedor = (byte)tipoProveedor.Acreedor;
    }

    enum tipoProveedor : byte
    {
        Proveedor = 1,
        Acreedor = 2,
    }
}
