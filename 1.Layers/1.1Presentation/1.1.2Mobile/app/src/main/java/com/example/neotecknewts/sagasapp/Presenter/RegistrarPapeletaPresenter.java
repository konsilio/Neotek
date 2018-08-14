package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;

/**
 * Created by neotecknewts on 13/08/18.
 */

public interface RegistrarPapeletaPresenter {
    void getOrdenesCompraExpedidor(int IdEmpresa, String token);
    void getOrdenesCompraPorteador(int IdEmpresa, String token);
    void onSuccessGetOrdenesCompraExpedidor(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO);
    void onSuccessGetOrdenesCompraPorteador(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO);
    void onError();
}
