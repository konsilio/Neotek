package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.ReporteDto;

/**
 * Created by neotecknewts on 15/08/18.
 */

public interface SubirImagenesView {
    void showProgress(int mensaje);
    void hideProgress();

    void onSuccessRegistroPapeleta();
    void onSuccessRegistroAndroid();

    void showError(String mensaje);

    void onRegistrarIniciarDescarga();

    void onSuccessRegistroRecarga();

    void onSuccessRegistroRecarga(boolean esAutoconsumoInventarioFinal, ReporteDto data);
}
