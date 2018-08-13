using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mail.MainModule
{
    public class EnviarCorreos
    {
        private string _Servidor = ConfigurationManager.AppSettings["Servidor"];
        private int _Puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"]);
        private string _Contrasena = ConfigurationManager.AppSettings["Contrasena"];
        private string _De;
        private string _Mensaje;
        private string _Asunto;
        private string _RutaArchivo;
        ArrayList _DatosPara = new ArrayList();
        ArrayList _DatosCC = new ArrayList();
        ArrayList _DatosCCO = new ArrayList();

        public string ServidorCorreo
        {
            get { return _Servidor; }
            set { _Servidor = value; }
        }

        public int Puerto
        {
            get { return _Puerto; }
            set { _Puerto = value; }
        }

        public string De
        {
            get { return _De; }
            set { _De = value; }
        }

        public string Para
        {
            set { _DatosPara.Add(value); }
        }

        public string Mensaje
        {
            get { return _Mensaje; }
            set { _Mensaje = value; }
        }

        public string ConCopia
        {
            set { _DatosCC.Add(value); }
        }

        public string ConCopiaOculta
        {
            set { _DatosCCO.Add(value); }
        }

        public string Asunto
        {
            get { return _Asunto; }
            set { _Asunto = value; }
        }

        public string Contrasena
        {
            get { return _Contrasena; }
            set { _Contrasena = value; }
        }

        public string RutaArchivoAdjunto
        {
            get { return _RutaArchivo; }
            set { _RutaArchivo = value; }
        }

        public void EnviarCorreo()
        {
            //Creamos los objetos del emisor y receptor de los correos....
            MailMessage mensaje = new MailMessage();
            MailAddress de = new MailAddress(_De);

            try
            {
                mensaje.From = de;

                foreach (string contactoPara in _DatosPara)
                {
                    if(!string.IsNullOrEmpty(contactoPara))
                        mensaje.To.Add(contactoPara);
                }

                foreach (string contacotConCopia in _DatosCC)
                {
                    mensaje.CC.Add(contacotConCopia);
                }

                foreach (string contactoConCopiaOculta in _DatosCCO)
                {
                    mensaje.Bcc.Add(contactoConCopiaOculta);
                }

                //Se agrega un archivo adjunto, si es necesario.
                if ((this._RutaArchivo == null | (this._RutaArchivo == "")))
                {
                    //No intento adjuntar nada
                }
                else
                {
                    Attachment ArchivoAttachment;
                    ArchivoAttachment = new Attachment(_RutaArchivo);
                    if (System.IO.File.Exists(_RutaArchivo))
                    {
                        mensaje.Attachments.Add(ArchivoAttachment);
                    }
                    else
                    {
                        //throw new System.Exception("El archivo que desea adjuntar no exixste");
                    }
                }

                //Seguimos con el asunto del correo.
                mensaje.Subject = _Asunto;
                mensaje.SubjectEncoding = System.Text.Encoding.UTF8;

                //Y terminamos con el mensaje del correo.
                mensaje.Body = _Mensaje;
                mensaje.IsBodyHtml = true;
                mensaje.BodyEncoding = System.Text.Encoding.UTF8;

                // Se crea el objeto para el envío de correos electrónicos indicándole
                // la dirección del servidor SMTP a utilizarse.
                SmtpClient protocolo = new SmtpClient(_Servidor, _Puerto);
                protocolo.EnableSsl = false;
                System.Net.NetworkCredential credenciales = new System.Net.NetworkCredential(this._De, this._Contrasena);
                protocolo.Credentials = credenciales;

                //Se enviará el correo a su destino o destinos.
                protocolo.Send(mensaje);
            }
            catch (Exception smtpEx) //SmtpException
            {
                // En caso de suceder problemas enviando el mensaje, la descripción del
                // problema sucedido es enviada al cliente que consumió el servicio.
                smtpEx.ToString();
                throw;
            }

            //Se limpiará los recursos.
            mensaje.Dispose();
        }
    }
}
