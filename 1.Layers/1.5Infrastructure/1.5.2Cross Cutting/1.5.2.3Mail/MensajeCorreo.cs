using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Collections;

namespace Mail.MainModule
{
    public class MensajeCorreo : IMensajeCorreo, IDisposable
    {
        public string Emisor
        {
            set
            {
                _emisor = new MailAddress(value);
            }
        }

        public IEnumerable<string> Destinatarios
        {
            set
            {
                _listaDestinatarios = new ArrayList(value.ToArray());
                foreach (var dato in _listaDestinatarios)
                {
                    _destinatarios.Add(dato.ToString());
                }
            }
        }

        public string Mensaje
        {
            set
            {
                _mensaje.Body = value;
            }
        }

        public string Asunto
        {
            set
            {
                _mensaje.Subject = value;
            }
        }

        private MailAddress _emisor;
        private MailAddressCollection _destinatarios;
        private MailMessage _mensaje;
        private ArrayList _listaDestinatarios;

        //Estos tres valores deberían salir de un archivo de configuración
        private string contrasena = "xxxxxxxxxx";
        private string servidor = "mta1.infotec.com.mx";
        private int puerto = 25;

        public MensajeCorreo()
        {
            _mensaje = new MailMessage();
            _mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
            _mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            _destinatarios = new MailAddressCollection();
        }

        public void Enviar()
        {
            //Si no se ha asignado un emisor, sale sin enviar nada.
            if (_emisor == null) return;

            //Se crea el objeto para el envío de correos electrónicos indicándole la dirección del servidor SMTP a utilizarse.
            SmtpClient protocolo = new SmtpClient(servidor, puerto);

            //Creamos el cuerpo del correo. Iniciamos con el correo emisor.
            _mensaje.From = _emisor;

            //Después segimos con los correos destinatarios. Recorremos una lista de correos.
            //Destinatarios = new MailAdressCollection();
            foreach (var destino in _destinatarios)
            {
                //Destinatarios.Add(new MailAdress(destinos.ToString()));
                _mensaje.To.Add(destino);
            }

            //Autentificamos el correo.
            protocolo.Credentials = new NetworkCredential(_mensaje.From.ToString(), contrasena);
            protocolo.EnableSsl = false;
            protocolo.Send(_mensaje);

            //Revisar si esto es necesario
            _mensaje.Dispose();
            this.disposed = true;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _mensaje.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
