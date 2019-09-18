package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.DatosTraspasoDTO;

import java.util.List;

public interface TraspasoPipaView {
    void ValidarForm();
    void ErrorForm(List<String> mensaje);
    void onSuccessLista(DatosTraspasoDTO dto);
    void onError(String mensaje);
    void onShowProgress(int mensaje);
    void onHiddeProgress();
    void goback();
}
