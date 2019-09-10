using Application.MainModule.DTOs.Respuesta;
using System;

namespace Application.MainModule.DTOs
{
    public class CFDIDTO
    {
        public int Id_RelTF { get; set; }
        public string Id_FolioVenta { get; set; }
        public byte Id_FormaPago { get; set; }
        public int Id_MetodoPago { get; set; }
        public short IdUsoCFDI { get; set; }
        public string UsoCFDI { get; set; }
        public string VersionCFDI { get; set; }
        public DateTime FechaTimbrado { get; set; }
        public string UUID { get; set; }
        public int Folio { get; set; }
        public string Serie { get; set; }
        public string URLPdf { get; set; }
        public string URLXml { get; set; }
        public string Respuesta { get; set; }
        public bool Exito { get; set; }
        public RespuestaDto RespuestaTimbrado { get; set; }
    }
}
