package com.example.neotecknewts.sagasapp.Activity;

public interface AnticipoTablaView {
    void VerificarCampos();

    void onShowProgress(int message_cargando);

    void HiddeProgress();

    void onSuccess();

    void onError(String mensaje);

    void onSuccessAndroid();

    void onError(Object ob);
}
