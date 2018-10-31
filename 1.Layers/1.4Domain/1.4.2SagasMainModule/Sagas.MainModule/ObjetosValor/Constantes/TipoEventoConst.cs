using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Constantes
{
    //public static class TipoEventoConst
    //{
    //    public const string UbicacionPendiente = "Ubicación pendiente";
    //    public const string ErrorParcial = "Algunos cambios no se realizaron correctamente";
    //    public const string Actualizacion = "(Actualización){0}";
    //}
    public enum TipoEventoConst { Inicial, Final, Descarga, Recarga, AutoConsumo, Traspaso, Calibración , TomaDeLectura, Venta };

    public static class TipoEventoConstStr
    {
        public static string TI = TipoEventoConst.Inicial.ToString();
        public static string TF = TipoEventoConst.Final.ToString();
        public static string TD = TipoEventoConst.Descarga.ToString();
        public static string TR = TipoEventoConst.Recarga.ToString();
        public static string TAC = TipoEventoConst.AutoConsumo.ToString();
        public static string TT = TipoEventoConst.Traspaso.ToString();
        public static string TC = TipoEventoConst.Calibración.ToString();
        public static string TTL = TipoEventoConst.TomaDeLectura.ToString();
        public static string TV = TipoEventoConst.Venta.ToString();


    }
}
