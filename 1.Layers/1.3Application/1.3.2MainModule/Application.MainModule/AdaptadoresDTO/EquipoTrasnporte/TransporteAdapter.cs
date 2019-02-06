using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Transporte;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO
{
    public static class TransporteAdapter
    {
        public static List<TransporteDTO> ToDTO(List<PuntoVenta> pvs, List<AsignacionUtilitarios> utili)
        {
            List<TransporteDTO> list = new List<TransporteDTO>();
            list.AddRange(ToDTO(pvs));
            list.AddRange(ToDTO(utili));
            return list;
        }
        public static TransporteDTO ToDTO(PuntoVenta pv)
        {
            TransporteDTO t = new TransporteDTO();
            t.IdChofer = pv.IdOperadorChofer;
            t.IdEmpresa = pv.IdEmpresa;
            t.Chofer = pv.OperadorChofer.Usuario.Nombre;
            t.IdVehiculo = pv.IdCAlmacenGas;
            t.Vehiculo = AlmacenGasServicio.ObtenerNombreUnidadAlmacenGas(pv.UnidadesAlmacen);
            if (pv.UnidadesAlmacen.IdPipa != null)
                t.TipoVehiculo = TipoUnidadEqTransporteEnum.Pipa;
            if (pv.UnidadesAlmacen.IdCamioneta != null)
                t.TipoVehiculo = TipoUnidadEqTransporteEnum.Camioneta;
            return t;
        }
        public static List<TransporteDTO> ToDTO(List<PuntoVenta> pvs)
        {
            return pvs.Select(x => ToDTO(x)).ToList();
        }
        public static TransporteDTO ToDTO(AsignacionUtilitarios ut)
        {
            TransporteDTO t = new TransporteDTO();
            t.IdChofer = ut.IdChoferOperador;
            t.IdEmpresa = ut.IdEmpresa;
            t.Chofer = ut.COperadorChofer.Usuario.Nombre;
            t.IdVehiculo = Convert.ToInt16(ut.IdUtilitario);
            t.Vehiculo = VehiculoUtilitarioServicio.ObtenerNombre(ut.IdUtilitario);
            t.TipoVehiculo = TipoUnidadEqTransporteEnum.Utilitario;
            return t;
        }

        public static List<TransporteDTO> ToDTO(List<AsignacionUtilitarios> utili)
        {
            return utili.Select(x => ToDTO(x)).ToList();
        }
    }
}
