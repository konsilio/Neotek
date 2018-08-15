using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacen;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Mobile;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Mobile
    {

        public RespuestaOrdenesCompraDTO ConsultarOrdenesCompra(short IdEmpresa, bool EsGas, bool EsActivoVenta, bool EsTransporteGas)
        {
           return OrdenesCompraServicio.Consultar(IdEmpresa,EsGas,EsActivoVenta,EsTransporteGas);
        }

        public List<MenuDto> ObtenerMenu()
        {
            int idUsuario = TokenServicio.ObtenerIdUsuario();
            return MenuServicio.Crear(idUsuario);
        }

        public List<MedidorDto> ObtenerMedidores()
        {
            return TipoMedidorAdapter.ToDto(TipoMedidorGasServicio.Obtener());
        }

        public List<AlmacenDto> ObtenerAlmacenesGas()
        {
            return AlmacenAdapter.ToDto(AlmacenGasServicio.ObtenerTodos(TokenServicio.ObtenerIdEmpresa()));
        }

        public RespuestaDto RegistrarPapeleta(PapeletaDTO papeletaDto)
        {
            return EntradaGasServicio.RegistrarPapeleta(AlmacenAdapter.FromDto(papeletaDto));
        }

        public RespuestaDto InicializarDescarga()
        {


            return EntradaGasServicio.;
        }

        public RespuestaDto FinalizarDescarga()
        {
            return EntradaGasServicio.RegistrarPapeleta();
        }
    }
}
