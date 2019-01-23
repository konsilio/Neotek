using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Respuesta;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class CalibracionAdapter
    {
        public static DatosCalibracionDto ToDTO(List<UnidadAlmacenGas> estaciones, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosCalibracionDto()
            {
               estaciones = estaciones.Select(x=> ToDTO(x,medidores)).ToList(),
               medidores = TipoMedidorAdapter.ToDto(medidores)
            };
        }

        public static EstacionesDto ToDTO(UnidadAlmacenGas unidadAlmacen, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            //string NomPipa = string.Empty;
            //if (unidadAlmacen.Pipa == null)
            //    NomPipa = "";
            //else
            //    NomPipa = unidadAlmacen.Pipa.Nombre;
            return new EstacionesDto()
            {
                IdTipoMedidor = unidadAlmacen.IdTipoMedidor,
                CantidadP5000 = unidadAlmacen.P5000Actual,
                IdAlmacenGas = unidadAlmacen.IdCAlmacenGas,
                Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x => x.IdTipoMedidor.Equals(unidadAlmacen.IdTipoMedidor))),
                NombreAlmacen = unidadAlmacen.Numero,
                PorcentajeMedidor = unidadAlmacen.PorcentajeActual,
                NombrePipa = unidadAlmacen.Pipa == null ? "" :  unidadAlmacen.Pipa.Nombre 
            };
        }

        public static AlmacenGasCalibracion FromDTO(CalibracionDto dto,int IdOrden)
        {
            int num = 0;
          
            return new AlmacenGasCalibracion()
            {
                IdCAlmacenGas = dto.IdCAlmacenGas,
                IdTipoMedidor = dto.IdTipoMedidor,
                P5000 = dto.P5000,
                IdOrden = IdOrden,
                Porcentaje = dto.PorcentajeMedidor1,
                PorcentajeCalibracion = dto.PorcentajeMedidor2,
                Fotografias = dto.Imagenes.Select(x => ToDTO(x, IdOrden, num++,dto.IdCAlmacenGas)).ToList(),
            };
        }

        public static UnidadAlmacenGas FromDTOAlmacenCalibracion(UnidadAlmacenGas unidadAlmacen, AlmacenGasCalibracion adapter)
        {
            return new UnidadAlmacenGas()
            {
                Activo = unidadAlmacen.Activo,
                CantidadActualKg = unidadAlmacen.CantidadActualKg,
                CantidadActualLt = unidadAlmacen.CantidadActualLt,
                PorcentajeCalibracionPlaneada = adapter.PorcentajeCalibracion??0,
                EsAlterno = unidadAlmacen.EsAlterno,
                EsGeneral = unidadAlmacen.EsGeneral,
                FechaRegistro = unidadAlmacen.FechaRegistro,
                IdAlmacenGas = unidadAlmacen.IdAlmacenGas,
                IdCAlmacenGas = unidadAlmacen.IdCAlmacenGas,
                IdCamioneta= unidadAlmacen.IdCamioneta,
                IdPipa = unidadAlmacen.IdPipa,
                IdEstacionCarburacion = unidadAlmacen.IdEstacionCarburacion,
                IdEmpresa = unidadAlmacen.IdEmpresa,
                IdTipoAlmacen = unidadAlmacen.IdTipoAlmacen,
                IdTipoMedidor = unidadAlmacen.IdTipoMedidor,
                //Medidor = unidadAlmacen.Medidor,
                Numero = unidadAlmacen.Numero,
                //duda
                P5000Actual = unidadAlmacen.P5000Actual,
                PorcentajeActual = unidadAlmacen.PorcentajeActual,
                CapacidadTanqueLt = unidadAlmacen.CapacidadTanqueLt,
                CapacidadTanqueKg = unidadAlmacen.CapacidadTanqueKg,
                
            };
        }

        public static AlmacenGasCalibracionFoto ToDTO(string fotografia,int IdOrden,int IdOrdenFoto,short IdCAlmacenGas)
        {
            return new AlmacenGasCalibracionFoto()
            {
                CadenaBase64=fotografia,
                IdOrden = IdOrden,
                IdOrdenFoto =  (short) IdOrdenFoto,
                IdCAlmacenGas = IdCAlmacenGas
            };
        }

        public static DatosCalibracionDto ToDTOEstaciones(List<UnidadAlmacenGas> estaciones, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosCalibracionDto()
            {
                estaciones = estaciones.Select(x => ToDTOEstacion(x, medidores)).ToList(),
                medidores = TipoMedidorAdapter.ToDto(medidores)
            }; throw new NotImplementedException();
        }

        public static EstacionesDto ToDTOEstacion(UnidadAlmacenGas unidadAlmacen, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new EstacionesDto()
            {
                IdTipoMedidor = unidadAlmacen.IdTipoMedidor,
                CantidadP5000 = unidadAlmacen.P5000Actual,
                IdAlmacenGas = unidadAlmacen.IdCAlmacenGas,
                Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x => x.IdTipoMedidor.Equals(unidadAlmacen.IdTipoMedidor))),
                NombreAlmacen = unidadAlmacen.EstacionCarburacion.Nombre,
                PorcentajeMedidor = unidadAlmacen.PorcentajeActual,
                NombrePipa = unidadAlmacen.EstacionCarburacion == null ? "" : unidadAlmacen.EstacionCarburacion.Nombre
            };
        }
    }
}
