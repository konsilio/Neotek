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

        public static AlmacenGasTomaLectura FromDTO(LecturaCamionetaDTO lcdto, int idOrden)
        {
            return new AlmacenGasTomaLectura()
            {
                ClaveOperacion = lcdto.ClaveProceso,
                IdCAlmacenGas = lcdto.IdCAlmacenGas,
                IdOrden = idOrden,                
                Cilindros = FromDTOCilindro(lcdto, idOrden)
            };
        }

        public static List<AlmacenGasTomaLecturaCilindro> FromDTOCilindro(LecturaCamionetaDTO lcdto, int idOrden)
        {
            short num = 1;
            int x = 0;
            List<AlmacenGasTomaLecturaCilindro> list = new List<AlmacenGasTomaLecturaCilindro>();
            lcdto.CilindroCantidad.ForEach(y=> list.Add(FromDTOCilindro(y, lcdto.IdCilindro.ElementAt(x++), lcdto.IdCAlmacenGas, idOrden, num++)));
            return list;
        }

        public static AlmacenGasTomaLecturaCilindro FromDTOCilindro(decimal cantidad,short idCilindro, short idAlmEntrGasLec, int IdOrden, short numOrden)
        {
            return new AlmacenGasTomaLecturaCilindro()
            {  
                IdCAlmacenGas = idAlmEntrGasLec,
                IdOrden = IdOrden,
                IdOrdenCilindro = numOrden,
                IdCilindro = idCilindro,
                Cantidad = cantidad,
            };
        }
        
        public static DatosTomaLecturaDto ToDto(List<UnidadAlmacenGas> alms, List<TipoMedidorUnidadAlmacenGas> meds)
        {
            return new DatosTomaLecturaDto()
            {
                Almacenes = AlmacenAdapter.ToDto(alms),
                Medidores = TipoMedidorAdapter.ToDto(meds),
            };
        }

        public static DatosTomaLecturaDto ToDto(List<UnidadAlmacenGas> alms, List<AlmacenGasTomaLectura> lects, List<TipoMedidorUnidadAlmacenGas> meds)
        {
            return new DatosTomaLecturaDto()
            {   
                Almacenes = AlmacenAdapter.ToDto(alms, lects),
                Medidores = TipoMedidorAdapter.ToDto(meds),
            };
        }

        internal static DatosTomaLecturaDto ToDtoReporte(List<UnidadAlmacenGas> alms)
        {
            return new DatosTomaLecturaDto()
            {
                Almacenes = AlmacenAdapter.ToDto(alms)

            };
        }
    }
}
