using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Mail.MainModule
{
    interface IMensajeCorreo
    {
        string Emisor { set; }
        IEnumerable<string> Destinatarios { set; }
        string Mensaje { set; }
        string Asunto { set; }
        void Enviar();
    }
}
