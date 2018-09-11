using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class AlmacenLecturaAdapter
    {
        public static AlmacenGasTomaLectura FromDTO(LecturaDTO liadto)
        {

            return new AlmacenGasTomaLectura()
            {
                ClaveOperacion = liadto.ClaveProceso,
                IdCAlmacenGas = liadto.IdCAlmacenGas,
                IdTipoMedidor = liadto.IdTipoMedidor,
                P5000 = liadto.CantidadP5000,
                Porcentaje = liadto.PorcentajeMedidor,
            };
        }
        public static AlmacenGasTomaLecturaFoto FromDTO(string cadenaBase64, short idAlmEntrGasLec, int IdOrden , short numOrden)
        {
            return new AlmacenGasTomaLecturaFoto()
            {
                IdCAlmacenGas = idAlmEntrGasLec,
                IdOrden = IdOrden,
                CadenaBase64 = cadenaBase64,
                IdOrdenFoto = numOrden,
                IdImagenDe = 1,
                
            };
        }

        public static List<AlmacenGasTomaLecturaFoto> FromDTO(List<string> imagenes, short idAlmEntrGasLec, int IdOrden)
        {
            short num = 0;
            return imagenes.ToList().Select(x => FromDTO(x, idAlmEntrGasLec, IdOrden, num++)).ToList();
        }

        public static AlmacenGasTomaLectura FromDTO(LecturaCamionetaDTO lcdto)
        {
            return new AlmacenGasTomaLectura()
            {
                ClaveOperacion = lcdto.ClaveProceso,
                IdCAlmacenGas = lcdto.IdCAlmacenGas,
                
            };
        }

        public static List<AlmacenGasTomaLecturaCilindro> FromDTO(List<decimal> cilindroCantidades,List<short> IdCilindros, short idCAlmacenGas, int idOrden)
        {
            short num = 0;
            int x = 0;
            List<AlmacenGasTomaLecturaCilindro> list = new List<AlmacenGasTomaLecturaCilindro>();
            /*for( x = 0; x < IdCilindros.Count; x++)
            {
                list.Add(FromDTO(cilindroCantidades[x], IdCilindros[x], idCAlmacenGas,idOrden, num));
                num++;
            }*/

            /*x = 0;
            foreach (var idCilindro in IdCilindros)
            {
                list.Add(FromDTO(cilindroCantidades.ElementAt(x), idCilindro, idCAlmacenGas, idOrden, num));
            }*/

            x = -1;
            cilindroCantidades.ForEach(y=> list.Add(FromDTO(y, IdCilindros.ElementAt(x++), idCAlmacenGas, idOrden, num)));

            //return cilindroCantidad.ToList().Select(x => FromDTO(x, idCAlmacenGas, idOrden, num++)).ToList();
            return list;
        }

        public static AlmacenGasTomaLecturaCilindro FromDTO(decimal cantidad,short idCilindro, short idAlmEntrGasLec, int IdOrden, short numOrden)
        {
            return new AlmacenGasTomaLecturaCilindro()
            {
               
                IdCAlmacenGas = idAlmEntrGasLec,
                IdOrden = IdOrden,
                IdOrdenCilindro = numOrden,
                Cantidad = cantidad,
                
            };
        }
    }
}
