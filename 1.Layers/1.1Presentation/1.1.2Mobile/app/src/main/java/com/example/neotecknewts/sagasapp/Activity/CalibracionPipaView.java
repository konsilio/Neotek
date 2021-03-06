package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.DatosCalibracionDTO;

import java.util.ArrayList;

public interface CalibracionPipaView {
    void validarForm();
    void errorDialog(ArrayList<String> mensaje);
    void dialogoGoBack();
    void onShowProgress(int mensaje);
    void onHiddenProgress();
    void onSuccessList(DatosCalibracionDTO dto);
    void onError(String mensaje);
}
