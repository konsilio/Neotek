package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosReporteDTO;
import com.neotecknewts.sagasapp.Model.ReporteDto;

public interface ReportePresenter {
    void GetUnidades(String token);

    void onSuccessUnidades(DatosReporteDTO data) ;

    void onError(String mensaje_error);

    void Reporte(int id, String fecha, String token);

    void onSuccessReport(ReporteDto reporteDTO);

    void onError(ReporteDto reporteDTO);
}
