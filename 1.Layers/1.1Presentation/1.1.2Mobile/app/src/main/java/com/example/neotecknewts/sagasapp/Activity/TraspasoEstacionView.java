package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.DatosTraspasoDTO;

import java.util.List;

public interface TraspasoEstacionView {
    void onSuccessList(DatosTraspasoDTO dto);
    void onError(String mensaje);
    void onShowProgress(int mensaje);
    void onHiddeProgress();
    void ValidarForm();
    void MostrarErrores(List<String> mensajes);
    void DialogoBack();
}
