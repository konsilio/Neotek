using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Compras;

namespace Application.MainModule.AdaptadoresDTO.Compras
{
    public class OrdenComprasAdapter
    {
        public static RequisicionOCDTO ToDTO(Sagas.MainModule.Entidades.Requisicion _req)
        {
            RequisicionOCDTO Req = new RequisicionOCDTO();
            Req.NumeroRequisicion = _req.NumeroRequisicion;
            Req.UsuarioSolicitante = _req.Solicitante.NombreUsuario;
            Req.NombreComercial = _req.Empresa.NombreComercial;
            Req.MotivoRequisicion = _req.MotivoRequisicion;
            Req.RequeridoEn = _req.RequeridoEn;
            Req.Productos = ProductosOCAdapter.ToDTO(_req.Productos.ToList());
            return Req;
        }
    }
}
