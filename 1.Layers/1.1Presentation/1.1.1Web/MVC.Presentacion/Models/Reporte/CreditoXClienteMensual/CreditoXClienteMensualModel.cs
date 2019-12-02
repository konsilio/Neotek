using Application.MainModule.DTOs.Cobranza;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class CreditoXClienteMensualModel
    {        
        public string Nombre { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal SaldoCorriente { get; set; }
        [Display(Name = "Saldo Vencido")]
        public decimal Vencido { get; set; }
        [Display(Name = "Dias 1 a 7")]
        public decimal Dias1a7 { get; set; }
        [Display(Name = "Dias 8 a 16")]
        public decimal Dias8a16 { get; set; }
        [Display(Name = "Dias 17 a 31")]
        public decimal Dias17a31 { get; set; }
        [Display(Name = "Dias 32 a 61")]
        public decimal Dias32a61 { get; set; }
        [Display(Name = "Dias 62 a 91")]
        public decimal Dias62a91 { get; set; }
        [Display(Name = "Mas 91")]
        public decimal Mas91 { get; set; }
   



    }
}