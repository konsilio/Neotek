package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.DatosReporteDTO;
import com.neotecknewts.sagasapp.Model.ReporteDto;

import java.util.ArrayList;

public interface ReporteView {
    void VermificarCampos();
    void MensajeError(ArrayList<String> mensaje);
    void onSuccessGetUnidades(DatosReporteDTO data);
    void onErrorMessage(String mensaje);
    void onShowProgress(int mensaje);
    void hiddeProgress();

    void onSuccessReport(ReporteDto reporteDTO);

    void onErrorMessage(ReporteDto reporteDTO);
}
