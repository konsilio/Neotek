using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.ObjetosValor.Enum;

namespace Application.MainModule.Servicios.Almacen
{
    public static class AlmacenGasServicio
    {
        public static RespuestaDto InsertarDescargaGas(AlmacenGasDescarga alm)
        {
            return new AlmacenGasDescargaDataAccess().Insertar(alm);
        }
        public static RespuestaDto ActualizarDescargaGas(AlmacenGasDescarga alm, List<AlmacenGasDescargaFoto> fotos)
        {
            return new AlmacenGasDescargaDataAccess().Actualizar(alm, fotos);
        }
        public static List<AlmacenGas> ObtenerTodos(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarTodos(idEmpresa);
        }

        public static List<AlmacenGasTomaLectura> ObtenerLecturas(short idCAlmacenGas)
        {
            return new AlmacenGasDataAccess().BuscarLecturas(idCAlmacenGas);
        }

        public static RespuestaDto InsertarLectura(AlmacenGasTomaLectura lia)
        {
            return new AlmacenGasDataAccess().Insertar(lia);
        }

        public static List<UnidadAlmacenGas> ObtenerAlmacenGeneral(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarTodos(idEmpresa, true);
        }

        public static List<UnidadAlmacenGas> ObtenerEstaciones(short idEmpresa)
        {
            var alms = new AlmacenGasDataAccess().BuscarTodosEstacionCarburacion(idEmpresa);
            alms = alms.Where(x=>x.IdEstacionCarburacion != null).ToList();
            return alms;
        }

        public static List<UnidadAlmacenGas> ObtenerPipas(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarTodosPipas(idEmpresa);
        }

        public static List<UnidadAlmacenGas> ObtenerCamionetas(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarTodosCamionetas(idEmpresa);
        }

        public static AlmacenGasTomaLectura ObtenerLecturaPorClaveOperacion(string claveProceso)
        {
            return new AlmacenGasDataAccess().BuscarClaveOperacion(claveProceso);
        }

        public static AlmacenGasTomaLectura ObtenerUltimaLectura(UnidadAlmacenGas uniAlm, bool final = false)
        {
            if (uniAlm != null)
                if (uniAlm.TomasLectura != null)
                    if (uniAlm.TomasLectura.Count > 0)
                            return !final
                                ? uniAlm.TomasLectura.Last(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final))
                                : uniAlm.TomasLectura.Last(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial));
            return !final
                ? ObtenerLecturas(uniAlm.IdCAlmacenGas).Last(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final))
                : ObtenerLecturas(uniAlm.IdCAlmacenGas).Last(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial));                
        }

        public static AlmacenGas Obtener(short idAlmacenGas)
        {
            return new AlmacenGasDataAccess().Buscar(idAlmacenGas);
        }
        public static AlmacenGasDescarga ObtenerDescargaPorOCompraExpedidor(int idOCompra)
        {
            return new AlmacenGasDescargaDataAccess().BuscarOCompraExpedidor(idOCompra);
        }
        public static AlmacenGasDescarga ObtenerDescargaPorClaveOperacion(string claveOperacion)
        {
            return new AlmacenGasDescargaDataAccess().BuscarClaveOperacion(claveOperacion);
        }
        public static string ObtenerNombreUnidadAlmacenGas(UnidadAlmacenGas uAG)
        {
            if (uAG.EsGeneral) return uAG.Numero;

            var nombre = EquipoTransporteServicio.ObtenerNombre(uAG);
            if (!string.IsNullOrEmpty(nombre))
                return nombre;

            return EstacionCarburacionServicio.ObtenerNombre(uAG);
        }
        public static decimal ObtenerCantidadActualAlmacenGeneral(short IdEmpresa, bool EnLitros = true)
        {
            var almacenGas = new AlmacenGasDataAccess().ProductoAlmacenGas(IdEmpresa);
            if (EnLitros)
                return almacenGas.UnidadesAlmacenGas.Where(z => z.EsGeneral).Sum(x => x.CantidadActualLt);
            else
                return almacenGas.UnidadesAlmacenGas.Where(z => z.EsGeneral).Sum(x => x.CantidadActualKg);

        }
    }
}
