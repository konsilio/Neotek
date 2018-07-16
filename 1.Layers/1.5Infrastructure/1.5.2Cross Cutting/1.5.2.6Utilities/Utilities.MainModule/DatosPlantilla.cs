using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.MainModule
{
    public class DatosPlantilla
    {
        public Dictionary<string, string> ListaReemplazo;

        public string Emisor { set { ListaReemplazo.Add("[EMISOR]", value); } }
        public string Receptor { set { ListaReemplazo.Add("[RECEPTOR]", value); } }
        public string Comisionado { set { ListaReemplazo.Add("[COMISIONADO]",value); } }
        public string Area { set { ListaReemplazo.Add("[AREA]", value); } }
        public string NoProyecto { set { ListaReemplazo.Add("[NO_PROYECTO]", value); } }
        public string NombreProyecto { set { ListaReemplazo.Add("[NOMBRE_PROYECTO]", value); } }
        public string Folio { set { ListaReemplazo.Add("[FOLIO]", value); } }
        public string Monto { set { ListaReemplazo.Add("[MONTO]", value); } }
        public string TiempoServidor { set { ListaReemplazo.Add("[DATE_TIME_NOW]", value); } }
        public string Mensaje { set { ListaReemplazo.Add("[MENSAJE]", value);  }  }
        public string Url { set { ListaReemplazo.Add("[URL]",value); } } 
        public string UrlAceptar { set { ListaReemplazo.Add("[URL_ACEPTAR]",value); } }
        public string UrlCancelar { set { ListaReemplazo.Add("[URL_CANCELAR]", value); } }
        public string UrlVerSolicitud { set { ListaReemplazo.Add("[VER_SOLICITUD]", value); } }
        public string Motivo { set { ListaReemplazo.Add("[MOTIVO]", value); } }
        public string Proceso { set { ListaReemplazo.Add("[PROCESO]", value); } }
        public string Evento { set { ListaReemplazo.Add("[EVENTO]", value); } }
        public string ImporteAPagar { set { ListaReemplazo.Add("[IMPORTE_PAGAR]", value); } }
        public string ImporteAPagarMN { set { ListaReemplazo.Add("[IMPORTE_PAGAR_MN]", value); } }
        public string TipoCambio { set { ListaReemplazo.Add("[TIPO_CAMBIO]", value); } }
        public string FechaTipoCambio { set { ListaReemplazo.Add("[FECHA_TIPO_CAMBIO]", value); } }
        public string LeyendaTipoCambio { set { ListaReemplazo.Add("[LEYENDA_TIPO_CAMBIO]", value); } }
        public string Estatus { set { ListaReemplazo.Add("[ESTATUS]", value); } }
        public string MesSuficiencia { set { ListaReemplazo.Add("[MES_SUFICIENCIA]", value); } }
        public string Moneda { set { ListaReemplazo.Add("[MONEDA]", value); } }
        public string FechaComision { set { ListaReemplazo.Add("[DATE_TIME_COMISION]", value); } }
        public string Lugar { set { ListaReemplazo.Add("[LUGAR]", value); } }
        public string MotivoComision { set { ListaReemplazo.Add("[MOTIVO_COMISION]", value); } }
        public string DiasHabiles { set { ListaReemplazo.Add("[DIAS_HABILES]", value); } }
        public string NombreBanco { set { ListaReemplazo.Add("[BANCO]", value); } }
        public string Clabe { set { ListaReemplazo.Add("[CLABE]", value); } }
        public DatosPlantilla()
        {
            ListaReemplazo = new Dictionary<string, string>();
        }
    }
}
