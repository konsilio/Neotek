package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.DatosCalibracionDTO;

import java.util.ArrayList;

public interface CalibracionEstacionView {
    void verificarForm();
    void onSuccessList(DatosCalibracionDTO dto);
    void onError(String mensaje);
    void dialogoErrorForm(ArrayList<String> mensajes);
    void onShowProgress(int mensaje);
    void onHiddeProgress();
    void dialogoGoBack();
}
