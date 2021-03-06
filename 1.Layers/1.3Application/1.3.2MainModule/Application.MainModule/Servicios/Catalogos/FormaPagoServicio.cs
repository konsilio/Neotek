﻿using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public class FormaPagoServicio
    {
        public static List<FormaPago> Obtener()
        {
            //var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            //if (empresa.EsAdministracionCentral)
                return new FormaPagoDataAccess().BuscarTodos();
            //else
            //    return new FormaPagoDataAccess().ListaProductos(empresa.IdEmpresa);
        }

        public static FormaPago Obtener(byte id)
        {           
            return new FormaPagoDataAccess().Buscar(id);           
        }
    }
}
