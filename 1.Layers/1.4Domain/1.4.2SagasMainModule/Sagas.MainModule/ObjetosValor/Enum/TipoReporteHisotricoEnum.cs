using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class TipoReporteHisotricoEnum
    {
        public static int General = (int)TipoReporteHisotrico.General;
        public static int CamionetaVSPipa = (int)TipoReporteHisotrico.CamionetaVSPipa;
        public static int LocalesVSForeaneos = (int)TipoReporteHisotrico.LocalesVSForeaneos;
    }
    enum TipoReporteHisotrico : int
    {
      
        General = 1,
        CamionetaVSPipa = 2,
        LocalesVSForeaneos = 3
        
    }

}
