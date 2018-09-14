using System;
using System.Collections.Generic;
using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.Entidades;
using System.Linq;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class AlmacenRecargaAdapter
    {
        public static AlmacenGasRecarga FromDTO(RecargaDTO rdto)
        {
            return new AlmacenGasRecarga()
            {
                ClaveOperacion = rdto.ClaveOperacion,
                IdCAlmacenGasEntrada = rdto.IdCAlmacenGasEntrada,
                
            };
        }

        public static List<AlmacenGasRecargaCilindro> FromDTOCilindros(RecargaDTO rdto)
        {
            short num = 1;
            List<AlmacenGasRecargaCilindro> list = new List<AlmacenGasRecargaCilindro>();
            rdto.Cilindros.ForEach(y => list.Add(FromDTOCilindros(y,num++)));
            return list;
        }

        private static AlmacenGasRecargaCilindro FromDTOCilindros(CilindroDto cilindro, short numOrden)
        {
            return new AlmacenGasRecargaCilindro()
            {
                Cantidad = cilindro.Cantidad,
                IdCilindro = cilindro.IdCilindro,
                IdOrden = numOrden
            };
        }

        public static AlmacenGasRecarga FromDTOEvento(RecargaDTO rdto)
        {
            return new AlmacenGasRecarga()
            {
                ClaveOperacion = rdto.ClaveOperacion,
                IdCAlmacenGasEntrada = rdto.IdCAlmacenGasEntrada,
                IdCAlmacenGasSalida = rdto.IdCAlmacenGasSalida,
                IdTipoMedidorEntrada = rdto.IdTipoMedidorEntrada,
                IdTipoMedidorSalida = rdto.IdTipoMedidorSalida,
                P5000Entrada = rdto.P5000Entrada,
                P5000Salida = rdto.P5000Salida
            };
        }

        public  static List<AlmacenGasRecargaFoto> FromDTO(List<string> imagenes)
        {
            short num = 1;
            return imagenes.ToList().Select(x => FromDTO(x, num++)).ToList();
        }

        private static AlmacenGasRecargaFoto FromDTO(string cadenaBase64, short IdOrden)
        {
            return new AlmacenGasRecargaFoto()
            {
                CadenaBase64 = cadenaBase64,
                IdOrden = IdOrden,
                IdImagenDe = 1
            };
        }
    }
}
