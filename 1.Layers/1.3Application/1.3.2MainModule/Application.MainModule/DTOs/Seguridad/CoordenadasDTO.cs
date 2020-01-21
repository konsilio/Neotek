using Application.MainModule.DTOs.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class CoordenadasDTO
    {
        public CoordenadasDTO(Empresa emp)
        {
            this.Latitud= (double)(emp.CoordenadaLat ?? 0);
            this.Longitud = (double)(emp.CoordenadaLong ?? 0);
        }
        public CoordenadasDTO(AutenticacionDto dto)
        {
            this.Latitud = Convert.ToDouble(dto.Coordenadas.Split(',')[0]);
            this.Longitud = Convert.ToDouble(dto.Coordenadas.Split(',')[1]);
        }
        public double Latitud { get; set; }

        public double Longitud { get; set; }
    }
}
