package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;

/**
 * Created by neotecknewts on 13/08/18.
 */

public interface RegistrarPapeletaView {
    void showProgress(int mensaje);
    void hideProgress();
    void messageError(int mensaje);
    void onSuccessGetOrdenesCompraExpedidor(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO);
    void onSuccessGetOrdenesCompraPorteador(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO);
}
