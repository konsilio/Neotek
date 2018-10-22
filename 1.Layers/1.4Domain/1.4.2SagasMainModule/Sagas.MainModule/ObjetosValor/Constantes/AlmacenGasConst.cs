using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Constantes
{
    public static class AlmacenGasConst
    {
        //Nombres de elementos
        public const string NombreAlmacenAlterno = "Almcén Alterno No. {0}";
        public const string AlmacenPrincipal = "AlmacenPrincipal";
        public const string Tractor = "Tractor";
        public const string P5000 = "P5000";

        //Procesos
        public const string Descarga = "Descarga";
        public const string Recarga = "Recarga";
        public const string AutoConsumo = "Auto-consumo";
        public const string Traspaso = "Traspaso";
        public const string Calibracion = "Calibración";
        public const string TomaDeLectura = "Toma de lectura";

        //Eventos
        public const string Entrada = "Entrada";        
        public const string Salida = "Salida";
    }
}
