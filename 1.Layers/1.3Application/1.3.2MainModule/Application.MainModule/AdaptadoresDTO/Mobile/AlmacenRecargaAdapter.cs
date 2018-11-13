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

        public static DatosRecargaDto ToDTO(List<UnidadAlmacenGas> almacenesAlternos, List<CamionetaDto> camionetas, List<TipoMedidorUnidadAlmacenGas> tipoMedidores)
        {
            return new DatosRecargaDto()
            {
                AlmacenesAlternos = almacenesAlternos.Select(x => ToDTO(x, tipoMedidores)).ToList(),
                Camionetas = camionetas,
                Medidores = TipoMedidorAdapter.ToDto(tipoMedidores)
            };
        }

        public static AlmacenAlternoDto ToDTO(UnidadAlmacenGas almacenAlterno, List<TipoMedidorUnidadAlmacenGas> tipoMedidores)
        {
            return new AlmacenAlternoDto()
            {
                IdCAlmacen = almacenAlterno.IdCAlmacenGas,
                NombreAlmacen = almacenAlterno.Numero,
                IdAlmacenGas = almacenAlterno.IdAlmacenGas.Value,
                P5000Actual = (almacenAlterno.P5000Actual.HasValue)?almacenAlterno.P5000Actual.Value:0,
                Medidor = TipoMedidorAdapter.ToDto(tipoMedidores.Single(x => x.IdTipoMedidor.Equals(almacenAlterno.IdTipoMedidor))),
                PorcentajeActual = almacenAlterno.PorcentajeActual,
                CantidadActualKg = almacenAlterno.CantidadActualKg,
                CantidadActualLt = almacenAlterno.CantidadActualLt,
                CapacidadTanqueKg = almacenAlterno.CapacidadTanqueKg.Value,
                CapacidadTanqueLt = almacenAlterno.CapacidadTanqueLt.Value              
            };
        }

        public static List<CamionetaDto> ToDTOCamionetas(List<UnidadAlmacenGas> camionetas, List<TipoMedidorUnidadAlmacenGas> tipoMedidores)
        {
            List<CamionetaDto> camionetasDTO = new List<CamionetaDto>();
            foreach (var camioneta in camionetas)
            {
                var camionetaCilindros = Servicios.Almacenes.AlmacenGasServicio.ObtenerCilindros(camioneta);
                camionetasDTO.Add(new CamionetaDto()
                {
                    CantidadActualKg = camioneta.CantidadActualKg,
                    CantidadActualLt = camioneta.CantidadActualLt,
                    IdCAlmacen = camioneta.IdCAlmacenGas,
                    Numero = camioneta.Numero,
                    PorcentajeActual = camioneta.PorcentajeActual,
                    Medidor = TipoMedidorAdapter.ToDto(tipoMedidores.Single(x => x.IdTipoMedidor.Equals(camioneta.IdTipoMedidor))),
                    Cilindros = ToDTO(camionetaCilindros)
                });
            }
            return camionetasDTO;
        }

        public static List<CilindroDto> ToDTO(List<CamionetaCilindro> camionetaCilindros)
        {
            List<CilindroDto> cilindroDto = new List<CilindroDto>();
            foreach (var camionetaCilindro in camionetaCilindros)
            {
                cilindroDto.Add(new CilindroDto()
                {
                    Cantidad = camionetaCilindro.Cantidad,
                    CapacidadKg = camionetaCilindro.UnidadAlmacenGasCilindro.CapacidadKg.ToString(),
                    IdCilindro = camionetaCilindro.IdCilindro
                });
            }
            return cilindroDto;
            
        }

        public static DatosRecargaDto ToDTO(List<UnidadAlmacenGas> pipas, List<UnidadAlmacenGas> estaciones, List<TipoMedidorUnidadAlmacenGas> tipoMedidores)
        {
            List<PipaDto> pipasDto = new List<PipaDto>();
            List<EstacionesDto> estacionesDto = new List<EstacionesDto>();
            pipasDto = pipas.Select(x=>ToDTOPipa(x,tipoMedidores)).ToList();
            estacionesDto = estaciones.Select(x => ToDTOEstaciones(x, tipoMedidores)).ToList();
            return new DatosRecargaDto()
            {
                Pipas = pipasDto,
                Estaciones = estacionesDto,
                Medidores = TipoMedidorAdapter.ToDto(tipoMedidores)
            };
        }

        public static EstacionesDto ToDTOEstaciones(UnidadAlmacenGas estacion, List<TipoMedidorUnidadAlmacenGas> tipoMedidores)
        {
            return new EstacionesDto()
            {
                IdAlmacenGas = estacion.IdAlmacenGas.Value,
                CantidadP5000 = estacion.P5000Actual,
                IdTipoMedidor = estacion.IdTipoMedidor,
                NombreAlmacen = estacion.Numero,
                PorcentajeMedidor = estacion.PorcentajeActual,
                Medidor = TipoMedidorAdapter.ToDto(tipoMedidores.Single(x=>x.IdTipoMedidor.Equals(estacion.IdTipoMedidor)))
            };
        }

        public static PipaDto ToDTOPipa(UnidadAlmacenGas pipa, List<TipoMedidorUnidadAlmacenGas> tipoMedidores)
        {
            return new PipaDto()
            {
                IdTipoMedidor = pipa.IdTipoMedidor,
                CantidadP5000 = pipa.P5000Actual,
                IdAlmacenGas = pipa.IdAlmacenGas.Value,
                Medidor = TipoMedidorAdapter.ToDto(tipoMedidores.Single(x=>x.IdTipoMedidor.Equals(pipa.IdTipoMedidor))),
                NombreAlmacen = pipa.Numero,
                PorcentajeMedidor = pipa.PorcentajeActual
            };
        }
    }
}
