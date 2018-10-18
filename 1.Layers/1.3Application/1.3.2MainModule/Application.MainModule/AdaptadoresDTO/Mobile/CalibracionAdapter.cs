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
            return new EstacionesDto()
            {
                IdTipoMedidor = unidadAlmacen.IdTipoMedidor,
                CantidadP5000 = unidadAlmacen.P5000Actual,
                IdAlmacenGas = unidadAlmacen.IdCAlmacenGas,
                Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x=>x.IdTipoMedidor.Equals(unidadAlmacen.IdTipoMedidor))),
                NombreAlmacen = unidadAlmacen.Numero,
                PorcentajeMedidor = unidadAlmacen.PorcentajeActual
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
    }
}
