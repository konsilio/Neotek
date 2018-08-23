package com.example.neotecknewts.sagasapp.Activity;

/**
 * Created by neotecknewts on 15/08/18.
 */

public interface SubirImagenesView {
    void showProgress(int mensaje);
    void hideProgress();

    void onSuccessRegistroPapeleta();
    void onSuccessRegistroAndroid();
}
