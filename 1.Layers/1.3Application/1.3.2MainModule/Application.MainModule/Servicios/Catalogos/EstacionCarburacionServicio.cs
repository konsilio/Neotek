﻿using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class EstacionCarburacionServicio
    {
        public static string ObtenerNombre(UnidadAlmacenGas uAG)
        {   
            if (uAG.IdEstacionCarburacion != null)
                return new EstacionCarburacionDataAccess().BuscarEstacionCarburacion(uAG).Nombre;
            return null;
        }
        public static EstacionCarburacion Obtener(int idEstacion)
        {
            return new EstacionCarburacionDataAccess().Buscar(idEstacion);
        }
        public static List<EstacionCarburacion> ObtenerTodas()
        {
            return new EstacionCarburacionDataAccess().BuscarTodos();
        }
        public static List<EstacionCarburacion> ObtenerTodas(short IdEmpresa)
        {
            return new EstacionCarburacionDataAccess().BuscarTodos(IdEmpresa);
        }
    }
}
