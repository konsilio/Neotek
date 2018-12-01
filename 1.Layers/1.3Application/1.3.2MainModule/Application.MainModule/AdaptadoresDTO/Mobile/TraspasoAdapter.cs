using System.Collections.Generic;
using System.Linq;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile;
using System;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class TraspasoAdapter
    {
        public static DatosTraspasoDto ToDTOPipa(List<Pipa> lpipas, List<Pipa> filtradas, Pipa pipa, List<TipoMedidorUnidadAlmacenGas> medidores,UnidadAlmacenGas unidadAlmacen)
        {
            return new DatosTraspasoDto()
            {
                 pipas = lpipas.Select(x=>ToDTO(x,medidores,x.UnidadAlmacenGas.First())).ToList(),
                 pipaEntrada = filtradas.Select(x=>ToDTO(x,medidores, x.UnidadAlmacenGas.First())).ToList(),
                 PipaPredeterminada = ToDTO(pipa,medidores,pipa.UnidadAlmacenGas.First())
            };
        }

        public static PipaDto ToDTO(Pipa pipa, List<TipoMedidorUnidadAlmacenGas> medidores,UnidadAlmacenGas unidadAlmacen)
        {
            //var unidadAlmacen = unidadesAlmacenes.First(x=>x.IdPipa.Equals(pipa.IdPipa));
            return new PipaDto()
            {
                CantidadP5000 = unidadAlmacen.P5000Actual,
                IdAlmacenGas = unidadAlmacen.IdCAlmacenGas,
                IdTipoMedidor = unidadAlmacen.IdTipoMedidor,
                Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x => x.IdTipoMedidor.Equals(unidadAlmacen.IdTipoMedidor))),
                NombreAlmacen = pipa.Nombre,
                PorcentajeMedidor =unidadAlmacen.PorcentajeActual
            };
        }

        public static DatosTraspasoDto ToDTOEstacion(List<EstacionCarburacion> lestaciones, List<Pipa> lpipas, EstacionCarburacion estacion, List<TipoMedidorUnidadAlmacenGas> medidores, UnidadAlmacenGas unidadAlmacen)
        {
            return new DatosTraspasoDto()
            {
                pipas = lpipas.Select(x=>ToDTO(x,medidores,x.UnidadAlmacenGas.First())).ToList(),
                estaciones = lestaciones.Select(x=>ToDTO(x,x.UnidadAlmacenGas.First(),medidores)).ToList(),
                medidores = TipoMedidorAdapter.ToDto(medidores),
                EstacionPredeterminada = ToDTO(estacion,estacion.UnidadAlmacenGas.First(),medidores)
            };
        }

        public static EstacionesDto ToDTO(EstacionCarburacion estacion, UnidadAlmacenGas unidadAlmacenGas,List<TipoMedidorUnidadAlmacenGas>medidores)
        {
            return new EstacionesDto()
            {
                CantidadP5000 = unidadAlmacenGas.P5000Actual,
                IdAlmacenGas = unidadAlmacenGas.IdCAlmacenGas,
                IdTipoMedidor = unidadAlmacenGas.IdTipoMedidor,
                Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x => x.IdTipoMedidor.Equals(unidadAlmacenGas.IdTipoMedidor))),
                NombreAlmacen = estacion.Nombre,
                PorcentajeMedidor = unidadAlmacenGas.PorcentajeActual
            };
        }
        /*
public static DatosTraspasoDto ToDTO(List<UnidadAlmacenGas> pipas, short predeterminada, List<TipoMedidorUnidadAlmacenGas> medidores)
{
return new DatosTraspasoDto()
{
//predeterminada = predeterminada,
pipas = pipas.Select(x=>ToDTOPipas(x,medidores,x.Pipa)).ToList(),
medidores = TipoMedidorAdapter.ToDto(medidores)

};
}

public static PipaDto ToDTOPipas(UnidadAlmacenGas unidad, List<TipoMedidorUnidadAlmacenGas> medidores,Pipa pipa)
{
return new PipaDto()
{
CantidadP5000 = unidad.P5000Actual,
IdAlmacenGas = unidad.IdCAlmacenGas,
IdTipoMedidor = unidad.IdTipoMedidor,
Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x => x.IdTipoMedidor.Equals(unidad.IdTipoMedidor))),
NombreAlmacen = pipa.Nombre,
PorcentajeMedidor = unidad.PorcentajeActual
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
pipas = pipas.Select(x=>ToDTOPipas(x,medidores,x.Pipa)).ToList(),
medidores = TipoMedidorAdapter.ToDto(medidores),
estaciones = estaciones.Select(x=>ToDTOEstaciones(x,medidores)).ToList()
};
}

public static DatosTraspasoDto ToDTO(List<Pipa> lpipas, Pipa pipaPredeterminada, UnidadAlmacenGas unidadesAlmacen, List<TipoMedidorUnidadAlmacenGas> medidores)
{
var lista = lpipas.FindAll(x => x.IdPipa.Equals(pipaPredeterminada.IdPipa));
return new DatosTraspasoDto()
{
pipas = lpipas.Select(x=>ToDTO(x,medidores)).ToList(),
pipaEntrada = lista.Select(x=>ToDTO(x,medidores)).ToList(),
predeterminada = (short)pipaPredeterminada.IdPipa
};
}

private static PipaDto ToDTO(Pipa pipa, List<TipoMedidorUnidadAlmacenGas> medidores)
{
var unidadAlmacen = pipa.UnidadAlmacenGas.First();
return new PipaDto()
{
CantidadP5000 = unidadAlmacen.P5000Actual,
IdAlmacenGas = unidadAlmacen.IdCAlmacenGas,
IdTipoMedidor = unidadAlmacen.IdTipoMedidor,
NombreAlmacen = pipa.Nombre,
PorcentajeMedidor = unidadAlmacen.PorcentajeActual,
Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x => x.IdTipoMedidor.Equals(unidadAlmacen.IdTipoMedidor))),
};
}
*/
    }
}
