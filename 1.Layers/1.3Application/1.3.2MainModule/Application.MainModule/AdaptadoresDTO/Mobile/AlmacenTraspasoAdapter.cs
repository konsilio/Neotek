using System;
using System.Linq;
using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class AlmacenTraspasoAdapter
    {
        public static AlmacenGasTraspaso FromDTO(TraspasoDto dto, short ordenTraspaso,short idEmpresa)
        {
            int num = 0;
            return new AlmacenGasTraspaso()
            {
                IdCAlmacenGasEntrada = dto.IdCAlmacenGasEntrada,
                IdCAlmacenGasSalida = dto.IdCAlmacenGasSalida,
                IdTipoMedidorSalida = dto.IdTipoMedidorSalida,
                P5000Entrada = dto.P5000Entrada,
                P5000Salida = dto.P5000Salida,
                PorcentajeSalida = dto.PorcentajeSalida,
                IdEmpresa = idEmpresa,
                Fotografias = dto.Imagenes.Select(x => FromDTO(x, ordenTraspaso, num++, dto.FechaRegistro,dto.IdCAlmacenGasEntrada,dto.IdCAlmacenGasSalida,idEmpresa)).ToList()
            };
        }

        public static AlmacenGasTraspasoFoto FromDTO(string imagen, short ordenTraspaso, int orden, DateTime fechaRegistro, short idCAlmacenGasEntrada, short idCAlmacenGasSalida, short idEmpresa)
        {
            return new AlmacenGasTraspasoFoto()
            {
                CadenaBase64 = imagen,
                Orden = ordenTraspaso,
                Dia = (byte)fechaRegistro.Day,
                Mes = (byte)fechaRegistro.Month,
                Year = (short)fechaRegistro.Year,
                OrdenImagen = (short)orden,
                IdCAlmacenGasEntrada = idCAlmacenGasEntrada,
                IdCAlmacenGasSalida = idCAlmacenGasSalida,
                IdEmpresa = idEmpresa,
            };
        }

    }
}
