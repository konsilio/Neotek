﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class TipoPersonaFiscalEnum
    {
        public static byte Fisica = (byte)tipoPersona.Fisica;
        public static byte Moral = (byte)tipoPersona.Moral;
    }

    enum tipoPersona : byte
    {
        Fisica = 1,
        Moral = 2,
    }
}
