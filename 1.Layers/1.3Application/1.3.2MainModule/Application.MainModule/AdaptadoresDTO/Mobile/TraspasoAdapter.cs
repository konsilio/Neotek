using System.Collections.Generic;
using System.Linq;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class TraspasoAdapter
    {
        public static DatosTraspasoDto ToDTO(List<UnidadAlmacenGas> pipas, short predeterminada, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosTraspasoDto()
            {
                predeterminada = predeterminada,
                pipas = pipas.Select(x=>ToDTOPipas(x,medidores)).ToList(),
                medidores = TipoMedidorAdapter.ToDto(medidores)

            };
        }

        public static PipaDto ToDTOPipas(UnidadAlmacenGas pipa, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new PipaDto()
            {
                CantidadP5000 = pipa.P5000Actual,
                IdAlmacenGas = pipa.IdCAlmacenGas,
                IdTipoMedidor = pipa.IdTipoMedidor,
                Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x => x.IdTipoMedidor.Equals(pipa.IdTipoMedidor))),
                NombreAlmacen = pipa.Numero,
                PorcentajeMedidor = pipa.PorcentajeActual
            };
        }
        public static EstacionesDto ToDTOEstaciones(UnidadAlmacenGas estacion,List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new EstacionesDto()
            {
                CantidadP5000 = estacion.P5000Actual,
                IdAlmacenGas = estacion.IdCAlmacenGas,
                IdTipoMedidor = estacion.IdTipoMedidor,
                NombreAlmacen = estacion.Numero,
                PorcentajeMedidor = estacion.PorcentajeActual,
                Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x=>x.IdTipoMedidor.Equals(estacion.IdTipoMedidor))),
            };
        }

        public static DatosTraspasoDto ToDTO(List<UnidadAlmacenGas> estaciones, List<UnidadAlmacenGas> pipas, short predeterminada, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosTraspasoDto()
            {
                predeterminada = predeterminada,
                pipas = pipas.Select(x=>ToDTOPipas(x,medidores)).ToList(),
                medidores = TipoMedidorAdapter.ToDto(medidores),
                estaciones = estaciones.Select(x=>ToDTOEstaciones(x,medidores)).ToList()
            };
        }
    }
}
