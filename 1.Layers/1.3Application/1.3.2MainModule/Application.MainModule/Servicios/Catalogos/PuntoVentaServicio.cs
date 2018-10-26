﻿using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class PuntoVentaServicio
    {
        public static List<PuntoVentaDTO> Obtener()
        {            
            List<PuntoVentaDTO> lPventas = AdaptadoresDTO.Catalogo.PuntoVentaAdapter.ToDTO(new PuntoVentaDataAccess().BuscarTodos());
            return lPventas;
        }

        public static PuntoVenta Obtener(int idPuntoVenta)
        {
            return new PuntoVentaDataAccess().Buscar(idPuntoVenta);
        }

        public static OperadorChoferDTO ObtenerOperador(int idUsuario)
        {
            OperadorChoferDTO lPventas = AdaptadoresDTO.Catalogo.OperadorChoferAdapter.ToOperador(new PuntoVentaDataAccess().BuscarPorUsuario(idUsuario));
            return lPventas;         
        }

        public static List<OperadorChoferDTO> ObtenerUsuariosOperador(short idEmpresa)
        {
            List<OperadorChoferDTO> lPventas = AdaptadoresDTO.Catalogo.OperadorChoferAdapter.ToUsuariosOpe(new OperadorChoferDataAccess().BuscarTodos(idEmpresa));
            return lPventas;
        }
        public static List<PuntoVenta> ObtenerIdEmp(short IdEmpresa)
        {
            return new PuntoVentaDataAccess().BuscarTodos(IdEmpresa);
        }

        public static PuntoVenta Obtener(OperadorChofer opCh)
        {
            if (opCh != null)
                if (opCh.PuntosVenta != null)
                    if (opCh.PuntosVenta.Count > 0)
                        return opCh.PuntosVenta.FirstOrDefault();

            return BuscarPorOperadorChofer(opCh.IdOperadorChofer).FirstOrDefault();
        }

        public static PuntoVenta Obtener(UnidadAlmacenGas unidadAlmacen)
        {
            return new PuntoVentaDataAccess().BuscarPorUnidadAlmacenGas(unidadAlmacen.IdCAlmacenGas);
        }

        public static List<PuntoVenta> BuscarPorOperadorChofer(int idOperadorChofer)
        {
            return new PuntoVentaDataAccess().BuscarPorOperadorChofer(idOperadorChofer);
        }

        public static PuntoVenta ObtenerPorUsuarioAplicacion()
        {
            var operadorChofer = OperadorChoferServicio.ObtenerPorUsuarioAplicacion();
            return Obtener(operadorChofer);
        }
       
        public static RespuestaDto Eliminar(PuntoVenta cteLoc)
        {
            return new PuntoVentaDataAccess().Eliminar(cteLoc);
        }
        public static RespuestaDto Modificar(PuntoVenta cte)
        {
            return new PuntoVentaDataAccess().Actualizar(cte);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El punto de venta");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
