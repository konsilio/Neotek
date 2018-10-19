﻿using System;
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

        public static DatosAutoconsumoDto ToDTO(List<UnidadAlmacenGas>almacenes,UnidadAlmacenGas predeterminado, List<UnidadAlmacenGas> pipas, List<UnidadAlmacenGas> camionetas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosAutoconsumoDto()
            {
                EstacionEntrada = ToDTO(almacenes, medidores),
                EstacionSalida = ToDTO(pipas,camionetas,medidores),
                Predeterminada = ToDTO(predeterminado,medidores.Single(x=>x.IdTipoMedidor.Equals(predeterminado.IdTipoMedidor)))
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

        public static DatosAutoconsumoDto ToDTOInventarioGeneral(List<UnidadAlmacenGas> pipas, List<UnidadAlmacenGas> camionetas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosAutoconsumoDto()
            {
                EstacionEntrada = ToDTO(pipas, camionetas, medidores)
            };
        }

        public static DatosAutoconsumoDto ToDTOFinal(List<UnidadAlmacenGas> estacionesInicioEnInicial, List<UnidadAlmacenGas> estacionesFinEnInicial, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosAutoconsumoDto()
            {
               EstacionEntrada = ToDTO(estacionesInicioEnInicial, medidores),
               EstacionSalida = ToDTO(estacionesFinEnInicial,medidores)
            };
        }

        public static DatosAutoconsumoDto ToDTOInventarioGeneral(List<UnidadAlmacenGas> estacionesInicioEnInicial, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosAutoconsumoDto()
            {
                EstacionEntrada = ToDTO(estacionesInicioEnInicial, medidores)
            };
        }
    }
}