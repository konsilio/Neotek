using System;
using System.Collections.Generic;
using System.Linq;
using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class AlmacenAutoconsumoAdapter
    {
        public static AlmacenGasAutoConsumo FormDTO(AutoconsumoDTO dto)
        {
            return new AlmacenGasAutoConsumo()
            {
                ClaveOperacion = dto.ClaveOperacion,
                IdCAlmacenGasEntrada = dto.IdCAlmacenGasEntrada,
                IdCAlmacenGasSalida = dto.IdCAlmacenGasSalida,
                P5000Salida = dto.P5000Salida                
            };
        }

        public static List<AlmacenGasAutoConsumoFoto> FormDTO(AutoconsumoDTO dto, UnidadAlmacenGas almacenEntrada, UnidadAlmacenGas almacenSalida,short IdOrden,short IdEmpresa)
        {
            short num = 0;
            return dto.Imagenes.ToList().Select(x => FromDTO(x, almacenEntrada, almacenSalida,dto,IdOrden,num++,IdEmpresa)).ToList();
        }

        public static AlmacenGasAutoConsumoFoto FromDTO(string CadenaBase64, UnidadAlmacenGas almacenEntrada, UnidadAlmacenGas almacenSalida, AutoconsumoDTO dto, short idOrden, short idOrdenImagen,short IdEmpresa)
        {
            return new AlmacenGasAutoConsumoFoto()
            {
                CadenaBase64 = CadenaBase64,
                IdCAlmacenGasEntrada = almacenEntrada.IdCAlmacenGas,
                IdCAlmacenGasSalida = almacenSalida.IdCAlmacenGas,
                OrdenImagen = idOrdenImagen,
                Orden = idOrdenImagen,
                Dia = (byte) dto.FechaRegistro.Day,
                Mes = (byte) dto.FechaRegistro.Month,
                Year = (short) dto.FechaRegistro.Year,
                IdEmpresa =  IdEmpresa
            };
        }

        public static DatosAutoconsumoDto ToDTO(List<UnidadAlmacenGas>almacenes,UnidadAlmacenGas predeterminado, List<Pipa> pipas, List<Camioneta> camionetas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosAutoconsumoDto()
            {
                EstacionEntrada = ToDTO(pipas, medidores),
                EstacionSalida = ToDTO(pipas,camionetas,medidores),
                Predeterminada = ToDTO(predeterminado,medidores.Single(x=>x.IdTipoMedidor.Equals(predeterminado.IdTipoMedidor)))
            };
        }

        public static List<EstacionesDto> ToDTO(List<Pipa> pipas, List<Camioneta> camionetas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            List<EstacionesDto> list = new List<EstacionesDto>();
            list.AddRange(ToDTO(pipas, medidores));
            list.AddRange(ToDTO(camionetas, medidores));
            return list;
        }

        public static List<EstacionesDto> ToDTO(List<Camioneta> camionetas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return camionetas.Select(x => ToDTO(x, medidores)).ToList();
        }

        public static EstacionesDto ToDTO(Camioneta camioneta, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new EstacionesDto()
            {
                CantidadP5000 = camioneta.UnidadAlmacenGas.First().P5000Actual,
                IdTipoMedidor = camioneta.UnidadAlmacenGas.First().IdTipoMedidor,
                IdAlmacenGas = camioneta.UnidadAlmacenGas.First().IdCAlmacenGas,
                Medidor = TipoMedidorAdapter.ToDto(camioneta.UnidadAlmacenGas.First().Medidor),
                NombreAlmacen = camioneta.Nombre,
                PorcentajeMedidor = camioneta.UnidadAlmacenGas.First().PorcentajeActual
            };
        }

        public static List<EstacionesDto> ToDTO(List<Pipa> pipas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return pipas.Select(x => ToDTO(x, medidores)).ToList();
        }

        public static EstacionesDto ToDTO(Pipa pipa, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new EstacionesDto()
            {
                 CantidadP5000=pipa.UnidadAlmacenGas.First().P5000Actual,
                 IdTipoMedidor = pipa.UnidadAlmacenGas.First().IdTipoMedidor,
                 IdAlmacenGas = pipa.UnidadAlmacenGas.First().IdCAlmacenGas,
                 Medidor = TipoMedidorAdapter.ToDto(pipa.UnidadAlmacenGas.First().Medidor),
                 NombreAlmacen = pipa.Nombre,
                 PorcentajeMedidor = pipa.UnidadAlmacenGas.First().PorcentajeActual
            };
        }

        private static EstacionesDto ToDTO(UnidadAlmacenGas almacen,TipoMedidorUnidadAlmacenGas medidor)
        {
            return new EstacionesDto()
            {
                CantidadP5000 = almacen.P5000Actual,
                IdTipoMedidor = almacen.IdTipoMedidor,
                IdAlmacenGas = almacen.IdCAlmacenGas,
                Medidor = TipoMedidorAdapter.ToDto(medidor),
                NombreAlmacen = almacen.Numero,
                PorcentajeMedidor = almacen.PorcentajeActual
            };
        }

        public static List<EstacionesDto> ToDTO(List<UnidadAlmacenGas> pipas, List<UnidadAlmacenGas> camionetas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            List<EstacionesDto> estaciones = new List<EstacionesDto>();
            foreach (var pipa in pipas)
            {
                estaciones.Add(new EstacionesDto()
                {
                    CantidadP5000 = pipa.P5000Actual,
                    IdAlmacenGas = pipa.IdCAlmacenGas,
                    Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x=>x.IdTipoMedidor.Equals(pipa.IdTipoMedidor))),
                    IdTipoMedidor = pipa.IdTipoMedidor,
                    PorcentajeMedidor = pipa.PorcentajeActual,
                    NombreAlmacen = pipa.Numero 
                });
            }
            foreach (var camioneta in camionetas)
            {
                estaciones.Add(new EstacionesDto()
                {
                    CantidadP5000 = camioneta.P5000Actual,
                    Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x=>x.IdTipoMedidor.Equals(camioneta.IdTipoMedidor))),
                    IdTipoMedidor = camioneta.IdTipoMedidor,
                    IdAlmacenGas = camioneta.IdCAlmacenGas,
                    NombreAlmacen = camioneta.Numero,
                    PorcentajeMedidor = camioneta.PorcentajeActual
                });
            }
            return estaciones;
        }

        public static List<EstacionesDto> ToDTO(List<UnidadAlmacenGas> almacenes, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            List<EstacionesDto> estacion = new List<EstacionesDto>();
            foreach (var almacen in almacenes)
            {
                estacion.Add(new EstacionesDto()
                {
                    CantidadP5000 = almacen.P5000Actual,
                    IdTipoMedidor = almacen.IdTipoMedidor,
                    IdAlmacenGas = almacen.IdCAlmacenGas,
                    Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x => x.IdTipoMedidor.Equals(almacen.IdTipoMedidor))),
                    NombreAlmacen = almacen.Numero,
                    PorcentajeMedidor = almacen.PorcentajeActual
                });
            }
            
            return estacion;
        }

        public static DatosAutoconsumoDto ToDTOInventarioGeneral(List<UnidadAlmacenGas> pipas, List<UnidadAlmacenGas> camionetas, List<Pipa> lpipas,List<Camioneta> lcamionetas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosAutoconsumoDto()
            {
                EstacionEntrada = ToDTO(lpipas, lcamionetas, medidores)
            };
        }

        public static DatosAutoconsumoDto ToDTOFinal(List<UnidadAlmacenGas> estacionesInicioEnInicial, List<EstacionCarburacion> estacionesFinEnInicial,List<Pipa> pipas,List<Camioneta> camionetas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosAutoconsumoDto()
            {
               EstacionEntrada = ToDTO(estacionesInicioEnInicial, medidores),
               EstacionSalida = ToDTO(pipas,camionetas,medidores)
            };
        }

        public static DatosAutoconsumoDto ToDTOInventarioGeneral(List<UnidadAlmacenGas> estacionesInicioEnInicial, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosAutoconsumoDto()
            {
                EstacionEntrada = ToDTO(estacionesInicioEnInicial, medidores)
            };
        }

        public static DatosAutoconsumoDto ToDTO(List<EstacionCarburacion> lestaciones, UnidadAlmacenGas predeterminado, List<Pipa> lpipas, List<Camioneta> lcamionetas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosAutoconsumoDto()
            {
                EstacionEntrada = ToDTO(lestaciones, medidores),
                EstacionSalida = ToDTO(lpipas, lcamionetas, medidores),
                Predeterminada = ToDTO(predeterminado, medidores.Single(x => x.IdTipoMedidor.Equals(predeterminado.IdTipoMedidor)))
            };
        }

        public static List<EstacionesDto> ToDTO(List<EstacionCarburacion> lestaciones, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return lestaciones.Select(x => ToDTO(x, medidores)).ToList();
        }

        public static EstacionesDto ToDTO(EstacionCarburacion estacion, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new EstacionesDto()
            {
                CantidadP5000 = estacion.UnidadAlmacenGas.First().P5000Actual,
                Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x => x.IdTipoMedidor.Equals(estacion.UnidadAlmacenGas.First().IdTipoMedidor))),
                IdTipoMedidor = estacion.UnidadAlmacenGas.First().IdTipoMedidor,
                IdAlmacenGas = estacion.UnidadAlmacenGas.First().IdCAlmacenGas,
                NombreAlmacen = estacion.Nombre,
                PorcentajeMedidor = estacion.UnidadAlmacenGas.First().PorcentajeActual
            };
        }

        public static DatosAutoconsumoDto ToDTOInventarioGeneral(List<UnidadAlmacenGas> estacionesInicioEnInicial, List<UnidadAlmacenGas> pipas, List<UnidadAlmacenGas> camionetas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosAutoconsumoDto()
            {
                EstacionEntrada = ToDTO(pipas, camionetas, medidores)
            };
        }

        public static DatosAutoconsumoDto ToDTOInventarioGeneral(List<UnidadAlmacenGas> pipas, List<UnidadAlmacenGas> camionetas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosAutoconsumoDto()
            {
                EstacionEntrada = ToDTO(pipas, camionetas, medidores)
            };
        }
    }
}
