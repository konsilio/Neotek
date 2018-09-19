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
using Application.MainModule.Servicios.Seguridad;

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

        public static AlmacenGasTomaLectura BuscarUltimaLectura(short idCAlmacenGas, byte idTipoEvento)
        {
            return new AlmacenGasDataAccess().BuscarUltimaLectura(idCAlmacenGas, idTipoEvento);
        }
        public static List<AlmacenGasTomaLectura> ObtenerLecturas(short idCAlmacenGas)
        {            
            return new AlmacenGasDataAccess().BuscarLecturas(idCAlmacenGas); 
        }
        public static RespuestaDto InsertarLectura(AlmacenGasTomaLectura lia)
        {
            return new AlmacenGasDataAccess().Insertar(lia);
        }
        public static List<UnidadAlmacenGas> ObtenerAlmacenGeneral(short idEmpresa, bool incluyeAlterno = false)
        {
            return new AlmacenGasDataAccess().BuscarTodos(idEmpresa, true, incluyeAlterno);
        }
        public static List<UnidadAlmacenGas> ObtenerAlmacenGeneral(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarTodas(idEmpresa);
        }

        public static List<UnidadAlmacenGas> ObtenerEstaciones(short idEmpresa)
        {            
            return new AlmacenGasDataAccess().BuscarTodosEstacionCarburacion(idEmpresa);
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
                ? BuscarUltimaLectura(uniAlm.IdCAlmacenGas, TipoEventoEnum.Final)
                : BuscarUltimaLectura(uniAlm.IdCAlmacenGas, TipoEventoEnum.Inicial);
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
        public static UnidadAlmacenGasCilindro ObtenerCilindro(int idCilindro)
        {
            return new AlmacenGasDataAccess().BuscarCilindro(idCilindro);
        }
        public static List<UnidadAlmacenGasCilindro> ObtenerCilindros()
        {
            return new AlmacenGasDataAccess().BuscarTodosCilindros(TokenServicio.ObtenerIdEmpresa());
        }
        /// <summary>
        /// Adaptamos una entidad AlmacenGasTomaLEcturaCilindro en una UnidadAlmacenGasCilindro.
        /// Solo en la cantidad de cilindros, y este método es especial ya que se hizo para la toma
        /// de lecturas de Camionetas
        /// </summary>
        /// <param name="tmCil"></param>
        /// <returns></returns>
        public static UnidadAlmacenGasCilindro AdaptarCilindro(AlmacenGasTomaLecturaCilindro tmCil)
        {
            var cil = ObtenerCilindro(tmCil.IdCilindro);
            if (cil != null)
                cil.Cantidad = tmCil.Cantidad;

            return cil;
        }
        public static UnidadAlmacenGasCilindro AdaptarCilindro(UnidadAlmacenGasCilindro cil, decimal cantidad)
        {
            if (cil != null)
                cil.Cantidad = cantidad;

            return cil;
        }
        public static List<UnidadAlmacenGasCilindro> AdaptarCilindro(List<AlmacenGasTomaLecturaCilindro> tmCil)
        {            
            return tmCil.Select(x=> AdaptarCilindro(x)).ToList();
        }
        public static List<UnidadAlmacenGasCilindro> AdaptarCilindro(decimal cantidad)
        {
            var cilindros = new List<UnidadAlmacenGasCilindro>();

            foreach (var cil in ObtenerCilindros())
                cilindros.Add(AdaptarCilindro(cil, cantidad));

            return cilindros;
        }
    }
}

        internal static RespuestaDto InsertarRecargaGas(AlmacenGasRecarga adapter)
        {
            return new AlmacenGasDataAccess().Insertar(adapter);
        }
        public static AlmacenGasRecarga ObtenerRecargaPorClaveOperacion(string claveOperacion)
        {
            return new AlmacenGasDataAccess().BuscarRecargaClaveOperacion(claveOperacion);
        }