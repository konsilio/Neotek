using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class AdministracionDTO
    {
        public decimal TotalCamionetas { get; set; }
        public decimal TotalPipas { get; set; }
        public decimal TotalEstaciones { get; set; }
        public decimal TotalVetna { get; set; }
        public string Json { get; set; }
    }
}
