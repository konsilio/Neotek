﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class ClienteAdapter
    {
        public static Cliente FromDTO(ClienteDTO cliente)
        {
            return new Cliente()
            {
                Nombre = cliente.Nombre,
                Apellido1 = cliente.Apellido1,
                Apellido2 = cliente.Apellido2,
                Celular = cliente.Celular,
                Celular1 = cliente.Celular,
                Celular2 = cliente.Celular,
                Celular3 =cliente.Celular,
                Telefono = cliente.TelefonoFijo,
                Telefono1 =cliente.TelefonoFijo,
                Telefono2 = cliente.TelefonoFijo,
                Rfc = cliente.RFC,
                IdTipoPersona = cliente.IdTipoPersona,
                IdRegimenFiscal = cliente.IdTipoRegimen,
                RazonSocial = cliente.RazonSocial,
                
            };
        }

        public static DatosClientesDto FromDTO(List<Cliente> clientes)
        {
           
            List<ClienteDTO> list = new List<ClienteDTO>();
            foreach (var cliente in clientes)
            {
                list.Add(ToDTO(cliente));
            }
            return new DatosClientesDto()
            {
                clientes = list
            };
        }

        public static ClienteDTO ToDTO(Cliente cliente)
        {

            return new ClienteDTO()
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                RazonSocial = (cliente.RazonSocial != null) ? cliente.RazonSocial : null,
                Apellido1 = cliente.Apellido1,
                Apellido2 = cliente.Apellido2,
                TelefonoFijo = cliente.Telefono,
                Celular = cliente.Celular,
                IdTipoRegimen = cliente.IdRegimenFiscal.Value,
                IdTipoPersona = cliente.IdTipoPersona.Value,
                RFC = cliente.Rfc.Trim(),
                Credito = (cliente.limiteCreditoDias <= 0 && cliente.limiteCreditoMonto > 0) ? true : false,
                Factura = (cliente.Rfc.Trim()!="")? true:false
            };
        }     
    }
}
