package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.UnidadesDTO;

import java.util.ArrayList;

public interface ReporteView {
    void VermificarCampos();
    void MensajeError(ArrayList<String> mensaje);
    void onSuccessGetUnidades(ArrayList<UnidadesDTO> data);
    void onErrorMessage(String mensaje);
    void onShowProgress();
    void hiddeProgress();
}
