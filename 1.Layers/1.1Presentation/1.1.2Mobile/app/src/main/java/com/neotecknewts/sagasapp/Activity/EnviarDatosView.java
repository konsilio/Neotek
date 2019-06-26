package com.neotecknewts.sagasapp.Activity;

public interface EnviarDatosView {
    void onSuccessEnvio();
    void onError(String mensaje);
    void onSuccessAndroid();
    void showProgressDialog();
    void hiddenProgressDialog();
}
