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
    }
}
