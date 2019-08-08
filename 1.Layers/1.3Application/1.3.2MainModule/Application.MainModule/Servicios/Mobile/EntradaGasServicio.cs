using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Compras;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Mobile
{
    public static class EntradaGasServicio
    {
        public static RespuestaDto RegistrarPapeleta(AlmacenGasDescarga alm)
        {
            alm.FechaEntraGas = alm.FechaRegistro;
            alm.DatosProcesados = false;
            return AlmacenGasServicio.InsertarDescargaGas(alm);
        }

        public static RespuestaDto Descargar(DescargaDto desDto, bool finDescarga = false)
        {
            var des = AlmacenGasServicio.ObtenerDescargaPorOCompraExpedidor(desDto.IdOrdenCompra);
            short numOrden = 0;
            if (!des.Fotos.Count.Equals(0))
                numOrden = (short)(des.Fotos.Max(x => x.Orden) + 1);
            desDto.FechaDescarga = DateTime.Now;            

            var descarga = AlmacenAdapter.FromEntity(des);
            descarga = AlmacenAdapter.FromDto(desDto, descarga, finDescarga);
            var fotos = AlmacenAdapter.FromDto(desDto.Imagenes, descarga.IdAlmacenEntradaGasDescarga, numOrden);
            List<OrdenCompra> ocs = new List<OrdenCompra>();
            var complemento = OrdenCompraServicio.BuscarComplementoGas(OrdenCompraServicio.Buscar(desDto.IdOrdenCompra));
            var ocExp = OrdenComprasAdapter.FromEntity(OrdenCompraServicio.Buscar(complemento.IdOrdenCompraExpedidor));
            ocExp.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.EnComplementoCompra;
            var ocPort = OrdenComprasAdapter.FromEntity(OrdenCompraServicio.Buscar(complemento.IdOrdenCompraPorteador));
            ocPort.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.EnComplementoCompra;

            ocs.Add(ocExp);
            ocs.Add(ocPort);

            return AlmacenGasServicio.ActualizarDescargaGas(descarga, fotos, ocs);
        }

        public static RespuestaDto EvaluarClaveOperacion(DescargaDto dto)
        {
            return GasServicio.EvaluarClaveOperacion(dto);
        }

        public static RespuestaDto EvaluarClaveOperacion(PapeletaDTO dto)
        {
            return GasServicio.EvaluarClaveOperacion(dto);
        }
        
        public static RespuestaDto EvaluarExistenciaRegistro(PapeletaDTO dto)
        {
            return GasServicio.EvaluarClaveOperacion(dto);
        }

        public static RespuestaDto EvaluarExistenciaRegistro(DescargaDto dto)
        {
            return GasServicio.EvaluarClaveOperacion(dto);
        }       

        private static RespuestaDto EvaluarExistenciaRegistro(int idOCompra)
        {
            var alm = AlmacenGasServicio.ObtenerDescargaPorOCompraExpedidor(idOCompra);
            return new RespuestaDto()
            {
                Exito = alm != null ? true : false,
                Mensaje = alm != null ? Exito.OK : string.Format(Error.M0002, "la descarga"),
            };
        }
    }
}
