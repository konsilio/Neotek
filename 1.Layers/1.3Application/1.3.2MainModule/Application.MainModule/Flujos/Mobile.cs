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
using Sagas.MainModule.Entidades;

namespace Application.MainModule.Flujos
{
    public class Mobile
    {

        public RespuestaOrdenesCompraDTO ConsultarOrdenesCompra(short IdEmpresa, bool EsGas, bool EsActivoVenta, bool EsTransporteGas)
        {
           return MobileOrdenesCompraServicio.Consultar(IdEmpresa,EsGas,EsActivoVenta,EsTransporteGas);
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
            return AlmacenAdapter.ToDto(AlmacenGasServicio.ObtenerAlmacenGeneral(TokenServicio.ObtenerIdEmpresa(), true));
        }

        public RespuestaDto RegistrarPapeleta(PapeletaDTO papeletaDto)
        {
            var resp = EntradaGasServicio.EvaluarClaveOperacion(papeletaDto);
            if (resp.Exito) return resp;

            resp = EntradaGasServicio.EvaluarExistenciaRegistro(papeletaDto);
            if (resp.Exito) return resp;

            return EntradaGasServicio.RegistrarPapeleta(AlmacenAdapter.FromDto(papeletaDto));
        }

        public RespuestaDto InicializarDescarga(DescargaDto desDto)
        {
            var resp = EntradaGasServicio.EvaluarClaveOperacion(desDto);
            if (resp.Exito) return resp;

            return EntradaGasServicio.Descargar(desDto);
        }

        public RespuestaDto FinalizarDescarga(DescargaDto desDto)
        {
            var resp = EntradaGasServicio.EvaluarClaveOperacion(desDto);
            if (resp.Exito) return resp;

            return EntradaGasServicio.Descargar(desDto, true);
        }

        public RespuestaDto InicializarTomaDeLectura(LecturaDTO liadto)
        {
            var resp = LecturaGasServicio.EvaluarClaveOperacion(liadto);
            if (resp.Exito) return resp;

            return LecturaGasServicio.Lectura(liadto);
        }

        public RespuestaDto FinalizarTomaDeLectura(LecturaDTO lfadto)
        {
            var resp = LecturaGasServicio.EvaluarClaveOperacion(lfadto);
            if (resp.Exito) return resp;

            return LecturaGasServicio.Lectura(lfadto, true);
        }

        public RespuestaDto IniciarTomaDeLecturaCamioneta(LecturaCamionetaDTO lcdto)
        {
            var resp = LecturaGasServicio.EvaluarClaveOperacion(lcdto);
            if (resp.Exito) return resp;

            return LecturaGasServicio.Lectura(lcdto);
        }

        public RespuestaDto FinalizarTomaDeLecturaCamioneta(LecturaCamionetaDTO lcdto)
        {
            var resp = LecturaGasServicio.EvaluarClaveOperacion(lcdto);
            if (resp.Exito) return resp;

            return LecturaGasServicio.Lectura(lcdto,true);
        }

        public DatosTomaLecturaDto ConsultaDatosTomaLectura(bool esEstacion, bool esPipa, bool esCamioneta, bool esFinalizar)
        {
            if(esEstacion)
                return LecturaGasServicio.ConsultaDatosTomaLecturaEstacionCarburacion(esFinalizar);

            if (esPipa)
                return LecturaGasServicio.ConsultaDatosTomaLecturaPipa(esFinalizar);

            if (esCamioneta)
                return LecturaGasServicio.ConsultaDatosTomaLecturaCamioneta(esFinalizar);

            return LecturaGasServicio.ConsultaDatosTomaLecturaAlmacenGeneral(esFinalizar);
        }

        public RespuestaDto IniciarRecargaCamioneta(RecargaDTO rdto)
        {
            var resp = RecargaGasServicio.EvaluarClaveOperacion(rdto);
            if (resp.Exito) return resp;

            return RecargaGasServicio.Recarga(rdto);
        }

        public RespuestaDto IniciarRecarga(RecargaDTO ridto)
        {
            var resp = RecargaGasServicio.EvaluarClaveOperacion(ridto);
            if (resp.Exito) return resp;

            return RecargaGasServicio.Recarga(ridto,true);
        }

        public RespuestaDto FinalizarRecarga(RecargaDTO rfdto)
        {
            var resp = RecargaGasServicio.EvaluarClaveOperacion(rfdto);
            if (resp.Exito) return resp;

            return RecargaGasServicio.Recarga(rfdto, true);
        }

        public DatosTomaLecturaDto CatalogoUnidades()
        {
            return LecturaGasServicio.ConsultaDatosReporteDelDia();
        }

        public RespuestaDto CatalogoEstaciones(bool esAnticipo, bool esCorteCaja)
        {
            return null;
        }

        public DatosTipoPersonaDto CatalogoTipoPersona()
        {
            return ClientesServicio.ConsultarTipoPersonas();
        }

        public RespuestaDto registrarCliente(ClienteDTO cliente)
        {
            var resp = ClientesServicio.EvaluarCliente(cliente);
            if (resp.IdCliente!=0)
                return ClientesServicio.Modificar(cliente,TokenServicio.ObtenerIdEmpresa());
            else
                return ClientesServicio.Registar(cliente,TokenServicio.ObtenerIdEmpresa());
        }

        public List<ClienteDTO> BuscadorClientes(string criterio)
        {
            return ClientesServicio.BuscadorClientes(criterio);
        }

        public ReporteDiaDTO ReporteDia(DateTime fecha, short idCAlmacenGas)
        {
            var ReporteAlmacen = AlmacenGasServicio.ReporteDia(fecha, idCAlmacenGas);
            return ReporteAlmacen;
        }

        public RespuestaDto Venta(VentaDTO venta,bool esCamioneta, bool esEstacion, bool esPipa)
        {
            var resp = VentaServicio.BuscarFolioVenta(venta.FolioVenta,TokenServicio.ObtenerIdUsuario());
            return null;
        }

        public DatosRecargaDto CatalogoRecargas(bool esEstacion, bool esPipa, bool esCamioneta)
        {
            var tipoMedidores = TipoMedidorGasServicio.Obtener();
            if (esCamioneta)
            {
                var almacenesAlternos = AlmacenGasServicio.ObtenerAlmacenGeneral(TokenServicio.ObtenerIdEmpresa(), true);
                var camionetas = AlmacenGasServicio.ObtenerCamionetas(TokenServicio.ObtenerIdEmpresa());
                var camionetasDTO = AlmacenRecargaAdapter.ToDTOCamionetas(camionetas, tipoMedidores);
                return AlmacenRecargaAdapter.ToDTO(almacenesAlternos, camionetasDTO, tipoMedidores);
            }
            else if (esEstacion)
            {
                var pipas = AlmacenGasServicio.ObtenerPipas(TokenServicio.ObtenerIdEmpresa());
                var estaciones = AlmacenGasServicio.ObtenerEstaciones(TokenServicio.ObtenerIdEmpresa());

                return AlmacenRecargaAdapter.ToDTO(pipas, estaciones, tipoMedidores);
            }
            else if (esPipa)
            {
                var pipas = AlmacenGasServicio.ObtenerPipas(TokenServicio.ObtenerIdEmpresa());
                var estaciones = AlmacenGasServicio.ObtenerEstaciones(TokenServicio.ObtenerIdEmpresa());

                return AlmacenRecargaAdapter.ToDTO(pipas, estaciones, tipoMedidores);
            }
            return null;

        }

        public RespuestaDto Autoconsumo(bool esPipa, bool esInventario, bool esEstacion)
        {
            return null;
        }
    }
}
