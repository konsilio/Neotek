﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class EstacionesDto : AlmacenDto
    {
        public MedidorDto Medidor { get; set; }
    }
}