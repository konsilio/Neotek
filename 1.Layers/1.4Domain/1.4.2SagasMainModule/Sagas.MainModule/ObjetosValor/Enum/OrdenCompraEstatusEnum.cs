using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class OrdenCompraEstatusEnum
    {

        public static byte Espera_autorizacion = (byte) Estatus.Espera_autorizacion;
        public static byte Proceso_compra = (byte) Estatus.Proceso_compra;
        public static byte Compra_exitosa = (byte) Estatus.Compra_exitosa;
        public static byte Compra_cancelada = (byte) Estatus.Compra_cancelada;
        
    }
    enum Estatus : byte
    {
        Espera_autorizacion = 3,
        Proceso_compra = 4,
        Compra_exitosa = 5,
        Compra_cancelada = 6
    }

}
