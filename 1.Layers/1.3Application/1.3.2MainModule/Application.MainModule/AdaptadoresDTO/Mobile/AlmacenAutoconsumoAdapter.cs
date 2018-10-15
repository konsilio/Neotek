using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
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
                //Dia,
                //Mes,
                //Year,
                IdEmpresa =  IdEmpresa
            };
        }

        public static DatosAutoconsumoDto ToDTO(UnidadAlmacenGas almacen, List<UnidadAlmacenGas> pipas, List<UnidadAlmacenGas> camionetas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            return new DatosAutoconsumoDto()
            {
                EstacionEntrada = ToDTO(almacen,medidores),
                EstacionSalida = ToDTO(pipas,camionetas,medidores)
            };
        }

        private static List<EstacionesDto> ToDTO(List<UnidadAlmacenGas> pipas, List<UnidadAlmacenGas> camionetas, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            throw new NotImplementedException();
        }

        public static List<EstacionesDto> ToDTO(UnidadAlmacenGas almacen, List<TipoMedidorUnidadAlmacenGas> medidores)
        {
            List<EstacionesDto> estacion = new List<EstacionesDto>();
            estacion.Add(new EstacionesDto() {
                CantidadP5000 = almacen.P5000Actual,
                IdTipoMedidor = almacen.IdTipoMedidor,
                IdAlmacenGas = almacen.IdAlmacenGas.Value,
                Medidor = TipoMedidorAdapter.ToDto(medidores.Single(x=>x.IdTipoMedidor.Equals(almacen.IdTipoMedidor))),
                NombreAlmacen = almacen.Numero,
                PorcentajeMedidor = almacen.PorcentajeActual
            });
            return estacion;
        }
    }
}
