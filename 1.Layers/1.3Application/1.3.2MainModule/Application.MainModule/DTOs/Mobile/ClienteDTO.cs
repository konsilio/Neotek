/***
 * ClienteDTO
 * Clase de tipo modelo DTO para la sección de clientes 
 * en la aplicación mobil.
 * Developer: Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
 * Company: Neoteck
 * Date: 01/10/2018
 * Update: 01/10/2018
 * 
 **/

namespace Application.MainModule.DTOs.Mobile
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }
        public byte IdTipoPersona { get; set; }
        public short IdTipoRegimen { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Celular { get; set; }
        public string TelefonoFijo { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public bool Credito { get; set; }
        public bool Factura { get; internal set; }
    }
}
