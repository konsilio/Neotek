using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Seguridad
{
    public class RolCompras
    {
        public bool AppCompraVerOCompra { get; set; }
        public bool AppCompraEntraGas { get; set; }
        public bool AppCompraGasIniciarDescarga { get; set; }
        public bool AppCompraGasFinalizarDescarga { get; set; }
        public bool AppAutoconsumoInventarioGral { get; set; }
        public bool AppAutoconsumoEstacionCarb { get; set; }
        public bool AppAutoconsumoPipa { get; set; }
        public bool AppCalibracionEstacionCarb { get; set; }
        public bool AppCalibracionPipa { get; set; }
        public bool AppCalibracionCamionetaCilindro { get; set; }
        public bool AppRecargaEstacionCarb { get; set; }
        public bool AppRecargaPipa { get; set; }
        public bool AppRecargaCamionetaCilindro { get; set; }
        public bool AppTomaLecturaAlmacenPral { get; set; }
        public bool AppTomaLecturaEstacionCarb { get; set; }
        public bool AppTomaLecturaPipa { get; set; }
        public bool AppTomaLecturaCamionetaCilindro { get; set; }
        public bool AppTomaLecturaReporteDelDia { get; set; }
        public bool AppTraspasoEstacionCarb { get; set; }
        public bool AppTraspasoPipa { get; set; }
    }
}
