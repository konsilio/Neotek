using Application.MainModule.DTOs;
using Mail.MainModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO
{
    public static class CorreoAdapter
    {
        public static EnviarCorreos FromDto(CorreoDto dto)
        {
            var _enviarCorreos = new EnviarCorreos()
            {
                De = dto.De,
                Mensaje = dto.Mensaje,
                Asunto = dto.Asunto,
                RutaArchivoAdjunto = dto.RutaArchivo,
            };
            
            if (!string.IsNullOrEmpty(dto.Para))
                _enviarCorreos.Para = dto.Para;

            if (dto.ParaLista != null && dto.ParaLista.Count > 0)
                foreach (string para in dto.ParaLista)
                    _enviarCorreos.Para = para;

            if (dto.ConCopia != null && dto.ConCopia.Count > 0)
                foreach (string copia in dto.ConCopia)
                    _enviarCorreos.ConCopia = copia;

            if (dto.ConCopiaOculta != null && dto.ConCopiaOculta.Count > 0)
                foreach (string copiaOculta in dto.ConCopiaOculta)
                    _enviarCorreos.ConCopia = copiaOculta;

            return _enviarCorreos;
        }
    }
}
