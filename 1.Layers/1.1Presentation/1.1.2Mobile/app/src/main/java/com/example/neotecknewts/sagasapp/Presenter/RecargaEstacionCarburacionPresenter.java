package com.example.neotecknewts.sagasapp.Presenter;

public interface RecargaEstacionCarburacionPresenter {
    void getLista(String token);

    void onError(String mensaje);

    void onSuccesslista(Object datosRecargaDTO);
}
