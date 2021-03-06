package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.DatosReporteDTO;
import com.example.neotecknewts.sagasapp.Model.ReporteDto;
import com.example.neotecknewts.sagasapp.Model.UnidadesDTO;

import java.util.ArrayList;
import java.util.List;

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
